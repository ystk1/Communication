using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using namespaceErrCode;

namespace NAME_SPACE_SYSDAT
{

    class CSystemDat
    {

        // CHデータ

//        public List< List<byte>> m_bytListSendDat = new List< List<byte>> ( consLISTSZ );

        public List<string> m_listCommStr = new List<string> ( consLISTSZ );            // 送信文字列リスト
    
        // シングルトン
        private static CSystemDat singleton = new CSystemDat ( );


        // const変数
        public const int consLISTSZ = 100;

        //public int consLISTSZ {
        //    get;
        //}        // Chデータ格納用のListのサイズを１００に設定

        // コンストラクタ
        private CSystemDat ( ) {
                    }

        
        // インスタンスを取得
        public static CSystemDat GetInstance ( ) {
            return singleton;
        }
    }
}