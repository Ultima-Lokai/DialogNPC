namespace Arya.DialogEditor
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class OutfitEditor : UserControl
    {
        private Button bAdd;
        private Button bBeardHue;
        private Button bDelete;
        private Button bHairHue;
        private Button bHue;
        private Button bMobHue;
        private Button bMustacheHue;
        private CheckBox chkBlessed;
        private CheckBox chkCreature;
        private CheckBox chkCustomOutfit;
        private CheckBox chkMustache;
        private CheckBox chkOutfitFunction;
        private ComboBox cmbBeard;
        private ComboBox cmbGender;
        private ComboBox cmbHair;
        private ComboBox cmbLoot;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ListBox lst;
        private ListBox lstSkills;
        private Outfit m_Outfit;
        private int m_SkillIndex = -1;
        private static string[] m_SkillNames = new string[] { 
            "Alchemy", "Anatomy", "AnimalLore", "ItemID", "Arms Lore", "Parry", "Begging", "Blacksmith", "Fletching", "Peacemaking", "Camping", "Carpentry", "Cartography", "Cooking", "Detect Hidden", "Discordance", 
            "Eval Int", "Healing", "Fishing", "Forensics", "Herding", "Hiding", "Provocation", "Inscribe", "Lockpicking", "Magery", "Magic Resist", "Tactics", "Snooping", "Musicianship", "Poisoning", "Archery", 
            "Spirit Speak", "Stealing", "Tailoring", "Animal Taming", "Taste ID", "Tinkering", "Tracking", "Veterinary", "Swords", "Macing", "Fencing", "Wrestling", "Lumberjacking", "Mining", "Meditation", "Stealth", 
            "RemoveTrap", "Necromancy", "Focus", "Chivalry"
         };
        private NumericUpDown nAmount;
        private NumericUpDown nBeardHue;
        private NumericUpDown nBody;
        private NumericUpDown nDex;
        private NumericUpDown nHairHue;
        private NumericUpDown nHue;
        private NumericUpDown nInt;
        private NumericUpDown nMobHue;
        private NumericUpDown nMustacheHue;
        private NumericUpDown nSkill;
        private NumericUpDown nStr;
        private TextBox txName;
        private TextBox txOutfitFunction;
        private TextBox txOutfitType;
        private TextBox txTitle;
        private TextBox txType;

        public event EventHandler Changed;

        public OutfitEditor(Outfit outfit)
        {
            this.InitializeComponent();
            this.m_Outfit = outfit;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            this.m_Outfit.AddItem(this.txType.Text, (int) this.nHue.Value, (int) this.nAmount.Value, this.cmbLoot.Text);
            this.RefreshItems();
            this.txType.Text = string.Empty;
            this.txType.Focus();
            this.OnChanged();
        }

        private void bBeardHue_Click(object sender, EventArgs e)
        {
            HuePicker picker = new HuePicker();
            picker.Hue = this.m_Outfit.BeardHue;
            picker.ShowDialog();
            this.nBeardHue.Value = picker.Hue;
            this.OnChanged();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            ItemEntry selectedItem = this.lst.SelectedItem as ItemEntry;
            this.m_Outfit.Items.Remove(selectedItem);
            this.RefreshItems();
            this.OnChanged();
        }

        private void bHairHue_Click(object sender, EventArgs e)
        {
            HuePicker picker = new HuePicker();
            picker.Hue = this.m_Outfit.HairHue;
            picker.ShowDialog();
            this.nHairHue.Value = picker.Hue;
            this.OnChanged();
        }

        private void bHue_Click(object sender, EventArgs e)
        {
            HuePicker picker = new HuePicker();
            picker.Hue = (int) this.nHue.Value;
            picker.ShowDialog();
            this.nHue.Value = picker.Hue;
        }

        private void bMobHue_Click(object sender, EventArgs e)
        {
            HuePicker picker = new HuePicker();
            picker.Hue = (int) this.nMobHue.Value;
            picker.ShowDialog();
            this.nHue.Value = picker.Hue;
        }

        private void bMustacheHue_Click(object sender, EventArgs e)
        {
            HuePicker picker = new HuePicker();
            picker.Hue = this.m_Outfit.MustacheHue;
            picker.ShowDialog();
            this.nMustacheHue.Value = picker.Hue;
            this.OnChanged();
        }

        private void CalculateSkillIndex()
        {
            if (this.lstSkills.SelectedIndex == -1)
            {
                this.m_SkillIndex = -1;
            }
            else
            {
                string selectedItem = this.lstSkills.SelectedItem as string;
                int num = -1;
                for (int i = 0; i < 0x34; i++)
                {
                    if (m_SkillNames[i] == selectedItem)
                    {
                        num = i;
                        break;
                    }
                }
                if (num == -1)
                {
                    this.m_SkillIndex = -1;
                }
                else
                {
                    this.m_SkillIndex = num;
                }
            }
        }

        private void cheCustomOutfit_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Outfit.CustomOutfit = this.chkCustomOutfit.Checked;
            this.OnChanged();
        }

        private void chkBlessed_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Blessed = this.chkBlessed.Checked;
            this.OnChanged();
        }

        private void chkCreature_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Creature = this.chkCreature.Checked;
            this.OnChanged();
        }

        private void chkMustache_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Mustache = this.chkMustache.Checked;
            this.OnChanged();
        }

        private void cmbBeard_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Beard = this.cmbBeard.Text;
            this.OnChanged();
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Female = this.cmbGender.SelectedIndex == 0;
            this.OnChanged();
        }

        private void cmbHair_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Hair = this.cmbHair.Text;
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

        private void InitializeComponent()
        {
            this.chkCustomOutfit = new CheckBox();
            this.txName = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txTitle = new TextBox();
            this.cmbGender = new ComboBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.cmbHair = new ComboBox();
            this.label5 = new Label();
            this.nHairHue = new NumericUpDown();
            this.bHairHue = new Button();
            this.label6 = new Label();
            this.cmbBeard = new ComboBox();
            this.label7 = new Label();
            this.nBeardHue = new NumericUpDown();
            this.bBeardHue = new Button();
            this.chkMustache = new CheckBox();
            this.nMustacheHue = new NumericUpDown();
            this.bMustacheHue = new Button();
            this.groupBox1 = new GroupBox();
            this.bAdd = new Button();
            this.bDelete = new Button();
            this.lst = new ListBox();
            this.label12 = new Label();
            this.nAmount = new NumericUpDown();
            this.label11 = new Label();
            this.label10 = new Label();
            this.txType = new TextBox();
            this.label9 = new Label();
            this.nHue = new NumericUpDown();
            this.bHue = new Button();
            this.cmbLoot = new ComboBox();
            this.chkBlessed = new CheckBox();
            this.chkCreature = new CheckBox();
            this.nBody = new NumericUpDown();
            this.label8 = new Label();
            this.label13 = new Label();
            this.nMobHue = new NumericUpDown();
            this.bMobHue = new Button();
            this.label14 = new Label();
            this.label15 = new Label();
            this.label16 = new Label();
            this.nStr = new NumericUpDown();
            this.nDex = new NumericUpDown();
            this.nInt = new NumericUpDown();
            this.groupBox2 = new GroupBox();
            this.nSkill = new NumericUpDown();
            this.label17 = new Label();
            this.lstSkills = new ListBox();
            this.chkOutfitFunction = new CheckBox();
            this.label18 = new Label();
            this.label19 = new Label();
            this.txOutfitType = new TextBox();
            this.txOutfitFunction = new TextBox();
            this.nHairHue.BeginInit();
            this.nBeardHue.BeginInit();
            this.nMustacheHue.BeginInit();
            this.groupBox1.SuspendLayout();
            this.nAmount.BeginInit();
            this.nHue.BeginInit();
            this.nBody.BeginInit();
            this.nMobHue.BeginInit();
            this.nStr.BeginInit();
            this.nDex.BeginInit();
            this.nInt.BeginInit();
            this.groupBox2.SuspendLayout();
            this.nSkill.BeginInit();
            base.SuspendLayout();
            this.chkCustomOutfit.FlatStyle = FlatStyle.System;
            this.chkCustomOutfit.Location = new Point(4, 0);
            this.chkCustomOutfit.Name = "chkCustomOutfit";
            this.chkCustomOutfit.Size = new Size(0x58, 20);
            this.chkCustomOutfit.TabIndex = 0;
            this.chkCustomOutfit.Text = "Custom Outfit";
            this.chkCustomOutfit.CheckedChanged += new EventHandler(this.cheCustomOutfit_CheckedChanged);
            this.txName.Location = new Point(0x40, 60);
            this.txName.Name = "txName";
            this.txName.Size = new Size(0x74, 20);
            this.txName.TabIndex = 1;
            this.txName.Text = "";
            this.txName.TextChanged += new EventHandler(this.txName_TextChanged);
            this.label1.Location = new Point(4, 0x40);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x24, 0x10);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            this.label2.Location = new Point(4, 0x54);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x24, 0x10);
            this.label2.TabIndex = 3;
            this.label2.Text = "Title";
            this.txTitle.Location = new Point(0x40, 80);
            this.txTitle.Name = "txTitle";
            this.txTitle.Size = new Size(0x74, 20);
            this.txTitle.TabIndex = 4;
            this.txTitle.Text = "";
            this.txTitle.TextChanged += new EventHandler(this.txTitle_TextChanged);
            this.cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbGender.Items.AddRange(new object[] { "Female", "Male" });
            this.cmbGender.Location = new Point(0x40, 100);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new Size(0x74, 0x15);
            this.cmbGender.TabIndex = 5;
            this.cmbGender.SelectedIndexChanged += new EventHandler(this.cmbGender_SelectedIndexChanged);
            this.label3.Location = new Point(4, 0x68);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x30, 0x10);
            this.label3.TabIndex = 6;
            this.label3.Text = "Gender";
            this.label4.Location = new Point(4, 0x80);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x24, 0x10);
            this.label4.TabIndex = 7;
            this.label4.Text = "Hair";
            this.cmbHair.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbHair.Items.AddRange(new object[] { "None", "Afro", "Buns", "Krisna", "Long", "Mohawk", "Pageboy", "Pony Tail", "Receeding", "Short", "Two Pig Tails" });
            this.cmbHair.Location = new Point(0x40, 0x7c);
            this.cmbHair.Name = "cmbHair";
            this.cmbHair.Size = new Size(120, 0x15);
            this.cmbHair.TabIndex = 8;
            this.cmbHair.SelectedIndexChanged += new EventHandler(this.cmbHair_SelectedIndexChanged);
            this.label5.Location = new Point(4, 0x98);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x38, 0x10);
            this.label5.TabIndex = 9;
            this.label5.Text = "Hair Hue";
            this.nHairHue.Location = new Point(0x40, 0x90);
            int[] bits = new int[4];
            bits[0] = 0xbb8;
            this.nHairHue.Maximum = new decimal(bits);
            this.nHairHue.Name = "nHairHue";
            this.nHairHue.Size = new Size(0x30, 20);
            this.nHairHue.TabIndex = 10;
            this.nHairHue.ValueChanged += new EventHandler(this.nHairHue_ValueChanged);
            this.bHairHue.FlatStyle = FlatStyle.System;
            this.bHairHue.Location = new Point(0x74, 0x90);
            this.bHairHue.Name = "bHairHue";
            this.bHairHue.Size = new Size(0x44, 20);
            this.bHairHue.TabIndex = 11;
            this.bHairHue.Text = "Pick";
            this.bHairHue.Click += new EventHandler(this.bHairHue_Click);
            this.label6.Location = new Point(4, 0xac);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x24, 0x10);
            this.label6.TabIndex = 12;
            this.label6.Text = "Beard";
            this.cmbBeard.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbBeard.Items.AddRange(new object[] { "None", "Goatee", "Long", "Medium Long", "Medium Short", "Short" });
            this.cmbBeard.Location = new Point(0x40, 0xa8);
            this.cmbBeard.Name = "cmbBeard";
            this.cmbBeard.Size = new Size(120, 0x15);
            this.cmbBeard.TabIndex = 13;
            this.cmbBeard.SelectedIndexChanged += new EventHandler(this.cmbBeard_SelectedIndexChanged);
            this.label7.Location = new Point(4, 0xc0);
            this.label7.Name = "label7";
            this.label7.Size = new Size(60, 0x10);
            this.label7.TabIndex = 14;
            this.label7.Text = "Beard Hue";
            this.nBeardHue.Location = new Point(0x40, 0xbc);
            bits = new int[4];
            bits[0] = 0xbb8;
            this.nBeardHue.Maximum = new decimal(bits);
            this.nBeardHue.Name = "nBeardHue";
            this.nBeardHue.Size = new Size(0x30, 20);
            this.nBeardHue.TabIndex = 15;
            this.nBeardHue.ValueChanged += new EventHandler(this.nBeardHue_ValueChanged);
            this.bBeardHue.FlatStyle = FlatStyle.System;
            this.bBeardHue.Location = new Point(0x74, 0xbc);
            this.bBeardHue.Name = "bBeardHue";
            this.bBeardHue.Size = new Size(0x44, 20);
            this.bBeardHue.TabIndex = 0x10;
            this.bBeardHue.Text = "Pick";
            this.bBeardHue.Click += new EventHandler(this.bBeardHue_Click);
            this.chkMustache.FlatStyle = FlatStyle.System;
            this.chkMustache.Location = new Point(4, 0xd4);
            this.chkMustache.Name = "chkMustache";
            this.chkMustache.Size = new Size(0x48, 20);
            this.chkMustache.TabIndex = 0x11;
            this.chkMustache.Text = "Mustache";
            this.chkMustache.CheckedChanged += new EventHandler(this.chkMustache_CheckedChanged);
            this.nMustacheHue.Location = new Point(80, 0xd4);
            bits = new int[4];
            bits[0] = 0xbb8;
            this.nMustacheHue.Maximum = new decimal(bits);
            this.nMustacheHue.Name = "nMustacheHue";
            this.nMustacheHue.Size = new Size(0x30, 20);
            this.nMustacheHue.TabIndex = 0x12;
            this.nMustacheHue.ValueChanged += new EventHandler(this.nMustacheHue_ValueChanged);
            this.bMustacheHue.FlatStyle = FlatStyle.System;
            this.bMustacheHue.Location = new Point(0x84, 0xd4);
            this.bMustacheHue.Name = "bMustacheHue";
            this.bMustacheHue.Size = new Size(0x34, 20);
            this.bMustacheHue.TabIndex = 0x13;
            this.bMustacheHue.Text = "Pick";
            this.bMustacheHue.Click += new EventHandler(this.bMustacheHue_Click);
            this.groupBox1.Controls.Add(this.bAdd);
            this.groupBox1.Controls.Add(this.bDelete);
            this.groupBox1.Controls.Add(this.lst);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.nAmount);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txType);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.nHue);
            this.groupBox1.Controls.Add(this.bHue);
            this.groupBox1.FlatStyle = FlatStyle.System;
            this.groupBox1.Location = new Point(0xbc, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xb8, 0xe8);
            this.groupBox1.TabIndex = 0x15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items List";
            this.bAdd.Enabled = false;
            this.bAdd.FlatStyle = FlatStyle.System;
            this.bAdd.Location = new Point(0x6c, 0x70);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new Size(0x44, 0x18);
            this.bAdd.TabIndex = 30;
            this.bAdd.Text = "Add";
            this.bAdd.Click += new EventHandler(this.bAdd_Click);
            this.bDelete.Enabled = false;
            this.bDelete.FlatStyle = FlatStyle.System;
            this.bDelete.Location = new Point(4, 0x70);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new Size(0x44, 0x18);
            this.bDelete.TabIndex = 0x1d;
            this.bDelete.Text = "Delete";
            this.bDelete.Click += new EventHandler(this.bDelete_Click);
            this.lst.Location = new Point(4, 140);
            this.lst.Name = "lst";
            this.lst.Size = new Size(0xac, 0x52);
            this.lst.TabIndex = 0x1c;
            this.lst.SelectedIndexChanged += new EventHandler(this.lst_SelectedIndexChanged);
            this.label12.Location = new Point(4, 0x5c);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x30, 0x10);
            this.label12.TabIndex = 0x1b;
            this.label12.Text = "Loot";
            this.nAmount.Location = new Point(0x38, 0x40);
            bits = new int[4];
            bits[0] = 0xea60;
            this.nAmount.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.nAmount.Minimum = new decimal(bits);
            this.nAmount.Name = "nAmount";
            this.nAmount.TabIndex = 0x1a;
            bits = new int[4];
            bits[0] = 1;
            this.nAmount.Value = new decimal(bits);
            this.label11.Location = new Point(4, 0x40);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x30, 0x10);
            this.label11.TabIndex = 0x19;
            this.label11.Text = "Amount";
            this.label10.Location = new Point(4, 0x2c);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x30, 0x10);
            this.label10.TabIndex = 0x18;
            this.label10.Text = "Hue";
            this.txType.Location = new Point(0x38, 0x10);
            this.txType.Name = "txType";
            this.txType.Size = new Size(120, 20);
            this.txType.TabIndex = 0x17;
            this.txType.Text = "";
            this.txType.TextChanged += new EventHandler(this.txType_TextChanged);
            this.label9.Location = new Point(4, 20);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x30, 0x10);
            this.label9.TabIndex = 0x16;
            this.label9.Text = "Type";
            this.nHue.Location = new Point(0x38, 40);
            bits = new int[4];
            bits[0] = 0xbb8;
            this.nHue.Maximum = new decimal(bits);
            this.nHue.Name = "nHue";
            this.nHue.Size = new Size(0x30, 20);
            this.nHue.TabIndex = 0x16;
            this.bHue.FlatStyle = FlatStyle.System;
            this.bHue.Location = new Point(0x6c, 40);
            this.bHue.Name = "bHue";
            this.bHue.Size = new Size(0x44, 20);
            this.bHue.TabIndex = 0x16;
            this.bHue.Text = "Pick";
            this.bHue.Click += new EventHandler(this.bHue_Click);
            this.cmbLoot.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbLoot.Items.AddRange(new object[] { "Regular", "Newbied", "Blessed", "Cursed" });
            this.cmbLoot.Location = new Point(0xf4, 0x58);
            this.cmbLoot.Name = "cmbLoot";
            this.cmbLoot.Size = new Size(120, 0x15);
            this.cmbLoot.TabIndex = 0x16;
            this.chkBlessed.FlatStyle = FlatStyle.System;
            this.chkBlessed.Location = new Point(0x5c, 0);
            this.chkBlessed.Name = "chkBlessed";
            this.chkBlessed.Size = new Size(0x58, 20);
            this.chkBlessed.TabIndex = 0x17;
            this.chkBlessed.Text = "Blessed";
            this.chkBlessed.CheckedChanged += new EventHandler(this.chkBlessed_CheckedChanged);
            this.chkCreature.FlatStyle = FlatStyle.System;
            this.chkCreature.Location = new Point(4, 20);
            this.chkCreature.Name = "chkCreature";
            this.chkCreature.Size = new Size(0x58, 20);
            this.chkCreature.TabIndex = 0x18;
            this.chkCreature.Text = "Non Human";
            this.chkCreature.CheckedChanged += new EventHandler(this.chkCreature_CheckedChanged);
            this.nBody.Location = new Point(0x80, 20);
            bits = new int[4];
            bits[0] = 0x4e20;
            this.nBody.Maximum = new decimal(bits);
            this.nBody.Name = "nBody";
            this.nBody.Size = new Size(0x34, 20);
            this.nBody.TabIndex = 0x19;
            this.nBody.ValueChanged += new EventHandler(this.nBody_ValueChanged);
            this.label8.Location = new Point(0x5c, 0x18);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x24, 0x10);
            this.label8.TabIndex = 0x1a;
            this.label8.Text = "Body";
            this.label13.Location = new Point(4, 40);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x30, 0x10);
            this.label13.TabIndex = 0x1b;
            this.label13.Text = "Hue";
            this.nMobHue.Location = new Point(0x40, 40);
            bits = new int[4];
            bits[0] = 0xbb8;
            this.nMobHue.Maximum = new decimal(bits);
            this.nMobHue.Name = "nMobHue";
            this.nMobHue.Size = new Size(0x30, 20);
            this.nMobHue.TabIndex = 0x1d;
            this.nMobHue.ValueChanged += new EventHandler(this.nMobHue_ValueChanged);
            this.bMobHue.FlatStyle = FlatStyle.System;
            this.bMobHue.Location = new Point(0x74, 40);
            this.bMobHue.Name = "bMobHue";
            this.bMobHue.Size = new Size(0x40, 20);
            this.bMobHue.TabIndex = 0x1c;
            this.bMobHue.Text = "Pick";
            this.bMobHue.Click += new EventHandler(this.bMobHue_Click);
            this.label14.Location = new Point(0x20c, 8);
            this.label14.Name = "label14";
            this.label14.Size = new Size(12, 0x10);
            this.label14.TabIndex = 30;
            this.label14.Text = "I";
            this.label15.Location = new Point(0x1c0, 8);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x10, 0x10);
            this.label15.TabIndex = 0x1f;
            this.label15.Text = "D";
            this.label16.Location = new Point(0x178, 8);
            this.label16.Name = "label16";
            this.label16.Size = new Size(12, 0x10);
            this.label16.TabIndex = 0x20;
            this.label16.Text = "S";
            this.nStr.Location = new Point(0x188, 4);
            bits = new int[4];
            bits[0] = 0xea60;
            this.nStr.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.nStr.Minimum = new decimal(bits);
            this.nStr.Name = "nStr";
            this.nStr.Size = new Size(0x34, 20);
            this.nStr.TabIndex = 0x21;
            bits = new int[4];
            bits[0] = 1;
            this.nStr.Value = new decimal(bits);
            this.nDex.Location = new Point(0x1d4, 4);
            bits = new int[4];
            bits[0] = 0xea60;
            this.nDex.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.nDex.Minimum = new decimal(bits);
            this.nDex.Name = "nDex";
            this.nDex.Size = new Size(0x34, 20);
            this.nDex.TabIndex = 0x22;
            bits = new int[4];
            bits[0] = 1;
            this.nDex.Value = new decimal(bits);
            this.nInt.Location = new Point(540, 4);
            bits = new int[4];
            bits[0] = 0xea60;
            this.nInt.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.nInt.Minimum = new decimal(bits);
            this.nInt.Name = "nInt";
            this.nInt.Size = new Size(0x34, 20);
            this.nInt.TabIndex = 0x23;
            bits = new int[4];
            bits[0] = 1;
            this.nInt.Value = new decimal(bits);
            this.groupBox2.Controls.Add(this.nSkill);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.lstSkills);
            this.groupBox2.FlatStyle = FlatStyle.System;
            this.groupBox2.Location = new Point(0x178, 0x18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xd8, 0x90);
            this.groupBox2.TabIndex = 0x24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Skills";
            this.nSkill.Location = new Point(0x88, 0x20);
            bits = new int[4];
            bits[0] = 0xea60;
            this.nSkill.Maximum = new decimal(bits);
            this.nSkill.Name = "nSkill";
            this.nSkill.Size = new Size(0x48, 20);
            this.nSkill.TabIndex = 2;
            this.nSkill.ValueChanged += new EventHandler(this.nSkill_ValueChanged);
            this.label17.Location = new Point(0x88, 0x10);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x48, 0x10);
            this.label17.TabIndex = 1;
            this.label17.Text = "Value";
            this.lstSkills.Location = new Point(8, 0x10);
            this.lstSkills.Name = "lstSkills";
            this.lstSkills.Size = new Size(120, 0x79);
            this.lstSkills.Sorted = true;
            this.lstSkills.TabIndex = 0;
            this.lstSkills.SelectedIndexChanged += new EventHandler(this.lstSkills_SelectedIndexChanged);
            this.chkOutfitFunction.Location = new Point(0x178, 0xa8);
            this.chkOutfitFunction.Name = "chkOutfitFunction";
            this.chkOutfitFunction.Size = new Size(140, 20);
            this.chkOutfitFunction.TabIndex = 0x25;
            this.chkOutfitFunction.Text = "Custom Outfit Function";
            this.label18.Location = new Point(0x178, 0xc0);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x38, 0x10);
            this.label18.TabIndex = 3;
            this.label18.Text = "Type";
            this.label19.Location = new Point(0x178, 0xd8);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x38, 0x10);
            this.label19.TabIndex = 0x26;
            this.label19.Text = "Function";
            this.txOutfitType.Location = new Point(0x1b0, 0xbc);
            this.txOutfitType.Name = "txOutfitType";
            this.txOutfitType.Size = new Size(160, 20);
            this.txOutfitType.TabIndex = 0x27;
            this.txOutfitType.Text = "";
            this.txOutfitFunction.Location = new Point(0x1b0, 0xd4);
            this.txOutfitFunction.Name = "txOutfitFunction";
            this.txOutfitFunction.Size = new Size(160, 20);
            this.txOutfitFunction.TabIndex = 40;
            this.txOutfitFunction.Text = "";
            base.Controls.Add(this.txOutfitFunction);
            base.Controls.Add(this.txOutfitType);
            base.Controls.Add(this.label19);
            base.Controls.Add(this.chkOutfitFunction);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.nInt);
            base.Controls.Add(this.nDex);
            base.Controls.Add(this.nStr);
            base.Controls.Add(this.label16);
            base.Controls.Add(this.label15);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.nMobHue);
            base.Controls.Add(this.bMobHue);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.nBody);
            base.Controls.Add(this.chkCreature);
            base.Controls.Add(this.chkBlessed);
            base.Controls.Add(this.cmbLoot);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.bMustacheHue);
            base.Controls.Add(this.nMustacheHue);
            base.Controls.Add(this.chkMustache);
            base.Controls.Add(this.bBeardHue);
            base.Controls.Add(this.nBeardHue);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.cmbBeard);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.bHairHue);
            base.Controls.Add(this.nHairHue);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.cmbHair);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cmbGender);
            base.Controls.Add(this.txTitle);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txName);
            base.Controls.Add(this.chkCustomOutfit);
            base.Controls.Add(this.label18);
            base.Name = "OutfitEditor";
            base.Size = new Size(0x254, 0xec);
            base.Load += new EventHandler(this.OutfitEditor_Load);
            this.nHairHue.EndInit();
            this.nBeardHue.EndInit();
            this.nMustacheHue.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.nAmount.EndInit();
            this.nHue.EndInit();
            this.nBody.EndInit();
            this.nMobHue.EndInit();
            this.nStr.EndInit();
            this.nDex.EndInit();
            this.nInt.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.nSkill.EndInit();
            base.ResumeLayout(false);
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bDelete.Enabled = this.lst.SelectedItem != null;
        }

        private void lstSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateSkill();
            this.CalculateSkillIndex();
            this.nSkill.Value = this.m_Outfit.Skills[this.m_SkillIndex];
        }

        private void nBeardHue_ValueChanged(object sender, EventArgs e)
        {
            this.m_Outfit.BeardHue = (int) this.nBeardHue.Value;
            this.OnChanged();
        }

        private void nBody_ValueChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Body = (int) this.nBody.Value;
            this.OnChanged();
        }

        private void nHairHue_ValueChanged(object sender, EventArgs e)
        {
            this.m_Outfit.HairHue = (int) this.nHairHue.Value;
            this.OnChanged();
        }

        private void nMobHue_ValueChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Hue = (int) this.nMobHue.Value;
            this.OnChanged();
        }

        private void nMustacheHue_ValueChanged(object sender, EventArgs e)
        {
            this.m_Outfit.MustacheHue = (int) this.nMustacheHue.Value;
            this.OnChanged();
        }

        private void nSkill_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateSkill();
        }

        protected virtual void OnChanged()
        {
            if (this.Changed != null)
            {
                this.Changed(this, EventArgs.Empty);
            }
        }

        private void OutfitEditor_Load(object sender, EventArgs e)
        {
            this.chkCustomOutfit.Checked = this.m_Outfit.CustomOutfit;
            this.chkBlessed.Checked = this.m_Outfit.Blessed;
            this.txName.Text = this.m_Outfit.Name;
            this.txTitle.Text = this.m_Outfit.Title;
            this.cmbGender.SelectedIndex = this.m_Outfit.Female ? 0 : 1;
            this.cmbHair.SelectedItem = this.m_Outfit.Hair;
            this.nHairHue.Value = this.m_Outfit.HairHue;
            this.cmbBeard.SelectedItem = this.m_Outfit.Beard;
            this.nBeardHue.Value = this.m_Outfit.BeardHue;
            this.chkMustache.Checked = this.m_Outfit.Mustache;
            this.nMustacheHue.Value = this.m_Outfit.MustacheHue;
            this.cmbLoot.SelectedIndex = 0;
            this.nMobHue.Value = this.m_Outfit.Hue;
            this.nStr.Value = this.m_Outfit.Str;
            this.nDex.Value = this.m_Outfit.Dex;
            this.nInt.Value = this.m_Outfit.Int;
            this.chkOutfitFunction.Checked = this.m_Outfit.CustomFunction;
            this.txOutfitType.Text = this.m_Outfit.FunctionType;
            this.txOutfitFunction.Text = this.m_Outfit.Function;
            this.lstSkills.Items.AddRange(m_SkillNames);
            this.RefreshItems();
        }

        private void RefreshItems()
        {
            this.lst.Items.Clear();
            foreach (ItemEntry entry in this.m_Outfit.Items)
            {
                this.lst.Items.Add(entry);
            }
            this.bDelete.Enabled = false;
        }

        private void txName_TextChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Name = this.txName.Text;
            this.OnChanged();
        }

        private void txTitle_TextChanged(object sender, EventArgs e)
        {
            this.m_Outfit.Title = this.txTitle.Text;
            this.OnChanged();
        }

        private void txType_TextChanged(object sender, EventArgs e)
        {
            this.bAdd.Enabled = this.txType.Text.Length > 0;
        }

        private void UpdateSkill()
        {
            if (this.m_SkillIndex != -1)
            {
                this.m_Outfit.Skills[this.m_SkillIndex] = (int) this.nSkill.Value;
            }
        }

        public void UpdateValues()
        {
            this.m_Outfit.HairHue = (int) this.nHairHue.Value;
            this.m_Outfit.Body = (int) this.nBody.Value;
            this.m_Outfit.BeardHue = (int) this.nBeardHue.Value;
            this.m_Outfit.MustacheHue = (int) this.nMustacheHue.Value;
            this.m_Outfit.Hue = (int) this.nMobHue.Value;
            this.UpdateSkill();
        }
    }
}

