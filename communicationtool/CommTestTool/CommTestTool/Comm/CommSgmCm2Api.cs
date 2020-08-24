using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;

using System.IO;
using System.IO.Ports;
//using System.ComponentModel;


using namespaceErrCode;

namespace serial_comm
{


    /// <summary>
    /// ロボット通信API
    /// </summary>
    class CCommSgmCm2_Api
    {
        const int consTM_5MS = 5;           // 通信処理間タイマー値

        byte [] ENQ = { 0x05 };
        byte [] EOT = { 0x04 };
        byte [] ACK = { 0x06 };
        byte [] CAN = { 0x18 };
        byte [] ETX = { 0x03 };

        //const byte[] byACK ={ 0x06};

        //private CSerialComm m_seriCur;      // カレントのCSerialComm

        public SerialPort m_serialPort; //通信コンポーネント本体
  //      private DateTime endTime;    //タイマーイベント用

        byte [] bRecvBuf = new byte [1024];     // 受信バッファ

        public static  bool bCommEndFlg = false;                // Communicationの中断フラグ
        
        // コンストラクタ
        public CCommSgmCm2_Api ( ) {
            m_serialPort = new SerialPort ( );
            //m_seriCur = new CSerialComm ( );

            m_serialPort.ReadBufferSize = 1024;     // 受信バッファサイズ

    //      this.m_serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);          //受信デリゲート
            m_serialPort.ReadTimeout = 500;             // TimeOut [ミリsec]
            m_serialPort.WriteTimeout = 500;
        }

        //// シリアル通信　データ　をカレント情報として設定
        //public void Set_SerialComm ( serial_comm.CSerialComm m_seridat ) {
        //    m_seriCur = m_seridat;
        //}

        //// 別スレッドでタイマー待ち
        //private void timer_action ( ) {
        //    Thread.Sleep ( 3000 );
        //}


        /// <summary>
        /// 通信プロトコル throw CErrCode型
        /// </summary>
        public void Sync_Protocol_Send_DL ( string str_src, ref SerialPort refSeriPrt ) {
            enErrCode_ enret = enErrCode_.en_success;
            List<byte> receivedBuf = new List<byte> ( ); //受信バッファ
            string strSendDat = null;   //rs232cで送信する文字列
        //    ThreadStart thd_deleg = new ThreadStart ( timer_action);
      //      Thread tm_thd = new Thread ( thd_deleg );
            bCommEndFlg = false;    // 通信中断フラグ初期化

            // 受信/送信バッファクリア
            refSeriPrt.DiscardInBuffer ( );
            refSeriPrt.DiscardOutBuffer ( );

            // ENQ発行
            refSeriPrt.Write ( System.Text.Encoding.ASCII.GetString ( ENQ ) ); //送信

            //待機
            System.Threading.Thread.Sleep ( consTM_5MS );

            // ACKの受信
            // 応答パケット(同期通信)
            while ( true ) {
                if ( refSeriPrt.BytesToRead > 0 ) {
                    // 1バイト受信してバッファに格納
                    //benddat = ( byte ) refSeriPrt.ReadByte ( );
                    receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );

                    ////待機
                    //System.Threading.Thread.Sleep ( consTM_5MS );

                    break;
                }
                else if ( bCommEndFlg ) {       // 通信中断確認
                    throw new CExcept_ ( enErrCode_.en_err_comm_interrupt );
                }
                    //待機
                    System.Threading.Thread.Sleep ( consTM_5MS );
                
            }

            //// 応答パケット(同期通信)
            //    while ( refSeriPrt.BytesToRead > 0 ) {
            //        byte brevbuf = ( byte ) refSeriPrt.ReadByte ( );
            //        receivedBuf.Add ( brevbuf );
            //        if ( brevbuf == ACK[0] )       // 終了文字
            //            break;
            //    }

            // ACK受信確認
            if (receivedBuf.Count() <0 ||
                receivedBuf [0] != 0x06 ) {
                Sync_ErrSeq2 ( ref refSeriPrt );        // Error 2
                throw new CExcept_ ( enErrCode_.en_err_recv_ack );
            }


            // 通信コマンド生成
            if ((enret = CCommUtility.GetInstance().Create_Send_Dat(str_src, ref strSendDat, false, false, false, false, false, false, false, CommTestTool.Form1.enSendType.en_SendType_Hex)) != enErrCode_.en_success) {
                throw new CExcept_ ( enret );
            }

            receivedBuf.Clear ( );

            // 送信
            refSeriPrt.Write ( strSendDat ); //送信

            //待機
            System.Threading.Thread.Sleep ( consTM_5MS );

