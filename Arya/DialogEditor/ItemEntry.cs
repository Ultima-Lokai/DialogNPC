namespace Arya.DialogEditor
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ItemEntry
    {
        private int m_Amount;
        private int m_Hue;
        private string m_LootType;
        private string m_Type;

        public ItemEntry()
        {
            this.m_Amount = 1;
        }

        public ItemEntry(string type, int hue, int amount, string loot)
        {
            this.m_Amount = 1;
            this.m_Type = type;
            this.m_Hue = hue;
            this.m_Amount = amount;
            this.m_LootType = loot;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2}) {3}", new object[] { this.m_Amount, this.m_Type, this.m_Hue, this.m_LootType });
        }

        [XmlAttribute]
        public int Amount
        {
            get
            {
                return this.m_Amount;
            }
            set
            {
                this.m_Amount = value;
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
        public string LootType
        {
            get
            {
                return this.m_LootType;
            }
            set
            {
                this.m_LootType = value;
            }
        }

        [XmlAttribute]
        public string Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
            }
        }
    }
}

