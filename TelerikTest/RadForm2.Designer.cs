﻿namespace TelerikTest
{
    partial class RadForm2
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.sender = new System.Windows.Forms.TextBox();
            this.radListView1 = new Telerik.WinControls.UI.RadListView();
            this.body = new Telerik.WinControls.UI.RadRichTextEditor();
            this.subject = new Telerik.WinControls.UI.RadTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.radMultiColumnComboBox1 = new Telerik.WinControls.UI.RadMultiColumnComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.body)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1.EditorControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1.EditorControl.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(47, 590);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(110, 24);
            this.radButton1.TabIndex = 17;
            this.radButton1.Text = "Отправить";
            this.radButton1.ThemeName = "Office2013Light";
            this.radButton1.Click += new System.EventHandler(this.RadButton1_Click);
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(849, 172);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(74, 18);
            this.radLabel4.TabIndex = 16;
            this.radLabel4.Text = "Приложение";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(47, 172);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(74, 18);
            this.radLabel3.TabIndex = 15;
            this.radLabel3.Text = "Содержимое";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(49, 98);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(32, 18);
            this.radLabel2.TabIndex = 14;
            this.radLabel2.Text = "Тема";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(49, 2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(67, 18);
            this.radLabel1.TabIndex = 13;
            this.radLabel1.Text = "Получатель";
            // 
            // sender
            // 
            this.sender.Location = new System.Drawing.Point(47, 40);
            this.sender.Multiline = true;
            this.sender.Name = "sender";
            this.sender.Size = new System.Drawing.Size(922, 46);
            this.sender.TabIndex = 12;
            // 
            // radListView1
            // 
            this.radListView1.Location = new System.Drawing.Point(849, 192);
            this.radListView1.Name = "radListView1";
            this.radListView1.Size = new System.Drawing.Size(120, 392);
            this.radListView1.TabIndex = 11;
            this.radListView1.ThemeName = "Office2013Light";
            // 
            // body
            // 
            this.body.AllowDrop = true;
            this.body.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            this.body.Location = new System.Drawing.Point(47, 192);
            this.body.Name = "body";
            this.body.SelectionFill = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(78)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.body.SelectionStroke = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.body.Size = new System.Drawing.Size(796, 392);
            this.body.TabIndex = 10;
            this.body.ThemeName = "Office2013Light";
            this.body.DragDrop += new System.Windows.Forms.DragEventHandler(this.Body_DragDrop);
            this.body.DragEnter += new System.Windows.Forms.DragEventHandler(this.Body_DragEnter);
            // 
            // subject
            // 
            this.subject.Location = new System.Drawing.Point(47, 119);
            this.subject.Multiline = true;
            this.subject.Name = "subject";
            // 
            // 
            // 
            this.subject.RootElement.StretchVertically = true;
            this.subject.Size = new System.Drawing.Size(922, 47);
            this.subject.TabIndex = 9;
            this.subject.ThemeName = "Office2013Light";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(849, 590);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 43);
            this.listBox1.TabIndex = 0;
            this.listBox1.Visible = false;
            // 
            // radMultiColumnComboBox1
            // 
            // 
            // radMultiColumnComboBox1.NestedRadGridView
            // 
            this.radMultiColumnComboBox1.EditorControl.BackColor = System.Drawing.SystemColors.Window;
            this.radMultiColumnComboBox1.EditorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radMultiColumnComboBox1.EditorControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radMultiColumnComboBox1.EditorControl.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.radMultiColumnComboBox1.EditorControl.MasterTemplate.AllowAddNewRow = false;
            this.radMultiColumnComboBox1.EditorControl.MasterTemplate.AllowCellContextMenu = false;
            this.radMultiColumnComboBox1.EditorControl.MasterTemplate.AllowColumnChooser = false;
            this.radMultiColumnComboBox1.EditorControl.MasterTemplate.EnableGrouping = false;
            this.radMultiColumnComboBox1.EditorControl.MasterTemplate.ShowFilteringRow = false;
            this.radMultiColumnComboBox1.EditorControl.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radMultiColumnComboBox1.EditorControl.Name = "NestedRadGridView";
            this.radMultiColumnComboBox1.EditorControl.ReadOnly = true;
            this.radMultiColumnComboBox1.EditorControl.ShowGroupPanel = false;
            this.radMultiColumnComboBox1.EditorControl.Size = new System.Drawing.Size(240, 150);
            this.radMultiColumnComboBox1.EditorControl.TabIndex = 0;
            this.radMultiColumnComboBox1.Location = new System.Drawing.Point(47, 21);
            this.radMultiColumnComboBox1.Name = "radMultiColumnComboBox1";
            this.radMultiColumnComboBox1.Size = new System.Drawing.Size(922, 21);
            this.radMultiColumnComboBox1.TabIndex = 18;
            this.radMultiColumnComboBox1.TabStop = false;
            this.radMultiColumnComboBox1.ThemeName = "Office2013Light";
            this.radMultiColumnComboBox1.Visible = false;
            this.radMultiColumnComboBox1.SelectedIndexChanged += new System.EventHandler(this.RadMultiColumnComboBox1_SelectedIndexChanged);
            // 
            // RadForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 638);
            this.Controls.Add(this.radMultiColumnComboBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radLabel4);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.sender);
            this.Controls.Add(this.radListView1);
            this.Controls.Add(this.body);
            this.Controls.Add(this.subject);
            this.Name = "RadForm2";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "RadForm2";
            this.ThemeName = "Office2013Light";
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.body)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1.EditorControl.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1.EditorControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.TextBox sender;
        private Telerik.WinControls.UI.RadListView radListView1;
        private Telerik.WinControls.UI.RadRichTextEditor body;
        private Telerik.WinControls.UI.RadTextBox subject;
        private System.Windows.Forms.ListBox listBox1;
        private Telerik.WinControls.UI.RadMultiColumnComboBox radMultiColumnComboBox1;
    }
}
