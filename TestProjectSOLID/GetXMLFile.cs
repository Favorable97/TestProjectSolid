using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectSOLID
{
    class GetXMLFile
    {
        private string Date { get; set; }
        public GetXMLFile(string date)
        {
            Date = date; 
        }

        public string ToGetXMLFile()
        {
            string url1 = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + Date;
            var webhost = WebRequest.CreateHttp(url1);
            string strResp;
            using (WebResponse req = webhost.GetResponse())
            {
                using (Stream stream = req.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(stream, Encoding.Default))
                    {
                        strResp = sReader.ReadToEnd();
                    }
                }
            }

            return strResp;
        }
    }
}
