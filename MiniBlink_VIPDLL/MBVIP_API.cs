using System;
using System.Runtime.InteropServices;


/// <summary>
/// 挫盟主只写代码不写文档，这个是根据mb.h头文件整理的，有错误找他，别找我
/// 封装以2019-11-11的mb.h文件为基准，后续修改尽量同步
/// 参考kyozy大神，https://gitee.com/kyozy/miniblinknet
/// 合作联系QQ：17136608，违法黑产勿扰，我还年轻！
/// 项目地址：https://github.com/ampereufo/MiniBlink_VIPDemo
/// </summary>
namespace MBVIP
{
    #region 枚举

    /// <summary>
    /// 鼠标标志位
    /// </summary>
    public enum mbMouseFlags
    {
        MB_LBUTTON = 0x01,
        MB_RBUTTON = 0x02,
        MB_SHIFT = 0x04,
        MB_CONTROL = 0x08,
        MB_MBUTTON = 0x10
    }

    /// <summary>
    /// 按键标志位
    /// </summary>
    public enum mbKeyFlags
    {
        MB_EXTENDED = 0x0100,
        MB_REPEAT = 0x4000
    }

    /// <summary>
    /// 鼠标消息
    /// </summary>
    public enum mbMouseMsg
    {
        MB_MSG_MOUSEMOVE = 0x0200,
        MB_MSG_LBUTTONDOWN = 0x0201,
        MB_MSG_LBUTTONUP = 0x0202,
        MB_MSG_LBUTTONDBLCLK = 0x0203,
        MB_MSG_RBUTTONDOWN = 0x0204,
        MB_MSG_RBUTTONUP = 0x0205,
        MB_MSG_RBUTTONDBLCLK = 0x0206,
        MB_MSG_MBUTTONDOWN = 0x0207,
        MB_MSG_MBUTTONUP = 0x0208,
        MB_MSG_MBUTTONDBLCLK = 0x0209,
        MB_MSG_MOUSEWHEEL = 0x020A
    }

    /// <summary>
    /// 代理类型
    /// </summary>
    public enum mbProxyType
    {
        MB_PROXY_NONE,
        MB_PROXY_HTTP,
        MB_PROXY_SOCKS4,
        MB_PROXY_SOCKS4A,
        MB_PROXY_SOCKS5,
        MB_PROXY_SOCKS5HOSTNAME
    }

    /// <summary>
    /// 初始化设置项参数
    /// </summary>
    public enum mbSettingMask
    {
        MB_SETTING_PROXY = 1,
        MB_SETTING_PAINTCALLBACK_IN_OTHER_THREAD = 1 << 2,
        MB_ENABLE_NODEJS = 1 << 3,
        MB_ENABLE_DISABLE_H5VIDEO = 1 << 4,
        MB_ENABLE_DISABLE_PDFVIEW = 1 << 5,
        MB_ENABLE_DISABLE_CC = 1 << 6
    }

    /// <summary>
    /// cookie命令
    /// </summary>
    public enum mbCookieCommand
    {
        mbCookieCommandClearAllCookies,
        mbCookieCommandClearSessionCookies,
        mbCookieCommandFlushCookiesToFile,
        mbCookieCommandReloadCookiesFromFile
    }

    /// <summary>
    /// 导航类型
    /// </summary>
    public enum mbNavigationType
    {
        MB_NAVIGATION_TYPE_LINKCLICK,
        MB_NAVIGATION_TYPE_FORMSUBMITTE,
        MB_NAVIGATION_TYPE_BACKFORWARD,
        MB_NAVIGATION_TYPE_RELOAD,
        MB_NAVIGATION_TYPE_FORMRESUBMITT,
        MB_NAVIGATION_TYPE_OTHER
    }

    /// <summary>
    /// 光标类型
    /// </summary>
    public enum mbCursorInfoType
    {
        kMbCursorInfoPointer,
        kMbCursorInfoCross,
        kMbCursorInfoHand,
        kMbCursorInfoIBeam,
        kMbCursorInfoWait,
        kMbCursorInfoHelp,
        kMbCursorInfoEastResize,
        kMbCursorInfoNorthResize,
        kMbCursorInfoNorthEastResize,
        kMbCursorInfoNorthWestResize,
        kMbCursorInfoSouthResize,
        kMbCursorInfoSouthEastResize,
        kMbCursorInfoSouthWestResize,
        kMbCursorInfoWestResize,
        kMbCursorInfoNorthSouthResize,
        kMbCursorInfoEastWestResize,
        kMbCursorInfoNorthEastSouthWestResize,
        kMbCursorInfoNorthWestSouthEastResize,
        kMbCursorInfoColumnResize,
        kMbCursorInfoRowResize,
        kMbCursorInfoMiddlePanning,
        kMbCursorInfoEastPanning,
        kMbCursorInfoNorthPanning,
        kMbCursorInfoNorthEastPanning,
        kMbCursorInfoNorthWestPanning,
        kMbCursorInfoSouthPanning,
        kMbCursorInfoSouthEastPanning,
        kMbCursorInfoSouthWestPanning,
        kMbCursorInfoWestPanning,
        kMbCursorInfoMove,
        kMbCursorInfoVerticalText,
        kMbCursorInfoCell,
        kMbCursorInfoContextMenu,
        kMbCursorInfoAlias,
        kMbCursorInfoProgress,
        kMbCursorInfoNoDrop,
        kMbCursorInfoCopy,
        kMbCursorInfoNone,
        kMbCursorInfoNotAllowed,
        kMbCursorInfoZoomIn,
        kMbCursorInfoZoomOut,
        kMbCursorInfoGrab,
        kMbCursorInfoGrabbing,
        kMbCursorInfoCustom
    }

    /// <summary>
    /// 页面拖拽操作
    /// </summary>
    public enum mbWebDragOperation
    {
        mbWebDragOperationNone = 0,
        mbWebDragOperationCopy = 1,
        mbWebDragOperationLink = 2,
        mbWebDragOperationGeneric = 4,
        mbWebDragOperationPrivate = 8,
        mbWebDragOperationMove = 16,
        mbWebDragOperationDelete = 32,
        mbWebDragOperationEvery = 0xFFFFFFF
    }

    /// <summary>
    /// 资源类型
    /// </summary>
    public enum mbResourceType
    {
        MB_RESOURCE_TYPE_MAIN_FRAME = 0,       // top level page
        MB_RESOURCE_TYPE_SUB_FRAME = 1,        // frame or iframe
        MB_RESOURCE_TYPE_STYLESHEET = 2,       // a CSS stylesheet
        MB_RESOURCE_TYPE_SCRIPT = 3,           // an external script
        MB_RESOURCE_TYPE_IMAGE = 4,            // an image (jpg/gif/png/etc)
        MB_RESOURCE_TYPE_FONT_RESOURCE = 5,    // a font
        MB_RESOURCE_TYPE_SUB_RESOURCE = 6,     // an "other" subresource.
        MB_RESOURCE_TYPE_OBJECT = 7,           // an object (or embed) tag for a plugin, or a resource that a plugin requested.
        MB_RESOURCE_TYPE_MEDIA = 8,            // a media resource.
        MB_RESOURCE_TYPE_WORKER = 9,           // the main resource of a dedicated worker.
        MB_RESOURCE_TYPE_SHARED_WORKER = 10,   // the main resource of a shared worker.
        MB_RESOURCE_TYPE_PREFETCH = 11,        // an explicitly requested prefetch
        MB_RESOURCE_TYPE_FAVICON = 12,         // a favicon
        MB_RESOURCE_TYPE_XHR = 13,             // a XMLHttpRequest
        MB_RESOURCE_TYPE_PING = 14,            // a ping request for <a ping>
        MB_RESOURCE_TYPE_SERVICE_WORKER = 15,  // the main resource of a service worker.
        MB_RESOURCE_TYPE_LAST_TYPE = 16
    }

