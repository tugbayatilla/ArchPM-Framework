//using ArchPM.Core.Session;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace ArchPM.Web.Core
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <code>must be set in config file at pages section in pageBaseType="ArchPM.Web.Core.BaseViewPage"</code>
//    public class BaseViewPage : WebViewPage
//    {
//        /// <summary>
//        /// When overridden in a derived class, gets a user value based on the HTTP context.
//        /// </summary>
//        public new AuthenticatedUserInfo User
//        {
//            get { return base.User as AuthenticatedUserInfo; }
//        }

//        public override void Execute()
//        {
//        }
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <code>must be set in config file at pages section in pageBaseType="ArchPM.Web.Core.BaseViewPage"</code>
//    public class BaseViewPage<TModel> : WebViewPage<TModel>
//    {
//        /// <summary>
//        /// When overridden in a derived class, gets a user value based on the HTTP context.
//        /// </summary>
//        public new AuthenticatedUserInfo User
//        {
//            get { return base.User as AuthenticatedUserInfo; }
//        }

//        public override void Execute()
//        {
//        }
//    }
//}
