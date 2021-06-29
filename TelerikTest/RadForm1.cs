using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.FormatProviders.OpenXml.Docx;
using Telerik.WinForms.Documents.Model;
using TelerikTest.Properties;
using Awesomium.Windows.Forms;
using b2xtranslator.StructuredStorage.Reader;
using b2xtranslator.DocFileFormat;
using b2xtranslator.OpenXmlLib.WordprocessingML;
using b2xtranslator.WordprocessingMLMapping;
using MailKit;
using MailKit.Net.Imap;
using Microsoft.Office.Interop.Word;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using static b2xtranslator.OpenXmlLib.OpenXmlPackage;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.WinForms.Documents.FormatProviders.Txt;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using Word = Microsoft.Office.Interop.Word;


namespace TelerikTest
{
    public class CustomRichTextEditorRibbonBar : RichTextEditorRibbonBar
    {
        protected override void Initialize()
        {
            base.Initialize();
            buttonSaveHTML.Visible = false;
            buttonSavePlain.Visible = false;
            buttonSaveRich.Visible = false;
            buttonXAML.Visible = false;
            CloseButton = false;
            MaximizeButton = false;
            MinimizeButton = false;
            LayoutMode = RibbonLayout.Simplified;
            BuiltInStylesVersion = Telerik.WinForms.Documents.Model.Styles.BuiltInStylesVersion.Office2013;
            ThemeName = "Office2013Light";
        }
    }
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        private List<string> _foldersList = new List<string>();
        private List<string> _filesList = new List<string>();
        private string ConnectedMail = "";

        Thread receiveThread;
        static string userName;
        static string key;
        private const string host = "127.0.0.1";//"95.165.142.183";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;

        private ImapClient mailclient = new ImapClient();
        private string pass = "";

        private readonly string[] _formats = new[]
        {
            "rtf", "txt", "doc", "docm", "docx", "dot", "dotm", "dotx", "htm", "html", "mht", "mhtml", "odt", "xls",
            "xlsx", "xlsm", "pdf"
        };

        public RadForm1()
        {
            InitializeComponent();
            //Подтягиваем сохраненные email
            var s = File.ReadAllLines("SavedMail.txt");
            try
            {
                loginbox.DataSource = s.Select(x => new { StrValue = x.Substring(0, x.IndexOf(" ")) }).ToList(); ;
                loginbox.Columns[0].HeaderText = "Мои аккаунты";
                loginbox.Columns[0].Width = loginbox.Width;
                loginbox.Text = "";
                passbox.Text = "";
            }
            catch
            {

            }
            //Подтягиваем ключи
            CheckForIllegalCrossThreadCalls = false;
            s = File.ReadAllLines("keys.txt");
            try
            {
                keybox.DataSource = s.Select(x => new { StrValue = x }).ToList(); ;
                keybox.Columns[0].HeaderText = "Сохраненные ключи";
                keybox.Columns[0].Width = keybox.Width;
                keybox.Text = "";
            }
            catch
            {

            }
            // ДОБАВЛЕНИЕ ПАПКИ ЗАГРУЗКИ
            _foldersList.Add(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString());
            imageList1.Images.Add(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString(), Resources.download);
            foldersPanel.Items.Add("Загрузки", imageList1.Images.Count - 1);
            // END ДОБАВЛЕНИЕ ПАПКИ ЗАГРУЗКИ    

            // ДОБАВЛЕНИЕ КОРЗИНЫ
            _filesList.Add("Trash");
            imageList2.Images.Add(Resources.trash);
            filesPanel.Items.Add("Корзина", imageList2.Images.Count - 1);
            filesPanel.Items[0].Tag = "trash";
            // END ДОБАВЛЕНИЕ КОРЗИНЫ 
        }

