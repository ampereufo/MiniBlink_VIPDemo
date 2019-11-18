using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MBVIP
{
    public class MBVIP_WebView : IDisposable
    {
        private IntPtr m_WebView;
        private IntPtr m_hWnd;
        private IntPtr m_OldProc;
        private bool m_noDestory;
        private mbSettings m_settings;


        private mbPaintUpdatedCallback m_mbPaintUpdatedCallback;
        private WndProcCallback m_mbWndProcCallback;
        private mbTitleChangedCallback m_mbTitleChangedCallback;
        private mbUrlChangedCallback m_mbUrlChangedCallback;
        private mbDocumentReadyCallback m_mbDocumentReadyCallback;
        private mbLoadingFinishCallback m_mbLoadingFinishCallback;
        private mbDownloadCallback m_mbDownloadCallback;
        private mbCreateViewCallback m_mbCreateViewCallback;
        private mbLoadUrlBeginCallback m_mbLoadUrlBeginCallback;
        private mbLoadUrlEndCallback m_mbLoadUrlEndCallback;


        private event EventHandler<WindowProcEventArgs> m_OnWindowProc;
        private event EventHandler<TitleChangeEventArgs> m_TitleChangeHandler = null;
        private event EventHandler<UrlChangeEventArgs> m_UrlChangeHandler = null;
        private event EventHandler<DocumentReadyEventArgs> m_DocumentReadyHandler = null;
        private event EventHandler<LoadingFinishEventArgs> m_LoadingFinishHandler = null;
        private event EventHandler<DownloadEventArgs> m_DownloadHandler = null;
        private event EventHandler<CreateViewEventArgs> m_CreateViewHandler = null;
        private event EventHandler<LoadUrlBeginEventArgs> m_LoadUrlBeginHandler = null;
        private event EventHandler<LoadUrlEndEventArgs> m_LoadUrlEndHandler = null;


        #region --------------------------- 各种事件参数 ---------------------------

        /// <summary>
        /// MB事件参数基类
        /// </summary>
        public class MiniblinkEventArgs : EventArgs
        {
            private IntPtr m_webView;

            public MiniblinkEventArgs(IntPtr webView)
            {
                m_webView = webView;
            }

            public IntPtr Handle
            {
                get { return m_webView; }
            }
        }

        /// <summary>
        /// 窗口过程事件参数
        /// </summary>
        public class WindowProcEventArgs : EventArgs
        {
            private IntPtr m_hWnd;
            private int m_msg;
            private IntPtr m_wParam;
            private IntPtr m_lParam;
            private IntPtr m_result;
            private bool m_bHand;

            public WindowProcEventArgs(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
            {
                m_hWnd = hWnd;
                m_msg = msg;
                m_wParam = wParam;
                m_lParam = lParam;
            }

            public IntPtr Handle
            {
                get { return m_hWnd; }
            }

            public int Msg
            {
                get { return m_msg; }
            }

            public IntPtr wParam
            {
                get { return m_wParam; }
            }

            public IntPtr lParam
            {
                get { return m_lParam; }
            }

            public IntPtr Result
            {
                get { return m_result; }
                set { m_result = value; }
            }

            /// <summary>
            /// 如果为 true 则会返回 Result ，false 则返回默认值
            /// </summary>
            public bool bHand
            {
                get { return m_bHand; }
                set { m_bHand = value; }
            }
        }

        /// <summary>
        /// OnTitleChange事件参数
        /// </summary>
        public class TitleChangeEventArgs : MiniblinkEventArgs
        {
            public IntPtr Title { get; }

            public TitleChangeEventArgs(IntPtr webView, IntPtr title) : base(webView)
            {
                Title = title;
            }
        }

        /// <summary>
        /// OnUrlChange事件参数
        /// </summary>
        public class UrlChangeEventArgs : MiniblinkEventArgs
        {
            public IntPtr URL { get; }

            public UrlChangeEventArgs(IntPtr webView, IntPtr url) : base(webView)
            {
                URL = url;
            }
        }

        /// <summary>
        /// OnDocumentReady事件参数
        /// </summary>
        public class DocumentReadyEventArgs : MiniblinkEventArgs
        {
            public DocumentReadyEventArgs(IntPtr webView, IntPtr frame) : base(webView)
            {
                Frame = frame;
            }

            public IntPtr Frame { get; }
        }

        /// <summary>
        /// OnLoadingFinish事件参数
        /// </summary>
        public class LoadingFinishEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;
            private IntPtr m_failedReason;

            public LoadingFinishEventArgs(IntPtr webView, IntPtr url, IntPtr frameId, mbLoadingResult result, IntPtr failedReason) : base(webView)
            {
                m_url = url;
                LoadingResult = result;
                m_failedReason = failedReason;
                FrameId = frameId;
            }

            public mbLoadingResult LoadingResult { get; }
            public IntPtr FrameId { get; }

            public string URL
            {
                get
                {
                    string strRet = null;
                    if (m_url != IntPtr.Zero)
                    {
                        strRet = Marshal.PtrToStringUni(m_url);
                    }

                    return strRet;
                }
            }

            public string FailedReason
            {
                get
                {
                    string strRet = null;
                    if (m_failedReason != IntPtr.Zero)
                    {
                        strRet = Marshal.PtrToStringUni(MBVIP_API.mbGetString(m_failedReason));
                    }

                    return strRet;
                }
            }
        }

        /// <summary>
        /// OnDownload事件参数
        /// </summary>
        public class DownloadEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;

            public DownloadEventArgs(IntPtr webView, IntPtr url) : base(webView)
            {
                m_url = url;
            }

            /// <summary>
            /// 设置是否取消，true表示取消
            /// </summary>
            public bool Cancel { get; set; }

            public string URL
            {
                get
                {
                    string strRet = null;
                    if (m_url != IntPtr.Zero)
                    {
                        strRet = MBVIP_Common.UTF8PtrToStr(m_url);
                    }

                    return strRet;
                }
            }
        }

        /// <summary>
        /// OnCreateView事件参数
        /// </summary>
        public class CreateViewEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;
            private IntPtr m_windowFeatures;

            public CreateViewEventArgs(IntPtr webView, mbNavigationType navigationType, IntPtr url, IntPtr windowFeatures) : base(webView)
            {
                NavigationType = navigationType;
                m_url = url;
                m_windowFeatures = windowFeatures;
                SetNewWebViewHandle = webView;
            }

            public mbNavigationType NavigationType { get; }

            public IntPtr SetNewWebViewHandle { get; set; }

            public string URL
            {
                get
                {
                    string strRet = null;
                    if (m_url != IntPtr.Zero)
                    {
                        strRet = Marshal.PtrToStringUni(MBVIP_API.mbGetString(m_url));
                    }

                    return strRet;
                }
            }

            public mbWindowFeatures WindowFeatures
            {
                get
                {
                    if (m_windowFeatures != IntPtr.Zero)
                    {
                        return (mbWindowFeatures)Marshal.PtrToStructure(m_windowFeatures, typeof(mbWindowFeatures));
                    }
                    else
                    {
                        return new mbWindowFeatures();
                    }
                }
            }
        }

        /// <summary>
        /// OnLoadUrlBegin事件参数
        /// </summary>
        public class LoadUrlBeginEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;

            public LoadUrlBeginEventArgs(IntPtr webView, IntPtr url, IntPtr job) : base(webView)
            {
                m_url = url;
                Job = job;
            }

            public IntPtr Job { get; }

            /// <summary>
            /// 是否取消载入，true 表示取消
            /// </summary>
            public bool Cancel { get; set; }

            public string URL
            {
                get
                {
                    string strRet = null;
                    if (m_url != IntPtr.Zero)
                    {
                        strRet = MBVIP_Common.UTF8PtrToStr(m_url);
                    }

                    return strRet;
                }
            }
        }

        /// <summary>
        /// OnLoadUrlEnd事件参数
        /// </summary>
        public class LoadUrlEndEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;
            private IntPtr m_buf;
            private int m_len;

            public LoadUrlEndEventArgs(IntPtr webView, IntPtr url, IntPtr job, IntPtr buf, int len) : base(webView)
            {
                m_url = url;
                Job = job;
                m_buf = buf;
                m_len = len;
            }

            public IntPtr Job { get; }

            public string URL
            {
                get
                {
                    string strRet = null;
                    if (m_url != IntPtr.Zero)
                    {
                        strRet = MBVIP_Common.UTF8PtrToStr(m_url);
                    }

                    return strRet;
                }
            }

            public byte[] Data
            {
                get
                {
                    byte[] data = null;
                    if (m_buf != IntPtr.Zero)
                    {
                        data = new byte[m_len];
                        Marshal.Copy(m_buf, data, 0, m_len);
                    }

                    return data;
                }
            }
        }

        #endregion

        #region --------------------------- 事件处理函数 ---------------------------

        /// <summary>
        /// 窗口刷新事件处理函数
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="hWnd"></param>
        /// <param name="hdc"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        private void mbOnPaintUpdated(IntPtr webView, IntPtr hWnd, IntPtr hdc, int x, int y, int cx, int cy)
        {
            if ((int)WinConst.WS_EX_LAYERED == ((int)WinConst.WS_EX_LAYERED & MBVIP_Common.GetWindowLong(m_hWnd, (int)WinConst.GWL_EXSTYLE)))
            {
                RECT rectDest = new RECT();
                MBVIP_Common.GetWindowRect(m_hWnd, ref rectDest);
                MBVIP_Common.OffsetRect(ref rectDest, -rectDest.Left, -rectDest.Top);

                SIZE sizeDest = new SIZE(rectDest.Right - rectDest.Left, rectDest.Bottom - rectDest.Top);
                POINT pointSource = new POINT();

                BITMAP bmp = new BITMAP();
                IntPtr hBmp = MBVIP_Common.GetCurrentObject(hdc, (int)WinConst.OBJ_BITMAP);
                MBVIP_Common.GetObject(hBmp, Marshal.SizeOf(typeof(BITMAP)), ref bmp);

                sizeDest.cx = bmp.bmWidth;
                sizeDest.cy = bmp.bmHeight;

                IntPtr hdcScreen = MBVIP_Common.GetDC(hWnd);

                BLENDFUNCTION blend = new BLENDFUNCTION();
                blend.BlendOp = (byte)WinConst.AC_SRC_OVER;
                blend.SourceConstantAlpha = 255;
                blend.AlphaFormat = (byte)WinConst.AC_SRC_ALPHA;

                int callOk = MBVIP_Common.UpdateLayeredWindow(m_hWnd, hdcScreen, IntPtr.Zero, ref sizeDest, hdc, ref pointSource, 0, ref blend, (int)WinConst.ULW_ALPHA);
                if (callOk == 0)
                {
                    IntPtr hdcMemory = MBVIP_Common.CreateCompatibleDC(hdcScreen);
                    IntPtr hbmpMemory = MBVIP_Common.CreateCompatibleBitmap(hdcScreen, sizeDest.cx, sizeDest.cy);
                    IntPtr hbmpOld = MBVIP_Common.SelectObject(hdcMemory, hbmpMemory);

                    MBVIP_Common.BitBlt(hdcMemory, 0, 0, sizeDest.cx, sizeDest.cy, hdc, 0, 0, (int)WinConst.SRCCOPY | (int)WinConst.CAPTUREBLT);
                    MBVIP_Common.BitBlt(hdc, 0, 0, sizeDest.cx, sizeDest.cy, hdcMemory, 0, 0, (int)WinConst.SRCCOPY | (int)WinConst.CAPTUREBLT);

                    callOk = MBVIP_Common.UpdateLayeredWindow(m_hWnd, hdcScreen, IntPtr.Zero, ref sizeDest, hdcMemory, ref pointSource, 0, ref blend, (int)WinConst.ULW_ALPHA);

                    MBVIP_Common.SelectObject(hdcMemory, hbmpOld);
                    MBVIP_Common.DeleteObject(hbmpMemory);
                    MBVIP_Common.DeleteDC(hdcMemory);
                }

                MBVIP_Common.ReleaseDC(m_hWnd, hdcScreen);
            }
            else
            {
                RECT rc = new RECT(x, y, x + cx, y + cy);
                MBVIP_Common.InvalidateRect(m_hWnd, ref rc, true);
            }
        }

        /// <summary>
        /// 窗口过程事件处理函数，主要处理各种消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr OnWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            int iRet = 0;
            if (m_OnWindowProc != null)
            {
                WindowProcEventArgs e = new WindowProcEventArgs(hWnd, (int)msg, wParam, lParam);
                m_OnWindowProc(this, e);
                if (e.bHand)
                {
                    return e.Result;
                }
            }

            switch (msg)
            {
                case (uint)WinConst.WM_PAINT:
                    {
                        if ((int)WinConst.WS_EX_LAYERED != ((int)WinConst.WS_EX_LAYERED & (int)MBVIP_Common.GetWindowLong(hWnd, (int)WinConst.GWL_EXSTYLE)))
                        {
                            PAINTSTRUCT ps = new PAINTSTRUCT();
                            IntPtr hdc = MBVIP_Common.BeginPaint(hWnd, ref ps);

                            RECT rcClip = ps.rcPaint;
                            RECT rcClient = new RECT();
                            MBVIP_Common.GetClientRect(hWnd, ref rcClient);

                            RECT rcInvalid = rcClient;
                            if (rcClip.Right != rcClip.Left && rcClip.Bottom != rcClip.Top)
                            {
                                MBVIP_Common.IntersectRect(ref rcInvalid, ref rcClip, ref rcClient);
                            }

                            int srcX = rcInvalid.Left - rcClient.Left;
                            int srcY = rcInvalid.Top - rcClient.Top;
                            int destX = rcInvalid.Left;
                            int destY = rcInvalid.Top;
                            int width = rcInvalid.Right - rcInvalid.Left;
                            int height = rcInvalid.Bottom - rcInvalid.Top;

                            if (0 != width && 0 != height)
                            {
                                IntPtr hMbDC = MBVIP_API.mbGetLockedViewDC(m_WebView);
                                MBVIP_Common.BitBlt(hdc, destX, destY, width, height, hMbDC, srcX, srcY, (int)WinConst.SRCCOPY);
                            }

                            MBVIP_Common.EndPaint(hWnd, ref ps);
                            return IntPtr.Zero;
                        }

                        break;
                    }
                case (uint)WinConst.WM_ERASEBKGND:
                    {
                        return new IntPtr(1);
                    }
                case (uint)WinConst.WM_SIZE:
                    {
                        int width = lParam.ToInt32() & 65535;
                        int height = lParam.ToInt32() >> 16;
                        MBVIP_API.mbResize(m_WebView, width, height);
                        break;
                    }
                case (uint)WinConst.WM_KEYDOWN:
                    {
                        uint virtualKeyCode = (uint)wParam.ToInt32();
                        uint flags = 0;
                        if (((lParam.ToInt32() >> 16) & (int)WinConst.KF_REPEAT) != 0)
                        {
                            flags |= (uint)mbKeyFlags.MB_REPEAT;
                        }

                        if (((lParam.ToInt32() >> 16) & (int)WinConst.KF_EXTENDED) != 0)
                        {
                            flags |= (uint)mbKeyFlags.MB_EXTENDED;
                        }

                        iRet = MBVIP_API.mbFireKeyDownEvent(m_WebView, virtualKeyCode, flags, 0);
                        if (iRet != 0)
                        {
                            return IntPtr.Zero;
                        }

                        break;
                    }
                case (uint)WinConst.WM_KEYUP:
                    {
                        uint virtualKeyCode = (uint)wParam.ToInt32();
                        uint flags = 0;
                        if (((lParam.ToInt32() >> 16) & (int)WinConst.KF_REPEAT) != 0)
                        {
                            flags |= (uint)mbKeyFlags.MB_REPEAT;
                        }

                        if (((lParam.ToInt32() >> 16) & (int)WinConst.KF_EXTENDED) != 0)
                        {
                            flags |= (uint)mbKeyFlags.MB_EXTENDED;
                        }

                        iRet = MBVIP_API.mbFireKeyUpEvent(m_WebView, virtualKeyCode, flags, 0);
                        if (iRet != 0)
                        {
                            return IntPtr.Zero;
                        }

                        break;
                    }
                case (uint)WinConst.WM_CHAR:
                    {
                        uint charCode = (uint)wParam.ToInt32();
                        uint flags = 0;
                        if (((lParam.ToInt32() >> 16) & (int)WinConst.KF_REPEAT) != 0)
                        {
                            flags |= (uint)mbKeyFlags.MB_REPEAT;
                        }

                        if (((lParam.ToInt32() >> 16) & (int)WinConst.KF_EXTENDED) != 0)
                        {
                            flags |= (uint)mbKeyFlags.MB_EXTENDED;
                        }

                        iRet = MBVIP_API.mbFireKeyPressEvent(m_WebView, charCode, flags, 0);
                        if (iRet != 0)
                        {
                            return IntPtr.Zero;
                        }

                        break;
                    }
                case (uint)WinConst.WM_LBUTTONDOWN:
                case (uint)WinConst.WM_MBUTTONDOWN:
                case (uint)WinConst.WM_RBUTTONDOWN:
                case (uint)WinConst.WM_LBUTTONDBLCLK:
                case (uint)WinConst.WM_MBUTTONDBLCLK:
                case (uint)WinConst.WM_RBUTTONDBLCLK:
                case (uint)WinConst.WM_LBUTTONUP:
                case (uint)WinConst.WM_MBUTTONUP:
                case (uint)WinConst.WM_RBUTTONUP:
                case (uint)WinConst.WM_MOUSEMOVE:
                    {
                        if (msg == (uint)WinConst.WM_LBUTTONDOWN || msg == (uint)WinConst.WM_MBUTTONDOWN || msg == (uint)WinConst.WM_RBUTTONDOWN)
                        {
                            if (MBVIP_Common.GetFocus() != hWnd)
                            {
                                MBVIP_Common.SetFocus(hWnd);
                            }
                            
                            MBVIP_Common.SetCapture(hWnd);
                        }
                        else if (msg == (uint)WinConst.WM_LBUTTONUP || msg == (uint)WinConst.WM_MBUTTONUP || msg == (uint)WinConst.WM_RBUTTONUP)
                        {
                            MBVIP_Common.ReleaseCapture();
                        }

                        uint flags = 0;
                        int x = MBVIP_Common.LOWORD(lParam);
                        int y = MBVIP_Common.HIWORD(lParam);

                        if ((wParam.ToInt32() & (int)WinConst.MK_CONTROL) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_CONTROL;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_SHIFT) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_SHIFT;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_LBUTTON) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_LBUTTON;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_MBUTTON) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_MBUTTON;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_RBUTTON) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_RBUTTON;
                        }

                        iRet = MBVIP_API.mbFireMouseEvent(m_WebView, msg, x, y, flags);
                        if (iRet != 0)
                        {
                            return IntPtr.Zero;
                        }

                        break;
                    }
                case (uint)WinConst.WM_CONTEXTMENU:
                    {
                        POINT pt;
                        uint flags = 0;

                        pt.x = MBVIP_Common.LOWORD(lParam);
                        pt.y = MBVIP_Common.HIWORD(lParam);

                        if (pt.x != -1 && pt.y != -1)
                        {
                            MBVIP_Common.ScreenToClient(hWnd, ref pt);
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_CONTROL) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_CONTROL;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_SHIFT) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_SHIFT;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_LBUTTON) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_LBUTTON;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_MBUTTON) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_MBUTTON;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_RBUTTON) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_RBUTTON;
                        }

                        iRet = MBVIP_API.mbFireContextMenuEvent(m_WebView, pt.x, pt.y, flags);
                        if (iRet != 0)
                        {
                            return IntPtr.Zero;
                        }

                        break;
                    }
                case (uint)WinConst.WM_MOUSEWHEEL:
                    {
                        POINT pt;
                        uint flags = 0;
                        int delta = MBVIP_Common.HIWORD(wParam);

                        pt.x = MBVIP_Common.LOWORD(lParam);
                        pt.y = MBVIP_Common.HIWORD(lParam);

                        MBVIP_Common.ScreenToClient(hWnd, ref pt);

                        if ((wParam.ToInt32() & (int)WinConst.MK_CONTROL) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_CONTROL;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_SHIFT) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_SHIFT;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_LBUTTON) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_LBUTTON;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_MBUTTON) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_MBUTTON;
                        }

                        if ((wParam.ToInt32() & (int)WinConst.MK_RBUTTON) != 0)
                        {
                            flags |= (uint)mbMouseFlags.MB_RBUTTON;
                        }

                        iRet = MBVIP_API.mbFireMouseWheelEvent(m_WebView, pt.x, pt.y, delta, flags);
                        if (iRet != 0)
                        {
                            return IntPtr.Zero;
                        }
                        
                        break;
                    }
                case (uint)WinConst.WM_SETFOCUS:
                    {
                        MBVIP_API.mbSetFocus(m_WebView);
                        return IntPtr.Zero;
                    }
                case (uint)WinConst.WM_KILLFOCUS:
                    {
                        MBVIP_API.mbKillFocus(m_WebView);
                        return IntPtr.Zero;
                    }
                case (uint)WinConst.WM_SETCURSOR:
                    {
                        iRet = MBVIP_API.mbFireWindowsMessage(m_WebView, hWnd, (uint)WinConst.WM_SETCURSOR, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                        if (iRet != 0)
                        {
                            return IntPtr.Zero;
                        }

                        break;
                    }
                case (uint)WinConst.WM_IME_STARTCOMPOSITION:
                    {
                        iRet = MBVIP_API.mbFireWindowsMessage(m_WebView, hWnd, (uint)WinConst.WM_IME_STARTCOMPOSITION, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                        if (iRet != 0)
                        {
                            return IntPtr.Zero;
                        }

                        break;
                    }
                case (uint)WinConst.WM_INPUTLANGCHANGE:
                    {
                        return MBVIP_Common.DefWindowProc(hWnd, msg, wParam, lParam);
                    }
                default:
                    {
                        break;
                    }
            }

            return MBVIP_Common.CallWindowProc(m_OldProc, hWnd, msg, wParam, lParam);
        }

        #endregion

        #region --------------------------- 自定义事件 ---------------------------

        public event EventHandler<TitleChangeEventArgs> OnTitleChange
        {
            add
            {
                if (m_TitleChangeHandler == null)
                {
                    MBVIP_API.mbOnTitleChanged(m_WebView, m_mbTitleChangedCallback, IntPtr.Zero);
                }

                m_TitleChangeHandler += value;
            }

            remove
            {
                m_TitleChangeHandler -= value;

                if (m_TitleChangeHandler == null)
                {
                    MBVIP_API.mbOnTitleChanged(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<UrlChangeEventArgs> OnUrlChange
        {
            add
            {
                if (m_UrlChangeHandler == null)
                {
                    MBVIP_API.mbOnURLChanged(m_WebView, m_mbUrlChangedCallback, IntPtr.Zero);
                }
                m_UrlChangeHandler += value;
            }

            remove
            {
                m_UrlChangeHandler -= value;
                if (m_UrlChangeHandler == null)
                {
                    MBVIP_API.mbOnURLChanged(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<DocumentReadyEventArgs> OnDocumentReady
        {
            add
            {
                if (m_DocumentReadyHandler == null)
                {
                    MBVIP_API.mbOnDocumentReady(m_WebView, m_mbDocumentReadyCallback, IntPtr.Zero);
                }
                m_DocumentReadyHandler += value;
            }

            remove
            {
                m_DocumentReadyHandler -= value;
                if (m_DocumentReadyHandler == null)
                {
                    MBVIP_API.mbOnDocumentReady(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<LoadingFinishEventArgs> OnLoadingFinish
        {
            add
            {
                if (m_LoadingFinishHandler == null)
                {
                    MBVIP_API.mbOnLoadingFinish(m_WebView, m_mbLoadingFinishCallback, IntPtr.Zero);
                }
                m_LoadingFinishHandler += value;
            }

            remove
            {
                m_LoadingFinishHandler -= value;
                if (m_LoadingFinishHandler == null)
                {
                    MBVIP_API.mbOnLoadingFinish(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<DownloadEventArgs> OnDownload
        {
            add
            {
                if (m_DownloadHandler == null)
                {
                    MBVIP_API.mbOnDownload(m_WebView, m_mbDownloadCallback, IntPtr.Zero);
                }
                m_DownloadHandler += value;
            }

            remove
            {
                m_DownloadHandler -= value;
                if (m_DownloadHandler == null)
                {
                    MBVIP_API.mbOnDownload(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<CreateViewEventArgs> OnCreateView
        {
            add
            {
                if (m_CreateViewHandler == null)
                {
                    MBVIP_API.mbOnCreateView(m_WebView, m_mbCreateViewCallback, IntPtr.Zero);
                }
                m_CreateViewHandler += value;
            }

            remove
            {
                m_CreateViewHandler -= value;
                if (m_CreateViewHandler == null)
                {
                    MBVIP_API.mbOnCreateView(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<LoadUrlBeginEventArgs> OnLoadUrlBegin
        {
            add
            {
                if (m_LoadUrlBeginHandler == null)
                {
                    MBVIP_API.mbOnLoadUrlBegin(m_WebView, m_mbLoadUrlBeginCallback, IntPtr.Zero);
                }
                m_LoadUrlBeginHandler += value;
            }

            remove
            {
                m_LoadUrlBeginHandler -= value;
                if (m_LoadUrlBeginHandler == null)
                {
                    MBVIP_API.mbOnLoadUrlBegin(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<LoadUrlEndEventArgs> OnLoadUrlEnd
        {
            add
            {
                if (m_LoadUrlEndHandler == null)
                {
                    MBVIP_API.mbOnLoadUrlEnd(m_WebView, m_mbLoadUrlEndCallback, IntPtr.Zero);
                }
                m_LoadUrlEndHandler += value;
            }

            remove
            {
                m_LoadUrlEndHandler -= value;
                if (m_LoadUrlEndHandler == null)
                {
                    MBVIP_API.mbOnLoadUrlEnd(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        #endregion

        /// <summary>
        /// RT
        /// </summary>
        protected void SetCallBack()
        {
            m_mbPaintUpdatedCallback = new mbPaintUpdatedCallback(mbOnPaintUpdated);
            m_mbWndProcCallback = new WndProcCallback(OnWndProc);
            m_mbTitleChangedCallback = new mbTitleChangedCallback((IntPtr WebView, IntPtr param, IntPtr title) =>
            {
                m_TitleChangeHandler?.Invoke(this, new TitleChangeEventArgs(WebView, title));
            });

            m_mbUrlChangedCallback = new mbUrlChangedCallback((IntPtr webView, IntPtr param, IntPtr url, int canGoBack, int canGoForward) =>
            {
                m_UrlChangeHandler?.Invoke(this, new UrlChangeEventArgs(webView, url));
            });

            m_mbDocumentReadyCallback = new mbDocumentReadyCallback((IntPtr webView, IntPtr param, IntPtr frame) =>
            {
                m_DocumentReadyHandler?.Invoke(this, new DocumentReadyEventArgs(webView, frame));
            });

            m_mbLoadingFinishCallback = new mbLoadingFinishCallback((IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, mbLoadingResult result, IntPtr failedReason) =>
            {
                m_LoadingFinishHandler?.Invoke(this, new LoadingFinishEventArgs(webView, frameId, url, result, failedReason));
            });

            m_mbDownloadCallback = new mbDownloadCallback((IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, IntPtr downloadJob) =>
            {
                int iRet = 1;
                if (m_DownloadHandler != null)
                {
                    DownloadEventArgs e = new DownloadEventArgs(webView, url);
                    m_DownloadHandler(this, e);

                    iRet = (byte)(e.Cancel ? 0 : 1);
                }

                return iRet;
            });

            m_mbCreateViewCallback = new mbCreateViewCallback((IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url, IntPtr windowFeatures) =>
            {
                IntPtr ptrRet = IntPtr.Zero;
                if (m_CreateViewHandler != null)
                {
                    CreateViewEventArgs e = new CreateViewEventArgs(webView, navigationType, url, windowFeatures);
                    m_CreateViewHandler(this, e);

                    ptrRet = e.SetNewWebViewHandle;
                }

                return ptrRet;
            });

            m_mbLoadUrlBeginCallback = new mbLoadUrlBeginCallback((IntPtr webView, IntPtr param, IntPtr url, IntPtr job) =>
            {
                int iRet = 0;
                if (m_LoadUrlBeginHandler != null)
                {
                    LoadUrlBeginEventArgs e = new LoadUrlBeginEventArgs(webView, url, job);
                    m_LoadUrlBeginHandler(this, e);

                    iRet = (byte)(e.Cancel ? 1 : 0);
                }

                return iRet;
            });

            m_mbLoadUrlEndCallback = new mbLoadUrlEndCallback((IntPtr webView, IntPtr param, IntPtr url, IntPtr job, IntPtr buf, int len) =>
            {
                if (m_LoadUrlEndHandler != null)
                {
                    LoadUrlEndEventArgs e = new LoadUrlEndEventArgs(webView, url, job, buf, len);
                    m_LoadUrlEndHandler(this, e);
                }
            });
        }

        #region --------------------------- 各种方法封装 ---------------------------

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        public MBVIP_WebView()
        {
            m_settings.mask = mbSettingMask.MB_ENABLE_NODEJS;
            MBVIP_API.mbInit(ref m_settings);

            SetCallBack();

            m_WebView = MBVIP_API.mbCreateWebView();
            MBVIP_API.mbSetHandle(m_WebView, m_hWnd);
        }

        /// <summary>
        /// 销毁WebView
        /// </summary>
        public void Dispose()
        {
            if (m_WebView != IntPtr.Zero)
            {
                if (m_OldProc != IntPtr.Zero)
                {
                    MBVIP_Common.SetWindowLong(m_hWnd, (int)WinConst.GWL_WNDPROC, m_OldProc.ToInt32());
                    m_OldProc = IntPtr.Zero;
                }

                if (!m_noDestory)
                {
                    MBVIP_API.mbSetHandle(m_WebView, IntPtr.Zero);
                    MBVIP_API.mbDestroyWebView(m_WebView);
                }

                MBVIP_API.mbUninit();

                m_WebView = IntPtr.Zero;
                m_hWnd = IntPtr.Zero;
                m_noDestory = false;
            }
        }

        /// <summary>
        /// 将m_WebView和某个窗体控件绑定，通常是pannel
        /// </summary>
        /// <param name="hWnd">控件句柄</param>
        /// <returns></returns>
        public bool Bind(IntPtr hWnd)
        {
            if (m_hWnd == hWnd)
            {
                return true;
            }
            else
            {
                m_hWnd = hWnd;
            }

            if (m_WebView == IntPtr.Zero)
            {
                m_WebView = MBVIP_API.mbCreateWebView();
                if (m_WebView == IntPtr.Zero)
                {
                    return false;
                }
            }

            MBVIP_API.mbSetHandle(m_WebView, m_hWnd);
            MBVIP_API.mbOnPaintUpdated(m_WebView, m_mbPaintUpdatedCallback, m_hWnd);

            m_OldProc = MBVIP_Common.GetWindowLongIntPtr(m_hWnd, (int)WinConst.GWL_WNDPROC);
            if (m_OldProc != Marshal.GetFunctionPointerForDelegate(m_mbWndProcCallback))
            {
                m_OldProc = MBVIP_Common.SetWindowLongDelegate(m_hWnd, (int)WinConst.GWL_WNDPROC, m_mbWndProcCallback);
            }

            RECT rc = new RECT();
            MBVIP_Common.GetClientRect(m_hWnd, ref rc);
            MBVIP_API.mbResize(m_WebView, rc.Right - rc.Left, rc.Bottom - rc.Top);

            return true;
        }

        public void LoadUrl(string strUrl)
        {
            MBVIP_API.mbLoadURL(m_WebView, strUrl);
        }

        #endregion
    }
}
