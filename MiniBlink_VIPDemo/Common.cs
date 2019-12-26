using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MBVIP;

namespace MiniBlink_VIPDemo
{
    static class Common
    {
        [DllImport("kernel32.dll", EntryPoint = "lstrlenA")]
        private static extern int lstrlen(IntPtr lpString);

        public static object UTF8PtrToStruct(this IntPtr structPtr, Type type)
        {
            return Marshal.PtrToStructure(structPtr, type);
        }

        public static string UnicodePtrToStr(this IntPtr unicode)
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

        public static byte[] StructToBytes(this object structObj)
        {
            int iSize = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[iSize];
            IntPtr structPtr = Marshal.AllocHGlobal(iSize);
            Marshal.StructureToPtr(structObj, structPtr, false);
            Marshal.Copy(structPtr, bytes, 0, iSize);
            Marshal.FreeHGlobal(structPtr);

            return bytes;
        }

        public static Dictionary<string, string> GetHttpPostData(MBVIP_WebView webView, IntPtr ptrJob)
        {
            Dictionary<string, string> HttpDataRet = new Dictionary<string, string>();

            try
            {
                mbPostBodyElement[] elementsArr = webView.GetPostBody(ptrJob);
                foreach (var item in elementsArr)
                {
                    if (item.type == mbHttpBodyElementType.mbHttpBodyElementTypeData)
                    {
                        mbMemBuf memBuf = (mbMemBuf)item.data.UTF8PtrToStruct(typeof(mbMemBuf));
                        byte[] byteBuf = new byte[memBuf.length];
                        Marshal.Copy(memBuf.data, byteBuf, 0, byteBuf.Length);

                        string strHttpData = Encoding.UTF8.GetString(byteBuf);
                        if (!strHttpData.StartsWith("--"))
                        {
                            string[] strDatas = strHttpData.Split('&');
                            foreach (string strData in strDatas)
                            {
                                string[] strDatas2 = strData.Split('=');
                                if (strDatas2.Length < 1)
                                {
                                    continue;
                                }
                                else if (strDatas2.Length == 1)
                                {
                                    HttpDataRet[strDatas2[0]] = string.Empty;
                                }
                                else
                                {
                                    int iIndex = strData.IndexOf("=", StringComparison.Ordinal) + 1;
                                    HttpDataRet[strDatas2[0]] = strData.Length > iIndex ? strData.Substring(iIndex) : string.Empty;
                                }
                            }
                        }
                    }
                    else if (item.type == mbHttpBodyElementType.mbHttpBodyElementTypeFile)
                    {
                        string strFile = item.filePath.UnicodePtrToStr();
                    }
                }
            }
            catch
            {
                HttpDataRet = null;
            }

            return HttpDataRet;
        }
    }
}
