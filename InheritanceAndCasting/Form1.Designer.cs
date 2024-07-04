namespace InheritanceAndCasting
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
            Btn_Circle = new Button();
            Btn_Rectangle = new Button();
            statusStrip1 = new StatusStrip();
            Txt_Dimensions = new ToolStripStatusLabel();
            Txt_Coords = new ToolStripStatusLabel();
            Txt_Status = new ToolStripStatusLabel();
            panel1 = new Panel();
            Btn_RandomShapes = new Button();
            Num_TotalAmountOfParticularShape = new NumericUpDown();
            Btn_Arc = new Button();
            Btn_Lines = new Button();
            Txt_Console = new RichTextBox();
            Btn_Cancel = new Button();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Num_TotalAmountOfParticularShape).BeginInit();
            SuspendLayout();
            // 
            // Btn_Circle
            // 
            Btn_Circle.Location = new Point(12, 12);
            Btn_Circle.Name = "Btn_Circle";
            Btn_Circle.Size = new Size(75, 23);
            Btn_Circle.TabIndex = 1;
            Btn_Circle.Text = "Circle";
            Btn_Circle.UseVisualStyleBackColor = true;
            Btn_Circle.Click += Btn_Circle_Click;
            // 
            // Btn_Rectangle
            // 
            Btn_Rectangle.Location = new Point(93, 12);
            Btn_Rectangle.Name = "Btn_Rectangle";
            Btn_Rectangle.Size = new Size(75, 23);
            Btn_Rectangle.TabIndex = 2;
            Btn_Rectangle.Text = "Rectangle";
            Btn_Rectangle.UseVisualStyleBackColor = true;
            Btn_Rectangle.Click += Btn_Rectangle_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { Txt_Dimensions, Txt_Coords, Txt_Status });
            statusStrip1.Location = new Point(0, 514);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(869, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // Txt_Dimensions
            // 
            Txt_Dimensions.Name = "Txt_Dimensions";
            Txt_Dimensions.Size = new Size(49, 17);
            Txt_Dimensions.Text = "100 x 80";
            // 
            // Txt_Coords
            // 
            Txt_Coords.Name = "Txt_Coords";
            Txt_Coords.Size = new Size(55, 17);
            Txt_Coords.Text = "100 x 100";
            // 
            // Txt_Status
            // 
            Txt_Status.Name = "Txt_Status";
            Txt_Status.Size = new Size(92, 17);
            Txt_Status.Text = "Unknown status";
            // 
            // panel1
            // 
            panel1.Controls.Add(Btn_Cancel);
            panel1.Controls.Add(Btn_RandomShapes);
            panel1.Controls.Add(Num_TotalAmountOfParticularShape);
            panel1.Controls.Add(Btn_Arc);
            panel1.Controls.Add(Btn_Lines);
            panel1.Controls.Add(Btn_Rectangle);
            panel1.Controls.Add(Btn_Circle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(869, 46);
            panel1.TabIndex = 4;
            // 
            // Btn_RandomShapes
            // 
            Btn_RandomShapes.Location = new Point(369, 12);
            Btn_RandomShapes.Name = "Btn_RandomShapes";
            Btn_RandomShapes.Size = new Size(103, 23);
            Btn_RandomShapes.TabIndex = 6;
            Btn_RandomShapes.Text = "Random Shapes";
            Btn_RandomShapes.UseVisualStyleBackColor = true;
            Btn_RandomShapes.Click += Btn_RandomShapes_Click;
            // 
            // Num_TotalAmountOfParticularShape
            // 
            Num_TotalAmountOfParticularShape.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            Num_TotalAmountOfParticularShape.Location = new Point(478, 12);
            Num_TotalAmountOfParticularShape.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            Num_TotalAmountOfParticularShape.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            Num_TotalAmountOfParticularShape.Name = "Num_TotalAmountOfParticularShape";
            Num_TotalAmountOfParticularShape.Size = new Size(75, 23);
            Num_TotalAmountOfParticularShape.TabIndex = 5;
            Num_TotalAmountOfParticularShape.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // Btn_Arc
            // 
            Btn_Arc.Location = new Point(255, 12);
            Btn_Arc.Name = "Btn_Arc";
            Btn_Arc.Size = new Size(75, 23);
            Btn_Arc.TabIndex = 4;
            Btn_Arc.Text = "Arc";
            Btn_Arc.UseVisualStyleBackColor = true;
            Btn_Arc.Click += Btn_Arc_Click;
            // 
            // Btn_Lines
            // 
            Btn_Lines.Location = new Point(174, 12);
            Btn_Lines.Name = "Btn_Lines";
            Btn_Lines.Size = new Size(75, 23);
            Btn_Lines.TabIndex = 3;
            Btn_Lines.Text = "Line";
            Btn_Lines.UseVisualStyleBackColor = true;
            Btn_Lines.Click += Btn_Lines_Click;
            // 
            // Txt_Console
            // 
            Txt_Console.AutoWordSelection = true;
            Txt_Console.BackColor = SystemColors.WindowFrame;
            Txt_Console.Dock = DockStyle.Bottom;
            Txt_Console.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Txt_Console.ForeColor = SystemColors.Info;
            Txt_Console.Location = new Point(0, 394);
            Txt_Console.Name = "Txt_Console";
            Txt_Console.Size = new Size(869, 120);
            Txt_Console.TabIndex = 5;
            Txt_Console.Text = "The Console";
            // 
            // Btn_Cancel
            // 
            Btn_Cancel.Location = new Point(780, 12);
            Btn_Cancel.Name = "Btn_Cancel";
            Btn_Cancel.Size = new Size(77, 23);
            Btn_Cancel.TabIndex = 7;
            Btn_Cancel.Text = "Cancel";
            Btn_Cancel.UseVisualStyleBackColor = true;
            Btn_Cancel.Click += Btn_Cancel_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(869, 536);
            Controls.Add(Txt_Console);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Num_TotalAmountOfParticularShape).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Btn_Circle;
        private Button Btn_Rectangle;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel Txt_Dimensions;
        private Panel panel1;
        private ToolStripStatusLabel Txt_Status;
        private RichTextBox Txt_Console;
        private Button Btn_Lines;
        private ToolStripStatusLabel Txt_Coords;
        private NumericUpDown Num_TotalAmountOfParticularShape;
        private Button Btn_Arc;
        private Button Btn_RandomShapes;
        private Button Btn_Cancel;
    }
}
