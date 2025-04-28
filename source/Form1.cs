using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Xml.Serialization;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit;
using MimeKit;
using System.Timers;
using System.Diagnostics;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Drawing.Printing;
using MailKit.Security;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Interop;

namespace AutoMailPrint
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer _timer;
        private bool _isTimerRunning = false;
        private Point _mouseDownPos;
        private const string RegistryKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string AppName = "AutoMailPrint";
        private Dictionary<string, DateTime> _fileCreationTimes = new Dictionary<string, DateTime>();
        private readonly string appDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "AutoMailPrint"
    );
        private string _logFilePath;
        private string _attachmentsLogPath;
        private string _attachmentsDir;
        private string _settingsFilePath;
        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            // Pfade initialisieren
            _logFilePath = Path.Combine(appDataPath, "AMPlog.log");
            _attachmentsLogPath = Path.Combine(appDataPath, "AMPattachments.log");
            _attachmentsDir = Path.Combine(appDataPath, "attachments");
            _settingsFilePath = Path.Combine(appDataPath, "settings.xml");

            // Attachments-Ordner erstellen
            if (!Directory.Exists(_attachmentsDir))
                Directory.CreateDirectory(_attachmentsDir);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Printers();
            LoadSettingsFromFile();
            InitializeLog();
            LoadLogFile();
            InitializeListViewColumns();
            LoadLogToListView();
            LoadPdfReadersToComboBox();
            cbProtocol.SelectedIndex = 0;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownPos = new Point(e.X, e.Y);
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int deltaX = e.X - _mouseDownPos.X;
                int deltaY = e.Y - _mouseDownPos.Y;
                this.Location = new Point(Location.X + deltaX, Location.Y + deltaY);
            }
        }
        private void btnTestMail_Click(object sender, EventArgs e)
        {
            try
            {
                string server = tbMailServer.Text;
                int port = (int)numPort.Value;
                string username = tbUser.Text;
                string password = tbPassword.Text;
                bool useSsl = chkSSL.Checked;
                string protocol = cbProtocol.SelectedItem.ToString();

                if (protocol == "POP3")
                {
                    TestPop3Connection(server, port, username, password, useSsl);
                    btnTestMail.BackgroundImage = Properties.Resources.checkMailTest;
                    btnTestMail.Enabled = false;
                }
                else if (protocol == "IMAP")
                {
                    TestImapConnection(server, port, username, password, useSsl);
                    btnTestMail.BackgroundImage = Properties.Resources.checkMailTest;
                    btnTestMail.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please select a valid protocol.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}");
                Log($"Connection error: {ex.Message}");
            }
        }
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (!_isTimerRunning)
            {
                InitializeTimer((int)numInterval.Value);
                btnStartStop.BackgroundImage = Properties.Resources.btnStop;
                miStartStop.Text = "Stop";
                Start();
            }
            else
            {
                Stop();
                btnStartStop.BackgroundImage = Properties.Resources.btnStart;
                miStartStop.Text = "Start";
            }
            _isTimerRunning = !_isTimerRunning;
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notify.Visible = true;
        }
        private void miStartStop_Click(object sender, EventArgs e)
        {
            if (!_isTimerRunning)
            {
                InitializeTimer((int)numInterval.Value);
                btnStartStop.BackgroundImage = Properties.Resources.btnStop;
                miStartStop.Text = "Stop";
                Start();
            }
            else
            {
                Stop();
                btnStartStop.BackgroundImage = Properties.Resources.btnStart;
                miStartStop.Text = "Start";
            }
            _isTimerRunning = !_isTimerRunning;
        }
        private void btnPrinters_Click(object sender, EventArgs e)
        {
            Printers();
        }
        private void miOpen_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
        private void miExit_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }
        private void lblLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(_logFilePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _logFilePath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Die Log-Datei wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblAttch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(_attachmentsLogPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _attachmentsLogPath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Log file was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblGit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openUrl();
        }
        private void notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notify.Visible = false;
        }
        private void btnTestErrorMail_Click(object sender, EventArgs e)
        {
            SendMail("Test performed successfully!\n\nYou will now be informed about critical errors.");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }
        private void imgIcon_Click(object sender, EventArgs e)
        {
            openUrl();
        }
        private void btnBMAC_Click(object sender, EventArgs e)
        {
            payPalMe();
        }
        private void lblDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            payPalMe();
        }
        private void btnReloadReader_Click(object sender, EventArgs e)
        {
            LoadPdfReadersToComboBox();
        }
        private void payPalMe()
        {
            string url = "https://www.paypal.com/paypalme/tuefteltyp";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        private void openUrl()
        {
            string url = "https://github.com/TueftelTyp/AutoMailPrint";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        private void Start()
        {
            tbMailServer.Enabled = false;
            numPort.Enabled = false;
            tbUser.Enabled = false;
            tbPassword.Enabled = false;
            chkSSL.Enabled = false;
            cbProtocol.Enabled = false;
            cbPrinters.Enabled = false;
            numInterval.Enabled = false;
            cbReader.Enabled = false;
            cbDefaultReader.Enabled = false;
            btnTestMail.Enabled = false;
            btnTestMail.BackgroundImage = Properties.Resources.checkMailTest;
            chkAutostart.Enabled = false;
            numKeepLog.Enabled = false;
            chkSendMail.Enabled = false;
            tbErrorAddress.Enabled = false;
            btnPrinters.Enabled = false;
            btnTestErrorMail.Enabled = false;
            numPrintTimeout.Enabled = false;
            btnReloadReader.Enabled = false;
            SaveDataToFile();
        }
        private void InitializeTimer(int intervalMinutes)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            _timer = new System.Timers.Timer(intervalMinutes * 60 * 1000); // Konvertiere Minuten in Millisekunden
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }
        private void Stop()
        {
            tbMailServer.Enabled = true;
            numPort.Enabled = true;
            tbUser.Enabled = true;
            tbPassword.Enabled = true;
            chkSSL.Enabled = true;
            cbProtocol.Enabled = true;
            cbPrinters.Enabled = true;
            numInterval.Enabled = true;
            cbReader.Enabled = true;
            cbDefaultReader.Enabled = true;
            btnTestMail.Enabled = true;
            btnTestMail.BackgroundImage = Properties.Resources.checkMail;
            chkAutostart.Enabled = true;
            numKeepLog.Enabled = true;
            chkSendMail.Enabled = true;
            tbErrorAddress.Enabled = true;
            btnPrinters.Enabled = true;
            btnTestErrorMail.Enabled= true;
            numPrintTimeout.Enabled = true;
            btnReloadReader.Enabled = true;
            StopTimer();
        }
        private void StopTimer()
        {
            if (_timer != null && _timer.Enabled)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }
        private void ExitApplication()
        {
            if (MessageBox.Show("Do you really want to exit the application?\nThis will stop the function.", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                StopTimer();
                Application.Exit();
            }
        }
        private void Printers()
        {
            cbPrinters.Items.Clear();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbPrinters.Items.Add(printer);
            }
            if (cbPrinters.Items.Count > 0)
            {
                cbPrinters.SelectedIndex = 0;
            }
        }
        private void TestPop3Connection(string server, int port, string username, string password, bool useSsl)
        {
            Task.Run(async () =>
            {
                try
                {
                    using (var client = new Pop3Client())
                    {
                        client.Timeout = 5000;
                        await client.ConnectAsync(server, port, useSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTlsWhenAvailable);
                        await client.AuthenticateAsync(username, password);

                        this.Invoke((MethodInvoker)delegate {
                            btnTestMail.BackgroundImage = Properties.Resources.checkMailOk;
                            btnTestMail.Enabled = true;
                            Log("POP3-Connection successfully established!");
                        });

                        await client.DisconnectAsync(true);
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate {
                        btnTestMail.BackgroundImage = Properties.Resources.checkMailError;
                        btnTestMail.Enabled = true;
                        Log($"POP3-Connection failed: {ex.Message}");
                    });
                }
            });
        }
        private void TestImapConnection(string server, int port, string username, string password, bool useSsl)
        {
            Task.Run(async () =>
            {
                try
                {
                    using (var client = new ImapClient())
                    {
                        client.Timeout = 5000;
                        await client.ConnectAsync(server, port, useSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTlsWhenAvailable);
                        await client.AuthenticateAsync(username, password);

                        this.Invoke((MethodInvoker)delegate {
                            btnTestMail.BackgroundImage = Properties.Resources.checkMailOk;
                            btnTestMail.Enabled = true;
                            Log("IMAP-Connection successfully established!");
                        });

                        await client.DisconnectAsync(true);
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate {
                        btnTestMail.BackgroundImage = Properties.Resources.checkMailError;
                        btnTestMail.Enabled = true;
                        Log($"IMAP-Connection failed: {ex.Message}");
                    });
                }
            });
        }
        private void SaveDataToFile()
        {
            try
            {
                var settings = new AutoMailPrintSettings
                {
                    MailServer = tbMailServer.Text,
                    Port = (int)numPort.Value,
                    User = tbUser.Text,
                    Password = tbPassword.Text,
                    SSL = chkSSL.Checked,
                    Protocol = cbProtocol.SelectedItem?.ToString(),
                    SelectedPrinter = cbPrinters.SelectedItem?.ToString(),
                    Interval = (int)numInterval.Value,
                    SelectedReader = cbReader.SelectedItem?.ToString(),
                    DaysToKeepLogs = (int)numKeepLog.Value,
                    SendMail = chkSendMail.Checked,
                    ErrorAddress = tbErrorAddress.Text,
                    PrintTimeoutSeconds = (int)numPrintTimeout.Value,
                    UseDefaultPdfReader = cbDefaultReader.Checked
                };

                var serializer = new XmlSerializer(typeof(AutoMailPrintSettings));
                using (var writer = new StreamWriter(_settingsFilePath))
                {
                    serializer.Serialize(writer, settings);
                }
                Log("Saving successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Saving failed: {ex.Message}");
                Log($"Saving failed: {ex.Message}");
            }
        }
        [Serializable]
        public class AutoMailPrintSettings
        {
            public string MailServer { get; set; }
            public int Port { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public bool SSL { get; set; }
            public string Protocol { get; set; }
            public string SelectedPrinter { get; set; }
            public int Interval { get; set; }
            public string SelectedReader { get; set; }
            public int DaysToKeepLogs { get; set; }
            public bool SendMail { get; set; }
            public string ErrorAddress { get; set; }
            public int PrintTimeoutSeconds { get; set; }
            public bool UseDefaultPdfReader { get; set; }
        }
        private void LoadSettingsFromFile()
        {
            if (File.Exists(_settingsFilePath))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(AutoMailPrintSettings));
                    using (var reader = new StreamReader(_settingsFilePath))
                    {
                        var settings = (AutoMailPrintSettings)serializer.Deserialize(reader);
                        // Felder mit den geladenen Daten aktualisieren
                        tbMailServer.Text = settings.MailServer;
                        numPort.Value = settings.Port;
                        tbUser.Text = settings.User;
                        tbPassword.Text = settings.Password;
                        chkSSL.Checked = settings.SSL;
                        cbProtocol.SelectedItem = settings.Protocol;
                        numInterval.Value = settings.Interval;
                        numKeepLog.Value = settings.DaysToKeepLogs;
                        chkSendMail.Checked = settings.SendMail;
                        tbErrorAddress.Text = settings.ErrorAddress;
                        cbDefaultReader.Checked = settings.UseDefaultPdfReader;
                        if (settings.PrintTimeoutSeconds > 0 && settings.PrintTimeoutSeconds <= numPrintTimeout.Maximum)
                        {
                            numPrintTimeout.Value = settings.PrintTimeoutSeconds;
                        }
                        else
                        {
                            numPrintTimeout.Value = 20; // Default
                        }
                        // Drucker auswählen, falls vorhanden
                        if (!string.IsNullOrEmpty(settings.SelectedPrinter) && cbPrinters.Items.Contains(settings.SelectedPrinter))
                        {
                            cbPrinters.SelectedItem = settings.SelectedPrinter;
                        }
                        // Reader auswählen, falls vorhanden
                        if (!string.IsNullOrEmpty(settings.SelectedReader) && cbReader.Items.Contains(settings.SelectedReader))
                        {
                            cbReader.SelectedItem = settings.SelectedReader;
                        }
                        tbMailServer.Enabled = false;
                        numPort.Enabled = false;
                        tbUser.Enabled = false;
                        tbPassword.Enabled = false;
                        chkSSL.Enabled = false;
                        cbProtocol.Enabled = false;
                        cbPrinters.Enabled = false;
                        cbReader.Enabled = false;
                        numInterval.Enabled = false;
                        btnTestMail.Enabled = false;
                        chkAutostart.Enabled = false;
                        numKeepLog.Enabled = false;
                        chkSendMail.Enabled = false;
                        tbErrorAddress.Enabled = false;
                        btnPrinters.Enabled = false;
                        btnTestErrorMail.Enabled = false;
                        numPrintTimeout.Enabled = false;
                        btnReloadReader.Enabled = false;
                        cbDefaultReader.Enabled = false;
                        Log("Settings loaded successfully");
                        InitializeTimer((int)numInterval.Value);
                        _isTimerRunning = true;
                        btnStartStop.BackgroundImage = Properties.Resources.btnStop;
                        miStartStop.Text = "Stop";

                    }
                }
                catch (Exception ex)
                {
                    Log($"Failed to load the settings: {ex.Message}");
                }
            }
        }
        private void ProcessEmailAttachments()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ProcessEmailAttachments));
                return;
            }

            try
            {
                string server = tbMailServer.Text;
                int port = (int)numPort.Value;
                string username = tbUser.Text;
                string password = tbPassword.Text;
                bool useSsl = chkSSL.Checked;
                string protocol = cbProtocol.SelectedItem?.ToString();
                string attachmentsFolder = _attachmentsDir;

                if (!Directory.Exists(attachmentsFolder))
                {
                    Directory.CreateDirectory(attachmentsFolder);
                }

                if (protocol == "POP3")
                {
                    using (var client = new Pop3Client())
                    {
                        try
                        {
                            client.Connect(server, port, useSsl);
                            client.Authenticate(username, password);
                        }
                        catch (SocketException ex)
                        {
                            SendMail("Connection to the mail server failed: " + ex.Message);
                            Log(ex.Message);
                            return;
                        }
                        catch (AuthenticationException ex)
                        {
                            SendMail("Authentication failed: " + ex.Message);
                            Log(ex.Message);
                            return;
                        }
                        catch (Exception ex)
                        {
                            SendMail("Unknown error: " + ex.Message);
                            Log(ex.Message);
                            return;
                        }

                        // Anzahl der Nachrichten vor der Verarbeitung abrufen
                        int messageCount = client.Count;

                        for (int i = messageCount - 1; i >= 0; i--)
                        {
                            try
                            {
                                var message = client.GetMessage(i);
                                SavePdfAttachments(message, attachmentsFolder);
                                AddAttachmentFilenamesToListView(attachmentsFolder);
                                // Nachricht zum Löschen markieren
                                client.DeleteMessage(i);
                            }
                            catch (Exception ex)
                            {
                                string msg = ($"Error processing or deleting the message {i}: {ex.Message}");
                                Log(msg);
                                SendMail(msg);
                            }
                        }

                        // Alle zum Löschen markierten Nachrichten endgültig löschen
                        client.Disconnect(true);
                    }
                }
                else if (protocol == "IMAP")
                {
                    using (var client = new ImapClient())
                    {
                        try
                        {
                            client.Connect(server, port, useSsl);
                            client.Authenticate(username, password);
                        }
                        catch (SocketException ex)
                        {
                            SendMail("Connection to the mail server failed: " + ex.Message);
                            Log(ex.Message);
                            return;
                        }
                        catch (AuthenticationException ex)
                        {
                            SendMail("Authentication failed: " + ex.Message);
                            Log(ex.Message);
                            return;
                        }
                        catch (Exception ex)
                        {
                            SendMail("Unknown error: " + ex.Message);
                            Log(ex.Message);
                            return;
                        }

                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadWrite);
                        for (int i = inbox.Count - 1; i >= 0; i--)
                        {
                            try
                            {
                                var message = inbox.GetMessage(i);
                                // Anhänge extrahieren und Dateinamen zur Listview hinzufügen
                                SavePdfAttachments(message, attachmentsFolder);
                                AddAttachmentFilenamesToListView(attachmentsFolder);
                                // Nachricht löschen
                                inbox.AddFlags(i, MessageFlags.Deleted, true);
                            }
                            catch (Exception ex)
                            {
                                string msg = ($"Error processing or deleting the message {i}: {ex.Message}");
                                Log(msg);
                                SendMail(msg);
                            }
                        }
                        inbox.Expunge(); // Löscht die als gelöscht markierten Nachrichten permanent

                        client.Disconnect(true);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid protocol.");
                    return;
                }
                // Dateien drucken und löschen
                PrintAndDeleteFiles(attachmentsFolder);
            }
            catch (Exception ex)
            {
                string msg = ($"Error when processing e-mails: {ex.Message}");
                MessageBox.Show(msg);
                Log(msg);
                SendMail(msg);
            }
        }
        private void InitializeListViewColumns()
        {
            lvData.View = View.Details;
            lvData.Columns.Clear();
            lvData.Columns.Add("Date/Time", 130);
            lvData.Columns.Add("Filename", 900);
        }
        private void AddAttachmentFilenamesToListView(string attachmentsFolder)
        {
            try
            {
                if (Directory.Exists(attachmentsFolder))
                {
                    using (var logWriter = new StreamWriter(_attachmentsLogPath, true))
                    {
                        string[] files = Directory.GetFiles(attachmentsFolder);

                        foreach (string file in files)
                        {
                            string filename = Path.GetFileName(file);
                            DateTime creationTime = File.GetCreationTime(file);
                            string formattedDateTime = creationTime.ToString("yyyy.MM.dd HH:mm");
                            // ListView-Eintrag
                            var item = new ListViewItem(formattedDateTime);
                            item.SubItems.Add(filename);
                            lvData.Items.Add(item);
                            item.EnsureVisible();
                            logWriter.WriteLine($"{formattedDateTime}-{filename}");
                        }
                    }
                }
                else
                {
                    Log($"Folder not found: {attachmentsFolder}");
                }
            }
            catch (Exception ex)
            {
                string msg = ($"Error: {ex.Message}");
                Log(msg);
                SendMail(msg);
            }
        }
        private void LoadLogToListView()
        {
            if (!File.Exists(_attachmentsLogPath)) return;

            try
            {
                foreach (string line in File.ReadAllLines(_attachmentsLogPath))
                {
                    string[] parts = line.Split('-');
                    if (parts.Length == 2)
                    {
                        var item = new ListViewItem(parts[0]);
                        item.SubItems.Add(parts[1]);
                        lvData.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Error loading the log: {ex.Message}");
            }
        }
        private void SavePdfAttachments(MimeMessage message, string attachmentsFolder)
        {
            foreach (var attachment in message.Attachments)
            {
                if (attachment is MimePart mimePart && mimePart.ContentType.MimeType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                {
                    string filePath = Path.Combine(attachmentsFolder, mimePart.FileName ?? "attachment.pdf");

                    using (var stream = File.Create(filePath))
                    {
                        mimePart.Content.DecodeTo(stream);
                    }
                }
            }
        }
        void PrintAndDeleteFiles(string attachmentsFolder)
        {
            var printerName = cbPrinters.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(printerName))
            {
                MessageBox.Show("Please select a printer.");
                Log("No printer selected");
                return;
            }

            // Prüfe, ob der Drucker valide ist
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = printerName;
            if (!pd.PrinterSettings.IsValid)
            {
                string msg = ($"The attachment(s) were saved but NOT printed.\nPrinter '{printerName}' is not valid!\nPlease check!");
                Log(msg);
                SendMail(msg);
                return;
            }

            foreach (var filePath in Directory.GetFiles(attachmentsFolder, "*.pdf"))
            {
                try
                {
                    // Verwende den Standard-PDF-Reader, um die Datei zu drucken
                    PrintPdf(filePath, printerName, (int)numPrintTimeout.Value);

                    // Lösche die Datei nach dem Drucken
                    File.Delete(filePath);
                    string fileName = Path.GetFileName(filePath);
                    Log($"'{fileName}' printed and deleted");
                }
                catch (Exception ex)
                {
                    string fileName = Path.GetFileName(filePath);
                    string msg = ($"Error when printing '{fileName}': {ex.Message}");
                    Log(msg);
                    SendMail(msg);
                }
            }
        }
        void PrintPdf(string pdfPath, string printerName, int maxWaitSeconds)
        {
            try
            {
                ProcessStartInfo processStartInfo;

                // Prüfe, ob der Standard-Reader verwendet werden soll
                if (cbDefaultReader.Checked)
                {
                    // Standard-Programm über Shell (wie gehabt)
                    processStartInfo = new ProcessStartInfo
                    {
                        FileName = pdfPath,
                        Verb = "printto",
                        Arguments = $"\"{printerName}\"",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        UseShellExecute = true
                    };
                }
                else
                {
                    // Benutzerdefiniertes Programm aus ComboBox verwenden
                    string selectedReader = cbReader.SelectedItem?.ToString();
                    if (string.IsNullOrEmpty(selectedReader) || !File.Exists(selectedReader))
                    {
                        string msg = ("No valid PDF program selected!");
                        SendMail(msg);
                        Log(msg);
                        throw new Exception("No valid PDF program selected!");
                    }


                    // Die meisten PDF-Reader unterstützen die /t-Option für Drucken
                    // Beispiel für Adobe Reader: "<ReaderExe>" /t "<PDF>" "<Printer>"
                    processStartInfo = new ProcessStartInfo
                    {
                        FileName = selectedReader,
                        Arguments = $"\"{pdfPath}\" /t \"{printerName}\"",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        UseShellExecute = false // Direktes Starten des Readers
                    };
                }

                using (var process = Process.Start(processStartInfo))
                {
                    if (process != null)
                    {
                        int maxWaitMs = maxWaitSeconds * 1000;
                        int waitIntervalMs = 500;
                        int waited = 0;

                        while (!process.HasExited && waited < maxWaitMs)
                        {
                            System.Threading.Thread.Sleep(waitIntervalMs);
                            waited += waitIntervalMs;
                        }

                        if (!process.HasExited)
                        {
                            process.Kill();
                        }
                    }
                }

                string fileName = Path.GetFileName(pdfPath);
                Log($"'{fileName}' successfully sent to '{printerName}'.");
            }
            catch (Exception ex)
            {
                string msg = ($"Error printing the file {pdfPath}: {ex.Message}");
                SendMail(msg);
                Log(msg);
                throw new Exception($"Error printing the file {pdfPath}: {ex.Message}");
            }
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            ProcessEmailAttachments();
            int daysToKeep = (int)numKeepLog.Value;
            string[] logFiles = { _attachmentsLogPath, _logFilePath };

            foreach (string filePath in logFiles)
            {
                ClearOldLogFiles(filePath, daysToKeep);
            }
        }
        private void InitializeLog()
        {
            if (!File.Exists(_logFilePath))
            {
                try
                {
                    File.Create(_logFilePath).Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating the log file: {ex.Message}");
                }
            }
            lvLogs.View = View.Details;
            lvLogs.Columns.Add("Date/Time", 130);
            lvLogs.Columns.Add("Message", 900);
            lvLogs.FullRowSelect = true;
        }
        private void LoadLogFile()
        {
            try
            {
                if (File.Exists(_logFilePath))
                {
                    using (StreamReader reader = new StreamReader(_logFilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(new string[] { " - " }, StringSplitOptions.None);
                            if (parts.Length == 2)
                            {
                                string timestamp = parts[0];
                                string message = parts[1];

                                ListViewItem item = new ListViewItem(timestamp);
                                item.SubItems.Add(message);
                                lvLogs.Items.Add(item);
                                item.EnsureVisible();
                            }
                            else
                            {
                                Console.WriteLine($"Ungültiger Log-Eintrag: {line}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading log file:{ex.Message}");
            }
        }
        private void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

            try
            {
                // In Datei schreiben
                using (StreamWriter writer = File.AppendText(_logFilePath))
                {
                    writer.WriteLine($"{timestamp} - {message}");
                }
                // In ListView schreiben
                if (lvLogs.InvokeRequired)
                {
                    lvLogs.Invoke(new Action(() =>
                    {
                        ListViewItem item = new ListViewItem(timestamp);
                        item.SubItems.Add(message);
                        lvLogs.Items.Add(item);
                        item.EnsureVisible();
                    }));
                }
                else
                {
                    ListViewItem item = new ListViewItem(timestamp);
                    item.SubItems.Add(message);
                    lvLogs.Items.Add(item);
                    item.EnsureVisible();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing to the log: {ex.Message}");
            }
        }
        private void ChkAutostart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutostart.Checked)
            {
                EnableAutostart();
            }
            else
            {
                DisableAutostart();
            }
        }
        private void EnableAutostart()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(RegistryKeyPath, true))
                {
                    if (key == null)
                    {
                        MessageBox.Show("Failed to access the registry. Please run the application as administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkAutostart.Checked = false;
                        return;
                    }

                    string executablePath = Application.ExecutablePath;
                    key.SetValue(AppName, $"\"{executablePath}\"");
                    Log("Autostart enabled.");
                }
            }
            catch (Exception ex)
            {
                Log($"Failed to enable autostart: {ex.Message}");
                chkAutostart.Checked = false;
            }
        }
        private void DisableAutostart()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(RegistryKeyPath, true))
                {
                    if (key == null)
                    {
                        MessageBox.Show("Failed to access the registry. Please run the application as administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    key.DeleteValue(AppName, false);
                    Log("Autostart disabled.");
                }
            }
            catch (Exception ex)
            {
                Log($"Failed to disable autostart: {ex.Message}");
            }
        }
        private void ClearOldLogFiles(string filePath, int daysToKeep)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    // Initial Erstellungszeit speichern
                    if (!_fileCreationTimes.ContainsKey(filePath))
                    {
                        _fileCreationTimes[filePath] = File.GetCreationTime(filePath);
                    }

                    DateTime creationTime = _fileCreationTimes[filePath];
                    DateTime nextClearDate = creationTime.AddDays(daysToKeep);

                    // Neuberechnung bei jeder Ausführung
                    while (DateTime.Now >= nextClearDate)
                    {
                        File.WriteAllText(filePath, string.Empty);
                        Log($"{filePath} cleared (Next clean-up: {nextClearDate})");

                        // ListView leeren, basierend auf Dateipfad
                        if (filePath == _logFilePath)
                        {
                            lvLogs.Items.Clear();
                        }
                        else if (filePath == _attachmentsLogPath)
                        {
                            lvData.Items.Clear();
                        }

                        // nächste Leerung berechnen
                        creationTime = nextClearDate; // Neuer Referenzzeitpunkt
                        nextClearDate = nextClearDate.AddDays(daysToKeep);
                        _fileCreationTimes[filePath] = creationTime; // aktualisieren
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Error when clearing the log file {filePath}: {ex.Message}");
            }
        }
        private void chkSendMail_CheckedChanged(object sender, EventArgs e)
        {
            tbErrorAddress.Enabled = chkSendMail.Checked;
            btnTestErrorMail.Enabled = chkSendMail.Checked;
        }
        private void SendMail(string body)
        {
            if (chkSendMail.Checked)
            {
                try
                {
                    SmtpClient client = new SmtpClient(tbMailServer.Text, 25);
                    client.EnableSsl = false;
                    client.Credentials = new NetworkCredential(tbUser.Text, tbPassword.Text);
                    string hostname = System.Environment.MachineName;
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(tbUser.Text);
                    mailMessage.To.Add(tbErrorAddress.Text);
                    mailMessage.Subject = $"AutoMailPrint on {hostname}";
                    string signature = $"\n\n\n---\nHostname: {hostname}\n{timestamp}\n\n-AutoMailPrint-\nby TueftelTyp";
                    mailMessage.Body = body + signature;


                    client.Send(mailMessage);

                    Log("Mail was successfully sent to: " + tbErrorAddress.Text);
                }
                catch (Exception ex)
                {
                    Log("Mail could not be sent: " + ex.Message);
                }
            }
        }
        private void LoadPdfReadersToComboBox()
        {
            cbReader.Items.Clear();

            // 1. Standard-PDF-Reader ermitteln
            string defaultReaderName = GetDefaultPdfReaderName();
            if (!string.IsNullOrEmpty(defaultReaderName))
            {
                cbReader.Items.Add(defaultReaderName);
            }

            // 2. Weitere PDF-Reader aus OpenWithProgIDs hinzufügen
            try
            {
                using (RegistryKey openWithKey = Registry.ClassesRoot.OpenSubKey(".pdf\\OpenWithProgIDs"))
                {
                    if (openWithKey != null)
                    {
                        foreach (var progId in openWithKey.GetValueNames())
                        {
                            string appName = GetAppNameFromProgId(progId);
                            if (!string.IsNullOrEmpty(appName) && !cbReader.Items.Contains(appName))
                            {
                                cbReader.Items.Add(appName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der PDF-Reader: " + ex.Message);
            }

            if (cbReader.Items.Count > 0)
                cbReader.SelectedIndex = 0;
        }
        private string GetDefaultPdfReaderName()
        {
            try
            {
                using (RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.pdf\UserChoice"))
                {
                    if (userChoiceKey != null)
                    {
                        string progId = userChoiceKey.GetValue("ProgId") as string;
                        if (!string.IsNullOrEmpty(progId))
                        {
                            // Pfad zum Programm aus ProgID ermitteln
                            using (RegistryKey commandKey = Registry.ClassesRoot.OpenSubKey(progId + @"\shell\open\command"))
                            {
                                if (commandKey != null)
                                {
                                    string command = commandKey.GetValue(null) as string;
                                    if (!string.IsNullOrEmpty(command))
                                    {
                                        // Pfad aus dem command extrahieren
                                        string exePath = ExtractExePath(command);
                                        if (!string.IsNullOrEmpty(exePath) && System.IO.File.Exists(exePath))
                                        {
                                            // Dateiname als Programmname
                                            return System.IO.Path.GetFileNameWithoutExtension(exePath);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Fehler ignorieren und null zurückgeben
            }
            return null;
        }
        private string ExtractExePath(string command)
        {
            // Beispiel command: "\"C:\\Program Files (x86)\\Adobe\\Acrobat Reader DC\\Reader\\AcroRd32.exe\" /n /s /o /h \"%1\""
            var match = Regex.Match(command, "\"([^\"]+)\"");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                // Falls keine Anführungszeichen, nimm den ersten Teil bis zum Leerzeichen
                int index = command.IndexOf(' ');
                if (index > 0)
                    return command.Substring(0, index);
                else
                    return command;
            }
        }
        private string GetAppNameFromProgId(string progId)
        {
            try
            {
                using (RegistryKey progIdKey = Registry.ClassesRoot.OpenSubKey(progId + "\\Application"))
                {
                    if (progIdKey != null)
                    {
                        var appName = progIdKey.GetValue("ApplicationName") as string;
                        if (!string.IsNullOrEmpty(appName))
                            return appName;
                    }
                }
            }
            catch
            {
                // Ignorieren
            }
            return progId; // Fallback: ProgID als Name
        }
        private void cbDefaultReader_CheckedChanged(object sender, EventArgs e)
        {
            cbReader.Enabled = !cbDefaultReader.Checked;
        }

        
    }
}