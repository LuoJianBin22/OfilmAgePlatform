namespace OfilmAgePlatform
{
    partial class Form_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.pnl_container = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新布局ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_设置 = new System.Windows.Forms.ToolStripButton();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.tabPage_主页面 = new System.Windows.Forms.TabPage();
            this.tabPage_通讯页面 = new System.Windows.Forms.TabPage();
            this.timer_queryAgeStatus = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl_main.SuspendLayout();
            this.tabPage_主页面.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_container
            // 
            this.pnl_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_container.Location = new System.Drawing.Point(3, 3);
            this.pnl_container.Name = "pnl_container";
            this.pnl_container.Size = new System.Drawing.Size(1344, 567);
            this.pnl_container.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新布局ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 28);
            // 
            // 刷新布局ToolStripMenuItem
            // 
            this.刷新布局ToolStripMenuItem.Name = "刷新布局ToolStripMenuItem";
            this.刷新布局ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.刷新布局ToolStripMenuItem.Text = "刷新布局";
            this.刷新布局ToolStripMenuItem.Click += new System.EventHandler(this.刷新布局ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_设置});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1358, 27);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_设置
            // 
            this.toolStripButton_设置.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_设置.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_设置.Image")));
            this.toolStripButton_设置.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_设置.Name = "toolStripButton_设置";
            this.toolStripButton_设置.Size = new System.Drawing.Size(43, 24);
            this.toolStripButton_设置.Text = "设置";
            this.toolStripButton_设置.Click += new System.EventHandler(this.toolStripButton_设置_Click);
            // 
            // tabControl_main
            // 
            this.tabControl_main.Controls.Add(this.tabPage_主页面);
            this.tabControl_main.Controls.Add(this.tabPage_通讯页面);
            this.tabControl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_main.Location = new System.Drawing.Point(0, 27);
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(1358, 602);
            this.tabControl_main.TabIndex = 11;
            // 
            // tabPage_主页面
            // 
            this.tabPage_主页面.Controls.Add(this.pnl_container);
            this.tabPage_主页面.Location = new System.Drawing.Point(4, 25);
            this.tabPage_主页面.Name = "tabPage_主页面";
            this.tabPage_主页面.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_主页面.Size = new System.Drawing.Size(1350, 573);
            this.tabPage_主页面.TabIndex = 0;
            this.tabPage_主页面.Text = "主页面";
            this.tabPage_主页面.UseVisualStyleBackColor = true;
            // 
            // tabPage_通讯页面
            // 
            this.tabPage_通讯页面.Location = new System.Drawing.Point(4, 25);
            this.tabPage_通讯页面.Name = "tabPage_通讯页面";
            this.tabPage_通讯页面.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_通讯页面.Size = new System.Drawing.Size(1350, 573);
            this.tabPage_通讯页面.TabIndex = 1;
            this.tabPage_通讯页面.Text = "通讯页面";
            this.tabPage_通讯页面.UseVisualStyleBackColor = true;
            // 
            // timer_queryAgeStatus
            // 
            this.timer_queryAgeStatus.Tick += new System.EventHandler(this.timer_queryAgeStatus_Tick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 629);
            this.Controls.Add(this.tabControl_main);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Text = "老化测试设备";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Main_FormClosed);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.SizeChanged += new System.EventHandler(this.Form_Main_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl_main.ResumeLayout(false);
            this.tabPage_主页面.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnl_container;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 刷新布局ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_设置;
        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.TabPage tabPage_主页面;
        private System.Windows.Forms.TabPage tabPage_通讯页面;
        private System.Windows.Forms.Timer timer_queryAgeStatus;
        private System.Windows.Forms.Timer timer1;
    }
}

