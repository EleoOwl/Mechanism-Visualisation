namespace Mechanism_Visualisation
{
    partial class Form_main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.рисунокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.движениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geometryparamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kinematicparamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exstraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.каркасToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.траекторияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.escapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(779, 509);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.рисунокToolStripMenuItem,
            this.движениеToolStripMenuItem,
            this.параметрыToolStripMenuItem,
            this.траекторияToolStripMenuItem,
            this.escapeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // рисунокToolStripMenuItem
            // 
            this.рисунокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripMenuItem2,
            this.dToolStripMenuItem});
            this.рисунокToolStripMenuItem.Name = "рисунокToolStripMenuItem";
            this.рисунокToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.рисунокToolStripMenuItem.Text = "Рисунок";
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.drawToolStripMenuItem.Text = "Нарисовать";
            this.drawToolStripMenuItem.Click += new System.EventHandler(this.drawToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.clearToolStripMenuItem.Text = "Стереть";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem2.Text = "2D";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Enabled = false;
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.dToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.dToolStripMenuItem.Text = "3D";
            this.dToolStripMenuItem.Click += new System.EventHandler(this.dToolStripMenuItem_Click);
            // 
            // движениеToolStripMenuItem
            // 
            this.движениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.движениеToolStripMenuItem.Name = "движениеToolStripMenuItem";
            this.движениеToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.движениеToolStripMenuItem.Text = "Движение";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.startToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.startToolStripMenuItem.Text = "Начать";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.stopToolStripMenuItem.Text = "Остановить";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // параметрыToolStripMenuItem
            // 
            this.параметрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geometryparamsToolStripMenuItem,
            this.kinematicparamsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exstraToolStripMenuItem,
            this.каркасToolStripMenuItem});
            this.параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            this.параметрыToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.параметрыToolStripMenuItem.Text = "Параметры";
            // 
            // geometryparamsToolStripMenuItem
            // 
            this.geometryparamsToolStripMenuItem.Name = "geometryparamsToolStripMenuItem";
            this.geometryparamsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.geometryparamsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.geometryparamsToolStripMenuItem.Text = "Геометрические";
            this.geometryparamsToolStripMenuItem.Click += new System.EventHandler(this.geometryparamsToolStripMenuItem_Click);
            // 
            // kinematicparamsToolStripMenuItem
            // 
            this.kinematicparamsToolStripMenuItem.Name = "kinematicparamsToolStripMenuItem";
            this.kinematicparamsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.kinematicparamsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.kinematicparamsToolStripMenuItem.Text = "Кинематические";
            this.kinematicparamsToolStripMenuItem.Click += new System.EventHandler(this.kinematicparamsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
            // 
            // exstraToolStripMenuItem
            // 
            this.exstraToolStripMenuItem.Name = "exstraToolStripMenuItem";
            this.exstraToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.exstraToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.exstraToolStripMenuItem.Text = "Дополнительно";
            this.exstraToolStripMenuItem.Click += new System.EventHandler(this.exstraToolStripMenuItem_Click);
            // 
            // каркасToolStripMenuItem
            // 
            this.каркасToolStripMenuItem.Name = "каркасToolStripMenuItem";
            this.каркасToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.каркасToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.каркасToolStripMenuItem.Text = "Каркас";
            this.каркасToolStripMenuItem.Click += new System.EventHandler(this.каркасToolStripMenuItem_Click);
            // 
            // траекторияToolStripMenuItem
            // 
            this.траекторияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pointAToolStripMenuItem,
            this.pointBToolStripMenuItem});
            this.траекторияToolStripMenuItem.Name = "траекторияToolStripMenuItem";
            this.траекторияToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.траекторияToolStripMenuItem.Text = "Траектория";
            // 
            // pointAToolStripMenuItem
            // 
            this.pointAToolStripMenuItem.Name = "pointAToolStripMenuItem";
            this.pointAToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.pointAToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.pointAToolStripMenuItem.Text = "Точка А";
            this.pointAToolStripMenuItem.Click += new System.EventHandler(this.pointAToolStripMenuItem_Click);
            // 
            // pointBToolStripMenuItem
            // 
            this.pointBToolStripMenuItem.Name = "pointBToolStripMenuItem";
            this.pointBToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.pointBToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.pointBToolStripMenuItem.Text = "Точка В";
            this.pointBToolStripMenuItem.Click += new System.EventHandler(this.pointBToolStripMenuItem_Click);
            // 
            // escapeToolStripMenuItem
            // 
            this.escapeToolStripMenuItem.Name = "escapeToolStripMenuItem";
            this.escapeToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.escapeToolStripMenuItem.Text = "Выход";
            this.escapeToolStripMenuItem.Click += new System.EventHandler(this.escapeToolStripMenuItem_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Location = new System.Drawing.Point(782, 24);
            this.vScrollBar1.Maximum = 360;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(18, 521);
            this.vScrollBar1.TabIndex = 2;
            this.vScrollBar1.Value = 180;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Location = new System.Drawing.Point(0, 528);
            this.hScrollBar1.Maximum = 360;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(782, 17);
            this.hScrollBar1.TabIndex = 3;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 545);
            this.ControlBox = false;
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_main";
            this.Text = "Кинематика блочного механизма";
            this.Load += new System.EventHandler(this.Form_main_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Form_main_Scroll);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form_main_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_main_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem рисунокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem движениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem траекторияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem escapeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geometryparamsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kinematicparamsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointBToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exstraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.ToolStripMenuItem каркасToolStripMenuItem;
    }
}

