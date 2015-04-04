using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Stamp4Fun.Upgrade
{
    public class Util
    {
        public static void DownloadWebFile(string url)
        {
            // replace the local ones
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            BinaryReader brd = new BinaryReader(dataStream);
            FileStream fs = new FileStream(url.Substring(
                url.LastIndexOf("/") + 1), FileMode.Create);

            byte[] buf = new byte[1024 * 1024];

            int len = 0;
            int imglen = 0;
            do
            {
                len = brd.Read(buf, imglen, buf.Length - imglen);
                imglen += len;

            } while (len > 0);

            brd.Close();
            dataStream.Close();
            response.Close();

            fs.Write(buf, 0, imglen);
            fs.Close();
        }
    }
}
