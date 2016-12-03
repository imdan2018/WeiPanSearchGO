using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace WeiPanSearchGO
{
    class Program
    {
        static void Main(string[] args)
        {
            run();

            Console.ReadLine();
        }

        static private void run()
        {
            StreamWriter sw = new StreamWriter("G:/xiangmu/WeiPanSearchGO/WeiPanSearchGO/bin/Release/data.txt");
            for (int i = 1; i <= 638; i++)
            {
                string jsonstring = HttpClass.xGet("cid=0&page="+ i +"&page_size=20&_=1480687377116");
                //Console.WriteLine(jsonstring);

                //StreamWriter sw = new StreamWriter("G:/xiangmu/WeiPanSearchGO/WeiPanSearchGO/bin/Release/data.txt");
               
                
               

                WeiBoDown[] data = HttpClass.JsonTo<WeiBoDowns>(jsonstring).data;
                string demostr=null;
                foreach (var x in data)
                {

                    demostr = demostr + "\n\r" + x.share_id + " " + x.title + " " + x.link + "\n\r";

                    Console.WriteLine(x.share_id + " " + x.title);
                    
                }
                sw.WriteLine(demostr);
               
            }
            sw.Close();

           
        }


    }

    class WeiBoDowns
    {
        public int totalnum { get; set; }
        public WeiBoDown[] data { get; set; }

    }
    class WeiBoDown
    {
        public string share_id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string link { get; set; }
    }

}
