namespace Arya.DialogEditor
{
    using System;
    using System.Collections;
    using System.Xml.Serialization;

    [Serializable]
    public class NPCProps
    {
        private string m_AI = "Thief";
        private string m_Damage = string.Empty;
        private string[] m_Damages = new string[5];
        private int m_Fame = 0;
        private string m_FightMode = "None";
        private string m_Hits = string.Empty;
        private int m_Karma = 0;
        private string m_Mana = string.Empty;
        private string m_Mount = string.Empty;
        private int m_MountHue = 0;
        private ArrayList m_Properties;
        private string[] m_Resistances = new string[5];
        private string m_Stam = string.Empty;
        private ArrayList m_Values;

        public NPCProps()
        {
            for (int i = 0; i < 5; i++)
            {
                this.m_Damages[i] = string.Empty;
                this.m_Resistances[i] = string.Empty;
            }
            this.m_Properties = new ArrayList();
            this.m_Values = new ArrayList();
        }

        private string GetResistName(int i)
        {
            switch (i)
            {
                case 0:
                    return "Cold";

                case 1:
                    return "Energy";

                case 2:
                    return "Fire";

                case 3:
                    return "Physical";

                case 4:
                    return "Poison";
            }
            return "Unspecified";
        }

        public bool Validate(ref string err)
        {
            if (!this.ValidateStringValue(this.m_Damage, ref err))
            {
                err = string.Format(err, "Damage");
                return false;
            }
            for (int i = 0; i < 5; i++)
            {
                if (!this.ValidateStringValue(this.m_Resistances[i], ref err))
                {
                    err = string.Format(err, string.Format("{0} Resistance", this.GetResistName(i)));
                    return false;
                }
                if (!this.ValidateStringValue(this.m_Damages[i], ref err))
                {
                    err = string.Format(err, string.Format("{0} Damage", this.GetResistName(i)));
                    return false;
                }
            }
            if (!this.ValidateStringValue(this.m_Hits, ref err))
            {
                err = string.Format(err, "Hits");
                return false;
            }
            if (!this.ValidateStringValue(this.m_Stam, ref err))
            {
                err = string.Format(err, "Stam");
                return false;
            }
            if (!this.ValidateStringValue(this.m_Mana, ref err))
            {
                err = string.Format(err, "Mana");
                return false;
            }
            if ((this.m_Mount.Length > 0) && (this.m_Mount.IndexOf(" ") != -1))
            {
                err = "The current configuration contains an error:\n\nThe name of the mount type contains spaces, therefore it's not a valid type name.";
                return false;
            }
            return true;
        }

        private bool ValidateStringValue(string val, ref string err)
        {
            if (val == string.Empty)
            {
                return true;
            }
            val = val.Replace(" ", "");
            if (val.IndexOf(",") == -1)
            {
                try
                {
                    int.Parse(val);
                    goto Label_0099;
                }
                catch
                {
                    err = "The current configuration contains an error:\n\nThe value specified for {0} is invalid";
                    return false;
                }
            }
            string[] strArray = val.Split(new char[] { ',' });
            if (strArray.Length != 2)
            {
                err = "The current configuration contains an error:\n\nThe value specified for {0} is invalid";
                return false;
            }
            try
            {
                int num = int.Parse(strArray[0]);
                if (int.Parse(strArray[1]) < num)
                {
                    err = "The current configuration contains an error:\n\nThe value specified for {0} is an invalid range because the second parameter is lower than the first.";
                    return false;
                }
            }
            catch
            {
                err = "The current configuration contains an error:\n\nThe value specified for {0} is invalid";
                return false;
            }
        Label_0099:
            return true;
        }

        [XmlAttribute]
        public string AI
        {
            get
            {
                return this.m_AI;
            }
            set
            {
                this.m_AI = value;
            }
        }

        [XmlAttribute]
        public string Damage
        {
            get
            {
                return this.m_Damage;
            }
            set
            {
                this.m_Damage = value;
            }
        }

        public string[] Damages
        {
            get
            {
                return this.m_Damages;
            }
            set
            {
                this.m_Damages = value;
            }
        }

        [XmlAttribute]
        public int Fame
        {
            get
            {
                return this.m_Fame;
            }
            set
            {
                this.m_Fame = value;
            }
        }

        [XmlAttribute]
        public string FightMode
        {
            get
            {
                return this.m_FightMode;
            }
            set
            {
                this.m_FightMode = value;
            }
        }

        [XmlAttribute]
        public string Hits
        {
            get
            {
                return this.m_Hits;
            }
            set
            {
                this.m_Hits = value;
            }
        }

        [XmlAttribute]
        public int Karma
        {
            get
            {
                return this.m_Karma;
            }
            set
            {
                this.m_Karma = value;
            }
        }

        [XmlAttribute]
        public string Mana
        {
            get
            {
                return this.m_Mana;
            }
            set
            {
                this.m_Mana = value;
            }
        }

        [XmlAttribute]
        public string Mount
        {
            get
            {
                return this.m_Mount;
            }
            set
            {
                this.m_Mount = value;
            }
        }

        [XmlAttribute]
        public int MountHue
        {
            get
            {
                return this.m_MountHue;
            }
            set
            {
                this.m_MountHue = value;
            }
        }

        public ArrayList Properties
        {
            get
            {
                return this.m_Properties;
            }
            set
            {
                this.m_Properties = value;
            }
        }

        public string[] Resistances
        {
            get
            {
                return this.m_Resistances;
            }
            set
            {
                this.m_Resistances = value;
            }
        }

        [XmlAttribute]
        public string Stam
        {
            get
            {
                return this.m_Stam;
            }
            set
            {
                this.m_Stam = value;
            }
        }

        public ArrayList Values
        {
            get
            {
                return this.m_Values;
            }
            set
            {
                this.m_Values = value;
            }
        }
    }
}

