using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.IO.Ports;
using System.IO;
using namespaceErrCode;
using serial_comm;
using NAME_SPACE_SYSDAT;

namespace CommTestTool
{
    public partial class Form1 : Form
    {

        // コントロール配列
        private Button[] btnCtrlPtrAry;
        private ComboBox[] cmbbxCtrlPtrAry;
        private TextBox[] txtbxCtrlPtrAry;

        public enum enRunStatus { en_autorun = 0, en_idle_ascii, en_none_connect, en_idle_uni };  //　本アプリの状態

        public delegate void Delegate_RcvDataToTextBox(string data);         // 受信文字表示用デリゲート

        private string strSendDat_1Row = null;               // 1行送信バッファ

        //public enSendType en_sendtype { set; get;}          // アクセサ


        //= enSendType.en_SendType_Hex;            // 送信タイプ ASCII or HEX



        //        public enum enTxtBxCtrlIdx {  en_status}
        public enum enBtnCtrlPtrIdx
        {
            en_idx_btn_connect = 0,
            en_idx_btn_send,
            en_idx_btn_runstop,
            en_idx_btn_enq,
            en_idx_btn_ack,
            en_idx_btn_eot,
            en_idx_btn_can,
            en_idx_btn_add,
            en_idx_btn_txtbx1,
            en_idx_btn_txtbx2,

            en_idx_btn_createCmd,
            en_idx_btn_clear,

            en_idx_btn_num
        };
        public enum enComboBoxPtrIdx
        {
            en_idx_cmbbx_bps = 0,
            en_idx_cmbbx_commno,
            en_idx_cmbbx_num
        };
        public enum enTxtBxPtrIdx
        {
            en_idx_txtbx_intvaltm = 0,
            en_idx_txtbx_senddat,
            en_idx_txtbx_num
        }

        // 送信タイプ
        public enum enSendType
        {
            en_SendType_Ascii = 0,
            en_SendType_Hex
        }

