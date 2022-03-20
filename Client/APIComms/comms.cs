using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.Comms
{
    class comms
    {
        public static string SendGET(string addr){
            HttpWebRequest req = WebRequest.CreateHttp(addr);

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse()) {
                using (Stream stream = resp.GetResponseStream()) {
                    using (StreamReader reader = new StreamReader(stream)) { return reader.ReadToEnd(); }
                }
            }
        }

        public static string SendPOST(string addr, string content){
            HttpWebRequest req = WebRequest.CreateHttp(addr);

            byte[] data = Encoding.UTF8.GetBytes(content);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.ContentLength = data.Length;

            using (var stream = req.GetRequestStream()) { stream.Write(data, 0, data.Length); }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            return new StreamReader(resp.GetResponseStream()).ReadToEnd();
        }

        public static string SendDELETE(string addr){
            HttpWebRequest req = WebRequest.CreateHttp(addr);
            req.Method = "DELETE";

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse()) {
                using (Stream stream = resp.GetResponseStream()) {
                    using (StreamReader reader = new StreamReader(stream)) { return reader.ReadToEnd(); }
                }
            }
        }

    }
}
