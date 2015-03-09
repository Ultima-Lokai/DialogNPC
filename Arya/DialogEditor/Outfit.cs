namespace Arya.DialogEditor
{
    using System;
    using System.Collections;
    using System.Xml.Serialization;

    [Serializable, XmlInclude(typeof(ItemEntry))]
    public class Outfit
    {
        private string m_Beard = "None";
        private int m_BeardHue;
        private bool m_Blessed;
        private int m_Body;
        private bool m_Creature;
        private bool m_CustomFunction = false;
        private bool m_CustomOutfit = false;
        private int m_Dex = 1;
        private bool m_Female;
        private string m_Function = string.Empty;
        private string m_FunctionType = string.Empty;
        private string m_Hair = "None";
        private int m_HairHue;
        private int m_Hue;
        private int m_Int = 1;
        private ArrayList m_Items = new ArrayList();
        private bool m_Mustache;
        private int m_MustacheHue;
        private string m_Name = string.Empty;
        private int[] m_Skills = new int[0x34];
        private int m_Str = 1;
        private string m_Title = string.Empty;

        public Outfit()
        {
            this.m_Skills.Initialize();
        }

        public void AddItem(string type, int hue, int amount, string loot)
        {
            ItemEntry entry = new ItemEntry(type, hue, amount, loot);
            this.m_Items.Add(entry);
        }

        [XmlAttribute]
        public string Beard
        {
            get
            {
                return this.m_Beard;
            }
            set
            {
                this.m_Beard = value;
            }
        }

        [XmlAttribute]
        public int BeardHue
        {
            get
            {
                return this.m_BeardHue;
            }
            set
            {
                this.m_BeardHue = value;
            }
        }

        [XmlAttribute]
        public bool Blessed
        {
            get
            {
                return this.m_Blessed;
            }
            set
            {
                this.m_Blessed = value;
            }
        }

        [XmlAttribute]
        public int Body
        {
            get
            {
                return this.m_Body;
            }
            set
            {
                this.m_Body = value;
            }
        }

        [XmlAttribute]
        public bool Creature
        {
            get
            {
                return this.m_Creature;
            }
            set
            {
                this.m_Creature = value;
            }
        }

        [XmlAttribute]
        public bool CustomFunction
        {
            get
            {
                return this.m_CustomFunction;
            }
            set
            {
                this.m_CustomFunction = value;
            }
        }

        [XmlAttribute]
        public bool CustomOutfit
        {
            get
            {
                return this.m_CustomOutfit;
            }
            set
            {
                this.m_CustomOutfit = value;
            }
        }

        [XmlAttribute]
        public int Dex
        {
            get
            {
                return this.m_Dex;
            }
            set
            {
                this.m_Dex = value;
            }
        }

        [XmlAttribute]
        public bool Female
        {
            get
            {
                return this.m_Female;
            }
            set
            {
                this.m_Female = value;
            }
        }

        [XmlAttribute]
        public string Function
        {
            get
            {
                return this.m_Function;
            }
            set
            {
                this.m_Function = value;
            }
        }

        [XmlAttribute]
        public string FunctionType
        {
            get
            {
                return this.m_FunctionType;
            }
            set
            {
                this.m_FunctionType = value;
            }
        }

        [XmlAttribute]
        public string Hair
        {
            get
            {
                return this.m_Hair;
            }
            set
            {
                this.m_Hair = value;
            }
        }

        [XmlAttribute]
        public int HairHue
        {
            get
            {
                return this.m_HairHue;
            }
            set
            {
                this.m_HairHue = value;
            }
        }

        [XmlAttribute]
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

        [XmlAttribute]
        public int Int
        {
            get
            {
                return this.m_Int;
            }
            set
            {
                this.m_Int = value;
            }
        }

        public ArrayList Items
        {
            get
            {
                return this.m_Items;
            }
            set
            {
                this.m_Items = value;
            }
        }

        [XmlAttribute]
        public bool Mustache
        {
            get
            {
                return this.m_Mustache;
            }
            set
            {
                this.m_Mustache = value;
            }
        }

        [XmlAttribute]
        public int MustacheHue
        {
            get
            {
                return this.m_MustacheHue;
            }
            set
            {
                this.m_MustacheHue = value;
            }
        }

        [XmlAttribute]
        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public int[] Skills
        {
            get
            {
                return this.m_Skills;
            }
            set
            {
                this.m_Skills = value;
            }
        }

        [XmlAttribute]
        public int Str
        {
            get
            {
                return this.m_Str;
            }
            set
            {
                this.m_Str = value;
            }
        }

        [XmlAttribute]
        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

