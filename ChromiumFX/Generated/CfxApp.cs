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

// Generated file. Do not edit.


using System;

namespace Chromium {
    /// <summary>
    /// Implement this structure to provide handler implementations. Methods will be
    /// called by the process and/or thread indicated.
    /// </summary>
    public class CfxApp : CfxBase {

        internal static CfxApp Wrap(IntPtr nativePtr) {
            if(nativePtr == IntPtr.Zero) return null;
            var handlePtr = CfxApi.cfx_app_get_gc_handle(nativePtr);
            return (CfxApp)System.Runtime.InteropServices.GCHandle.FromIntPtr(handlePtr).Target;
        }


        internal static void on_before_command_line_processing(IntPtr gcHandlePtr, IntPtr process_type_str, int process_type_length, IntPtr command_line) {
            var self = (CfxApp)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null) {
                return;
            }
            var e = new CfxOnBeforeCommandLineProcessingEventArgs(process_type_str, process_type_length, command_line);
            var eventHandler = self.m_OnBeforeCommandLineProcessing;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_command_line_wrapped == null) {
                CfxApi.cfx_release(e.m_command_line);
            } else {
                e.m_command_line_wrapped.Dispose();
            }
        }

        internal static void on_register_custom_schemes(IntPtr gcHandlePtr, IntPtr registrar) {
            var self = (CfxApp)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null) {
                return;
            }
            var e = new CfxOnRegisterCustomSchemesEventArgs(registrar);
            var eventHandler = self.m_OnRegisterCustomSchemes;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_registrar_wrapped == null) {
                CfxApi.cfx_release(e.m_registrar);
            } else {
                e.m_registrar_wrapped.Dispose();
            }
        }

        internal static void get_resource_bundle_handler(IntPtr gcHandlePtr, out IntPtr __retval) {
            var self = (CfxApp)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null) {
                __retval = default(IntPtr);
                return;
            }
            var e = new CfxGetResourceBundleHandlerEventArgs();
            var eventHandler = self.m_GetResourceBundleHandler;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            __retval = CfxResourceBundleHandler.Unwrap(e.m_returnValue);
        }

        internal static void get_browser_process_handler(IntPtr gcHandlePtr, out IntPtr __retval) {
            var self = (CfxApp)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null) {
                __retval = default(IntPtr);
                return;
            }
            var e = new CfxGetBrowserProcessHandlerEventArgs();
            var eventHandler = self.m_GetBrowserProcessHandler;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            __retval = CfxBrowserProcessHandler.Unwrap(e.m_returnValue);
        }

        internal static void get_render_process_handler(IntPtr gcHandlePtr, out IntPtr __retval) {
            var self = (CfxApp)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null) {
                __retval = default(IntPtr);
                return;
            }
            var e = new CfxGetRenderProcessHandlerEventArgs();
            var eventHandler = self.m_GetRenderProcessHandler;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            __retval = CfxRenderProcessHandler.Unwrap(e.m_returnValue);
        }

        internal CfxApp(IntPtr nativePtr) : base(nativePtr) {}
        public CfxApp() : base(CfxApi.cfx_app_ctor) {}

        /// <summary>
        /// Provides an opportunity to view and/or modify command-line arguments before
        /// processing by CEF and Chromium. The |ProcessType| value will be NULL for
        /// the browser process. Do not keep a reference to the CfxCommandLine
        /// object passed to this function. The CfxSettings.CommandLineArgsDisabled
        /// value can be used to start with an NULL command-line object. Any values
        /// specified in CfxSettings that equate to command-line arguments will be set
        /// before this function is called. Be cautious when using this function to
        /// modify command-line arguments for non-browser processes as this may result
        /// in undefined behavior including crashes.
        /// </summary>
        public event CfxOnBeforeCommandLineProcessingEventHandler OnBeforeCommandLineProcessing {
            add {
                if(m_OnBeforeCommandLineProcessing == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 0, 1);
                }
                m_OnBeforeCommandLineProcessing += value;
            }
            remove {
                m_OnBeforeCommandLineProcessing -= value;
                if(m_OnBeforeCommandLineProcessing == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 0, 0);
                }
            }
        }

        private CfxOnBeforeCommandLineProcessingEventHandler m_OnBeforeCommandLineProcessing;

        /// <summary>
        /// Provides an opportunity to register custom schemes. Do not keep a reference
        /// to the |Registrar| object. This function is called on the main thread for
        /// each process and the registered schemes should be the same across all
        /// processes.
        /// </summary>
        public event CfxOnRegisterCustomSchemesEventHandler OnRegisterCustomSchemes {
            add {
                if(m_OnRegisterCustomSchemes == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 1, 1);
                }
                m_OnRegisterCustomSchemes += value;
            }
            remove {
                m_OnRegisterCustomSchemes -= value;
                if(m_OnRegisterCustomSchemes == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 1, 0);
                }
            }
        }

        private CfxOnRegisterCustomSchemesEventHandler m_OnRegisterCustomSchemes;

        /// <summary>
        /// Return the handler for resource bundle events. If
        /// CfxSettings.PackLoadingDisabled is true (1) a handler must be returned.
        /// If no handler is returned resources will be loaded from pack files. This
        /// function is called by the browser and render processes on multiple threads.
        /// </summary>
        public event CfxGetResourceBundleHandlerEventHandler GetResourceBundleHandler {
            add {
                if(m_GetResourceBundleHandler == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 2, 1);
                }
                m_GetResourceBundleHandler += value;
            }
            remove {
                m_GetResourceBundleHandler -= value;
                if(m_GetResourceBundleHandler == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 2, 0);
                }
            }
        }

        private CfxGetResourceBundleHandlerEventHandler m_GetResourceBundleHandler;

        /// <summary>
        /// Return the handler for functionality specific to the browser process. This
        /// function is called on multiple threads in the browser process.
        /// </summary>
        public event CfxGetBrowserProcessHandlerEventHandler GetBrowserProcessHandler {
            add {
                if(m_GetBrowserProcessHandler == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 3, 1);
                }
                m_GetBrowserProcessHandler += value;
            }
            remove {
                m_GetBrowserProcessHandler -= value;
                if(m_GetBrowserProcessHandler == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 3, 0);
                }
            }
        }

        private CfxGetBrowserProcessHandlerEventHandler m_GetBrowserProcessHandler;

        /// <summary>
        /// Return the handler for functionality specific to the render process. This
        /// function is called on the render process main thread.
        /// </summary>
        public event CfxGetRenderProcessHandlerEventHandler GetRenderProcessHandler {
            add {
                if(m_GetRenderProcessHandler == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 4, 1);
                }
                m_GetRenderProcessHandler += value;
            }
            remove {
                m_GetRenderProcessHandler -= value;
                if(m_GetRenderProcessHandler == null) {
                    CfxApi.cfx_app_activate_callback(NativePtr, 4, 0);
                }
            }
        }

        private CfxGetRenderProcessHandlerEventHandler m_GetRenderProcessHandler;

        internal override void OnDispose(IntPtr nativePtr) {
            if(m_OnBeforeCommandLineProcessing != null) {
                m_OnBeforeCommandLineProcessing = null;
                CfxApi.cfx_app_activate_callback(NativePtr, 0, 0);
            }
            if(m_OnRegisterCustomSchemes != null) {
                m_OnRegisterCustomSchemes = null;
                CfxApi.cfx_app_activate_callback(NativePtr, 1, 0);
            }
            if(m_GetResourceBundleHandler != null) {
                m_GetResourceBundleHandler = null;
                CfxApi.cfx_app_activate_callback(NativePtr, 2, 0);
            }
            if(m_GetBrowserProcessHandler != null) {
                m_GetBrowserProcessHandler = null;
                CfxApi.cfx_app_activate_callback(NativePtr, 3, 0);
            }
            if(m_GetRenderProcessHandler != null) {
                m_GetRenderProcessHandler = null;
                CfxApi.cfx_app_activate_callback(NativePtr, 4, 0);
            }
            base.OnDispose(nativePtr);
        }
    }


    public delegate void CfxOnBeforeCommandLineProcessingEventHandler(object sender, CfxOnBeforeCommandLineProcessingEventArgs e);

    /// <summary>
    /// Provides an opportunity to view and/or modify command-line arguments before
    /// processing by CEF and Chromium. The |ProcessType| value will be NULL for
    /// the browser process. Do not keep a reference to the CfxCommandLine
    /// object passed to this function. The CfxSettings.CommandLineArgsDisabled
    /// value can be used to start with an NULL command-line object. Any values
    /// specified in CfxSettings that equate to command-line arguments will be set
    /// before this function is called. Be cautious when using this function to
    /// modify command-line arguments for non-browser processes as this may result
    /// in undefined behavior including crashes.
    /// </summary>
    public class CfxOnBeforeCommandLineProcessingEventArgs : CfxEventArgs {

        internal IntPtr m_process_type_str;
        internal int m_process_type_length;
        internal string m_process_type;
        internal IntPtr m_command_line;
        internal CfxCommandLine m_command_line_wrapped;

        internal CfxOnBeforeCommandLineProcessingEventArgs(IntPtr process_type_str, int process_type_length, IntPtr command_line) {
            m_process_type_str = process_type_str;
            m_process_type_length = process_type_length;
            m_command_line = command_line;
        }

        public string ProcessType {
            get {
                CheckAccess();
                if(m_process_type == null && m_process_type_str != IntPtr.Zero) m_process_type = System.Runtime.InteropServices.Marshal.PtrToStringUni(m_process_type_str, m_process_type_length);
                return m_process_type;
            }
        }
        public CfxCommandLine CommandLine {
            get {
                CheckAccess();
                if(m_command_line_wrapped == null) m_command_line_wrapped = CfxCommandLine.Wrap(m_command_line);
                return m_command_line_wrapped;
            }
        }

        public override string ToString() {
            return String.Format("ProcessType={{{0}}}, CommandLine={{{1}}}", ProcessType, CommandLine);
        }
    }

    public delegate void CfxOnRegisterCustomSchemesEventHandler(object sender, CfxOnRegisterCustomSchemesEventArgs e);

    /// <summary>
    /// Provides an opportunity to register custom schemes. Do not keep a reference
    /// to the |Registrar| object. This function is called on the main thread for
    /// each process and the registered schemes should be the same across all
    /// processes.
    /// </summary>
    public class CfxOnRegisterCustomSchemesEventArgs : CfxEventArgs {

        internal IntPtr m_registrar;
        internal CfxSchemeRegistrar m_registrar_wrapped;

        internal CfxOnRegisterCustomSchemesEventArgs(IntPtr registrar) {
            m_registrar = registrar;
        }

        public CfxSchemeRegistrar Registrar {
            get {
                CheckAccess();
                if(m_registrar_wrapped == null) m_registrar_wrapped = CfxSchemeRegistrar.Wrap(m_registrar);
                return m_registrar_wrapped;
            }
        }

        public override string ToString() {
            return String.Format("Registrar={{{0}}}", Registrar);
        }
    }

    public delegate void CfxGetResourceBundleHandlerEventHandler(object sender, CfxGetResourceBundleHandlerEventArgs e);

    /// <summary>
    /// Return the handler for resource bundle events. If
    /// CfxSettings.PackLoadingDisabled is true (1) a handler must be returned.
    /// If no handler is returned resources will be loaded from pack files. This
    /// function is called by the browser and render processes on multiple threads.
    /// </summary>
    public class CfxGetResourceBundleHandlerEventArgs : CfxEventArgs {


        internal CfxResourceBundleHandler m_returnValue;
        private bool returnValueSet;

        internal CfxGetResourceBundleHandlerEventArgs() {
        }

        public void SetReturnValue(CfxResourceBundleHandler returnValue) {
            CheckAccess();
            if(returnValueSet) {
                throw new CfxException("The return value has already been set");
            }
            returnValueSet = true;
            this.m_returnValue = returnValue;
        }
    }

    public delegate void CfxGetBrowserProcessHandlerEventHandler(object sender, CfxGetBrowserProcessHandlerEventArgs e);

    /// <summary>
    /// Return the handler for functionality specific to the browser process. This
    /// function is called on multiple threads in the browser process.
    /// </summary>
    public class CfxGetBrowserProcessHandlerEventArgs : CfxEventArgs {


        internal CfxBrowserProcessHandler m_returnValue;
        private bool returnValueSet;

        internal CfxGetBrowserProcessHandlerEventArgs() {
        }

        public void SetReturnValue(CfxBrowserProcessHandler returnValue) {
            CheckAccess();
            if(returnValueSet) {
                throw new CfxException("The return value has already been set");
            }
            returnValueSet = true;
            this.m_returnValue = returnValue;
        }
    }

    public delegate void CfxGetRenderProcessHandlerEventHandler(object sender, CfxGetRenderProcessHandlerEventArgs e);

    /// <summary>
    /// Return the handler for functionality specific to the render process. This
    /// function is called on the render process main thread.
    /// </summary>
    public class CfxGetRenderProcessHandlerEventArgs : CfxEventArgs {


        internal CfxRenderProcessHandler m_returnValue;
        private bool returnValueSet;

        internal CfxGetRenderProcessHandlerEventArgs() {
        }

        public void SetReturnValue(CfxRenderProcessHandler returnValue) {
            CheckAccess();
            if(returnValueSet) {
                throw new CfxException("The return value has already been set");
            }
            returnValueSet = true;
            this.m_returnValue = returnValue;
        }
    }

}
