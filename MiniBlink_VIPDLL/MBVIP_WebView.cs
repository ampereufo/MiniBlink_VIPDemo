using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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


        // 定义回调变量
        private WndProcCallback m_mbWndProcCallback = null;
        private mbPaintUpdatedCallback m_mbPaintUpdatedCallback = null;
        private mbPaintBitUpdatedCallback m_mbPaintBitUpdatedCallback = null;
        private mbOnBlinkThreadInitCallback m_mbBlinkThreadInitCallback = null;
        private mbOnGetPdfPageDataCallback m_mbGetPdfPageDataCallback = null;
        private mbRunJsCallback m_mbRunJsCallback = null;
        private mbJsQueryCallback m_mbJsQueryCallback = null;
        private mbTitleChangedCallback m_mbTitleChangedCallback = null;
        private mbMouseOverUrlChangedCallback m_mbMouseOverUrlChangedCallback = null;
        private mbUrlChangedCallback m_mbUrlChangedCallback = null;
        private mbUrlChangedCallback2 m_mbUrlChangedCallback2 = null;
        private mbAlertBoxCallback m_mbAlertBoxCallback = null;
        private mbConfirmBoxCallback m_mbConfirmBoxCallback = null;
        private mbPromptBoxCallback m_mbPromptBoxCallback = null;
        private mbNavigationCallback m_mbNavigationCallback = null;
        private mbCreateViewCallback m_mbCreateViewCallback = null;
        private mbDocumentReadyCallback m_mbDocumentReadyCallback = null;
        private mbCloseCallback m_mbCloseCallback = null;
        private mbDestroyCallback m_mbDestroyCallback = null;
        private mbOnShowDevtoolsCallback m_mbShowDevtoolsCallback = null;
        private mbDidCreateScriptContextCallback m_mbDidCreateScriptContextCallback = null;
        private mbGetPluginListCallback m_mbGetPluginListCallback = null;
        private mbLoadingFinishCallback m_mbLoadingFinishCallback = null;
        private mbDownloadCallback m_mbDownloadCallback = null;
        private mbConsoleCallback m_mbConsoleCallback = null;
        private mbLoadUrlBeginCallback m_mbLoadUrlBeginCallback = null;
        private mbLoadUrlEndCallback m_mbLoadUrlEndCallback = null;
        private mbWillReleaseScriptContextCallback m_mbWillReleaseScriptContextCallback = null;
        private mbNetResponseCallback m_mbNetResponseCallback = null;
        private mbNetGetFaviconCallback m_mbNetGetFaviconCallback = null;
        private mbCanGoBackForwardCallback m_mbCanGoBackForwardCallback = null;
        private mbGetCookieCallback m_mbGetCookieCallback = null;
        private mbGetSourceCallback m_mbGetSourceCallback = null;
        private mbGetContentAsMarkupCallback m_mbGetContentAsMarkupCallback = null;
        private mbOnUrlRequestWillRedirectCallback m_mbUrlRequestWillRedirectCallback = null;
        private mbOnUrlRequestDidReceiveResponseCallback m_mbUrlRequestDidReceiveResponseCallback = null;
        private mbOnUrlRequestDidReceiveDataCallback m_mbUrlRequestDidReceiveDataCallback = null;
        private mbOnUrlRequestDidFailCallback m_mbUrlRequestDidFailCallback = null;
        private mbOnUrlRequestDidFinishLoadingCallback m_mbUrlRequestDidFinishLoadingCallback = null;
        private mbNetJobDataRecvCallback m_mbNetJobDataRecvCallback = null;
        private mbNetJobDataFinishCallback m_mbNetJobDataFinishCallback = null;
        private mbPopupDialogSaveNameCallback m_mbPopupDialogSaveNameCallback = null;
        private mbDownloadInBlinkThreadCallback m_mbDownloadInBlinkThreadCallback = null;
        private mbPrintPdfDataCallback m_mbPrintPdfDataCallback = null;
        private mbPrintBitmapCallback m_mbPrintBitmapCallback = null;
        private mbWindowClosingCallback m_mbWindowClosingCallback = null;
        private mbWindowDestroyCallback m_mbWindowDestroyCallback = null;
        private mbDraggableRegionsChangedCallback m_mbDraggableRegionsChangedCallback = null;
        private mbPrintingCallback m_mbPrintingCallback = null;
        private mbImageBufferToDataURLCallback m_mbImageBufferToDataUrlCallback = null;
        private mbOnScreenshotCallback m_mbScreenshotCallback = null;
        private mbOnCallUiThread m_mbCallUiThreadCallback = null;


        // 定义事件句柄
        private event EventHandler<WindowProcEventArgs> m_mbWindowProcHandler = null;
        private event EventHandler<PaintBitUpdatedEventArgs> m_mbPaintBitUpdatedHandler = null;
        private event EventHandler<BlinkThreadInitEventArgs> m_mbBlinkThreadInitHandler = null;
        private event EventHandler<GetPdfPageDataEventArgs> m_mbGetPdfPageDataHandler = null;
        private event EventHandler<RunJsEventArgs> m_mbRunJsHandler = null;
        private event EventHandler<JsQueryEventArgs> m_mbJsQueryHandler = null;
        private event EventHandler<TitleChangedEventArgs> m_mbTitleChangedHandler = null;
        private event EventHandler<MouseOverUrlChangedEventArgs> m_mbMouseOverUrlChangedHandler = null;
        private event EventHandler<UrlChangedEventArgs> m_mbUrlChangedHandler = null;
        private event EventHandler<UrlChangedEventArgs2> m_mbUrlChangedHandler2 = null;
        private event EventHandler<AlertBoxEventArgs> m_mbAlertBoxHandler = null;
        private event EventHandler<ConfirmBoxEventArgs> m_mbConfirmBoxHandler = null;
        private event EventHandler<PromptBoxEventArgs> m_mbPromptBoxHandler = null;
        private event EventHandler<NavigationEventArgs> m_mbNavigationHandler = null;
        private event EventHandler<CreateViewEventArgs> m_mbCreateViewHandler = null;
        private event EventHandler<DocumentReadyEventArgs> m_mbDocumentReadyHandler = null;
        private event EventHandler<CloseEventArgs> m_mbCloseHandler = null;
        private event EventHandler<DestroyEventArgs> m_mbDestroyHandler = null;
        private event EventHandler<ShowDevtoolsEventArgs> m_mbShowDevtoolsHandler = null;
        private event EventHandler<DidCreateScriptContextEventArgs> m_mbDidCreateScriptContextHandler = null;
        private event EventHandler<GetPluginListEventArgs> m_mbGetPluginListHandler = null;
        private event EventHandler<LoadingFinishEventArgs> m_mbLoadingFinishHandler = null;
        private event EventHandler<DownloadEventArgs> m_mbDownloadHandler = null;
        private event EventHandler<ConsoleEventArgs> m_mbConsoleHandler = null;
        private event EventHandler<LoadUrlBeginEventArgs> m_mbLoadUrlBeginHandler = null;
        private event EventHandler<LoadUrlEndEventArgs> m_mbLoadUrlEndHandler = null;
        private event EventHandler<WillReleaseScriptContextEventArgs> m_mbWillReleaseScriptContextHandler = null;
        private event EventHandler<NetResponseEventArgs> m_mbNetResponseHandler = null;
        private event EventHandler<NetGetFaviconEventArgs> m_mbNetGetFaviconHandler = null;
        private event EventHandler<CanGoBackForwardEventArgs> m_mbCanGoBackForwardHandler = null;
        private event EventHandler<GetCookieEventArgs> m_mbGetCookieHandler = null;
        private event EventHandler<GetSourceEventArgs> m_mbGetSourceHandler = null;
        private event EventHandler<GetContentAsMarkupEventArgs> m_mbGetContentAsMarkupHandler = null;
        private event EventHandler<UrlRequestWillRedirectEventArgs> m_mbUrlRequestWillRedirectHandler = null;
        private event EventHandler<UrlRequestDidReceiveResponseEventArgs> m_mbUrlRequestDidReceiveResponseHandler = null;
        private event EventHandler<UrlRequestDidReceiveDataEventArgs> m_mbUrlRequestDidReceiveDataHandler = null;
        private event EventHandler<UrlRequestDidFailEventArgs> m_mbUrlRequestDidFailHandler = null;
        private event EventHandler<UrlRequestDidFinishLoadingEventArgs> m_mbUrlRequestDidFinishLoadingHandler = null;
        private event EventHandler<NetJobDataRecvEventArgs> m_mbNetJobDataRecvHandler = null;
        private event EventHandler<NetJobDataFinishEventArgs> m_mbNetJobDataFinishHandler = null;
        private event EventHandler<PopupDialogSaveNameEventArgs> m_mbPopupDialogSaveNameHandler = null;
        private event EventHandler<DownloadInBlinkThreadEventArgs> m_mbDownloadInBlinkThreadHandler = null;
        private event EventHandler<PrintPdfDataEventArgs> m_mbPrintPdfDataHandler = null;
        private event EventHandler<PrintBitmapEventArgs> m_mbPrintBitmapHandler = null;
        private event EventHandler<WindowClosingEventArgs> m_mbWindowClosingHandler = null;
        private event EventHandler<WindowDestroyEventArgs> m_mbWindowDestroyHandler = null;
        private event EventHandler<DraggableRegionsChangedEventArgs> m_mbDraggableRegionsChangedHandler = null;
        private event EventHandler<PrintingEventArgs> m_mbPrintingHandler = null;
        private event EventHandler<ImageBufferToDataUrlEventArgs> m_mbImageBufferToDataUrlHandler = null;
        private event EventHandler<ScreenshotEventArgs> m_mbScreenshotHandler = null;
        private event EventHandler<CallUiThreadEventArgs> m_mbCallUiThreadHandler = null;



        #region --------------------------- 各种事件参数 ---------------------------

        public class MiniblinkEventArgs : EventArgs
        {
            public MiniblinkEventArgs(IntPtr webView)
            {
                Handle = webView;
            }

            public IntPtr Handle { get; }
        }

        public class WindowProcEventArgs : EventArgs
        {
            public WindowProcEventArgs(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
            {
                Handle = hWnd;
                Msg = msg;
                this.wParam = wParam;
                this.lParam = lParam;
            }

            public IntPtr Handle { get; }
            public int Msg { get; }
            public IntPtr wParam { get; }
            public IntPtr lParam { get; }
            public IntPtr Result { get; set; }

            /// <summary>
            /// 如果为 true 则会返回 Result ，false 则返回默认值
            /// </summary>
            public bool bHand { get; set; }
        }

        public class PaintBitUpdatedEventArgs : MiniblinkEventArgs
        {
            public PaintBitUpdatedEventArgs(IntPtr webView, IntPtr param, IntPtr buffer, IntPtr rect, int width, int height) : base(webView)
            { 
            
            }
        }

        public class BlinkThreadInitEventArgs
        {
            public BlinkThreadInitEventArgs(IntPtr param)
            {

            }
        }

        public class GetPdfPageDataEventArgs : MiniblinkEventArgs
        {
            public GetPdfPageDataEventArgs(IntPtr webView, IntPtr param, IntPtr data, ulong size) : base(webView)
            {

            }
        }

        public class RunJsEventArgs : MiniblinkEventArgs
        {
            IntPtr m_es;
            ulong m_value;

            public RunJsEventArgs(IntPtr webView, IntPtr param, IntPtr es, ulong v) : base(webView)
            {
                m_es = es;
                m_value = v;
            }

            public double numRet
            {
                get
                {
                    return MBVIP_API.mbJsToDouble(m_es, m_value);
                }
            }

            public string strRet
            {
                get
                {
                    IntPtr ptrRet = MBVIP_API.mbJsToString(m_es, m_value);
                    return MBVIP_Common.UTF8PtrToStr(ptrRet);
                }
            }

            public bool bRet
            {
                get
                {
                    return MBVIP_API.mbJsToBoolean(m_es, m_value) == 1 ? true : false;
                }
            }
        }

        public class JsQueryEventArgs : MiniblinkEventArgs
        {
            public JsQueryEventArgs(IntPtr webView, IntPtr param, IntPtr es, ulong queryId, int customMsg, IntPtr request) : base(webView)
            {

            }
        }

        public class TitleChangedEventArgs : MiniblinkEventArgs
        {
            public string Title { get; }

            public TitleChangedEventArgs(IntPtr webView, IntPtr param, IntPtr title) : base(webView)
            {
                Title = MBVIP_Common.UTF8PtrToStr(title);
            }
        }

        public class MouseOverUrlChangedEventArgs : MiniblinkEventArgs
        {
            public MouseOverUrlChangedEventArgs(IntPtr webView, IntPtr param, IntPtr url) : base(webView)
            {

            }
        }

        public class UrlChangedEventArgs : MiniblinkEventArgs
        {
            public string URL { get; }

            public UrlChangedEventArgs(IntPtr webView, IntPtr param, IntPtr url, int canGoBack, int canGoForward) : base(webView)
            {
                URL = MBVIP_Common.UTF8PtrToStr(url);
            }
        }

        public class UrlChangedEventArgs2 : MiniblinkEventArgs
        {
            public UrlChangedEventArgs2(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url) : base(webView)
            {

            }
        }

        public class AlertBoxEventArgs : MiniblinkEventArgs
        {
            public AlertBoxEventArgs(IntPtr webView, IntPtr param, IntPtr msg) : base(webView)
            {

            }
        }

        public class ConfirmBoxEventArgs : MiniblinkEventArgs
        {
            public ConfirmBoxEventArgs(IntPtr webView, IntPtr param, IntPtr msg) : base(webView)
            {
                strMsg = MBVIP_Common.UTF8PtrToStr(msg);
                ptrMsg = msg;
            }

            public string strMsg { get; }
            public IntPtr ptrMsg { get; }
            public int iRet
            {
                get
                {
                    return ptrMsg == IntPtr.Zero ? 0 : 1;
                }
            }
        }

        public class PromptBoxEventArgs : MiniblinkEventArgs
        {
            public PromptBoxEventArgs(IntPtr webView, IntPtr param, IntPtr msg, IntPtr defaultResult) : base(webView)
            {
                strMsg = MBVIP_Common.UTF8PtrToStr(msg);
                strResult = MBVIP_Common.UTF8PtrToStr(defaultResult);
                ptrRet = msg;
            }

            public string strMsg { get; }
            public string strResult { get; }
            public IntPtr ptrRet { get; }
        }

        public class NavigationEventArgs : MiniblinkEventArgs
        {
            public NavigationEventArgs(IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url) : base(webView)
            {
                Type = navigationType;
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                ptrUrl = url;
            }

            public mbNavigationType Type { get; set; }
            public string strUrl { get; }
            public IntPtr ptrUrl { get; }
            public int iRet
            {
                get
                {
                    return ptrUrl == IntPtr.Zero ? 0 : 1;
                }
            }
        }

        public class CreateViewEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;
            private IntPtr m_windowFeatures;

            public CreateViewEventArgs(IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url, IntPtr windowFeatures) : base(webView)
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
                    return MBVIP_Common.UTF8PtrToStr(m_url);
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

        public class DocumentReadyEventArgs : MiniblinkEventArgs
        {
            public DocumentReadyEventArgs(IntPtr webView, IntPtr param, IntPtr frameId) : base(webView)
            {
                Frame = frameId;
            }

            public IntPtr Frame { get; }
        }

        public class CloseEventArgs : MiniblinkEventArgs
        {
            public CloseEventArgs(IntPtr webView, IntPtr param, IntPtr unuse) : base(webView)
            {

            }

            /// <summary>
            /// 1取消关闭，0不取消
            /// </summary>
            public int iCancel { get; set; }
        }

        public class DestroyEventArgs : MiniblinkEventArgs
        {
            public DestroyEventArgs(IntPtr webView, IntPtr param, IntPtr unuse) : base(webView)
            {
                iRet = 1;
            }

            public int iRet { get; }
        }

        public class ShowDevtoolsEventArgs : MiniblinkEventArgs
        {
            public ShowDevtoolsEventArgs(IntPtr webView, IntPtr param) : base(webView)
            {

            }
        }

        public class DidCreateScriptContextEventArgs : MiniblinkEventArgs
        {
            public DidCreateScriptContextEventArgs(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr context, int extensionGroup, int worldId) : base(webView)
            {

            }
        }

        public class GetPluginListEventArgs
        {
            public GetPluginListEventArgs(int refresh, IntPtr pluginListBuilder, IntPtr param)
            {
                iRefresh = refresh;
            }

            public int iRefresh { get; }
        }

        public class LoadingFinishEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;
            private IntPtr m_failedReason;

            public LoadingFinishEventArgs(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, mbLoadingResult result, IntPtr failedReason) : base(webView)
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
                    return MBVIP_Common.UTF8PtrToStr(m_url);
                }
            }

            public string FailedReason
            {
                get
                {
                    return MBVIP_Common.UTF8PtrToStr(m_failedReason);
                }
            }
        }

        public class DownloadEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;

            public DownloadEventArgs(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, IntPtr downloadJob) : base(webView)
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
                    return MBVIP_Common.UTF8PtrToStr(m_url);
                }
            }
        }

        public class ConsoleEventArgs : MiniblinkEventArgs
        {
            public ConsoleEventArgs(IntPtr webView, IntPtr param, mbConsoleLevel level, IntPtr message, IntPtr sourceName, uint sourceLine, IntPtr stackTrace) : base(webView)
            {

            }
        }

        public class LoadUrlBeginEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;

            public LoadUrlBeginEventArgs(IntPtr webView, IntPtr param, IntPtr url, IntPtr job) : base(webView)
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
                    return MBVIP_Common.UTF8PtrToStr(m_url);
                }
            }
        }

        public class LoadUrlEndEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_url;
            private IntPtr m_buf;
            private int m_len;

            public LoadUrlEndEventArgs(IntPtr webView, IntPtr param, IntPtr url, IntPtr job, IntPtr buf, int len) : base(webView)
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
                    return MBVIP_Common.UTF8PtrToStr(m_url);
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

        public class WillReleaseScriptContextEventArgs : MiniblinkEventArgs
        {
            public WillReleaseScriptContextEventArgs(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr context, int worldId) : base(webView)
            {

            }
        }

        public class NetResponseEventArgs : MiniblinkEventArgs
        {
            public NetResponseEventArgs(IntPtr webView, IntPtr param, IntPtr url, IntPtr job) : base(webView)
            {
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                ptrUrl = url;
            }

            public string strUrl { get; }
            public IntPtr ptrUrl { get; }

            public int iRet
            {
                get
                {
                    return ptrUrl == IntPtr.Zero ? 0 : 1;
                }
            }
        }

        public class NetGetFaviconEventArgs : MiniblinkEventArgs
        {
            public NetGetFaviconEventArgs(IntPtr webView, IntPtr param, IntPtr url, IntPtr buf) : base(webView)
            {

            }
        }

        public class CanGoBackForwardEventArgs : MiniblinkEventArgs
        {
            public CanGoBackForwardEventArgs(IntPtr webView, IntPtr param, MbAsynRequestState state, int b) : base(webView)
            {

            }
        }

        public class GetCookieEventArgs : MiniblinkEventArgs
        {
            public GetCookieEventArgs(IntPtr webView, IntPtr param, MbAsynRequestState state, IntPtr cookie) : base(webView)
            {

            }
        }

        public class GetSourceEventArgs : MiniblinkEventArgs
        {
            IntPtr strHtmlCode = IntPtr.Zero;

            public GetSourceEventArgs(IntPtr webView, IntPtr param, IntPtr mhtml) : base(webView)
            {
                strHtmlCode = mhtml;
            }

            public string HtmlCode
            {
                get
                {
                    return MBVIP_Common.UTF8PtrToStr(strHtmlCode);
                }
            }
        }

        public class GetContentAsMarkupEventArgs : MiniblinkEventArgs
        {
            public GetContentAsMarkupEventArgs(IntPtr webView, IntPtr param, IntPtr content, ulong size) : base(webView)
            {

            }
        }

        public class UrlRequestWillRedirectEventArgs : MiniblinkEventArgs
        {
            public UrlRequestWillRedirectEventArgs(IntPtr webView, IntPtr param, IntPtr oldRequest, IntPtr request, IntPtr redirectResponse) : base(webView)
            {

            }
        }

        public class UrlRequestDidReceiveResponseEventArgs : MiniblinkEventArgs
        {
            public UrlRequestDidReceiveResponseEventArgs(IntPtr webView, IntPtr param, IntPtr request, IntPtr response) : base(webView)
            {

            }
        }

        public class UrlRequestDidReceiveDataEventArgs : MiniblinkEventArgs
        {
            public UrlRequestDidReceiveDataEventArgs(IntPtr webView, IntPtr param, IntPtr request, IntPtr data, int dataLength) : base(webView)
            {

            }
        }

        public class UrlRequestDidFailEventArgs : MiniblinkEventArgs
        {
            public UrlRequestDidFailEventArgs(IntPtr webView, IntPtr param, IntPtr request, IntPtr error) : base(webView)
            {

            }
        }

        public class UrlRequestDidFinishLoadingEventArgs : MiniblinkEventArgs
        {
            public UrlRequestDidFinishLoadingEventArgs(IntPtr webView, IntPtr param, IntPtr request, double finishTime) : base(webView)
            {

            }
        }

        public class NetJobDataRecvEventArgs
        {
            public NetJobDataRecvEventArgs(IntPtr ptr, IntPtr job, IntPtr data, int length)
            {

            }
        }

        public class NetJobDataFinishEventArgs
        {
            public NetJobDataFinishEventArgs(IntPtr ptr, IntPtr job, mbLoadingResult result)
            {

            }
        }

        public class PopupDialogSaveNameEventArgs
        {
            public PopupDialogSaveNameEventArgs(IntPtr ptr, IntPtr filePath)
            {

            }
        }

        public class DownloadInBlinkThreadEventArgs : MiniblinkEventArgs
        {
            public DownloadInBlinkThreadEventArgs(IntPtr webView, IntPtr param, ulong expectedContentLength, IntPtr url, IntPtr mime, IntPtr disposition, IntPtr job, IntPtr dataBind) : base(webView)
            {
                Param = param;
                Length = expectedContentLength;
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                strMime = MBVIP_Common.UTF8PtrToStr(mime);
                strDisposition = MBVIP_Common.UTF8PtrToStr(disposition);
            }

            public IntPtr Param { get; }
            public ulong Length { get; }
            public string strUrl { get; }
            public string strMime { get; }
            public string strDisposition { get; }

            public mbDownloadOpt downloadRet
            {
                get
                {
                    return Length == 0 ? mbDownloadOpt.kMbDownloadOptCancel : mbDownloadOpt.kMbDownloadOptCacheData;
                }
            }
        }

        public class PrintPdfDataEventArgs : MiniblinkEventArgs
        {
            public PrintPdfDataEventArgs(IntPtr webView, IntPtr param, IntPtr datas) : base(webView)
            {

            }
        }

        public class PrintBitmapEventArgs : MiniblinkEventArgs
        {
            public PrintBitmapEventArgs(IntPtr webView, IntPtr param, IntPtr data, ulong size) : base(webView)
            {

            }
        }

        public class WindowClosingEventArgs : MiniblinkEventArgs
        {
            public WindowClosingEventArgs(IntPtr webView, IntPtr param) : base(webView)
            {

            }

            /// <summary>
            /// 1取消关闭，0不取消
            /// </summary>
            public int Cancel { get; set; }
        }

        public class WindowDestroyEventArgs : MiniblinkEventArgs
        {
            public WindowDestroyEventArgs(IntPtr webView, IntPtr param) : base(webView)
            {

            }
        }

        public class DraggableRegionsChangedEventArgs : MiniblinkEventArgs
        {
            public DraggableRegionsChangedEventArgs(IntPtr webView, IntPtr param, IntPtr rects, int rectCount) : base(webView)
            {

            }
        }

        public class PrintingEventArgs : MiniblinkEventArgs
        {
            public PrintingEventArgs(IntPtr webView, IntPtr param, mbPrintintStep step, IntPtr hDC, IntPtr settings, int pageCount) : base(webView)
            {
                Param = param;
                mStep = step;
                HDC = hDC;
                Settings = MBVIP_Common.UTF8PtrToStr(settings);
                iPageCount = pageCount;
            }

            public IntPtr Param { get; }
            public mbPrintintStep mStep { get; set; }
            public IntPtr HDC { get; set; }
            public string Settings { get; set; }
            public int iPageCount { get; set; }
        }

        public class ImageBufferToDataUrlEventArgs : MiniblinkEventArgs
        {
            public ImageBufferToDataUrlEventArgs(IntPtr webView, IntPtr param, IntPtr data, ulong size) : base(webView)
            {
                Param = param;
                Data = data;
                Size = size;
            }

            public IntPtr Param { get; }
            public IntPtr Data { get; set; }
            public ulong Size { get; set; }
        }

        public class ScreenshotEventArgs : MiniblinkEventArgs
        {
            public ScreenshotEventArgs(IntPtr webView, IntPtr param, IntPtr data, ulong size) : base(webView)
            {

            }
        }

        public class CallUiThreadEventArgs : MiniblinkEventArgs
        {
            public CallUiThreadEventArgs(IntPtr webView, IntPtr paramOnInThread) : base(webView)
            {

            }
        }

        #endregion

        #region --------------------------- 窗口相关事件处理函数 ---------------------------

        /// <summary>
        /// 窗口刷新事件处理函数
        /// </summary>
        /// <param name="webView">浏览器窗口</param>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="hdc">DC句柄</param>
        /// <param name="x">初始X坐标</param>
        /// <param name="y">初始Y坐标</param>
        /// <param name="cx">X坐标变化量</param>
        /// <param name="cy">Y坐标变化量</param>
        private void onPaintUpdated(IntPtr webView, IntPtr hWnd, IntPtr hdc, int x, int y, int cx, int cy)
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
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="msg">消息类型</param>
        /// <param name="wParam">消息参数1</param>
        /// <param name="lParam">消息参数2</param>
        /// <returns></returns>
        private IntPtr onWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            int iRet = 0;
            if (m_mbWindowProcHandler != null)
            {
                WindowProcEventArgs e = new WindowProcEventArgs(hWnd, (int)msg, wParam, lParam);
                m_mbWindowProcHandler(this, e);
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

        #region --------------------------- 自定义事件，封装MB回调 ---------------------------

        public event EventHandler<PaintBitUpdatedEventArgs> onPaintBitUpdated
        {
            add
            {
                m_mbPaintBitUpdatedHandler += value;
            }

            remove
            {
                m_mbPaintBitUpdatedHandler -= value;
            }
        }

        public event EventHandler<BlinkThreadInitEventArgs> onBlinkThreadInit
        {
            add
            {
                m_mbBlinkThreadInitHandler += value;
            }

            remove
            {
                m_mbBlinkThreadInitHandler -= value;
            }
        }

        public event EventHandler<GetPdfPageDataEventArgs> onGetPdfPageData
        {
            add
            {
                m_mbGetPdfPageDataHandler += value;
            }

            remove
            {
                m_mbGetPdfPageDataHandler -= value;
            }
        }

        public event EventHandler<RunJsEventArgs> onRunJs
        {
            add
            {
                m_mbRunJsHandler += value;
            }

            remove
            {
                m_mbRunJsHandler -= value;
            }
        }

        public event EventHandler<JsQueryEventArgs> onJsQuery
        {
            add
            {
                m_mbJsQueryHandler += value;
            }

            remove
            {
                m_mbJsQueryHandler -= value;
            }
        }

        public event EventHandler<TitleChangedEventArgs> onTitleChanged
        {
            add
            {
                if (m_mbTitleChangedHandler == null)
                {
                    MBVIP_API.mbOnTitleChanged(m_WebView, m_mbTitleChangedCallback, IntPtr.Zero);
                }

                m_mbTitleChangedHandler += value;
            }

            remove
            {
                m_mbTitleChangedHandler -= value;

                if (m_mbTitleChangedHandler == null)
                {
                    MBVIP_API.mbOnTitleChanged(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<MouseOverUrlChangedEventArgs> onMouseOverUrlChanged
        {
            add
            {
                m_mbMouseOverUrlChangedHandler += value;
            }

            remove
            {
                m_mbMouseOverUrlChangedHandler -= value;
            }
        }

        public event EventHandler<UrlChangedEventArgs> onUrlChanged
        {
            add
            {
                if (m_mbUrlChangedHandler == null)
                {
                    MBVIP_API.mbOnURLChanged(m_WebView, m_mbUrlChangedCallback, IntPtr.Zero);
                }
                m_mbUrlChangedHandler += value;
            }

            remove
            {
                m_mbUrlChangedHandler -= value;
                if (m_mbUrlChangedHandler == null)
                {
                    MBVIP_API.mbOnURLChanged(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<UrlChangedEventArgs2> onUrlChanged2
        {
            add
            {
                m_mbUrlChangedHandler2 += value;
            }

            remove
            {
                m_mbUrlChangedHandler2 -= value;
            }
        }

        public event EventHandler<AlertBoxEventArgs> onAlertBox
        {
            add
            {
                m_mbAlertBoxHandler += value;
            }

            remove
            {
                m_mbAlertBoxHandler -= value;
            }
        }

        public event EventHandler<ConfirmBoxEventArgs> onConfirmBox
        {
            add
            {
                m_mbConfirmBoxHandler += value;
            }

            remove
            {
                m_mbConfirmBoxHandler -= value;
            }
        }

        public event EventHandler<PromptBoxEventArgs> onPromptBox
        {
            add
            {
                m_mbPromptBoxHandler += value;
            }

            remove
            {
                m_mbPromptBoxHandler -= value;
            }
        }

        public event EventHandler<NavigationEventArgs> onNavigation
        {
            add
            {
                m_mbNavigationHandler += value;
            }

            remove
            {
                m_mbNavigationHandler -= value;
            }
        }

        public event EventHandler<CreateViewEventArgs> onCreateView
        {
            add
            {
                if (m_mbCreateViewHandler == null)
                {
                    MBVIP_API.mbOnCreateView(m_WebView, m_mbCreateViewCallback, IntPtr.Zero);
                }
                m_mbCreateViewHandler += value;
            }

            remove
            {
                m_mbCreateViewHandler -= value;
                if (m_mbCreateViewHandler == null)
                {
                    MBVIP_API.mbOnCreateView(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<DocumentReadyEventArgs> onDocumentReady
        {
            add
            {
                if (m_mbDocumentReadyHandler == null)
                {
                    MBVIP_API.mbOnDocumentReady(m_WebView, m_mbDocumentReadyCallback, IntPtr.Zero);
                }
                m_mbDocumentReadyHandler += value;
            }

            remove
            {
                m_mbDocumentReadyHandler -= value;
                if (m_mbDocumentReadyHandler == null)
                {
                    MBVIP_API.mbOnDocumentReady(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<CloseEventArgs> onClose
        {
            add
            {
                m_mbCloseHandler += value;
            }

            remove
            {
                m_mbCloseHandler -= value;
            }
        }

        public event EventHandler<DestroyEventArgs> onDestroy
        {
            add
            {
                m_mbDestroyHandler += value;
            }

            remove
            {
                m_mbDestroyHandler -= value;
            }
        }

        public event EventHandler<ShowDevtoolsEventArgs> onShowDevtools
        {
            add
            {
                m_mbShowDevtoolsHandler += value;
            }

            remove
            {
                m_mbShowDevtoolsHandler -= value;
            }
        }

        public event EventHandler<DidCreateScriptContextEventArgs> onDidCreateScriptContext
        {
            add
            {
                m_mbDidCreateScriptContextHandler += value;
            }

            remove
            {
                m_mbDidCreateScriptContextHandler -= value;
            }
        }

        public event EventHandler<GetPluginListEventArgs> onGetPluginList
        {
            add
            {
                m_mbGetPluginListHandler += value;
            }

            remove
            {
                m_mbGetPluginListHandler -= value;
            }
        }

        public event EventHandler<LoadingFinishEventArgs> onLoadingFinish
        {
            add
            {
                if (m_mbLoadingFinishHandler == null)
                {
                    MBVIP_API.mbOnLoadingFinish(m_WebView, m_mbLoadingFinishCallback, IntPtr.Zero);
                }
                m_mbLoadingFinishHandler += value;
            }

            remove
            {
                m_mbLoadingFinishHandler -= value;
                if (m_mbLoadingFinishHandler == null)
                {
                    MBVIP_API.mbOnLoadingFinish(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<DownloadEventArgs> onDownload
        {
            add
            {
                if (m_mbDownloadHandler == null)
                {
                    MBVIP_API.mbOnDownload(m_WebView, m_mbDownloadCallback, IntPtr.Zero);
                }
                m_mbDownloadHandler += value;
            }

            remove
            {
                m_mbDownloadHandler -= value;
                if (m_mbDownloadHandler == null)
                {
                    MBVIP_API.mbOnDownload(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<ConsoleEventArgs> onConsole
        {
            add
            {
                m_mbConsoleHandler += value;
            }

            remove
            {
                m_mbConsoleHandler -= value;
            }
        }

        public event EventHandler<LoadUrlBeginEventArgs> onLoadUrlBegin
        {
            add
            {
                if (m_mbLoadUrlBeginHandler == null)
                {
                    MBVIP_API.mbOnLoadUrlBegin(m_WebView, m_mbLoadUrlBeginCallback, IntPtr.Zero);
                }
                m_mbLoadUrlBeginHandler += value;
            }

            remove
            {
                m_mbLoadUrlBeginHandler -= value;
                if (m_mbLoadUrlBeginHandler == null)
                {
                    MBVIP_API.mbOnLoadUrlBegin(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<LoadUrlEndEventArgs> onLoadUrlEnd
        {
            add
            {
                if (m_mbLoadUrlEndHandler == null)
                {
                    MBVIP_API.mbOnLoadUrlEnd(m_WebView, m_mbLoadUrlEndCallback, IntPtr.Zero);
                }
                m_mbLoadUrlEndHandler += value;
            }

            remove
            {
                m_mbLoadUrlEndHandler -= value;
                if (m_mbLoadUrlEndHandler == null)
                {
                    MBVIP_API.mbOnLoadUrlEnd(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<WillReleaseScriptContextEventArgs> onWillReleaseScriptContext
        {
            add
            {
                m_mbWillReleaseScriptContextHandler += value;
            }

            remove
            {
                m_mbWillReleaseScriptContextHandler -= value;
            }
        }

        public event EventHandler<NetResponseEventArgs> onNetResponse
        {
            add
            {
                m_mbNetResponseHandler += value;
            }

            remove
            {
                m_mbNetResponseHandler -= value;
            }
        }

        public event EventHandler<NetGetFaviconEventArgs> onNetGetFavicon
        {
            add
            {
                m_mbNetGetFaviconHandler += value;
            }

            remove
            {
                m_mbNetGetFaviconHandler -= value;
            }
        }

        public event EventHandler<CanGoBackForwardEventArgs> onCanGoBackForward
        {
            add
            {
                m_mbCanGoBackForwardHandler += value;
            }

            remove
            {
                m_mbCanGoBackForwardHandler -= value;
            }
        }

        public event EventHandler<GetCookieEventArgs> onGetCookie
        {
            add
            {
                m_mbGetCookieHandler += value;
            }

            remove
            {
                m_mbGetCookieHandler -= value;
            }
        }

        public event EventHandler<GetSourceEventArgs> onGetSource
        {
            add
            {
                m_mbGetSourceHandler += value;
            }

            remove
            {
                m_mbGetSourceHandler -= value;
            }
        }

        public event EventHandler<GetContentAsMarkupEventArgs> onGetContentAsMarkup
        {
            add
            {
                m_mbGetContentAsMarkupHandler += value;
            }

            remove
            {
                m_mbGetContentAsMarkupHandler -= value;
            }
        }

        public event EventHandler<UrlRequestWillRedirectEventArgs> onUrlRequestWillRedirect
        {
            add
            {
                m_mbUrlRequestWillRedirectHandler += value;
            }

            remove
            {
                m_mbUrlRequestWillRedirectHandler -= value;
            }
        }

        public event EventHandler<UrlRequestDidReceiveResponseEventArgs> onUrlRequestDidReceiveResponse
        {
            add
            {
                m_mbUrlRequestDidReceiveResponseHandler += value;
            }

            remove
            {
                m_mbUrlRequestDidReceiveResponseHandler -= value;
            }
        }

        public event EventHandler<UrlRequestDidReceiveDataEventArgs> onUrlRequestDidReceiveData
        {
            add
            {
                m_mbUrlRequestDidReceiveDataHandler += value;
            }

            remove
            {
                m_mbUrlRequestDidReceiveDataHandler -= value;
            }
        }

        public event EventHandler<UrlRequestDidFailEventArgs> onUrlRequestDidFail
        {
            add
            {
                m_mbUrlRequestDidFailHandler += value;
            }

            remove
            {
                m_mbUrlRequestDidFailHandler -= value;
            }
        }

        public event EventHandler<UrlRequestDidFinishLoadingEventArgs> onUrlRequestDidFinishLoading
        {
            add
            {
                m_mbUrlRequestDidFinishLoadingHandler += value;
            }

            remove
            {
                m_mbUrlRequestDidFinishLoadingHandler -= value;
            }
        }

        public event EventHandler<NetJobDataRecvEventArgs> onNetJobDataRecv
        {
            add
            {
                m_mbNetJobDataRecvHandler += value;
            }

            remove
            {
                m_mbNetJobDataRecvHandler -= value;
            }
        }

        public event EventHandler<NetJobDataFinishEventArgs> onNetJobDataFinish
        {
            add
            {
                m_mbNetJobDataFinishHandler += value;
            }

            remove
            {
                m_mbNetJobDataFinishHandler -= value;
            }
        }

        public event EventHandler<PopupDialogSaveNameEventArgs> onPopupDialogSaveName
        {
            add
            {
                m_mbPopupDialogSaveNameHandler += value;
            }

            remove
            {
                m_mbPopupDialogSaveNameHandler -= value;
            }
        }

        public event EventHandler<DownloadInBlinkThreadEventArgs> onDownloadInBlinkThread
        {
            add
            {
                m_mbDownloadInBlinkThreadHandler += value;
            }

            remove
            {
                m_mbDownloadInBlinkThreadHandler -= value;
            }
        }

        public event EventHandler<PrintPdfDataEventArgs> onPrintPdfData
        {
            add
            {
                m_mbPrintPdfDataHandler += value;
            }

            remove
            {
                m_mbPrintPdfDataHandler -= value;
            }
        }

        public event EventHandler<PrintBitmapEventArgs> onPrintBitmap
        {
            add
            {
                m_mbPrintBitmapHandler += value;
            }

            remove
            {
                m_mbPrintBitmapHandler -= value;
            }
        }

        public event EventHandler<WindowClosingEventArgs> onWindowClosing
        {
            add
            {
                m_mbWindowClosingHandler += value;
            }

            remove
            {
                m_mbWindowClosingHandler -= value;
            }
        }

        public event EventHandler<WindowDestroyEventArgs> onWindowDestroy
        {
            add
            {
                m_mbWindowDestroyHandler += value;
            }

            remove
            {
                m_mbWindowDestroyHandler -= value;
            }
        }

        public event EventHandler<DraggableRegionsChangedEventArgs> onDraggableRegionsChanged
        {
            add
            {
                m_mbDraggableRegionsChangedHandler += value;
            }

            remove
            {
                m_mbDraggableRegionsChangedHandler -= value;
            }
        }

        public event EventHandler<PrintingEventArgs> onPrinting
        {
            add
            {
                m_mbPrintingHandler += value;
            }

            remove
            {
                m_mbPrintingHandler -= value;
            }
        }

        public event EventHandler<ImageBufferToDataUrlEventArgs> onImageBufferToDataURL
        {
            add
            {
                m_mbImageBufferToDataUrlHandler += value;
            }

            remove
            {
                m_mbImageBufferToDataUrlHandler -= value;
            }
        }

        public event EventHandler<ScreenshotEventArgs> onScreenshot
        {
            add
            {
                m_mbScreenshotHandler += value;
            }

            remove
            {
                m_mbScreenshotHandler -= value;
            }
        }

        public event EventHandler<CallUiThreadEventArgs> onCallUiThread
        {
            add
            {
                m_mbCallUiThreadHandler += value;
            }

            remove
            {
                m_mbCallUiThreadHandler -= value;
            }
        }

        #endregion

        #region --------------------------- 设置回调事件，给事件参数赋值 ---------------------------

        protected void SetCallBack()
        {
            m_mbPaintUpdatedCallback = new mbPaintUpdatedCallback(onPaintUpdated);

            m_mbWndProcCallback = new WndProcCallback(onWndProc);

            m_mbPaintBitUpdatedCallback = new mbPaintBitUpdatedCallback((IntPtr webView, IntPtr param, IntPtr buffer, IntPtr rect, int width, int height) =>
            {
                m_mbPaintBitUpdatedHandler?.Invoke(this, new PaintBitUpdatedEventArgs(webView, param, buffer, rect, width, height));
            });

            m_mbBlinkThreadInitCallback = new mbOnBlinkThreadInitCallback((IntPtr param) =>
            {
                m_mbBlinkThreadInitHandler?.Invoke(this, new BlinkThreadInitEventArgs(param));
            });

            m_mbGetPdfPageDataCallback = new mbOnGetPdfPageDataCallback((IntPtr webView, IntPtr param, IntPtr data, ulong size) =>
            {
                m_mbGetPdfPageDataHandler?.Invoke(this, new GetPdfPageDataEventArgs(webView, param, data, size));
            });

            m_mbRunJsCallback = new mbRunJsCallback((IntPtr webView, IntPtr param, IntPtr es, ulong v) =>
            {
                m_mbRunJsHandler?.Invoke(this, new RunJsEventArgs(webView, param, es, v));
            });

            m_mbJsQueryCallback = new mbJsQueryCallback((IntPtr webView, IntPtr param, IntPtr es, ulong queryId, int customMsg, IntPtr request) =>
            {
                m_mbJsQueryHandler?.Invoke(this, new JsQueryEventArgs(webView, param, es, queryId, customMsg, request));
            });

            m_mbTitleChangedCallback = new mbTitleChangedCallback((IntPtr WebView, IntPtr param, IntPtr title) =>
            {
                m_mbTitleChangedHandler?.Invoke(this, new TitleChangedEventArgs(WebView, param, title));
            });

            m_mbMouseOverUrlChangedCallback = new mbMouseOverUrlChangedCallback((IntPtr webView, IntPtr param, IntPtr url) =>
            {
                m_mbMouseOverUrlChangedHandler?.Invoke(this, new MouseOverUrlChangedEventArgs(webView, param, url));
            });

            m_mbUrlChangedCallback = new mbUrlChangedCallback((IntPtr webView, IntPtr param, IntPtr url, int canGoBack, int canGoForward) =>
            {
                m_mbUrlChangedHandler?.Invoke(this, new UrlChangedEventArgs(webView, param, url, canGoBack, canGoForward));
            });

            m_mbUrlChangedCallback2 = new mbUrlChangedCallback2((IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url) =>
            {
                m_mbUrlChangedHandler2?.Invoke(this, new UrlChangedEventArgs2(webView, param, frameId, url));
            });

            m_mbAlertBoxCallback = new mbAlertBoxCallback((IntPtr webView, IntPtr param, IntPtr msg) =>
            {
                m_mbAlertBoxHandler?.Invoke(this, new AlertBoxEventArgs(webView, param, msg));
            });

            m_mbConfirmBoxCallback = new mbConfirmBoxCallback((IntPtr webView, IntPtr param, IntPtr msg) =>
            {
                int iRet = 0;
                if (m_mbConfirmBoxHandler != null)
                {
                    ConfirmBoxEventArgs e = new ConfirmBoxEventArgs(webView, param, msg);
                    m_mbConfirmBoxHandler(this, e);

                    iRet = e.iRet;
                }

                return iRet; 
            });

            m_mbPromptBoxCallback = new mbPromptBoxCallback((IntPtr webView, IntPtr param, IntPtr msg, IntPtr defaultResult) =>
            {
                IntPtr ptrRet = IntPtr.Zero;
                if (m_mbPromptBoxHandler != null)
                {
                    PromptBoxEventArgs e = new PromptBoxEventArgs(webView, param, msg, defaultResult);
                    m_mbPromptBoxHandler(this, e);

                    ptrRet = e.ptrRet;
                }

                return ptrRet;
            });

            m_mbNavigationCallback = new mbNavigationCallback((IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url) =>
            {
                int iRet = 0;
                if (m_mbNavigationHandler != null)
                {
                    NavigationEventArgs e = new NavigationEventArgs(webView, param, navigationType, url);
                    m_mbNavigationHandler(this, e);

                    iRet = e.iRet;
                }

                return iRet;
            });

            m_mbCreateViewCallback = new mbCreateViewCallback((IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url, IntPtr windowFeatures) =>
            {
                IntPtr ptrRet = IntPtr.Zero;
                if (m_mbCreateViewHandler != null)
                {
                    CreateViewEventArgs e = new CreateViewEventArgs(webView, param, navigationType, url, windowFeatures);
                    m_mbCreateViewHandler(this, e);

                    ptrRet = e.SetNewWebViewHandle;
                }

                return ptrRet;
            });

            m_mbDocumentReadyCallback = new mbDocumentReadyCallback((IntPtr webView, IntPtr param, IntPtr frame) =>
            {
                m_mbDocumentReadyHandler?.Invoke(this, new DocumentReadyEventArgs(webView, param, frame));
            });

            m_mbCloseCallback = new mbCloseCallback((IntPtr webView, IntPtr param, IntPtr unuse) =>
            {
                int iRet = 1;
                if (m_mbCloseHandler != null)
                {
                    CloseEventArgs e = new CloseEventArgs(webView, param, unuse);
                    m_mbCloseHandler(this, e);

                    iRet = e.iCancel;
                }

                return iRet;
            });

            m_mbDestroyCallback = new mbDestroyCallback((IntPtr webView, IntPtr param, IntPtr unuse) =>
            {
                int iRet = 1;
                if (m_mbDestroyHandler != null)
                {
                    DestroyEventArgs e = new DestroyEventArgs(webView, param, unuse);
                    m_mbDestroyHandler(this, e);

                    iRet = e.iRet;
                }

                return iRet;
            });

            m_mbShowDevtoolsCallback = new mbOnShowDevtoolsCallback((IntPtr webView, IntPtr param) =>
            {
                m_mbShowDevtoolsHandler?.Invoke(this, new ShowDevtoolsEventArgs(webView, param));
            });

            m_mbDidCreateScriptContextCallback = new mbDidCreateScriptContextCallback((IntPtr webView, IntPtr param, IntPtr frameId, IntPtr context, int extensionGroup, int worldId) =>
            {
                m_mbDidCreateScriptContextHandler?.Invoke(this, new DidCreateScriptContextEventArgs(webView, param, frameId, context, extensionGroup, worldId));
            });

            m_mbGetPluginListCallback = new mbGetPluginListCallback((int refresh, IntPtr pluginListBuilder, IntPtr param) =>
            {
                int iRet = 0;
                if (m_mbGetPluginListHandler != null)
                {
                    GetPluginListEventArgs e = new GetPluginListEventArgs(refresh, pluginListBuilder, param);
                    m_mbGetPluginListHandler(this, e);

                    iRet = e.iRefresh;
                }

                return iRet;
            });

            m_mbLoadingFinishCallback = new mbLoadingFinishCallback((IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, mbLoadingResult result, IntPtr failedReason) =>
            {
                m_mbLoadingFinishHandler?.Invoke(this, new LoadingFinishEventArgs(webView, param, frameId, url, result, failedReason));
            });

            m_mbDownloadCallback = new mbDownloadCallback((IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, IntPtr downloadJob) =>
            {
                int iRet = 1;
                if (m_mbDownloadHandler != null)
                {
                    DownloadEventArgs e = new DownloadEventArgs(webView, param, frameId, url, downloadJob);
                    m_mbDownloadHandler(this, e);

                    iRet = e.Cancel ? 0 : 1;
                }

                return iRet;
            });

            m_mbConsoleCallback = new mbConsoleCallback((IntPtr webView, IntPtr param, mbConsoleLevel level, IntPtr message, IntPtr sourceName, uint sourceLine, IntPtr stackTrace) =>
            {
                m_mbConsoleHandler?.Invoke(this, new ConsoleEventArgs(webView, param, level, message, sourceName, sourceLine, stackTrace));
            });

            m_mbLoadUrlBeginCallback = new mbLoadUrlBeginCallback((IntPtr webView, IntPtr param, IntPtr url, IntPtr job) =>
            {
                int iRet = 0;
                if (m_mbLoadUrlBeginHandler != null)
                {
                    LoadUrlBeginEventArgs e = new LoadUrlBeginEventArgs(webView, param, url, job);
                    m_mbLoadUrlBeginHandler(this, e);

                    iRet = e.Cancel ? 1 : 0;
                }

                return iRet;
            });

            m_mbLoadUrlEndCallback = new mbLoadUrlEndCallback((IntPtr webView, IntPtr param, IntPtr url, IntPtr job, IntPtr buf, int len) =>
            {
                m_mbLoadUrlEndHandler?.Invoke(this, new LoadUrlEndEventArgs(webView, param, url, job, buf, len));
            });

            m_mbWillReleaseScriptContextCallback = new mbWillReleaseScriptContextCallback((IntPtr webView, IntPtr param, IntPtr frameId, IntPtr context, int worldId) =>
            {
                m_mbWillReleaseScriptContextHandler?.Invoke(this, new WillReleaseScriptContextEventArgs(webView, param, frameId, context, worldId));
            });

            m_mbNetResponseCallback = new mbNetResponseCallback((IntPtr webView, IntPtr param, IntPtr url, IntPtr job) =>
            {
                int iRet = 0;
                if (m_mbNetResponseHandler != null)
                {
                    NetResponseEventArgs e = new NetResponseEventArgs(webView, param, url, job);
                    m_mbNetResponseHandler(this, e);

                    iRet = e.iRet;
                }

                return iRet;
            });

            m_mbNetGetFaviconCallback = new mbNetGetFaviconCallback((IntPtr webView, IntPtr param, IntPtr url, IntPtr buf) =>
            {
                m_mbNetGetFaviconHandler?.Invoke(this, new NetGetFaviconEventArgs(webView, param, url, buf));
            });

            m_mbCanGoBackForwardCallback = new mbCanGoBackForwardCallback((IntPtr webView, IntPtr param, MbAsynRequestState state, int b) =>
            {
                m_mbCanGoBackForwardHandler?.Invoke(this, new CanGoBackForwardEventArgs(webView, param, state, b));
            });

            m_mbGetCookieCallback = new mbGetCookieCallback((IntPtr webView, IntPtr param, MbAsynRequestState state, IntPtr cookie) =>
            {
                m_mbGetCookieHandler?.Invoke(this, new GetCookieEventArgs(webView, param, state, cookie));
            });

            m_mbGetSourceCallback = new mbGetSourceCallback((IntPtr webView, IntPtr param, IntPtr mhtml) =>
            {
                m_mbGetSourceHandler?.Invoke(this, new GetSourceEventArgs(webView, param, mhtml));
            });

            m_mbGetContentAsMarkupCallback = new mbGetContentAsMarkupCallback((IntPtr webView, IntPtr param, IntPtr content, ulong size) =>
            {
                m_mbGetContentAsMarkupHandler?.Invoke(this, new GetContentAsMarkupEventArgs(webView, param, content, size));
            });

            m_mbUrlRequestWillRedirectCallback = new mbOnUrlRequestWillRedirectCallback((IntPtr webView, IntPtr param, IntPtr oldRequest, IntPtr request, IntPtr redirectResponse) =>
            {
                m_mbUrlRequestWillRedirectHandler?.Invoke(this, new UrlRequestWillRedirectEventArgs(webView, param, oldRequest, request, redirectResponse));
            });

            m_mbUrlRequestDidReceiveResponseCallback = new mbOnUrlRequestDidReceiveResponseCallback((IntPtr webView, IntPtr param, IntPtr request, IntPtr response) =>
            {
                m_mbUrlRequestDidReceiveResponseHandler?.Invoke(this, new UrlRequestDidReceiveResponseEventArgs(webView, param, request, response));
            });

            m_mbUrlRequestDidReceiveDataCallback = new mbOnUrlRequestDidReceiveDataCallback((IntPtr webView, IntPtr param, IntPtr request, IntPtr data, int dataLength) =>
            {
                m_mbUrlRequestDidReceiveDataHandler?.Invoke(this, new UrlRequestDidReceiveDataEventArgs(webView, param, request, data, dataLength));
            });

            m_mbUrlRequestDidFailCallback = new mbOnUrlRequestDidFailCallback((IntPtr webView, IntPtr param, IntPtr request, IntPtr error) =>
            {
                m_mbUrlRequestDidFailHandler?.Invoke(this, new UrlRequestDidFailEventArgs(webView, param, request, error));
            });

            m_mbUrlRequestDidFinishLoadingCallback = new mbOnUrlRequestDidFinishLoadingCallback((IntPtr webView, IntPtr param, IntPtr request, double finishTime) =>
            {
                m_mbUrlRequestDidFinishLoadingHandler?.Invoke(this, new UrlRequestDidFinishLoadingEventArgs(webView, param, request, finishTime));
            });

            m_mbNetJobDataRecvCallback = new mbNetJobDataRecvCallback((IntPtr ptr, IntPtr job, IntPtr data, int length) =>
            {
                m_mbNetJobDataRecvHandler?.Invoke(this, new NetJobDataRecvEventArgs(ptr, job, data, length));
            });

            m_mbNetJobDataFinishCallback = new mbNetJobDataFinishCallback((IntPtr ptr, IntPtr job, mbLoadingResult result) =>
            {
                m_mbNetJobDataFinishHandler?.Invoke(this, new NetJobDataFinishEventArgs(ptr, job, result));
            });

            m_mbPopupDialogSaveNameCallback = new mbPopupDialogSaveNameCallback((IntPtr ptr, IntPtr filePath) =>
            {
                m_mbPopupDialogSaveNameHandler?.Invoke(this, new PopupDialogSaveNameEventArgs(ptr, filePath));
            });

            m_mbDownloadInBlinkThreadCallback = new mbDownloadInBlinkThreadCallback((IntPtr webView, IntPtr param, ulong expectedContentLength, IntPtr url, IntPtr mime, IntPtr disposition, IntPtr job, IntPtr dataBind) =>
            {
                mbDownloadOpt downloadRet = mbDownloadOpt.kMbDownloadOptCacheData;
                if (m_mbDownloadInBlinkThreadHandler != null)
                {
                    DownloadInBlinkThreadEventArgs e = new DownloadInBlinkThreadEventArgs(webView, param, expectedContentLength, url, mime, disposition, job, dataBind);
                    m_mbDownloadInBlinkThreadHandler(this, e);

                    downloadRet = e.downloadRet;
                }

                return downloadRet;
            });

            m_mbPrintPdfDataCallback = new mbPrintPdfDataCallback((IntPtr webView, IntPtr param, IntPtr datas) =>
            {
                m_mbPrintPdfDataHandler?.Invoke(this, new PrintPdfDataEventArgs(webView, param, datas));
            });

            m_mbPrintBitmapCallback = new mbPrintBitmapCallback((IntPtr webView, IntPtr param, IntPtr data, ulong size) =>
            {
                m_mbPrintBitmapHandler?.Invoke(this, new PrintBitmapEventArgs(webView, param, data, size));
            });

            m_mbWindowClosingCallback = new mbWindowClosingCallback((IntPtr webView, IntPtr param) =>
            {
                int iRet = 0;
                if (m_mbWindowClosingHandler != null)
                {
                    WindowClosingEventArgs e = new WindowClosingEventArgs(webView, param);
                    m_mbWindowClosingHandler(this, e);

                    iRet = e.Cancel;
                }

                return iRet;
            });

            m_mbWindowDestroyCallback = new mbWindowDestroyCallback((IntPtr webView, IntPtr param) =>
            {
                m_mbWindowDestroyHandler?.Invoke(this, new WindowDestroyEventArgs(webView, param));
            });

            m_mbDraggableRegionsChangedCallback = new mbDraggableRegionsChangedCallback((IntPtr webView, IntPtr param, IntPtr rects, int rectCount) =>
            {
                m_mbDraggableRegionsChangedHandler?.Invoke(this, new DraggableRegionsChangedEventArgs(webView, param, rects, rectCount));
            });

            m_mbPrintingCallback = new mbPrintingCallback((IntPtr webView, IntPtr param, mbPrintintStep step, IntPtr hDC, IntPtr settings, int pageCount) =>
            {
                int iRet = 0;
                if (m_mbPrintingHandler != null)
                {
                    PrintingEventArgs e = new PrintingEventArgs(webView, param, step, hDC, settings, pageCount);
                    m_mbPrintingHandler(this, e);

                    iRet = e.iPageCount;
                }

                return iRet;
            });

            m_mbImageBufferToDataUrlCallback = new mbImageBufferToDataURLCallback((IntPtr webView, IntPtr param, IntPtr data, ulong size) =>
            {
                IntPtr ptrRet = IntPtr.Zero;
                if (m_mbImageBufferToDataUrlHandler != null)
                {
                    ImageBufferToDataUrlEventArgs e = new ImageBufferToDataUrlEventArgs(webView, param, data, size);
                    m_mbImageBufferToDataUrlHandler(this, e);

                    ptrRet = e.Data;
                }

                return ptrRet;
            });

            m_mbScreenshotCallback = new mbOnScreenshotCallback((IntPtr webView, IntPtr param, IntPtr data, ulong size) =>
            {
                m_mbScreenshotHandler?.Invoke(this, new ScreenshotEventArgs(webView, param, data, size));
            });

            m_mbCallUiThreadCallback = new mbOnCallUiThread((IntPtr webView, IntPtr paramOnInThread) =>
            {
                m_mbCallUiThreadHandler?.Invoke(this, new CallUiThreadEventArgs(webView, paramOnInThread));
            });
        }

        #endregion

        #region --------------------------- 各种属性封装 ---------------------------

        /// <summary>
        /// 获取或设置 WebView 的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取网页标题
        /// </summary>
        public string Title
        {
            get { return MBVIP_Common.UTF8PtrToStr(MBVIP_API.mbGetTitle(m_WebView)); }
        }

        /// <summary>
        /// 获取网页URL
        /// </summary>
        public string URL
        {
            get { return MBVIP_Common.UTF8PtrToStr(MBVIP_API.mbGetUrl(m_WebView)); }
        }

        /// <summary>
        /// 是否允许鼠标
        /// </summary>
        public bool MouseEnabled
        {
            set { MBVIP_API.mbSetMouseEnabled(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 是否允许触屏
        /// </summary>
        public bool TouchEnabled
        {
            set { MBVIP_API.mbSetTouchEnabled(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 是否允许菜单
        /// </summary>
        public bool ContextMenuEnabled
        {
            set { MBVIP_API.mbSetContextMenuEnabled(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 设置是否导航到新窗口
        /// </summary>
        public bool NavigationToNewWindowEnable
        {
            set { MBVIP_API.mbSetNavigationToNewWindowEnable(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 是否允许无头模式，启用后可关闭渲染
        /// </summary>
        public bool HeadlessEnabled
        {
            set { MBVIP_API.mbSetHeadlessEnabled(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 是否允许拖放
        /// </summary>
        public bool DragDropEnable
        {
            set { MBVIP_API.mbSetDragDropEnable(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 是否允许拖动
        /// </summary>
        public bool DragEnable
        {
            set { MBVIP_API.mbSetDragEnable(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 设置页面代理，全局生效
        /// </summary>
        public mbProxy Proxy
        {
            set { MBVIP_API.mbSetProxy(m_WebView, ref value); }
        }

        /// <summary>
        /// 设置UA
        /// </summary>
        public string UserAgent
        {
            set { MBVIP_API.mbSetUserAgent(m_WebView, MBVIP_Common.StrToUtf8Ptr(value)); }
        }

        /// <summary>
        /// 是否允许Cookie
        /// </summary>
        public bool CookieEnabled
        {
            set { MBVIP_API.mbSetCookieEnabled(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 设置Cookie文件的完整路径，注意：是完整的，路径，不存在会自动新建
        /// </summary>
        public string CookieFullPath
        {
            set { MBVIP_API.mbSetCookieJarFullPath(m_WebView, MBVIP_Common.StrToUtf8Ptr(value)); }
        }

        /// <summary>
        /// 设置Storage文件的完整路径，注意：是完整的，路径，不存在会自动新建
        /// </summary>
        public string StorageFullPath
        {
            set { MBVIP_API.mbSetLocalStorageFullPath(m_WebView, MBVIP_Common.StrToUtf8Ptr(value)); }
        }

        /// <summary>
        /// 是否允许跨域检查
        /// </summary>
        public bool CspCheckEnable
        {
            set { MBVIP_API.mbSetCspCheckEnable(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 是否允许Npapi
        /// </summary>
        public bool NpapiEnabled
        {
            set { MBVIP_API.mbSetNpapiPluginsEnabled(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 是否允许内存缓存
        /// </summary>
        public bool MemoryCacheEnable
        {
            set { MBVIP_API.mbSetMemoryCacheEnable(m_WebView, value ? 1 : 0); }
        }

        public float ZoomFactor
        {
            set { MBVIP_API.mbSetZoomFactor(m_WebView, value); }
            get { return MBVIP_API.mbGetZoomFactor(m_WebView); }
        }

        /// <summary>
        /// 是否允许磁盘缓存
        /// </summary>
        public bool DiskCacheEnabled
        {
            set { MBVIP_API.mbSetDiskCacheEnabled(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 设置磁盘缓存路径
        /// </summary>
        public string DiskCachePath
        {
            set { MBVIP_API.mbSetDiskCachePath(m_WebView, MBVIP_Common.StrToUtf8Ptr(value)); }
        }

        /// <summary>
        /// 设置磁盘缓存大小
        /// </summary>
        public long DiskCacheLimit
        {
            set { MBVIP_API.mbSetDiskCacheLimit(m_WebView, value); }
        }

        /// <summary>
        /// 设置磁盘缓存限制
        /// </summary>
        public long DiskCacheLimitDisk
        {
            set { MBVIP_API.mbSetDiskCacheLimitDisk(m_WebView, value); }
        }

        /// <summary>
        /// 设置磁盘缓存级别
        /// </summary>
        public int DiskCacheLevel
        {
            set { MBVIP_API.mbSetDiskCacheLevel(m_WebView, value); }
        }

        #endregion

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

        /// <summary>
        /// 设置mb.dll路径，默认是exe的同目录
        /// </summary>
        /// <param name="strPath"></param>
        public void setMBDllPath(string strPath)
        {
            IntPtr ptrPath = MBVIP_Common.StrToUtf8Ptr(strPath);
            MBVIP_API.mbSetMbDllPath(ptrPath);
        }

        /// <summary>
        /// 设置node.dll路径，默认是exe的同目录
        /// </summary>
        /// <param name="strPath"></param>
        public void setNodeDllPath(string strPath)
        {
            IntPtr ptrPath = MBVIP_Common.StrToUtf8Ptr(strPath);
            MBVIP_API.mbSetMbMainDllPath(ptrPath);
        }

        /// <summary>
        /// 设置居中（相对父控件）
        /// </summary>
        public void SetCenter()
        {
            MBVIP_API.mbMoveToCenter(m_WebView);
        }

        /// <summary>
        /// 加载网站
        /// </summary>
        /// <param name="strUrl"></param>
        public void LoadUrl(string strUrl)
        {
            MBVIP_API.mbLoadURL(m_WebView, MBVIP_Common.StrToUtf8Ptr(strUrl));
        }

        /// <summary>
        /// 设置硬件参数，可以用于模拟手机环境等
        /// </summary>
        /// <param name="strDevice">设备的字符串。可取值有：
        /// "navigator.maxTouchPoints"此时 paramInt 需要被设置，表示 touch 的点数。
        /// "navigator.platform"此时 paramStr 需要被设置，表示js里获取的 navigator.platform字符串。
        /// "navigator.hardwareConcurrency"此时 paramInt 需要被设置，表示js里获取的 navigator.hardwareConcurrency 整数值。
        /// "screen.width"此时 paramInt 需要被设置，表示js里获取的 screen.width 整数值。
        /// "screen.height"此时 paramInt 需要被设置，表示js里获取的 screen.height 整数值。
        /// "screen.availWidth"此时 paramInt 需要被设置，表示js里获取的 screen.availWidth 整数值。
        /// "screen.availHeight"此时 paramInt 需要被设置，表示js里获取的 screen.availHeight 整数值。
        /// "screen.pixelDepth"此时 paramInt 需要被设置，表示js里获取的 screen.pixelDepth 整数值。
        /// "screen.pixelDepth"目前等价于"screen.pixelDepth"。
        /// "window.devicePixelRatio"同上</param>
        /// <param name="strParam"></param>
        /// <param name="iParam"></param>
        /// <param name="fParam"></param>
        public void SetDeviceParameter(string strDevice, string strParam, int iParam = 0, float fParam = 0)
        {
            IntPtr ptrDevice = MBVIP_Common.StrToUtf8Ptr(strDevice);
            IntPtr ptrParam = MBVIP_Common.StrToUtf8Ptr(strParam);

            MBVIP_API.mbSetDeviceParameter(m_WebView, ptrDevice, ptrParam, iParam, fParam);
        }

        /// <summary>
        /// 开启一些实验性选项。
        /// </summary>
        /// <param name="strDebug">
        /// "showDevTools"	开启开发者工具，此时param要填写开发者工具的资源路径，如file:///c:/miniblink-release/front_end/inspector.html。注意param此时必须是utf8编
        /// "wakeMinInterval" 设置帧率，默认值是10，值越大帧率越低
        /// "drawMinInterval" 设置帧率，默认值是3，值越大帧率越低
        /// "antiAlias" 设置抗锯齿渲染。param必须设置为"1"
        /// "minimumFontSize" 最小字体
        /// "minimumLogicalFontSize" 最小逻辑字体
        /// "defaultFontSize" 默认字体
        /// "defaultFixedFontSize" 默认fixed字体</param>
        /// <param name="strParam"></param>
        public void SetDebugConfig(string strDebug, string strParam)
        {
            IntPtr ptrDebug = MBVIP_Common.StrToUtf8Ptr(strDebug);
            IntPtr ptrParam = MBVIP_Common.StrToUtf8Ptr(strParam);

            MBVIP_API.mbSetDebugConfig(m_WebView, ptrDebug, ptrParam);
        }

        /// <summary>
        /// 重新载入
        /// </summary>
        public void Reload()
        {
            MBVIP_API.mbReload(m_WebView);
        }

        /// <summary>
        /// 停止载入
        /// </summary>
        public void StopLoading()
        {
            MBVIP_API.mbStopLoading(m_WebView);
        }

        /// <summary>
        /// 后退
        /// </summary>
        public void GoBack()
        {
            MBVIP_API.mbGoBack(m_WebView);
        }

        /// <summary>
        /// 前进
        /// </summary>
        public void GoForward()
        {
            MBVIP_API.mbGoForward(m_WebView);
        }

        /// <summary>
        /// 运行一段js
        /// </summary>
        /// <param name="strJs"></param>
        public void RunJs(string strJs)
        {
            IntPtr ptrJs = MBVIP_Common.StrToUtf8Ptr(strJs);
            IntPtr ptrFrameId = MBVIP_API.mbWebFrameGetMainFrame(m_WebView);

            MBVIP_API.mbRunJs(m_WebView, ptrFrameId, ptrJs, 1, m_mbRunJsCallback, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// 获取页面html代码
        /// </summary>
        public void GetSource()
        {
            MBVIP_API.mbGetSource(m_WebView, m_mbGetSourceCallback, IntPtr.Zero);
        }

        /// <summary>
        /// 设置mimeType，需在OnLoadUrlBegin事件中调用
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <param name="strMimeType"></param>
        public void SetMimeType(IntPtr ptrJob, string strMimeType)
        {
            MBVIP_API.mbNetSetMIMEType(ptrJob, MBVIP_Common.StrToUtf8Ptr(strMimeType));
        }

        /// <summary>
        /// 获取mimeType，需在OnLoadUrlBegin事件中调用
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <returns></returns>
        public string GetMimeType(IntPtr ptrJob)
        {
            IntPtr ptrRet = MBVIP_API.mbNetGetMIMEType(ptrJob);
            return MBVIP_Common.UTF8PtrToStr(ptrRet);
        }

        /// <summary>
        /// 设置HTTP请求头，需在OnLoadUrlBegin事件中调用
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        public void SetHttpHeaderField(IntPtr ptrJob, string strKey, string strValue)
        {
            IntPtr ptrKey = MBVIP_Common.StrToUtf8Ptr(strKey);
            IntPtr ptrValue = MBVIP_Common.StrToUtf8Ptr(strValue);
            MBVIP_API.mbNetSetHTTPHeaderField(ptrJob, ptrKey, ptrValue, 0);
        }

        /// <summary>
        /// 获取HTTP请求头字段，需在OnLoadUrlBegin事件中调用
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <param name="strKey"></param>
        /// <param name="iRequestOrResponse"></param>
        /// <returns></returns>
        public string GetHttpHeaderField(IntPtr ptrJob, string strKey, int iRequestOrResponse)
        {
            IntPtr ptrValue = MBVIP_API.mbNetGetHTTPHeaderField(ptrJob, strKey, iRequestOrResponse);
            return MBVIP_Common.UTF8PtrToStr(ptrValue);
        }

        /// <summary>
        /// 设置网络数据，需在OnLoadUrlBegin事件中调用
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <param name="data"></param>
        public void NetSetData(IntPtr ptrJob, byte[] data)
        {
            MBVIP_API.mbNetSetData(ptrJob, data, data.Length);
        }

        /// <summary>
        /// 设置网络数据，需在OnLoadUrlBegin事件中调用
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <param name="strData">string数据</param>
        public void NetSetData(IntPtr ptrJob, string strData)
        {
            byte[] data = Encoding.UTF8.GetBytes(strData);
            NetSetData(ptrJob, data);
        }

        /// <summary>
        /// 设置网络数据，需在OnLoadUrlBegin事件中调用
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <param name="img">图片数据</param>
        /// <param name="fmt">图片格式</param>
        public void NetSetData(IntPtr ptrJob, Image img, ImageFormat fmt)
        {
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, fmt);
                data = ms.GetBuffer();
            }

            NetSetData(ptrJob, data);
        }

        /// <summary>
        /// 需在OnLoadUrlBegin事件中调用。如果设置了此钩子，则会缓存获取到的网络数据，
        /// 并在这次网络请求结束后调用mbOnLoadUrlEnd设置的回调，同时传递缓存的数据。在此期间，mb不会处理网络数据。
        /// 如果在OnLoadUrlBegin事件里没设置mbNetHookRequest，则不会触发mbOnLoadUrlEnd回调。
        /// </summary>
        /// <param name="ptrJob"></param>
        public void NetHookRequest(IntPtr ptrJob)
        {
            MBVIP_API.mbNetHookRequest(ptrJob);
        }

        /// <summary>
        /// 设置Cookie，格式必须是PRODUCTINFO=webxpress; domain=.fidelity.com; path=/; secure的标准格式
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="strCookie"></param>
        public void SetCookie(string strUrl, string strCookie)
        {
            IntPtr ptrUrl = MBVIP_Common.StrToUtf8Ptr(strUrl);
            IntPtr ptrCookie = MBVIP_Common.StrToUtf8Ptr(strCookie);
            MBVIP_API.mbSetCookie(m_WebView, ptrUrl, ptrCookie);
        }

        /// <summary>
        /// 设置菜单是否显示
        /// </summary>
        /// <param name="item"></param>
        /// <param name="bShow"></param>
        public void SetContextMenuItemShow(mbMenuItemId item, bool bShow)
        {
            MBVIP_API.mbSetContextMenuItemShow(m_WebView, item, bShow ? 1 : 0);
        }

        #endregion
    }
}
