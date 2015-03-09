namespace Arya.DialogEditor
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using TheBox.ArtViewer;
    using TheBox.Common;
    using TheBox.Mul;
    using Ultima;

    public class HuePicker : Form
    {
        private TheBox.ArtViewer.ArtViewer art;
        private TrackBar bar;
        private Button bNoHue;
        private Button bSelect;
        private HuesChart chart;
        private Container components = null;
        private int m_Hue;
        private static int m_Index = 0;
        private static bool m_Items = true;
        private RadioButton rItems;
        private RadioButton rMobiles;

        public HuePicker()
        {
            this.InitializeComponent();
        }

        private void bar_Scroll(object sender, EventArgs e)
        {
            m_Index = this.bar.Value;
            this.art.ArtIndex = m_Index;
        }

        private void bNoHue_Click(object sender, EventArgs e)
        {
            this.m_Hue = 0;
            base.Close();
        }

        private void bSelect_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void chart_HueChanged(object sender, EventArgs e)
        {
            this.m_Hue = this.chart.SelectedIndex;
            this.art.Hue = this.m_Hue;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FixBar()
        {
            if (m_Items)
            {
                this.bar.Minimum = 0;
                this.bar.Maximum = 0x3e8;
                if (m_Index > 0x3e8)
                {
                    this.bar.Value = 1;
                }
            }
            else
            {
                this.bar.Minimum = 0;
                this.bar.Maximum = 0x4268;
            }
        }

        private void HuePicker_Load(object sender, EventArgs e)
        {
            TheBox.Mul.Hues hues = TheBox.Mul.Hues.Load(Client.GetFilePath("hues.mul"));
            this.chart.Hues = hues;
            this.rItems.Checked = m_Items;
            this.rMobiles.Checked = !m_Items;
            if (m_Items)
            {
                this.art.Art = TheBox.ArtViewer.Art.Items;
            }
            else
            {
                this.art.Art = TheBox.ArtViewer.Art.NPCs;
            }
            this.art.ArtIndex = m_Index;
            this.art.Hue = this.m_Hue;
            this.FixBar();
            this.chart.SelectedIndex = this.m_Hue;
            this.chart.HueChanged += new EventHandler(this.chart_HueChanged);
        }

        private void InitializeComponent()
        {
            this.chart = new HuesChart();
            this.art = new TheBox.ArtViewer.ArtViewer();
            this.rItems = new RadioButton();
            this.rMobiles = new RadioButton();
            this.bar = new TrackBar();
            this.bNoHue = new Button();
            this.bSelect = new Button();
            this.bar.BeginInit();
            base.SuspendLayout();
            this.chart.ColorTableIndex = 0x1c;
            this.chart.Hues = null;
            this.chart.Location = new Point(8, 8);
            this.chart.Name = "chart";
            this.chart.SelectedIndex = 1;
            this.chart.Size = new Size(0x1c4, 0x12e);
            this.chart.TabIndex = 0;
            this.chart.Text = "huesChart1";
            this.art.Animate = true;
            this.art.Art = TheBox.ArtViewer.Art.Items;
            this.art.ArtIndex = 0;
            this.art.BackColor = Color.White;
            this.art.Hue = 0;
            this.art.Location = new Point(0x1d8, 0x80);
            this.art.Name = "art";
            this.art.ResizeTallItems = true;
            this.art.RoomView = true;
            this.art.ShowHexID = true;
            this.art.ShowID = true;
            this.art.Size = new Size(160, 0xb8);
            this.art.TabIndex = 1;
            this.art.Text = "artViewer1";
            this.rItems.FlatStyle = FlatStyle.System;
            this.rItems.Location = new Point(480, 40);
            this.rItems.Name = "rItems";
            this.rItems.Size = new Size(0x38, 0x18);
            this.rItems.TabIndex = 2;
            this.rItems.Text = "Items";
            this.rMobiles.FlatStyle = FlatStyle.System;
            this.rMobiles.Location = new Point(0x228, 40);
            this.rMobiles.Name = "rMobiles";
            this.rMobiles.Size = new Size(0x48, 0x18);
            this.rMobiles.TabIndex = 3;
            this.rMobiles.Text = "Mobiles";
            this.bar.Location = new Point(0x1d8, 0x48);
            this.bar.Name = "bar";
            this.bar.Size = new Size(160, 0x2d);
            this.bar.TabIndex = 4;
            this.bar.Scroll += new EventHandler(this.bar_Scroll);
            this.bNoHue.FlatStyle = FlatStyle.System;
            this.bNoHue.Location = new Point(560, 8);
            this.bNoHue.Name = "bNoHue";
            this.bNoHue.TabIndex = 5;
            this.bNoHue.Text = "No Hue";
            this.bNoHue.Click += new EventHandler(this.bNoHue_Click);
            this.bSelect.FlatStyle = FlatStyle.System;
            this.bSelect.Location = new Point(0x1d8, 8);
            this.bSelect.Name = "bSelect";
            this.bSelect.TabIndex = 6;
            this.bSelect.Text = "Select";
            this.bSelect.Click += new EventHandler(this.bSelect_Click);
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(640, 0x13e);
            base.Controls.Add(this.bSelect);
            base.Controls.Add(this.bNoHue);
            base.Controls.Add(this.bar);
            base.Controls.Add(this.rMobiles);
            base.Controls.Add(this.rItems);
            base.Controls.Add(this.art);
            base.Controls.Add(this.chart);
            base.FormBorderStyle = FormBorderStyle.Fixed3D;
            base.MaximizeBox = false;
            base.Name = "HuePicker";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "HuePicker";
            base.Load += new EventHandler(this.HuePicker_Load);
            this.bar.EndInit();
            base.ResumeLayout(false);
        }

        public int Hue
        {
            get
            {
                return this.m_Hue;
            }
            set
            {
                this.m_Hue = value;
            }
        }
    }
}

