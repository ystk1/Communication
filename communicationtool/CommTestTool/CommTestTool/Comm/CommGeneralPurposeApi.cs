using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


using System.IO;
using System.IO.Ports;

using namespaceErrCode;

namespace serial_comm
{


    /// <summary>
    /// ロボット通信API
    /// </summary>
    class CCommGeneralPurposeApi
    {
        const int consTM_5MS = 5;           // 通信処理間タイマー値

        byte [] ENQ = { 0x05 };
        byte [] EOT = { 0x04 };
        byte [] ACK = { 0x06 };
        byte [] CAN = { 0x18 };
        byte [] ETX = { 0x03 };
        byte [] CR = {0x0D};
        byte [] LF = { 0x0A };

        //const byte[] byACK ={ 0x06};

        //   private CSerialComm m_seriCur;      // カレントのCSerialComm

        public SerialPort m_serialPort; //通信コンポーネント本体
        //      private DateTime endTime;    //タイマーイベント用

        byte [] bRecvBuf = new byte [1024];     // 受信バッファ

        public static bool bCommEndFlg = false;                // Communicationの中断フラグ
        System.Windows.Forms.Control cnt_mainform;          // メインのフォームコントロール 受信時のデリゲート先に使用
        public delegate void MyEventHandler ( string str );         // form用イベントハンドラ
        public event MyEventHandler eveCommGenePurPosApi = null;           // 受信時用イベント

        bool bAsyncRecvContinueFlg = false;         // 送信前に、非同期受信が連続している場合 true / 直前が送信処理 false

        // コンストラクタ
        public CCommGeneralPurposeApi ( System.Windows.Forms.Control mainThreadForm ) {
            m_serialPort = new SerialPort ( );
            //   m_seriCur = new CSerialComm ( );

            m_serialPort.ReadBufferSize = 1024;     // 受信バッファサイズ
            m_serialPort.WriteBufferSize = 1024;

            //      this.m_serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);          //受信デリゲート
            m_serialPort.ReadTimeout = 500;             // TimeOut [ミリsec]
            m_serialPort.WriteTimeout = 500;

            m_serialPort.Encoding = Encoding.ASCII;     // ASCII

            cnt_mainform = mainThreadForm;      // メインFormのコントロール書き込み
            m_serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler ( serialPort_ASyncDataReceived );       //　受信用デリゲート
        }

        //// シリアル通信　データ　をカレント情報として設定
        //public void Set_SerialComm ( serial_comm.CSerialComm m_seridat ) {
        //    m_seriCur = m_seridat;
        //}


        /// <summary>
        /// 文字列送信
        /// </summary>
        /// <param name="str_src"></param>
        /// <param name="refseriPrt"></param>
        private void Async_Protocol_Send ( string str_src, ref SerialPort refSeriPrt ) {
            //    enErrCode_ enret = enErrCode_.en_success;

            // 送信バッファクリアx
            refSeriPrt.DiscardOutBuffer ( );

            // 送信
            refSeriPrt.Write ( str_src ); //送信

            //待機
            System.Threading.Thread.Sleep ( consTM_5MS );
        }

