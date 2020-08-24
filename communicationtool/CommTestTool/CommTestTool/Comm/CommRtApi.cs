using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Ports;
//using System.ComponentModel;


using namespaceErrCode;

namespace serial_comm
{


    /// <summary>
    /// ロボット通信API
    /// </summary>
    class CCommRtApi
    {

     //   private CSerialComm m_seriCur;      // カレントのCSerialComm

        public SerialPort m_serialPort; //通信コンポーネント本体

        // コンストラクタ
        public CCommRtApi ( ) {
            m_serialPort = new SerialPort ( );
       //     m_seriCur = new CSerialComm ( );

            m_serialPort.ReadBufferSize = 1024;     // 受信バッファサイズ

        }

        //// シリアル通信　データ　をカレント情報として設定
        //public void Set_SerialComm ( serial_comm.CSerialComm m_seridat ) {
        //    m_seriCur = m_seridat;
        //}

        // TypeA 通信プロトコル
        /// <summary>
        /// 通信プロトコルA　throw CErrCode_型
        /// </summary>
        /// <param name="rtdbdat"></param>
        /// <param name="refSeriPrt"></param>
        public void Sync_ProtocolA ( CCmdRtDBType_var rtdbdat, ref SerialPort refSeriPrt ) {
            enErrCode_ enret = enErrCode_.en_success;
            List<byte> receivedBuf = new List<byte> ( ); // 受信バッファ

            string strSendDat = null;   //rs232cで送信する文字列

            // コマンドパケット生成
            if ( ( enret = CCommUtility.GetInstance().Create_Send_Dat ( rtdbdat.strCmdName, ref strSendDat, false,false,false,false,false,false, false, CommTestTool.Form1.enSendType.en_SendType_Hex) ) != enErrCode_.en_success ) {         // bool型フラグは全てfalse / 本ソフトウェアでは、Form上でboolフラグ入りの送信文字列が作成されるため、boolフラグは全てOFFにする
                throw new CExcept_ ( enret );
            }

            //! 送信するテキストがない場合、データ送信は行わない.
            if ( string.IsNullOrEmpty ( strSendDat ) == true ) {
                throw new CExcept_ ( enErrCode_.en_err_none_send_str );
            }

            refSeriPrt.Write ( strSendDat ); //送信

            //待機
            System.Threading.Thread.Sleep ( 10 );

            // 応答パケット(同期通信)
            while ( refSeriPrt.BytesToRead > 0 ) {
                // 1バイト受信してバッファに格納
                receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );    
            }
            // 配列に変換
            byte [] recevDat = receivedBuf.ToArray ( );
            
            // 通信完了
        }

        public void ProtocolA ( CCmdRtDBType_var rtdbdat ) {
            Sync_ProtocolA ( rtdbdat, ref m_serialPort );
        }

    }
}