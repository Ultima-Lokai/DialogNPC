namespace Arya.DialogEditor
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable, XmlInclude(typeof(DialogInit)), XmlInclude(typeof(DialogSpeech)), XmlInclude(typeof(DialogChoice))]
    public class Dialog
    {
        private bool m_AllowClose;
        private Hashtable m_Choice = new Hashtable();
        private ArrayList m_ChoiceList = new ArrayList();
        private ArrayList m_Init = new ArrayList();
        private Arya.DialogEditor.Outfit m_Outfit = new Arya.DialogEditor.Outfit();
        private NPCProps m_Props = new NPCProps();
        private int m_Range = 2;
        private Hashtable m_Speech = new Hashtable();
        private ArrayList m_SpeechList = new ArrayList();
        private int m_SpeechRange = 6;
        private ArrayList m_Start = new ArrayList();
        private string m_Title = "New Dialog";

        public void AddChoices(ArrayList choices)
        {
            foreach (DialogChoice choice in choices)
            {
                this.m_Choice.Add(choice.ID, choice);
            }
        }

        public void AddSpeeches(ArrayList speeches)
        {
            foreach (DialogSpeech speech in speeches)
            {
                this.m_Speech.Add(speech.ID, speech);
            }
        }

        public void BuildLists()
        {
            this.m_SpeechList.Clear();
            this.m_ChoiceList.Clear();
            this.m_SpeechList.AddRange(this.m_Speech.Values);
            this.m_ChoiceList.AddRange(this.m_Choice.Values);
        }

        public void BuildTables()
        {
            this.m_Speech.Clear();
            this.m_Choice.Clear();
            foreach (DialogSpeech speech in this.m_SpeechList)
            {
                this.m_Speech.Add(speech.ID, speech);
            }
            foreach (DialogChoice choice in this.m_ChoiceList)
            {
                this.m_Choice.Add(choice.ID, choice);
            }
        }

        public static Dialog Load(string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Dialog));
                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                Dialog dialog = serializer.Deserialize(stream) as Dialog;
                stream.Close();
                dialog.BuildTables();
                return dialog;
            }
            catch
            {
                return null;
            }
        }

        public bool Save(string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Dialog));
                XmlTextWriter writer = new XmlTextWriter(filename, Encoding.Unicode);
                this.BuildLists();
                serializer.Serialize((XmlWriter) writer, this);
                writer.Close();
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return false;
            }
        }

        [XmlAttribute]
        public bool AllowClose
        {
            get
            {
                return this.m_AllowClose;
            }
            set
            {
                this.m_AllowClose = value;
            }
        }

        public ArrayList Choice
        {
            get
            {
                return this.m_ChoiceList;
            }
            set
            {
                this.m_ChoiceList = value;
            }
        }

        [XmlIgnore]
        public Hashtable ChoiceList
        {
            get
            {
                return this.m_Choice;
            }
            set
            {
                this.m_Choice = value;
            }
        }

        public ArrayList Init
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

        public Arya.DialogEditor.Outfit Outfit
        {
            get
            {
                return this.m_Outfit;
            }
            set
            {
                this.m_Outfit = value;
            }
        }

        public NPCProps Props
        {
            get
            {
                return this.m_Props;
            }
            set
            {
                this.m_Props = value;
            }
        }

        [XmlAttribute]
        public int Range
        {
            get
            {
                return this.m_Range;
            }
            set
            {
                this.m_Range = value;
            }
        }

        public ArrayList Speech
        {
            get
            {
                return this.m_SpeechList;
            }
            set
            {
                this.m_SpeechList = value;
            }
        }

        [XmlIgnore]
        public Hashtable SpeechList
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
        public int SpeechRange
        {
            get
            {
                return this.m_SpeechRange;
            }
            set
            {
                this.m_SpeechRange = value;
            }
        }

        public ArrayList Start
        {
            get
            {
                return this.m_Start;
            }
            set
            {
                this.m_Start = value;
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

