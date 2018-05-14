using service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace weixinApi.Controllers
{
    public class wechatController : ApiController
    {
        // GET: api/wechat
        public IEnumerable<string> Get()
        {
            WechatService s = new WechatService();
            var QYToken = s.getQYToken();
            s.send(QYToken);
            return new string[] { "value1", "value2" };
        }

        // GET: api/wechat/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/wechat
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/wechat/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/wechat/5
        public void Delete(int id)
        {
        }
    }
}
