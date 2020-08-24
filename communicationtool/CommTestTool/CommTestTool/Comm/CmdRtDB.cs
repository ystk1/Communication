using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;

namespace serial_comm
{

    /// <summary>
    /// ロボット　コマンドID　
    /// </summary>
    public enum enRtCmdIDType
    {
        en_cmdID_SOG,
        en_cmdID_CST,
    }

    /// <summary>
    /// コマンド　種類　データベース
    /// </summary>
    class CCmdRtDBType_var
    {
        public enRtCmdIDType enCmdID { get; set; }
        public string strCmdName { get; set; }
        public int ID { get; set; }
    }

    // ListDataBase
    class CListDat_RtDB
    {
        public delegate void SerialDataReceivedEventHandler ( SerialDataReceivedEventArgs e ); // 受信データ　でりげーと


        // 通信コマンド　リスト
        private List<CCmdRtDBType_var> m_listCmdRtDb = new List<CCmdRtDBType_var> {
            // 原点復帰
            new CCmdRtDBType_var{
                enCmdID = enRtCmdIDType.en_cmdID_SOG,
                strCmdName = "SOG",
            },

            // 現在座標取得
            new CCmdRtDBType_var{
                enCmdID = enRtCmdIDType.en_cmdID_CST,
                strCmdName = "CST",
           }
        };

        public Dictionary<enRtCmdIDType, CCmdRtDBType_var> dictRtDb { set; get; } // 通信コマンド　ディクショナリ


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CListDat_RtDB ( ) {
            CraeteDict ( );
        }

        /// <summary>
        /// 通信コマンド　ディクショナリ生成(ディクショナリ std::mapのようなもの)
        /// </summary>
        void CraeteDict ( ){
            dictRtDb = new Dictionary<enRtCmdIDType, CCmdRtDBType_var> ( );     //　ディクショナリ生成
            foreach ( CCmdRtDBType_var cmdrtdbtype in m_listCmdRtDb ) {
                dictRtDb.Add ( cmdrtdbtype.enCmdID, cmdrtdbtype ); 
           }
        }
    }
}
