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

namespace AutoMailPrint
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer _timer;
        private bool _isTimerRunning = false;
        private string _logFilePath = "AMPlog.log";
        private Point _mouseDownPos;
        private const string RegistryKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string AppName = "AutoMailPrint";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Printers();
            LoadSettingsFromFile();
            InitializeLog();
            LoadLogFile();
            InitializeListViewColumns();
            LoadLogToListView();
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
                }
                else if (protocol == "IMAP")
                {
                    TestImapConnection(server, port, username, password, useSsl);
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
            // Aktuelles Arbeitsverzeichnis ermitteln
            string currentDirectory = Directory.GetCurrentDirectory();

            // Pfad zur Log-Datei erstellen
            string logFilePath = Path.Combine(currentDirectory, "AMPlog.log");

            // Überprüfen, ob die Datei existiert
            if (File.Exists(logFilePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = logFilePath,
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
            // Aktuelles Arbeitsverzeichnis ermitteln
            string currentDirectory = Directory.GetCurrentDirectory();
            // Pfad zur erstellen
            string logFilePath = Path.Combine(currentDirectory, "AMPattachments.log");
            if (File.Exists(logFilePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = logFilePath,
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }
        private void imgIcon_Click(object sender, EventArgs e)
        {
            openUrl();
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
            btnTestMail.Enabled = false;
            btnTestMail.BackColor = Color.Transparent;
            chkAutostart.Enabled = false;
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
            btnTestMail.Enabled = true;
            btnTestMail.BackColor = Color.Transparent;
            chkAutostart.Enabled = true;
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
            using (var client = new System.Net.Mail.SmtpClient())
            {
                client.Host = server;
                client.Port = port;
                client.EnableSsl = useSsl;
                client.Credentials = new NetworkCredential(username, password);
                client.Timeout = 5000;
                // Testverbindung durchführen
                client.SendCompleted += (s, e) => {
                    if (e.Error != null)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            btnTestMail.BackColor = Color.Red;
                            Log($"POP3-Connection failed: {e.Error.Message}");
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate {
                            btnTestMail.BackColor = Color.Green;
                            Log("POP3-Connection successfully established!");
                        });
                    }
                };
                // Leere Testmail senden
                var msg = new MailMessage(username, username, "POP3 Test", "");
                client.SendAsync(msg, null);
            }
        }
        private void TestImapConnection(string server, int port, string username, string password, bool useSsl)
        {
            try
            {
                using (var client = new MailKit.Net.Imap.ImapClient())
                {
                    client.Connect(server, port, useSsl);
                    client.Authenticate(username, password);

                    if (client.IsConnected && client.IsAuthenticated)
                    {
                        btnTestMail.BackColor = Color.Green;
                        Log("IMAP-Connection successfully established!");
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                btnTestMail.BackColor = Color.Red;
                Log($"IMAP-Connection failed: {ex.Message}");
            }
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
                    SelectedReader = cbReader.SelectedItem?.ToString()
                };

                var serializer = new XmlSerializer(typeof(AutoMailPrintSettings));
                using (var writer = new StreamWriter("settings.xml"))
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
        }
        private void LoadSettingsFromFile()
        {
            if (File.Exists("settings.xml"))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(AutoMailPrintSettings));
                    using (var reader = new StreamReader("settings.xml"))
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
                string attachmentsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "attachments");

                if (!Directory.Exists(attachmentsFolder))
                {
                    Directory.CreateDirectory(attachmentsFolder);
                }

                if (protocol == "POP3")
                {
                    using (var client = new Pop3Client())
                    {
                        client.Connect(server, port, useSsl);
                        client.Authenticate(username, password);
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
                                Log($"Error processing or deleting the message {i}: {ex.Message}");
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
                        client.Connect(server, port, useSsl);
                        client.Authenticate(username, password);

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
                                Log($"Error processing or deleting the message {i}: {ex.Message}");
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
                MessageBox.Show($"Error when processing e-mails: {ex.Message}");
                Log($"Error when processing e-mails: {ex.Message}");
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
            lvData.Items.Clear();
            const string logFile = "AMPattachments.log";

            try
            {
                if (Directory.Exists(attachmentsFolder))
                {
                    using (var logWriter = new StreamWriter(logFile, true))
                    {
                        string[] files = Directory.GetFiles(attachmentsFolder);

                        foreach (string file in files)
                        {
                            string filename = Path.GetFileName(file);
                            DateTime creationTime = File.GetCreationTime(file);
                            string formattedDateTime = creationTime.ToString("yyyy.MM.dd HH:mm");
                            // ListView-Eintrag
                            var item = new ListViewItem(filename);
                            item.SubItems.Add(formattedDateTime);
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
                Log($"Error: {ex.Message}");
            }
        }
        private void LoadLogToListView()
        {
            const string logFile = "AMPattachments.log";

            if (!File.Exists(logFile)) return;

            try
            {
                foreach (string line in File.ReadAllLines(logFile))
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

            foreach (var filePath in Directory.GetFiles(attachmentsFolder, "*.pdf"))
            {
                try
                {
                    // Verwende den Standard-PDF-Reader, um die Datei zu drucken
                    PrintPdfWithDefaultReader(filePath, printerName);

                    // Lösche die Datei nach dem Drucken
                    File.Delete(filePath);
                    string fileName = Path.GetFileName(filePath);
                    Log($"{fileName} printed and deleted");
                }
                catch (Exception ex)
                {
                    string fileName = Path.GetFileName(filePath);
                    Log($"Error when printing {fileName}: {ex.Message}");
                }
            }
        }
        void PrintPdfWithDefaultReader(string pdfPath, string printerName)
        {
            try
            {
                // Starte den Standard-PDF-Reader mit Druckbefehl
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = pdfPath,
                    Verb = "printto", 
                    Arguments = $"\"{printerName}\"",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true // Verwende die Shell, um den Standard-Reader zu starten
                };

                using (var process = Process.Start(processStartInfo))
                {
                    if (process != null)
                    {
                        process.WaitForExit(20000); // Warte maximal 20 Sekunden auf den Abschluss
                        if (!process.HasExited)
                        {
                            process.Kill(); // Beende falls es hängt
                        }
                    }
                }

                string fileName = Path.GetFileName(pdfPath);
                Log($"{fileName} successfully sent to {printerName}.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error printing the file {pdfPath}: {ex.Message}");
            }
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            ProcessEmailAttachments();
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
    }
}