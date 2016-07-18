namespace Projekt_5._0
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
            this.Lbox_befehle = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ausToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_liste_Befehle = new System.Windows.Forms.Label();
            this.NotifyIcon_Sps = new System.Windows.Forms.NotifyIcon(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lbox_befehle
            // 
            this.Lbox_befehle.FormattingEnabled = true;
            this.Lbox_befehle.ItemHeight = 25;
            this.Lbox_befehle.Location = new System.Drawing.Point(74, 92);
            this.Lbox_befehle.Margin = new System.Windows.Forms.Padding(4);
            this.Lbox_befehle.Name = "Lbox_befehle";
            this.Lbox_befehle.Size = new System.Drawing.Size(330, 879);
            this.Lbox_befehle.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1838, 49);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anToolStripMenuItem,
            this.ausToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(107, 45);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // anToolStripMenuItem
            // 
            this.anToolStripMenuItem.Name = "anToolStripMenuItem";
            this.anToolStripMenuItem.Size = new System.Drawing.Size(457, 46);
            this.anToolStripMenuItem.Text = "Start speech recognition";
            this.anToolStripMenuItem.Click += new System.EventHandler(this.anToolStripMenuItem_Click);
            // 
            // ausToolStripMenuItem
            // 
            this.ausToolStripMenuItem.Enabled = false;
            this.ausToolStripMenuItem.Name = "ausToolStripMenuItem";
            this.ausToolStripMenuItem.Size = new System.Drawing.Size(457, 46);
            this.ausToolStripMenuItem.Text = "Stop speech recognition";
            this.ausToolStripMenuItem.Click += new System.EventHandler(this.ausToolStripMenuItem_Click);
            // 
            // lbl_liste_Befehle
            // 
            this.lbl_liste_Befehle.AutoSize = true;
            this.lbl_liste_Befehle.Location = new System.Drawing.Point(68, 56);
            this.lbl_liste_Befehle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_liste_Befehle.Name = "lbl_liste_Befehle";
            this.lbl_liste_Befehle.Size = new System.Drawing.Size(125, 26);
            this.lbl_liste_Befehle.TabIndex = 6;
            this.lbl_liste_Befehle.Text = "Commands";
            // 
            // NotifyIcon_Sps
            // 
            this.NotifyIcon_Sps.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon_Sps.Icon")));
            this.NotifyIcon_Sps.Text = "Sprachsteuerung";
            this.NotifyIcon_Sps.Visible = true;
            this.NotifyIcon_Sps.Click += new System.EventHandler(this.NotifyIcon_Sps_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(430, 88);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1384, 886);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(425, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 26);
            this.label1.TabIndex = 8;
            this.label1.Text = "Output";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1838, 985);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lbl_liste_Befehle);
            this.Controls.Add(this.Lbox_befehle);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Engine";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Lbox_befehle;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ausToolStripMenuItem;
        private System.Windows.Forms.Label lbl_liste_Befehle;
        private System.Windows.Forms.NotifyIcon NotifyIcon_Sps;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
    }
}

