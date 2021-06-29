using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using MimeKit.Text;
using Telerik.WinControls;

namespace TelerikTest
{
    public partial class MailMessage : Telerik.WinControls.UI.RadForm
    {
        private IMailFolder inbox;
        private int Numbermessage;
        private string ConnectedMail;
        private ImapClient mclient;
        private string pass;
        public MailMessage(ImapClient mailclient, int num, string ConnMAil, string password)
        {
            InitializeComponent();
            pass = password;
            mclient = mailclient;
            ConnectedMail = ConnMAil;
            Numbermessage = num;
            inbox = mailclient.Inbox;
            var message = inbox.GetMessage(num);

            subject.Text = message.Subject;
            try
            {
                body.Text = message.GetTextBody(TextFormat.Plain);
            }
            catch
            {
                File.WriteAllText("temp.html", message.HtmlBody, Encoding.UTF8);
                WebBrowser wb = new WebBrowser();
                var html = Application.StartupPath + @"\temp.html";
                wb.Navigate(html);
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                {
                    Thread.Sleep(200);
                    Application.DoEvents();
                }

                wb.Document.ExecCommand("SelectAll", false, null);
                wb.Document.ExecCommand("Copy", false, null);
                body.Paste();
            }

            sender.Text = message.From.OfType<MailboxAddress>().First().Address;


            foreach (var att in message.Attachments)
            {
                radListView1.Items.Add(att.ContentType.Name);


            }


        }

        private void RadListView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                radContextMenu1.Show(radListView1, new Point(e.X, e.Y));
            }
        }

        private void radMenuItem1_Click(object sender, System.EventArgs e)
        {
            var message = inbox.GetMessage(Numbermessage);

            foreach (var attachment in message.Attachments)
            {
                if (radListView1.SelectedItem.Text != attachment.ContentType.Name)
                    continue;
                FolderBrowserDialog fb = new FolderBrowserDialog();
                if (fb.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = File.Create(fb.SelectedPath + "\\" + attachment.ContentType.Name))
                    {
                        if (attachment is MessagePart)
                        {
                            var part = (MessagePart)attachment;

                            part.Message.WriteTo(stream);
                        }
                        else
                        {
                            var part = (MimePart)attachment;

                            part.Content.DecodeTo(stream);
                        }
                    }
                }

            }
        }

        private void RadButton1_Click(object sender, EventArgs e)
        {
            RadForm2 rf = new RadForm2(inbox, Numbermessage, ConnectedMail, mclient, pass, 0);
            rf.ShowDialog();
        }

        private void RadLabel4_Click(object sender, EventArgs e)
        {

        }

        private void RadLabel3_Click(object sender, EventArgs e)
        {

        }

        private void RadLabel2_Click(object sender, EventArgs e)
        {

        }

        private void RadLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Sender_TextChanged(object sender, EventArgs e)
        {

        }

        private void RadListView1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void Body_Click(object sender, EventArgs e)
        {

        }

        private void Subject_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