        // 通信コンポーネント本体
        private CCommGeneralPurposeApi m_serialPort1;                     // SgmCm2通信


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1() {
            InitializeComponent();

            m_serialPort1 = new CCommGeneralPurposeApi(this);            // SgmCm2通信

            // コントロール配列生成
            btnCtrlPtrAry = new Button[(int)enBtnCtrlPtrIdx.en_idx_btn_num];
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_connect] = btnConnect;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_send] = btnSend;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_runstop] = btnRunStp;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_enq] = btnEnq;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_ack] = btnAck;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_eot] = btnEot;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_can] = btnCan;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_add] = btnSendDatAdd;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_txtbx1] = btnSendDatDelete;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_txtbx2] = btnRecvDatDelete;

            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_createCmd] = btnSendDatCreate;
            btnCtrlPtrAry[(int)enBtnCtrlPtrIdx.en_idx_btn_clear] = btnSendDatClr;

            cmbbxCtrlPtrAry = new ComboBox[(int)enComboBoxPtrIdx.en_idx_cmbbx_num];
            cmbbxCtrlPtrAry[(int)enComboBoxPtrIdx.en_idx_cmbbx_bps] = cmbComBps;
            cmbbxCtrlPtrAry[(int)enComboBoxPtrIdx.en_idx_cmbbx_commno] = cmbCommNo;

            txtbxCtrlPtrAry = new TextBox[(int)enTxtBxPtrIdx.en_idx_txtbx_num];
            txtbxCtrlPtrAry[(int)enTxtBxPtrIdx.en_idx_txtbx_intvaltm] = txtbxIntvalTm;
            txtbxCtrlPtrAry[(int)enTxtBxPtrIdx.en_idx_txtbx_senddat] = txtbxSendStr;

            m_serialPort1.eveCommGenePurPosApi += new CCommGeneralPurposeApi.MyEventHandler(RcvDataToTextBox);       // 受信データを表示

        }


        /// <summary>
        /// シリアル通信　設定Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Serial_Comm_Load(object sender, EventArgs e) {

            //! 利用可能なシリアルポート名の配列を取得する.
            string[] PortList = SerialPort.GetPortNames();

            if (PortList.Length == 0) {
                // 接続COMなし
                string strTxt = "接続されたCOM端子なし";
                MessageBox.Show(strTxt);

                btnConnect.Enabled = false;
            }

            cmbCommNo.Items.Clear();

            //! シリアルポート名をコンボボックスにセットする.
            foreach (string PortName in PortList) {
                cmbCommNo.Items.Add(PortName);
            }
            if (cmbCommNo.Items.Count > 0) {
                cmbCommNo.SelectedIndex = 0;
            }

            cmbComBps.Items.Clear();

            // ボーレート ComboBox
            cmbComBps.Items.Add("57600");
            cmbComBps.Items.Add("38400");
            cmbComBps.Items.Add("19200");
            cmbComBps.Items.Add("9600");
            cmbComBps.SelectedIndex = Properties.Settings.Default.iiniBpsCurVal;
            //            cmbComBps.SelectedIndex = 1;


            // ストップビット
            foreach (string a in Enum.GetNames(typeof(StopBits))) {
                this.cmbbxStpBit.Items.Add(a);
            }
            //cmbbxStpBit.Items.Add("NONE");
            //cmbbxStpBit.Items.Add("1");
            //cmbbxStpBit.Items.Add("2");
            //cmbbxStpBit.Items.Add("1.5");
            cmbbxStpBit.SelectedIndex = 1;

            // ハンドシェイク
            foreach (string a in Enum.GetNames(typeof(Handshake))) {
                this.cmbbxHandShake.Items.Add(a);
            }
            //cmbbxHandShake.Items.Add("NONE");
            //cmbbxHandShake.Items.Add("XonXoff");
            //cmbbxHandShake.Items.Add("RTS");
            //cmbbxHandShake.Items.Add("RTS/XonXoff");
            cmbbxHandShake.SelectedIndex = 0;


            // パリティ
            foreach (string a in Enum.GetNames(typeof(Parity))) {
                this.cmbCommParity.Items.Add(a);

            }
            //cmbCommParity.Items.Add("NONE");
            //cmbCommParity.Items.Add("ODD");
            //cmbCommParity.Items.Add("EVEN");
            cmbCommParity.SelectedIndex = 0;


            // エンコード
            cmbCommEnc.Items.Add("ASCII");
            cmbCommEnc.Items.Add("UNICODE");
            cmbCommEnc.SelectedIndex = 0;

            // データbit
            cmbCommDatBit.Items.Add("8");
            cmbCommDatBit.Items.Add("7");
            cmbCommDatBit.SelectedIndex = 0;





        }

        /// <summary>
        /// 送信する文字列を追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendDatCreate_Click(object sender, EventArgs e) {
            try {
                string strbuf = null;
                CCommUtility.GetInstance().Create_Send_Dat(txtbxSendStr.Text, ref strbuf, chkbx_stx.Checked, chkbx_etx.Checked, chkbtnCheckSum.Checked, chkbtnStrlen.Checked, chkbxCR.Checked, chkbxLF.Checked, chkCRC.Checked, enSendType.en_SendType_Hex);
                strSendDat_1Row += strbuf;
                txt1RowSendDat.Text = SprSgm.CUtility.ChgUnicodeToAscii_ForRecvDat(strSendDat_1Row);

            }
            catch (CExcept_ excp) {
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// 1行送信バッファ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendDatClr_Click(object sender, EventArgs e) {
            try {
                strSendDat_1Row = null;
                txt1RowSendDat.Text = null;
            }
            catch (CExcept_ excp) {
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// 送信する文字列をして、txt1RowSendDatを追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendDatAdd_Click(object sender, EventArgs e) {
            try {

                if (strSendDat_1Row != null) {
                    TxtBx_Send.Text += SprSgm.CUtility.ChgUnicodeToAscii_ForRecvDat(strSendDat_1Row) + "\r\n";
                    //            TxtBx_Send.Text += SprSgm.CUtility.ChgUnicodeToAscii_ForRecvDat(strSendDat) + "\r\n";

                    CSystemDat.GetInstance().m_listCommStr.Add(strSendDat_1Row);
                }
            }
            catch (CExcept_ excp) {
                MessageBox.Show(excp.Message);
            }
        }

        ///// <summary>
        ///// 送信する文字列を追加する
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnSendDatAdd_Click ( object sender, EventArgs e ) {
        //    try {
        //        string strSendDat = null;

        //        CCommUtility.GetInstance ( ).Create_Send_Dat ( txtbxSendStr.Text, ref strSendDat, chkbx_stx.Checked, chkbx_etx.Checked, chkbtnCheckSum.Checked, chkbtnStrlen.Checked, chkbxCR.Checked,chkbxLF.Checked );

        //        TxtBx_Send.Text += SprSgm.CUtility.ChgUnicodeToAscii_ForRecvDat ( strSendDat ) + "\r\n";

        //        CSystemDat.GetInstance ( ).m_listCommStr.Add ( strSendDat );
        //    }
        //    catch ( CExcept_ excp ) {
        //        MessageBox.Show ( excp.Message );
        //    }
        //}

        /// <summary>
        /// 通信接続/切断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e) {
            try {
                SetEnableCtrl(enRunStatus.en_autorun);

                if (m_serialPort1.m_serialPort.IsOpen == true) {
                    //! シリアルポートをクローズする.
                    m_serialPort1.m_serialPort.Close();

                    //! ボタンの表示を[切断]から[接続]に変える.
                    btnConnect.Text = "connect/接続";

                    SetEnableCtrl(enRunStatus.en_none_connect);
                }
                else {
                    //! オープンするシリアルポートをコンボボックスから取り出す.
                    m_serialPort1.m_serialPort.PortName = cmbCommNo.SelectedItem.ToString();

                    //! ボーレートをコンボボックスから取り出す.
                    m_serialPort1.m_serialPort.BaudRate = int.Parse(cmbComBps.SelectedItem.ToString());

                    //! ストップビットをセットする. (ストップビット = 1ビット)
                    StopBits enbuf = (StopBits)Enum.ToObject(typeof(StopBits), cmbbxStpBit.SelectedIndex);
                    m_serialPort1.m_serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), this.cmbbxStpBit.SelectedItem.ToString());
           
                    //! フロー制御
                    m_serialPort1.m_serialPort.Handshake = (Handshake)Enum.ToObject(typeof(Handshake), cmbbxHandShake.SelectedIndex);
           
                    // add 160517
                    // パリティ
                    m_serialPort1.m_serialPort.Parity =
                          (System.IO.Ports.Parity)Enum.ToObject(typeof(Parity), cmbCommParity.SelectedIndex);

                    //! 文字コードをセットする.        // パリティを変更
                    // add 160517
                    switch (cmbCommEnc.SelectedItem.ToString()) {
                        case "UNICODE":
                            m_serialPort1.m_serialPort.Encoding = Encoding.Unicode;
                            break;
                        default:
                            m_serialPort1.m_serialPort.Encoding = Encoding.ASCII;
                            break;
                    }
                    
                    //! データビットをセットする. (データビット = 8ビット)
                    // add 160517
                    switch (cmbCommDatBit.SelectedText) {
                        case "7":
                            m_serialPort1.m_serialPort.DataBits = 7;
                            break;
                        default:
                            m_serialPort1.m_serialPort.DataBits = 8;
                            break;
                    }

                     //! シリアルポートをオープンする.
                    m_serialPort1.m_serialPort.Open();

                    //! ボタンの表示を[接続]から[切断]に変える.
                    btnConnect.Text = "none connect/切断";

                    if (m_serialPort1.m_serialPort.Encoding == Encoding.ASCII) {
                        SetEnableCtrl(enRunStatus.en_idle_ascii);
                    }
                    else {
                        SetEnableCtrl(enRunStatus.en_idle_uni);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                SetEnableCtrl(enRunStatus.en_none_connect);
            }
        }


        /// <summary>
        /// コントロールの状態設定
        /// </summary>
        /// <param name="enbuf"></param>
        private void SetEnableCtrl(enRunStatus enbuf) {

            switch (enbuf) {
                case enRunStatus.en_autorun: {
                        for (int i = 0; i < btnCtrlPtrAry.Length; i++) {

                            if (i == (int)enBtnCtrlPtrIdx.en_idx_btn_runstop)
                                btnCtrlPtrAry[i].Enabled = true;
                            else
                                btnCtrlPtrAry[i].Enabled = false;
                        }
                        for (int i = 0; i < cmbbxCtrlPtrAry.Length; i++) {
                            cmbbxCtrlPtrAry[i].Enabled = false;
                        }

                        for (int i = 0; i < txtbxCtrlPtrAry.Length; i++) {
                            txtbxCtrlPtrAry[i].Enabled = false;
                        }

                        // CRC
                        chkCRC.Enabled = false;
                        break;
                    }

                case enRunStatus.en_none_connect: {
                        for (int i = 0; i < btnCtrlPtrAry.Length; i++) {
                            if (i == (int)enBtnCtrlPtrIdx.en_idx_btn_connect)
                                btnCtrlPtrAry[i].Enabled = true;
                            else
                                btnCtrlPtrAry[i].Enabled = false;
                        }
                        for (int i = 0; i < cmbbxCtrlPtrAry.Length; i++) {
                            cmbbxCtrlPtrAry[i].Enabled = true;
                        }
                        for (int i = 0; i < txtbxCtrlPtrAry.Length; i++) {
                            txtbxCtrlPtrAry[i].Enabled = true;
                        }

                        // CRC
                        chkCRC.Enabled = false;
                        break;
                    }

                case enRunStatus.en_idle_uni: {
                        for (int i = 0; i < btnCtrlPtrAry.Length; i++) {
                            btnCtrlPtrAry[i].Enabled = true;
                        }
                        for (int i = 0; i < cmbbxCtrlPtrAry.Length; i++) {
                            cmbbxCtrlPtrAry[i].Enabled = true;
                        }

                        for (int i = 0; i < txtbxCtrlPtrAry.Length; i++) {
                            txtbxCtrlPtrAry[i].Enabled = true;
                        }

                        // CRC
                        chkCRC.Enabled = true;
                        break;

                    }

                default: {
                        for (int i = 0; i < btnCtrlPtrAry.Length; i++) {
                            btnCtrlPtrAry[i].Enabled = true;
                        }
                        for (int i = 0; i < cmbbxCtrlPtrAry.Length; i++) {
                            cmbbxCtrlPtrAry[i].Enabled = true;
                        }

                        for (int i = 0; i < txtbxCtrlPtrAry.Length; i++) {
                            txtbxCtrlPtrAry[i].Enabled = true;
                        }

                        // CRC
                        chkCRC.Enabled = true;
//                        chkCRC.Enabled = false;
                        break;
                    }
            }
        }


        /// <summary>
        /// 文字列を送信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSend_Click(object sender, EventArgs e) {
            try {
                CCommGeneralPurposeApi.bCommEndFlg = false;         // 通信中断フラグ初期化

                SetEnableCtrl(enRunStatus.en_autorun);
                await Task.Run(() => {

                    // 一時コメント化
                    //// ASCII
                    //m_serialPort1.Async_Protocol_Send_AllDat(
                    //    CSystemDat.GetInstance().m_listCommStr,
                    //    ref m_serialPort1.m_serialPort,
                    //    int.Parse(txtbxIntvalTm.Text)
                    //    );

                    // BYTE
                    m_serialPort1.Async_Protocol_Send_AllDat_BYTE(
    CSystemDat.GetInstance().m_listCommStr,
    ref m_serialPort1.m_serialPort,
    int.Parse(txtbxIntvalTm.Text)
    );
                    //                     enSendType.en_SendType_Ascii);
                    //                   enSendType.en_SendType_Hex);
                });
            }
            catch (Exception excp) {
                MessageBox.Show(excp.Message);
            }
            finally {
                //    prgbarSendRecv.Value = 0;   // プログレスバー
                if (m_serialPort1.m_serialPort.Encoding == Encoding.ASCII) {
                    SetEnableCtrl(enRunStatus.en_idle_ascii);
                }
                else {
                    SetEnableCtrl(enRunStatus.en_idle_uni);
                }
                //SetEnableCtrl ( enRunStatus.en_idle );

                CCommGeneralPurposeApi.bCommEndFlg = false;
                //    bflgRun = false;
                //}
            }
        }

        /// <summary>
        /// 全行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendDatDelete_Click(object sender, EventArgs e) {
            try {
                TxtBx_Send.Clear();
                CSystemDat.GetInstance().m_listCommStr.Clear();

            }
            catch (CExcept_ excp) {
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// 全行消去
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecvDatDelete_Click(object sender, EventArgs e) {
            try {
                TxtBx_Recv.Clear();
            }
            catch (CExcept_ excp) {
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        ///  受信データをTextBoxに書き込む
        /// </summary>
        /// <param name="data"></param>
        private void RcvDataToTextBox(string data) {
            string strdat = null;
            //! 受信データをテキストボックスの最後に追記する.
            if (data != null) {
                byte[] bytetest = CCommUtility.GetInstance().StringToAscii_(data);

                switch (bytetest[0]) {
                    case 0x07:
                        strdat = "ACK";
                        break;
                    case 0x05:
                        strdat = "EOT";
                        break;
                    default:
                        strdat = data;
                        break;
                }

                TxtBx_Recv.AppendText(strdat + "\r\n");
            }
        }

        /// <summary>
        /// ENQ文字追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnq_Click(object sender, EventArgs e) {
            try {
                CSystemDat.GetInstance().m_listCommStr.Add("ENQ");
                TxtBx_Send.Text += "(ENQ)" + "\r\n";
            }
            catch (CExcept_ excp) {
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// ACK文字追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAck_Click(object sender, EventArgs e) {
            try {
                CSystemDat.GetInstance().m_listCommStr.Add("ACK");
                TxtBx_Send.Text += "(ACK)" + "\r\n";
            }
            catch (CExcept_ excp) {
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// EOT文字追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEot_Click(object sender, EventArgs e) {
            try {
                CSystemDat.GetInstance().m_listCommStr.Add("EOT");
                TxtBx_Send.Text += "(EOT)" + "\r\n";
            }
            catch (CExcept_ excp) {
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// CAN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCan_Click(object sender, EventArgs e) {
            try {
                CSystemDat.GetInstance().m_listCommStr.Add("CAN");
                TxtBx_Send.Text += "(CAN)" + "\r\n";
            }
            catch (CExcept_ excp) {
                MessageBox.Show(excp.Message);
            }
        }

        /// <summary>
        /// 送信停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunStp_Click(object sender, EventArgs e) {
            CCommGeneralPurposeApi.bCommEndFlg = true;
        }


        /// <summary>
        /// アプリケーション設定ファイル生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            CAppSettings.GetInstance().icommcur = cmbCommNo.SelectedIndex;
            CAppSettings.GetInstance().ibpscur = cmbComBps.SelectedIndex;
            CAppSettings.GetInstance().iStpbit = cmbbxStpBit.SelectedIndex;
            CAppSettings.GetInstance().iHandshake = cmbbxHandShake.SelectedIndex;

            // 別処理で保存
            // 画面データを保存
            System.Configuration.Configuration conf = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            conf.AppSettings.Settings.Add("bpscur", cmbComBps.ToString());
            conf.Save(System.Configuration.ConfigurationSaveMode.Modified);

            Properties.Settings.Default.Save();

        }
    }
}