using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace namespaceErrCode
{

    // エラーコード
    enum enErrCode_
    {
        en_success = 0,
        en_err_create_send_dat,                       // 送信データ生成失敗
        en_err_none_send_str,                          // 送信するメッセージがない
        en_err_FilePath_xlsx,                           // xlsxがファイルパスにない
        en_err_recv_ack,                                // ACK信号の受信に失敗
        en_err_recv_a0,                                 // A0受信エラー
        en_err_recv_buff_ovrflow,                   // 受信文字取得バッファがオーバーフローした

        en_err_maxlimit_ovr,                          // 上限値を超えるパラメタ検知
        en_err_minlimit_ovr,                            // 下限値を下回るパラメタ検知

        en_err_dishonest_val,                           // 不正値
        en_err_retry_receive,                           // 受信に失敗しました。
        en_err_grid_null,                                   // DataGridViewのセルにnullがあった
        en_err_tabledata_maxlimit_ovr,              // Tableデータ　上限値over
        en_err_tabledata_minlimit_ovr,              // 下限値
        en_err_vac_maxlimit_ovr,                       // vac 負圧        上限値over
        en_err_vac_minlimit_ovr,                        // 下限値          
        en_err_comm_interrupt,                          // 通信を中断
    };


    /// <summary>
    /// 例外処理　
    /// </summary>
    class CExcept_ : System.ApplicationException
    {

        private enErrCode_ envar { set; get; }      // カレントのエラー
        private string strAdded = null;               // エラー付加文字


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="e"></param>
        public CExcept_ ( enErrCode_ e ) {
            envar = e;
        }

        public CExcept_ ( enErrCode_ e, string strAddBuf ) {
            envar = e;
            strAdded = strAddBuf;
        }

        public override string Message {
            get {
                return GetString ( );
                //                return base.Message;
            }
        }


        /// <summary>
        /// エラー文字列取得
        /// </summary>
        /// <returns></returns>
        public string GetString ( ) {
            string strbuf = null;

            switch ( envar ) {
                case enErrCode_.en_err_create_send_dat:
                    strbuf = "送信に失敗しました。";
                    break;

                case enErrCode_.en_err_FilePath_xlsx:
                    strbuf = "Fileパスが不正な値です。";
                    break;

                case enErrCode_.en_err_maxlimit_ovr:
                    strbuf = strAdded + "上限値を上回る値を検出しました。";
                    break;

                case enErrCode_.en_err_minlimit_ovr:
                    strbuf = strAdded + "下限値を下回る値を検出しました。";
                    break;

                case enErrCode_.en_err_none_send_str:
                    strbuf = "送信文字列がありません。";
                    break;

                case enErrCode_.en_err_recv_a0:
                    strbuf = "A0の受信を確認できませんでした";
                    break;

                case enErrCode_.en_err_recv_ack:
                    strbuf = "ACKの受信を確認できませんでした。";
                    break;

                case enErrCode_.en_err_recv_buff_ovrflow:
                    strbuf = "バッファオーバーフローを検出しました。";
                    break;

                case enErrCode_.en_err_dishonest_val:
                    strbuf = strAdded + "不正な値を検出しました。";
                    break;

                case enErrCode_.en_err_retry_receive:
                    strbuf = strAdded + "再度受信してください。";
                    break;

                case enErrCode_.en_err_grid_null:
                    strbuf = "値が空のセルを検出しました。";
                    break;

                case enErrCode_.en_err_tabledata_maxlimit_ovr:
                    strbuf = "Tableの上限値を超える値を検出しました。";
                    break;

                case enErrCode_.en_err_tabledata_minlimit_ovr:
                    strbuf = "Tableの下限値を下回る値を検出しました。";
                    break;

                case enErrCode_.en_err_vac_maxlimit_ovr:
                    strbuf = "負圧の上限値を超える値を検出しました。";
                    break;

                case enErrCode_.en_err_vac_minlimit_ovr:
                    strbuf = "負圧の下限値を下回る値を検出しました。";
                    break;

                case enErrCode_.en_err_comm_interrupt:
                    strbuf = "通信処理を中断しました。";
                    break;

                default:
                    strbuf = base.Message;
                    break;

            }
            return strbuf;
        }
    }
}
