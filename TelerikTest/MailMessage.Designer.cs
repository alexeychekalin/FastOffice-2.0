namespace TelerikTest
{
    partial class MailMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.subject = new Telerik.WinControls.UI.RadTextBox();
            this.body = new Telerik.WinControls.UI.RadRichTextEditor();
            this.radListView1 = new Telerik.WinControls.UI.RadListView();
            this.sender = new System.Windows.Forms.TextBox();
            this.radContextMenu1 = new Telerik.WinControls.UI.RadContextMenu(this.components);
            this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.subject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.body)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // subject
            // 
            this.subject.Location = new System.Drawing.Point(28, 98);
            this.subject.Multiline = true;
            this.subject.Name = "subject";
            // 
            // 
            // 
            this.subject.RootElement.StretchVertically = true;
            this.subject.Size = new System.Drawing.Size(922, 47);
            this.subject.TabIndex = 0;
            this.subject.ThemeName = "Office2013Light";
            this.subject.TextChanged += new System.EventHandler(this.Subject_TextChanged);
            // 
            // body
            // 
            this.body.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            this.body.Location = new System.Drawing.Point(28, 171);
            this.body.Name = "body";
            this.body.SelectionFill = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(78)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.body.SelectionStroke = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.body.Size = new System.Drawing.Size(796, 392);
            this.body.TabIndex = 1;
            this.body.ThemeName = "Office2013Light";
            this.body.Click += new System.EventHandler(this.Body_Click);
            // 
            // radListView1
            // 
            this.radListView1.Location = new System.Drawing.Point(830, 171);
            this.radListView1.Name = "radListView1";
            this.radListView1.Size = new System.Drawing.Size(120, 392);
            this.radListView1.TabIndex = 2;
            this.radListView1.ThemeName = "Office2013Light";
            this.radListView1.SelectedItemChanged += new System.EventHandler(this.RadListView1_SelectedItemChanged);
            this.radListView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadListView1_MouseDown);
            // 
            // sender
            // 
            this.sender.Location = new System.Drawing.Point(28, 22);
            this.sender.Multiline = true;
            this.sender.Name = "sender";
            this.sender.Size = new System.Drawing.Size(922, 46);
            this.sender.TabIndex = 3;
            this.sender.TextChanged += new System.EventHandler(this.Sender_TextChanged);
            // 
            // radContextMenu1
            // 
            this.radContextMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItem1});
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Text = "Сохранить";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(30, 3);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(73, 18);
            this.radLabel1.TabIndex = 4;
            this.radLabel1.Text = "Отправитель";
            this.radLabel1.Click += new System.EventHandler(this.RadLabel1_Click);
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(30, 77);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(32, 18);
            this.radLabel2.TabIndex = 5;
            this.radLabel2.Text = "Тема";
            this.radLabel2.Click += new System.EventHandler(this.RadLabel2_Click);
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(28, 151);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(74, 18);
            this.radLabel3.TabIndex = 6;
            this.radLabel3.Text = "Содержимое";
            this.radLabel3.Click += new System.EventHandler(this.RadLabel3_Click);
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(830, 151);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(74, 18);
            this.radLabel4.TabIndex = 7;
            this.radLabel4.Text = "Приложение";
            this.radLabel4.Click += new System.EventHandler(this.RadLabel4_Click);
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(28, 569);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(110, 24);
            this.radButton1.TabIndex = 8;
            this.radButton1.Text = "Ответить";
            this.radButton1.ThemeName = "Office2013Light";
            this.radButton1.Click += new System.EventHandler(this.RadButton1_Click);
            // 
            // MailMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 603);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radLabel4);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.sender);
            this.Controls.Add(this.radListView1);
            this.Controls.Add(this.body);
            this.Controls.Add(this.subject);
            this.Name = "MailMessage";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "MailMessage";
            this.ThemeName = "Office2013Light";
            ((System.ComponentModel.ISupportInitialize)(this.subject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.body)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox subject;
        private Telerik.WinControls.UI.RadRichTextEditor body;
        private Telerik.WinControls.UI.RadListView radListView1;
        private System.Windows.Forms.TextBox sender;
        private Telerik.WinControls.UI.RadContextMenu radContextMenu1;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadButton radButton1;
    }
}
