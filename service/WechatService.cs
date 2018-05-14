using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace service
{
    public class WechatService
    {
        public class sendToUserModel
        {
            public string touser { set; get; }
            public string msgtype { set; get; }
            public int agentid { set; get; }
            public Text text { set; get; }
            public string content { set; get; }
            public int safe { set; get; }
        }
        public class Text
        {
            public string content { set; get; }
        }

        /// <summary>
        /// 获取企业号token
        /// </summary>
        /// <returns></returns>
        public string getQYToken()
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", ConfigurationManager.AppSettings["cropid"], ConfigurationManager.AppSettings["corpSecret"]);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = 10000;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string resultString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            JObject resultJSON = (JObject)JsonConvert.DeserializeObject(resultString);
            string token = resultJSON["access_token"].ToString();
            return token;
        }

        /// <summary>
        /// 获取企业号token
        /// </summary>
        /// <returns></returns>
        public string send(string token)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");//处理跨域问题
            Text text = new Text();
            text.content = "欢迎登陆<a href=\"http://i199527q33.imwork.net:8000\">自家用测试版</a>！\n后续功能将逐步开放，敬请期待！";
            sendToUserModel stum = new sendToUserModel();
            stum.touser = "JinYuanZhen";
            stum.msgtype = "text";
            stum.agentid = 1000002;
            stum.text = text;
            stum.safe = 0;
            var jsontest = Newtonsoft.Json.JsonConvert.SerializeObject(stum);
            var strtest = jsontest.ToString();
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", token);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = 10000;
            byte[] postBytes = Encoding.UTF8.GetBytes(jsontest);
            request.ContentLength = Encoding.UTF8.GetBytes(jsontest).Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
            }

            //HttpContext.Current.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(stum));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            reader.Close();

            return null;
        }
    }
}
