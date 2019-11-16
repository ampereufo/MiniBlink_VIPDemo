using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlink_VIPDemo
{
    class Common
    {
        public static string UTF8PtrtoString(IntPtr UTF8Ptr)
        {
            int size = 0;
            byte[] buffer = { };
            do
            {
                size++;
                Array.Resize(ref buffer, size);
                Marshal.Copy(UTF8Ptr, buffer, 0, size);
            } while (buffer[size - 1] != 0);

            if (size == 1)
            {
                return string.Empty;
            }
            else
            {
                Array.Resize(ref buffer, size - 1);
                return Encoding.UTF8.GetString(buffer);
            }
        }
    }
}
