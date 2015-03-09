namespace Arya.DialogEditor
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class DialogChoice
    {
        private Guid m_ChoiceID;
        private bool m_EndDialog = false;
        private Guid m_ID = Guid.NewGuid();
        private bool m_Invoke = false;
        private string m_InvokeFunction;
        private string m_InvokeType;
        private string m_Text = "Enter choice text";

        public bool Validate(ref string err)
        {
            if ((this.m_ChoiceID == Guid.Empty) && !this.m_EndDialog)
            {
                err = "Dialog choice has no target speech and doesn't end the conversation";
                return false;
            }
            if (this.m_Invoke && (((this.m_InvokeType == null) || (this.m_InvokeType.Length == 0)) || ((this.m_InvokeFunction == null) || (this.m_InvokeFunction.Length == 0))))
            {
                err = "Trying to invoke a function without specifying a type of function name";
                return false;
            }
            if ((this.m_Text != null) && (this.m_Text.Length != 0))
            {
                return true;
            }
            err = "No text specified for a dialog choice";
            return false;
        }

        [XmlAttribute]
        public Guid ChoiceID
        {
            get
            {
                return this.m_ChoiceID;
            }
            set
            {
                this.m_ChoiceID = value;
            }
        }

        [XmlAttribute]
        public bool EndDialog
        {
            get
            {
                return this.m_EndDialog;
            }
            set
            {
                this.m_EndDialog = value;
            }
        }

        [XmlAttribute]
        public Guid ID
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID = value;
            }
        }

        [XmlAttribute]
        public bool Invoke
        {
            get
            {
                return this.m_Invoke;
            }
            set
            {
                this.m_Invoke = value;
            }
        }

        [XmlAttribute]
        public string InvokeFunction
        {
            get
            {
                return this.m_InvokeFunction;
            }
            set
            {
                this.m_InvokeFunction = value;
            }
        }

        [XmlAttribute]
        public string InvokeType
        {
            get
            {
                return this.m_InvokeType;
            }
            set
            {
                this.m_InvokeType = value;
            }
        }

        [XmlAttribute]
        public string Text
        {
            get
            {
                return this.m_Text;
            }
            set
            {
                this.m_Text = value;
            }
        }
    }
}

