namespace Arya.DialogEditor
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class DialogInit
    {
        private int m_AmountBackpack = 1;
        private int m_AmountGiven = 1;
        private string m_FunctionName = string.Empty;
        private string m_FunctionType = string.Empty;
        private string[] m_Keywords;
        private bool m_ReactToDoubleClick;
        private bool m_ReactToItemGiven;
        private bool m_ReactToItemInBackpack;
        private bool m_ReactToKeywords;
        private Guid m_Speech = Guid.Empty;
        private bool m_TriggerFunction = false;
        private string m_TypeBackpack;
        private string m_TypeGiven;

        public bool Validate(ref string err)
        {
            if (this.m_Speech == Guid.Empty)
            {
                err = "Trigger not linked to a speech";
                return false;
            }
            if (this.m_ReactToKeywords && ((this.m_Keywords == null) || (this.m_Keywords.Length == 0)))
            {
                err = "No keywords have been specified for the react to keywords trigger";
                return false;
            }
            if (this.m_ReactToItemInBackpack && ((this.m_TypeBackpack == null) || (this.m_TypeBackpack.Length == 0)))
            {
                err = "No type specified for the 'Item in backpack' trigger";
                return false;
            }
            if (this.m_ReactToItemGiven && ((this.m_TypeGiven == null) || (this.m_TypeGiven.Length == 0)))
            {
                err = "No type specified for the 'Item given' trigger";
                return false;
            }
            bool flag = ((this.m_ReactToItemGiven || this.m_ReactToItemInBackpack) || this.m_ReactToKeywords) || this.m_ReactToDoubleClick;
            if (!flag && this.m_TriggerFunction)
            {
                err = "You can only use the function trigger in conjunction with one of the other triggers";
                return false;
            }
            if (this.m_TriggerFunction && (((this.m_FunctionType == null) || (this.m_FunctionType.Length == 0)) || ((this.m_FunctionName == null) || (this.m_FunctionName.Length == 0))))
            {
                err = "You must specify a valid type and function name when using the trigger function option";
                return false;
            }
            if (!flag)
            {
                err = "No trigger specified";
                return false;
            }
            return true;
        }

        [XmlAttribute]
        public int AmountBackpack
        {
            get
            {
                return this.m_AmountBackpack;
            }
            set
            {
                this.m_AmountBackpack = value;
            }
        }

        [XmlAttribute]
        public int AmountGiven
        {
            get
            {
                return this.m_AmountGiven;
            }
            set
            {
                this.m_AmountGiven = value;
            }
        }

        [XmlAttribute]
        public string FunctionName
        {
            get
            {
                return this.m_FunctionName;
            }
            set
            {
                this.m_FunctionName = value;
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

        public string[] Keywords
        {
            get
            {
                return this.m_Keywords;
            }
            set
            {
                this.m_Keywords = value;
            }
        }

        [XmlIgnore]
        public string KeywordsString
        {
            get
            {
                if ((this.m_Keywords == null) || (this.m_Keywords.Length == 0))
                {
                    return null;
                }
                string str = this.m_Keywords[0];
                for (int i = 1; i < this.m_Keywords.Length; i++)
                {
                    str = str + "," + this.m_Keywords[i];
                }
                return str;
            }
            set
            {
                this.m_Keywords = value.Split(new char[] { ',' });
                for (int i = 0; i < this.m_Keywords.Length; i++)
                {
                    this.m_Keywords[i] = this.m_Keywords[i].Trim();
                }
            }
        }

        [XmlAttribute]
        public bool ReactToDoubleClick
        {
            get
            {
                return this.m_ReactToDoubleClick;
            }
            set
            {
                this.m_ReactToDoubleClick = value;
            }
        }

        [XmlAttribute]
        public bool ReactToItemGiven
        {
            get
            {
                return this.m_ReactToItemGiven;
            }
            set
            {
                this.m_ReactToItemGiven = value;
            }
        }

        [XmlAttribute]
        public bool ReactToItemInBackpack
        {
            get
            {
                return this.m_ReactToItemInBackpack;
            }
            set
            {
                this.m_ReactToItemInBackpack = value;
            }
        }

        [XmlAttribute]
        public bool ReactToKeywords
        {
            get
            {
                return this.m_ReactToKeywords;
            }
            set
            {
                this.m_ReactToKeywords = value;
            }
        }

        [XmlAttribute]
        public Guid Speech
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

        [XmlAttribute]
        public bool TriggerFunction
        {
            get
            {
                return this.m_TriggerFunction;
            }
            set
            {
                this.m_TriggerFunction = value;
            }
        }

        [XmlAttribute]
        public string TypeBackpack
        {
            get
            {
                return this.m_TypeBackpack;
            }
            set
            {
                this.m_TypeBackpack = value;
            }
        }

        [XmlAttribute]
        public string TypeGiven
        {
            get
            {
                return this.m_TypeGiven;
            }
            set
            {
                this.m_TypeGiven = value;
            }
        }
    }
}

