namespace triangle003
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.heightToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // heightToolStripMenuItem
            // 
            this.heightToolStripMenuItem.Name = "heightToolStripMenuItem";
            this.heightToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.heightToolStripMenuItem.Text = "Height";
            this.heightToolStripMenuItem.Click += new System.EventHandler(this.heightToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.resetToolStripMenuItem.Text = "reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillModeToolStripMenuItem,
            this.backgroundColorToolStripMenuItem});
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(91, 24);
            this.propertiesToolStripMenuItem.Text = "properties";
            // 
            // fillModeToolStripMenuItem
            // 
            this.fillModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solidToolStripMenuItem,
            this.wireframeToolStripMenuItem,
            this.pointToolStripMenuItem});
            this.fillModeToolStripMenuItem.Name = "fillModeToolStripMenuItem";
            this.fillModeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.fillModeToolStripMenuItem.Text = "fill mode";
            // 
            // solidToolStripMenuItem
            // 
            this.solidToolStripMenuItem.Name = "solidToolStripMenuItem";
            this.solidToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.solidToolStripMenuItem.Text = "solid";
            this.solidToolStripMenuItem.Click += new System.EventHandler(this.solidToolStripMenuItem_Click_1);
            // 
            // wireframeToolStripMenuItem
            // 
            this.wireframeToolStripMenuItem.Name = "wireframeToolStripMenuItem";
            this.wireframeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.wireframeToolStripMenuItem.Text = "wireframe";
            this.wireframeToolStripMenuItem.Click += new System.EventHandler(this.wireframeToolStripMenuItem_Click_1);
            // 
            // pointToolStripMenuItem
            // 
            this.pointToolStripMenuItem.Name = "pointToolStripMenuItem";
            this.pointToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.pointToolStripMenuItem.Text = "point";
            this.pointToolStripMenuItem.Click += new System.EventHandler(this.pointToolStripMenuItem_Click_1);
            // 
            // backgroundColorToolStripMenuItem
            // 
            this.backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
            this.backgroundColorToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.backgroundColorToolStripMenuItem.Text = "background color";
            this.backgroundColorToolStripMenuItem.Click += new System.EventHandler(this.backgroundColorToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "terrain generator";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundColorToolStripMenuItem;
    }
}

