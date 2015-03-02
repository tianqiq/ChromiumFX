// Copyright (c) 2014-2015 Wolfgang Borgsmüller
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// 1. Redistributions of source code must retain the above copyright 
//    notice, this list of conditions and the following disclaimer.
// 
// 2. Redistributions in binary form must reproduce the above copyright 
//    notice, this list of conditions and the following disclaimer in the 
//    documentation and/or other materials provided with the distribution.
// 
// 3. Neither the name of the copyright holder nor the names of its 
//    contributors may be used to endorse or promote products derived 
//    from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE 
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS 
// OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND 
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
// TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.



using System;
using System.Windows.Forms;
using Chromium.Remote;

namespace Chromium.WebBrowser {

    /// <summary>
    /// Represents a javascript function in the render process.
    /// </summary>
    public class JSFunction {

        internal CfrV8Handler v8Handler;
        internal CfrV8Value v8Function;

        /// <summary>
        /// Event to be called for this function.
        /// </summary>
        public event CfrV8HandlerExecuteEventHandler Execute;

        /// <summary>
        /// Name for this function in the frame's global object.
        /// </summary>
        public string FunctionName { get; private set; }

        /// <summary>
        /// The browser this function belongs to. 
        /// </summary>
        public ChromiumWebBrowser Browser { get; internal set; }
        
        /// <summary>
        /// If true, then the function is executed on the thread that owns the browser's 
        /// underlying window handle. Preserves affinity to the render thread.
        /// </summary>
        public bool InvokeOnBrowser { get; set; }
        
        /// <summary>
        /// Creates a JS Function for registration with a
        /// ChromiumWebBrowser control.
        /// </summary>
        public JSFunction(string functionName) {
            this.FunctionName = functionName;
        }

        /// <summary>
        /// Creates a JS Function for registration with a
        /// ChromiumWebBrowser control.
        /// If invokeOnBrowser is true, then the function is executed on the thread that 
        /// owns the browser's underlying window handle. Preserves affinity to the render thread.
        /// </summary>
        public JSFunction(string functionName, bool invokeOnBrowser) {
            this.FunctionName = functionName;
            this.InvokeOnBrowser = invokeOnBrowser;
        }

        internal void SetV8Handler(CfrV8Handler handler) {
            handler.Execute += new CfrV8HandlerExecuteEventHandler(handler_Execute);
        }

        private void handler_Execute(object sender, CfrV8HandlerExecuteEventArgs e) {
            var eventHandler = Execute;
            if(eventHandler != null) {
                if(InvokeOnBrowser) {
                    Browser.RenderThreadInvoke((MethodInvoker)(() => { eventHandler(this, e); }));
                } else {
                    eventHandler(this, e);
                }
            }
        }
    }
}