    /// <summary>
    /// 网络请求类型
    /// </summary>
    public enum mbRequestType
    {
        kMbRequestTypeInvalidation,
        kMbRequestTypeGet,
        kMbRequestTypePost,
        kMbRequestTypePut,
    }

    /// <summary>
    /// 鼠标右键弹出菜单操作（复制粘贴等）的id
    /// </summary>
    public enum mbMenuItemId
    {
        kMbMenuSelectedAllId = 1 << 1,
        kMbMenuSelectedTextId = 1 << 2,
        kMbMenuUndoId = 1 << 3,
        kMbMenuCopyImageId = 1 << 4,
        kMbMenuInspectElementAtId = 1 << 5,
        kMbMenuCutId = 1 << 6,
        kMbMenuPasteId = 1 << 7,
        kMbMenuPrintId = 1 << 8,
        kMbMenuGoForwardId = 1 << 9,
        kMbMenuGoBackId = 1 << 10,
        kMbMenuReloadId = 1 << 11
    }

    /// <summary>
    /// JS值类型
    /// </summary>
    public enum mbJsType
    {
        kMbJsTypeNumber = 0,
        kMbJsTypeString = 1,
        kMbJsTypeint = 2,
        kMbJsTypeObject = 3,
        kMbJsTypeFunction = 4,
        kMbJsTypeUndefined = 5,
        kMbJsTypeArray = 6,
        kMbJsTypeNull = 7
    }

    /// <summary>
    /// 加载结果
    /// </summary>
    public enum mbLoadingResult
    {
        MB_LOADING_SUCCEEDED,
        MB_LOADING_FAILED,
        MB_LOADING_CANCELED
    }

    /// <summary>
    /// 命令行
    /// </summary>
    public enum mbConsoleLevel
    {
        mbLevelLog = 1,
        mbLevelWarning = 2,
        mbLevelError = 3,
        mbLevelDebug = 4,
        mbLevelInfo = 5,
        mbLevelRevokedError = 6,
        mbLevelLast = mbLevelInfo
    }

    /// <summary>
    /// 异步请求状态
    /// </summary>
    public enum MbAsynRequestState
    {
        kMbAsynRequestStateOk = 0,
        kMbAsynRequestStateFail = 1
    }

    /// <summary>
    /// 下载操作
    /// </summary>
    public enum mbDownloadOpt
    {
        kMbDownloadOptCancel = 0,
        kMbDownloadOptCacheData = 1
    }

    /// <summary>
    /// 网页元素类型
    /// </summary>
    public enum mbHttBodyElementType
    {
        mbHttBodyElementTypeData = 0,
        mbHttBodyElementTypeFile = 1
    }

    /// <summary>
    /// 窗口类型
    /// </summary>
    public enum mbWindowType
    {
        MB_WINDOW_TYPE_POPUP,
        MB_WINDOW_TYPE_TRANSPARENT,
        MB_WINDOW_TYPE_CONTROL
    }

    /// <summary>
    /// 打印状态
    /// </summary>
    public enum mbPrintintStep
    {
        kPrintintStepStart,
        kPrintintStepPreview,
        kPrintintStepPrinting
    }


    /// <summary>
    /// 储存类型
    /// </summary>
    public enum mbStorageType
    {
        // String data with an associated MIME type. Depending on the MIME type, there may be
        // optional metadata attributes as well.
        StorageTypeString,
        // Stores the name of one file being dragged into the renderer.
        StorageTypeFilename,
        // An image being dragged out of the renderer. Contains a buffer holding the image data
        // as well as the suggested name for saving the image to.
        StorageTypeBinaryData,
        // Stores the filesystem URL of one file being dragged into the renderer.
        StorageTypeFileSystemFile
    }

    #endregion

    #region 结构体

