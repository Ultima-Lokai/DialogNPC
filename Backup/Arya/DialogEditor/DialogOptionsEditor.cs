namespace Arya.DialogEditor
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using UOFont;

    public class DialogOptionsEditor : UserControl
    {
        private CheckBox chkAllowClose;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Arya.DialogEditor.Dialog m_Dialog;
        private NumericUpDown nRange;
        private NumericUpDown nSpeechRange;
        private PictureBox pct;
        private TextBox txTitle;

        public event EventHandler Changed;

        public event EventHandler TitleChanged;

        public DialogOptionsEditor()
        {
            this.InitializeComponent();
        }

        private void chkAllowClose_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Dialog.AllowClose = this.chkAllowClose.Checked;
            this.OnChanged();
        }

        private void DialogOptionsEditor_Load(object sender, EventArgs e)
        {
            if (this.m_Dialog == null)
            {
                this.m_Dialog = new Arya.DialogEditor.Dialog();
            }
            this.txTitle.Text = this.m_Dialog.Title;
            this.nRange.Value = this.m_Dialog.Range;
            this.nSpeechRange.Value = this.m_Dialog.SpeechRange;
            this.chkAllowClose.Checked = this.m_Dialog.AllowClose;
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
            this.label1 = new Label();
            this.txTitle = new TextBox();
            this.label2 = new Label();
            this.pct = new PictureBox();
            this.chkAllowClose = new CheckBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.nRange = new NumericUpDown();
            this.nSpeechRange = new NumericUpDown();
            this.nRange.BeginInit();
            this.nSpeechRange.BeginInit();
            base.SuspendLayout();
            this.label1.Location = new Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(100, 0x10);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            this.label1.TextAlign = ContentAlignment.BottomLeft;
            this.txTitle.Location = new Point(4, 20);
            this.txTitle.Name = "txTitle";
            this.txTitle.Size = new Size(310, 20);
            this.txTitle.TabIndex = 1;
            this.txTitle.Text = "";
            this.txTitle.TextChanged += new EventHandler(this.txTitle_TextChanged);
            this.label2.Location = new Point(4, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(100, 0x10);
            this.label2.TabIndex = 2;
            this.label2.Text = "Preview";
            this.label2.TextAlign = ContentAlignment.BottomLeft;
            this.pct.BackColor = Color.MidnightBlue;
            this.pct.BorderStyle = BorderStyle.FixedSingle;
            this.pct.Location = new Point(4, 0x38);
            this.pct.Name = "pct";
            this.pct.Size = new Size(310, 20);
            this.pct.TabIndex = 6;
            this.pct.TabStop = false;
            this.chkAllowClose.FlatStyle = FlatStyle.System;
            this.chkAllowClose.Location = new Point(4, 0x7c);
            this.chkAllowClose.Name = "chkAllowClose";
            this.chkAllowClose.Size = new Size(0xfc, 20);
            this.chkAllowClose.TabIndex = 7;
            this.chkAllowClose.Text = "Allow players to end the conversation";
            this.chkAllowClose.CheckedChanged += new EventHandler(this.chkAllowClose_CheckedChanged);
            this.label3.Location = new Point(4, 0x68);
            this.label3.Name = "label3";
            this.label3.Size = new Size(100, 0x10);
            this.label3.TabIndex = 8;
            this.label3.Text = "Speech Range";
            this.label3.TextAlign = ContentAlignment.BottomLeft;
            this.label4.Location = new Point(4, 0x54);
            this.label4.Name = "label4";
            this.label4.Size = new Size(100, 0x10);
            this.label4.TabIndex = 9;
            this.label4.Text = "Reaction Range";
            this.label4.TextAlign = ContentAlignment.BottomLeft;
            this.nRange.Location = new Point(0x6c, 80);
            int[] bits = new int[4];
            bits[0] = 15;
            this.nRange.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.nRange.Minimum = new decimal(bits);
            this.nRange.Name = "nRange";
            this.nRange.Size = new Size(0x38, 20);
            this.nRange.TabIndex = 10;
            bits = new int[4];
            bits[0] = 1;
            this.nRange.Value = new decimal(bits);
            this.nRange.ValueChanged += new EventHandler(this.nRange_ValueChanged);
            this.nSpeechRange.Location = new Point(0x6c, 0x68);
            bits = new int[4];
            bits[0] = 15;
            this.nSpeechRange.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.nSpeechRange.Minimum = new decimal(bits);
            this.nSpeechRange.Name = "nSpeechRange";
            this.nSpeechRange.Size = new Size(0x38, 20);
            this.nSpeechRange.TabIndex = 11;
            bits = new int[4];
            bits[0] = 1;
            this.nSpeechRange.Value = new decimal(bits);
            this.nSpeechRange.ValueChanged += new EventHandler(this.nSpeechRange_ValueChanged);
            this.nSpeechRange.Leave += new EventHandler(this.nSpeechRange_ValueChanged);
            base.Controls.Add(this.nSpeechRange);
            base.Controls.Add(this.nRange);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.chkAllowClose);
            base.Controls.Add(this.pct);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txTitle);
            base.Controls.Add(this.label1);
            base.Name = "DialogOptionsEditor";
            base.Size = new Size(0x148, 0xec);
            base.Load += new EventHandler(this.DialogOptionsEditor_Load);
            this.nRange.EndInit();
            this.nSpeechRange.EndInit();
            base.ResumeLayout(false);
        }

        private void nRange_ValueChanged(object sender, EventArgs e)
        {
            this.m_Dialog.Range = (int) this.nRange.Value;
            this.OnChanged();
        }

        private void nSpeechRange_ValueChanged(object sender, EventArgs e)
        {
            this.m_Dialog.SpeechRange = (int) this.nSpeechRange.Value;
            this.OnChanged();
        }

        protected virtual void OnChanged()
        {
            if (this.Changed != null)
            {
                this.Changed(this, EventArgs.Empty);
            }
        }

        protected virtual void OnTitleChanged()
        {
            if (this.TitleChanged != null)
            {
                this.TitleChanged(this, EventArgs.Empty);
            }
        }

        private void txTitle_TextChanged(object sender, EventArgs e)
        {
            this.m_Dialog.Title = this.txTitle.Text;
            this.UpdatePreview();
            this.OnChanged();
            this.OnTitleChanged();
        }

        private void UpdatePreview()
        {
            try
            {
                if ((this.m_Dialog.Title != null) && (this.m_Dialog.Title.Length > 0))
                {
                    this.pct.Image = UnicodeFonts.GetStringImage(2, this.m_Dialog.Title);
                }
                else
                {
                    this.pct.Image = null;
                }
            }
            catch
            {
                this.pct.Image = null;
            }
        }

        public Arya.DialogEditor.Dialog Dialog
        {
            get
            {
                return this.m_Dialog;
            }
            set
            {
                this.m_Dialog = value;
            }
        }
    }
}

