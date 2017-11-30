using ArchPM.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchPM.Web.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <code>must be set in config file at pages section in pageBaseType="ArchPM.Web.Core.BaseViewPage"</code>
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new AuthenticatedUserInfo User
        {
            get { return base.User as AuthenticatedUserInfo; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <code>must be set in config file at pages section in pageBaseType="ArchPM.Web.Core.BaseViewPage"</code>
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new AuthenticatedUserInfo User
        {
            get { return base.User as AuthenticatedUserInfo; }
        }
    }
}