        /// <summary>
        /// すべての文字列送信
        /// </summary>
        /// <param name="list_str_src"></param>
        /// <param name="refSeriPrt"></param>
        /// <param name="iIntvalTm"></param>　送信間隔
        public void Async_Protocol_Send_AllDat ( List<string> list_str_src, ref SerialPort refSeriPrt, int iIntvalTm) {
            //    enErrCode_ enret = enErrCode_.en_success;

            // TextBox送信文字列から送信用文字列(ASCII)を生成したもの
            List<string> list_str_ascii_senddat = new List<string> ( );

            // 送信文字をListに埋めていく
            for (int i = 0; i < list_str_src.Count(); i++) {
                // 中断
                if (bCommEndFlg)
                    throw new CExcept_(enErrCode_.en_err_comm_interrupt);


                string strsenddat = list_str_src[i];

                // ASCIIタイプ
              //  if (enSendType == CommTestTool.Form1.enSendType.en_SendType_Ascii) {
                    // byte bdat;
                    switch (strsenddat) {
                        case "ENQ": {
                                string strbuf = null;
                                strbuf = System.Text.Encoding.ASCII.GetString(ENQ);
                                list_str_ascii_senddat.Add(strbuf);
                                break;
                            }

                        case "ACK": {
                                string strbuf = null;
                                strbuf = System.Text.Encoding.ASCII.GetString(ACK);
                                list_str_ascii_senddat.Add(strbuf);
                                break;
                            }

                        case "EOT": {
                                string strbuf = null;
                                strbuf = System.Text.Encoding.ASCII.GetString(EOT);
                                list_str_ascii_senddat.Add(strbuf);
                                break;
                            }

                        case "CAN": {
                                string strbuf = null;
                                strbuf = System.Text.Encoding.ASCII.GetString(CAN);
                                list_str_ascii_senddat.Add(strbuf);
                                break;
                            }

                        case "CR": {
                                string strbuf = null;
                                strbuf = System.Text.Encoding.ASCII.GetString(CR);
                                list_str_ascii_senddat.Add(strbuf);
                                break;

                            }
                        case "LF": {
                                string strbuf = null;
                                strbuf = System.Text.Encoding.ASCII.GetString(LF);
                                list_str_ascii_senddat.Add(strbuf);
                                break;
                            }

                        // 自作送信文字列
                        default: {
                                string strbuf = null;

                                byte[] bAsciiDat = null;          // 入力された送信データ
                                bAsciiDat = CCommUtility.GetInstance().StringToAscii_(strsenddat);
                                strbuf = System.Text.Encoding.ASCII.GetString(bAsciiDat);
                                list_str_ascii_senddat.Add(strbuf);
                                break;
                            }
                    }
                }
       //     }
              

            bAsyncRecvContinueFlg = false;
            foreach ( string strsenddat in list_str_ascii_senddat ) {

                // 中断
                if ( bCommEndFlg )
                    throw new CExcept_ ( enErrCode_.en_err_comm_interrupt );

                // 送信バッファクリア
                refSeriPrt.DiscardOutBuffer ( );


                // 受信バッファと同期させる
                DateTime st_time = DateTime.Now;
                while (true) {
                    if (!bAsyncRecvContinueFlg) {
                        break;
                    }

                    // 中断
                    if (bCommEndFlg)
                        throw new CExcept_(enErrCode_.en_err_comm_interrupt);

                    // add 151111
                    // タイムアウト
                    DateTime now_tm = DateTime.Now;
                    TimeSpan span = now_tm - st_time;

                    if (span.Seconds > 5) {
                        break;
                    }
                    // add end

                    //待機
                    System.Threading.Thread.Sleep(consTM_5MS);
                }

                // 送信
                refSeriPrt.Write ( strsenddat ); //送信

                bAsyncRecvContinueFlg = true;       // 受信待ちフラグON


                // 送信文字をFormに投げる
                cnt_mainform.Invoke ( eveCommGenePurPosApi, new Object [] { "→ " + SprSgm.CUtility.ChgUnicodeToAscii_ForRecvDat ( strsenddat ) + "\r\n" } );


                //待機
                System.Threading.Thread.Sleep ( iIntvalTm );
            }
        }


        /// <summary>
        /// すべての文字列送信
        /// </summary>
        /// <param name="list_str_src"></param>
        /// <param name="refSeriPrt"></param>
        /// <param name="iIntvalTm"></param>　送信間隔
        public void Async_Protocol_Send_AllDat_BYTE(List<string> list_str_src, ref SerialPort refSeriPrt, int iIntvalTm) {
            //    enErrCode_ enret = enErrCode_.en_success;

            // TextBox送信文字列から送信用文字列(ASCII)を生成したもの
            List<string> list_str_ascii_senddat = new List<string>();

            bAsyncRecvContinueFlg = false;
         foreach (string strsenddat in list_str_src) {

                // 中断
                if (bCommEndFlg)
                    throw new CExcept_(enErrCode_.en_err_comm_interrupt);

                // 送信バッファクリア
                refSeriPrt.DiscardOutBuffer();


                // 受信バッファと同期させる
                DateTime st_time = DateTime.Now;
                while (true) {
                    if (!bAsyncRecvContinueFlg) {
                        break;
                    }

                    // 中断
                    if (bCommEndFlg)
                        throw new CExcept_(enErrCode_.en_err_comm_interrupt);

                    // add 151111
                    // タイムアウト
                    DateTime now_tm = DateTime.Now;
                    TimeSpan span = now_tm - st_time;

                    if (span.Seconds > 5) {
                        break;
                    }
                    // add end

                    //待機
                    System.Threading.Thread.Sleep(consTM_5MS);
                }

                // 送信
                byte[] bsend;
                bsend = serial_comm.CCommUtility.StringToByte_(strsenddat);
                refSeriPrt.Write(bsend,0,bsend.Length); //送信


                bAsyncRecvContinueFlg = true;       // 受信待ちフラグON


                // 送信文字をFormに投げる
                cnt_mainform.Invoke(eveCommGenePurPosApi, new Object[] { "→ " + SprSgm.CUtility.ChgUnicodeToAscii_ForRecvDat(strsenddat) + "\r\n" });


                //待機
                System.Threading.Thread.Sleep(iIntvalTm);
            }
        }