            // 応答パケット(同期通信)
            int irecvsz = 0;     // BytesToReadのサイズ
            while ( true ) {
                if ( ( irecvsz = refSeriPrt.BytesToRead ) > 0 ) {
                    byte [] bbuf = new byte [irecvsz];

                    refSeriPrt.Read ( bbuf, 0, bbuf.Length );

                    foreach ( byte v in bbuf ) {
                        receivedBuf.Add ( v );
                    }
                    //           receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );         // バイトで取得しない

                    if ( receivedBuf [receivedBuf.Count ( )-1] == 0x03 )
                        break;

                }
                else if ( bCommEndFlg ) {       // 通信中断確認
                    throw new CExcept_ ( enErrCode_.en_err_comm_interrupt );
                }

                //待機
                System.Threading.Thread.Sleep ( consTM_5MS );
            }


            // delete 150708
            //// A0の受信
            //// 応答パケット(同期通信)
            //while ( refSeriPrt.BytesToRead > 0 ) {
            //    // 1バイト受信してバッファに格納
            //    //benddat = ( byte ) refSeriPrt.ReadByte ( );
            //    receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );

            //    // タイムアウトは上位でcatchしてくれ

            //    //待機
            //    System.Threading.Thread.Sleep ( consTM_5MS );
            //}


            // A0受信確認
            if ( receivedBuf.Count ( ) < 4 ||
                receivedBuf [4] != 0x30 ) {
                Sync_ErrSeq2 ( ref refSeriPrt );        // Error 2
                throw new CExcept_ ( enErrCode_.en_err_recv_a0 );
            }

            // EOT送信
            refSeriPrt.Write ( System.Text.Encoding.ASCII.GetString ( EOT ) ); //送信
        }

        /// <summary>
        /// エラーシーケンス　２
        /// </summary>
        private void Sync_ErrSeq2 ( ref SerialPort refSeriPrt ) {

            // CAN送信
            refSeriPrt.Write ( System.Text.Encoding.ASCII.GetString ( CAN ) ); //送信

            //待機
            System.Threading.Thread.Sleep ( consTM_5MS );

            // EOT送信
            refSeriPrt.Write ( System.Text.Encoding.ASCII.GetString ( EOT ) ); //送信
        }


        /// <summary>
        /// 通信プロトコル throw CErrCode型
        /// </summary>
        public void Sync_Protocol_Send_UP ( string str_src, ref byte[] refbyteRecvDat, int iszRecvbuf, ref SerialPort refSeriPrt ) {
            enErrCode_ enret = enErrCode_.en_success;
            List<byte> receivedBuf = new List<byte> ( ); //受信バッファ
            string strSendDat = null;   //rs232cで送信する文字列
            bCommEndFlg = false;    // 通信中断フラグ初期化

         //   string strRecvDat = null;   // rs232cで受信した文字列
            //bool bendflg = false;
            //byte benddat = 0x00;


            // 受信/送信バッファクリア
            refSeriPrt.DiscardInBuffer ( );
            refSeriPrt.DiscardOutBuffer ( );

            // ENQ発行
            refSeriPrt.Write ( System.Text.Encoding.ASCII.GetString ( ENQ ) ); //送信

            //待機
            System.Threading.Thread.Sleep ( consTM_5MS );


            // ACKの受信
            // 応答パケット(同期通信)
            while ( true ) {
                if ( refSeriPrt.BytesToRead > 0 ) {
                    // 1バイト受信してバッファに格納
                    //benddat = ( byte ) refSeriPrt.ReadByte ( );
                    receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );

                    ////待機
                    //System.Threading.Thread.Sleep ( consTM_5MS );

                    break;
                }
                else if ( bCommEndFlg ) {       // 通信中断確認
                    throw new CExcept_ ( enErrCode_.en_err_comm_interrupt );
                }

                //待機
                System.Threading.Thread.Sleep ( consTM_5MS );

            }

            // ACK受信確認
            if (receivedBuf.Count() <0 || 
                receivedBuf [0] != 0x06 ) {
                throw new CExcept_ ( enErrCode_.en_err_recv_ack );
            }

            // 通信コマンド生成
            if ( ( enret = CCommUtility.GetInstance().Create_Send_Dat ( str_src, ref strSendDat, false,false,false,false,false,false,false, CommTestTool.Form1.enSendType.en_SendType_Hex ) ) != enErrCode_.en_success ) {
                throw new CExcept_ ( enret );
            }

            receivedBuf.Clear ( );
            bRecvBuf.Initialize ( );

            // 送信
            refSeriPrt.Write ( strSendDat ); //送信


            //待機
            System.Threading.Thread.Sleep ( consTM_5MS );
            //bendflg = false;

