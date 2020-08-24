using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.IO.Ports;


using namespaceErrCode;

namespace serial_comm
{


    class CCommUtility
    {

        // シングルトン
        private static CCommUtility singleton = new CCommUtility();

        // インスタンスを取得
        public static CCommUtility GetInstance() {
            return singleton;
        }


        // チェックサム算出
        public byte CalcChkSum(byte[] byteSendDat) {

            byte bval = 0x00;

            for (int i = 0; i < byteSendDat.Length; i++) {
                bval = (byte)(bval - byteSendDat[i]);
            }

            return bval;
        }

        // チェックサム算出
        public byte[] CalcChkSum(string strSendDat) {
            byte[] byteDat = System.Text.Encoding.ASCII.GetBytes(strSendDat);

            //    byte chksumvval;
            int ichksumbuf = 0x0000000;

            for (int i = 0; i < byteDat.Length; i++) {
                ichksumbuf -= (int)byteDat[i];
            }

            // チェックサム格納
            byte[] bret = new byte[2];

            bret.Initialize();
            //foreach ( int i in bret ) {         // 初期化
            //    bret [i] = 0x00;
            //}

            byte[] bytebuf;
            bytebuf = BitConverter.GetBytes(ichksumbuf);

            bret[0] = bytebuf[0];
            bret[1] = bytebuf[1];

            return bret;
        }

        // 16進文字列をASCII文字に変換
        public Byte[] StringToAscii_(string dat) {
            return Encoding.ASCII.GetBytes(dat);
        }

        // ASCII文字を16進文字列に変換
        public string AsciiToString_(byte[] dat) {
            return Encoding.ASCII.GetString(dat);
        }

        public static List<string> AsciiToStringArray_(string[] bybuf) {
           List<string> retstr;
            string strbuf;
            retstr =  new List<string> ( );
            for (int i = 0; i < bybuf.Length; i++) {
                strbuf = bybuf[i].ToString();
               retstr.Add(strbuf);
            }
            return retstr;
        }

        /// <summary>
        /// １６進数文字列バイト配列変換
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string data) {
            if (data == null)
                throw new ArgumentNullException();
            if (data.Length % 2 == 1)
                data = "0" + data;  // 補正

            var list = new List<byte>();
            for (int i = 0; i < data.Length - 1; i++, i++)
                list.Add(Convert.ToByte(data.Substring(i, 2), 16));
            return list.ToArray();
        }


        /// <summary>
        /// 16進数の文字列からバイト配列を生成します。
        /// </summary>
        /// <param name="str">16進数文字列</param>
        /// <returns>バイト配列</returns>
        public static byte[] StringToByte_(string str) {
            int length = str.Length / 2;
            byte[] bytes = new byte[length];
            int j = 0;
            for (int i = 0; i < length; i++) {
                bytes[i] = Convert.ToByte(str.Substring(j, 2), 16);
                j += 2;
            }
            return bytes;
        }

        // byte変数をＡＳＣＩＩのbyte値に変換
        public byte byteToAscii_(byte bdat) {
            byte byret;
            if (0x00 <= bdat && 0x09 >= bdat)
                byret = (byte)(0x30 + (int)bdat);
            else if (0xA <= bdat && 0x0F >= bdat)
                byret = (byte)(0x41 + (int)(bdat - 0x0a));
            else
                byret = 0x00;

            return byret;
        }



        //// シリアル接続 
        //public void Connect_rs232c ( SerialPort m_prt, ref CSerialComm refcomm ) {

        //    try {
        //        if ( m_prt.IsOpen == true ) {
        //            m_prt.Close ( );
        //        }

        //        // 変数をセットする
        //        m_prt.BaudRate = refcomm.m_baurate.Baurate; // ボーレート
        //        m_prt.DataBits = refcomm.DatBit;    // データビット
        //        m_prt.Parity = refcomm.Parity;  // パリティ
        //        m_prt.StopBits = refcomm.StpBit;    // ストップビット
        //        m_prt.Handshake = refcomm.m_handshake.Handshake; // ハンドシェイク
        //        m_prt.Encoding = refcomm.Encoding; // エンコード
        //    }
        //    catch ( Exception ex ) {

        //    }
        //}

