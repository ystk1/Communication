namespace CommTestTool
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose ( bool disposing ) {
            if ( disposing && ( components != null ) ) {
                components.Dispose ( );
            }
            base.Dispose ( disposing );
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent ( ) {
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grbxComm = new System.Windows.Forms.GroupBox();
            this.cmbCommDatBit = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbCommParity = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbCommEnc = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbbxHandShake = new System.Windows.Forms.ComboBox();
            this.cmbbxStpBit = new System.Windows.Forms.ComboBox();
            this.cmbCommNo = new System.Windows.Forms.ComboBox();
            this.cmbComBps = new System.Windows.Forms.ComboBox();
            this.grbxOpe = new System.Windows.Forms.GroupBox();
            this.grpbxCommStr = new System.Windows.Forms.GroupBox();
            this.chkCRC = new System.Windows.Forms.CheckBox();
            this.txt1RowSendDat = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSendDatClr = new System.Windows.Forms.Button();
            this.btnSendDatCreate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.chkbxLF = new System.Windows.Forms.CheckBox();
            this.chkbxCR = new System.Windows.Forms.CheckBox();
            this.txtbxSendStr = new System.Windows.Forms.TextBox();
            this.chkbx_stx = new System.Windows.Forms.CheckBox();
            this.chkbx_etx = new System.Windows.Forms.CheckBox();
            this.chkbtnStrlen = new System.Windows.Forms.CheckBox();
            this.btnSendDatAdd = new System.Windows.Forms.Button();
            this.chkbtnCheckSum = new System.Windows.Forms.CheckBox();
            this.grpbx = new System.Windows.Forms.GroupBox();
            this.btnCan = new System.Windows.Forms.Button();
            this.btnEnq = new System.Windows.Forms.Button();
            this.btnAck = new System.Windows.Forms.Button();
            this.btnEot = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRunStp = new System.Windows.Forms.Button();
            this.btnSendDatDelete = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.TxtBx_Send = new System.Windows.Forms.TextBox();
            this.grbParam = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtbxIntvalTm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtBx_Recv = new System.Windows.Forms.TextBox();
            this.grbCommLog = new System.Windows.Forms.GroupBox();
            this.btnRecvDatDelete = new System.Windows.Forms.Button();
            this.grbxComm.SuspendLayout();
            this.grbxOpe.SuspendLayout();
            this.grpbxCommStr.SuspendLayout();
            this.grpbx.SuspendLayout();
            this.grbParam.SuspendLayout();
            this.grbCommLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnConnect.Location = new System.Drawing.Point(399, 16);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(96, 35);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "connect/接続";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "COM:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "bps/ボーレート:";
            // 
            // grbxComm
            // 
            this.grbxComm.Controls.Add(this.cmbCommDatBit);
            this.grbxComm.Controls.Add(this.label12);
            this.grbxComm.Controls.Add(this.cmbCommParity);
            this.grbxComm.Controls.Add(this.label11);
            this.grbxComm.Controls.Add(this.cmbCommEnc);
            this.grbxComm.Controls.Add(this.label10);
            this.grbxComm.Controls.Add(this.label7);
            this.grbxComm.Controls.Add(this.label6);
            this.grbxComm.Controls.Add(this.cmbbxHandShake);
            this.grbxComm.Controls.Add(this.cmbbxStpBit);
            this.grbxComm.Controls.Add(this.btnConnect);
            this.grbxComm.Controls.Add(this.label2);
            this.grbxComm.Controls.Add(this.cmbCommNo);
            this.grbxComm.Controls.Add(this.label1);
            this.grbxComm.Controls.Add(this.cmbComBps);
            this.grbxComm.Location = new System.Drawing.Point(6, 12);
            this.grbxComm.Name = "grbxComm";
            this.grbxComm.Size = new System.Drawing.Size(503, 154);
            this.grbxComm.TabIndex = 5;
            this.grbxComm.TabStop = false;
            this.grbxComm.Text = "setting/通信設定";
            // 
            // cmbCommDatBit
            // 
            this.cmbCommDatBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommDatBit.FormattingEnabled = true;
            this.cmbCommDatBit.Location = new System.Drawing.Point(117, 120);
            this.cmbCommDatBit.Name = "cmbCommDatBit";
            this.cmbCommDatBit.Size = new System.Drawing.Size(81, 20);
            this.cmbCommDatBit.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 124);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "databit/データビット";
            // 
            // cmbCommParity
            // 
            this.cmbCommParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommParity.FormattingEnabled = true;
            this.cmbCommParity.Location = new System.Drawing.Point(352, 87);
            this.cmbCommParity.Name = "cmbCommParity";
            this.cmbCommParity.Size = new System.Drawing.Size(81, 20);
            this.cmbCommParity.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(272, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "parity/パリティ";
            // 
            // cmbCommEnc
            // 
            this.cmbCommEnc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommEnc.FormattingEnabled = true;
            this.cmbCommEnc.Location = new System.Drawing.Point(117, 87);
            this.cmbCommEnc.Name = "cmbCommEnc";
            this.cmbCommEnc.Size = new System.Drawing.Size(81, 20);
            this.cmbCommEnc.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "encode/エンコード";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(218, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "handshake/ハンドシェイク";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "stopbit/ｽﾄｯﾌﾟﾋﾞｯﾄ";
            // 
            // cmbbxHandShake
            // 
            this.cmbbxHandShake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxHandShake.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbbxHandShake.FormattingEnabled = true;
            this.cmbbxHandShake.Location = new System.Drawing.Point(352, 55);
            this.cmbbxHandShake.Name = "cmbbxHandShake";
            this.cmbbxHandShake.Size = new System.Drawing.Size(81, 19);
            this.cmbbxHandShake.TabIndex = 6;
            // 
            // cmbbxStpBit
            // 
            this.cmbbxStpBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxStpBit.FormattingEnabled = true;
            this.cmbbxStpBit.Location = new System.Drawing.Point(117, 54);
            this.cmbbxStpBit.Name = "cmbbxStpBit";
            this.cmbbxStpBit.Size = new System.Drawing.Size(81, 20);
            this.cmbbxStpBit.TabIndex = 5;
            // 
            // cmbCommNo
            // 
            this.cmbCommNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbCommNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommNo.FormattingEnabled = true;
            this.cmbCommNo.Location = new System.Drawing.Point(50, 24);
            this.cmbCommNo.Name = "cmbCommNo";
            this.cmbCommNo.Size = new System.Drawing.Size(121, 20);
            this.cmbCommNo.TabIndex = 1;
            // 
            // cmbComBps
            // 
            this.cmbComBps.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbComBps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComBps.FormattingEnabled = true;
            this.cmbComBps.Location = new System.Drawing.Point(261, 24);
            this.cmbComBps.Name = "cmbComBps";
            this.cmbComBps.Size = new System.Drawing.Size(121, 20);
            this.cmbComBps.TabIndex = 2;
            // 
            // grbxOpe
            // 
            this.grbxOpe.Controls.Add(this.grpbxCommStr);
            this.grbxOpe.Controls.Add(this.grpbx);
            this.grbxOpe.Controls.Add(this.label3);
            this.grbxOpe.Controls.Add(this.btnRunStp);
            this.grbxOpe.Controls.Add(this.btnSendDatDelete);
            this.grbxOpe.Controls.Add(this.btnSend);
            this.grbxOpe.Controls.Add(this.TxtBx_Send);
            this.grbxOpe.Location = new System.Drawing.Point(6, 183);
            this.grbxOpe.Name = "grbxOpe";
            this.grbxOpe.Size = new System.Drawing.Size(503, 477);
            this.grbxOpe.TabIndex = 6;
            this.grbxOpe.TabStop = false;
            this.grbxOpe.Text = "send/送信";
            // 
            // grpbxCommStr
            // 
            this.grpbxCommStr.Controls.Add(this.chkCRC);
            this.grpbxCommStr.Controls.Add(this.txt1RowSendDat);
            this.grpbxCommStr.Controls.Add(this.label9);
            this.grpbxCommStr.Controls.Add(this.btnSendDatClr);
            this.grpbxCommStr.Controls.Add(this.btnSendDatCreate);
            this.grpbxCommStr.Controls.Add(this.label8);
            this.grpbxCommStr.Controls.Add(this.chkbxLF);
            this.grpbxCommStr.Controls.Add(this.chkbxCR);
            this.grpbxCommStr.Controls.Add(this.txtbxSendStr);
            this.grpbxCommStr.Controls.Add(this.chkbx_stx);
            this.grpbxCommStr.Controls.Add(this.chkbx_etx);
            this.grpbxCommStr.Controls.Add(this.chkbtnStrlen);
            this.grpbxCommStr.Controls.Add(this.btnSendDatAdd);
            this.grpbxCommStr.Controls.Add(this.chkbtnCheckSum);
            this.grpbxCommStr.Location = new System.Drawing.Point(14, 77);
            this.grpbxCommStr.Name = "grpbxCommStr";
            this.grpbxCommStr.Size = new System.Drawing.Size(481, 210);
            this.grpbxCommStr.TabIndex = 16;
            this.grpbxCommStr.TabStop = false;
            this.grpbxCommStr.Text = "create send data(1row)/送信1行データ生成";
            // 
            // chkCRC
            // 
            this.chkCRC.AutoSize = true;
            this.chkCRC.Location = new System.Drawing.Point(269, 69);
            this.chkCRC.Name = "chkCRC";
            this.chkCRC.Size = new System.Drawing.Size(89, 16);
            this.chkCRC.TabIndex = 24;
            this.chkCRC.Text = "CRC(2バイト)";
            this.chkCRC.UseVisualStyleBackColor = true;
            // 
            // txt1RowSendDat
            // 
            this.txt1RowSendDat.Location = new System.Drawing.Point(12, 162);
            this.txt1RowSendDat.Multiline = true;
            this.txt1RowSendDat.Name = "txt1RowSendDat";
            this.txt1RowSendDat.ReadOnly = true;
            this.txt1RowSendDat.Size = new System.Drawing.Size(334, 39);
            this.txt1RowSendDat.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(175, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "send data(1Row) / 送信1行データ";
            // 
            // btnSendDatClr
            // 
            this.btnSendDatClr.Location = new System.Drawing.Point(417, 159);
            this.btnSendDatClr.Name = "btnSendDatClr";
            this.btnSendDatClr.Size = new System.Drawing.Size(53, 42);
            this.btnSendDatClr.TabIndex = 23;
            this.btnSendDatClr.Text = "Clear/削除";
            this.btnSendDatClr.UseVisualStyleBackColor = true;
            this.btnSendDatClr.Click += new System.EventHandler(this.btnSendDatClr_Click);
            // 
            // btnSendDatCreate
            // 
            this.btnSendDatCreate.Location = new System.Drawing.Point(391, 33);
            this.btnSendDatCreate.Name = "btnSendDatCreate";
            this.btnSendDatCreate.Size = new System.Drawing.Size(75, 68);
            this.btnSendDatCreate.TabIndex = 17;
            this.btnSendDatCreate.Text = "create/生成";
            this.btnSendDatCreate.UseVisualStyleBackColor = true;
            this.btnSendDatCreate.Click += new System.EventHandler(this.btnSendDatCreate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "data/データ部:";
            // 
            // chkbxLF
            // 
            this.chkbxLF.AutoSize = true;
            this.chkbxLF.Location = new System.Drawing.Point(207, 96);
            this.chkbxLF.Name = "chkbxLF";
            this.chkbxLF.Size = new System.Drawing.Size(37, 16);
            this.chkbxLF.TabIndex = 15;
            this.chkbxLF.Text = "LF";
            this.chkbxLF.UseVisualStyleBackColor = true;
            // 
            // chkbxCR
            // 
            this.chkbxCR.AutoSize = true;
            this.chkbxCR.Location = new System.Drawing.Point(207, 69);
            this.chkbxCR.Name = "chkbxCR";
            this.chkbxCR.Size = new System.Drawing.Size(40, 16);
            this.chkbxCR.TabIndex = 14;
            this.chkbxCR.Text = "CR";
            this.chkbxCR.UseVisualStyleBackColor = true;
            // 
            // txtbxSendStr
            // 
            this.txtbxSendStr.AllowDrop = true;
            this.txtbxSendStr.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtbxSendStr.Location = new System.Drawing.Point(6, 33);
            this.txtbxSendStr.Name = "txtbxSendStr";
            this.txtbxSendStr.Size = new System.Drawing.Size(379, 22);
            this.txtbxSendStr.TabIndex = 5;
            // 
            // chkbx_stx
            // 
            this.chkbx_stx.AutoSize = true;
            this.chkbx_stx.Checked = true;
            this.chkbx_stx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbx_stx.Location = new System.Drawing.Point(14, 69);
            this.chkbx_stx.Name = "chkbx_stx";
            this.chkbx_stx.Size = new System.Drawing.Size(40, 16);
            this.chkbx_stx.TabIndex = 6;
            this.chkbx_stx.Text = "stx";
            this.chkbx_stx.UseVisualStyleBackColor = true;
            // 
            // chkbx_etx
            // 
            this.chkbx_etx.AutoSize = true;
            this.chkbx_etx.Checked = true;
            this.chkbx_etx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbx_etx.Location = new System.Drawing.Point(14, 96);
            this.chkbx_etx.Name = "chkbx_etx";
            this.chkbx_etx.Size = new System.Drawing.Size(40, 16);
            this.chkbx_etx.TabIndex = 7;
            this.chkbx_etx.Text = "etx";
            this.chkbx_etx.UseVisualStyleBackColor = true;
            // 
            // chkbtnStrlen
            // 
            this.chkbtnStrlen.AutoSize = true;
            this.chkbtnStrlen.Checked = true;
            this.chkbtnStrlen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbtnStrlen.Location = new System.Drawing.Point(60, 96);
            this.chkbtnStrlen.Name = "chkbtnStrlen";
            this.chkbtnStrlen.Size = new System.Drawing.Size(142, 16);
            this.chkbtnStrlen.TabIndex = 13;
            this.chkbtnStrlen.Text = "string length/文字列長";
            this.chkbtnStrlen.UseVisualStyleBackColor = true;
            // 
            // btnSendDatAdd
            // 
            this.btnSendDatAdd.Location = new System.Drawing.Point(351, 159);
            this.btnSendDatAdd.Name = "btnSendDatAdd";
            this.btnSendDatAdd.Size = new System.Drawing.Size(65, 42);
            this.btnSendDatAdd.TabIndex = 8;
            this.btnSendDatAdd.Text = "add/追加";
            this.btnSendDatAdd.UseVisualStyleBackColor = true;
            this.btnSendDatAdd.Click += new System.EventHandler(this.btnSendDatAdd_Click);
            // 
            // chkbtnCheckSum
            // 
            this.chkbtnCheckSum.AutoSize = true;
            this.chkbtnCheckSum.Checked = true;
            this.chkbtnCheckSum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbtnCheckSum.Location = new System.Drawing.Point(60, 69);
            this.chkbtnCheckSum.Name = "chkbtnCheckSum";
            this.chkbtnCheckSum.Size = new System.Drawing.Size(136, 16);
            this.chkbtnCheckSum.TabIndex = 12;
            this.chkbtnCheckSum.Text = "check sum/チェックサム";
            this.chkbtnCheckSum.UseVisualStyleBackColor = true;
            // 
            // grpbx
            // 
            this.grpbx.Controls.Add(this.btnCan);
            this.grpbx.Controls.Add(this.btnEnq);
            this.grpbx.Controls.Add(this.btnAck);
            this.grpbx.Controls.Add(this.btnEot);
            this.grpbx.Location = new System.Drawing.Point(14, 18);
            this.grpbx.Name = "grpbx";
            this.grpbx.Size = new System.Drawing.Size(364, 53);
            this.grpbx.TabIndex = 15;
            this.grpbx.TabStop = false;
            this.grpbx.Text = "signal/信号";
            // 
            // btnCan
            // 
            this.btnCan.Location = new System.Drawing.Point(278, 20);
            this.btnCan.Name = "btnCan";
            this.btnCan.Size = new System.Drawing.Size(75, 23);
            this.btnCan.TabIndex = 5;
            this.btnCan.Text = "CAN";
            this.btnCan.UseVisualStyleBackColor = true;
            this.btnCan.Visible = false;
            this.btnCan.Click += new System.EventHandler(this.btnCan_Click);
            // 
            // btnEnq
            // 
            this.btnEnq.Location = new System.Drawing.Point(15, 20);
            this.btnEnq.Name = "btnEnq";
            this.btnEnq.Size = new System.Drawing.Size(75, 23);
            this.btnEnq.TabIndex = 2;
            this.btnEnq.Text = "ENQ";
            this.btnEnq.UseVisualStyleBackColor = true;
            this.btnEnq.Click += new System.EventHandler(this.btnEnq_Click);
            // 
            // btnAck
            // 
            this.btnAck.Location = new System.Drawing.Point(96, 20);
            this.btnAck.Name = "btnAck";
            this.btnAck.Size = new System.Drawing.Size(75, 23);
            this.btnAck.TabIndex = 3;
            this.btnAck.Text = "ACK";
            this.btnAck.UseVisualStyleBackColor = true;
            this.btnAck.Click += new System.EventHandler(this.btnAck_Click);
            // 
            // btnEot
            // 
            this.btnEot.Location = new System.Drawing.Point(177, 20);
            this.btnEot.Name = "btnEot";
            this.btnEot.Size = new System.Drawing.Size(75, 23);
            this.btnEot.TabIndex = 4;
            this.btnEot.Text = "EOT";
            this.btnEot.UseVisualStyleBackColor = true;
            this.btnEot.Click += new System.EventHandler(this.btnEot_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 319);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "send data/全送信データ:";
            // 
            // btnRunStp
            // 
            this.btnRunStp.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRunStp.Location = new System.Drawing.Point(197, 432);
            this.btnRunStp.Name = "btnRunStp";
            this.btnRunStp.Size = new System.Drawing.Size(116, 28);
            this.btnRunStp.TabIndex = 11;
            this.btnRunStp.Text = "stop/停止";
            this.btnRunStp.UseVisualStyleBackColor = true;
            this.btnRunStp.Click += new System.EventHandler(this.btnRunStp_Click);
            // 
            // btnSendDatDelete
            // 
            this.btnSendDatDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnSendDatDelete.Location = new System.Drawing.Point(431, 334);
            this.btnSendDatDelete.Name = "btnSendDatDelete";
            this.btnSendDatDelete.Size = new System.Drawing.Size(49, 92);
            this.btnSendDatDelete.TabIndex = 10;
            this.btnSendDatDelete.Text = "clear\r\n/全行削除";
            this.btnSendDatDelete.UseVisualStyleBackColor = false;
            this.btnSendDatDelete.Click += new System.EventHandler(this.btnSendDatDelete_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.Control;
            this.btnSend.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSend.Location = new System.Drawing.Point(11, 432);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(167, 28);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "send/送信";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // TxtBx_Send
            // 
            this.TxtBx_Send.BackColor = System.Drawing.SystemColors.Info;
            this.TxtBx_Send.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtBx_Send.Location = new System.Drawing.Point(11, 334);
            this.TxtBx_Send.Multiline = true;
            this.TxtBx_Send.Name = "TxtBx_Send";
            this.TxtBx_Send.ReadOnly = true;
            this.TxtBx_Send.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtBx_Send.Size = new System.Drawing.Size(419, 92);
            this.TxtBx_Send.TabIndex = 1;
            // 
            // grbParam
            // 
            this.grbParam.Controls.Add(this.label5);
            this.grbParam.Controls.Add(this.txtbxIntvalTm);
            this.grbParam.Controls.Add(this.label4);
            this.grbParam.Location = new System.Drawing.Point(521, 554);
            this.grbParam.Name = "grbParam";
            this.grbParam.Size = new System.Drawing.Size(76, 106);
            this.grbParam.TabIndex = 20;
            this.grbParam.TabStop = false;
            this.grbParam.Text = "parameter/パラメタ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "intervaltime\r\n/間隔時間";
            // 
            // txtbxIntvalTm
            // 
            this.txtbxIntvalTm.Location = new System.Drawing.Point(8, 61);
            this.txtbxIntvalTm.Name = "txtbxIntvalTm";
            this.txtbxIntvalTm.Size = new System.Drawing.Size(50, 19);
            this.txtbxIntvalTm.TabIndex = 17;
            this.txtbxIntvalTm.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "[msec]";
            // 
            // TxtBx_Recv
            // 
            this.TxtBx_Recv.AllowDrop = true;
            this.TxtBx_Recv.BackColor = System.Drawing.SystemColors.Info;
            this.TxtBx_Recv.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtBx_Recv.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtBx_Recv.Location = new System.Drawing.Point(6, 28);
            this.TxtBx_Recv.Multiline = true;
            this.TxtBx_Recv.Name = "TxtBx_Recv";
            this.TxtBx_Recv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtBx_Recv.Size = new System.Drawing.Size(359, 502);
            this.TxtBx_Recv.TabIndex = 7;
            // 
            // grbCommLog
            // 
            this.grbCommLog.Controls.Add(this.btnRecvDatDelete);
            this.grbCommLog.Controls.Add(this.TxtBx_Recv);
            this.grbCommLog.Location = new System.Drawing.Point(515, 12);
            this.grbCommLog.Name = "grbCommLog";
            this.grbCommLog.Size = new System.Drawing.Size(422, 536);
            this.grbCommLog.TabIndex = 16;
            this.grbCommLog.TabStop = false;
            this.grbCommLog.Text = "communication trace/通信トレース";
            // 
            // btnRecvDatDelete
            // 
            this.btnRecvDatDelete.Location = new System.Drawing.Point(371, 28);
            this.btnRecvDatDelete.Name = "btnRecvDatDelete";
            this.btnRecvDatDelete.Size = new System.Drawing.Size(44, 502);
            this.btnRecvDatDelete.TabIndex = 8;
            this.btnRecvDatDelete.Text = "clear\r\n/全行削除";
            this.btnRecvDatDelete.UseVisualStyleBackColor = true;
            this.btnRecvDatDelete.Click += new System.EventHandler(this.btnRecvDatDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 672);
            this.Controls.Add(this.grbCommLog);
            this.Controls.Add(this.grbxOpe);
            this.Controls.Add(this.grbParam);
            this.Controls.Add(this.grbxComm);
            this.Name = "Form1";
            this.Text = "communication test tool/通信テストツール";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Serial_Comm_Load);
            this.grbxComm.ResumeLayout(false);
            this.grbxComm.PerformLayout();
            this.grbxOpe.ResumeLayout(false);
            this.grbxOpe.PerformLayout();
            this.grpbxCommStr.ResumeLayout(false);
            this.grpbxCommStr.PerformLayout();
            this.grpbx.ResumeLayout(false);
            this.grbParam.ResumeLayout(false);
            this.grbParam.PerformLayout();
            this.grbCommLog.ResumeLayout(false);
            this.grbCommLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbCommNo;
        private System.Windows.Forms.ComboBox cmbComBps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grbxComm;
        private System.Windows.Forms.GroupBox grbxOpe;
        private System.Windows.Forms.TextBox TxtBx_Send;
        private System.Windows.Forms.Button btnSendDatAdd;
        private System.Windows.Forms.CheckBox chkbx_etx;
        private System.Windows.Forms.CheckBox chkbx_stx;
        private System.Windows.Forms.TextBox txtbxSendStr;
        private System.Windows.Forms.Button btnEot;
        private System.Windows.Forms.Button btnAck;
        private System.Windows.Forms.Button btnEnq;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSendDatDelete;
        private System.Windows.Forms.TextBox TxtBx_Recv;
        private System.Windows.Forms.Button btnRunStp;
        private System.Windows.Forms.CheckBox chkbtnStrlen;
        private System.Windows.Forms.CheckBox chkbtnCheckSum;
        private System.Windows.Forms.GroupBox grpbxCommStr;
        private System.Windows.Forms.GroupBox grpbx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grbCommLog;
        private System.Windows.Forms.Button btnRecvDatDelete;
        private System.Windows.Forms.GroupBox grbParam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtbxIntvalTm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCan;
        private System.Windows.Forms.CheckBox chkbxLF;
        private System.Windows.Forms.CheckBox chkbxCR;
        private System.Windows.Forms.ComboBox cmbbxStpBit;
        private System.Windows.Forms.ComboBox cmbbxHandShake;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt1RowSendDat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSendDatCreate;
        private System.Windows.Forms.Button btnSendDatClr;
        private System.Windows.Forms.CheckBox chkCRC;
        private System.Windows.Forms.ComboBox cmbCommParity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbCommEnc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbCommDatBit;
    }
}

