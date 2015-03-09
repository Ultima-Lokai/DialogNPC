namespace Arya.DialogEditor
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DialogPreviewForm : Form
    {
        private Button b1;
        private Button b2;
        private Button b3;
        private Button b4;
        private Button b5;
        private Button b6;
        private Button b7;
        private Button b8;
        private Button bEnd;
        private Button button10;
        private Container components = null;
        private Label labDlg;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label labTitle;
        private int m_Choice = -1;
        private RichTextBox tx;

        public DialogPreviewForm()
        {
            this.InitializeComponent();
        }

        private void b1_Click(object sender, EventArgs e)
        {
            this.m_Choice = 0;
            base.Close();
        }

        private void b2_Click(object sender, EventArgs e)
        {
            this.m_Choice = 1;
            base.Close();
        }

        private void b3_Click(object sender, EventArgs e)
        {
            this.m_Choice = 2;
            base.Close();
        }

        private void b4_Click(object sender, EventArgs e)
        {
            this.m_Choice = 3;
            base.Close();
        }

        private void b5_Click(object sender, EventArgs e)
        {
            this.m_Choice = 4;
            base.Close();
        }

        private void b6_Click(object sender, EventArgs e)
        {
            this.m_Choice = 5;
            base.Close();
        }

        private void b7_Click(object sender, EventArgs e)
        {
            this.m_Choice = 6;
            base.Close();
        }

        private void b8_Click(object sender, EventArgs e)
        {
            this.m_Choice = 7;
            base.Close();
        }

        private void bEnd_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        public void DisplayDialog(string quest, string title, string text, ArrayList choices, bool allowClose)
        {
            Button[] buttonArray = new Button[] { this.b1, this.b2, this.b3, this.b4, this.b5, this.b6, this.b7, this.b8 };
            Label[] labelArray = new Label[] { this.label1, this.label2, this.label3, this.label4, this.label5, this.label6, this.label7, this.label8 };
            this.labDlg.Text = quest;
            this.labTitle.Text = title;
            this.tx.Text = text;
            for (int i = 0; i < choices.Count; i++)
            {
                DialogChoice choice = choices[i] as DialogChoice;
                buttonArray[i].Visible = true;
                labelArray[i].Text = choice.Text;
            }
            this.bEnd.Enabled = allowClose;
            this.label9.Enabled = allowClose;
            base.ShowDialog();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labDlg = new Label();
            this.labTitle = new Label();
            this.tx = new RichTextBox();
            this.b1 = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.b2 = new Button();
            this.label3 = new Label();
            this.b3 = new Button();
            this.label4 = new Label();
            this.b4 = new Button();
            this.label5 = new Label();
            this.b5 = new Button();
            this.label6 = new Label();
            this.b6 = new Button();
            this.label7 = new Label();
            this.b7 = new Button();
            this.label8 = new Label();
            this.b8 = new Button();
            this.bEnd = new Button();
            this.label9 = new Label();
            this.button10 = new Button();
            base.SuspendLayout();
            this.labDlg.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.labDlg.Location = new Point(8, 8);
            this.labDlg.Name = "labDlg";
            this.labDlg.Size = new Size(0x150, 0x17);
            this.labDlg.TabIndex = 0;
            this.labDlg.Text = "label1";
            this.labDlg.TextAlign = ContentAlignment.MiddleLeft;
            this.labTitle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.labTitle.Location = new Point(8, 0x20);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new Size(0x150, 0x17);
            this.labTitle.TabIndex = 1;
            this.labTitle.Text = "label2";
            this.labTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.tx.Location = new Point(8, 0x38);
            this.tx.Name = "tx";
            this.tx.Size = new Size(0x150, 0xd0);
            this.tx.TabIndex = 2;
            this.tx.Text = "richTextBox1";
            this.b1.FlatStyle = FlatStyle.System;
            this.b1.Location = new Point(8, 0x110);
            this.b1.Name = "b1";
            this.b1.Size = new Size(0x18, 0x17);
            this.b1.TabIndex = 3;
            this.b1.Text = ">";
            this.b1.Visible = false;
            this.b1.Click += new EventHandler(this.b1_Click);
            this.label1.Location = new Point(0x20, 0x110);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x138, 0x17);
            this.label1.TabIndex = 4;
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            this.label2.Location = new Point(0x20, 0x128);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x138, 0x17);
            this.label2.TabIndex = 6;
            this.label2.TextAlign = ContentAlignment.MiddleLeft;
            this.b2.FlatStyle = FlatStyle.System;
            this.b2.Location = new Point(8, 0x128);
            this.b2.Name = "b2";
            this.b2.Size = new Size(0x18, 0x17);
            this.b2.TabIndex = 5;
            this.b2.Text = ">";
            this.b2.Visible = false;
            this.b2.Click += new EventHandler(this.b2_Click);
            this.label3.Location = new Point(0x20, 320);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x138, 0x17);
            this.label3.TabIndex = 8;
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            this.b3.FlatStyle = FlatStyle.System;
            this.b3.Location = new Point(8, 320);
            this.b3.Name = "b3";
            this.b3.Size = new Size(0x18, 0x17);
            this.b3.TabIndex = 7;
            this.b3.Text = ">";
            this.b3.Visible = false;
            this.b3.Click += new EventHandler(this.b3_Click);
            this.label4.Location = new Point(0x20, 0x158);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x138, 0x17);
            this.label4.TabIndex = 10;
            this.label4.TextAlign = ContentAlignment.MiddleLeft;
            this.b4.FlatStyle = FlatStyle.System;
            this.b4.Location = new Point(8, 0x158);
            this.b4.Name = "b4";
            this.b4.Size = new Size(0x18, 0x17);
            this.b4.TabIndex = 9;
            this.b4.Text = ">";
            this.b4.Visible = false;
            this.b4.Click += new EventHandler(this.b4_Click);
            this.label5.Location = new Point(0x20, 0x170);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x138, 0x17);
            this.label5.TabIndex = 12;
            this.label5.TextAlign = ContentAlignment.MiddleLeft;
            this.b5.FlatStyle = FlatStyle.System;
            this.b5.Location = new Point(8, 0x170);
            this.b5.Name = "b5";
            this.b5.Size = new Size(0x18, 0x17);
            this.b5.TabIndex = 11;
            this.b5.Text = ">";
            this.b5.Visible = false;
            this.b5.Click += new EventHandler(this.b5_Click);
            this.label6.Location = new Point(0x20, 0x188);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x138, 0x17);
            this.label6.TabIndex = 14;
            this.label6.TextAlign = ContentAlignment.MiddleLeft;
            this.b6.FlatStyle = FlatStyle.System;
            this.b6.Location = new Point(8, 0x188);
            this.b6.Name = "b6";
            this.b6.Size = new Size(0x18, 0x17);
            this.b6.TabIndex = 13;
            this.b6.Text = ">";
            this.b6.Visible = false;
            this.b6.Click += new EventHandler(this.b6_Click);
            this.label7.Location = new Point(0x20, 0x1a0);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x138, 0x17);
            this.label7.TabIndex = 0x10;
            this.label7.TextAlign = ContentAlignment.MiddleLeft;
            this.b7.FlatStyle = FlatStyle.System;
            this.b7.Location = new Point(8, 0x1a0);
            this.b7.Name = "b7";
            this.b7.Size = new Size(0x18, 0x17);
            this.b7.TabIndex = 15;
            this.b7.Text = ">";
            this.b7.Visible = false;
            this.b7.Click += new EventHandler(this.b7_Click);
            this.label8.Location = new Point(0x20, 440);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x138, 0x17);
            this.label8.TabIndex = 0x12;
            this.label8.TextAlign = ContentAlignment.MiddleLeft;
            this.b8.FlatStyle = FlatStyle.System;
            this.b8.Location = new Point(8, 440);
            this.b8.Name = "b8";
            this.b8.Size = new Size(0x18, 0x17);
            this.b8.TabIndex = 0x11;
            this.b8.Text = ">";
            this.b8.Visible = false;
            this.b8.Click += new EventHandler(this.b8_Click);
            this.bEnd.FlatStyle = FlatStyle.System;
            this.bEnd.Location = new Point(8, 0x1d8);
            this.bEnd.Name = "bEnd";
            this.bEnd.Size = new Size(0x18, 0x17);
            this.bEnd.TabIndex = 0x13;
            this.bEnd.Text = ">";
            this.bEnd.Click += new EventHandler(this.bEnd_Click);
            this.label9.Location = new Point(0x20, 0x1d8);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x70, 0x17);
            this.label9.TabIndex = 20;
            this.label9.Text = "End Conversation";
            this.label9.TextAlign = ContentAlignment.MiddleLeft;
            this.button10.FlatStyle = FlatStyle.System;
            this.button10.Location = new Point(0xe8, 0x1d8);
            this.button10.Name = "button10";
            this.button10.Size = new Size(0x70, 0x17);
            this.button10.TabIndex = 0x15;
            this.button10.Text = "Stop Testing";
            this.button10.Click += new EventHandler(this.button10_Click);
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x160, 0x1f6);
            base.Controls.Add(this.button10);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.bEnd);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.b8);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.b7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.b6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.b5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.b4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.b3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.b2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.b1);
            base.Controls.Add(this.tx);
            base.Controls.Add(this.labTitle);
            base.Controls.Add(this.labDlg);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Name = "DialogPreviewForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "DialogPreviewForm";
            base.ResumeLayout(false);
        }

        public int Choice
        {
            get
            {
                return this.m_Choice;
            }
        }
    }
}