    [StructLayout(LayoutKind.Sequential)]
    public struct mbRect
    {
        public int x;
        public int y;
        public int w;
        public int h;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbPoint
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbSize
    {
        public int w;
        public int h;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbProxy
    {
        public mbProxyType type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string hostname;
        public ushort port;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string username;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string password;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbSettings
    {
        public mbProxy proxy;
        public mbSettingMask mask;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbOnBlinkThreadInitCallback blinkThreadInitCallback;
        public IntPtr blinkThreadInitparam;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbViewSettings
    {
        public int size;
        public uint bgColor;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbWindowFeatures
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public int menuBarVisible;
        public int statusBarVisible;
        public int toolBarVisible;
        public int locationBarVisible;
        public int scrollbarsVisible;
        public int resizable;
        public int fullscreen;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbPrintSettings
    {
        public int structSize;
        public int dpi;
        public int width;
        public int height;
        public int marginTop;
        public int marginBottom;
        public int marginLeft;
        public int marginRight;
        public int isPrintPageHeadAndFooter;
        public int isPrintBackgroud;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbMemBuf
    {
        public int size;
        public IntPtr data;
        public ulong length;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Item
    {
        public mbStorageType storageType;

        // Only valid when storageType == StorageTypeString.
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr stringType;
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr stringData;

        // Only valid when storageType == StorageTypeFilename.
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr filenameData;
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr displayNameData;

        // Only valid when storageType == StorageTypeBinaryData.
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr binaryData;

        // Title associated with a link when stringType == "text/uri-list".
        // Filename when storageType == StorageTypeBinaryData.
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr title;

        // Only valid when storageType == StorageTypeFileSystemFile.
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr fileSystemURL;
        public long fileSystemFileSize;

        // Only valid when stringType == "text/html".
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr baseURL;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct mbWebDragData
    {
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr itemList;
        public int itemListLength;
        public int modifierKeyState; // State of Shift/Ctrl/Alt/Meta keys.
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr filesystemId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbSlist
    {
        public IntPtr data;
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbWebsocketHookCallbacks
    {
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal onWillConnect WillConnect;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal onConnected Connected;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal onReceive Receive;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal onSend Send;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal onError Error;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbUrlRequestCallbacks
    {
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbOnUrlRequestWillRedirectCallback willRedirectCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbOnUrlRequestDidReceiveResponseCallback didReceiveResponseCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbOnUrlRequestDidReceiveDataCallback didReceiveDataCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbOnUrlRequestDidFailCallback didFailCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbOnUrlRequestDidFinishLoadingCallback didFinishLoadingCallback;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbNetJobDataBind
    {
        public IntPtr param;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbNetJobDataRecvCallback recvCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbNetJobDataFinishCallback finishCallback;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbDownloadBind
    {
        public IntPtr param;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbNetJobDataRecvCallback recvCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbNetJobDataFinishCallback finishCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal mbPopupDialogSaveNameCallback saveNameCallback;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbPdfDatas
    {
        public int count;
        public IntPtr sizes;
        public IntPtr datas;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbScreenshotSettings
    {
        public int structSize;
        public int width;
        public int height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbPostBodyElement
    {
        public int size;
        public mbHttBodyElementType type;
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr data;
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr filePath;
        public long fileStart;
        public long fileLength; // -1表示到文件结尾
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbPostBodyElements
    {
        public int size;
        [MarshalAs(UnmanagedType.LPStruct)]
        public IntPtr element;
        public long elementSize;
        [MarshalAs(UnmanagedType.I1)]
        public char isDirty;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbDraggableRegion
    {
        public RECT bounds;
        public int draggable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbPrintintSettings
    {
        public int dpi;
        public int width;
        public int height;
        public float scale;
    }


    #endregion

    #region 委托（函数指针）

    // 各种回调
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate IntPtr WndProcCallback(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbPaintUpdatedCallback(IntPtr webView, IntPtr param, IntPtr hdc, int x, int y, int cx, int cy);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbPaintBitUpdatedCallback(IntPtr webView, IntPtr param, IntPtr buffer, IntPtr rect, int width, int height);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnBlinkThreadInitCallback(IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnGetPdfPageDataCallback(IntPtr webView, IntPtr param, IntPtr data, ulong size);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbRunJsCallback(IntPtr webView, IntPtr param, IntPtr es, ulong v);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbJsQueryCallback(IntPtr webView, IntPtr param, IntPtr es, ulong queryId, int customMsg, IntPtr request);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbTitleChangedCallback(IntPtr webView, IntPtr param, IntPtr title);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbMouseOverUrlChangedCallback(IntPtr webView, IntPtr param, IntPtr url);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbUrlChangedCallback(IntPtr webView, IntPtr param, IntPtr url, int canGoBack, int canGoForward);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbUrlChangedCallback2(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbAlertBoxCallback(IntPtr webView, IntPtr param, IntPtr msg);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbConfirmBoxCallback(IntPtr webView, IntPtr param, IntPtr msg);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate IntPtr mbPromptBoxCallback(IntPtr webView, IntPtr param, IntPtr msg, IntPtr defaultResult);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbNavigationCallback(IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate IntPtr mbCreateViewCallback(IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url, IntPtr windowFeatures);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbDocumentReadyCallback(IntPtr webView, IntPtr param, IntPtr frameId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbCloseCallback(IntPtr webView, IntPtr param, IntPtr unuse);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbDestroyCallback(IntPtr webView, IntPtr param, IntPtr unuse);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnShowDevtoolsCallback(IntPtr webView, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbDidCreateScriptContextCallback(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr context, int extensionGroup, int worldId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbGetPluginListCallback(int refresh, IntPtr pluginListBuilder, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbLoadingFinishCallback(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, mbLoadingResult result, IntPtr failedReason);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbDownloadCallback(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, IntPtr downloadJob);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbConsoleCallback(IntPtr webView, IntPtr param, mbConsoleLevel level, IntPtr message, IntPtr sourceName, uint sourceLine, IntPtr stackTrace);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbLoadUrlBeginCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr job);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbLoadUrlEndCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr job, IntPtr buf, int len);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbWillReleaseScriptContextCallback(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr context, int worldId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbNetResponseCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr job);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbNetGetFaviconCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr buf);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbCanGoBackForwardCallback(IntPtr webView, IntPtr param, MbAsynRequestState state, int b);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbGetCookieCallback(IntPtr webView, IntPtr param, MbAsynRequestState state, IntPtr cookie);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbGetSourceCallback(IntPtr webView, IntPtr param, IntPtr mhtml);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbGetContentAsMarkupCallback(IntPtr webView, IntPtr param, IntPtr content, ulong size);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnUrlRequestWillRedirectCallback(IntPtr webView, IntPtr param, IntPtr oldRequest, IntPtr request, IntPtr redirectResponse);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnUrlRequestDidReceiveResponseCallback(IntPtr webView, IntPtr param, IntPtr request, IntPtr response);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnUrlRequestDidReceiveDataCallback(IntPtr webView, IntPtr param, IntPtr request, IntPtr data, int dataLength);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnUrlRequestDidFailCallback(IntPtr webView, IntPtr param, IntPtr request, IntPtr error);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnUrlRequestDidFinishLoadingCallback(IntPtr webView, IntPtr param, IntPtr request, double finishTime);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbNetJobDataRecvCallback(IntPtr ptr, IntPtr job, IntPtr data, int length);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbNetJobDataFinishCallback(IntPtr ptr, IntPtr job, mbLoadingResult result);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbPopupDialogSaveNameCallback(IntPtr ptr, IntPtr filePath);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate mbDownloadOpt mbDownloadInBlinkThreadCallback(IntPtr webView, IntPtr param, ulong expectedContentLength, IntPtr url, IntPtr mime, IntPtr disposition, IntPtr job, IntPtr dataBind);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbPrintPdfDataCallback(IntPtr webView, IntPtr param, IntPtr datas);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbPrintBitmapCallback(IntPtr webView, IntPtr param, IntPtr data, ulong size);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbWindowClosingCallback(IntPtr webView, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbWindowDestroyCallback(IntPtr webView, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbDraggableRegionsChangedCallback(IntPtr webView, IntPtr param, IntPtr rects, int rectCount);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbPrintingCallback(IntPtr webView, IntPtr param, mbPrintintStep step, IntPtr hDC, IntPtr settings, int pageCount);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate byte[] mbImageBufferToDataURLCallback(IntPtr webView, IntPtr param, IntPtr data, ulong size);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnScreenshotCallback(IntPtr webView, IntPtr param, IntPtr data, ulong size);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbOnCallUiThreadCallback(IntPtr webView, IntPtr paramOnInThread);

    // mbWebsocketHookCallbacks相关回调
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate IntPtr onWillConnect(IntPtr webView, IntPtr param, IntPtr channel, IntPtr url, IntPtr needHook);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int onConnected(IntPtr webView, IntPtr param, IntPtr channel);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate IntPtr onReceive(IntPtr webView, IntPtr param, IntPtr channel, int opCode, IntPtr buf, ulong len, IntPtr isContinue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate IntPtr onSend(IntPtr webView, IntPtr param, IntPtr channel, int opCode, IntPtr buf, ulong len, IntPtr isContinue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void onError(IntPtr webView, IntPtr param, IntPtr channel);

    // Cookie访问器
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int mbCookieVisitor(IntPtr param, IntPtr name, IntPtr value, IntPtr domain, IntPtr path, int secure, int httpOnly, IntPtr expires);

    // 调用UI线程，暂时没用
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void mbCallUiThread(IntPtr webView, mbOnCallUiThreadCallback func, IntPtr param);



    #endregion

    #region 封装mb.dll导出的C接口

    internal class MBVIP_API
    {
        /// <summary>
        /// 初始化，此句必须在所有API调用前最先调用。并且所有API必须和调用mbInit的线程为同个线程
        /// </summary>
        /// <param name="settings"> mask可以取：MB_SETTING_PROXY：效果和mbSetProxy一样。
        /// 通过proxy设置MB_SETTING_PAINTCALLBACK_IN_OTHER_THREAD：这是个高级用法，开启后onPaint回调会在另外个线程（其实就是渲染线程）。
        /// 这个是用来实现多线程上屏功能，性能更快。</param>
        [DllImport("mb.dll", EntryPoint = "mbInit", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbInit(ref mbSettings setting);

        /// <summary>
        /// 设置mb.dll路径，默认是exe的同目录，不修改的话不用调用此接口
        /// </summary>
        /// <param name="dllMbPath"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetMbDllPath", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetMbDllPath(IntPtr dllMbPath);

        /// <summary>
        /// 设置node.dll路径，默认是exe的同目录，不修改的话不用调用此接口
        /// </summary>
        /// <param name="dllNodePath"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetMbMainDllPath", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetMbMainDllPath(IntPtr dllNodePath);

        /// <summary>
        /// 反初始化
        /// </summary>
        [DllImport("mb.dll", EntryPoint = "mbUninit", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbUninit();

        /// <summary>
        /// 创建一个WebView，但不创建真窗口。一般用在离屏渲染里，如游戏
        /// </summary>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateWebView", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbCreateWebView();

        /// <summary>
        /// 销毁webview，释放资源
        /// </summary>
        /// <param name="webview"></param>
        [DllImport("mb.dll", EntryPoint = "mbDestroyWebView", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbDestroyWebView(IntPtr webview);

        /// <summary>
        /// 新建一个web窗口，只是在内存结构上新建，如果要显示到屏幕上，还需要配合窗口绘制函数或直接绑定到一个可见的控件上
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateWebWindow", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbCreateWebWindow(mbWindowType type, IntPtr parent, int x, int y, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="style"></param>
        /// <param name="styleEx"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateWebCustomWindow", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbCreateWebCustomWindow(IntPtr parent, ulong style, ulong styleEx, int x, int y, int width, int height);

        /// <summary>
        /// 设置窗口相对父窗口居中
        /// </summary>
        /// <param name="webview"></param>
        [DllImport("mb.dll", EntryPoint = "mbMoveToCenter", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbMoveToCenter(IntPtr webview);

        /// <summary>
        /// 创建字符串对象
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateString", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbCreateString(IntPtr str, long length);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateStringWithoutNullTermination", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbCreateStringWithoutNullTermination(IntPtr str, long length);

        /// <summary>
        /// 删除 mbCreateString 创建的对象
        /// </summary>
        /// <param name="str"></param>
        [DllImport("mb.dll", EntryPoint = "mbDeleteString", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbDeleteString(IntPtr str);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetStringLen", CallingConvention = CallingConvention.StdCall)]
        internal static extern long mbGetStringLen(IntPtr str);

        /// <summary>
        /// 设计初衷是用来转码，该接口会继续调用node.dll的wkeGetStringW接口，从ASCII转成UTF8字符指针，
        /// 但是由于vip版的字符串已经是utf8了，所以一般不再需要调用此接口。同时，由于VIP版本的部分接口是在非主线程进行的，
        /// 这种情况下调用mbGetString会报错，所以此接口在VIP版本中基本是没用的。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetString", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbGetString(IntPtr str);

        /// <summary>
        /// 设置页面代理，全局生效
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="proxy"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetProxy", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetProxy(IntPtr webView, ref mbProxy proxy);

        /// <summary>
        /// 开启一些实验性选项。
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="debugString">
        /// "showDevTools"	开启开发者工具，此时param要填写开发者工具的资源路径，如file:///c:/miniblink-release/front_end/inspector.html。注意param此时必须是utf8
        /// "wakeMinInterval" 设置帧率，默认值是10，值越大帧率越低
        /// "drawMinInterval" 设置帧率，默认值是3，值越大帧率越低
        /// "antiAlias" 设置抗锯齿渲染。param必须设置为"1"
        /// "minimumFontSize" 最小字体
        /// "minimumLogicalFontSize" 最小逻辑字体
        /// "defaultFontSize" 默认字体
        /// "defaultFixedFontSize" 默认fixed字体</param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDebugConfig", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern void mbSetDebugConfig(IntPtr webView, string debugString, string param);

        /// <summary>
        /// 设置hook后缓存的数据，网络层收到数据会存储在一buf内，接收数据完成后响应OnLoadUrlEnd事件，此调用严重影响性能，慎用。
        /// 此函数和mbNetHookRequest的区别是，mbNetHookRequest会在接受到真正网络数据后再调用回调，并允许回调修改网络数据，
        /// 而mbNetSetData是在网络数据还没发送的时候修改
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetSetData", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern void mbNetSetData(IntPtr jobPtr, [MarshalAs(UnmanagedType.LPArray)]byte[] buf, int len);

        /// <summary>
        /// RT，对网络请求下钩子。此接口需在mbLoadUrlBeginCallback里设置。如果设置了此钩子，则会缓存获取到的网络数据，
        /// 并在这次网络请求结束后调用mbOnLoadUrlEnd设置的回调，同时传递缓存的数据。在此期间，mb不会处理网络数据。
        /// 如果在mbLoadUrlBeginCallback里没设置mbNetHookRequest，则不会触发mbOnLoadUrlEnd回调。
        /// </summary>
        /// <param name="jobPtr"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetHookRequest", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetHookRequest(IntPtr jobPtr);

        /// <summary>
        /// 修改请求url
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <param name="url"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetChangeRequestUrl", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern void mbNetChangeRequestUrl(IntPtr jobPtr, string url);

        /// <summary>
        /// 继续执行后续的操作，通常配合其他异步操作的接口一起使用
        /// </summary>
        /// <param name="jobPtr"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetContinueJob", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetContinueJob(IntPtr jobPtr);

        /// <summary>
        /// 在mbOnLoadUrlBegin回调里调用，获取curl返回的原生请求头
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns>const mbSlist*，是一个C语言链表，详情看mb.h</returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetRawHttpHeadInBlinkThread", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbNetGetRawHttpHeadInBlinkThread(IntPtr jobPtr);

        /// <summary>
        /// 获取渲染线程响应头
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetRawResponseHeadInBlinkThread", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbNetGetRawResponseHeadInBlinkThread(IntPtr jobPtr);

        /// <summary>
        /// 高级用法。在mbOnLoadUrlBegin回调里调用。mbOnLoadUrlBegin里拦截到一个请求后，不能马上判断出结果。
        /// 此时可以调用本接口，然后在异步的某个时刻，调用mbNetContinueJob来让此请求继续进行
        /// </summary>
        /// <param name="jobPtr"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetHoldJobToAsynCommit", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetHoldJobToAsynCommit(IntPtr jobPtr);

        /// <summary>
        /// 取消网络请求
        /// </summary>
        /// <param name="jobPtr"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetCancelRequest", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetCancelRequest(IntPtr jobPtr);

        /// <summary>
        /// socket发生时将触发回调
        /// </summary>
        /// <param name="webview"></param>
        /// <param name="callbacks"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetSetWebsocketCallback", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetSetWebsocketCallback(IntPtr webview, ref mbWebsocketHookCallbacks callbacks, IntPtr param);

        /// <summary>
        /// 发送的文本
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetSendWsText", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetSendWsText(IntPtr channel, [MarshalAs(UnmanagedType.LPArray)]byte[] buf, long len);

        /// <summary>
        /// 发送的二进制数据
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetSendWsBlob", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetSendWsBlob(IntPtr channel, [MarshalAs(UnmanagedType.LPArray)]byte[] buf, long len);

        /// <summary>
        /// 获取此请求中的post数据。只有当请求是post时才有效果
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetPostBody", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbNetGetPostBody(IntPtr jobPtr);

        /// <summary>
        /// 这四个接口要结合起来使用。 当mbOnLoadUrlBegin里判断是post时，可以通过mbNetCreatePostBodyElements来创建一个新的post数据包。 
        /// 然后mbNetFreePostBodyElements来释放原post数据。
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetCreatePostBodyElements", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbNetCreatePostBodyElements(IntPtr webView, long length);

        /// <summary>
        /// 这四个接口要结合起来使用。 当mbOnLoadUrlBegin里判断是post时，可以通过mbNetCreatePostBodyElements来创建一个新的post数据包。 
        /// 然后mbNetFreePostBodyElements来释放原post数据。
        /// </summary>
        /// <param name="elements"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetFreePostBodyElements", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetFreePostBodyElements(IntPtr elements);

        /// <summary>
        /// 这四个接口要结合起来使用。 当mbOnLoadUrlBegin里判断是post时，可以通过mbNetCreatePostBodyElements来创建一个新的post数据包。 
        /// 然后mbNetFreePostBodyElements来释放原post数据。
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetCreatePostBodyElement", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbNetCreatePostBodyElement(IntPtr webView);

        /// <summary>
        /// 这四个接口要结合起来使用。 当mbOnLoadUrlBegin里判断是post时，可以通过mbNetCreatePostBodyElements来创建一个新的post数据包。 
        /// 然后mbNetFreePostBodyElements来释放原post数据。
        /// </summary>
        /// <param name="element"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetFreePostBodyElement", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetFreePostBodyElement(IntPtr element);

        /// <summary>
        /// 创建网络请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="mime"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetCreateWebUrlRequest", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbNetCreateWebUrlRequest(IntPtr url, IntPtr method, IntPtr mime);

        /// <summary>
        /// 向请求中添加http头字段
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetAddHTTPHeaderFieldToUrlRequest", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetAddHTTPHeaderFieldToUrlRequest(IntPtr request, IntPtr name, IntPtr value);

        /// <summary>
        /// 请求开始将触发回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="request"></param>
        /// <param name="param"></param>
        /// <param name="callbacks"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetStartUrlRequest", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbNetStartUrlRequest(IntPtr webView, IntPtr request, IntPtr param, ref mbUrlRequestCallbacks callbacks);

        /// <summary>
        /// 获取http响应状态码
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetHttpStatusCode", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbNetGetHttpStatusCode(IntPtr response);

        /// <summary>
        /// 获取此请求的method，如post还是get
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetRequestMethod", CallingConvention = CallingConvention.StdCall)]
        internal static extern mbRequestType mbNetGetRequestMethod(IntPtr jobPtr);

        /// <summary>
        /// 获取预期内容长度
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetExpectedContentLength", CallingConvention = CallingConvention.StdCall)]
        internal static extern long mbNetGetExpectedContentLength(IntPtr response);

        /// <summary>
        /// 获取响应地址
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetResponseUrl", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbNetGetResponseUrl(IntPtr response);

        /// <summary>
        /// 取消web请求
        /// </summary>
        /// <param name="requestId"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetCancelWebUrlRequest", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetCancelWebUrlRequest(int requestId);

        /// <summary>
        /// 在mbOnLoadUrlBegin回调里调用，表示设置http请求的MIME type
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <param name="type"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetSetMIMEType", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetSetMIMEType(IntPtr jobPtr, IntPtr type);

        /// <summary>
        /// 只能在blink线程调用（非主线程）
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetMIMEType", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbNetGetMIMEType(IntPtr jobPtr);

        /// <summary>
        /// 获取HTTP请求头字段
        /// </summary>
        /// <param name="job"></param>
        /// <param name="key"></param>
        /// <param name="fromRequestOrResponse"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetHTTPHeaderField", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern IntPtr mbNetGetHTTPHeaderField(IntPtr job, string key, int fromRequestOrResponse);

        /// <summary>
        /// 在mbOnLoadUrlBegin回调里调用，表示设置http请求（或者file:///协议）的 http header field。response一直要被设置成0
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="response"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetSetHTTPHeaderField", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbNetSetHTTPHeaderField(IntPtr jobPtr, IntPtr key, IntPtr value, int response);

        /// <summary>
        /// 是否允许使用鼠标
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetMouseEnabled", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetMouseEnabled(IntPtr webView, int b);

        /// <summary>
        /// 是否允许触屏
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetTouchEnabled", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetTouchEnabled(IntPtr webView, int b);

        /// <summary>
        /// 是否允许右键菜单
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetContextMenuEnabled", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetContextMenuEnabled(IntPtr webView, int b);

        /// <summary>
        /// 是否允许导航到新窗口
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetNavigationToNewWindowEnable", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetNavigationToNewWindowEnable(IntPtr webView, int b);

        /// <summary>
        /// 开启无头模式。开启后，将不会渲染页面，提升了网页性能。此功能方便用来实现一些爬虫或者**工具，提示：有些网页可能会判断网页是否真的显示，导致网页加载失败
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetHeadlessEnabled", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetHeadlessEnabled(IntPtr webView, int b);

        /// <summary>
        /// 是否可关闭拖拽到其他进程，True 禁止拖拽，false 启用拖拽
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDragDropEnable", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetDragDropEnable(IntPtr webView, int b);

        /// <summary>
        /// 是否允许拖动
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDragEnable", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetDragEnable(IntPtr webView, int b);

        /// <summary>
        /// 设置右键菜单显示
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="item"></param>
        /// <param name="isShow"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetContextMenuItemShow", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetContextMenuItemShow(IntPtr webView, mbMenuItemId item, int isShow);

        /// <summary>
        /// 设置mbWebView对应的窗口句柄。只有在无窗口模式下才能使用。如果是用mbCreateWebWindow创建的webview，已经自带窗口句柄了。
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="wnd"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetHandle", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetHandle(IntPtr webView, IntPtr wnd);

        /// <summary>
        /// 设置无窗口模式下的绘制偏移。在某些情况下（主要是离屏模式），绘制的地方不在真窗口的(0, 0)处，就需要手动调用此接口
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetHandleOffset", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetHandleOffset(IntPtr webView, int x, int y);

        /// <summary>
        /// 获取webveiw对应的窗口句柄。
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetHostHWND", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbGetHostHWND(IntPtr webView);

        /// <summary>
        /// 是否进行跨域检查，关闭后可以做任何跨域操作，如跨域ajax，跨域设置iframe
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCspCheckEnable", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetCspCheckEnable(IntPtr webView, int b);

        /// <summary>
        /// 启用或禁用 NPAPI插件，如启用会在当前目录增加plugins文件夹
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetNpapiPluginsEnabled", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetNpapiPluginsEnabled(IntPtr webView, int b);

        /// <summary>
        /// 开启内存缓存，网页的图片等都会在内存缓存里。默认关闭，关闭时内存使用会降低一些。但开启容易引起一些问题，慎用
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetMemoryCacheEnable", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetMemoryCacheEnable(IntPtr webView, int b);

        /// <summary>
        /// 设置页面cookie，cookie必须符合curl的cookie写法
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCookie", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetCookie(IntPtr webView, IntPtr url, IntPtr cookie);

        /// <summary>
        /// 开启或关闭cookie，这个接口只是影响blink，并不会设置curl。所以还是会生成curl的cookie文件
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="enable"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCookieEnabled", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetCookieEnabled(IntPtr webView, int enable);

        /// <summary>
        /// 设置cookie的本地文件目录，如果不存在则自动新建。默认是当前目录，cookies存在当前目录的“cookie.dat”里
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCookieJarPath", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetCookieJarPath(IntPtr webView, IntPtr path);

        /// <summary>
        /// 设置cookie的全路径+文件名，如果不存在则自动新建
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCookieJarFullPath", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetCookieJarFullPath(IntPtr webView, IntPtr path);

        /// <summary>
        /// 设置local storage的全路径，如果不存在则自动新建
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetLocalStorageFullPath", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetLocalStorageFullPath(IntPtr webView, IntPtr path);

        /// <summary>
        /// 获取页面标题
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetTitle", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbGetTitle(IntPtr webView);

        /// <summary>
        /// 获取页面URL
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetUrl", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbGetUrl(IntPtr webView);

        /// <summary>
        /// 添加插件目录
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbAddPluginDirectory", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbAddPluginDirectory(IntPtr webView, IntPtr path);

        /// <summary>
        /// 设置UA
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="userAgent"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetUserAgent", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetUserAgent(IntPtr webView, IntPtr userAgent);

        /// <summary>
        /// 设置页面缩放系数，默认是1
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="factor"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetZoomFactor", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetZoomFactor(IntPtr webView, float factor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetZoomFactor", CallingConvention = CallingConvention.StdCall)]
        internal static extern float mbGetZoomFactor(IntPtr webView);

        /// <summary>
        /// 开启或关闭硬盘本地缓存，这个接口是全局的。webView参数暂时没用
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="enable"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCacheEnabled", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetDiskCacheEnabled(IntPtr webView, int enable);

        /// <summary>
        /// 设置硬盘缓存路径
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCachePath", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetDiskCachePath(IntPtr webView, IntPtr path);

        /// <summary>
        /// 设置硬盘缓存大小
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="limit"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCacheLimit", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetDiskCacheLimit(IntPtr webView, long limit);

        /// <summary>
        /// 设置硬盘缓存磁盘
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="limit"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCacheLimitDisk", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetDiskCacheLimitDisk(IntPtr webView, long limit);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="Level"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCacheLevel", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetDiskCacheLevel(IntPtr webView, int Level);

        /// <summary>
        /// 设置资源自动清理时间间隔，默认是盟主说忘了
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="intervalSec"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetResourceGc", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetResourceGc(IntPtr webView, int intervalSec);

        /// <summary>
        /// 判断能否前进
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbCanGoForward", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbCanGoForward(IntPtr webView, mbCanGoBackForwardCallback callback, IntPtr param);

        /// <summary>
        /// 判断能否后退
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbCanGoBack", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbCanGoBack(IntPtr webView, mbCanGoBackForwardCallback callback, IntPtr param);

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbGetCookie", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbGetCookie(IntPtr webView, mbGetCookieCallback callback, IntPtr param);

        /// <summary>
        /// 获取渲染线程Cookie
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetCookieOnBlinkThread", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbGetCookieOnBlinkThread(IntPtr webView);

        /// <summary>
        /// 清理cookie。目前只支持清理所有页面的cookie。
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbClearCookie", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbClearCookie(IntPtr webView);


        /// <summary>
        /// 重新设置页面的宽高。如果webView是带窗口模式的，会设置真窗口的宽高。
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        [DllImport("mb.dll", EntryPoint = "mbResize", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbResize(IntPtr webView, int w, int h);

        /// <summary>
        /// 网页开始浏览将触发回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback">mbNavigationCallback回调的返回值，如果是true，表示可以继续进行浏览，false表示阻止本次浏览。</param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnNavigation", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnNavigation(IntPtr webView, mbNavigationCallback callback, IntPtr param);

        /// <summary>
        /// 网页异步加载时将触发回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnNavigationSync", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnNavigationSync(IntPtr webView, mbNavigationCallback callback, IntPtr param);

        /// <summary>
        /// 点击超链接创建新窗口时将触发此回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnCreateView", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnCreateView(IntPtr webView, mbCreateViewCallback callback, IntPtr param);

        /// <summary>
        /// 网页文档加载完成时会触发此回调，相比mbOnLoadingFinish，优先推荐此接口
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnDocumentReady", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnDocumentReady(IntPtr webView, mbDocumentReadyCallback callback, IntPtr param);

        /// <summary>
        /// 页面有任何部分需要重新绘制时（移动、缩放、窗口重叠部分被移开等），将触发此回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnPaintUpdated", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnPaintUpdated(IntPtr webView, mbPaintUpdatedCallback callback, IntPtr param);

        /// <summary>
        /// 任何网络请求发起前，会触发此回调，如果mbLoadUrlBeginCallback回调里返回true，表示取消该请求。
        /// 注意：与普通版不同，VIP版的此接口是在非UI线程执行，请自行处理需要同步的线程数据。
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnLoadUrlBegin", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnLoadUrlBegin(IntPtr webView, mbLoadUrlBeginCallback callback, IntPtr param);

        /// <summary>
        /// 网络请求结束时，如果在mbLoadUrlBeginCallback里设置mbNetHookRequest，则会触发此回调。
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnLoadUrlEnd", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnLoadUrlEnd(IntPtr webView, mbLoadUrlEndCallback callback, IntPtr param);

        /// <summary>
        /// 网页标题改变时会触发此回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnTitleChanged", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnTitleChanged(IntPtr webView, mbTitleChangedCallback callback, IntPtr param);

        /// <summary>
        /// 网页URL改变时会触发此回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnURLChanged", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnURLChanged(IntPtr webView, mbUrlChangedCallback callback, IntPtr param);

        /// <summary>
        /// 网页URL改变时会触发此回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnURLChanged", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnURLChanged2(IntPtr webView, mbUrlChangedCallback2 callback, IntPtr param);

        /// <summary>
        /// 网页加载完成时会触发此回调，如果URL的主域名（如百度搜索后跳转搜索结果页面时）未改变的，则不会触发
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnLoadingFinish", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnLoadingFinish(IntPtr webView, mbLoadingFinishCallback callback, IntPtr param);

        /// <summary>
        /// 页面下载事件回调。点击某些链接，触发下载会调用
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnDownload", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnDownload(IntPtr webView, mbDownloadCallback callback, IntPtr param);

        /// <summary>
        /// 页面下载事件回调，但回调提供的参数更多。另外回调是在非UI线程。此外可在回调中调用mbPopupDialogAndDownload，这样能弹出文件另存框，并自动下载
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnDownloadInBlinkThread", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnDownloadInBlinkThread(IntPtr webView, mbDownloadInBlinkThreadCallback callback, IntPtr param);

        /// <summary>
        /// 网页调用alert会触发此回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnAlertBox", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnAlertBox(IntPtr webView, mbAlertBoxCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnConfirmBox", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnConfirmBox(IntPtr webView, mbConfirmBoxCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnPromptBox", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnPromptBox(IntPtr webView, mbPromptBoxCallback callback, IntPtr param);

        /// <summary>
        /// 获取网站logo，此接口必须在mbOnLoadingFinish回调里调用
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnNetGetFavicon", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnNetGetFavicon(IntPtr webView, mbNetGetFaviconCallback callback, IntPtr param);

        /// <summary>
        /// 网页调用console触发此回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnConsole", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnConsole(IntPtr webView, mbConsoleCallback callback, IntPtr param);

        /// <summary>
        /// mbWebView如果是真窗口模式，则在收到WM_CLODE消息时触发此回调。可以通过在回调中返回false拒绝关闭窗口
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbOnClose", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbOnClose(IntPtr webView, mbCloseCallback callback, IntPtr param);

        /// <summary>
        /// 窗口即将被销毁时触发回调。不像mbOnClose，这个操作无法取消
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbOnDestroy", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbOnDestroy(IntPtr webView, mbDestroyCallback callback, IntPtr param);

        /// <summary>
        /// 打印时会触发此回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbOnPrinting", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbOnPrinting(IntPtr webView, mbPrintingCallback callback, IntPtr param);

        /// <summary>
        /// javascript的v8执行环境被创建时触发此回调，每个frame创建时都会触发此回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnDidCreateScriptContext", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnDidCreateScriptContext(IntPtr webView, mbDidCreateScriptContextCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnPluginList", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnPluginList(IntPtr webView, mbGetPluginListCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnImageBufferToDataURL", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnImageBufferToDataURL(IntPtr webView, mbImageBufferToDataURLCallback callback, IntPtr param);

        /// <summary>
        /// 强制让页面后退（如果可以后退的话）
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbGoBack", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbGoBack(IntPtr webView);

        /// <summary>
        /// 强制让页面前进（如果可以前进的话）
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbGoForward", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbGoForward(IntPtr webView);

        /// <summary>
        /// 停止加载页面
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbStopLoading", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbStopLoading(IntPtr webView);

        /// <summary>
        /// 重新加载页面
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbReload", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbReload(IntPtr webView);

        /// <summary>
        /// 通过设置mb内置的curl来操作cookie。
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="command">mbCookieCommandClearAllCookies: 内部实际执行了curl_easy_setopt(curl, CURLOPT_COOKIELIST, "ALL");
        /// mbCookieCommandClearSessionCookies: curl_easy_setopt(curl, CURLOPT_COOKIELIST, "SESS");
        /// mbCookieCommandFlushCookiesToFile: curl_easy_setopt(curl, CURLOPT_COOKIELIST, "FLUSH");
        /// mbCookieCommandReloadCookiesFromFile: curl_easy_setopt(curl, CURLOPT_COOKIELIST, "RELOAD");</param>
        [DllImport("mb.dll", EntryPoint = "mbPerformCookieCommand", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbPerformCookieCommand(IntPtr webView, mbCookieCommand command);

        /// <summary>
        /// 给webview发送全选命令
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorSelectAll", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbEditorSelectAll(IntPtr webView);

        /// <summary>
        /// 拷贝页面里被选中的字符串
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorCopy", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbEditorCopy(IntPtr webView);

        /// <summary>
        /// 剪切页面里被选中的字符串
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorCut", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbEditorCut(IntPtr webView);

        /// <summary>
        /// 给webview发送粘贴命令
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorPaste", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbEditorPaste(IntPtr webView);

        /// <summary>
        /// 给webview发送删除命令
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorDelete", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbEditorDelete(IntPtr webView);

        /// <summary>
        /// 给webview发送撤销命令
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorUndo", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbEditorUndo(IntPtr webView);

        /// <summary>
        /// 给webview发送鼠标事件
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="message"></param>
        /// <param name="x">相对窗口的X坐标</param>
        /// <param name="y">相对窗口的Y坐标</param>
        /// <param name="flags">可取值有MB_CONTROL、MB_SHIFT、MB_LBUTTON、MB_MBUTTON、MB_RBUTTON，可通过“或”操作并联。</param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireMouseEvent", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbFireMouseEvent(IntPtr webView, uint message, int x, int y, uint flags);

        /// <summary>
        /// 给webview发送菜单事件
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireContextMenuEvent", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbFireContextMenuEvent(IntPtr webView, int x, int y, uint flags);

        /// <summary>
        /// 给webview发送鼠标滚动事件
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="delta"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireMouseWheelEvent", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbFireMouseWheelEvent(IntPtr webView, int x, int y, int delta, uint flags);

        /// <summary>
        /// 给webview发送按键按下事件
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="virtualKeyCode"></param>
        /// <param name="flags"></param>
        /// <param name="systemKey"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireKeyUpEvent", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbFireKeyUpEvent(IntPtr webView, uint virtualKeyCode, uint flags, int systemKey);

        /// <summary>
        /// 给webview发送按键抬起事件
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="virtualKeyCode"></param>
        /// <param name="flags"></param>
        /// <param name="systemKey"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireKeyDownEvent", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbFireKeyDownEvent(IntPtr webView, uint virtualKeyCode, uint flags, int systemKey);

        /// <summary>
        /// 给webview发送按键敲击事件
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="charCode"></param>
        /// <param name="flags"></param>
        /// <param name="systemKey"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireKeyPressEvent", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbFireKeyPressEvent(IntPtr webView, uint charCode, uint flags, int systemKey);

        /// <summary>
        /// 向mb发送任意windows消息。不过目前mb主要用来处理光标相关。mb在无窗口模式下，要响应光标事件，需要通过本函数手动发送光标消息
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="IntPtr"></param>
        /// <param name="message"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireWindowsMessage", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern int mbFireWindowsMessage(IntPtr webView, IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam, ref string result);

        /// <summary>
        /// 设置webview是焦点态。如果webveiw关联了窗口，窗口也会有焦点
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetFocus", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetFocus(IntPtr webView);

        /// <summary>
        /// 取消webview是焦点态
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbKillFocus", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbKillFocus(IntPtr webView);

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="webview"></param>
        /// <param name="show"></param>
        [DllImport("mb.dll", EntryPoint = "mbShowWindow", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbShowWindow(IntPtr webview, int show);

        /// <summary>
        /// 加载URL
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="url"></param>
        [DllImport("mb.dll", EntryPoint = "mbLoadURL", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbLoadURL(IntPtr webView, IntPtr url);

        /// <summary>
        /// 加载一段html，但可以指定baseURL，也就是相对于那个目录的url
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="html"></param>
        /// <param name="baseUrl"></param>
        [DllImport("mb.dll", EntryPoint = "mbLoadHtmlWithBaseUrl", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbLoadHtmlWithBaseUrl(IntPtr webView, IntPtr html, IntPtr baseUrl);

        /// <summary>
        /// post数据
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="postLen"></param>
        [DllImport("mb.dll", EntryPoint = "mbPostURL", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbPostURL(IntPtr webView, IntPtr url, [MarshalAs(UnmanagedType.LPArray)]byte[] postData, int postLen);

        /// <summary>
        /// 锁定窗口DC
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetLockedViewDC", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbGetLockedViewDC(IntPtr webView);

        /// <summary>
        /// 解锁窗口DC
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbUnlockViewDC", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbUnlockViewDC(IntPtr webView);

        /// <summary>
        /// 强制唤醒MB，好像没啥用
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbWake", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbWake(IntPtr webView);

        /// <summary>
        /// js返回值转化为double类型
        /// </summary>
        /// <param name="es"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbJsToDouble", CallingConvention = CallingConvention.StdCall)]
        internal static extern double mbJsToDouble(IntPtr es, ulong v);

        /// <summary>
        /// js返回值转化为bool类型
        /// </summary>
        /// <param name="es"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbJsToBoolean", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbJsToBoolean(IntPtr es, ulong v);

        /// <summary>
        /// js返回值转化为string类型
        /// </summary>
        /// <param name="es"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbJsToString", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbJsToString(IntPtr es, ulong v);

        /// <summary>
        /// 注册js通知native的回调。
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnJsQuery", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbOnJsQuery(IntPtr webView, mbJsQueryCallback callback, IntPtr param);

        /// <summary>
        /// 网络响应查询
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="queryId"></param>
        /// <param name="customMsg"></param>
        /// <param name="response"></param>
        [DllImport("mb.dll", EntryPoint = "mbResponseQuery", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbResponseQuery(IntPtr webView, long queryId, int customMsg, IntPtr response);

        /// <summary>
        /// 运行一段js，返回js的值mbValue在callback中获取。mbValue是个封装了内部v8各种类型的类
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <param name="script"></param>
        /// <param name="isInClosure"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <param name="unuse"></param>
        [DllImport("mb.dll", EntryPoint = "mbRunJs", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbRunJs(IntPtr webView, IntPtr frameId, IntPtr script, int isInClosure, mbRunJsCallback callback, IntPtr param, IntPtr unuse);

        /// <summary>
        /// 异步运行js
        /// </summary>
        /// <param name="webView">窗体</param>
        /// <param name="frameId">框架ID</param>
        /// <param name="script">待执行脚本</param>
        /// <param name="isInClosure">参数：isInClosure表示是否在外层包个function() {}形式的闭包。
        /// 注意：如果需要返回值，在isInClosure为true时，需要写return，为false则不用</param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbRunJsSync", CallingConvention = CallingConvention.StdCall)]
        internal static extern ulong mbRunJsSync(IntPtr webView, IntPtr frameId, IntPtr script, int isInClosure);

        /// <summary>
        /// 获取主frame的句柄
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbWebFrameGetMainFrame", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbWebFrameGetMainFrame(IntPtr webView);

        /// <summary>
        /// 判断frameId是否是主frame
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbIsMainFrame", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbIsMainFrame(IntPtr webView, IntPtr frameId);

        /// <summary>
        /// 是否允许nodejs
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetNodeJsEnable", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetNodeJsEnable(IntPtr webView, int b);

        /// <summary>
        /// 设置硬件参数，可以用于模拟手机环境等
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="device">设备的字符串。可取值有：
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
        /// <param name="paramStr"></param>
        /// <param name="paramInt"></param>
        /// <param name="paramFloat"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDeviceParameter", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbSetDeviceParameter(IntPtr webView, IntPtr device, IntPtr paramStr, int paramInt, float paramFloat);

        /// <summary>
        /// 从标记获取内容
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="calback"></param>
        /// <param name="param"></param>
        /// <param name="frameId"></param>
        [DllImport("mb.dll", EntryPoint = "mbGetContentAsMarkup", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbGetContentAsMarkup(IntPtr webView, mbGetContentAsMarkupCallback calback, IntPtr param, IntPtr frameId);

        /// <summary>
        /// 获取网页源码，可以在回调函数中干点你想干的事
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="calback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbGetSource", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbGetSource(IntPtr webView, mbGetSourceCallback calback, IntPtr param);

        /// <summary>
        /// 页面序列化成MHTML时，触发回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="calback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbUtilSerializeToMHTML", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbUtilSerializeToMHTML(IntPtr webView, mbGetSourceCallback calback, IntPtr param);

        /// <summary>
        /// 创建请求码
        /// </summary>
        /// <param name="registerInfo"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilCreateRequestCode", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern string mbUtilCreateRequestCode(string registerInfo);

        /// <summary>
        /// 是否注册
        /// </summary>
        /// <param name="defaultPath"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilIsRegistered", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbUtilIsRegistered(IntPtr defaultPath);

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <param name="printParams"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilPrint", CallingConvention = CallingConvention.StdCall)]
        internal static extern int mbUtilPrint(IntPtr webView, IntPtr frameId, ref mbPrintSettings printParams);

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilBase64Encode", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbUtilBase64Encode(IntPtr str);

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilBase64Decode", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbUtilBase64Decode(IntPtr str);

        /// <summary>
        /// url编码转回
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilDecodeURLEscape", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbUtilDecodeURLEscape(IntPtr url);

        /// <summary>
        /// url编码转义
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilEncodeURLEscape", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbUtilEncodeURLEscape(IntPtr url);

        /// <summary>
        /// 创建V8引擎执行后的内存快照
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilCreateV8Snapshot", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbUtilCreateV8Snapshot(IntPtr str);

        /// <summary>
        /// 将页面转换成PDF，可以设置回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <param name="settings"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbUtilPrintToPdf", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbUtilPrintToPdf(IntPtr webView, IntPtr frameId, ref mbPrintSettings settings, mbPrintPdfDataCallback callback, IntPtr param);

        /// <summary>
        /// 打印位图时触发回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <param name="settings"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbUtilPrintToBitmap", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbUtilPrintToBitmap(IntPtr webView, IntPtr frameId, ref mbScreenshotSettings settings, mbPrintBitmapCallback callback, IntPtr param);

        /// <summary>
        /// 截图时触发回调
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="settings"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbUtilScreenshot", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbUtilScreenshot(IntPtr webView, ref mbScreenshotSettings settings, mbOnScreenshotCallback callback, IntPtr param);

        /// <summary>
        /// 弹出下载管理器
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="url"></param>
        /// <param name="downloadJob"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbPopupDownloadMgr", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern int mbPopupDownloadMgr(IntPtr webView, string url, IntPtr downloadJob);

        /// <summary>
        /// 弹出下载对话框的方式下载
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="param"></param>
        /// <param name="contentLength"></param>
        /// <param name="url"></param>
        /// <param name="mime"></param>
        /// <param name="disposition"></param>
        /// <param name="job"></param>
        /// <param name="dataBind"></param>
        /// <param name="callbackBind"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbPopupDialogAndDownload", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern mbDownloadOpt mbPopupDialogAndDownload(IntPtr webView, IntPtr param, long contentLength, 
            string url, string mime, string disposition, IntPtr job, ref mbNetJobDataBind dataBind, ref mbDownloadBind callbackBind);

        /// <summary>
        /// 下载到指定目录
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="param"></param>
        /// <param name="path"></param>
        /// <param name="contentLength"></param>
        /// <param name="url"></param>
        /// <param name="mime"></param>
        /// <param name="disposition"></param>
        /// <param name="job"></param>
        /// <param name="dataBind"></param>
        /// <param name="callbackBind"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbDownloadByPath", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern mbDownloadOpt mbDownloadByPath(IntPtr webView, IntPtr param, IntPtr path, long contentLength,
            string url, string mime, string disposition, IntPtr job, ref mbNetJobDataBind dataBind, ref mbDownloadBind callbackBind);

        /// <summary>
        /// 获取pdf页面数据，可以通过回调干点你想干的事情
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbGetPdfPageData", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbGetPdfPageData(IntPtr webView, mbOnGetPdfPageDataCallback callback, IntPtr param);

        /// <summary>
        /// 创建内存缓存
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="buf"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateMemBuf", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr mbCreateMemBuf(IntPtr webView, IntPtr buf, long length);

        /// <summary>
        /// 释放内存缓存
        /// </summary>
        /// <param name="buf"></param>
        [DllImport("mb.dll", EntryPoint = "mbFreeMemBuf", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbFreeMemBuf(ref mbMemBuf buf);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="fileName"></param>
        [DllImport("mb.dll", EntryPoint = "mbPluginListBuilderAddPlugin", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbPluginListBuilderAddPlugin(IntPtr builder, IntPtr name, IntPtr description, IntPtr fileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        [DllImport("mb.dll", EntryPoint = "mbPluginListBuilderAddMediaTypeToLastPlugin", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbPluginListBuilderAddMediaTypeToLastPlugin(IntPtr builder, IntPtr name, IntPtr description);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="fileExtension"></param>
        [DllImport("mb.dll", EntryPoint = "mbPluginListBuilderAddFileExtensionToLastMediaType", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbPluginListBuilderAddFileExtensionToLastMediaType(IntPtr builder, IntPtr fileExtension);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("mb.dll", EntryPoint = "mbEnableHighDPISupport", CallingConvention = CallingConvention.StdCall)]
        internal static extern void mbEnableHighDPISupport();
    }

    #endregion
}