            // A0の受信
            // 応答パケット（同期通信)
            int irecvsz = 0;     // BytesToReadのサイズ
            while ( true ) {
                if ( ( irecvsz = refSeriPrt.BytesToRead ) > 0 ) {
                    byte [] bbuf = new byte [irecvsz];

                    refSeriPrt.Read ( bbuf, 0, bbuf.Length );

                    foreach ( byte v in bbuf ) {
                        receivedBuf.Add ( v );
                    }
                    //           receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );         // バイトで取得しない

                    if ( receivedBuf [receivedBuf.Count ( )-1] == 0x03 )
                        break;
                }
                
                if ( bCommEndFlg ) {       // 通信中断確認
                    throw new CExcept_ ( enErrCode_.en_err_comm_interrupt );
                }

                //待機
                System.Threading.Thread.Sleep ( consTM_5MS );
            }

            // delete 150708
           // // 応答パケット(同期通信)
           // while ( refSeriPrt.BytesToRead > 0){
           ////     && !bendflg ) {
           //     // 1バイト受信してバッファに格納
           //     //benddat = ( byte ) refSeriPrt.ReadByte ( );
           //     receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );

           //     // タイムアウトは上位でcatchしてくれ
           //     //if ( benddat == 0x03 )
           //     //    bendflg = true;

           //     //待機
           //     System.Threading.Thread.Sleep ( consTM_5MS );
           // }


            // A0受信確認
            if ( receivedBuf.Count() <4 ||
                receivedBuf [4] != 0x30 ) {
                throw new CExcept_ ( enErrCode_.en_err_recv_a0 );
            }


            // ACK発行
            refSeriPrt.Write ( System.Text.Encoding.ASCII.GetString ( ACK ) ); //送信

            receivedBuf.Clear ( );

            //待機
            System.Threading.Thread.Sleep ( consTM_5MS );


            // 応答パケット(同期通信)
         //   int irecvsz = 0;     // BytesToReadのサイズ
            while ( true ) {
                if ( ( irecvsz = refSeriPrt.BytesToRead ) > 0 ) {
                    byte [] bbuf = new byte [irecvsz];

                    refSeriPrt.Read ( bbuf, 0, bbuf.Length );

                    foreach ( byte v in bbuf ) {
                        receivedBuf.Add ( v );
                    }
                    //           receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );         // バイトで取得しない

                    if ( receivedBuf [receivedBuf.Count ( )-1] == 0x03 )
                        break;
                }

                if ( bCommEndFlg ) {       // 通信中断確認
                    throw new CExcept_ ( enErrCode_.en_err_comm_interrupt );
                }

                //待機
                System.Threading.Thread.Sleep ( consTM_5MS );
            }

            // delete 150708
     //       // 応答パケット(同期通信)
     //       int irecvsz =0;     // BytesToReadのサイズ
     //       while ( (irecvsz =refSeriPrt.BytesToRead) > 0 ) {
     //            byte [] bbuf = new byte[irecvsz];

     //            refSeriPrt.Read ( bbuf, 0, bbuf.Length );
    
     //           foreach ( byte v in bbuf ) {
     //               receivedBuf.Add ( v );
     //           }
     ////           receivedBuf.Add ( ( byte ) refSeriPrt.ReadByte ( ) );         // バイトで取得しない

     //           //待機
     //           System.Threading.Thread.Sleep ( consTM_5MS );
     //       }


            // 受信文字列が、受信文字取得用バッファのサイズに納まるものか確認
            if ( receivedBuf.Count ( ) > iszRecvbuf ) {
                throw new CExcept_ ( enErrCode_.en_err_recv_buff_ovrflow );
            }

            receivedBuf.ToArray().CopyTo ( refbyteRecvDat, 0 );

     
            // EOT送信
            refSeriPrt.Write ( System.Text.Encoding.ASCII.GetString ( EOT ) ); //送信
        }



        //// タイマイベント
        //private void timer_Elapsed ( object sender, System.Timers.ElapsedEventArgs e ) {
        //    if ( DateTime.Now > endTime )
        //        ( ( System.Timers.Timer ) sender ).Stop ( );  // タイマー停止
        //}


        //// 受信デリゲート
        //private void serialPort1_DataReceived ( object sender, System.IO.Ports.SerialDataReceivedEventArgs e ) {
        //    // データ受信イベントの元になったシリアルポートを取得
        //    //byte [] bbuf = new byte [64];
        //    //SerialPort seri = ( SerialPort ) sender;
        //    //// 格納
        //    //int ilen = seri.Read ( , 0, bRecvBuf.Length );

        //    //bRecvBuf.Concat()
        //}
    }
}