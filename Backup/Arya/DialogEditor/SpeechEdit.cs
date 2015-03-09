namespace Arya.DialogEditor
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using UOFont;

    public class SpeechEdit : UserControl
    {
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private DialogSpeech m_Speech;
        private PictureBox pct;
        private TextBox txSpeech;
        private TextBox txTitle;

        public event EventHandler Changed;

        public event EventHandler TitleChanged;

        public SpeechEdit()
        {
            this.InitializeComponent();
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
            this.txTitle = new TextBox();
            this.txSpeech = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.pct = new PictureBox();
            this.label3 = new Label();
            base.SuspendLayout();
            this.txTitle.Location = new Point(4, 0x10);
            this.txTitle.Name = "txTitle";
            this.txTitle.Size = new Size(0x138, 20);
            this.txTitle.TabIndex = 0;
            this.txTitle.Text = "";
            this.txTitle.TextChanged += new EventHandler(this.txTitle_TextChanged);
            this.txSpeech.Location = new Point(4, 0x58);
            this.txSpeech.Multiline = true;
            this.txSpeech.Name = "txSpeech";
            this.txSpeech.Size = new Size(0x138, 0x90);
            this.txSpeech.TabIndex = 1;
            this.txSpeech.Text = "";
            this.txSpeech.TextChanged += new EventHandler(this.txSpeech_TextChanged);
            this.label1.Location = new Point(4, 0x24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(100, 0x10);
            this.label1.TabIndex = 2;
            this.label1.Text = "Title Preview";
            this.label1.TextAlign = ContentAlignment.BottomLeft;
            this.label2.Location = new Point(4, 0x48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(100, 0x10);
            this.label2.TabIndex = 3;
            this.label2.Text = "Speech Text";
            this.label2.TextAlign = ContentAlignment.BottomLeft;
            this.pct.BackColor = Color.MidnightBlue;
            this.pct.BorderStyle = BorderStyle.FixedSingle;
            this.pct.Location = new Point(4, 0x34);
            this.pct.Name = "pct";
            this.pct.Size = new Size(310, 20);
            this.pct.TabIndex = 4;
            this.pct.TabStop = false;
            this.label3.Location = new Point(4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(100, 0x10);
            this.label3.TabIndex = 5;
            this.label3.Text = "Title";
            this.label3.TextAlign = ContentAlignment.BottomLeft;
            base.Controls.Add(this.label3);
            base.Controls.Add(this.pct);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txSpeech);
            base.Controls.Add(this.txTitle);
            base.Name = "SpeechEdit";
            base.Size = new Size(320, 0xec);
            base.Load += new EventHandler(this.SpeechEdit_Load);
            base.ResumeLayout(false);
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

        private void SpeechEdit_Load(object sender, EventArgs e)
        {
            if (this.m_Speech == null)
            {
                this.m_Speech = new DialogSpeech();
            }
            if (this.m_Speech.Title != null)
            {
                this.txTitle.Text = this.m_Speech.Title;
            }
            if (this.m_Speech.Text != null)
            {
                this.txSpeech.Text = this.m_Speech.Text;
            }
        }

        private void txSpeech_TextChanged(object sender, EventArgs e)
        {
            this.m_Speech.Text = this.txSpeech.Text;
            this.OnChanged();
        }

        private void txTitle_TextChanged(object sender, EventArgs e)
        {
            this.m_Speech.Title = this.txTitle.Text;
            this.UpdateTitlePreview();
            this.OnTitleChanged();
            this.OnChanged();
        }

        private void UpdateTitlePreview()
        {
            try
            {
                if ((this.m_Speech.Title == null) || (this.m_Speech.Title.Length == 0))
                {
                    this.pct.Image = null;
                }
                else
                {
                    this.pct.Image = UnicodeFonts.GetStringImage(2, this.m_Speech.Title);
                }
            }
            catch
            {
                this.pct.Image = null;
            }
        }

        public DialogSpeech Speech
        {
            get
            {
                return this.m_Speech;
            }
            set
            {
                this.m_Speech = value;
            }
        }
    }
}