        // 送信文字列生成
        public enErrCode_ Create_Send_Dat(string strSendData, ref string refstrSendDat, bool bstx, bool betx, bool bchksum, bool bstrlen, bool bcr, bool blf, bool bcrc, CommTestTool.Form1.enSendType ensendtype) {
            enErrCode_ enret = enErrCode_.en_success;

            //  string hexDat;
            var listSendDat = new List<byte>();           // 送信データリスト(byte型)


            // ASCII文字系の通信
            if (ensendtype == CommTestTool.Form1.enSendType.en_SendType_Ascii) {
                // 文字列長（2ケタ）有無
                if (bstrlen) {
                    string strlenbuf = strSendData.Length.ToString("X2");
                    byte[] bStrLenBuf = StringToAscii_(strlenbuf);


                    for (int i = 0; i < bStrLenBuf.Length; i++) {
                        listSendDat.Add(bStrLenBuf[i]);
                    }
                }

                byte[] bAsciiDat = null;          // 入力された送信データ
                bAsciiDat = StringToAscii_(strSendData);

                int isz = bAsciiDat.GetLength(0);

                for (int i = 0; i < isz; i++) {
                    listSendDat.Add(bAsciiDat[i]);
                }


                // チェックサムの有無
                if (bchksum) {
                    // チェックサムを計算
                    byte chkdam = CalcChkSum(listSendDat.ToArray());
                    //         byte chkdam = CCommUtility.CalcChkSum ( bAsciiDat );

                    // チェックサム
                    byte[] bytechksumary;
                    bytechksumary = new byte[2];

                    bytechksumary[0] = (byte)((int)(chkdam & 0xF0) >> 4);
                    bytechksumary[1] = (byte)((int)chkdam & 0x0F);


                    isz = bytechksumary.Length;
                    for (int i = 0; i < isz; i++) {
                        listSendDat.Add(byteToAscii_(bytechksumary[i]));
                    }
                }


                //stxの有無
                if (bstx) {
                    //        if ( m_sericomm.m_addstrcommdat.stx ) {
                    byte nstx = 0x02;
                    listSendDat.Insert(0, nstx);
                }

                // etxの有無
                if (betx) {
                    //            if ( m_sericomm.m_addstrcommdat.etx ) {
                    byte netx = 0x03;
                    listSendDat.Add(netx);
                }

                // CR
                if (bcr) {
                    byte ncr = 0x0D;
                    listSendDat.Add(ncr);
                }

                // LF
                if (blf) {
                    byte nlf = 0x0A;
                    listSendDat.Add(nlf);
                }

                // 送信データ配列(byte型)
                byte[] bSendDat = listSendDat.ToArray();

                refstrSendDat = System.Text.Encoding.ASCII.GetString(bSendDat);

            }


                // 16進　直送
            else {

                // CRC
                if (bcrc) {
                    refstrSendDat = strSendData + BitConverter.ToString(calc_crc(StringToByte_(strSendData))).Replace("-","");
                    //    ushbuf.ToString();
                }
            }

            return enret;
        }


        // CRCコード算出
      byte[]  calc_crc(byte[] byteSendDat) {
    //    ushort calc_crc(byte[] byteSendDat) {
            int ilength = byteSendDat.Length;
            byte[]  bret = new byte[2];

            ushort crc = 0xFFFF;
            int i, j;
            byte carrayFlag;
            for (i = 0; i < ilength; i++) {
                crc ^= byteSendDat[i];
                for (j = 0; j < 8; j++) {
                    carrayFlag = (byte)(crc & 1);
                    crc = (ushort)(crc >> 1);
                    if (carrayFlag != 0x00) {
                        crc ^= 0xA001;
                    }
                }
            }

            // エンディアン入れ替え
            bret[0] = (byte)(crc & 0xFF);
            bret[1] = (byte)((crc & 0xFF00)>>8);

            return bret;
            }
    }
}


    //    /// <summary>
    //    /// 送信文字列作成
    //    /// </summary>
    //    /// <param name="strbuf"></param>
    //    /// <param name="strSendDat"></param>
    //    /// <returns></returns>
    //    public static void Create_Send_Dat ( string strbuf, ref string strSendDat, bool bstxflg, bool betxflg ) {
    //        try {
    //            //  enErrCode_ enret = 0;
    //            //     string data = TxtBx_Send.Text;

    //            //  string hexDat;
    //            var listSendDat = new List<byte> ( );           // 送信データリスト(byte型)

    //            byte [] bAsciiDat = null;          // 入力された送信データ
    //            bAsciiDat = CCommUtility.StringToAscii_ ( strbuf );

    //            int isz = bAsciiDat.GetLength ( 0 );

    //            for ( int i = 0; i < isz; i++ ) {
    //                listSendDat.Add ( bAsciiDat [i] );
    //            }

    //            // チェックサムを計算
    //            byte chkdam = CCommUtility.CalcChkSum ( bAsciiDat );

    //            // チェックサム
    //            byte [] bytechksumary;
    //            bytechksumary = new byte [2];

    //            bytechksumary [0] = ( byte ) ( ( int ) ( chkdam & 0xF0 ) >> 4 );
    //            bytechksumary [1] = ( byte ) ( ( int ) chkdam & 0x0F );

    //            isz = bytechksumary.Length;
    //            for ( int i = 0; i < isz; i++ ) {
    //                listSendDat.Add ( CCommUtility.byteToAscii_ ( bytechksumary [i] ) );
    //            }

    //            //stxの有無
    //            if ( bstxflg ) {
    //                byte nstx = 0x02;
    //                listSendDat.Insert ( 0, nstx );
    //            }

    //            // etxの有無
    //            if ( betxflg ) {
    //                byte netx = 0x03;
    //                listSendDat.Add ( netx );
    //            }

    //            // 送信データ配列(byte型)
    //            byte [] bSendDat = listSendDat.ToArray ( );

    //            strSendDat = System.Text.Encoding.ASCII.GetString ( bSendDat );

    //        }
    //        catch ( CExcept_ excp ) {
    //            throw new CExcept_ ( enErrCode_.en_err_create_send_dat );
    //        }
    //    }
    //}
//}