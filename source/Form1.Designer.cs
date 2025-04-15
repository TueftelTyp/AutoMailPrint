namespace AutoMailPrint
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbMailServer = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbPrinters = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numKeepLog = new System.Windows.Forms.NumericUpDown();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lvData = new System.Windows.Forms.ListView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkAutostart = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lvLogs = new System.Windows.Forms.ListView();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.MenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miStartStop = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.lblGit = new System.Windows.Forms.LinkLabel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkSendMail = new System.Windows.Forms.CheckBox();
            this.tbErrorAddress = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnTestErrorMail = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.imgIcon = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnBMAC = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnTestMail = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKeepLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.MenuStrip1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSSL);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbProtocol);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.tbUser);
            this.groupBox1.Controls.Add(this.tbMailServer);
            this.groupBox1.Controls.Add(this.btnTestMail);
            this.groupBox1.Location = new System.Drawing.Point(6, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Email";
            // 
            // chkSSL
            // 
            this.chkSSL.AutoSize = true;
            this.chkSSL.Location = new System.Drawing.Point(6, 126);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(71, 17);
            this.chkSSL.TabIndex = 6;
            this.chkSSL.Text = "SSL/TLS";
            this.chkSSL.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(184, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Protocol";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(187, 42);
            this.numPort.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(48, 20);
            this.numPort.TabIndex = 2;
            this.numPort.Value = new decimal(new int[] {
            995,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mailserver";
            // 
            // cbProtocol
            // 
            this.cbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtocol.FormattingEnabled = true;
            this.cbProtocol.Items.AddRange(new object[] {
            "POP3",
            "IMAP"});
            this.cbProtocol.Location = new System.Drawing.Point(241, 42);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(81, 21);
            this.cbProtocol.TabIndex = 3;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(187, 82);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(135, 20);
            this.tbPassword.TabIndex = 5;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(6, 82);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(175, 20);
            this.tbUser.TabIndex = 4;
            // 
            // tbMailServer
            // 
            this.tbMailServer.Location = new System.Drawing.Point(6, 42);
            this.tbMailServer.Name = "tbMailServer";
            this.tbMailServer.Size = new System.Drawing.Size(175, 20);
            this.tbMailServer.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbPrinters);
            this.groupBox2.Location = new System.Drawing.Point(6, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 55);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Printer";
            // 
            // cbPrinters
            // 
            this.cbPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrinters.FormattingEnabled = true;
            this.cbPrinters.Location = new System.Drawing.Point(6, 19);
            this.cbPrinters.Name = "cbPrinters";
            this.cbPrinters.Size = new System.Drawing.Size(316, 21);
            this.cbPrinters.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numKeepLog);
            this.groupBox3.Controls.Add(this.numInterval);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(364, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(177, 83);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Interval";
            // 
            // numKeepLog
            // 
            this.numKeepLog.Location = new System.Drawing.Point(84, 54);
            this.numKeepLog.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numKeepLog.Name = "numKeepLog";
            this.numKeepLog.Size = new System.Drawing.Size(37, 20);
            this.numKeepLog.TabIndex = 10;
            this.numKeepLog.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // numInterval
            // 
            this.numInterval.Location = new System.Drawing.Point(73, 24);
            this.numInterval.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(48, 20);
            this.numInterval.TabIndex = 9;
            this.numInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Keep Logs for                  days";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Check every                     minute(s)";
            // 
            // lvData
            // 
            this.lvData.HideSelection = false;
            this.lvData.Location = new System.Drawing.Point(-4, 0);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(558, 332);
            this.lvData.TabIndex = 0;
            this.lvData.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.btnBMAC);
            this.groupBox5.Controls.Add(this.chkAutostart);
            this.groupBox5.Controls.Add(this.btnStartStop);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Location = new System.Drawing.Point(0, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(549, 329);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Settings";
            // 
            // chkAutostart
            // 
            this.chkAutostart.AutoSize = true;
            this.chkAutostart.Location = new System.Drawing.Point(416, 206);
            this.chkAutostart.Name = "chkAutostart";
            this.chkAutostart.Size = new System.Drawing.Size(70, 17);
            this.chkAutostart.TabIndex = 13;
            this.chkAutostart.Text = " autostart";
            this.chkAutostart.UseVisualStyleBackColor = true;
            this.chkAutostart.CheckedChanged += new System.EventHandler(this.ChkAutostart_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.cbReader);
            this.groupBox4.Location = new System.Drawing.Point(364, 113);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(177, 66);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Interface";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Checked = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(143, 17);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "use standard PDF reader";
            this.radioButton1.UseVisualStyleBackColor = false;
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(6, 39);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(165, 21);
            this.cbReader.TabIndex = 9;
            this.cbReader.Visible = false;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Location = new System.Drawing.Point(0, 40);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(558, 358);
            this.tabControl2.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(550, 332);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Control";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.linkLabel2);
            this.tabPage2.Controls.Add(this.lvData);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(550, 332);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(494, 2);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(37, 13);
            this.linkLabel2.TabIndex = 0;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "full log";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAttch_LinkClicked);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this.lvLogs);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(550, 332);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(494, 2);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "full log";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLog_LinkClicked);
            // 
            // lvLogs
            // 
            this.lvLogs.HideSelection = false;
            this.lvLogs.Location = new System.Drawing.Point(-4, 0);
            this.lvLogs.MultiSelect = false;
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.Size = new System.Drawing.Size(558, 332);
            this.lvLogs.TabIndex = 0;
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.MenuStrip1;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "AutoMailPrint";
            this.notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseDoubleClick);
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStartStop,
            this.miOpen,
            this.toolStripSeparator1,
            this.miExit});
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(104, 76);
            // 
            // miStartStop
            // 
            this.miStartStop.Name = "miStartStop";
            this.miStartStop.Size = new System.Drawing.Size(103, 22);
            this.miStartStop.Text = "Start";
            this.miStartStop.Click += new System.EventHandler(this.miStartStop_Click);
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            this.miOpen.Size = new System.Drawing.Size(103, 22);
            this.miOpen.Text = "Open";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(103, 22);
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // lblGit
            // 
            this.lblGit.AutoSize = true;
            this.lblGit.Font = new System.Drawing.Font("Consolas", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGit.LinkColor = System.Drawing.Color.Black;
            this.lblGit.Location = new System.Drawing.Point(526, 49);
            this.lblGit.Name = "lblGit";
            this.lblGit.Size = new System.Drawing.Size(29, 9);
            this.lblGit.TabIndex = 14;
            this.lblGit.TabStop = true;
            this.lblGit.Text = "GitHub";
            this.lblGit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblGit_LinkClicked);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(48, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(102, 19);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.Text = "AutoMailPrint";
            // 
            // chkSendMail
            // 
            this.chkSendMail.AutoSize = true;
            this.chkSendMail.Location = new System.Drawing.Point(6, 19);
            this.chkSendMail.Name = "chkSendMail";
            this.chkSendMail.Size = new System.Drawing.Size(137, 17);
            this.chkSendMail.TabIndex = 16;
            this.chkSendMail.Text = "Send error messages to";
            this.chkSendMail.UseVisualStyleBackColor = true;
            this.chkSendMail.CheckedChanged += new System.EventHandler(this.chkSendMail_CheckedChanged);
            // 
            // tbErrorAddress
            // 
            this.tbErrorAddress.Enabled = false;
            this.tbErrorAddress.Location = new System.Drawing.Point(143, 17);
            this.tbErrorAddress.Name = "tbErrorAddress";
            this.tbErrorAddress.Size = new System.Drawing.Size(179, 20);
            this.tbErrorAddress.TabIndex = 17;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnTestErrorMail);
            this.groupBox6.Controls.Add(this.chkSendMail);
            this.groupBox6.Controls.Add(this.tbErrorAddress);
            this.groupBox6.Location = new System.Drawing.Point(6, 246);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(330, 73);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Reports";
            // 
            // btnTestErrorMail
            // 
            this.btnTestErrorMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTestErrorMail.FlatAppearance.BorderSize = 0;
            this.btnTestErrorMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestErrorMail.Font = new System.Drawing.Font("Microsoft Tai Le", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestErrorMail.Location = new System.Drawing.Point(270, 43);
            this.btnTestErrorMail.Name = "btnTestErrorMail";
            this.btnTestErrorMail.Size = new System.Drawing.Size(52, 28);
            this.btnTestErrorMail.TabIndex = 19;
            this.btnTestErrorMail.Text = "Send test";
            this.btnTestErrorMail.UseVisualStyleBackColor = true;
            this.btnTestErrorMail.Click += new System.EventHandler(this.btnTestErrorMail_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::AutoMailPrint.Properties.Resources.ctlExit;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(532, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 21);
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imgIcon
            // 
            this.imgIcon.BackgroundImage = global::AutoMailPrint.Properties.Resources.ampico;
            this.imgIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgIcon.FlatAppearance.BorderSize = 0;
            this.imgIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imgIcon.Location = new System.Drawing.Point(4, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(38, 28);
            this.imgIcon.TabIndex = 16;
            this.imgIcon.TabStop = false;
            this.imgIcon.UseVisualStyleBackColor = true;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackgroundImage = global::AutoMailPrint.Properties.Resources.ctlMini;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Location = new System.Drawing.Point(505, 5);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(21, 21);
            this.btnMinimize.TabIndex = 15;
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnBMAC
            // 
            this.btnBMAC.BackgroundImage = global::AutoMailPrint.Properties.Resources.bmac;
            this.btnBMAC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBMAC.FlatAppearance.BorderSize = 0;
            this.btnBMAC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBMAC.Location = new System.Drawing.Point(524, 302);
            this.btnBMAC.Name = "btnBMAC";
            this.btnBMAC.Size = new System.Drawing.Size(23, 24);
            this.btnBMAC.TabIndex = 15;
            this.btnBMAC.UseVisualStyleBackColor = true;
            this.btnBMAC.Click += new System.EventHandler(this.btnBMAC_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.BackgroundImage = global::AutoMailPrint.Properties.Resources.btnStart;
            this.btnStartStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStartStop.FlatAppearance.BorderSize = 0;
            this.btnStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartStop.Location = new System.Drawing.Point(407, 229);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(88, 53);
            this.btnStartStop.TabIndex = 12;
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnTestMail
            // 
            this.btnTestMail.BackColor = System.Drawing.Color.Transparent;
            this.btnTestMail.BackgroundImage = global::AutoMailPrint.Properties.Resources.checkMailTest;
            this.btnTestMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTestMail.FlatAppearance.BorderSize = 0;
            this.btnTestMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestMail.Location = new System.Drawing.Point(293, 122);
            this.btnTestMail.Name = "btnTestMail";
            this.btnTestMail.Size = new System.Drawing.Size(27, 27);
            this.btnTestMail.TabIndex = 7;
            this.btnTestMail.UseVisualStyleBackColor = false;
            this.btnTestMail.Click += new System.EventHandler(this.btnTestMail_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 396);
            this.Controls.Add(this.lblGit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.tabControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoMailPrint";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKeepLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.MenuStrip1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbProtocol;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbMailServer;
        private System.Windows.Forms.Button btnTestMail;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbPrinters;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.CheckBox chkSSL;
        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView lvLogs;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbReader;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ContextMenuStrip MenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miStartStop;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel lblGit;
        private System.Windows.Forms.Button imgIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkAutostart;
        private System.Windows.Forms.NumericUpDown numKeepLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBMAC;
        private System.Windows.Forms.TextBox tbErrorAddress;
        private System.Windows.Forms.CheckBox chkSendMail;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnTestErrorMail;
    }
}

