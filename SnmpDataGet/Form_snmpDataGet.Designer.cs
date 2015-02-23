namespace SnmpDGet
{
    partial class Form_snmpDataGet
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_snmpDataGet));
            this.m_commu = new System.Windows.Forms.TextBox();
            this.m_versioncombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_OIDcombo = new System.Windows.Forms.ComboBox();
            this.m_host = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_OK = new System.Windows.Forms.Button();
            this.m_end = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_getRadio = new System.Windows.Forms.RadioButton();
            this.m_walkRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_sysServices = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.m_sysLocation = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_sysName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_sysContact = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_sysUpTime = new System.Windows.Forms.TextBox();
            this.labet10 = new System.Windows.Forms.Label();
            this.m_sysObjectID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_sysDescr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.コピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.manyList = new System.Windows.Forms.ListView();
            this.bgworker = new System.ComponentModel.BackgroundWorker();
            this.suspendBtn = new System.Windows.Forms.Button();
            this.m_versioninfo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_listFile = new System.Windows.Forms.TextBox();
            this.m_selectBtn = new System.Windows.Forms.Button();
            this.m_readBtn = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_commu
            // 
            this.m_commu.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_commu.Location = new System.Drawing.Point(114, 45);
            this.m_commu.Name = "m_commu";
            this.m_commu.Size = new System.Drawing.Size(180, 22);
            this.m_commu.TabIndex = 1;
            this.m_commu.Text = "public";
            // 
            // m_versioncombo
            // 
            this.m_versioncombo.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_versioncombo.FormattingEnabled = true;
            this.m_versioncombo.Items.AddRange(new object[] {
            "v1",
            "v2c"});
            this.m_versioncombo.Location = new System.Drawing.Point(114, 76);
            this.m_versioncombo.Name = "m_versioncombo";
            this.m_versioncombo.Size = new System.Drawing.Size(180, 23);
            this.m_versioncombo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(15, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "コミュニティ(&c)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "バージョン(&v)";
            // 
            // m_OIDcombo
            // 
            this.m_OIDcombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OIDcombo.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_OIDcombo.FormattingEnabled = true;
            this.m_OIDcombo.Location = new System.Drawing.Point(114, 109);
            this.m_OIDcombo.Name = "m_OIDcombo";
            this.m_OIDcombo.Size = new System.Drawing.Size(278, 23);
            this.m_OIDcombo.TabIndex = 3;
            // 
            // m_host
            // 
            this.m_host.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_host.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.m_host.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.m_host.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_host.Location = new System.Drawing.Point(114, 17);
            this.m_host.Name = "m_host";
            this.m_host.Size = new System.Drawing.Size(585, 22);
            this.m_host.TabIndex = 0;
            this.m_host.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_host_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(14, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "ホスト名/IP(&ip)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(15, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "OID(&o)";
            // 
            // m_OK
            // 
            this.m_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OK.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_OK.Location = new System.Drawing.Point(737, 12);
            this.m_OK.Name = "m_OK";
            this.m_OK.Size = new System.Drawing.Size(77, 47);
            this.m_OK.TabIndex = 8;
            this.m_OK.Text = "実行";
            this.m_OK.UseVisualStyleBackColor = true;
            this.m_OK.Click += new System.EventHandler(this.m_OK_Click);
            // 
            // m_end
            // 
            this.m_end.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_end.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_end.Location = new System.Drawing.Point(824, 12);
            this.m_end.Name = "m_end";
            this.m_end.Size = new System.Drawing.Size(77, 47);
            this.m_end.TabIndex = 9;
            this.m_end.Text = "閉じる";
            this.m_end.UseVisualStyleBackColor = true;
            this.m_end.Click += new System.EventHandler(this.button2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(911, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // m_getRadio
            // 
            this.m_getRadio.AutoSize = true;
            this.m_getRadio.Checked = true;
            this.m_getRadio.Location = new System.Drawing.Point(27, 12);
            this.m_getRadio.Name = "m_getRadio";
            this.m_getRadio.Size = new System.Drawing.Size(57, 16);
            this.m_getRadio.TabIndex = 0;
            this.m_getRadio.TabStop = true;
            this.m_getRadio.Text = "GET(&t)";
            this.m_getRadio.UseVisualStyleBackColor = true;
            this.m_getRadio.CheckedChanged += new System.EventHandler(this.m_getRadio_CheckedChanged);
            // 
            // m_walkRadio
            // 
            this.m_walkRadio.AutoSize = true;
            this.m_walkRadio.Location = new System.Drawing.Point(103, 12);
            this.m_walkRadio.Name = "m_walkRadio";
            this.m_walkRadio.Size = new System.Drawing.Size(100, 16);
            this.m_walkRadio.TabIndex = 1;
            this.m_walkRadio.Text = "WALK/BULK(&t)";
            this.m_walkRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.m_getRadio);
            this.groupBox1.Controls.Add(this.m_walkRadio);
            this.groupBox1.Location = new System.Drawing.Point(407, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 34);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tabControl1.Location = new System.Drawing.Point(0, 180);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(911, 467);
            this.tabControl1.TabIndex = 12;
            this.tabControl1.Tag = "";
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.m_sysServices);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.m_sysLocation);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.m_sysName);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.m_sysContact);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.m_sysUpTime);
            this.tabPage1.Controls.Add(this.labet10);
            this.tabPage1.Controls.Add(this.m_sysObjectID);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.m_sysDescr);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(903, 438);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // m_sysServices
            // 
            this.m_sysServices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sysServices.BackColor = System.Drawing.SystemColors.Window;
            this.m_sysServices.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_sysServices.Location = new System.Drawing.Point(14, 404);
            this.m_sysServices.Name = "m_sysServices";
            this.m_sysServices.ReadOnly = true;
            this.m_sysServices.Size = new System.Drawing.Size(836, 22);
            this.m_sysServices.TabIndex = 6;
            this.m_sysServices.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(15, 386);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(322, 15);
            this.label10.TabIndex = 12;
            this.label10.Text = "サポートする設定フラグ(sysServices: .1.3.6.1.2.1.1.7)";
            // 
            // m_sysLocation
            // 
            this.m_sysLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sysLocation.BackColor = System.Drawing.SystemColors.Window;
            this.m_sysLocation.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_sysLocation.Location = new System.Drawing.Point(14, 349);
            this.m_sysLocation.Name = "m_sysLocation";
            this.m_sysLocation.ReadOnly = true;
            this.m_sysLocation.Size = new System.Drawing.Size(836, 22);
            this.m_sysLocation.TabIndex = 5;
            this.m_sysLocation.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(15, 329);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(261, 15);
            this.label11.TabIndex = 10;
            this.label11.Text = "機器の場所(sysLocation: .1.3.6.1.2.1.1.6)";
            // 
            // m_sysName
            // 
            this.m_sysName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sysName.BackColor = System.Drawing.SystemColors.Window;
            this.m_sysName.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_sysName.Location = new System.Drawing.Point(14, 288);
            this.m_sysName.Name = "m_sysName";
            this.m_sysName.ReadOnly = true;
            this.m_sysName.Size = new System.Drawing.Size(836, 22);
            this.m_sysName.TabIndex = 4;
            this.m_sysName.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(15, 270);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(288, 15);
            this.label12.TabIndex = 8;
            this.label12.Text = "完全修飾ドメイン名(sysName: .1.3.6.1.2.1.1.5)";
            // 
            // m_sysContact
            // 
            this.m_sysContact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sysContact.BackColor = System.Drawing.SystemColors.Window;
            this.m_sysContact.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_sysContact.Location = new System.Drawing.Point(14, 233);
            this.m_sysContact.Name = "m_sysContact";
            this.m_sysContact.ReadOnly = true;
            this.m_sysContact.Size = new System.Drawing.Size(836, 22);
            this.m_sysContact.TabIndex = 3;
            this.m_sysContact.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(15, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(308, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "管理者メールアドレス(sysContact: .1.3.6.1.2.1.1.4)";
            // 
            // m_sysUpTime
            // 
            this.m_sysUpTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sysUpTime.BackColor = System.Drawing.SystemColors.Window;
            this.m_sysUpTime.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_sysUpTime.Location = new System.Drawing.Point(14, 171);
            this.m_sysUpTime.Name = "m_sysUpTime";
            this.m_sysUpTime.ReadOnly = true;
            this.m_sysUpTime.Size = new System.Drawing.Size(836, 22);
            this.m_sysUpTime.TabIndex = 2;
            this.m_sysUpTime.TabStop = false;
            // 
            // labet10
            // 
            this.labet10.AutoSize = true;
            this.labet10.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labet10.Location = new System.Drawing.Point(15, 153);
            this.labet10.Name = "labet10";
            this.labet10.Size = new System.Drawing.Size(288, 15);
            this.labet10.TabIndex = 4;
            this.labet10.Text = "システム稼働時間(sysUpTime: .1.3.6.1.2.1.1.3)";
            // 
            // m_sysObjectID
            // 
            this.m_sysObjectID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sysObjectID.BackColor = System.Drawing.SystemColors.Window;
            this.m_sysObjectID.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_sysObjectID.Location = new System.Drawing.Point(14, 114);
            this.m_sysObjectID.Name = "m_sysObjectID";
            this.m_sysObjectID.ReadOnly = true;
            this.m_sysObjectID.Size = new System.Drawing.Size(836, 22);
            this.m_sysObjectID.TabIndex = 1;
            this.m_sysObjectID.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(15, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(268, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "オブジェクトID(sysObjectID: .1.3.6.1.2.1.1.2)";
            // 
            // m_sysDescr
            // 
            this.m_sysDescr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sysDescr.BackColor = System.Drawing.SystemColors.Window;
            this.m_sysDescr.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_sysDescr.Location = new System.Drawing.Point(14, 30);
            this.m_sysDescr.Multiline = true;
            this.m_sysDescr.Name = "m_sysDescr";
            this.m_sysDescr.ReadOnly = true;
            this.m_sysDescr.Size = new System.Drawing.Size(836, 57);
            this.m_sysDescr.TabIndex = 0;
            this.m_sysDescr.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(15, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(244, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "機器の説明(sysDescr: .1.3.6.1.2.1.1.1)";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(903, 438);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column19,
            this.Column20,
            this.Column21,
            this.Column22});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(897, 432);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridView1_PreviewKeyDown);
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "ifIndex";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "ifDescr";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "ifType";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ifMtu";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ifSpeed";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "ifPhysAddress";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "ifAdminStatus";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "ifOperStatus";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "ifLastChange";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "ifInOctets";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "ifInUcastPkts";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "ifInNUcastPkts";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "ifInDiscards";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "ifInErrors";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "ifInUnknownProtos";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "ifOutOctets";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "ifOutUcastPkts";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "ifOutNUcastPkts";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "ifOutDiscards";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "ifOutErrors";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "ifOutQLen";
            this.Column21.Name = "Column21";
            this.Column21.ReadOnly = true;
            // 
            // Column22
            // 
            this.Column22.HeaderText = "ifSpecific";
            this.Column22.Name = "Column22";
            this.Column22.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.コピーToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 26);
            this.contextMenuStrip1.Text = "コピー";
            // 
            // コピーToolStripMenuItem
            // 
            this.コピーToolStripMenuItem.Name = "コピーToolStripMenuItem";
            this.コピーToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.コピーToolStripMenuItem.Text = "コピー";
            this.コピーToolStripMenuItem.Click += new System.EventHandler(this.コピーToolStripMenuItem_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(903, 438);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.ContextMenuStrip = this.contextMenuStrip1;
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(3, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(897, 432);
            this.listView2.TabIndex = 12;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.VirtualMode = true;
            this.listView2.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView2_ColumnClick);
            this.listView2.DoubleClick += new System.EventHandler(this.listView2_DoubleClick);
            this.listView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView2_KeyDown);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.manyList);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(903, 438);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // manyList
            // 
            this.manyList.ContextMenuStrip = this.contextMenuStrip1;
            this.manyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manyList.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.manyList.GridLines = true;
            this.manyList.Location = new System.Drawing.Point(3, 3);
            this.manyList.Name = "manyList";
            this.manyList.Size = new System.Drawing.Size(897, 432);
            this.manyList.TabIndex = 13;
            this.manyList.UseCompatibleStateImageBehavior = false;
            this.manyList.View = System.Windows.Forms.View.Details;
            this.manyList.VirtualMode = true;
            this.manyList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.manyList_KeyDown);
            this.manyList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.manyList_MouseDoubleClick);
            // 
            // suspendBtn
            // 
            this.suspendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.suspendBtn.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.suspendBtn.Location = new System.Drawing.Point(824, 69);
            this.suspendBtn.Name = "suspendBtn";
            this.suspendBtn.Size = new System.Drawing.Size(77, 33);
            this.suspendBtn.TabIndex = 10;
            this.suspendBtn.Text = "中断";
            this.suspendBtn.UseVisualStyleBackColor = true;
            this.suspendBtn.Click += new System.EventHandler(this.suspendBtn_Click);
            // 
            // m_versioninfo
            // 
            this.m_versioninfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_versioninfo.AutoSize = true;
            this.m_versioninfo.Location = new System.Drawing.Point(828, 111);
            this.m_versioninfo.Name = "m_versioninfo";
            this.m_versioninfo.Size = new System.Drawing.Size(42, 12);
            this.m_versioninfo.TabIndex = 14;
            this.m_versioninfo.Text = "version";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(15, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "ファイル(&f)";
            // 
            // m_listFile
            // 
            this.m_listFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listFile.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_listFile.Location = new System.Drawing.Point(114, 142);
            this.m_listFile.Name = "m_listFile";
            this.m_listFile.Size = new System.Drawing.Size(585, 22);
            this.m_listFile.TabIndex = 5;
            this.m_listFile.TextChanged += new System.EventHandler(this.m_listFile_TextChanged);
            // 
            // m_selectBtn
            // 
            this.m_selectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_selectBtn.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_selectBtn.Location = new System.Drawing.Point(704, 141);
            this.m_selectBtn.Name = "m_selectBtn";
            this.m_selectBtn.Size = new System.Drawing.Size(40, 23);
            this.m_selectBtn.TabIndex = 6;
            this.m_selectBtn.Text = "参照";
            this.m_selectBtn.UseVisualStyleBackColor = true;
            this.m_selectBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_readBtn
            // 
            this.m_readBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_readBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_readBtn.Location = new System.Drawing.Point(750, 141);
            this.m_readBtn.Name = "m_readBtn";
            this.m_readBtn.Size = new System.Drawing.Size(83, 23);
            this.m_readBtn.TabIndex = 7;
            this.m_readBtn.Text = "読込";
            this.m_readBtn.UseVisualStyleBackColor = true;
            this.m_readBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form_snmpDataGet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 672);
            this.Controls.Add(this.m_readBtn);
            this.Controls.Add(this.m_selectBtn);
            this.Controls.Add(this.m_listFile);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.m_versioninfo);
            this.Controls.Add(this.suspendBtn);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.m_end);
            this.Controls.Add(this.m_OK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_host);
            this.Controls.Add(this.m_OIDcombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_versioncombo);
            this.Controls.Add(this.m_commu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_snmpDataGet";
            this.Text = "SnmpDGet";
            this.Load += new System.EventHandler(this.Form_snmpDataGet_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_commu;
        private System.Windows.Forms.ComboBox m_versioncombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_OIDcombo;
        private System.Windows.Forms.TextBox m_host;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button m_OK;
		private System.Windows.Forms.Button m_end;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.RadioButton m_getRadio;
		private System.Windows.Forms.RadioButton m_walkRadio;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox m_sysObjectID;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox m_sysDescr;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox m_sysServices;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox m_sysLocation;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox m_sysName;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox m_sysContact;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox m_sysUpTime;
		private System.Windows.Forms.Label labet10;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem コピーToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker bgworker;
		private System.Windows.Forms.Button suspendBtn;
		private System.Windows.Forms.Label m_versioninfo;
		private System.Windows.Forms.ListView listView2;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox m_listFile;
        private System.Windows.Forms.Button m_selectBtn;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView manyList;
        private System.Windows.Forms.Button m_readBtn;
    }
}

