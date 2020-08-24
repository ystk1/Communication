using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CommTestTool
{
    class CAppSettings : ApplicationSettingsBase
    {
        // シングルトン
        private static CAppSettings singleton = new CAppSettings ( );

        // インスタンスを取得
        public static CAppSettings GetInstance ( ) {

            return singleton;
        }

        public CAppSettings ( ) {
            // xml書き込み先
            Configuration conf = ConfigurationManager.OpenExeConfiguration ( ConfigurationUserLevel.None );
            string strdebug = conf.FilePath;
        }
        
        [UserScopedSetting]
        public int icommcur {
            get {
                return ( int ) this ["icommcur"];
            }
            set {
                this ["icommcur"] = value;
            }
        }
        [UserScopedSetting]
        public int ibpscur {
            get {
                return ( int ) this ["ibpscur"];
            }
            set {
                this ["ibpscur"] = value;
            }
        }
        [UserScopedSetting]
        public int iHandshake {
            get {
                return ( int ) this ["iHandshake"];
            }
            set {
                this ["iHandshake"] = value;
            }
        }
        [UserScopedSetting]
        public int iStpbit {
            get {
                return ( int ) this ["iStpbit"];
            }
            set {
                this ["iStpbit"] = value;
            }
        }
        [UserScopedSetting]
        public int iSendIntvalTm {
            get {
                return ( int ) this ["iSendIntvalTm"];
            }
            set {
                this ["iSendIntvalTm"] = value;
            }
        }
    }
}
