using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using namespaceErrCode;

namespace SprSgm
{
    static class CUtility
    {
        /// <summary>
        /// 値をチェック throw en_err_maxlimit_ovr/en_err_minlimit_ovr
        /// </summary>
        /// <param name="dval"></param>
        /// <param name="dmax"></param>
        /// <param name="dmin"></param>
        public static void ChkMaxMin ( double dval, double dmax, double dmin ) {
            if ( dval > dmax )
                throw new CExcept_ ( enErrCode_.en_err_maxlimit_ovr );
            if ( dval < dmin )
                throw new CExcept_ ( enErrCode_.en_err_minlimit_ovr );
        }


        /// <summary>
        /// string[]をstringに変換する
        /// </summary>
        /// <param name="strarry"></param>
        /// <returns></returns>
        public static string ChgStringAryToString ( string [] strarry ) {
            // string[] をstringに変換
            string str1buf = null;
            foreach ( string streacn in strarry ) {
                str1buf += streacn;
            }

            return str1buf;
        }


        /// <summary>
        /// 受信データ文字をASCIIで返す ( 0x05⇒ENQと文字変換する)
        /// </summary>
        public static string ChgUnicodeToAscii_ForRecvDat ( string str_unicode ) {

            byte [] brecvtest = System.Text.Encoding.ASCII.GetBytes ( str_unicode );
            string strrecvdat = null;
            for ( int i = 0; i < brecvtest.Length; i++ ) {
                switch ( brecvtest [i] ) {
                    case 0x06:
                        strrecvdat += "(ACK)";
                        break;
                    case 0x04:
                        strrecvdat += "(EOT)";
                        break;
                    case 0x05:
                        strrecvdat += "(ENQ)";
                        break;
                    case 0x02:
                        strrecvdat += "(STX)";
                        break;
                    case 0x03:
                        strrecvdat += "(ETX)";
                        break;
                    case 0x20:
                        strrecvdat += "□";
                        break;
                    case 0x18:
                        strrecvdat += "(CAN)";
                        break;
                    default:
                        strrecvdat += System.Text.Encoding.ASCII.GetString ( brecvtest, i, 1 );
                        break;
                }
            }

            return strrecvdat;
        }
    }
}