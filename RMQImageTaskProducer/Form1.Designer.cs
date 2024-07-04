namespace RMQImageTaskProducer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            Btn_StartStop = new Button();
            statusStrip1 = new StatusStrip();
            Txt_Status = new ToolStripStatusLabel();
            richTextBox1 = new RichTextBox();
            timer2 = new System.Windows.Forms.Timer(components);
            Chk_ImageServiceEnabled = new CheckBox();
            Chk_UpperCaseServiceEnabled = new CheckBox();
            Num_UpperCaseTaskMax = new NumericUpDown();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Num_UpperCaseTaskMax).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Interval = 6000;
            // 
            // Btn_StartStop
            // 
            Btn_StartStop.Location = new Point(12, 12);
            Btn_StartStop.Name = "Btn_StartStop";
            Btn_StartStop.Size = new Size(111, 39);
            Btn_StartStop.TabIndex = 0;
            Btn_StartStop.Text = "Start";
            Btn_StartStop.UseVisualStyleBackColor = true;
            Btn_StartStop.Click += Btn_StartStop_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { Txt_Status });
            statusStrip1.Location = new Point(0, 347);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(841, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // Txt_Status
            // 
            Txt_Status.Name = "Txt_Status";
            Txt_Status.Size = new Size(51, 17);
            Txt_Status.Text = "Stopped";
            // 
            // richTextBox1
            // 
            richTextBox1.AutoWordSelection = true;
            richTextBox1.Location = new Point(240, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(589, 332);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // timer2
            // 
            timer2.Interval = 5000;
            // 
            // Chk_ImageServiceEnabled
            // 
            Chk_ImageServiceEnabled.AutoSize = true;
            Chk_ImageServiceEnabled.Location = new Point(12, 72);
            Chk_ImageServiceEnabled.Name = "Chk_ImageServiceEnabled";
            Chk_ImageServiceEnabled.Size = new Size(138, 19);
            Chk_ImageServiceEnabled.TabIndex = 3;
            Chk_ImageServiceEnabled.Text = "ImageServiceEnabled";
            Chk_ImageServiceEnabled.UseVisualStyleBackColor = true;
            // 
            // Chk_UpperCaseServiceEnabled
            // 
            Chk_UpperCaseServiceEnabled.AutoSize = true;
            Chk_UpperCaseServiceEnabled.Checked = true;
            Chk_UpperCaseServiceEnabled.CheckState = CheckState.Checked;
            Chk_UpperCaseServiceEnabled.Location = new Point(12, 97);
            Chk_UpperCaseServiceEnabled.Name = "Chk_UpperCaseServiceEnabled";
            Chk_UpperCaseServiceEnabled.Size = new Size(162, 19);
            Chk_UpperCaseServiceEnabled.TabIndex = 4;
            Chk_UpperCaseServiceEnabled.Text = "UpperCaseServiceEnabled";
            Chk_UpperCaseServiceEnabled.UseVisualStyleBackColor = true;
            // 
            // Num_UpperCaseTaskMax
            // 
            Num_UpperCaseTaskMax.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Num_UpperCaseTaskMax.Location = new Point(12, 122);
            Num_UpperCaseTaskMax.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            Num_UpperCaseTaskMax.Name = "Num_UpperCaseTaskMax";
            Num_UpperCaseTaskMax.Size = new Size(71, 33);
            Num_UpperCaseTaskMax.TabIndex = 5;
            Num_UpperCaseTaskMax.Value = new decimal(new int[] { 3, 0, 0, 0 });
            Num_UpperCaseTaskMax.ValueChanged += Num_UpperCaseTaskMax_ValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(841, 369);
            Controls.Add(Num_UpperCaseTaskMax);
            Controls.Add(Chk_UpperCaseServiceEnabled);
            Controls.Add(Chk_ImageServiceEnabled);
            Controls.Add(richTextBox1);
            Controls.Add(statusStrip1);
            Controls.Add(Btn_StartStop);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Num_UpperCaseTaskMax).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Button Btn_StartStop;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel Txt_Status;
        private RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timer2;
        private CheckBox Chk_ImageServiceEnabled;
        private CheckBox Chk_UpperCaseServiceEnabled;
        private NumericUpDown Num_UpperCaseTaskMax;
    }
}