        // FOLDER панель
        private void foldersPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (Directory.Exists(files[0]))
                {
                    imageList1.Images.Add(files[0], Resources.folder);
                    _foldersList.Add(files[0]);
                    foldersPanel.Items.Add(files[0].Split(Path.DirectorySeparatorChar).Last(), imageList1.Images.Count-1);
                }
            }
        }
        private void foldersPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void foldersPanel_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.All);
        }

        private void foldersPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Directory.Exists(_foldersList[foldersPanel.SelectedItems[0].Index]))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = _foldersList[foldersPanel.SelectedItems[0].Index],
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show($@"Директории ""{_foldersList[foldersPanel.SelectedItems[0].Index]}"" не сущетсвует!");
            }
        }
        private void foldersPanel_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                pathToFileFolder.Text = _foldersList[foldersPanel.SelectedItems[0].Index];
                nameFileFolder.Text = foldersPanel.SelectedItems[0].Text;
                if (!Directory.Exists(_foldersList[foldersPanel.FocusedItem.Index]))
                {
                    imageList1.Images[imageList1.Images.IndexOfKey(_foldersList[foldersPanel.FocusedItem.Index])] =
                        ChangeOpacity(
                            imageList1.Images[imageList1.Images.IndexOfKey(_foldersList[foldersPanel.FocusedItem.Index])], 0.7f);
                }
            }
            else
            {
                pathToFileFolder.Text = "";
                nameFileFolder.Text = "";
            }

        }
        // END FOLDER панель

        // FILES панель
        private void filesPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
            
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (File.Exists(file[0]) && _formats.Any(Path.GetExtension(file[0]).ToLower().Contains))
                {
                    radWaitingBar2.AssociatedControl = filesPanel;
                    radWaitingBar2.StartWaiting();
                    switch (Path.GetExtension(file[0])?.ToLower())
                    {
                        case ".doc":
                            StructuredStorageReader reader = new StructuredStorageReader(file[0]);
                            WordDocument doc = new WordDocument(reader);
                            WordprocessingDocument docx = WordprocessingDocument.Create(file[0].Remove(file[0].LastIndexOf(".", StringComparison.Ordinal)) + ".docx", DocumentType.Document);
                            Converter.Convert(doc, docx);
                            file[0] = file[0].Remove(file[0].LastIndexOf(".", StringComparison.Ordinal)) + ".docx";
                            break;
                        case ".docm":
                        case ".dot":
                        case ".dotm":
                        case ".dotx":
                        case ".odt":
                            Convert(file[0], file[0].Remove(file[0].LastIndexOf(".", StringComparison.Ordinal)) + ".docx",
                                WdSaveFormat.wdFormatDocumentDefault);
                            file[0] = file[0].Remove(file[0].LastIndexOf(".", StringComparison.Ordinal)) + ".docx";
                            break;
                        
                        case ".xls":
                            var xlsConverter = new XlsToXlsx();
                            xlsConverter.ConvertToXlsxFile(file[0], file[0].Remove(file[0].LastIndexOf(".", StringComparison.Ordinal)) + ".xlsx");
                            file[0] = file[0].Remove(file[0].LastIndexOf(".", StringComparison.Ordinal)) + ".xlsx";
                            break;
                    }

                    imageList2.Images.Add(file[0], Icon.ExtractAssociatedIcon(file[0]));
                    FileInfo fi = new FileInfo(file[0]);
                    _filesList.Add(fi.FullName);
                    filesPanel.Items.Add(Path.GetFileNameWithoutExtension(fi.Name), imageList2.Images.Count - 1);
                    radWaitingBar2.StopWaiting();
                    radWaitingBar2.AssociatedControl = null;
                }
            }
            else if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                // //УДАЛЕНИЕ item
                var pos = filesPanel.PointToClient(new Point(e.X, e.Y));
                var hit = filesPanel.HitTest(pos);
                if (hit.Item != null && hit.Item.Tag != null)
                {
                    var dragItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                    if (dragItem.ListView.Name == "foldersPanel")
                    {
                        _foldersList.RemoveAt(dragItem.Index);
                        foldersPanel.Items.RemoveAt(dragItem.Index);
                    }
                    else
                    {
                        _filesList.RemoveAt(filesPanel.SelectedItems[0].Index);
                        filesPanel.Items.RemoveAt(filesPanel.SelectedItems[0].Index);
                    }

                }
            }
        }
        private void filesPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void filesPanel_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.All);
        }

        private void filesPanel_DoubleClick(object sender, EventArgs e)
        {
            if (File.Exists(_filesList[filesPanel.SelectedItems[0].Index]))
            {
                Process.Start(_filesList[filesPanel.SelectedItems[0].Index]);
            }
            else if (filesPanel.SelectedItems[0].Tag != null)
            {
                // do nothing
            }
            else
            {
                MessageBox.Show($"Файл \"{_filesList[filesPanel.SelectedItems[0].Index]}\" удалён или перемещён!");
            }
        }
        private void filesPanel_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                pathToFileFolder.Text = _filesList[filesPanel.SelectedItems[0].Index];
                nameFileFolder.Text = filesPanel.SelectedItems[0].Text;
                if (filesPanel.SelectedItems[0].Index != 0 && !File.Exists(_filesList[filesPanel.SelectedItems[0].Index]))
                {
                    imageList2.Images[imageList2.Images.IndexOfKey(_filesList[filesPanel.SelectedItems[0].Index])] =
                        ChangeOpacity(
                            imageList2.Images[imageList2.Images.IndexOfKey(_filesList[filesPanel.SelectedItems[0].Index])], 0.7f);
                }
            }
            else
            {
                pathToFileFolder.Text = "";
                nameFileFolder.Text = "";
            }
        }
        // END FILES панель

        // MAIN WINDOW
        private void radPageView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (!e.Data.GetDataPresent(typeof(ListViewItem))) return;

            radWaitingBar2.StartWaiting();
            var dragItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            if (dragItem.ListView.Name == "filesPanel")
            {
                var filePath = _filesList[dragItem.Index];
                //radPageView1.Pages.(dragItem.Text);
                var page = new RadPageViewPage(dragItem.Text);
                radPageView1.Pages.Add(page);
                radPageView1.SelectedPage = page;
                radWaitingBar2.AssociatedControl = page;
                radWaitingBar2.StartWaiting();
                switch (Path.GetExtension(filePath).ToLower())
                {
                    case ".docx":
                    case ".txt":
                        var editor = new RadRichTextEditor();
                        editor.LayoutMode = DocumentLayoutMode.Paged;
                        editor.ThemeName = "Office2013Light";
                        //object provider;
                        if (Path.GetExtension(filePath).ToLower() == ".docx")
                        {
                           var provider = new DocxFormatProvider();
                           using (FileStream inputStream = File.OpenRead(filePath))
                           {
                               editor.Document = provider.Import(inputStream);
                           }
                        }
                        else
                        {
                            var provider = new TxtFormatProvider();
                            using (FileStream inputStream = File.OpenRead(filePath))
                            {
                                editor.Document = provider.Import(inputStream);
                            }
                        }
                        
                        var ruler = new RadRichTextEditorRuler();
                        ruler.Dock = DockStyle.Fill;
                        ruler.AssociatedRichTextBox = editor;
                        ruler.Controls.Add(editor);
                        ruler.ThemeName = "Office2013Light";

                        var ribbon = new CustomRichTextEditorRibbonBar();
                        ribbon.AssociatedRichTextEditor = editor;
                        ribbon.OpenedFileName = filePath;
                        ribbon.Text = Path.GetFileName(filePath);
                        ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(ribbon.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(ribbon.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                        ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(ribbon.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(3))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                        ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(ribbon.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(4))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                        ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(ribbon.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(5))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                        ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(ribbon.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(6))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

                        page.Controls.Add(ruler);
                        page.Controls.Add(ribbon);
                        
                        break;
                    case ".htm":
                    case ".html":
                    case ".pdf":
                        WebControl webControl1 = new WebControl();
                        webControl1.Dock = DockStyle.Fill;
                        webControl1.Source = new Uri(filePath);

                        page.Controls.Add(webControl1);
                        break;
                    case ".mht":
                    case ".mhtml":
                        var webBrowser = new WebBrowser();
                        webBrowser.Navigate(filePath);
                        webBrowser.Dock = DockStyle.Fill;

                        page.Controls.Add(webBrowser);
                        break;
                    case ".xlsx":
                        var excel = new RadSpreadsheet();
                        excel.Dock = DockStyle.Fill;
                        excel.ThemeName = "Office2013Light";

                        var ribonExcel = new RadSpreadsheetRibbonBar
                        {
                            AssociatedSpreadsheet = excel,
                            ThemeName = "Office2013Light",
                            CloseButton = false,
                            MaximizeButton = false,
                            MinimizeButton = false,
                            LayoutMode = RibbonLayout.Simplified,

                            Text = Path.GetFileName(filePath)
                        };
                        ribonExcel.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(1).Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        ribonExcel.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(2).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                        ribonExcel.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(3).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                        ribonExcel.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(4).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                        ribonExcel.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(5).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                        ribonExcel.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(6).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

                        var formatProvider = new XlsxFormatProvider();
                        using (Stream input = new FileStream(filePath, FileMode.Open))
                        {
                            excel.Workbook = formatProvider.Import(input);
                        }
                        page.Controls.Add(excel);
                        page.Controls.Add(ribonExcel);
                        break;
                }
                
                radWaitingBar2.StopWaiting();
                radWaitingBar2.AssociatedControl = null;
            }
        }
        private void radPageView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(ListViewItem)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void radPageView1_PageRemoving(object sender, RadPageViewCancelEventArgs e)
        {
            //var closePage = (RadPageViewPage) e.Page;
            for (int i = e.Page.Controls.Count - 1; i >= 0; i--)
            {
                e.Page.Controls[i].Dispose();
            }
        }
        private void radPageView1_PageRemoved(object sender, RadPageViewEventArgs e)
        {
            e.Page.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.WaitForFullGCApproach();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }
        //END MAIN WINDOW

        //HELPERS
        public static void Convert(string input, string output, WdSaveFormat format)
        {
            _Application oWord = new Word.Application
            {
                Visible = false
            };

            // Interop requires objects.
            object oMissing = System.Reflection.Missing.Value;
            object isVisible = true;
            object readOnly = true;     // Does not cause any word dialog to show up
            //object readOnly = false;  // Causes a word object dialog to show at the end of the conversion
            object oInput = input;
            object oOutput = output;
            object oFormat = format;
            Object falseObj = false;
            _Document oDoc = null;

            try
            {
                oDoc = oWord.Documents.Open(
                    ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                );

                // Make this document the active document.
                oDoc.Activate();

                // Save this document using Word
                oDoc.SaveAs2(ref oOutput, ref oFormat, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                );

                // Always close Word.exe.
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                oDoc = null;
                oWord = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("error");
                oDoc.Close(ref falseObj, ref oMissing, ref oMissing);
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                oDoc = null;
                oWord = null;
                throw;
            }

        }
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();  // Releasing all resource used by graphics 
            return bmp;
        }
        //END HELPERS

        //MAIL AND CHAT
        private void RadButton1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (keybox.Text == "")
            {
                RadMessageBox.Show("Необходимо ввести ключ");
                return;
            }
            userName = chatname.Text;

            client = new TcpClient();
            try
            {
                client.Connect(host, port); //подключение клиента

                stream = client.GetStream(); // получаем поток
                string message = userName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
                key = keybox.Text;

                // запускаем новый поток для получения данных
                receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока

                listBox1.Items.Add("Добро пожаловать, " + userName);

                messagebox.Text = key;
                SendMessage();
                messagebox.Text = "";
                var s = File.ReadAllLines("keys.txt");
                if (!s.Contains(key))
                    File.AppendAllText("keys.txt", key + "\n");
                radButton8.Visible = true;
                radButton1.Visible = false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
        }

        void SendMessage()
        {
            string message = messagebox.Text;
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
        
        void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    listBox1.Items.Add(message);//вывод сообщения
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                    //listBox1.Items.Add("Подключение прервано!"); //соединение было прервано
                    //Console.ReadLine();
                    Disconnect();
                }
            }
        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
            {
                client.Dispose();
                client.Close(); //отключение клиента
                client = null;
            }
        }

        private void RadButton2_Click(object sender, EventArgs e)
        {
            SendMessage();
            messagebox.Text = "";
        }

        private void RadButton3_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectedMail = loginbox.Text;
                var pos = loginbox.Text.LastIndexOf("@");
                //pos = loginbox.Text.LastIndexOf(".", pos - 1);
                mailclient.Connect("imap." + loginbox.Text.Substring(pos + 1), 993, true);
                mailclient.Authenticate(loginbox.Text, passbox.Text);
                pass = passbox.Text;
            }
            catch
            {
                RadMessageBox.Show("Проверьте правильность ввода имя пользователя и пароля");
                return;
            }


            // The Inbox folder is always available on all IMAP servers...
            var inbox = mailclient.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            //passbox.Text = "Total messages:" + inbox.Count;
            //Console.WriteLine();
            //Console.WriteLine("Recent messages: {0}", inbox.Recent);
            radListView1.Items.Clear();
            listBox2.Items.Clear();
            for (int i = inbox.Count - 10; i < inbox.Count; i++)
            {
                var message = inbox.GetMessage(i);
                // radListView1.Items.Add(message.Subject);
                listBox2.Items.Add(i);
                radListView1.Items.Add(message.Subject);
                if (message.Attachments.Count() != 0)
                {

                    radListView1.Items[radListView1.Items.Count - 1].Image = new Bitmap("вложение.png");




                }
                //Console.WriteLine("Subject: {0}", );
            }


            radButton5.Visible = true;
            var s = File.ReadAllLines("SavedMail.txt");
            if (!s.Contains(loginbox.Text + " " + passbox.Text))
                File.AppendAllText("SavedMail.txt", loginbox.Text + " " + passbox.Text + "\n");
            passbox.Text = "";
            disconnectbutton.Visible = true;
            radButton6.Visible = true;
            findbox.Visible = true;
            radButton4.Visible = true;
            radButton7.Visible = true;
            //client.Disconnect(true);

        }

        private void RadButton4_Click(object sender, EventArgs e)
        {
            var inbox = mailclient.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            radListView1.Items.Clear();
            listBox2.Items.Clear();
            for (int i = inbox.Count - 10; i < inbox.Count; i++)
            {
                var message = inbox.GetMessage(i);
                radListView1.Items.Add(message.Subject);
                if (message.Attachments.Count() > 0)
                {
                    radListView1.Items[radListView1.Items.Count - 1].Image = new Bitmap("вложение.png");
                }
                listBox2.Items.Add(i);
            }

        }

        private void RadButton5_Click(object sender, EventArgs e)
        {
            RadForm2 rf = new RadForm2(mailclient.Inbox, 0, ConnectedMail, mailclient, pass, 1);
            rf.ShowDialog();
        }

        private void Loginbox_TextChanged(object sender, EventArgs e)
        {
            var s = File.ReadAllLines("SavedMail.txt");
            foreach (var t in s)
            {
                if (t.Substring(0, t.IndexOf(" ")) == loginbox.Text)
                {
                    passbox.Text = t.Substring(t.IndexOf(" ") + 1);
                    break;
                }
            }
        }

        private void Disconnectbutton_Click(object sender, EventArgs e)
        {
            mailclient.Disconnect(true);
            radListView1.Items.Clear();
            listBox2.Items.Clear();
            disconnectbutton.Visible = false;
            radButton5.Visible = false;
            radButton6.Visible = false;
            findbox.Visible = false;
            radButton4.Visible = false;
            radButton7.Visible = false;
        }

        private void RadListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MailMessage m = new MailMessage(mailclient, System.Convert.ToInt32(listBox2.Items[radListView1.SelectedIndex]), ConnectedMail, pass);
            m.ShowDialog();
        }

        private void RadButton6_Click(object sender, EventArgs e)
        {
            if (findbox.Text != "")
            {
                var inbox = mailclient.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                radListView1.Items.Clear();
                listBox2.Items.Clear();
                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    if ((message.Subject != null && message.Subject.Contains(findbox.Text)) || (message.TextBody != null && message.TextBody.Contains(findbox.Text)))
                    {
                        radListView1.Items.Add(message.Subject);
                        if (message.Attachments.Count() > 0)
                        {
                            radListView1.Items[radListView1.Items.Count - 1].Image = new Bitmap("вложение.png");
                        }

                        listBox2.Items.Add(i);
                    }
                }
            }
            else
            {
                var inbox = mailclient.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                radListView1.Items.Clear();
                listBox2.Items.Clear();
                for (int i = inbox.Count - 10; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    listBox2.Items.Add(i);
                    radListView1.Items.Add(message.Subject);
                    if (message.Attachments.Count() != 0)
                    {
                        radListView1.Items[radListView1.Items.Count - 1].Image = new Bitmap("вложение.png");
                    }
                }
            }
        }

        private void RadButton7_Click(object sender, EventArgs e)
        {
            var inbox = mailclient.Inbox;
            inbox.Open(FolderAccess.ReadWrite);
            for (int i = radListView1.Items.Count - 1; i >= 0; i--)
            {
                if (radListView1.Items[i].CheckState == ToggleState.On)
                {
                    inbox.AddFlags(System.Convert.ToInt32(listBox2.Items[i]), MessageFlags.Deleted, true);
                }
            }
            radListView1.Items.Clear();
            listBox2.Items.Clear();
            inbox.Close();
            inbox.Open(FolderAccess.ReadOnly);
            for (int i = inbox.Count - 10; i < inbox.Count; i++)
            {
                var message = inbox.GetMessage(i);
                listBox2.Items.Add(i);
                radListView1.Items.Add(message.Subject);
                if (message.Attachments.Count() != 0)
                {
                    radListView1.Items[radListView1.Items.Count - 1].Image = new Bitmap("вложение.png");
                }
            }
        }

        private void RadForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mailclient.Disconnect(true);
        }
        
        private void RadButton8_Click(object sender, EventArgs e)
        {
            receiveThread.Abort();
            receiveThread.Join(500);

            Disconnect();

            radButton1.Visible = true;
            radButton8.Visible = false;

        }
    }
}
