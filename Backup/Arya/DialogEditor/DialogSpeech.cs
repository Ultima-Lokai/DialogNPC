namespace Arya.DialogEditor
{
    using System;
    using System.Collections;
    using System.Xml.Serialization;

    [Serializable]
    public class DialogSpeech
    {
        private ArrayList m_Choices = new ArrayList();
        private Guid m_ID = Guid.NewGuid();
        private Guid m_Parent = Guid.Empty;
        private string m_Text;
        private string m_Title = "Section Title";

        public bool Validate(ref string err)
        {
            if ((this.m_Text == null) || (this.m_Text.Length == 0))
            {
                err = "No text specified in a speech";
                return false;
            }
            if (this.m_Choices.Count == 0)
            {
                err = "No choices avialable for a speech";
                return false;
            }
            return true;
        }

        public ArrayList Choices
        {
            get
            {
                return this.m_Choices;
            }
            set
            {
                this.m_Choices = value;
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
        public Guid Parent
        {
            get
            {
                return this.m_Parent;
            }
            set
            {
                this.m_Parent = value;
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