        /// <summary>
        /// 文字列受信
        /// </summary>
        /// <param name="strRecv"></param>
        public void Sync_Protocol_Recv ( ref byte [] refbytRecv, ref SerialPort refSeriPrt ) {
            List<byte> receivedBuf = new List<byte> ( ); //受信バッファ

            // ACKの受信
            // 応答パケット(同期通信)
            while ( true ) {
                if ( refSeriPrt.BytesToRead > 0 ) {
                    // 1バイト受信してバッファに格納
                    receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );

                    break;
                }
                else if ( bCommEndFlg ) {       // 通信中断確認
                    throw new CExcept_ ( enErrCode_.en_err_comm_interrupt );
                }

                //待機
                System.Threading.Thread.Sleep ( consTM_5MS );
            }

            refbytRecv = receivedBuf.ToArray ( );
        }

        ///// <summary>
        ///// 非同期文字列受信
        ///// </summary>
        //public void ASync_Protocol_Recv ( ) {
        //    //! 受信データを読み込む.
        //    string data = m_serialPort.ReadExisting ( );

        //    // 受信文字をFormに投げる
        //    cnt_mainform.Invoke ( eveCommGenePurPosApi, new Object [] { data } );

        //    //! 受信したデータをテキストボックスに書き込む.
        //    //        Invoke ( new CommTestTool.Form1.Delegate_RcvDataToTextBox ( TxtBx_Recv ), new Object [] { data } );
        //}


        /// <summary>
        /// 非同期受信　イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort_ASyncDataReceived ( object sender, System.IO.Ports.SerialDataReceivedEventArgs e ) {
            try {
                //! シリアルポートをオープンしていない場合、処理を行わない.
                if ( m_serialPort.IsOpen == false ) {
                    return;
                }

                string strrecvdat = null;

                List<string> list_recv_buf = new List<string> ( );


                // 受信バッファが空になるまで受信
                while ( true ) {
                    //byte brecvbuf = null;

                    if ( m_serialPort.BytesToRead == 0 ) {
                        break;
                    }

                    strrecvdat = m_serialPort.ReadExisting();
    
                    list_recv_buf.Add ( strrecvdat );

                    System.Threading.Thread.Sleep ( consTM_5MS );

                }

                string [] str1array = list_recv_buf.ToArray ( );

                // 受信文字をFormに投げる
                cnt_mainform.Invoke ( eveCommGenePurPosApi, new Object [] { "← " + SprSgm.CUtility.ChgUnicodeToAscii_ForRecvDat ( SprSgm.CUtility.ChgStringAryToString ( str1array ) ) + "\r\n" } );

                bAsyncRecvContinueFlg = false;
            }
            catch ( Exception ex ) {
                MessageBox.Show ( ex.Message );
            }
        }
    }
}


//    /// <summary>
//    /// 受信データ文字をASCIIで返す ( 0x05⇒ENQと文字変換する)
//    /// </summary>
//    private string ChgUnicodeToAscii_ForRecvDat ( string str_unicode ) {

//        byte [] brecvtest = System.Text.Encoding.ASCII.GetBytes ( str_unicode );
//        string strrecvdat = null;
//        for ( int i = 0; i < brecvtest.Length; i++ ) {
//            switch ( brecvtest [i] ) {
//                case 0x06:
//                    strrecvdat += "ACK";
//                    break;
//                case 0x04:
//                    strrecvdat += "EOT";
//                    break;
//                case 0x05:
//                    strrecvdat += "ENQ";
//                    break;
//                case 0x02:
//                    strrecvdat += "(STX)";
//                    break;
//                case 0x03:
//                    strrecvdat += "(ETX)";
//                    break;
//                default:
//                    strrecvdat += System.Text.Encoding.ASCII.GetString ( brecvtest, i, 1 );
//                    break;
//            }
//        }

//        return strrecvdat;
//    }
//}
//}