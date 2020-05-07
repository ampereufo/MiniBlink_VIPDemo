using System;
using System.Text;
using System.Runtime.InteropServices;


namespace MBVIP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;
        public int cy;
        public SIZE(int cx, int cy)
        {
            this.cx = cx;
            this.cy = cy;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PAINTSTRUCT
    {
        public int hdc;
        public int fErase;
        public RECT rcPaint;
        public int fRestore;
        public int fIncUpdate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] rgbReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COMPOSITIONFORM
    {
        public int dwStyle;
        public POINT ptCurrentPos;
        public RECT rcArea;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAP
    {
        public int bmType;
        public int bmWidth;
        public int bmHeight;
        public int bmWidthBytes;
        public short bmPlanes;
        public short bmBitsPixel;
        public int bmBits;
    }

    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct BLENDFUNCTION
    {
        [FieldOffset(0)]
        public byte BlendOp;
        [FieldOffset(1)]
        public byte BlendFlags;
        [FieldOffset(2)]
        public byte SourceConstantAlpha;
        [FieldOffset(3)]
        public byte AlphaFormat;
    }

    public enum WinConst : int
    {
        GWL_EXSTYLE = -20,
        GWL_WNDPROC = -4,
        WS_EX_LAYERED = 524288,
        WM_PAINT = 15,
        WM_ERASEBKGND = 20,
        WM_SIZE = 5,
        WM_KEYDOWN = 256,
        WM_KEYUP = 257,
        WM_CHAR = 258,
        WM_LBUTTONDOWN = 513,
        WM_LBUTTONUP = 514,
        WM_MBUTTONDOWN = 519,
        WM_RBUTTONDOWN = 516,
        WM_LBUTTONDBLCLK = 515,
        WM_MBUTTONDBLCLK = 521,
        WM_RBUTTONDBLCLK = 518,
        WM_MBUTTONUP = 520,
        WM_RBUTTONUP = 517,
        WM_MOUSEMOVE = 512,
        WM_CONTEXTMENU = 123,
        WM_MOUSEWHEEL = 522,
        WM_SETFOCUS = 7,
        WM_KILLFOCUS = 8,
        WM_IME_STARTCOMPOSITION = 269,
        WM_NCHITTEST = 132,
        WM_GETMINMAXINFO = 36,
        WM_DESTROY = 2,
        WM_SETCURSOR = 32,
        MK_CONTROL = 8,
        MK_SHIFT = 4,
        MK_LBUTTON = 1,
        MK_MBUTTON = 16,
        MK_RBUTTON = 2,
        KF_REPEAT = 16384,
        KF_EXTENDED = 256,
        SRCCOPY = 13369376,
        CAPTUREBLT = 1073741824,
        CFS_POINT = 2,
        CFS_FORCE_POSITION = 32,
        OBJ_BITMAP = 7,
        AC_SRC_OVER = 0,
        AC_SRC_ALPHA = 1,
        ULW_ALPHA = 2,
        WM_INPUTLANGCHANGE = 81,
        WM_NCDESTROY = 130,
    }


    internal static class MBVIP_Common
    {
        [DllImport("user32.dll", EntryPoint = "GetWindowLongW")]
        internal static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongW")]
        internal static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongW")]
        internal static extern IntPtr GetWindowLongIntPtr(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongW")]
        internal static extern IntPtr SetWindowLong(IntPtr hwnd, int nIndex, Delegate dwNewLong);

        [DllImport("user32.dll", EntryPoint = "CallWindowProcW")]
        internal static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetClientRect")]
        internal static extern int GetClientRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32.dll", EntryPoint = "BeginPaint")]
        internal static extern IntPtr BeginPaint(IntPtr hwnd, ref PAINTSTRUCT lpPaint);

        [DllImport("user32.dll", EntryPoint = "IntersectRect")]
        internal static extern int IntersectRect(ref RECT lpDestRect, ref RECT lpSrc1Rect, ref RECT lpSrc2Rect);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        internal static extern int BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll", EntryPoint = "EndPaint")]
        internal static extern int EndPaint(IntPtr hwnd, ref PAINTSTRUCT lpPaint);

        [DllImport("user32.dll", EntryPoint = "GetFocus")]
        internal static extern IntPtr GetFocus();

        [DllImport("user32.dll", EntryPoint = "SetFocus")]
        internal static extern IntPtr SetFocus(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "SetCapture")]
        internal static extern int SetCapture(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        internal static extern int ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "ScreenToClient")]
        internal static extern int ScreenToClient(IntPtr hwnd, ref POINT lpPoint);

        [DllImport("imm32.dll", EntryPoint = "ImmGetContext")]
        internal static extern IntPtr ImmGetContext(IntPtr hwnd);

        [DllImport("imm32.dll", EntryPoint = "ImmSetCompositionWindow")]
        internal static extern int ImmSetCompositionWindow(IntPtr himc, ref COMPOSITIONFORM lpCompositionForm);

        [DllImport("imm32.dll", EntryPoint = "ImmReleaseContext")]
        internal static extern int ImmReleaseContext(IntPtr hwnd, IntPtr himc);

        [DllImport("user32.dll", EntryPoint = "DefWindowProcA")]
        internal static extern IntPtr DefWindowProc(IntPtr hwnd, uint wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        internal static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32.dll", EntryPoint = "OffsetRect")]
        internal static extern int OffsetRect(ref RECT lpRect, int x, int y);

        [DllImport("gdi32.dll", EntryPoint = "GetCurrentObject")]
        internal static extern IntPtr GetCurrentObject(IntPtr hdc, int uObjectType);

        [DllImport("gdi32.dll", EntryPoint = "GetObjectW")]
        internal static extern int GetObject(IntPtr hObject, int nCount, ref BITMAP lpObject);

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        internal static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "UpdateLayeredWindow")]
        internal static extern int UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst, IntPtr pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        internal static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        internal static extern int DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        internal static extern int DeleteDC(IntPtr hdc);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        internal static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll", EntryPoint = "InvalidateRect")]
        internal static extern int InvalidateRect(IntPtr hwnd, ref RECT lpRect, bool bErase);

        [DllImport("kernel32.dll", EntryPoint = "lstrlenA")]
        internal static extern int lstrlen(IntPtr lpString);


        internal static int LOWORD(this IntPtr dword)
        {
            return (int)dword & 65535;
        }

        internal static int HIWORD(this IntPtr dword)
        {
            return (int)dword >> 16;
        }

        internal static string UTF8PtrToStr(this IntPtr utf8)
        {
            if (utf8 == IntPtr.Zero)
            {
                return string.Empty;
            }

            int iLen = lstrlen(utf8);
            byte[] bytes = new byte[iLen];
            Marshal.Copy(utf8, bytes, 0, iLen);

            return Encoding.UTF8.GetString(bytes);
        }

        internal static IntPtr StrToUtf8Ptr(this string str)
        {
            IntPtr ptr = IntPtr.Zero;

            if (!string.IsNullOrEmpty(str))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                ptr = Marshal.AllocHGlobal(bytes.Length + 1);
                Marshal.Copy(bytes, 0, ptr, bytes.Length);
                Marshal.WriteByte(ptr, bytes.Length, (byte)'\0');
            }

            return ptr;
        }


        internal static string UnicodePtrToStr(this IntPtr unicode)
        {
            if (unicode == IntPtr.Zero)
            {
                return string.Empty;
            }

            int iLen = lstrlen(unicode);
            byte[] bytes = new byte[iLen];
            Marshal.Copy(unicode, bytes, 0, iLen);

            return Encoding.Unicode.GetString(bytes);
        }

        internal static IntPtr StrToUnicodePtr(this string str)
        {
            IntPtr ptr = IntPtr.Zero;

            if (!string.IsNullOrEmpty(str))
            {
                byte[] bytes = Encoding.Unicode.GetBytes(str);
                ptr = Marshal.AllocHGlobal(bytes.Length + 1);
                Marshal.Copy(bytes, 0, ptr, bytes.Length);
                Marshal.WriteByte(ptr, bytes.Length, (byte)'\0');
            }

            return ptr;
        }

        internal static byte[] StructToBytes(this object structObj)
        {
            int iSize = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[iSize];
            IntPtr structPtr = Marshal.AllocHGlobal(iSize);
            Marshal.StructureToPtr(structObj, structPtr, false);
            Marshal.Copy(structPtr, bytes, 0, iSize);
            Marshal.FreeHGlobal(structPtr);

            return bytes;
        }

        internal static object BytesToStuct(this byte[] bytes, Type type)
        {
            object objRet = null;

            int iSize = Marshal.SizeOf(type);
            if (iSize <= bytes.Length)
            {
                IntPtr structPtr = Marshal.AllocHGlobal(iSize);
                Marshal.Copy(bytes, 0, structPtr, iSize);
                objRet = Marshal.PtrToStructure(structPtr, type);
                Marshal.FreeHGlobal(structPtr);
            }

            return objRet;
        }

        internal static IntPtr StructToUTF8Ptr(this object structObj)
        {
            int iSize = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[iSize];
            IntPtr structPtr = Marshal.AllocHGlobal(iSize);
            Marshal.StructureToPtr(structObj, structPtr, false);
            Marshal.Copy(structPtr, bytes, 0, iSize);

            return structPtr;
        }

        internal static object UTF8PtrToStruct(this IntPtr structPtr, Type type)
        {
            return Marshal.PtrToStructure(structPtr, type);
        }

        internal static byte[] UTF8PtrToByte(this IntPtr utf8)
        {
            if (utf8 == IntPtr.Zero)
            {
                return new byte[0];
            }

            int iLen = lstrlen(utf8);
            byte[] bytes = new byte[iLen];
            Marshal.Copy(utf8, bytes, 0, iLen);

            return bytes;
        }

        internal static IntPtr ByteToUtf8Ptr(this byte[] data)
        {
            IntPtr ptr = IntPtr.Zero;

            if (data != null)
            {
                ptr = Marshal.AllocHGlobal(data.Length + 1);
                Marshal.Copy(data, 0, ptr, data.Length);
                Marshal.WriteByte(ptr, data.Length, (byte)'\0');
            }

            return ptr;
        }
    }
}
