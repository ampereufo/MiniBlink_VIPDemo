using System;
using System.Runtime.InteropServices;


/// <summary>
/// 挫盟主只写代码不写文档，这个是根据mb.h头文件整理的，有错误找他，别找我
/// 封装以2019-10-28的mb.h文件为基准，后续修改尽量同步
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
    /// 子网掩码设置项
    /// </summary>
    public enum mbSettingMask
    {
        MB_SETTING_PROXY = 1,
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
        public mbOnBlinkThreadInitCallback blinkThreadInitCallback;
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
        public onWillConnect WillConnect;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public onConnected Connected;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public onReceive Receive;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public onSend Send;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public onError Error;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbUrlRequestCallbacks
    {
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbOnUrlRequestWillRedirectCallback willRedirectCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbOnUrlRequestDidReceiveResponseCallback didReceiveResponseCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbOnUrlRequestDidReceiveDataCallback didReceiveDataCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbOnUrlRequestDidFailCallback didFailCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbOnUrlRequestDidFinishLoadingCallback didFinishLoadingCallback;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbNetJobDataBind
    {
        public IntPtr param;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbNetJobDataRecvCallback recvCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbNetJobDataFinishCallback finishCallback;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct mbDownloadBind
    {
        public IntPtr param;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbNetJobDataRecvCallback recvCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbNetJobDataFinishCallback finishCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public mbPopupDialogSaveNameCallback saveNameCallback;
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

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate IntPtr OnWindowProcEventHandler(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate IntPtr WndProcCallback(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbPaintUpdatedCallback(IntPtr webView, IntPtr param, IntPtr hdc, int x, int y, int cx, int cy);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbPaintBitUpdatedCallback(IntPtr webView, IntPtr param, IntPtr buffer, IntPtr r, int width, int height);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbOnBlinkThreadInitCallback(IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate int mbCookieVisitor(IntPtr param, string name, string value, string domain, string path, int secure, int httpOnly, IntPtr expires);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate IntPtr onWillConnect(IntPtr webView, IntPtr param, IntPtr channel, string url, IntPtr needHook);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int onConnected(IntPtr webView, IntPtr param, IntPtr channel);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate IntPtr onReceive(IntPtr webView, IntPtr param, IntPtr channel, int opCode, string buf, ulong len, IntPtr isContinue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate IntPtr onSend(IntPtr webView, IntPtr param, IntPtr channel, int opCode, string buf, ulong len, IntPtr isContinue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void onError(IntPtr webView, IntPtr param, IntPtr channel);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbOnGetPdfPageDataCallback(IntPtr webView, IntPtr param, IntPtr data, ulong size);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbRunJsCallback(IntPtr webView, IntPtr param, IntPtr es, long v);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbJsQueryCallback(IntPtr webView, IntPtr param, IntPtr es, long queryId, int customMsg, string request);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    public delegate void mbTitleChangedCallback(IntPtr webView, IntPtr param, IntPtr title);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbMouseOverUrlChangedCallback(IntPtr webView, IntPtr param, IntPtr url);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbUrlChangedCallback(IntPtr webView, IntPtr param, IntPtr url, int canGoBack, int canGoForward);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbUrlChangedCallback2(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbAlertBoxCallback(IntPtr webView, IntPtr param, IntPtr msg);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate int mbConfirmBoxCallback(IntPtr webView, IntPtr param, IntPtr msg);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate IntPtr mbPromptBoxCallback(IntPtr webView, IntPtr param, IntPtr msg, IntPtr defaultResult);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate int mbNavigationCallback(IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate IntPtr mbCreateViewCallback(IntPtr webView, IntPtr param, mbNavigationType navigationType, IntPtr url, IntPtr windowFeatures);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbDocumentReadyCallback(IntPtr webView, IntPtr param, IntPtr frameId);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int mbCloseCallback(IntPtr webView, IntPtr param, IntPtr unuse);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int mbDestroyCallback(IntPtr webView, IntPtr param, IntPtr unuse);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbOnShowDevtoolsCallback(IntPtr webView, IntPtr param);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbDidCreateScriptContextCallback(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr context, int extensionGroup, int worldId);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int mbGetPluginListCallback(int refresh, IntPtr pluginListBuilder, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbLoadingFinishCallback(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, mbLoadingResult result, IntPtr failedReason);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate int mbDownloadCallback(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr url, IntPtr downloadJob);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbConsoleCallback(IntPtr webView, IntPtr param, mbConsoleLevel level, string message, string sourceName, uint sourceLine, string stackTrace);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbOnCallUiThread(IntPtr webView, IntPtr paramOnInThread);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbCallUiThread(IntPtr webView, mbOnCallUiThread func, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate int mbLoadUrlBeginCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr job);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbLoadUrlEndCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr job, IntPtr buf, int len);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbWillReleaseScriptContextCallback(IntPtr webView, IntPtr param, IntPtr frameId, IntPtr context, int worldId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate int mbNetResponseCallback(IntPtr webView, IntPtr param, string url, IntPtr job);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbNetGetFaviconCallback(IntPtr webView, IntPtr param, string url, IntPtr buf);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbCanGoBackForwardCallback(IntPtr webView, IntPtr param, MbAsynRequestState state, int b);
        
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public delegate void mbGetCookieCallback(IntPtr webView, IntPtr param, MbAsynRequestState state, string cookie);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbGetSourceCallback(IntPtr webView, IntPtr param, string mhtml);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbGetContentAsMarkupCallback(IntPtr webView, IntPtr param, string content, ulong size);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbOnUrlRequestWillRedirectCallback(IntPtr webView, IntPtr param, IntPtr oldRequest, IntPtr request, IntPtr redirectResponse);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbOnUrlRequestDidReceiveResponseCallback(IntPtr webView, IntPtr param, IntPtr request, IntPtr response);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbOnUrlRequestDidReceiveDataCallback(IntPtr webView, IntPtr param, IntPtr request, string data, int dataLength);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbOnUrlRequestDidFailCallback(IntPtr webView, IntPtr param, IntPtr request, string error);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbOnUrlRequestDidFinishLoadingCallback(IntPtr webView, IntPtr param, IntPtr request, double finishTime);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbNetJobDataRecvCallback(IntPtr ptr, IntPtr job, string data, int length);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbNetJobDataFinishCallback(IntPtr ptr, IntPtr job, mbLoadingResult result);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    public delegate void mbPopupDialogSaveNameCallback(IntPtr ptr, string filePath);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate mbDownloadOpt mbDownloadInBlinkThreadCallback(IntPtr webView, IntPtr param, ulong expectedContentLength, string url, string mime, string disposition, IntPtr job, IntPtr dataBind);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbPrintPdfDataCallback(IntPtr webview, IntPtr param, IntPtr datas);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbPrintBitmapCallback(IntPtr webview, IntPtr param, string data, ulong size);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void mbOnScreenshot(IntPtr webView, IntPtr param, string data, ulong size);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int mbWindowClosingCallback(IntPtr webview, IntPtr param);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbWindowDestroyCallback(IntPtr webview, IntPtr param);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void mbDraggableRegionsChangedCallback(IntPtr webview, IntPtr param, IntPtr rects, int rectCount);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int mbPrintingCallback(IntPtr webview, IntPtr param, mbPrintintStep step, IntPtr hDC, IntPtr settings, int pageCount);
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate IntPtr mbImageBufferToDataURLCallback(IntPtr webView, IntPtr param, string data, ulong size);

    #endregion

    #region 封装mb.dll导出的C接口

    public class MBVIP_API
    {
        /// <summary>
        /// 初始化，因为VIP版本是多线程渲染，所以要传入一个mbSettings结构体
        /// </summary>
        [DllImport("mb.dll", EntryPoint = "mbInit", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbInit(ref mbSettings settings);

        /// <summary>
        /// 设置mb.dll路径，默认是exe的同目录，不修改的话不用调用此接口
        /// </summary>
        /// <param name="dllMbPath"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetMbDllPath", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void mbSetMbDllPath(string dllMbPath);

        /// <summary>
        /// 设置node.dll路径，默认是exe的同目录，不修改的话不用调用此接口
        /// </summary>
        /// <param name="dllNodePath"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetMbMainDllPath", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void mbSetMbMainDllPath(string dllNodePath);

        /// <summary>
        /// 设置key路径，默认是exe的同目录，不修改的话不用调用此接口（盟主没有，我先替他写上）
        /// </summary>
        /// <param name="keyPath"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetKeyPath", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void mbSetKeyPath(string keyPath);

        /// <summary>
        /// 反初始化
        /// </summary>
        [DllImport("mb.dll", EntryPoint = "mbUninit", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbUninit();

        /// <summary>
        /// 创建一个WebView，但不创建真窗口。一般用在离屏渲染里，如游戏
        /// </summary>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateWebView", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbCreateWebView();

        /// <summary>
        /// 销毁webview，释放资源
        /// </summary>
        /// <param name="webview"></param>
        [DllImport("mb.dll", EntryPoint = "mbDestroyWebView", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbDestroyWebView(IntPtr webview);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateWebWindow", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbCreateWebWindow(mbWindowType type, IntPtr parent, int x, int y, int width, int height);

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
        public static extern IntPtr mbCreateWebCustomWindow(IntPtr parent, ulong style, ulong styleEx, int x, int y, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webview"></param>
        [DllImport("mb.dll", EntryPoint = "mbMoveToCenter", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbMoveToCenter(IntPtr webview);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateString", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr mbCreateString(string str, long length);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateStringWithoutNullTermination", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr mbCreateStringWithoutNullTermination(string str, long length);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        [DllImport("mb.dll", EntryPoint = "mbDeleteString", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbDeleteString(IntPtr str);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetStringLen", CallingConvention = CallingConvention.StdCall)]
        public static extern long mbGetStringLen(IntPtr str); 

        /// <summary>
        /// 视参数不同获取相关的字符串，标题，url等
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetString", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr mbGetString(IntPtr str);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="proxy"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetProxy", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetProxy(IntPtr webView, ref mbProxy proxy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="debugString"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDebugConfig", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbSetDebugConfig(IntPtr webView, string debugString, string param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        [DllImport("mb.dll", EntryPoint = "IntPtr", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbNetSetData(IntPtr jobPtr, [MarshalAs(UnmanagedType.LPArray)]byte[] buf, int len);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetHookRequest", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbNetHookRequest(IntPtr jobPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <param name="url"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetChangeRequestUrl", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbNetChangeRequestUrl(IntPtr jobPtr, string url);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetContinueJob", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbNetContinueJob(IntPtr jobPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetRawHttpHeadInBlinkThread", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbNetGetRawHttpHeadInBlinkThread(IntPtr jobPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetRawResponseHeadInBlinkThread", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbNetGetRawResponseHeadInBlinkThread(IntPtr jobPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetHoldJobToAsynCommit", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbNetHoldJobToAsynCommit(IntPtr jobPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetCancelRequest", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbNetCancelRequest(IntPtr jobPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webview"></param>
        /// <param name="callbacks"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetSetWebsocketCallback", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbNetSetWebsocketCallback(IntPtr webview, ref mbWebsocketHookCallbacks callbacks, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetPostBody", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbNetGetPostBody(IntPtr jobPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetCreatePostBodyElements", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbNetCreatePostBodyElements(IntPtr webView, long length);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetFreePostBodyElements", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbNetFreePostBodyElements(ref mbPostBodyElements elements);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetCreatePostBodyElement", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbNetCreatePostBodyElement(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetFreePostBodyElement", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbNetFreePostBodyElement(ref mbPostBodyElement element);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="mime"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetCreateWebUrlRequest", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr mbNetCreateWebUrlRequest(string url, string method, string mime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetAddHTTPHeaderFieldToUrlRequest", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbNetAddHTTPHeaderFieldToUrlRequest(IntPtr request, string name, string value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="request"></param>
        /// <param name="param"></param>
        /// <param name="callbacks"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetStartUrlRequest", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbNetStartUrlRequest(IntPtr webView, IntPtr request, [MarshalAs(UnmanagedType.LPArray)]byte[] param, ref mbUrlRequestCallbacks callbacks);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetHttpStatusCode", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbNetGetHttpStatusCode(IntPtr response);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetRequestMethod", CallingConvention = CallingConvention.StdCall)]
        public static extern mbRequestType mbNetGetRequestMethod(IntPtr jobPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetExpectedContentLength", CallingConvention = CallingConvention.StdCall)]
        public static extern long mbNetGetExpectedContentLength(IntPtr response);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetResponseUrl", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbNetGetResponseUrl(IntPtr response);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetCancelWebUrlRequest", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbNetCancelWebUrlRequest(int requestId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <param name="type"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetSetMIMEType", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbNetSetMIMEType(IntPtr jobPtr, string type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetMIMEType", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbNetGetMIMEType(IntPtr jobPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="key"></param>
        /// <param name="fromRequestOrResponse"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbNetGetHTTPHeaderField", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbNetGetHTTPHeaderField(IntPtr job, string key, int fromRequestOrResponse);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobPtr"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="response"></param>
        [DllImport("mb.dll", EntryPoint = "mbNetSetHTTPHeaderField", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void mbNetSetHTTPHeaderField(IntPtr jobPtr, string key, string value, int response);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetMouseEnabled", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetMouseEnabled(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetTouchEnabled", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetTouchEnabled(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetContextMenuEnabled", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetContextMenuEnabled(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetNavigationToNewWindowEnable", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetNavigationToNewWindowEnable(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetHeadlessEnabled", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetHeadlessEnabled(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDragDropEnable", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetDragDropEnable(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDragEnable", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetDragEnable(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="item"></param>
        /// <param name="isShow"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetContextMenuItemShow", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetContextMenuItemShow(IntPtr webView, mbMenuItemId item, int isShow);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="wnd"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetHandle", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetHandle(IntPtr webView, IntPtr wnd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetHandleOffset", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetHandleOffset(IntPtr webView, int x, int y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetHostIntPtr", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbGetHostIntPtr(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCspCheckEnable", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetCspCheckEnable(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetNpapiPluginsEnabled", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetNpapiPluginsEnabled(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetMemoryCacheEnable", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetMemoryCacheEnable(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCookie", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbSetCookie(IntPtr webView, string url, string cookie);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="enable"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCookieEnabled", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetCookieEnabled(IntPtr webView, int enable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCookieJarPath", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void mbSetCookieJarPath(IntPtr webView, string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetCookieJarFullPath", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void mbSetCookieJarFullPath(IntPtr webView, string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetLocalStorageFullPath", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void mbSetLocalStorageFullPath(IntPtr webView, string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetTitle", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbGetTitle(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetUrl", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbGetUrl(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbAddPluginDirectory", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void mbAddPluginDirectory(IntPtr webView, string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="userAgent"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetUserAgent", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbSetUserAgent(IntPtr webView, string userAgent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="factor"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetZoomFactor", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetZoomFactor(IntPtr webView, float factor);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetZoomFactor", CallingConvention = CallingConvention.StdCall)]
        public static extern float mbGetZoomFactor(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="enable"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCacheEnabled", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetDiskCacheEnabled(IntPtr webView, int enable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="path"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCachePath", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void mbSetDiskCachePath(IntPtr webView, string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="limit"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCacheLimit", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetDiskCacheLimit(IntPtr webView, long limit);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="limit"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCacheLimitDisk", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetDiskCacheLimitDisk(IntPtr webView, long limit);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="Level"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDiskCacheLevel", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetDiskCacheLevel(IntPtr webView, int Level);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="intervalSec"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetResourceGc", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetResourceGc(IntPtr webView, int intervalSec);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbCanGoForward", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbCanGoForward(IntPtr webView, mbCanGoBackForwardCallback callback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbCanGoBack", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbCanGoBack(IntPtr webView, mbCanGoBackForwardCallback callback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// RT
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbGetCookie", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbGetCookie(IntPtr webView, mbGetCookieCallback callback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetCookieOnBlinkThread", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbGetCookieOnBlinkThread(IntPtr webView);

        /// <summary>
        /// RT
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbClearCookie", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbClearCookie(IntPtr webView);


        /// <summary>
        /// 重新设置页面的宽高
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        [DllImport("mb.dll", EntryPoint = "mbResize", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbResize(IntPtr webView, int w, int h);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnNavigation", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnNavigation(IntPtr webView, mbNavigationCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnNavigationSync", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnNavigationSync(IntPtr webView, mbNavigationCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnCreateView", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnCreateView(IntPtr webView, mbCreateViewCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnDocumentReady", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnDocumentReady(IntPtr webView, mbDocumentReadyCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnPaintUpdated", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnPaintUpdated(IntPtr webView, mbPaintUpdatedCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnLoadUrlBegin", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnLoadUrlBegin(IntPtr webView, mbLoadUrlBeginCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnLoadUrlEnd", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnLoadUrlEnd(IntPtr webView, mbLoadUrlEndCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnTitleChanged", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnTitleChanged(IntPtr webView, mbTitleChangedCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnURLChanged", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnURLChanged(IntPtr webView, mbUrlChangedCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnLoadingFinish", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnLoadingFinish(IntPtr webView, mbLoadingFinishCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnDownload", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnDownload(IntPtr webView, mbDownloadCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnDownloadInBlinkThread", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnDownloadInBlinkThread(IntPtr webView, mbDownloadInBlinkThreadCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnAlertBox", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnAlertBox(IntPtr webView, mbAlertBoxCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnConfirmBox", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnConfirmBox(IntPtr webView, mbConfirmBoxCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnPromptBox", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnPromptBox(IntPtr webView, mbPromptBoxCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnNetGetFavicon", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnNetGetFavicon(IntPtr webView, mbNetGetFaviconCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnConsole", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnConsole(IntPtr webView, mbConsoleCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbOnClose", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbOnClose(IntPtr webView, mbCloseCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbOnDestroy", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbOnDestroy(IntPtr webView, mbDestroyCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbOnPrinting", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbOnPrinting(IntPtr webView, mbPrintingCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnDidCreateScriptContext", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnDidCreateScriptContext(IntPtr webView, mbDidCreateScriptContextCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnPluginList", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnPluginList(IntPtr webView, mbGetPluginListCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnImageBufferToDataURL", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnImageBufferToDataURL(IntPtr webView, mbImageBufferToDataURLCallback callback, IntPtr param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbGoBack", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbGoBack(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbGoForward", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbGoForward(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbStopLoading", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbStopLoading(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbReload", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbReload(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="command"></param>
        [DllImport("mb.dll", EntryPoint = "mbPerformCookieCommand", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbPerformCookieCommand(IntPtr webView, mbCookieCommand command);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorSelectAll", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbEditorSelectAll(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorCopy", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbEditorCopy(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorCut", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbEditorCut(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorPaste", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbEditorPaste(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorDelete", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbEditorDelete(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbEditorUndo", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbEditorUndo(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="message"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireMouseEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbFireMouseEvent(IntPtr webView, uint message, int x, int y, uint flags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireContextMenuEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbFireContextMenuEvent(IntPtr webView, int x, int y, uint flags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="delta"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireMouseWheelEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbFireMouseWheelEvent(IntPtr webView, int x, int y, int delta, uint flags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="virtualKeyCode"></param>
        /// <param name="flags"></param>
        /// <param name="systemKey"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireKeyUpEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbFireKeyUpEvent(IntPtr webView, uint virtualKeyCode, uint flags, int systemKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="virtualKeyCode"></param>
        /// <param name="flags"></param>
        /// <param name="systemKey"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireKeyDownEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbFireKeyDownEvent(IntPtr webView, uint virtualKeyCode, uint flags, int systemKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="charCode"></param>
        /// <param name="flags"></param>
        /// <param name="systemKey"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireKeyPressEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbFireKeyPressEvent(IntPtr webView, uint charCode, uint flags, int systemKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="IntPtr"></param>
        /// <param name="message"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbFireWindowsMessage", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbFireWindowsMessage(IntPtr webView, IntPtr IntPtr, uint message, IntPtr wParam, IntPtr lParam, IntPtr result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetFocus", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetFocus(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbKillFocus", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbKillFocus(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webview"></param>
        /// <param name="show"></param>
        [DllImport("mb.dll", EntryPoint = "mbShowWindow", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbShowWindow(IntPtr webview, int show);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="url"></param>
        [DllImport("mb.dll", EntryPoint = "mbLoadURL", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbLoadURL(IntPtr webView, string url);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="html"></param>
        /// <param name="baseUrl"></param>
        [DllImport("mb.dll", EntryPoint = "mbLoadHtmlWithBaseUrl", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbLoadHtmlWithBaseUrl(IntPtr webView, string html, string baseUrl);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbGetLockedViewDC", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbGetLockedViewDC(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbUnlockViewDC", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbUnlockViewDC(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        [DllImport("mb.dll", EntryPoint = "mbWake", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbWake(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="es"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbJsToDouble", CallingConvention = CallingConvention.StdCall)]
        public static extern double mbJsToDouble(IntPtr es, ulong v);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="es"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbJsTointean", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbJsTointean(IntPtr es, ulong v);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="es"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbJsToString", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbJsToString(IntPtr es, ulong v);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbOnJsQuery", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbOnJsQuery(IntPtr webView, mbJsQueryCallback callback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="queryId"></param>
        /// <param name="customMsg"></param>
        /// <param name="response"></param>
        [DllImport("mb.dll", EntryPoint = "mbResponseQuery", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbResponseQuery(IntPtr webView, long queryId, int customMsg, string response);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <param name="script"></param>
        /// <param name="isInClosure"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <param name="unuse"></param>
        [DllImport("mb.dll", EntryPoint = "mbRunJs", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbRunJs(IntPtr webView, IntPtr frameId, string script, int isInClosure, mbRunJsCallback callback, [MarshalAs(UnmanagedType.LPArray)]byte[] param, [MarshalAs(UnmanagedType.LPArray)]byte[] unuse);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <param name="script"></param>
        /// <param name="isInClosure"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbRunJsSync", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern long mbRunJsSync(IntPtr webView, IntPtr frameId, string script, int isInClosure);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbWebFrameGetMainFrame", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbWebFrameGetMainFrame(IntPtr webView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbIsMainFrame", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbIsMainFrame(IntPtr webView, IntPtr frameId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="b"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetNodeJsEnable", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbSetNodeJsEnable(IntPtr webView, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="device"></param>
        /// <param name="paramStr"></param>
        /// <param name="paramInt"></param>
        /// <param name="paramFloat"></param>
        [DllImport("mb.dll", EntryPoint = "mbSetDeviceParameter", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbSetDeviceParameter(IntPtr webView, string device, string paramStr, int paramInt, float paramFloat);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="calback"></param>
        /// <param name="param"></param>
        /// <param name="frameId"></param>
        [DllImport("mb.dll", EntryPoint = "mbGetContentAsMarkup", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbGetContentAsMarkup(IntPtr webView, mbGetContentAsMarkupCallback calback, [MarshalAs(UnmanagedType.LPArray)]byte[] param, IntPtr frameId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="calback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbGetSource", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbGetSource(IntPtr webView, mbGetSourceCallback calback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="calback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbUtilSerializeToMHTML", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbUtilSerializeToMHTML(IntPtr webView, mbGetSourceCallback calback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerInfo"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilCreateRequestCode", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbUtilCreateRequestCode(string registerInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultPath"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilIsRegistered", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern int mbUtilIsRegistered(string defaultPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <param name="printParams"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilPrint", CallingConvention = CallingConvention.StdCall)]
        public static extern int mbUtilPrint(IntPtr webView, IntPtr frameId, ref mbPrintSettings printParams);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilBase64Encode", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbUtilBase64Encode(string str);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilBase64Decode", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbUtilBase64Decode(string str);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilDecodeURLEscape", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbUtilDecodeURLEscape(string url);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilEncodeURLEscape", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string mbUtilEncodeURLEscape(string url);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbUtilCreateV8Snapshot", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr mbUtilCreateV8Snapshot(string str);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <param name="settings"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbUtilPrintToPdf", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbUtilPrintToPdf(IntPtr webView, IntPtr frameId, ref mbPrintSettings settings, mbPrintPdfDataCallback callback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="frameId"></param>
        /// <param name="settings"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbUtilPrintToBitmap", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbUtilPrintToBitmap(IntPtr webView, IntPtr frameId, ref mbScreenshotSettings settings, mbPrintBitmapCallback callback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="settings"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbUtilScreenshot", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbUtilScreenshot(IntPtr webView, ref mbScreenshotSettings settings, mbOnScreenshot callback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="url"></param>
        /// <param name="downloadJob"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbPopupDownloadMgr", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int mbPopupDownloadMgr(IntPtr webView, string url, [MarshalAs(UnmanagedType.LPArray)]byte[] downloadJob);

        /// <summary>
        /// 
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
        public static extern mbDownloadOpt mbPopupDialogAndDownload(IntPtr webView, [MarshalAs(UnmanagedType.LPArray)]byte[] param, long contentLength, string url, string mime, string disposition, IntPtr job, ref mbNetJobDataBind dataBind, ref mbDownloadBind callbackBind);

        /// <summary>
        /// 
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
        public static extern mbDownloadOpt mbDownloadByPath(IntPtr webView, [MarshalAs(UnmanagedType.LPArray)]byte[] param, [MarshalAs(UnmanagedType.LPWStr)]string path, long contentLength, string url, string mime, string disposition, IntPtr job, ref mbNetJobDataBind dataBind, ref mbDownloadBind callbackBind);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        [DllImport("mb.dll", EntryPoint = "mbGetPdfPageData", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbGetPdfPageData(IntPtr webView, mbOnGetPdfPageDataCallback callback, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="buf"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("mb.dll", EntryPoint = "mbCreateMemBuf", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mbCreateMemBuf(IntPtr webView, [MarshalAs(UnmanagedType.LPArray)]byte[] buf, long length);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        [DllImport("mb.dll", EntryPoint = "mbFreeMemBuf", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbFreeMemBuf(ref mbMemBuf buf);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="fileName"></param>
        [DllImport("mb.dll", EntryPoint = "mbPluginListBuilderAddPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbPluginListBuilderAddPlugin(string builder, string name, string description, string fileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        [DllImport("mb.dll", EntryPoint = "mbPluginListBuilderAddMediaTypeToLastPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbPluginListBuilderAddMediaTypeToLastPlugin(string builder, string name, string description);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="fileExtension"></param>
        [DllImport("mb.dll", EntryPoint = "mbPluginListBuilderAddFileExtensionToLastMediaType", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void mbPluginListBuilderAddFileExtensionToLastMediaType(string builder, string fileExtension);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("mb.dll", EntryPoint = "mbEnableHighDPISupport", CallingConvention = CallingConvention.StdCall)]
        public static extern void mbEnableHighDPISupport();
    }

    #endregion
}
