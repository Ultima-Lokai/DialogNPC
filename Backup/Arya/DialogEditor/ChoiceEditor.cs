namespace Arya.DialogEditor
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using UOFont;

    public class ChoiceEditor : UserControl
    {
        private Button bAction;
        private CheckBox chkEndConversation;
        private CheckBox chkInvokeFunction;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private DialogChoice m_Choice;
        private PictureBox pct;
        private TextBox txChoice;
        private TextBox txFunction;
        private TextBox txType;

        public event EventHandler Changed;

        public event EventHandler TitleChanged;

        public event EventHandler ViewChoice;

        public ChoiceEditor()
        {
            this.InitializeComponent();
        }

        private void bAction_Click(object sender, EventArgs e)
        {
            if ((this.bAction.Text != "Choice Action : Not Set") && (this.ViewChoice != null))
            {
                this.ViewChoice(this, EventArgs.Empty);
            }
        }

        private void bAction_DragDrop(object sender, DragEventArgs e)
        {
            DialogSpeech data = e.Data.GetData(typeof(DialogSpeech)) as DialogSpeech;
            if (data != null)
            {
                this.m_Choice.ChoiceID = data.ID;
                this.bAction.Text = "Choice Action : Set";
                this.OnChanged();
            }
        }

        private void bAction_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(DialogSpeech)) is DialogSpeech)
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void chkEndConversation_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Choice.EndDialog = this.chkEndConversation.Checked;
            this.OnChanged();
        }

        private void chkInvokeFunction_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Choice.Invoke = this.chkInvokeFunction.Checked;
            this.txType.Enabled = this.m_Choice.Invoke;
            this.txFunction.Enabled = this.m_Choice.Invoke;
            this.OnChanged();
        }

        private void ChoiceEditor_Load(object sender, EventArgs e)
        {
            if (this.m_Choice == null)
            {
                this.m_Choice = new DialogChoice();
            }
            if (this.m_Choice.Text != null)
            {
                this.txChoice.Text = this.m_Choice.Text;
            }
            if (this.m_Choice.ChoiceID != Guid.Empty)
            {
                this.bAction.Text = "Choice Action : Set";
            }
            else
            {
                this.bAction.Text = "Choice Action : Not Set";
            }
            this.chkEndConversation.Checked = this.m_Choice.EndDialog;
            this.chkInvokeFunction.Checked = this.m_Choice.Invoke;
            this.txType.Enabled = this.m_Choice.Invoke;
            this.txFunction.Enabled = this.m_Choice.Invoke;
            if (this.m_Choice.Invoke)
            {
                if (this.m_Choice.InvokeType != null)
                {
                    this.txType.Text = this.m_Choice.InvokeType;
                }
                if (this.m_Choice.InvokeFunction != null)
                {
                    this.txFunction.Text = this.m_Choice.InvokeFunction;
                }
            }
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
            this.pct = new PictureBox();
            this.txChoice = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.bAction = new Button();
            this.chkEndConversation = new CheckBox();
            this.chkInvokeFunction = new CheckBox();
            this.txType = new TextBox();
            this.label4 = new Label();
            this.label5 = new Label();
            this.txFunction = new TextBox();
            base.SuspendLayout();
            this.pct.BackColor = Color.MidnightBlue;
            this.pct.BorderStyle = BorderStyle.FixedSingle;
            this.pct.Location = new Point(4, 0x38);
            this.pct.Name = "pct";
            this.pct.Size = new Size(290, 20);
            this.pct.TabIndex = 5;
            this.pct.TabStop = false;
            this.txChoice.Location = new Point(4, 20);
            this.txChoice.Name = "txChoice";
            this.txChoice.Size = new Size(290, 20);
            this.txChoice.TabIndex = 6;
            this.txChoice.Text = "";
            this.txChoice.TextChanged += new EventHandler(this.txChoice_TextChanged);
            this.label1.Location = new Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(100, 0x10);
            this.label1.TabIndex = 7;
            this.label1.Text = "Choice Text";
            this.label1.TextAlign = ContentAlignment.BottomLeft;
            this.label2.Location = new Point(4, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(100, 0x10);
            this.label2.TabIndex = 8;
            this.label2.Text = "Preview";
            this.label2.TextAlign = ContentAlignment.BottomLeft;
            this.label3.Location = new Point(4, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x124, 40);
            this.label3.TabIndex = 9;
            this.label3.Text = "Drag'n'Drop a speech node on the following button to set the dialog speech that will be displayed when this choice is made.";
            this.label3.TextAlign = ContentAlignment.BottomLeft;
            this.bAction.AllowDrop = true;
            this.bAction.FlatStyle = FlatStyle.System;
            this.bAction.Location = new Point(4, 120);
            this.bAction.Name = "bAction";
            this.bAction.Size = new Size(0x124, 0x17);
            this.bAction.TabIndex = 10;
            this.bAction.Text = "Choice Action : Not Set";
            this.bAction.Click += new EventHandler(this.bAction_Click);
            this.bAction.DragEnter += new DragEventHandler(this.bAction_DragEnter);
            this.bAction.DragDrop += new DragEventHandler(this.bAction_DragDrop);
            this.chkEndConversation.FlatStyle = FlatStyle.System;
            this.chkEndConversation.Location = new Point(4, 0x94);
            this.chkEndConversation.Name = "chkEndConversation";
            this.chkEndConversation.Size = new Size(0x68, 20);
            this.chkEndConversation.TabIndex = 11;
            this.chkEndConversation.Text = "End Conversation";
            this.chkEndConversation.CheckedChanged += new EventHandler(this.chkEndConversation_CheckedChanged);
            this.chkInvokeFunction.FlatStyle = FlatStyle.System;
            this.chkInvokeFunction.Location = new Point(4, 0xa8);
            this.chkInvokeFunction.Name = "chkInvokeFunction";
            this.chkInvokeFunction.Size = new Size(0x68, 20);
            this.chkInvokeFunction.TabIndex = 12;
            this.chkInvokeFunction.Text = "Invoke Function";
            this.chkInvokeFunction.CheckedChanged += new EventHandler(this.chkInvokeFunction_CheckedChanged);
            this.txType.Location = new Point(0x4c, 0xbc);
            this.txType.Name = "txType";
            this.txType.Size = new Size(220, 20);
            this.txType.TabIndex = 13;
            this.txType.Text = "";
            this.txType.TextChanged += new EventHandler(this.txType_TextChanged);
            this.label4.Location = new Point(20, 0xc0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(40, 0x10);
            this.label4.TabIndex = 14;
            this.label4.Text = "Type";
            this.label4.TextAlign = ContentAlignment.BottomLeft;
            this.label5.Location = new Point(20, 0xd8);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x34, 0x10);
            this.label5.TabIndex = 15;
            this.label5.Text = "Function";
            this.label5.TextAlign = ContentAlignment.BottomLeft;
            this.txFunction.Location = new Point(0x4c, 0xd4);
            this.txFunction.Name = "txFunction";
            this.txFunction.Size = new Size(220, 20);
            this.txFunction.TabIndex = 0x10;
            this.txFunction.Text = "";
            this.txFunction.TextChanged += new EventHandler(this.txFunction_TextChanged);
            base.Controls.Add(this.txFunction);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txType);
            base.Controls.Add(this.chkInvokeFunction);
            base.Controls.Add(this.chkEndConversation);
            base.Controls.Add(this.bAction);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txChoice);
            base.Controls.Add(this.pct);
            base.Name = "ChoiceEditor";
            base.Size = new Size(300, 0xec);
            base.Load += new EventHandler(this.ChoiceEditor_Load);
            base.ResumeLayout(false);
        }

        public void MakeChoiceInvalid()
        {
            MessageBox.Show("The speech selected for this choice no longer exists");
            this.m_Choice.ChoiceID = Guid.Empty;
            this.bAction.Text = "Choice Action : Not Set";
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

        private void txChoice_TextChanged(object sender, EventArgs e)
        {
            this.m_Choice.Text = this.txChoice.Text;
            this.UpdateChoicePreview();
            this.OnChanged();
            this.OnTitleChanged();
        }

        private void txFunction_TextChanged(object sender, EventArgs e)
        {
            this.m_Choice.InvokeFunction = this.txFunction.Text;
            this.OnChanged();
        }

        private void txType_TextChanged(object sender, EventArgs e)
        {
            this.m_Choice.InvokeType = this.txType.Text;
            this.OnChanged();
        }

        private void UpdateChoicePreview()
        {
            try
            {
                if ((this.m_Choice.Text == null) || (this.m_Choice.Text.Length == 0))
                {
                    this.pct.Image = null;
                }
                else
                {
                    this.pct.Image = UnicodeFonts.GetStringImage(2, this.m_Choice.Text);
                }
            }
            catch
            {
                this.pct.Image = null;
            }
        }

        public DialogChoice Choice
        {
            get
            {
                return this.m_Choice;
            }
            set
            {
                this.m_Choice = value;
            }
        }
    }
}

