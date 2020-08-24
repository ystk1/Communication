using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommTestTool
{
    class CAppConfigWrapper
    {

        // シングルトン
        private static CAppConfigWrapper singleton = new CAppConfigWrapper ( );

        // インスタンスを取得
        public static CAppConfigWrapper GetInstance ( ) {
            return singleton;
        }

         // key<->valueテーブル
        Dictionary<string, string> appconfigdat = new Dictionary<string, string> ( ){
            {"commno","1"},
            {"bps","38400"},
            
        };
    }
}
