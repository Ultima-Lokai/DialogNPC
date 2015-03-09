namespace Arya.DialogEditor
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class PropertiesEditor : UserControl
    {
        private Button bAdd;
        private Button bDel;
        private Button bMountHue;
        private ComboBox cmbAI;
        private ComboBox cmbDamage;
        private ComboBox cmbFightMode;
        private ComboBox cmbResist;
        private Container components = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ListBox lst;
        private NPCProps m_Props;
        private NumericUpDown nFame;
        private NumericUpDown nKarma;
        private NumericUpDown nMountHue;
        private TextBox txDam;
        private TextBox txDamage;
        private TextBox txHits;
        private TextBox txMana;
        private TextBox txMount;
        private TextBox txProp;
        private TextBox txResist;
        private TextBox txStam;
        private TextBox txValue;

        public event EventHandler Changed;

        public PropertiesEditor(NPCProps props)
        {
            this.InitializeComponent();
            this.m_Props = props;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            this.m_Props.Properties.Add(this.txProp.Text);
            this.m_Props.Values.Add(this.txValue.Text);
            this.lst.Items.Add(string.Format("{0}={1}", this.txProp.Text, this.txValue.Text));
            this.txProp.Clear();
            this.txValue.Clear();
            this.txProp.Focus();
            this.OnChanged();
        }

        private void bMountHue_Click(object sender, EventArgs e)
        {
            HuePicker picker = new HuePicker();
            picker.Hue = this.m_Props.MountHue;
            picker.ShowDialog();
            this.nMountHue.Value = picker.Hue;
            this.m_Props.MountHue = picker.Hue;
            this.OnChanged();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.lst.SelectedIndex != -1)
            {
                int selectedIndex = this.lst.SelectedIndex;
                this.lst.Items.RemoveAt(selectedIndex);
                this.m_Props.Properties.RemoveAt(selectedIndex);
                this.m_Props.Values.RemoveAt(selectedIndex);
            }
        }

        private void cmbAI_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_Props.AI = this.cmbAI.Text;
            this.OnChanged();
        }

        private void cmbDamage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txDamage.Text = this.m_Props.Damages[this.cmbDamage.SelectedIndex];
        }

        private void cmbFightMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_Props.FightMode = this.cmbFightMode.Text;
            this.OnChanged();
        }

        private void cmbResist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txResist.Text = this.m_Props.Resistances[this.cmbResist.SelectedIndex];
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
            this.cmbAI = new ComboBox();
            this.label2 = new Label();
            this.cmbFightMode = new ComboBox();
            this.label3 = new Label();
            this.nFame = new NumericUpDown();
            this.label4 = new Label();
            this.nKarma = new NumericUpDown();
            this.label5 = new Label();
            this.label6 = new Label();
            this.txDam = new TextBox();
            this.label7 = new Label();
            this.cmbResist = new ComboBox();
            this.txResist = new TextBox();
            this.label8 = new Label();
            this.cmbDamage = new ComboBox();
            this.txDamage = new TextBox();
            this.label9 = new Label();
            this.txMount = new TextBox();
            this.label10 = new Label();
            this.nMountHue = new NumericUpDown();
            this.bMountHue = new Button();
            this.label11 = new Label();
            this.txHits = new TextBox();
            this.txStam = new TextBox();
            this.label12 = new Label();
            this.txMana = new TextBox();
            this.label13 = new Label();
            this.groupBox1 = new GroupBox();
            this.lst = new ListBox();
            this.bAdd = new Button();
            this.txValue = new TextBox();
            this.label16 = new Label();
            this.label14 = new Label();
            this.label15 = new Label();
            this.txProp = new TextBox();
            this.bDel = new Button();
            this.nFame.BeginInit();
            this.nKarma.BeginInit();
            this.nMountHue.BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.Location = new Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x30, 0x10);
            this.label1.TabIndex = 0;
            this.label1.Text = "AI";
            this.cmbAI.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAI.Items.AddRange(new object[] { "Melee", "Animal", "Archer", "Healer", "Vendor", "Mage", "Berserk", "Predator", "Thief" });
            this.cmbAI.Location = new Point(0x44, 0);
            this.cmbAI.Name = "cmbAI";
            this.cmbAI.Size = new Size(0x7c, 0x15);
            this.cmbAI.TabIndex = 1;
            this.cmbAI.SelectedIndexChanged += new EventHandler(this.cmbAI_SelectedIndexChanged);
            this.label2.Location = new Point(0, 0x1c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x40, 0x10);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fight Mode";
            this.cmbFightMode.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFightMode.Items.AddRange(new object[] { "None", "Agressor", "Strongest", "Weakest", "Closest", "Evil" });
            this.cmbFightMode.Location = new Point(0x44, 0x18);
            this.cmbFightMode.Name = "cmbFightMode";
            this.cmbFightMode.Size = new Size(0x7c, 0x15);
            this.cmbFightMode.TabIndex = 3;
            this.cmbFightMode.SelectedIndexChanged += new EventHandler(this.cmbFightMode_SelectedIndexChanged);
            this.label3.Location = new Point(0, 0x34);
            this.label3.Name = "label3";
            this.label3.Size = new Size(40, 0x10);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fame";
            this.nFame.Location = new Point(40, 0x30);
            int[] bits = new int[4];
            bits[0] = 0x186a0;
            this.nFame.Maximum = new decimal(bits);
            this.nFame.Name = "nFame";
            this.nFame.Size = new Size(0x38, 20);
            this.nFame.TabIndex = 5;
            bits = new int[4];
            bits[0] = 1;
            this.nFame.Value = new decimal(bits);
            this.nFame.ValueChanged += new EventHandler(this.nFame_ValueChanged);
            this.label4.Location = new Point(0x60, 0x34);
            this.label4.Name = "label4";
            this.label4.Size = new Size(40, 0x10);
            this.label4.TabIndex = 6;
            this.label4.Text = "Karma";
            this.nKarma.Location = new Point(0x88, 0x30);
            bits = new int[4];
            bits[0] = 0x186a0;
            this.nKarma.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 0x186a0;
            bits[3] = -2147483648;
            this.nKarma.Minimum = new decimal(bits);
            this.nKarma.Name = "nKarma";
            this.nKarma.Size = new Size(0x38, 20);
            this.nKarma.TabIndex = 7;
            this.nKarma.ValueChanged += new EventHandler(this.nKarma_ValueChanged);
            this.label5.Location = new Point(4, 0x44);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0xbc, 0x34);
            this.label5.TabIndex = 8;
            this.label5.Text = "Set the following properties by setting a single value, or by specifying a range by separating the two values with a ',': 100,200.";
            this.label6.Location = new Point(4, 0x7c);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x40, 0x10);
            this.label6.TabIndex = 9;
            this.label6.Text = "Damage";
            this.txDam.Location = new Point(0x60, 120);
            this.txDam.Name = "txDam";
            this.txDam.Size = new Size(0x60, 20);
            this.txDam.TabIndex = 10;
            this.txDam.Text = "";
            this.txDam.TextChanged += new EventHandler(this.txDam_TextChanged);
            this.label7.Location = new Point(4, 0x90);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x4c, 0x10);
            this.label7.TabIndex = 11;
            this.label7.Text = "Resistances:";
            this.cmbResist.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbResist.Items.AddRange(new object[] { "Cold", "Energy", "Fire", "Physical", "Poison" });
            this.cmbResist.Location = new Point(4, 0xa4);
            this.cmbResist.Name = "cmbResist";
            this.cmbResist.Size = new Size(0x58, 0x15);
            this.cmbResist.TabIndex = 12;
            this.cmbResist.SelectedIndexChanged += new EventHandler(this.cmbResist_SelectedIndexChanged);
            this.txResist.Location = new Point(0x60, 0xa4);
            this.txResist.Name = "txResist";
            this.txResist.Size = new Size(0x60, 20);
            this.txResist.TabIndex = 13;
            this.txResist.Text = "";
            this.txResist.TextChanged += new EventHandler(this.txResist_TextChanged);
            this.label8.Location = new Point(4, 0xc0);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x60, 0x10);
            this.label8.TabIndex = 14;
            this.label8.Text = "Damage Types:";
            this.cmbDamage.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDamage.Items.AddRange(new object[] { "Cold", "Energy", "Fire", "Physical", "Poison" });
            this.cmbDamage.Location = new Point(4, 0xd4);
            this.cmbDamage.Name = "cmbDamage";
            this.cmbDamage.Size = new Size(0x58, 0x15);
            this.cmbDamage.TabIndex = 15;
            this.cmbDamage.SelectedIndexChanged += new EventHandler(this.cmbDamage_SelectedIndexChanged);
            this.txDamage.Location = new Point(0x60, 0xd4);
            this.txDamage.Name = "txDamage";
            this.txDamage.Size = new Size(0x60, 20);
            this.txDamage.TabIndex = 0x10;
            this.txDamage.Text = "";
            this.txDamage.TextChanged += new EventHandler(this.txDamage_TextChanged);
            this.label9.Location = new Point(0xc4, 0x4c);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x40, 0x10);
            this.label9.TabIndex = 0x11;
            this.label9.Text = "Mount";
            this.txMount.Location = new Point(0x108, 0x48);
            this.txMount.Name = "txMount";
            this.txMount.Size = new Size(120, 20);
            this.txMount.TabIndex = 0x12;
            this.txMount.Text = "";
            this.txMount.TextChanged += new EventHandler(this.txMount_TextChanged);
            this.label10.Location = new Point(0xc4, 100);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x40, 0x10);
            this.label10.TabIndex = 0x13;
            this.label10.Text = "Mount Hue";
            this.nMountHue.Location = new Point(0x108, 0x60);
            bits = new int[4];
            bits[0] = 0xbb8;
            this.nMountHue.Maximum = new decimal(bits);
            this.nMountHue.Name = "nMountHue";
            this.nMountHue.Size = new Size(0x38, 20);
            this.nMountHue.TabIndex = 20;
            bits = new int[4];
            bits[0] = 1;
            this.nMountHue.Value = new decimal(bits);
            this.nMountHue.ValueChanged += new EventHandler(this.nMountHue_ValueChanged);
            this.bMountHue.Location = new Point(0x144, 0x60);
            this.bMountHue.Name = "bMountHue";
            this.bMountHue.Size = new Size(60, 20);
            this.bMountHue.TabIndex = 0x15;
            this.bMountHue.Text = "Pick";
            this.bMountHue.Click += new EventHandler(this.bMountHue_Click);
            this.label11.Location = new Point(0xc4, 4);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x40, 0x10);
            this.label11.TabIndex = 0x16;
            this.label11.Text = "Hits";
            this.txHits.Location = new Point(0x108, 0);
            this.txHits.Name = "txHits";
            this.txHits.Size = new Size(120, 20);
            this.txHits.TabIndex = 0x17;
            this.txHits.Text = "";
            this.txHits.TextChanged += new EventHandler(this.txHits_TextChanged);
            this.txStam.Location = new Point(0x108, 0x18);
            this.txStam.Name = "txStam";
            this.txStam.Size = new Size(120, 20);
            this.txStam.TabIndex = 0x19;
            this.txStam.Text = "";
            this.txStam.TextChanged += new EventHandler(this.txStam_TextChanged);
            this.label12.Location = new Point(0xc4, 0x1c);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x40, 0x10);
            this.label12.TabIndex = 0x18;
            this.label12.Text = "Stamina";
            this.txMana.Location = new Point(0x108, 0x30);
            this.txMana.Name = "txMana";
            this.txMana.Size = new Size(120, 20);
            this.txMana.TabIndex = 0x1b;
            this.txMana.Text = "";
            this.txMana.TextChanged += new EventHandler(this.txMana_TextChanged);
            this.label13.Location = new Point(0xc4, 0x34);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x40, 0x10);
            this.label13.TabIndex = 0x1a;
            this.label13.Text = "Mana";
            this.groupBox1.Controls.Add(this.lst);
            this.groupBox1.Controls.Add(this.bAdd);
            this.groupBox1.Controls.Add(this.txValue);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txProp);
            this.groupBox1.Controls.Add(this.bDel);
            this.groupBox1.FlatStyle = FlatStyle.System;
            this.groupBox1.Location = new Point(0x184, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xcc, 0xe8);
            this.groupBox1.TabIndex = 0x1c;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Other Properties";
            this.lst.Location = new Point(4, 0x84);
            this.lst.Name = "lst";
            this.lst.Size = new Size(0xc4, 0x5f);
            this.lst.TabIndex = 0x21;
            this.lst.SelectedIndexChanged += new EventHandler(this.lst_SelectedIndexChanged);
            this.bAdd.Location = new Point(0x4c, 0x6c);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new Size(0x44, 20);
            this.bAdd.TabIndex = 0x20;
            this.bAdd.Text = "Add";
            this.bAdd.Click += new EventHandler(this.bAdd_Click);
            this.txValue.Location = new Point(0x4c, 0x54);
            this.txValue.Name = "txValue";
            this.txValue.Size = new Size(0x7c, 20);
            this.txValue.TabIndex = 0x1f;
            this.txValue.Text = "";
            this.label16.Location = new Point(8, 0x58);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x40, 0x10);
            this.label16.TabIndex = 30;
            this.label16.Text = "Value";
            this.label14.Location = new Point(4, 0x10);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0xc4, 0x2c);
            this.label14.TabIndex = 0x1d;
            this.label14.Text = "Add other properties and values. The value must be parsable, as if used with the [set command.";
            this.label15.Location = new Point(8, 0x40);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x40, 0x10);
            this.label15.TabIndex = 0x1d;
            this.label15.Text = "Property";
            this.txProp.Location = new Point(0x4c, 60);
            this.txProp.Name = "txProp";
            this.txProp.Size = new Size(0x7c, 20);
            this.txProp.TabIndex = 0x1d;
            this.txProp.Text = "";
            this.txProp.TextChanged += new EventHandler(this.txProp_TextChanged);
            this.bDel.Location = new Point(4, 0x6c);
            this.bDel.Name = "bDel";
            this.bDel.Size = new Size(0x44, 20);
            this.bDel.TabIndex = 0x1d;
            this.bDel.Text = "Delete";
            this.bDel.Click += new EventHandler(this.button1_Click);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txMana);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.txStam);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.txHits);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.bMountHue);
            base.Controls.Add(this.nMountHue);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.txMount);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.txDamage);
            base.Controls.Add(this.cmbDamage);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.txResist);
            base.Controls.Add(this.cmbResist);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.txDam);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.nKarma);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.nFame);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cmbFightMode);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cmbAI);
            base.Controls.Add(this.label1);
            base.Name = "PropertiesEditor";
            base.Size = new Size(0x254, 0xec);
            base.Load += new EventHandler(this.PropertiesEditor_Load);
            this.nFame.EndInit();
            this.nKarma.EndInit();
            this.nMountHue.EndInit();
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bDel.Enabled = this.lst.SelectedIndex != -1;
        }

        private void nFame_ValueChanged(object sender, EventArgs e)
        {
            this.m_Props.Fame = (int) this.nFame.Value;
            this.OnChanged();
        }

        private void nKarma_ValueChanged(object sender, EventArgs e)
        {
            this.m_Props.Karma = (int) this.nKarma.Value;
            this.OnChanged();
        }

        private void nMountHue_ValueChanged(object sender, EventArgs e)
        {
            this.m_Props.MountHue = (int) this.nMountHue.Value;
            this.OnChanged();
        }

        protected virtual void OnChanged()
        {
            if (this.Changed != null)
            {
                this.Changed(this, EventArgs.Empty);
            }
        }

        private void PropertiesEditor_Load(object sender, EventArgs e)
        {
            this.cmbAI.SelectedItem = this.m_Props.AI;
            this.cmbFightMode.SelectedItem = this.m_Props.FightMode;
            this.nFame.Value = this.m_Props.Fame;
            this.nKarma.Value = this.m_Props.Karma;
            this.txDam.Text = this.m_Props.Damage;
            this.cmbResist.SelectedIndex = 0;
            this.txResist.Text = this.m_Props.Resistances[0];
            this.cmbDamage.SelectedIndex = 0;
            this.txDamage.Text = this.m_Props.Damages[0];
            this.txHits.Text = this.m_Props.Hits;
            this.txStam.Text = this.m_Props.Stam;
            this.txMana.Text = this.m_Props.Mana;
            this.txMount.Text = this.m_Props.Mount;
            this.nMountHue.Value = this.m_Props.MountHue;
            for (int i = 0; i < this.m_Props.Properties.Count; i++)
            {
                string str = this.m_Props.Properties[i] as string;
                string str2 = this.m_Props.Values[i] as string;
                this.lst.Items.Add(string.Format("{0}={1}", str, str2));
            }
            this.bDel.Enabled = false;
        }

        private void txDam_TextChanged(object sender, EventArgs e)
        {
            this.m_Props.Damage = this.txDam.Text;
            this.OnChanged();
        }

        private void txDamage_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.cmbDamage.SelectedIndex;
            this.m_Props.Damages[selectedIndex] = this.txDamage.Text;
        }

        private void txHits_TextChanged(object sender, EventArgs e)
        {
            this.m_Props.Hits = this.txHits.Text;
            this.OnChanged();
        }

        private void txMana_TextChanged(object sender, EventArgs e)
        {
            this.m_Props.Mana = this.txMana.Text;
            this.OnChanged();
        }

        private void txMount_TextChanged(object sender, EventArgs e)
        {
            this.m_Props.Mount = this.txMount.Text;
            this.OnChanged();
        }

        private void txProp_TextChanged(object sender, EventArgs e)
        {
            this.bAdd.Enabled = (this.txProp.Text.Length > 0) && (this.txProp.Text.IndexOf(" ") == -1);
        }

        private void txResist_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.cmbResist.SelectedIndex;
            this.m_Props.Resistances[selectedIndex] = this.txResist.Text;
            this.OnChanged();
        }

        private void txStam_TextChanged(object sender, EventArgs e)
        {
            this.m_Props.Stam = this.txStam.Text;
            this.OnChanged();
        }

        public void UpdateValues()
        {
            this.m_Props.Fame = (int) this.nFame.Value;
            this.m_Props.Karma = (int) this.nKarma.Value;
            this.m_Props.MountHue = (int) this.nMountHue.Value;
        }
    }
}

