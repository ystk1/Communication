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
    public enum enSgmCm2CmdIDType
    {
        en_cmdID_DI,//吐出
        en_cmdID_CST,
        en_cmdID_TblPre, // テーブル圧変更
        en_cmdID_VS, // 負圧設定コマンド
        en_cmdID_LH, // CHカレント設定コマンド
        en_cmdID_UL, // アップロードコマンド
    }

    /// <summary>
    /// コマンド　種類　データベース
    /// </summary>
    class CCmdSgmCm2_DBType_var
    {

        public enSgmCm2CmdIDType enCmdID { get; set; }
        //   public string strCmdName { get; set; }  // コマンド部のみの文字列データ　本当はカプセル化したい

        //        public string strCmdDat { get; set; }       // 実際に送信される文字列（コマンド部+文字列）
    }


    // 吐出コマンド
    class CCmdSgmCm2_DI : CCmdSgmCm2_DBType_var
    {
        public CCmdSgmCm2_DI ( ) {
            enCmdID = enSgmCm2CmdIDType.en_cmdID_DI;
        }

        public string CreateSendDat ( ) {
            return "DI  ";
        }
    }


    // カレントコマンド
    class CCmdSgmCm2_Cur : CCmdSgmCm2_DBType_var
    {
        public int ichno;

        public CCmdSgmCm2_Cur ( int icurno ) {
            enCmdID = enSgmCm2CmdIDType.en_cmdID_LH;
            ichno = icurno;
        }

        public void SetCurNo ( int icurno ) {
            ichno = icurno;
        }

        public string CreateSendDat ( ) {
            return "LH" + ichno.ToString ( "D3" );
        }
    }

    // テーブル圧
    class CCmdSgmCm2_TblPre : CCmdSgmCm2_DBType_var
    {
        public int [] iTblPre;          // 0~9　Indexの圧力


        // コンストラクタ　（送信用) int
        public CCmdSgmCm2_TblPre ( int [] iTblPrebuf ) {
            enCmdID = enSgmCm2CmdIDType.en_cmdID_TblPre;
            iTblPre = new int [10];  // Table数は１０個

            int isz = 0;
            if ( ( isz = iTblPrebuf.Count ( ) ) >= 10 ) {
                for ( int i = 0; i < iTblPre.Length; i++ ) {
                    iTblPre [i] = iTblPrebuf [i];
                }
                    //foreach ( int i in iTblPre ) {
                    //    iTblPre [i] = iTblPrebuf [i];
                    //}
            }
            else {
                for ( int i = 0; i < iTblPre.Length; i++ ) {
                    iTblPre [i] = 10;
                }
                //foreach ( int i in iTblPre ) {
                //    iTblPre [i] = 10;
                //}
            }
        }

        // コンストラクタ （送信用) double
        public CCmdSgmCm2_TblPre ( double [] dTblPrebuf ) {
            enCmdID = enSgmCm2CmdIDType.en_cmdID_TblPre;
            iTblPre = new int [10]; // Table数は１０個

            int isz = 0;
            if ( ( isz = dTblPrebuf.Count ( ) ) > 10 ) {
                //foreach ( int i in iTblPre ) {
                //    iTblPre [i] =(int)( dTblPrebuf [i] * 10.0);        // 10倍の整数値に変換
                //}

                for ( int i = 0; i < iTblPre.Length; i++ ) {
                    iTblPre [i] =(int)(Math.Floor( dTblPrebuf [i] * 10.0 ));        // 10倍の整数値に変換
                }
            }
        }

        // コンストラクタ  （送受信用)
        public CCmdSgmCm2_TblPre ( ) {
            iTblPre = new int [10];     // ダミー
        }

        /// <summary>
        /// テーブルデータにdouble配列をセット
        /// </summary>
        /// <param name="dTblDat"></param>
        /// <returns>成功:0 失敗 -1</returns>
        public int SetTblDat ( double [] dTblPrebuf ) {
            int isz = dTblPrebuf.Count ( );
            int iret = 0;
            if ( isz >= 10 ) {
                for ( int i = 0; i < iTblPre.Length; i++ ) {
                    iTblPre [i] = ( int ) ( dTblPrebuf [i] * 10.0 );        // 10倍の整数値に変換
                }
                    //foreach ( int i in iTblPre ) {
                    //    iTblPre [i] = ( int ) ( dTblPrebuf [i] * 10.0 );        // 10倍の整数値に変換
                    //}
            }
            else
                iret = -1;

            return iret;
        }

        // 送信データ生成
        public string CreateSendDat ( int iIdx ) {
            if ( iIdx < 0 || iIdx > 9 )
                return null;
            else
                return "ST" + iIdx.ToString ( "D1" )
                    + "P" + iTblPre [iIdx].ToString ( "D4" );
        }

    }

    // 負圧
    class CCmdSgmCm2_Vac : CCmdSgmCm2_DBType_var
    {
        public int ivac;                // 負圧は、０より小さい負の値で取り扱う

        public CCmdSgmCm2_Vac ( int ivacbuf ) {
            enCmdID = enSgmCm2CmdIDType.en_cmdID_VS;

            ivac = ivacbuf;
        }

        public string CreateSendDat ( ) {
            return "VS  V-" + Math.Abs(ivac).ToString ( "D4" );                    // ivacは、負の値
   //         return "VS  V-" + ivac.ToString ( "D4" );                 // ivacは、正の値
        }

        public void SetVacDat ( double dvacbuf ) {
            ivac = ( int ) ( dvacbuf * 100.0 );       // vaccumは100倍
        }
    }

    // アップロード
    class CCmdSgmCm2_Ul : CCmdSgmCm2_DBType_var
    {
        public enum en_cmdUiType { enCmdUI_disprm = 1, enCmdUI_cylisz = 2, enCmdUI_tablprm =3 };

        en_cmdUiType en_uitype;
        int ichno;

        public CCmdSgmCm2_Ul ( en_cmdUiType enbuf, int ichbuf ) {
            enCmdID = enSgmCm2CmdIDType.en_cmdID_UL;
            en_uitype = enbuf;
            ichno = ichbuf;
        }

        public void SetCurNo ( int ich ) {
            ichno = ich;
        }

        public string CreateSendDat ( ) {
            string strbuf;
            switch ( en_uitype ) {

                    // バキューム
                case en_cmdUiType.enCmdUI_disprm:
                    {
                    strbuf = "UL" + ichno.ToString ( "D3" ) + "D" + "01";
                        break;
                    }

                    // テーブル
                case en_cmdUiType.enCmdUI_tablprm: {
                    strbuf = "UL" + ichno.ToString ( "D3" ) + "D" + "19";
                        break;
                    }

                default: {
                        strbuf = null;
                        break;
                    }
            }

            return strbuf;
        }


        // テーブルデータをパーサー
        public int RecvParser_tableDat ( byte [] bytSrcRecv, ref double [] refdTbldat, int ibytTbldatSz ) {
            //stx38DA19t,pppp,pppp,pppp,pppp,pppp,pppp,pppp,pppp,pppp,pppp,??etx 

            //int icuridx = 0;// カレントのインデックス
            List<double> dlistans = new List<double> ( );

            int iret = 0;

            string strbuf = Encoding.ASCII.GetString ( bytSrcRecv );
            string [] strpress = strbuf.Split ( ',' );      // 圧力値取得
            Array.Clear ( strpress, 0, 1 ); // 最初の配列削除
            Array.Clear ( strpress, strpress.GetLength ( 0 ) - 1, 1 );// 最後の配列削除

            foreach ( string strbufeach in strpress ) {
                if ( strbufeach != null ) {
                    dlistans.Add ( ( double ) ( int.Parse ( strbufeach ) * 0.1 ) );
                }
            }

            if ( dlistans.Count ( ) != 10 ) {
                iret = -1;
            }
            else {
                int iidx = 0;
                foreach ( double dvaleach in dlistans ) {
                    if ( iidx < ibytTbldatSz )
                        refdTbldat [iidx++] = dvaleach;
                }
            }
            return iret;
        }

        // vaccumをパーサー
        public void RecvParser_vacDat ( byte [] bytVacParserRecv, ref double refvac ) {
            byte [] bytArraybuf = new byte [4];
            Array.Copy ( bytVacParserRecv,
                Array.FindIndex ( bytVacParserRecv, num => num == 0x56 ) +1, //v
                bytArraybuf,
                0,
                bytArraybuf.Length );

            refvac = ( double ) ( int.Parse ( CCommUtility.GetInstance().AsciiToString_ ( bytArraybuf ) ) );
            refvac = -1.0 * refvac*0.01;         // 負圧扱いなので、マイナス値にする

        //    refvac = double.Parse ( bytArraybuf.ToString ( ) );
        }

        public string RecvParser ( byte[] byterecv ) {
            string strbuf = null;

            switch ( en_uitype ) {
                case en_cmdUiType.enCmdUI_disprm: {
                        //                byterecv[]
                        break;
                    }

                default:

                    break;
            }

            return strbuf;
        }
    }
}
