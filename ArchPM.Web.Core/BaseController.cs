using ArchPM.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchPM.Web.Core
{
    public class BaseController : Controller
    {
        protected virtual new AuthenticatedUserInfo User
        {
            get { return HttpContext.User as AuthenticatedUserInfo; }
        }
    }
}
