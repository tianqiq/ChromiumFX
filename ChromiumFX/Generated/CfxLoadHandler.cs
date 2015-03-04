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
    /// Implement this structure to handle events related to browser load status. The
    /// functions of this structure will be called on the browser process UI thread
    /// or render process main thread (TID_RENDERER).
    /// </summary>
    public class CfxLoadHandler : CfxBase {

        internal static CfxLoadHandler Wrap(IntPtr nativePtr) {
            if(nativePtr == IntPtr.Zero) return null;
            var handlePtr = CfxApi.cfx_load_handler_get_gc_handle(nativePtr);
            return (CfxLoadHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(handlePtr).Target;
        }


        internal static void on_loading_state_change(IntPtr gcHandlePtr, IntPtr browser, int isLoading, int canGoBack, int canGoForward) {
            var self = (CfxLoadHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null) {
                return;
            }
            var e = new CfxOnLoadingStateChangeEventArgs(browser, isLoading, canGoBack, canGoForward);
            var eventHandler = self.m_OnLoadingStateChange;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_browser_wrapped == null) CfxApi.cfx_release(e.m_browser);
        }

        internal static void on_load_start(IntPtr gcHandlePtr, IntPtr browser, IntPtr frame) {
            var self = (CfxLoadHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null) {
                return;
            }
            var e = new CfxOnLoadStartEventArgs(browser, frame);
            var eventHandler = self.m_OnLoadStart;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_browser_wrapped == null) CfxApi.cfx_release(e.m_browser);
            if(e.m_frame_wrapped == null) CfxApi.cfx_release(e.m_frame);
        }

        internal static void on_load_end(IntPtr gcHandlePtr, IntPtr browser, IntPtr frame, int httpStatusCode) {
            var self = (CfxLoadHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null) {
                return;
            }
            var e = new CfxOnLoadEndEventArgs(browser, frame, httpStatusCode);
            var eventHandler = self.m_OnLoadEnd;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_browser_wrapped == null) CfxApi.cfx_release(e.m_browser);
            if(e.m_frame_wrapped == null) CfxApi.cfx_release(e.m_frame);
        }

        internal static void on_load_error(IntPtr gcHandlePtr, IntPtr browser, IntPtr frame, CfxErrorCode errorCode, IntPtr errorText_str, int errorText_length, IntPtr failedUrl_str, int failedUrl_length) {
            var self = (CfxLoadHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null) {
                return;
            }
            var e = new CfxOnLoadErrorEventArgs(browser, frame, errorCode, errorText_str, errorText_length, failedUrl_str, failedUrl_length);
            var eventHandler = self.m_OnLoadError;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_browser_wrapped == null) CfxApi.cfx_release(e.m_browser);
            if(e.m_frame_wrapped == null) CfxApi.cfx_release(e.m_frame);
        }

        internal CfxLoadHandler(IntPtr nativePtr) : base(nativePtr) {}
        public CfxLoadHandler() : base(CfxApi.cfx_load_handler_ctor) {}

        /// <summary>
        /// Called when the loading state has changed. This callback will be executed
        /// twice -- once when loading is initiated either programmatically or by user
        /// action, and once when loading is terminated due to completion, cancellation
        /// of failure.
        /// </summary>
        public event CfxOnLoadingStateChangeEventHandler OnLoadingStateChange {
            add {
                if(m_OnLoadingStateChange == null) {
                    CfxApi.cfx_load_handler_activate_callback(NativePtr, 0, 1);
                }
                m_OnLoadingStateChange += value;
            }
            remove {
                m_OnLoadingStateChange -= value;
                if(m_OnLoadingStateChange == null) {
                    CfxApi.cfx_load_handler_activate_callback(NativePtr, 0, 0);
                }
            }
        }

        private CfxOnLoadingStateChangeEventHandler m_OnLoadingStateChange;

        /// <summary>
        /// Called when the browser begins loading a frame. The |Frame| value will
        /// never be NULL -- call the is_main() function to check if this frame is the
        /// main frame. Multiple frames may be loading at the same time. Sub-frames may
        /// start or continue loading after the main frame load has ended. This
        /// function may not be called for a particular frame if the load request for
        /// that frame fails. For notification of overall browser load status use
        /// OnLoadingStateChange instead.
        /// </summary>
        public event CfxOnLoadStartEventHandler OnLoadStart {
            add {
                if(m_OnLoadStart == null) {
                    CfxApi.cfx_load_handler_activate_callback(NativePtr, 1, 1);
                }
                m_OnLoadStart += value;
            }
            remove {
                m_OnLoadStart -= value;
                if(m_OnLoadStart == null) {
                    CfxApi.cfx_load_handler_activate_callback(NativePtr, 1, 0);
                }
            }
        }

        private CfxOnLoadStartEventHandler m_OnLoadStart;

        /// <summary>
        /// Called when the browser is done loading a frame. The |Frame| value will
        /// never be NULL -- call the is_main() function to check if this frame is the
        /// main frame. Multiple frames may be loading at the same time. Sub-frames may
        /// start or continue loading after the main frame load has ended. This
        /// function will always be called for all frames irrespective of whether the
        /// request completes successfully.
        /// </summary>
        public event CfxOnLoadEndEventHandler OnLoadEnd {
            add {
                if(m_OnLoadEnd == null) {
                    CfxApi.cfx_load_handler_activate_callback(NativePtr, 2, 1);
                }
                m_OnLoadEnd += value;
            }
            remove {
                m_OnLoadEnd -= value;
                if(m_OnLoadEnd == null) {
                    CfxApi.cfx_load_handler_activate_callback(NativePtr, 2, 0);
                }
            }
        }

        private CfxOnLoadEndEventHandler m_OnLoadEnd;

        /// <summary>
        /// Called when the resource load for a navigation fails or is canceled.
        /// |ErrorCode| is the error code number, |ErrorText| is the error text and
        /// |FailedUrl| is the URL that failed to load. See net\base\net_error_list.h
        /// for complete descriptions of the error codes.
        /// </summary>
        public event CfxOnLoadErrorEventHandler OnLoadError {
            add {
                if(m_OnLoadError == null) {
                    CfxApi.cfx_load_handler_activate_callback(NativePtr, 3, 1);
                }
                m_OnLoadError += value;
            }
            remove {
                m_OnLoadError -= value;
                if(m_OnLoadError == null) {
                    CfxApi.cfx_load_handler_activate_callback(NativePtr, 3, 0);
                }
            }
        }

        private CfxOnLoadErrorEventHandler m_OnLoadError;

        internal override void OnDispose(IntPtr nativePtr) {
            if(m_OnLoadingStateChange != null) {
                m_OnLoadingStateChange = null;
                CfxApi.cfx_load_handler_activate_callback(NativePtr, 0, 0);
            }
            if(m_OnLoadStart != null) {
                m_OnLoadStart = null;
                CfxApi.cfx_load_handler_activate_callback(NativePtr, 1, 0);
            }
            if(m_OnLoadEnd != null) {
                m_OnLoadEnd = null;
                CfxApi.cfx_load_handler_activate_callback(NativePtr, 2, 0);
            }
            if(m_OnLoadError != null) {
                m_OnLoadError = null;
                CfxApi.cfx_load_handler_activate_callback(NativePtr, 3, 0);
            }
            base.OnDispose(nativePtr);
        }
    }


    public delegate void CfxOnLoadingStateChangeEventHandler(object sender, CfxOnLoadingStateChangeEventArgs e);

    /// <summary>
    /// Called when the loading state has changed. This callback will be executed
    /// twice -- once when loading is initiated either programmatically or by user
    /// action, and once when loading is terminated due to completion, cancellation
    /// of failure.
    /// </summary>
    public class CfxOnLoadingStateChangeEventArgs : CfxEventArgs {

        internal IntPtr m_browser;
        internal CfxBrowser m_browser_wrapped;
        internal int m_isLoading;
        internal int m_canGoBack;
        internal int m_canGoForward;

        internal CfxOnLoadingStateChangeEventArgs(IntPtr browser, int isLoading, int canGoBack, int canGoForward) {
            m_browser = browser;
            m_isLoading = isLoading;
            m_canGoBack = canGoBack;
            m_canGoForward = canGoForward;
        }

        public CfxBrowser Browser {
            get {
                CheckAccess();
                if(m_browser_wrapped == null) m_browser_wrapped = CfxBrowser.Wrap(m_browser);
                return m_browser_wrapped;
            }
        }
        public bool IsLoading {
            get {
                CheckAccess();
                return 0 != m_isLoading;
            }
        }
        public bool CanGoBack {
            get {
                CheckAccess();
                return 0 != m_canGoBack;
            }
        }
        public bool CanGoForward {
            get {
                CheckAccess();
                return 0 != m_canGoForward;
            }
        }

        public override string ToString() {
            return String.Format("Browser={{{0}}}, IsLoading={{{1}}}, CanGoBack={{{2}}}, CanGoForward={{{3}}}", Browser, IsLoading, CanGoBack, CanGoForward);
        }
    }

    public delegate void CfxOnLoadStartEventHandler(object sender, CfxOnLoadStartEventArgs e);

    /// <summary>
    /// Called when the browser begins loading a frame. The |Frame| value will
    /// never be NULL -- call the is_main() function to check if this frame is the
    /// main frame. Multiple frames may be loading at the same time. Sub-frames may
    /// start or continue loading after the main frame load has ended. This
    /// function may not be called for a particular frame if the load request for
    /// that frame fails. For notification of overall browser load status use
    /// OnLoadingStateChange instead.
    /// </summary>
    public class CfxOnLoadStartEventArgs : CfxEventArgs {

        internal IntPtr m_browser;
        internal CfxBrowser m_browser_wrapped;
        internal IntPtr m_frame;
        internal CfxFrame m_frame_wrapped;

        internal CfxOnLoadStartEventArgs(IntPtr browser, IntPtr frame) {
            m_browser = browser;
            m_frame = frame;
        }

        public CfxBrowser Browser {
            get {
                CheckAccess();
                if(m_browser_wrapped == null) m_browser_wrapped = CfxBrowser.Wrap(m_browser);
                return m_browser_wrapped;
            }
        }
        public CfxFrame Frame {
            get {
                CheckAccess();
                if(m_frame_wrapped == null) m_frame_wrapped = CfxFrame.Wrap(m_frame);
                return m_frame_wrapped;
            }
        }

        public override string ToString() {
            return String.Format("Browser={{{0}}}, Frame={{{1}}}", Browser, Frame);
        }
    }

    public delegate void CfxOnLoadEndEventHandler(object sender, CfxOnLoadEndEventArgs e);

    /// <summary>
    /// Called when the browser is done loading a frame. The |Frame| value will
    /// never be NULL -- call the is_main() function to check if this frame is the
    /// main frame. Multiple frames may be loading at the same time. Sub-frames may
    /// start or continue loading after the main frame load has ended. This
    /// function will always be called for all frames irrespective of whether the
    /// request completes successfully.
    /// </summary>
    public class CfxOnLoadEndEventArgs : CfxEventArgs {

        internal IntPtr m_browser;
        internal CfxBrowser m_browser_wrapped;
        internal IntPtr m_frame;
        internal CfxFrame m_frame_wrapped;
        internal int m_httpStatusCode;

        internal CfxOnLoadEndEventArgs(IntPtr browser, IntPtr frame, int httpStatusCode) {
            m_browser = browser;
            m_frame = frame;
            m_httpStatusCode = httpStatusCode;
        }

        public CfxBrowser Browser {
            get {
                CheckAccess();
                if(m_browser_wrapped == null) m_browser_wrapped = CfxBrowser.Wrap(m_browser);
                return m_browser_wrapped;
            }
        }
        public CfxFrame Frame {
            get {
                CheckAccess();
                if(m_frame_wrapped == null) m_frame_wrapped = CfxFrame.Wrap(m_frame);
                return m_frame_wrapped;
            }
        }
        public int HttpStatusCode {
            get {
                CheckAccess();
                return m_httpStatusCode;
            }
        }

        public override string ToString() {
            return String.Format("Browser={{{0}}}, Frame={{{1}}}, HttpStatusCode={{{2}}}", Browser, Frame, HttpStatusCode);
        }
    }

    public delegate void CfxOnLoadErrorEventHandler(object sender, CfxOnLoadErrorEventArgs e);

    /// <summary>
    /// Called when the resource load for a navigation fails or is canceled.
    /// |ErrorCode| is the error code number, |ErrorText| is the error text and
    /// |FailedUrl| is the URL that failed to load. See net\base\net_error_list.h
    /// for complete descriptions of the error codes.
    /// </summary>
    public class CfxOnLoadErrorEventArgs : CfxEventArgs {

        internal IntPtr m_browser;
        internal CfxBrowser m_browser_wrapped;
        internal IntPtr m_frame;
        internal CfxFrame m_frame_wrapped;
        internal CfxErrorCode m_errorCode;
        internal IntPtr m_errorText_str;
        internal int m_errorText_length;
        internal string m_errorText;
        internal IntPtr m_failedUrl_str;
        internal int m_failedUrl_length;
        internal string m_failedUrl;

        internal CfxOnLoadErrorEventArgs(IntPtr browser, IntPtr frame, CfxErrorCode errorCode, IntPtr errorText_str, int errorText_length, IntPtr failedUrl_str, int failedUrl_length) {
            m_browser = browser;
            m_frame = frame;
            m_errorCode = errorCode;
            m_errorText_str = errorText_str;
            m_errorText_length = errorText_length;
            m_failedUrl_str = failedUrl_str;
            m_failedUrl_length = failedUrl_length;
        }

        public CfxBrowser Browser {
            get {
                CheckAccess();
                if(m_browser_wrapped == null) m_browser_wrapped = CfxBrowser.Wrap(m_browser);
                return m_browser_wrapped;
            }
        }
        public CfxFrame Frame {
            get {
                CheckAccess();
                if(m_frame_wrapped == null) m_frame_wrapped = CfxFrame.Wrap(m_frame);
                return m_frame_wrapped;
            }
        }
        public CfxErrorCode ErrorCode {
            get {
                CheckAccess();
                return m_errorCode;
            }
        }
        public string ErrorText {
            get {
                CheckAccess();
                if(m_errorText == null && m_errorText_str != IntPtr.Zero) m_errorText = System.Runtime.InteropServices.Marshal.PtrToStringUni(m_errorText_str, m_errorText_length);
                return m_errorText;
            }
        }
        public string FailedUrl {
            get {
                CheckAccess();
                if(m_failedUrl == null && m_failedUrl_str != IntPtr.Zero) m_failedUrl = System.Runtime.InteropServices.Marshal.PtrToStringUni(m_failedUrl_str, m_failedUrl_length);
                return m_failedUrl;
            }
        }

        public override string ToString() {
            return String.Format("Browser={{{0}}}, Frame={{{1}}}, ErrorCode={{{2}}}, ErrorText={{{3}}}, FailedUrl={{{4}}}", Browser, Frame, ErrorCode, ErrorText, FailedUrl);
        }
    }

}
