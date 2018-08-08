namespace Club_Project
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.homeIcon = new System.Windows.Forms.PictureBox();
            this.startPanel = new System.Windows.Forms.Panel();
            this.b2t_Panel = new System.Windows.Forms.Panel();
            this.t2b_Panel = new System.Windows.Forms.Panel();
            this.t2b_ResultPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.t2b_Button = new System.Windows.Forms.PictureBox();
            this.t2b_TextBox = new System.Windows.Forms.RichTextBox();
            this.b2t_resultBox = new System.Windows.Forms.RichTextBox();
            this.b2t_ConversionButton = new System.Windows.Forms.PictureBox();
            this.b2t_openButton = new System.Windows.Forms.PictureBox();
            this.b2t_openImageBox = new Emgu.CV.UI.ImageBox();
            this.b2t_Icon = new System.Windows.Forms.PictureBox();
            this.t2b_Icon = new System.Windows.Forms.PictureBox();
            this.ImageOpen = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).BeginInit();
            this.startPanel.SuspendLayout();
            this.b2t_Panel.SuspendLayout();
            this.t2b_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t2b_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2t_ConversionButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2t_openButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2t_openImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2t_Icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t2b_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleName = "homePanel";
            this.splitContainer1.Panel1.Controls.Add(this.homeIcon);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleName = "mainPanel";
            this.splitContainer1.Panel2.Controls.Add(this.startPanel);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(733, 370);
            this.splitContainer1.SplitterDistance = 39;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1;
            // 
            // homeIcon
            // 
            this.homeIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeIcon.Image = global::Club_Project.Properties.Resources.home;
            this.homeIcon.Location = new System.Drawing.Point(0, 0);
            this.homeIcon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.homeIcon.Name = "homeIcon";
            this.homeIcon.Size = new System.Drawing.Size(733, 39);
            this.homeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeIcon.TabIndex = 0;
            this.homeIcon.TabStop = false;
            this.homeIcon.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // startPanel
            // 
            this.startPanel.Controls.Add(this.b2t_Panel);
            this.startPanel.Controls.Add(this.b2t_Icon);
            this.startPanel.Controls.Add(this.t2b_Icon);
            this.startPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPanel.Location = new System.Drawing.Point(0, 0);
            this.startPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.startPanel.Name = "startPanel";
            this.startPanel.Size = new System.Drawing.Size(733, 329);
            this.startPanel.TabIndex = 2;
            this.startPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.startPanel_Paint);
            // 
            // b2t_Panel
            // 
            this.b2t_Panel.Controls.Add(this.t2b_Panel);
            this.b2t_Panel.Controls.Add(this.b2t_resultBox);
            this.b2t_Panel.Controls.Add(this.b2t_ConversionButton);
            this.b2t_Panel.Controls.Add(this.b2t_openButton);
            this.b2t_Panel.Controls.Add(this.b2t_openImageBox);
            this.b2t_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.b2t_Panel.Location = new System.Drawing.Point(0, 0);
            this.b2t_Panel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b2t_Panel.Name = "b2t_Panel";
            this.b2t_Panel.Size = new System.Drawing.Size(733, 329);
            this.b2t_Panel.TabIndex = 2;
            this.b2t_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.b2t_Panel_Paint);
            // 
            // t2b_Panel
            // 
            this.t2b_Panel.Controls.Add(this.t2b_ResultPanel);
            this.t2b_Panel.Controls.Add(this.t2b_Button);
            this.t2b_Panel.Controls.Add(this.t2b_TextBox);
            this.t2b_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.t2b_Panel.Location = new System.Drawing.Point(0, 0);
            this.t2b_Panel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.t2b_Panel.Name = "t2b_Panel";
            this.t2b_Panel.Size = new System.Drawing.Size(733, 329);
            this.t2b_Panel.TabIndex = 6;
            // 
            // t2b_ResultPanel
            // 
            this.t2b_ResultPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t2b_ResultPanel.AutoScroll = true;
            this.t2b_ResultPanel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.t2b_ResultPanel.Location = new System.Drawing.Point(6, 97);
            this.t2b_ResultPanel.Name = "t2b_ResultPanel";
            this.t2b_ResultPanel.Size = new System.Drawing.Size(720, 226);
            this.t2b_ResultPanel.TabIndex = 3;
            // 
            // t2b_Button
            // 
            this.t2b_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t2b_Button.Image = global::Club_Project.Properties.Resources.Conversion;
            this.t2b_Button.Location = new System.Drawing.Point(646, 3);
            this.t2b_Button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.t2b_Button.Name = "t2b_Button";
            this.t2b_Button.Size = new System.Drawing.Size(70, 82);
            this.t2b_Button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.t2b_Button.TabIndex = 1;
            this.t2b_Button.TabStop = false;
            this.t2b_Button.Click += new System.EventHandler(this.t2b_Button_Click);
            // 
            // t2b_TextBox
            // 
            this.t2b_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t2b_TextBox.Location = new System.Drawing.Point(6, 3);
            this.t2b_TextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.t2b_TextBox.Name = "t2b_TextBox";
            this.t2b_TextBox.Size = new System.Drawing.Size(612, 82);
            this.t2b_TextBox.TabIndex = 0;
            this.t2b_TextBox.Text = "";
            // 
            // b2t_resultBox
            // 
            this.b2t_resultBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b2t_resultBox.Location = new System.Drawing.Point(461, 6);
            this.b2t_resultBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b2t_resultBox.Name = "b2t_resultBox";
            this.b2t_resultBox.ReadOnly = true;
            this.b2t_resultBox.Size = new System.Drawing.Size(267, 319);
            this.b2t_resultBox.TabIndex = 5;
            this.b2t_resultBox.Text = "";
            // 
            // b2t_ConversionButton
            // 
            this.b2t_ConversionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b2t_ConversionButton.Image = global::Club_Project.Properties.Resources.Conversion;
            this.b2t_ConversionButton.Location = new System.Drawing.Point(344, 150);
            this.b2t_ConversionButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b2t_ConversionButton.Name = "b2t_ConversionButton";
            this.b2t_ConversionButton.Size = new System.Drawing.Size(65, 57);
            this.b2t_ConversionButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.b2t_ConversionButton.TabIndex = 4;
            this.b2t_ConversionButton.TabStop = false;
            this.b2t_ConversionButton.Click += new System.EventHandler(this.b2t_ConversionButton_Click);
            // 
            // b2t_openButton
            // 
            this.b2t_openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b2t_openButton.Image = global::Club_Project.Properties.Resources.open;
            this.b2t_openButton.Location = new System.Drawing.Point(355, 62);
            this.b2t_openButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b2t_openButton.Name = "b2t_openButton";
            this.b2t_openButton.Size = new System.Drawing.Size(38, 40);
            this.b2t_openButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.b2t_openButton.TabIndex = 3;
            this.b2t_openButton.TabStop = false;
            this.b2t_openButton.Click += new System.EventHandler(this.b2t_openButton_Click_1);
            // 
            // b2t_openImageBox
            // 
            this.b2t_openImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b2t_openImageBox.BackColor = System.Drawing.SystemColors.Control;
            this.b2t_openImageBox.Location = new System.Drawing.Point(6, 6);
            this.b2t_openImageBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b2t_openImageBox.Name = "b2t_openImageBox";
            this.b2t_openImageBox.Size = new System.Drawing.Size(262, 317);
            this.b2t_openImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.b2t_openImageBox.TabIndex = 2;
            this.b2t_openImageBox.TabStop = false;
            this.b2t_openImageBox.Click += new System.EventHandler(this.b2t_openImageBox_Click);
            // 
            // b2t_Icon
            // 
            this.b2t_Icon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b2t_Icon.Image = global::Club_Project.Properties.Resources.b2t;
            this.b2t_Icon.Location = new System.Drawing.Point(285, 75);
            this.b2t_Icon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b2t_Icon.Name = "b2t_Icon";
            this.b2t_Icon.Size = new System.Drawing.Size(164, 36);
            this.b2t_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.b2t_Icon.TabIndex = 0;
            this.b2t_Icon.TabStop = false;
            this.b2t_Icon.Click += new System.EventHandler(this.b2t_Icon_Click);
            // 
            // t2b_Icon
            // 
            this.t2b_Icon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t2b_Icon.Image = global::Club_Project.Properties.Resources.t2b;
            this.t2b_Icon.Location = new System.Drawing.Point(285, 225);
            this.t2b_Icon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.t2b_Icon.Name = "t2b_Icon";
            this.t2b_Icon.Size = new System.Drawing.Size(164, 36);
            this.t2b_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.t2b_Icon.TabIndex = 1;
            this.t2b_Icon.TabStop = false;
            this.t2b_Icon.Click += new System.EventHandler(this.t2b_Icon_Click);
            // 
            // ImageOpen
            // 
            this.ImageOpen.FileName = "openFileDialog1";
            this.ImageOpen.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 370);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "B&T";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.homeIcon)).EndInit();
            this.startPanel.ResumeLayout(false);
            this.b2t_Panel.ResumeLayout(false);
            this.t2b_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.t2b_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2t_ConversionButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2t_openButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2t_openImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2t_Icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t2b_Icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox homeIcon;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox b2t_Icon;
        private System.Windows.Forms.PictureBox t2b_Icon;
        private System.Windows.Forms.Panel startPanel;
        private System.Windows.Forms.Panel b2t_Panel;
        private System.Windows.Forms.OpenFileDialog ImageOpen;
        private Emgu.CV.UI.ImageBox b2t_openImageBox;
        private System.Windows.Forms.PictureBox b2t_openButton;
        private System.Windows.Forms.PictureBox b2t_ConversionButton;
        private System.Windows.Forms.RichTextBox b2t_resultBox;
        private System.Windows.Forms.Panel t2b_Panel;
        private System.Windows.Forms.PictureBox t2b_Button;
        private System.Windows.Forms.RichTextBox t2b_TextBox;
        private System.Windows.Forms.FlowLayoutPanel t2b_ResultPanel;
    }
}

