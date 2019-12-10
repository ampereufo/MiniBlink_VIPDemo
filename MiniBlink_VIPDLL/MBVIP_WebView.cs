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
        private mbOnCallUiThreadCallback m_mbCallUiThreadCallback = null;


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
                iMsg = msg;
                this.wParam = wParam;
                this.lParam = lParam;
            }

            public IntPtr Handle { get; }
            public int iMsg { get; }
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
                byteBuffer = MBVIP_Common.UTF8PtrToByte(buffer);
                Rect = (mbRect)MBVIP_Common.UTF8PtrToStruct(rect, new mbRect().GetType());
                iWidth = width;
                iHeight = height;
            }

            public byte[] byteBuffer { get; }
            public mbRect Rect { get; }
            public int iWidth { get; }
            public int iHeight { get; }
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
                PdfData = MBVIP_Common.UTF8PtrToByte(data);
            }

            public byte[] PdfData { get; }
        }

        public class RunJsEventArgs : MiniblinkEventArgs
        {
            public RunJsEventArgs(IntPtr webView, IntPtr param, IntPtr es, ulong v) : base(webView)
            {
                fRet = MBVIP_API.mbJsToDouble(es, v);
                IntPtr ptrRet = MBVIP_API.mbJsToString(es, v);
                strRet = MBVIP_Common.UTF8PtrToStr(ptrRet);
                bRet = MBVIP_API.mbJsToBoolean(es, v) == 1 ? true : false;
            }

            public double fRet { get; }
            public string strRet { get; }
            public bool bRet { get; }
        }

        public class JsQueryEventArgs : MiniblinkEventArgs
        {
            public JsQueryEventArgs(IntPtr webView, IntPtr param, IntPtr es, ulong queryId, int customMsg, IntPtr request) : base(webView)
            {
                fNumRet = MBVIP_API.mbJsToDouble(es, queryId);
                IntPtr ptrRet = MBVIP_API.mbJsToString(es, queryId);
                strRet = MBVIP_Common.UTF8PtrToStr(ptrRet);
                bRet = MBVIP_API.mbJsToBoolean(es, queryId) == 1 ? true : false;
                iCustomMsg = customMsg;
                strRequest = MBVIP_Common.UTF8PtrToStr(request);
            }

            public double fNumRet { get; }
            public string strRet { get; }
            public bool bRet { get; }
            public int iCustomMsg { get; }
            public string strRequest { get; }
        }

        public class TitleChangedEventArgs : MiniblinkEventArgs
        {
            public TitleChangedEventArgs(IntPtr webView, IntPtr param, IntPtr title) : base(webView)
            {
                strTitle = MBVIP_Common.UTF8PtrToStr(title);
            }

            public string strTitle { get; }
        }

        public class MouseOverUrlChangedEventArgs : MiniblinkEventArgs
        {
            public MouseOverUrlChangedEventArgs(IntPtr webView, IntPtr param, IntPtr url) : base(webView)
            {
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
            }

            public string strUrl { get; }
        }

        public class UrlChangedEventArgs : MiniblinkEventArgs
        {
            public UrlChangedEventArgs(IntPtr webView, IntPtr param, IntPtr url, int canGoBack, int canGoForward) : base(webView)
            {
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                iCanGoBack = canGoBack;
                iCanGoForward = canGoForward;
            }

            public string strUrl { get; }
            public int iCanGoBack { get; }
            public int iCanGoForward { get; }
        }

        public class UrlChangedEventArgs2 : MiniblinkEventArgs
        {
            public UrlChangedEventArgs2(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url) : base(webView)
            {
                ptrFrameId = frameId;
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
            }

            public IntPtr ptrFrameId { get; }
            public string strUrl { get; }
        }

        public class AlertBoxEventArgs : MiniblinkEventArgs
        {
            public AlertBoxEventArgs(IntPtr webView, IntPtr param, IntPtr msg) : base(webView)
            {
                strMsg = MBVIP_Common.UTF8PtrToStr(msg);
            }

            public string strMsg { get; }
        }

        public class ConfirmBoxEventArgs : MiniblinkEventArgs
        {
            public ConfirmBoxEventArgs(IntPtr webView, IntPtr param, IntPtr msg) : base(webView)
            {
                strMsg = MBVIP_Common.UTF8PtrToStr(msg);
                iRet = msg == IntPtr.Zero ? 0 : 1;
            }

            public string strMsg { get; set; }
            public int iRet { get; }
        }

        public class PromptBoxEventArgs : MiniblinkEventArgs
        {
            public PromptBoxEventArgs(IntPtr webView, IntPtr param, IntPtr msg, IntPtr defaultResult) : base(webView)
            {
                strMsg = MBVIP_Common.UTF8PtrToStr(msg);
                strResult = MBVIP_Common.UTF8PtrToStr(defaultResult);
                ptrRet = msg;
            }

            public string strMsg { get; set; }
            public string strResult { get; }
            public IntPtr ptrRet { get; }
        }

        public class NavigationEventArgs : MiniblinkEventArgs
        {
            public NavigationEventArgs(IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url) : base(webView)
            {
                NavigationType = navigationType;
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                iRet = url == IntPtr.Zero ? 0 : 1;
            }

            public mbNavigationType NavigationType { get; set; }
            public string strUrl { get; }
            public int iRet { get; }
        }

        public class CreateViewEventArgs : MiniblinkEventArgs
        {
            private IntPtr m_windowFeatures;
            public CreateViewEventArgs(IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url, IntPtr windowFeatures) : base(webView)
            {
                NavigationType = navigationType;
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                m_windowFeatures = windowFeatures;
                NewWebViewHandle = webView;
            }

            public mbNavigationType NavigationType { get; }
            public IntPtr NewWebViewHandle { get; set; }
            public string strUrl { get; }

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
                ptrFrameId = frameId;
                strContext = MBVIP_Common.UTF8PtrToStr(context);
                iExtensionGroup = extensionGroup;
                iWorldId = worldId;
            }

            public IntPtr ptrFrameId { get; }
            public string strContext { get; }
            public int iExtensionGroup { get; }
            public int iWorldId { get; }
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
            public LoadingFinishEventArgs(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, mbLoadingResult result, IntPtr failedReason) : base(webView)
            {
                ptrFrameId = frameId;
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                LoadingResult = result;
                strFailedReason = MBVIP_Common.UTF8PtrToStr(failedReason);
            }

            public IntPtr ptrFrameId { get; }
            public string strUrl { get; }
            public mbLoadingResult LoadingResult { get; }
            public string strFailedReason { get; }
        }

        public class DownloadEventArgs : MiniblinkEventArgs
        {
            public DownloadEventArgs(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, IntPtr downloadJob) : base(webView)
            {
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
            }

            /// <summary>
            /// 设置是否取消，true表示取消
            /// </summary>
            public bool bCancel { get; set; }
            public string strUrl { get; }
        }

        public class ConsoleEventArgs : MiniblinkEventArgs
        {
            public ConsoleEventArgs(IntPtr webView, IntPtr param, mbConsoleLevel level, IntPtr message, IntPtr sourceName, uint sourceLine, IntPtr stackTrace) : base(webView)
            {
                Level = level;
                strMessage = MBVIP_Common.UTF8PtrToStr(message);
                strSourceName = MBVIP_Common.UTF8PtrToStr(sourceName);
                iSourceLine = sourceLine;
                strStackTrace = MBVIP_Common.UTF8PtrToStr(stackTrace);
            }

            public mbConsoleLevel Level { get; }
            public string strMessage { get; }
            public string strSourceName { get; }
            public uint iSourceLine { get; }
            public string strStackTrace { get; }
        }

        public class LoadUrlBeginEventArgs : MiniblinkEventArgs
        {
            public LoadUrlBeginEventArgs(IntPtr webView, IntPtr param, IntPtr url, IntPtr job) : base(webView)
            {
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
            }

            /// <summary>
            /// 是否取消载入，true 表示取消
            /// </summary>
            public bool bCancel { get; set; }
            public string strUrl { get; }
        }

        public class LoadUrlEndEventArgs : MiniblinkEventArgs
        {
            public LoadUrlEndEventArgs(IntPtr webView, IntPtr param, IntPtr url, IntPtr job, IntPtr buf, int len) : base(webView)
            {
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                byteData = MBVIP_Common.UTF8PtrToByte(buf);
            }

            public string strUrl { get; }
            public byte[] byteData { get; }
        }

        public class WillReleaseScriptContextEventArgs : MiniblinkEventArgs
        {
            public WillReleaseScriptContextEventArgs(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr context, int worldId) : base(webView)
            {
                ptrFrameId = frameId;
                strContext = MBVIP_Common.UTF8PtrToStr(context);
                iWorldId = worldId;
            }

            public IntPtr ptrFrameId { get; }
            public string strContext { get; }
            public int iWorldId { get; }
        }

        public class NetResponseEventArgs : MiniblinkEventArgs
        {
            public NetResponseEventArgs(IntPtr webView, IntPtr param, IntPtr url, IntPtr job) : base(webView)
            {
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                iRet = url == IntPtr.Zero ? 0 : 1; ;
            }

            public string strUrl { get; }
            public int iRet { get; }
        }

        public class NetGetFaviconEventArgs : MiniblinkEventArgs
        {
            public NetGetFaviconEventArgs(IntPtr webView, IntPtr param, IntPtr url, IntPtr buf) : base(webView)
            {
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                byteBuf = MBVIP_Common.UTF8PtrToByte(buf);
            }

            public string strUrl { get; }
            public byte[] byteBuf { get; }
        }

        public class CanGoBackForwardEventArgs : MiniblinkEventArgs
        {
            public CanGoBackForwardEventArgs(IntPtr webView, IntPtr param, MbAsynRequestState state, int b) : base(webView)
            {
                State = state;
                bRet = state == MbAsynRequestState.kMbAsynRequestStateOk ? (b == 1 ? true : false) : false;
            }

            public MbAsynRequestState State { get; }
            public bool bRet { get; }
        }

        public class GetCookieEventArgs : MiniblinkEventArgs
        {
            public GetCookieEventArgs(IntPtr webView, IntPtr param, MbAsynRequestState state, IntPtr cookie) : base(webView)
            {
                State = state;
                strCookie = State == MbAsynRequestState.kMbAsynRequestStateOk ? MBVIP_Common.UTF8PtrToStr(cookie) : null; ;
            }

            public MbAsynRequestState State { get; }
            public string strCookie { get; }
        }

        public class GetSourceEventArgs : MiniblinkEventArgs
        {
            public GetSourceEventArgs(IntPtr webView, IntPtr param, IntPtr mhtml) : base(webView)
            {
                strHtmlCode = MBVIP_Common.UTF8PtrToStr(mhtml);
            }

            public string strHtmlCode { get; }
        }

        public class GetContentAsMarkupEventArgs : MiniblinkEventArgs
        {
            public GetContentAsMarkupEventArgs(IntPtr webView, IntPtr param, IntPtr content, ulong size) : base(webView)
            {
                strContent = MBVIP_Common.UTF8PtrToStr(content); ;
            }

            public string strContent { get; }
        }

        public class UrlRequestWillRedirectEventArgs : MiniblinkEventArgs
        {
            public UrlRequestWillRedirectEventArgs(IntPtr webView, IntPtr param, IntPtr oldRequest, IntPtr request, IntPtr redirectResponse) : base(webView)
            {
                strOldRequest = MBVIP_Common.UTF8PtrToStr(oldRequest);
                strRequest = MBVIP_Common.UTF8PtrToStr(request);
                strRedirectResponse = MBVIP_Common.UTF8PtrToStr(redirectResponse);
            }

            public string strOldRequest { get; }
            public string strRequest { get; }
            public string strRedirectResponse { get; }
        }

        public class UrlRequestDidReceiveResponseEventArgs : MiniblinkEventArgs
        {
            public UrlRequestDidReceiveResponseEventArgs(IntPtr webView, IntPtr param, IntPtr request, IntPtr response) : base(webView)
            {
                strRequest = MBVIP_Common.UTF8PtrToStr(request);
                strResponse = MBVIP_Common.UTF8PtrToStr(response);
            }

            public string strRequest { get; }
            public string strResponse { get; }
        }

        public class UrlRequestDidReceiveDataEventArgs : MiniblinkEventArgs
        {
            public UrlRequestDidReceiveDataEventArgs(IntPtr webView, IntPtr param, IntPtr request, IntPtr data, int dataLength) : base(webView)
            {
                strRequest = MBVIP_Common.UTF8PtrToStr(request);
                byteData = MBVIP_Common.UTF8PtrToByte(data);
            }

            public string strRequest { get; }
            public byte[] byteData { get; }
        }

        public class UrlRequestDidFailEventArgs : MiniblinkEventArgs
        {
            public UrlRequestDidFailEventArgs(IntPtr webView, IntPtr param, IntPtr request, IntPtr error) : base(webView)
            {
                strRequest = MBVIP_Common.UTF8PtrToStr(request);
                strError = MBVIP_Common.UTF8PtrToStr(error);
            }

            public string strRequest { get; }
            public string strError { get; }
        }

        public class UrlRequestDidFinishLoadingEventArgs : MiniblinkEventArgs
        {
            public UrlRequestDidFinishLoadingEventArgs(IntPtr webView, IntPtr param, IntPtr request, double finishTime) : base(webView)
            {
                strRequest = MBVIP_Common.UTF8PtrToStr(request);
                fFinishTime = finishTime;
            }

            public string strRequest { get; }
            public double fFinishTime { get; }
        }

        public class NetJobDataRecvEventArgs
        {
            public NetJobDataRecvEventArgs(IntPtr ptr, IntPtr job, IntPtr data, int length)
            {
                byteData = MBVIP_Common.UTF8PtrToByte(data);
            }

            public byte[] byteData { get; }
        }

        public class NetJobDataFinishEventArgs
        {
            public NetJobDataFinishEventArgs(IntPtr ptr, IntPtr job, mbLoadingResult result)
            {
                Result = result;
            }

            public mbLoadingResult Result { get; }
        }

        public class PopupDialogSaveNameEventArgs
        {
            public PopupDialogSaveNameEventArgs(IntPtr ptr, IntPtr filePath)
            {
                strPath = MBVIP_Common.UnicodePtrToStr(filePath);
            }

            public string strPath { get; }
        }

        public class DownloadInBlinkThreadEventArgs : MiniblinkEventArgs
        {
            public DownloadInBlinkThreadEventArgs(IntPtr webView, IntPtr param, ulong expectedContentLength, IntPtr url, IntPtr mime, IntPtr disposition, IntPtr job, IntPtr dataBind) : base(webView)
            {
                iLength = expectedContentLength;
                strUrl = MBVIP_Common.UTF8PtrToStr(url);
                strMime = MBVIP_Common.UTF8PtrToStr(mime);
                strDisposition = MBVIP_Common.UTF8PtrToStr(disposition);
                DownloadRet = expectedContentLength == 0 ? mbDownloadOpt.kMbDownloadOptCancel : mbDownloadOpt.kMbDownloadOptCacheData;
                byteData = MBVIP_Common.UTF8PtrToByte(dataBind);
            }

            public ulong iLength { get; }
            public string strUrl { get; }
            public string strMime { get; }
            public string strDisposition { get; }
            public mbDownloadOpt DownloadRet { get; }
            public byte[] byteData { get; }
        }

        public class PrintPdfDataEventArgs : MiniblinkEventArgs
        {
            public PrintPdfDataEventArgs(IntPtr webView, IntPtr param, IntPtr data) : base(webView)
            {
                byteData = MBVIP_Common.UTF8PtrToByte(data);
            }

            public byte[] byteData { get; }
        }

        public class PrintBitmapEventArgs : MiniblinkEventArgs
        {
            public PrintBitmapEventArgs(IntPtr webView, IntPtr param, IntPtr data, ulong size) : base(webView)
            {
                byteData = MBVIP_Common.UTF8PtrToByte(data);
            }

            public byte[] byteData { get; }
        }

        public class WindowClosingEventArgs : MiniblinkEventArgs
        {
            public WindowClosingEventArgs(IntPtr webView, IntPtr param) : base(webView)
            {

            }

            /// <summary>
            /// 1取消关闭，0不取消
            /// </summary>
            public int iCancel { get; set; }
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
                Region = (mbDraggableRegion)MBVIP_Common.UTF8PtrToStruct(rects, new mbDraggableRegion().GetType());
                iRectCount = rectCount;
            }

            public mbDraggableRegion Region { get; }
            public int iRectCount { get; }
        }

        public class PrintingEventArgs : MiniblinkEventArgs
        {
            public PrintingEventArgs(IntPtr webView, IntPtr param, mbPrintintStep step, IntPtr hDC, IntPtr settings, int pageCount) : base(webView)
            {
                mStep = step;
                ptrHDC = hDC;
                Settings = (mbPrintintSettings)MBVIP_Common.UTF8PtrToStruct(settings, new mbPrintintSettings().GetType());
                iPageCount = pageCount;
            }

            public mbPrintintStep mStep { get; }
            public IntPtr ptrHDC { get; }
            public mbPrintintSettings Settings { get; }
            public int iPageCount { get; }
        }

        public class ImageBufferToDataUrlEventArgs : MiniblinkEventArgs
        {
            public ImageBufferToDataUrlEventArgs(IntPtr webView, IntPtr param, IntPtr data, ulong size) : base(webView)
            {
                byteData = MBVIP_Common.UTF8PtrToByte(data);
            }

            public byte[] byteData { get; set; }
        }

        public class ScreenshotEventArgs : MiniblinkEventArgs
        {
            public ScreenshotEventArgs(IntPtr webView, IntPtr param, IntPtr data, ulong size) : base(webView)
            {
                byteData = MBVIP_Common.UTF8PtrToByte(data);
            }

            public byte[] byteData { get; }
        }

        public class CallUiThreadEventArgs : MiniblinkEventArgs
        {
            public CallUiThreadEventArgs(IntPtr webView, IntPtr paramOnInThread) : base(webView)
            {
                strParamOnInThread = MBVIP_Common.UTF8PtrToStr(paramOnInThread);
            }

            public string strParamOnInThread { get; }
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
                if (m_mbMouseOverUrlChangedHandler == null)
                {

                }

                m_mbMouseOverUrlChangedHandler += value;
            }

            remove
            {
                m_mbMouseOverUrlChangedHandler -= value;

                if (m_mbMouseOverUrlChangedHandler == null)
                {

                }
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
                if (m_mbUrlChangedHandler2 == null)
                {
                    MBVIP_API.mbOnURLChanged2(m_WebView, m_mbUrlChangedCallback2, IntPtr.Zero);
                }

                m_mbUrlChangedHandler2 += value;
            }

            remove
            {
                m_mbUrlChangedHandler2 -= value;

                if (m_mbUrlChangedHandler2 == null)
                {
                    MBVIP_API.mbOnURLChanged2(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<AlertBoxEventArgs> onAlertBox
        {
            add
            {
                if (m_mbAlertBoxHandler == null)
                {
                    MBVIP_API.mbOnAlertBox(m_WebView, m_mbAlertBoxCallback, IntPtr.Zero);
                }

                m_mbAlertBoxHandler += value;
            }

            remove
            {
                m_mbAlertBoxHandler -= value;

                if (m_mbAlertBoxHandler == null)
                {
                    MBVIP_API.mbOnAlertBox(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<ConfirmBoxEventArgs> onConfirmBox
        {
            add
            {
                if (m_mbConfirmBoxHandler == null)
                {
                    MBVIP_API.mbOnConfirmBox(m_WebView, m_mbConfirmBoxCallback, IntPtr.Zero);
                }

                m_mbConfirmBoxHandler += value;
            }

            remove
            {
                m_mbConfirmBoxHandler -= value;

                if (m_mbConfirmBoxHandler == null)
                {
                    MBVIP_API.mbOnConfirmBox(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<PromptBoxEventArgs> onPromptBox
        {
            add
            {
                if (m_mbPromptBoxHandler == null)
                {
                    MBVIP_API.mbOnPromptBox(m_WebView, m_mbPromptBoxCallback, IntPtr.Zero);
                }

                m_mbPromptBoxHandler += value;
            }

            remove
            {
                m_mbPromptBoxHandler -= value;

                if (m_mbPromptBoxHandler == null)
                {
                    MBVIP_API.mbOnPromptBox(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<NavigationEventArgs> onNavigation
        {
            add
            {
                if (m_mbNavigationHandler == null)
                {
                    MBVIP_API.mbOnNavigation(m_WebView, m_mbNavigationCallback, IntPtr.Zero);
                }

                m_mbNavigationHandler += value;
            }

            remove
            {
                m_mbNavigationHandler -= value;
                if (m_mbNavigationHandler == null)
                {
                    MBVIP_API.mbOnNavigation(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<NavigationEventArgs> OnNavigationSync
        {
            add
            {
                if (m_mbNavigationHandler == null)
                {
                    MBVIP_API.mbOnNavigation(m_WebView, m_mbNavigationCallback, IntPtr.Zero);
                }

                m_mbNavigationHandler += value;
            }

            remove
            {
                m_mbNavigationHandler -= value;
                if (m_mbNavigationHandler == null)
                {
                    MBVIP_API.mbOnNavigation(m_WebView, null, IntPtr.Zero);
                }
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
                if (m_mbCloseHandler == null)
                {
                    MBVIP_API.mbOnClose(m_WebView, m_mbCloseCallback, IntPtr.Zero);
                }

                m_mbCloseHandler += value;
            }

            remove
            {
                m_mbCloseHandler -= value;

                if (m_mbCloseHandler == null)
                {
                    MBVIP_API.mbOnClose(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<DestroyEventArgs> onDestroy
        {
            add
            {
                if (m_mbDestroyHandler == null)
                {
                    MBVIP_API.mbOnDestroy(m_WebView, m_mbDestroyCallback, IntPtr.Zero);
                }

                m_mbDestroyHandler += value;
            }

            remove
            {
                m_mbDestroyHandler -= value;

                if (m_mbDestroyHandler == null)
                {
                    MBVIP_API.mbOnDestroy(m_WebView, null, IntPtr.Zero);
                }
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
                if (m_mbDidCreateScriptContextHandler == null)
                {
                    MBVIP_API.mbOnDidCreateScriptContext(m_WebView, m_mbDidCreateScriptContextCallback, IntPtr.Zero);
                }

                m_mbDidCreateScriptContextHandler += value;
            }

            remove
            {
                m_mbDidCreateScriptContextHandler -= value;

                if (m_mbDidCreateScriptContextHandler == null)
                {
                    MBVIP_API.mbOnDidCreateScriptContext(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<GetPluginListEventArgs> onGetPluginList
        {
            add
            {
                if (m_mbGetPluginListHandler == null)
                {
                    MBVIP_API.mbOnPluginList(m_WebView, m_mbGetPluginListCallback, IntPtr.Zero);
                }

                m_mbGetPluginListHandler += value;
            }

            remove
            {
                m_mbGetPluginListHandler -= value;

                if (m_mbGetPluginListHandler == null)
                {
                    MBVIP_API.mbOnPluginList(m_WebView, null, IntPtr.Zero);
                }
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
                if (m_mbConsoleHandler == null)
                {
                    MBVIP_API.mbOnConsole(m_WebView, m_mbConsoleCallback, IntPtr.Zero);
                }

                m_mbConsoleHandler += value;
            }

            remove
            {
                m_mbConsoleHandler -= value;

                if (m_mbConsoleHandler == null)
                {
                    MBVIP_API.mbOnConsole(m_WebView, null, IntPtr.Zero);
                }
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
                if (m_mbDownloadInBlinkThreadHandler == null)
                {
                    MBVIP_API.mbOnDownloadInBlinkThread(m_WebView, m_mbDownloadInBlinkThreadCallback, IntPtr.Zero);
                }

                m_mbDownloadInBlinkThreadHandler += value;
            }

            remove
            {
                m_mbDownloadInBlinkThreadHandler -= value;

                if (m_mbDownloadInBlinkThreadHandler == null)
                {
                    MBVIP_API.mbOnDownloadInBlinkThread(m_WebView, null, IntPtr.Zero);
                }
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
                if (m_mbPrintingHandler == null)
                {
                    MBVIP_API.mbOnPrinting(m_WebView, m_mbPrintingCallback, IntPtr.Zero);
                }

                m_mbPrintingHandler += value;
            }

            remove
            {
                m_mbPrintingHandler -= value;

                if (m_mbPrintingHandler == null)
                {
                    MBVIP_API.mbOnPrinting(m_WebView, null, IntPtr.Zero);
                }
            }
        }

        public event EventHandler<ImageBufferToDataUrlEventArgs> onImageBufferToDataURL
        {
            add
            {
                if (m_mbImageBufferToDataUrlHandler == null)
                {
                    MBVIP_API.mbOnImageBufferToDataURL(m_WebView, m_mbImageBufferToDataUrlCallback, IntPtr.Zero);
                }

                m_mbImageBufferToDataUrlHandler += value;
            }

            remove
            {
                m_mbImageBufferToDataUrlHandler -= value;

                if (m_mbImageBufferToDataUrlHandler == null)
                {
                    MBVIP_API.mbOnImageBufferToDataURL(m_WebView, null, IntPtr.Zero);
                }
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

            m_mbGetPdfPageDataCallback = new mbOnGetPdfPageDataCallback((IntPtr webView, IntPtr param, IntPtr data, uint size) =>
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

                    ptrRet = e.NewWebViewHandle;
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

                    iRet = e.bCancel ? 0 : 1;
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

                    iRet = e.bCancel ? 1 : 0;
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

            m_mbGetContentAsMarkupCallback = new mbGetContentAsMarkupCallback((IntPtr webView, IntPtr param, IntPtr content, uint size) =>
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

            m_mbDownloadInBlinkThreadCallback = new mbDownloadInBlinkThreadCallback((IntPtr webView, IntPtr param, uint expectedContentLength, IntPtr url, IntPtr mime, IntPtr disposition, IntPtr job, IntPtr dataBind) =>
            {
                mbDownloadOpt downloadRet = mbDownloadOpt.kMbDownloadOptCacheData;
                if (m_mbDownloadInBlinkThreadHandler != null)
                {
                    DownloadInBlinkThreadEventArgs e = new DownloadInBlinkThreadEventArgs(webView, param, expectedContentLength, url, mime, disposition, job, dataBind);
                    m_mbDownloadInBlinkThreadHandler(this, e);

                    downloadRet = e.DownloadRet;
                }

                return downloadRet;
            });

            m_mbPrintPdfDataCallback = new mbPrintPdfDataCallback((IntPtr webView, IntPtr param, IntPtr datas) =>
            {
                m_mbPrintPdfDataHandler?.Invoke(this, new PrintPdfDataEventArgs(webView, param, datas));
            });

            m_mbPrintBitmapCallback = new mbPrintBitmapCallback((IntPtr webView, IntPtr param, IntPtr data, uint size) =>
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

                    iRet = e.iCancel;
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

            m_mbImageBufferToDataUrlCallback = new mbImageBufferToDataURLCallback((IntPtr webView, IntPtr param, IntPtr data, uint size) =>
            {
                m_mbImageBufferToDataUrlHandler?.Invoke(this, new ImageBufferToDataUrlEventArgs(webView, param, data, size));
                return MBVIP_API.mbCreateString(data, size);
            });

            m_mbScreenshotCallback = new mbOnScreenshotCallback((IntPtr webView, IntPtr param, IntPtr data, uint size) =>
            {
                m_mbScreenshotHandler?.Invoke(this, new ScreenshotEventArgs(webView, param, data, size));
            });

            m_mbCallUiThreadCallback = new mbOnCallUiThreadCallback((IntPtr webView, IntPtr paramOnInThread) =>
            {
                m_mbCallUiThreadHandler?.Invoke(this, new CallUiThreadEventArgs(webView, paramOnInThread));
            });
        }

        #endregion

        #region --------------------------- 各种属性封装 ---------------------------

        /// <summary>
        /// 获取 WebView 句柄
        /// </summary>
        public IntPtr Handle
        {
            get { return m_WebView; }
        }

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
            set { MBVIP_API.mbSetCookieJarFullPath(m_WebView, MBVIP_Common.StrToUnicodePtr(value)); }
        }

        /// <summary>
        /// 设置Storage文件的完整路径，注意：是完整的，路径，不存在会自动新建
        /// </summary>
        public string StorageFullPath
        {
            set { MBVIP_API.mbSetLocalStorageFullPath(m_WebView, MBVIP_Common.StrToUnicodePtr(value)); }
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

        /// <summary>
        /// 设置或获取缩放比例，默认是1（100%）
        /// </summary>
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
            set { MBVIP_API.mbSetDiskCachePath(m_WebView, MBVIP_Common.StrToUnicodePtr(value)); }
        }

        /// <summary>
        /// 设置磁盘缓存大小
        /// </summary>
        public uint DiskCacheLimit
        {
            set { MBVIP_API.mbSetDiskCacheLimit(m_WebView, value); }
        }

        /// <summary>
        /// 设置磁盘缓存限制
        /// </summary>
        public uint DiskCacheLimitDisk
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

        /// <summary>
        /// 获取主句柄
        /// </summary>
        public IntPtr GetHostHWND
        {
            get { return MBVIP_API.mbGetHostHWND(m_WebView); }
        }

        /// <summary>
        /// 是否允许nodejs
        /// </summary>
        public bool SetNodeJsEnable
        {
            set { MBVIP_API.mbSetNodeJsEnable(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 解锁ViewDC
        /// </summary>
        public bool UnlockViewDC
        {
            set { MBVIP_API.mbUnlockViewDC(m_WebView); }
        }

        /// <summary>
        /// 强制唤醒MB，好像没啥用
        /// </summary>
        public bool Wake
        {
            set { MBVIP_API.mbWake(m_WebView); }
        }

        /// <summary>
        /// 设置支持高度DPI
        /// </summary>
        public bool EnableHighDPISupport
        {
            set { MBVIP_API.mbEnableHighDPISupport(); }
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
        /// 设置无窗口模式下的绘制偏移。在某些情况下（主要是离屏模式），绘制的地方不在真窗口的(0, 0)处，就需要手动调用此接口
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetHandleOffset(int x, int y)
        {
            MBVIP_API.mbSetHandleOffset(m_WebView, x, y);
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
        /// 打开网页
        /// </summary>
        /// <param name="strUrl"></param>
        public void LoadUrl(string strUrl)
        {
            MBVIP_API.mbLoadURL(m_WebView, MBVIP_Common.StrToUtf8Ptr(strUrl));
        }

        public void mbLoadHtmlWithBaseUrl(string strHtml, string strUrl)
        {
            IntPtr ptrHtml = MBVIP_Common.StrToUtf8Ptr(strHtml);
            IntPtr ptrUrl = MBVIP_Common.StrToUtf8Ptr(strUrl);

            MBVIP_API.mbLoadHtmlWithBaseUrl(m_WebView, ptrHtml, ptrUrl);
        }

        /// <summary>
        /// Post数据
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="data"></param>
        /// <param name="iDataLen"></param>
        public void PostData(string strUrl, byte[] data, int iDataLen)
        {
            MBVIP_API.mbPostURL(m_WebView, MBVIP_Common.StrToUtf8Ptr(strUrl), data, iDataLen);
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
            MBVIP_API.mbSetDebugConfig(m_WebView, strDebug, strParam);
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
        /// 异步运行js
        /// </summary>
        /// <param name="ptrFrameId"></param>
        /// <param name="strJs"></param>
        /// <returns></returns>
        public ulong RunJsSync(IntPtr ptrFrameId, string strJs)
        {
            IntPtr ptrJs = MBVIP_Common.StrToUtf8Ptr(strJs);

            return MBVIP_API.mbRunJsSync(m_WebView, ptrFrameId, ptrJs, 1);
        }

        /// <summary>
        /// 查询RunJsSync运行结果
        /// </summary>
        /// <param name="strParam"></param>
        public void mbOnJsQuery(string strParam)
        {
            IntPtr ptrParam = MBVIP_Common.StrToUtf8Ptr(strParam);
            MBVIP_API.mbOnJsQuery(m_WebView, m_mbJsQueryCallback, ptrParam);
        }

        /// <summary>
        /// 响应查询
        /// </summary>
        /// <param name="iQueryId"></param>
        /// <param name="iCustomMsg"></param>
        /// <param name="strResponse"></param>
        public void ResponseQuery(long iQueryId, int iCustomMsg, string strResponse)
        {
            IntPtr ptrResponse = MBVIP_Common.StrToUtf8Ptr(strResponse);
            MBVIP_API.mbResponseQuery(m_WebView, iQueryId, iCustomMsg, ptrResponse);
        }

        /// <summary>
        /// 判断是否是主框架
        /// </summary>
        /// <param name="ptrFrameId"></param>
        /// <returns></returns>
        public bool IsMainFrame(IntPtr ptrFrameId)
        {
            int iRet = MBVIP_API.mbIsMainFrame(m_WebView, ptrFrameId);
            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 获取页面html代码
        /// </summary>
        public void GetSource()
        {
            MBVIP_API.mbGetSource(m_WebView, m_mbGetSourceCallback, IntPtr.Zero);
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
        public void HookRequest(IntPtr ptrJob)
        {
            MBVIP_API.mbNetHookRequest(ptrJob);
        }

        /// <summary>
        /// 需在OnLoadUrlBegin事件中调用，修改请求的url
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <param name="strUrl"></param>
        public void ChangeRequestUrl(IntPtr ptrJob, string strUrl)
        {
            MBVIP_API.mbNetChangeRequestUrl(ptrJob, strUrl);
        }

        /// <summary>
        /// 需在OnLoadUrlBegin事件中调用，继续被中断的任务
        /// </summary>
        /// <param name="ptrJob"></param>
        public void ContinueJob(IntPtr ptrJob)
        {
            MBVIP_API.mbNetContinueJob(ptrJob);
        }

        /// <summary>
        /// 需在OnLoadUrlBegin事件中调用，获取渲染线程原始http头
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <returns></returns>
        public mbSlist GetRawHttpHeadInBlinkThread(IntPtr ptrJob)
        {
            IntPtr ptrStruct = MBVIP_API.mbNetGetRawHttpHeadInBlinkThread(ptrJob);
            return (mbSlist)MBVIP_Common.UTF8PtrToStruct(ptrStruct, new mbSlist().GetType());
        }

        /// <summary>
        /// 需在OnLoadUrlBegin事件中调用，获取渲染线程原始响应头
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <returns></returns>
        public mbSlist GetRawResponseHeadInBlinkThread(IntPtr ptrJob)
        {
            IntPtr ptrStruct = MBVIP_API.mbNetGetRawResponseHeadInBlinkThread(ptrJob);
            return (mbSlist)MBVIP_Common.UTF8PtrToStruct(ptrStruct, new mbSlist().GetType());
        }

        /// <summary>
        /// 需在OnLoadUrlBegin事件中调用，挂起任务，异步提交，配合ContinueJob使用
        /// </summary>
        /// <param name="ptrJob"></param>
        public void HoldJobToAsynCommit(IntPtr ptrJob)
        {
            MBVIP_API.mbNetHoldJobToAsynCommit(ptrJob);
        }

        /// <summary>
        /// 需在OnLoadUrlBegin事件中调用，取消请求
        /// </summary>
        /// <param name="ptrJob"></param>
        public void CancelRequest(IntPtr ptrJob)
        {
            MBVIP_API.mbNetCancelRequest(ptrJob);
        }

        /// <summary>
        /// 设置web通信管道，会将结果设置到mbWebsocketHookCallbacks结构体
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="ptrParam"></param>
        public void SetWebsocketCallback(ref mbWebsocketHookCallbacks callback, IntPtr ptrParam)
        {
            MBVIP_API.mbNetSetWebsocketCallback(m_WebView, ref callback, ptrParam);
        }

        /// <summary>
        /// 通过web通信管道发送文本数据
        /// </summary>
        /// <param name="strChannel"></param>
        /// <param name="data"></param>
        /// <param name="iDataLen"></param>
        public void SendWsText(string strChannel, byte[] data, uint iDataLen)
        {
            MBVIP_API.mbNetSendWsText(MBVIP_Common.StrToUtf8Ptr(strChannel), data, iDataLen);
        }

        /// <summary>
        /// 通过web通信管道发送二进制数据
        /// </summary>
        /// <param name="strChannel"></param>
        /// <param name="data"></param>
        /// <param name="iDataLen"></param>
        public void SendWsBlob(string strChannel, byte[] data, uint iDataLen)
        {
            MBVIP_API.mbNetSendWsBlob(MBVIP_Common.StrToUtf8Ptr(strChannel), data, iDataLen);
        }

        /// <summary>
        /// 获取Post内容
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <returns></returns>
        public mbPostBodyElements GetPostBody(IntPtr ptrJob)
        {
            IntPtr ptrStruct = MBVIP_API.mbNetGetPostBody(ptrJob);
            return (mbPostBodyElements)MBVIP_Common.UTF8PtrToStruct(ptrStruct, new mbPostBodyElements().GetType());
        }

        /// <summary>
        /// 创建多个post内容元素
        /// </summary>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public mbPostBodyElements CreatePostBodyElements(uint iLength)
        {
            IntPtr ptrStruct = MBVIP_API.mbNetCreatePostBodyElements(m_WebView, iLength);
            return (mbPostBodyElements)MBVIP_Common.UTF8PtrToStruct(ptrStruct, new mbPostBodyElements().GetType());
        }

        /// <summary>
        /// 释放多个Post内容元素
        /// </summary>
        /// <param name="elements"></param>
        public void FreePostBodyElements(mbPostBodyElements elements)
        {
            IntPtr ptrStruct = MBVIP_Common.StructToUTF8Ptr(elements);
            MBVIP_API.mbNetFreePostBodyElements(ptrStruct);
        }

        /// <summary>
        /// 创建post内容元素
        /// </summary>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public mbPostBodyElements CreatePostBodyElement()
        {
            IntPtr ptrStruct = MBVIP_API.mbNetCreatePostBodyElement(m_WebView);

            return (mbPostBodyElements)MBVIP_Common.UTF8PtrToStruct(ptrStruct, new mbPostBodyElements().GetType());
        }

        /// <summary>
        /// 释放Post内容元素
        /// </summary>
        /// <param name="elements"></param>
        public void FreePostBodyElement(mbPostBodyElements element)
        {
            IntPtr ptrStruct = MBVIP_Common.StructToUTF8Ptr(element);
            MBVIP_API.mbNetFreePostBodyElement(ptrStruct);
        }

        /// <summary>
        /// 创建网络请求
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="strMethod"></param>
        /// <param name="strMime"></param>
        /// <returns>返回mbWebUrlRequestPtr类型指针，暂未定义</returns>
        public IntPtr CreateWebUrlRequest(string strUrl, string strMethod, string strMime)
        {
            IntPtr ptrUrl = MBVIP_Common.StrToUtf8Ptr(strUrl);
            IntPtr ptrMethod = MBVIP_Common.StrToUtf8Ptr(strMethod);
            IntPtr ptrMime = MBVIP_Common.StrToUtf8Ptr(strMime);

            return MBVIP_API.mbNetCreateWebUrlRequest(ptrUrl, ptrMethod, ptrMime);
        }

        /// <summary>
        /// 向请求中添加http头字段
        /// </summary>
        /// <param name="ptrRequest">mbWebUrlRequestPtr类型指针，暂未定义</param>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        public void AddHTTPHeaderFieldToUrlRequest(IntPtr ptrRequest, string strName, string strValue)
        {
            IntPtr ptrName = MBVIP_Common.StrToUtf8Ptr(strName);
            IntPtr ptrValue = MBVIP_Common.StrToUtf8Ptr(strValue);

            MBVIP_API.mbNetAddHTTPHeaderFieldToUrlRequest(ptrRequest, ptrName, ptrValue);
        }

        /// <summary>
        /// 开始请求
        /// </summary>
        /// <param name="ptrRequest">mbWebUrlRequestPtr类型指针，暂未定义</param>
        /// <param name="RequestCallbacks"></param>
        /// <returns></returns>
        public int StartUrlRequest(IntPtr ptrRequest, mbUrlRequestCallbacks RequestCallbacks)
        {
            return MBVIP_API.mbNetStartUrlRequest(m_WebView, ptrRequest, IntPtr.Zero, ref RequestCallbacks);
        }

        /// <summary>
        /// 获取响应码
        /// </summary>
        /// <param name="ptrResponse">mbWebUrlResponsePtr类型指针，暂未定义</param>
        /// <returns></returns>
        public int GetHttpStatusCode(IntPtr ptrResponse)
        {
            return MBVIP_API.mbNetGetHttpStatusCode(ptrResponse);
        }

        /// <summary>
        /// 获取请求方法
        /// </summary>
        /// <param name="ptrJob"></param>
        /// <returns></returns>
        public mbRequestType GetRequestMethod(IntPtr ptrJob)
        {
            return MBVIP_API.mbNetGetRequestMethod(ptrJob);
        }

        /// <summary>
        /// 获取Content长度
        /// </summary>
        /// <param name="ptrResponse">mbWebUrlResponsePtr类型指针，暂未定义</param>
        /// <returns></returns>
        public long GetExpectedContentLength(IntPtr ptrResponse)
        {
            return MBVIP_API.mbNetGetExpectedContentLength(ptrResponse);
        }

        /// <summary>
        /// 获取响应url
        /// </summary>
        /// <param name="ptrResponse">mbWebUrlResponsePtr类型指针，暂未定义</param>
        /// <returns></returns>
        public string GetResponseUrl(IntPtr ptrResponse)
        {
            IntPtr ptrUrl = MBVIP_API.mbNetGetResponseUrl(ptrResponse);
            return MBVIP_Common.UTF8PtrToStr(ptrUrl);
        }

        /// <summary>
        /// 取消网络请求
        /// </summary>
        /// <param name="iRequestId"></param>
        public void CancelWebUrlRequest(int iRequestId)
        {
            MBVIP_API.mbNetCancelWebUrlRequest(iRequestId);
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
            IntPtr ptrKey = MBVIP_Common.StrToUnicodePtr(strKey);
            IntPtr ptrValue = MBVIP_Common.StrToUnicodePtr(strValue);
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

        /// <summary>
        /// 创建新窗口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="hParent">父窗口句柄</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        /// <returns>返回mbWebView指针</returns>
        public IntPtr CreateWebWindow(mbWindowType type, IntPtr hParent, int x, int y, int iWidth, int iHeight)
        {
            return MBVIP_API.mbCreateWebWindow(type, hParent, x, y, iWidth, iHeight);
        }

        /// <summary>
        /// 创建客户端窗口
        /// </summary>
        /// <param name="hParent">父窗口句柄</param>
        /// <param name="iStyle"></param>
        /// <param name="iStyleEx"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        /// <returns>返回mbWebView指针</returns>
        public IntPtr CreateWebCustomWindow(IntPtr hParent, ulong iStyle, ulong iStyleEx, int x, int y, int iWidth, int iHeight)
        {
            return MBVIP_API.mbCreateWebCustomWindow(hParent, iStyle, iStyleEx, x, y, iWidth, iHeight);
        }

        /// <summary>
        /// 创建MB内部使用的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string CreateString(string str)
        {
            IntPtr ptr = MBVIP_Common.StrToUtf8Ptr(str);
            IntPtr ptrRet = MBVIP_API.mbCreateString(ptr, (uint)str.Length);

            return MBVIP_Common.UTF8PtrToStr(ptrRet);
        }

        /// <summary>
        /// 创建一段不以\0结尾的字符串，其实就是一段内存数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string CreateMemory(string str)
        {
            IntPtr ptr = MBVIP_Common.StrToUtf8Ptr(str);
            IntPtr ptrRet = MBVIP_API.mbCreateStringWithoutNullTermination(ptr, (uint)str.Length);

            return MBVIP_Common.UTF8PtrToStr(ptrRet);
        }

        /// <summary>
        /// 删除CreateString或CreateMemory创建的字符串
        /// </summary>
        /// <param name="str"></param>
        public void DeleteString(string str)
        {
            MBVIP_API.mbDeleteString(MBVIP_Common.StrToUtf8Ptr(str));
        }

        /// <summary>
        /// 获取CreateString或CreateMemory创建的字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public long GetStringLen(string str)
        {
            return MBVIP_API.mbGetStringLen(MBVIP_Common.StrToUtf8Ptr(str));
        }

        /// <summary>
        /// 获取CreateString或CreateMemory创建的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetString(string str)
        {
            IntPtr ptrRet = MBVIP_API.mbGetString(MBVIP_Common.StrToUtf8Ptr(str));
            return MBVIP_Common.UTF8PtrToStr(ptrRet);
        }

        /// <summary>
        /// 添加插件目录
        /// </summary>
        /// <param name="strPah"></param>
        public void AddPluginDirectory(string strPah)
        {
            IntPtr ptrPath = MBVIP_Common.StrToUnicodePtr(strPah);
            MBVIP_API.mbAddPluginDirectory(m_WebView, ptrPath);
        }

        /// <summary>
        /// 设置资源自动清理时间间隔，默认是盟主说忘了
        /// </summary>
        /// <param name="iSecond"></param>
        public void SetResourceGc(int iSecond)
        {
            MBVIP_API.mbSetResourceGc(m_WebView, iSecond);
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public string Base64Encode(string strUrl)
        {
            IntPtr ptrUrl = MBVIP_Common.StrToUtf8Ptr(strUrl);
            ptrUrl = MBVIP_API.mbUtilBase64Encode(ptrUrl);

            return MBVIP_Common.UTF8PtrToStr(ptrUrl);
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public string Base64Decode(string strUrl)
        {
            IntPtr ptrUrl = MBVIP_Common.StrToUtf8Ptr(strUrl);
            ptrUrl = MBVIP_API.mbUtilBase64Decode(ptrUrl);

            return MBVIP_Common.UTF8PtrToStr(ptrUrl);
        }

        /// <summary>
        /// url转义
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public string EncodeURLEscape(string strUrl)
        {
            IntPtr ptrUrl = MBVIP_Common.StrToUtf8Ptr(strUrl);
            ptrUrl = MBVIP_API.mbUtilEncodeURLEscape(ptrUrl);

            return MBVIP_Common.UTF8PtrToStr(ptrUrl);
        }

        /// <summary>
        /// url反转义
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public string DecodeURLEscape(string strUrl)
        {
            IntPtr ptrUrl = MBVIP_Common.StrToUtf8Ptr(strUrl);
            ptrUrl = MBVIP_API.mbUtilDecodeURLEscape(ptrUrl);

            return MBVIP_Common.UTF8PtrToStr(ptrUrl);
        }

        /// <summary>
        /// 创建V8引擎内存快照
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public mbMemBuf CreateV8Snapshot(string strUrl)
        {
            IntPtr ptrUrl = MBVIP_Common.StrToUtf8Ptr(strUrl);
            IntPtr ptrMem = MBVIP_API.mbUtilCreateV8Snapshot(ptrUrl);

            return (mbMemBuf)MBVIP_Common.UTF8PtrToStruct(ptrMem, new mbMemBuf().GetType());
        }

        /// <summary>
        /// 创建内存缓存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public mbMemBuf CreateMemBuf(byte[] data)
        {
            IntPtr ptrBuf = MBVIP_Common.ByteToUtf8Ptr(data);
            IntPtr ptrMem = MBVIP_API.mbCreateMemBuf(m_WebView, ptrBuf, (uint)data.Length);

            return (mbMemBuf)MBVIP_Common.UTF8PtrToStruct(ptrMem, new mbMemBuf().GetType());
        }

        /// <summary>
        /// 释放CreateMemBuf或CreateV8Snapshot创建的内存
        /// </summary>
        /// <param name="buf"></param>
        public void FreeMemBuf(mbMemBuf buf)
        {
            MBVIP_API.mbFreeMemBuf(ref buf);
        }

        /// <summary>
        /// 查询能否前进
        /// </summary>
        /// <param name="strParam"></param>
        public void CanForward(string strParam)
        {
            IntPtr ptrParam = MBVIP_Common.StrToUtf8Ptr(strParam);
            MBVIP_API.mbCanGoForward(m_WebView, m_mbCanGoBackForwardCallback, ptrParam);
        }

        /// <summary>
        /// 查询能否后退
        /// </summary>
        /// <param name="strParam"></param>
        public void CanBack(string strParam)
        {
            IntPtr ptrParam = MBVIP_Common.StrToUtf8Ptr(strParam);
            MBVIP_API.mbCanGoBack(m_WebView, m_mbCanGoBackForwardCallback, ptrParam);
        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="strParam"></param>
        public void GetCookie(string strParam)
        {
            IntPtr ptrParam = MBVIP_Common.StrToUtf8Ptr(strParam);
            MBVIP_API.mbGetCookie(m_WebView, m_mbGetCookieCallback, ptrParam);
        }

        /// <summary>
        /// 获取渲染线程的cookie
        /// </summary>
        /// <returns></returns>
        public string GetCookieOnBlinkThread()
        {
            IntPtr ptrRet = MBVIP_API.mbGetCookieOnBlinkThread(m_WebView);
            return MBVIP_Common.UTF8PtrToStr(ptrRet);
        }

        /// <summary>
        /// 清除cookie
        /// </summary>
        /// <returns></returns>
        public void ClearCookie()
        {
            MBVIP_API.mbClearCookie(m_WebView);
        }

        /// <summary>
        /// 重新设置界面大小
        /// </summary>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        public void Resize(int iWidth, int iHeight)
        {
            MBVIP_API.mbResize(m_WebView, iWidth, iHeight);
        }

        /// <summary>
        /// 执行cookie相关命令
        /// </summary>
        /// <param name="com"></param>
        public void mbPerformCookieCommand(mbCookieCommand com)
        {
            MBVIP_API.mbPerformCookieCommand(m_WebView, com);
        }

        /// <summary>
        /// 全选
        /// </summary>
        public void EditorSelectAll()
        {
            MBVIP_API.mbEditorSelectAll(m_WebView);
        }

        /// <summary>
        /// 复制
        /// </summary>
        public void EditorCopy()
        {
            MBVIP_API.mbEditorCopy(m_WebView);
        }

        /// <summary>
        /// 剪切
        /// </summary>
        public void EditorCut()
        {
            MBVIP_API.mbEditorCut(m_WebView);
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        public void EditorPaste()
        {
            MBVIP_API.mbEditorPaste(m_WebView);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void EditorDelete()
        {
            MBVIP_API.mbEditorDelete(m_WebView);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public void EditorUndo()
        {
            MBVIP_API.mbEditorUndo(m_WebView);
        }

        /// <summary>
        /// 发送鼠标消息
        /// </summary>
        /// <param name="iMessage"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="iFlags"></param>
        /// <returns></returns>
        public bool FireMouseEvent(uint iMessage, int x, int y, uint iFlags)
        {
            int iRet = MBVIP_API.mbFireMouseEvent(m_WebView, iMessage, x, y, iFlags);
            return iRet == 1 ? true : false ;
        }

        /// <summary>
        /// 发送菜单消息
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="iFlags"></param>
        /// <returns></returns>
        public bool FireContextMenuEvent(int x, int y, uint iFlags)
        {
            int iRet = MBVIP_API.mbFireContextMenuEvent(m_WebView, x, y, iFlags);
            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 发送滚轮消息
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="iDelta"></param>
        /// <param name="iFlags"></param>
        /// <returns></returns>
        public bool FireMouseWheelEvent(int x, int y, int iDelta, uint iFlags)
        {
            int iRet = MBVIP_API.mbFireMouseWheelEvent(m_WebView, x, y, iDelta, iFlags);
            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 发送按键按下消息
        /// </summary>
        /// <param name="iVirtualKeyCode"></param>
        /// <param name="iFlags"></param>
        /// <param name="iSystemKey"></param>
        /// <returns></returns>
        public bool FireKeyDownEvent(uint iVirtualKeyCode, uint iFlags, int iSystemKey)
        {
            int iRet = MBVIP_API.mbFireKeyDownEvent(m_WebView, iVirtualKeyCode, iFlags, iSystemKey);
            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 发送按键抬起消息
        /// </summary>
        /// <param name="iVirtualKeyCode"></param>
        /// <param name="iFlags"></param>
        /// <param name="iSystemKey"></param>
        /// <returns></returns>
        public bool FireKeyUpEvent(uint iVirtualKeyCode, uint iFlags, int iSystemKey)
        {
            int iRet = MBVIP_API.mbFireKeyUpEvent(m_WebView, iVirtualKeyCode, iFlags, iSystemKey);
            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 发送按键消息
        /// </summary>
        /// <param name="iCharCode"></param>
        /// <param name="iFlags"></param>
        /// <param name="iSystemKey"></param>
        /// <returns></returns>
        public bool FireKeyPressEvent(uint iCharCode, uint iFlags, int iSystemKey)
        {
            int iRet = MBVIP_API.mbFireKeyPressEvent(m_WebView, iCharCode, iFlags, iSystemKey);
            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 发送通用消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="iMessage"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="strResult"></param>
        /// <returns></returns>
        public bool FireWindowsMessage(IntPtr hWnd, uint iMessage, IntPtr wParam, IntPtr lParam)
        {
            int iRet = MBVIP_API.mbFireWindowsMessage(m_WebView, hWnd, iMessage, wParam, lParam, IntPtr.Zero);
            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        public void SetFocus()
        {
            MBVIP_API.mbSetFocus(m_WebView);
        }

        /// <summary>
        /// 放弃焦点
        /// </summary>
        public void KillFocus()
        {
            MBVIP_API.mbKillFocus(m_WebView);
        }

        /// <summary>
        /// 是否显示窗口
        /// </summary>
        public bool ShowWindow
        {
            set { MBVIP_API.mbShowWindow(m_WebView, value ? 1 : 0); }
        }

        /// <summary>
        /// 插件相关，需在mbGetPluginListCallback中调用
        /// </summary>
        /// <param name="ptrBuilder"></param>
        /// <param name="strName"></param>
        /// <param name="strDesc"></param>
        /// <param name="strFile"></param>
        public void PluginListBuilderAddPlugin(IntPtr ptrBuilder, string strName, string strDesc, string strFile)
        {
            IntPtr ptrName = MBVIP_Common.StrToUtf8Ptr(strName);
            IntPtr PtrDesc = MBVIP_Common.StrToUtf8Ptr(strDesc);
            IntPtr PtrFile = MBVIP_Common.StrToUtf8Ptr(strFile);

            MBVIP_API.mbPluginListBuilderAddPlugin(ptrBuilder, ptrName, PtrDesc, PtrFile);
        }

        /// <summary>
        /// 插件相关，需在mbGetPluginListCallback中调用
        /// </summary>
        /// <param name="ptrBuilder"></param>
        /// <param name="strName"></param>
        /// <param name="strDesc"></param>
        public void PluginListBuilderAddMediaTypeToLastPlugin(IntPtr ptrBuilder, string strName, string strDesc)
        {
            IntPtr ptrName = MBVIP_Common.StrToUtf8Ptr(strName);
            IntPtr PtrDesc = MBVIP_Common.StrToUtf8Ptr(strDesc);

            MBVIP_API.mbPluginListBuilderAddMediaTypeToLastPlugin(ptrBuilder, ptrName, PtrDesc);
        }

        /// <summary>
        /// 插件相关，需在mbGetPluginListCallback中调用
        /// </summary>
        /// <param name="ptrBuilder"></param>
        /// <param name="strFile"></param>
        public void PluginListBuilderAddFileExtensionToLastMediaType(IntPtr ptrBuilder, string strFile)
        {
            IntPtr PtrFile = MBVIP_Common.StrToUtf8Ptr(strFile);

            MBVIP_API.mbPluginListBuilderAddFileExtensionToLastMediaType(ptrBuilder, PtrFile);
        }

        /// <summary>
        /// 下载相关，需在mbDownloadCallback中调用
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public bool PopupDownloadMgr(string strUrl)
        {
            int iRet = MBVIP_API.mbPopupDownloadMgr(m_WebView, strUrl, IntPtr.Zero);

            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 下载相关，需在mbDownloadCallback中调用
        /// </summary>
        /// <param name="iContentLength"></param>
        /// <param name="strUrl"></param>
        /// <param name="strMime"></param>
        /// <param name="strDisposition"></param>
        /// <param name="ptrJob"></param>
        /// <param name="dataBind"></param>
        /// <param name="callbackBind"></param>
        /// <returns></returns>
        public mbDownloadOpt PopupDialogAndDownload(uint iContentLength, string strUrl, string strMime,
            string strDisposition, IntPtr ptrJob, mbNetJobDataBind dataBind, mbDownloadBind callbackBind)
        {
            return MBVIP_API.mbPopupDialogAndDownload(m_WebView, IntPtr.Zero, iContentLength, strUrl, strMime,
                strDisposition, ptrJob, ref dataBind, ref callbackBind);
        }

        /// <summary>
        /// 下载相关，需在mbDownloadCallback中调用
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="iContentLength"></param>
        /// <param name="strUrl"></param>
        /// <param name="strMime"></param>
        /// <param name="strDisposition"></param>
        /// <param name="ptrJob"></param>
        /// <param name="dataBind"></param>
        /// <param name="callbackBind"></param>
        /// <returns></returns>
        public mbDownloadOpt DownloadByPath(string strPath, uint iContentLength, string strUrl, string strMime,
            string strDisposition, IntPtr ptrJob, mbNetJobDataBind dataBind, mbDownloadBind callbackBind)
        {
            IntPtr ptrPath = MBVIP_Common.StrToUnicodePtr(strPath);
            return MBVIP_API.mbDownloadByPath(m_WebView, IntPtr.Zero, ptrPath, iContentLength, strUrl, strMime,
                strDisposition, ptrJob, ref dataBind, ref callbackBind);
        }

        /// <summary>
        /// 获取pdf页面数据，结果在m_mbGetPdfPageDataCallback回调中取
        /// </summary>
        public void GetPdfPageData()
        {
            MBVIP_API.mbGetPdfPageData(m_WebView, m_mbGetPdfPageDataCallback, IntPtr.Zero);
        }

        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="settings"></param>
        public void UtilScreenshot(mbScreenshotSettings settings)
        {
            MBVIP_API.mbUtilScreenshot(m_WebView, ref settings, m_mbScreenshotCallback, IntPtr.Zero);
        }

        /// <summary>
        /// 打印pdf
        /// </summary>
        /// <param name="ptrFrameId"></param>
        /// <param name="settings"></param>
        public void UtilPrintToPdf(IntPtr ptrFrameId, mbPrintSettings settings)
        {
            MBVIP_API.mbUtilPrintToPdf(m_WebView, ptrFrameId, ref settings, m_mbPrintPdfDataCallback, IntPtr.Zero);
        }

        /// <summary>
        /// 打印位图
        /// </summary>
        /// <param name="ptrFrameId"></param>
        /// <param name="settings"></param>
        public void UtilPrintToBitmap(IntPtr ptrFrameId, mbScreenshotSettings settings)
        {
            MBVIP_API.mbUtilPrintToBitmap(m_WebView, ptrFrameId, ref settings, m_mbPrintBitmapCallback, IntPtr.Zero);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="ptrFrameId"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public bool UtilPrint(IntPtr ptrFrameId, mbPrintSettings settings)
        {
            int iRet = MBVIP_API.mbUtilPrint(m_WebView, ptrFrameId, ref settings);
            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="strDefaultPath"></param>
        /// <returns></returns>
        public bool UtilIsRegistered(string strDefaultPath)
        {
            IntPtr ptrDefaultPath = MBVIP_Common.StrToUnicodePtr(strDefaultPath);
            int iRet = MBVIP_API.mbUtilIsRegistered(ptrDefaultPath);

            return iRet == 1 ? true : false;
        }

        /// <summary>
        /// 创建请求码
        /// </summary>
        /// <param name="strRegisterInfo"></param>
        /// <returns></returns>
        public string UtilCreateRequestCode(string strRegisterInfo)
        {
            return MBVIP_API.mbUtilCreateRequestCode(strRegisterInfo);
        }

        /// <summary>
        /// 从标记获取内容，结果在m_mbGetContentAsMarkupCallback中取
        /// </summary>
        /// <param name="frameHandle"></param>
        public void GetContentAsMarkup(IntPtr frameHandle)
        {
            MBVIP_API.mbGetContentAsMarkup(m_WebView, m_mbGetContentAsMarkupCallback, IntPtr.Zero, frameHandle);
        }

        /// <summary>
        /// 获取网站logo
        /// </summary>
        public void GetFavicon()
        {
            MBVIP_API.mbOnNetGetFavicon(m_WebView, m_mbNetGetFaviconCallback, IntPtr.Zero);
        }

        #endregion
    }
}