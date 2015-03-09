namespace Arya.DialogEditor
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class InitEditor : UserControl
    {
        private Button bSpeech;
        private CheckBox chkDblClick;
        private CheckBox chkFunction;
        private CheckBox chkItemGiven;
        private CheckBox chkItemPack;
        private CheckBox chkKeywords;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private DialogInit m_Init;
        private NumericUpDown nGivenAmount;
        private NumericUpDown nPackAmount;
        private TextBox txFunctionName;
        private TextBox txFunctionType;
        private TextBox txGivenType;
        private TextBox txKeywords;
        private TextBox txPackType;

        public event EventHandler Changed;

        public event EventHandler ViewSpeech;

        public InitEditor()
        {
            this.InitializeComponent();
        }

        private void bGivenAmount_ValueChanged(object sender, EventArgs e)
        {
            this.m_Init.AmountGiven = (int) this.nGivenAmount.Value;
            this.OnChanged();
        }

        private void bSpeech_Click(object sender, EventArgs e)
        {
            if ((this.bSpeech.Text != "Initial Speech : Not Set") && (this.ViewSpeech != null))
            {
                this.ViewSpeech(this, EventArgs.Empty);
            }
        }

        private void bSpeech_DragDrop(object sender, DragEventArgs e)
        {
            DialogSpeech data = e.Data.GetData(typeof(DialogSpeech)) as DialogSpeech;
            if (data != null)
            {
                this.m_Init.Speech = data.ID;
                this.bSpeech.Text = "Initial Speech : Set";
                this.OnChanged();
            }
        }

        private void bSpeech_DragEnter(object sender, DragEventArgs e)
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

        private void chkDblClick_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Init.ReactToDoubleClick = this.chkDblClick.Checked;
            this.OnChanged();
        }

        private void chkFunction_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Init.TriggerFunction = this.chkFunction.Checked;
            this.OnChanged();
        }

        private void chkItemGiven_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Init.ReactToItemGiven = this.chkItemGiven.Checked;
            this.txGivenType.Enabled = this.m_Init.ReactToItemGiven;
            this.nGivenAmount.Enabled = this.m_Init.ReactToItemGiven;
            this.OnChanged();
        }

        private void chkItemPack_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Init.ReactToItemInBackpack = this.chkItemPack.Checked;
            this.txPackType.Enabled = this.m_Init.ReactToItemInBackpack;
            this.nPackAmount.Enabled = this.m_Init.ReactToItemInBackpack;
            this.OnChanged();
        }

        private void chkKeywords_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Init.ReactToKeywords = this.chkKeywords.Checked;
            this.txKeywords.Enabled = this.m_Init.ReactToKeywords;
            this.OnChanged();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitEditor_Load(object sender, EventArgs e)
        {
            if (this.m_Init == null)
            {
                this.m_Init = new DialogInit();
            }
            this.chkKeywords.Checked = this.m_Init.ReactToKeywords;
            this.txKeywords.Text = this.m_Init.KeywordsString;
            this.txKeywords.Enabled = this.m_Init.ReactToKeywords;
            this.chkDblClick.Checked = this.m_Init.ReactToDoubleClick;
            this.chkItemGiven.Checked = this.m_Init.ReactToItemGiven;
            this.txGivenType.Text = this.m_Init.TypeGiven;
            this.nGivenAmount.Value = this.m_Init.AmountGiven;
            this.txGivenType.Enabled = this.m_Init.ReactToItemGiven;
            this.nGivenAmount.Enabled = this.m_Init.ReactToItemGiven;
            this.chkItemPack.Checked = this.m_Init.ReactToItemInBackpack;
            this.txPackType.Text = this.m_Init.TypeBackpack;
            this.nPackAmount.Value = this.m_Init.AmountBackpack;
            this.txPackType.Enabled = this.m_Init.ReactToItemInBackpack;
            this.nPackAmount.Enabled = this.m_Init.ReactToItemInBackpack;
            if (this.m_Init.Speech != Guid.Empty)
            {
                this.bSpeech.Text = "Initial Speech : Set";
            }
            else
            {
                this.bSpeech.Text = "Initial Speech : Not Set";
            }
            this.chkFunction.Checked = this.m_Init.TriggerFunction;
            if (this.m_Init.FunctionName != null)
            {
                this.txFunctionName.Text = this.m_Init.FunctionName;
            }
            if (this.m_Init.FunctionType != null)
            {
                this.txFunctionType.Text = this.m_Init.FunctionType;
            }
        }

        private void InitializeComponent()
        {
            this.chkKeywords = new CheckBox();
            this.txKeywords = new TextBox();
            this.chkDblClick = new CheckBox();
            this.chkItemGiven = new CheckBox();
            this.label1 = new Label();
            this.txGivenType = new TextBox();
            this.label2 = new Label();
            this.nGivenAmount = new NumericUpDown();
            this.chkItemPack = new CheckBox();
            this.nPackAmount = new NumericUpDown();
            this.label3 = new Label();
            this.txPackType = new TextBox();
            this.label4 = new Label();
            this.bSpeech = new Button();
            this.label5 = new Label();
            this.chkFunction = new CheckBox();
            this.label6 = new Label();
            this.txFunctionType = new TextBox();
            this.label7 = new Label();
            this.txFunctionName = new TextBox();
            this.nGivenAmount.BeginInit();
            this.nPackAmount.BeginInit();
            base.SuspendLayout();
            this.chkKeywords.FlatStyle = FlatStyle.System;
            this.chkKeywords.Location = new Point(4, 4);
            this.chkKeywords.Name = "chkKeywords";
            this.chkKeywords.Size = new Size(0x124, 0x10);
            this.chkKeywords.TabIndex = 0;
            this.chkKeywords.Text = "React to keywords: separate keywords with a comma";
            this.chkKeywords.CheckedChanged += new EventHandler(this.chkKeywords_CheckedChanged);
            this.txKeywords.Location = new Point(0x18, 0x18);
            this.txKeywords.Name = "txKeywords";
            this.txKeywords.Size = new Size(0x110, 20);
            this.txKeywords.TabIndex = 1;
            this.txKeywords.Text = "";
            this.txKeywords.TextChanged += new EventHandler(this.txKeywords_TextChanged);
            this.chkDblClick.FlatStyle = FlatStyle.System;
            this.chkDblClick.Location = new Point(4, 0x2c);
            this.chkDblClick.Name = "chkDblClick";
            this.chkDblClick.Size = new Size(0x124, 0x10);
            this.chkDblClick.TabIndex = 2;
            this.chkDblClick.Text = "React to a double click on the NPC";
            this.chkDblClick.CheckedChanged += new EventHandler(this.chkDblClick_CheckedChanged);
            this.chkItemGiven.FlatStyle = FlatStyle.System;
            this.chkItemGiven.Location = new Point(4, 60);
            this.chkItemGiven.Name = "chkItemGiven";
            this.chkItemGiven.Size = new Size(0x124, 0x10);
            this.chkItemGiven.TabIndex = 3;
            this.chkItemGiven.Text = "React to an item given to the NPC";
            this.chkItemGiven.CheckedChanged += new EventHandler(this.chkItemGiven_CheckedChanged);
            this.label1.Location = new Point(20, 80);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x44, 0x10);
            this.label1.TabIndex = 4;
            this.label1.Text = "Type";
            this.txGivenType.Location = new Point(0x5c, 0x4c);
            this.txGivenType.Name = "txGivenType";
            this.txGivenType.Size = new Size(0xcc, 20);
            this.txGivenType.TabIndex = 5;
            this.txGivenType.Text = "";
            this.txGivenType.TextChanged += new EventHandler(this.txGivenType_TextChanged);
            this.label2.Location = new Point(20, 100);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x44, 0x10);
            this.label2.TabIndex = 6;
            this.label2.Text = "Min. Amount";
            this.nGivenAmount.Location = new Point(0x5c, 0x60);
            int[] bits = new int[4];
            bits[0] = 0xea60;
            this.nGivenAmount.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.nGivenAmount.Minimum = new decimal(bits);
            this.nGivenAmount.Name = "nGivenAmount";
            this.nGivenAmount.Size = new Size(0x40, 20);
            this.nGivenAmount.TabIndex = 7;
            bits = new int[4];
            bits[0] = 1;
            this.nGivenAmount.Value = new decimal(bits);
            this.nGivenAmount.ValueChanged += new EventHandler(this.bGivenAmount_ValueChanged);
            this.nGivenAmount.Leave += new EventHandler(this.bGivenAmount_ValueChanged);
            this.chkItemPack.FlatStyle = FlatStyle.System;
            this.chkItemPack.Location = new Point(4, 120);
            this.chkItemPack.Name = "chkItemPack";
            this.chkItemPack.Size = new Size(0x124, 0x10);
            this.chkItemPack.TabIndex = 8;
            this.chkItemPack.Text = "React to an item in the player's backpack";
            this.chkItemPack.CheckedChanged += new EventHandler(this.chkItemPack_CheckedChanged);
            this.nPackAmount.Location = new Point(0x5c, 0x9c);
            bits = new int[4];
            bits[0] = 0xea60;
            this.nPackAmount.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.nPackAmount.Minimum = new decimal(bits);
            this.nPackAmount.Name = "nPackAmount";
            this.nPackAmount.Size = new Size(0x40, 20);
            this.nPackAmount.TabIndex = 12;
            bits = new int[4];
            bits[0] = 1;
            this.nPackAmount.Value = new decimal(bits);
            this.nPackAmount.ValueChanged += new EventHandler(this.nPackAmount_ValueChanged);
            this.label3.Location = new Point(20, 160);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x44, 0x10);
            this.label3.TabIndex = 11;
            this.label3.Text = "Min. Amount";
            this.txPackType.Location = new Point(0x5c, 0x88);
            this.txPackType.Name = "txPackType";
            this.txPackType.Size = new Size(0xcc, 20);
            this.txPackType.TabIndex = 10;
            this.txPackType.Text = "";
            this.txPackType.TextChanged += new EventHandler(this.txPackType_TextChanged);
            this.label4.Location = new Point(20, 140);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x44, 0x10);
            this.label4.TabIndex = 9;
            this.label4.Text = "Type";
            this.bSpeech.AllowDrop = true;
            this.bSpeech.FlatStyle = FlatStyle.System;
            this.bSpeech.Location = new Point(4, 0xd0);
            this.bSpeech.Name = "bSpeech";
            this.bSpeech.Size = new Size(0x124, 0x17);
            this.bSpeech.TabIndex = 13;
            this.bSpeech.Text = "Initial Speech : Not Set";
            this.bSpeech.Click += new EventHandler(this.bSpeech_Click);
            this.bSpeech.DragEnter += new DragEventHandler(this.bSpeech_DragEnter);
            this.bSpeech.DragDrop += new DragEventHandler(this.bSpeech_DragDrop);
            this.label5.Location = new Point(4, 0xb0);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x124, 0x1c);
            this.label5.TabIndex = 14;
            this.label5.Text = "Drag'n'Drop a speech node on the button to set the speech displayed to the player when this condition is met";
            this.chkFunction.FlatStyle = FlatStyle.System;
            this.chkFunction.Location = new Point(300, 4);
            this.chkFunction.Name = "chkFunction";
            this.chkFunction.Size = new Size(0x6c, 0x10);
            this.chkFunction.TabIndex = 15;
            this.chkFunction.Text = "Trigger Function";
            this.chkFunction.CheckedChanged += new EventHandler(this.chkFunction_CheckedChanged);
            this.label6.Location = new Point(300, 0x18);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x34, 0x10);
            this.label6.TabIndex = 0x10;
            this.label6.Text = "Type";
            this.txFunctionType.Location = new Point(0x160, 20);
            this.txFunctionType.Name = "txFunctionType";
            this.txFunctionType.Size = new Size(0xec, 20);
            this.txFunctionType.TabIndex = 0x11;
            this.txFunctionType.Text = "";
            this.txFunctionType.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.label7.Location = new Point(300, 0x2c);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x34, 0x10);
            this.label7.TabIndex = 0x12;
            this.label7.Text = "Name";
            this.txFunctionName.Location = new Point(0x160, 0x2c);
            this.txFunctionName.Name = "txFunctionName";
            this.txFunctionName.Size = new Size(0xec, 20);
            this.txFunctionName.TabIndex = 0x13;
            this.txFunctionName.Text = "";
            this.txFunctionName.TextChanged += new EventHandler(this.txFunctionName_TextChanged);
            base.Controls.Add(this.txFunctionName);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.txFunctionType);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.chkFunction);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.bSpeech);
            base.Controls.Add(this.nPackAmount);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txPackType);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.chkItemPack);
            base.Controls.Add(this.nGivenAmount);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txGivenType);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.chkItemGiven);
            base.Controls.Add(this.chkDblClick);
            base.Controls.Add(this.txKeywords);
            base.Controls.Add(this.chkKeywords);
            base.Name = "InitEditor";
            base.Size = new Size(0x250, 0xec);
            base.Load += new EventHandler(this.InitEditor_Load);
            this.nGivenAmount.EndInit();
            this.nPackAmount.EndInit();
            base.ResumeLayout(false);
        }

        public void MakeSpeechInvalid()
        {
            MessageBox.Show("The speech selected for this condition no longer exists");
            this.m_Init.Speech = Guid.Empty;
            this.bSpeech.Text = "Initial Speech : Not Set";
            this.OnChanged();
        }

        private void nPackAmount_ValueChanged(object sender, EventArgs e)
        {
            this.m_Init.AmountBackpack = (int) this.nPackAmount.Value;
            this.OnChanged();
        }

        protected virtual void OnChanged()
        {
            if (this.Changed != null)
            {
                this.Changed(this, EventArgs.Empty);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.m_Init.FunctionType = this.txFunctionType.Text;
            this.OnChanged();
        }

        private void txFunctionName_TextChanged(object sender, EventArgs e)
        {
            this.m_Init.FunctionName = this.txFunctionName.Text;
            this.OnChanged();
        }

        private void txGivenType_TextChanged(object sender, EventArgs e)
        {
            this.m_Init.TypeGiven = this.txGivenType.Text;
            this.OnChanged();
        }

        private void txKeywords_TextChanged(object sender, EventArgs e)
        {
            this.m_Init.KeywordsString = this.txKeywords.Text;
            this.OnChanged();
        }

        private void txPackType_TextChanged(object sender, EventArgs e)
        {
            this.m_Init.TypeBackpack = this.txPackType.Text;
            this.OnChanged();
        }

        public DialogInit Init
        {
            get
            {
                return this.m_Init;
            }
            set
            {
                this.m_Init = value;
            }
        }
    }
}

