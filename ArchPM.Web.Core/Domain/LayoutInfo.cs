//using System;
//using ArchPM.Core.Enums;
//using System.Drawing;

//namespace ArchPM.Web.Core.Domain
//{
//    public sealed class LayoutInfo
//    {
//        public static readonly LayoutInfo Instance = new LayoutInfo();
//        private LayoutInfo()
//        {
//        }

//        public ApplicationEnvironments Environment
//        {
//            get
//            {

//                ApplicationEnvironments result = ApplicationEnvironments.Test;
//                String env = ConfigurationManager.AppSettings.Get("Kernel.Environment");
//                if (!String.IsNullOrEmpty(env))
//                    result = EnumManager<ApplicationEnvironments>.Parse(env);

//                return result;
//            }
//        }
//        public string EnvironmentName { get { return this.Environment.ToString(); } }
//        public Color BackgroundColor
//        {
//            get
//            {
//                Color result = Color.Transparent;
//                switch (this.Environment)
//                {                    
//                    case ApplicationEnvironments.Test: { result = Color.CadetBlue; break; }

//                    case ApplicationEnvironments.Dev: 
//                    case ApplicationEnvironments.Prod: { result = ColorTranslator.FromHtml("#2B3643"); break; }
//                    default:
//                        break;
//                }
//                return result;
//            }
//        }

//        public Color TitleColor
//        {
//            get
//            {
//                Color result = Color.White;
//                switch (this.Environment)
//                {
//                    case ApplicationEnvironments.Dev: { result = Color.Lime; break; }
//                    default:
//                        break;
//                }
//                return result;
//            }
//        }

//        public AuthUserDTO AuthUser
//        {
//            get
//            {
//                return Sisli.MIS.Modules.SosyalYardim.Infrastructure.SessionProvider.getAuthUser();
//            }
//        }

//        public String Version { get; set; }

//    }
//}
