using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiContrib.Formatting.Html;

namespace DotNetDoodle.OAuthServer.Controllers
{
    public class AuthorizeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return new ViewResult(Request, "Index", null);
        }
    }
}