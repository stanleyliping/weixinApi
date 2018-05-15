using service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace weixinApi.Controllers
{
    public class QYwechatController : ApiController
    {
        /// <summary>
        /// 发送消息给个人(id,text写死测试)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("wechatApi/QYwechat/sendMessageTest")]
        [ActionName("sendMessageTest")]
        public string sendMessageTest()
        {
            try {
                WechatService s = new WechatService();
                var QYToken = s.getQYToken();
                var result=s.send(QYToken);
                return result;
            }
            catch (Exception e) {
                return e.ToString();
            }
        }
        /// <summary>
        /// 通过ID获取企业成员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("wechatApi/QYwechat/getQYMemberById")]
        [ActionName("getQYMemberById")]
        public string getQYMemberById(string id)
        {
            try
            {
                WechatService s = new WechatService();
                var QYToken = s.getQYToken();
                var result = s.getQYMemberById(QYToken,id);
                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
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
