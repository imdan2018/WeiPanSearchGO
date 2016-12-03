using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;


namespace WeiPanSearchGO
{

    public class HttpClass
    {
        public static string xGet(string paras)
        {

            string x = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://vdisk.weibo.com/wap/api/weipan/list?"+ paras);
                req.Method = "GET";
                req.Referer = "http://vdisk.weibo.com/wap/share/list";
                /*req.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
                req.Host = "vdisk.weibo.com";*/
                using (WebResponse response = req.GetResponse())
                {
                    Stream receiveStream = response.GetResponseStream();

                    // Pipes the stream to a higher level stream reader with the required encoding format. 
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                   x = UnicodeDencode(readStream.ReadToEnd());

                   
                    response.Close();
                    readStream.Close();
                    

                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return x;
        }
        public static string UnicodeDencode( string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            return Regex.Unescape(str);
        }  

        public static string UnicodeEncode(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    strResult.Append("\\u");
                    strResult.Append(((int)str[i]).ToString("x4"));
                }
            }
            return strResult.ToString();
        }  

        public static void Post()
        {
            string param = "hl=zh-CN&newwindow=1";
            byte[] bs = Encoding.ASCII.GetBytes(param);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://www.jb51.net/");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理
            }
        }

        public static T JsonTo<T>(string json)
        {
            var t = JsonConvert.DeserializeObject<T>(json);
            return t;
        } 

    }
}
