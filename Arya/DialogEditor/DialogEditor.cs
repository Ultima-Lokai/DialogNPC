namespace Arya.DialogEditor
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using Ultima;

    public class DialogEditor : Form
    {
        private Button bChoice;
        private Button bCopy;
        private Button bDelete;
        private Button bDown;
        private Button bInit;
        private Button bSpeech;
        private Button bTest;
        private Button bUp;
        private Button bVerify;
        private Container components = null;
        private bool m_Changed = false;
        private Dialog m_Dialog;
        private Point m_DragStart = new Point(-100, -100);
        private string m_FileName = null;
        private Hashtable m_Nodes;
        private Control m_Panel;
        private object m_Selection = null;
        private MainMenu menu;
        private MenuItem menuItem6;
        private MenuItem mFile;
        private MenuItem mFileExit;
        private MenuItem mFileNew;
        private MenuItem mFileOpen;
        private MenuItem mFileSave;
        private MenuItem mFileSaveAs;
        private OpenFileDialog OpenFile;
        private Panel panelLeft;
        private Panel panelLow;
        private Panel panelTop;
        private SaveFileDialog SaveFile;
        private TreeView tree;

        public DialogEditor()
        {
            this.InitializeComponent();
        }

        private void bChoice_Click(object sender, EventArgs e)
        {
            DialogSpeech selection = this.m_Selection as DialogSpeech;
            if ((selection != null) && (this.tree.SelectedNode != null))
            {
                TreeNode choiceNode = this.GetChoiceNode(selection);
                this.tree.SelectedNode.Nodes.Add(choiceNode);
                this.tree.SelectedNode = choiceNode;
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tree.SelectedNode;
            this.RemoveNode(selectedNode);
            this.tree.Nodes.Remove(selectedNode);
            this.tree.SelectedNode = null;
            this.UpdateButtons();
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            if (this.tree.SelectedNode != null)
            {
                TreeNode selectedNode = this.tree.SelectedNode;
                TreeNode parent = selectedNode.Parent;
                int index = parent.Nodes.IndexOf(selectedNode);
                parent.Nodes.Remove(selectedNode);
                parent.Nodes.Insert(++index, selectedNode);
                this.tree.SelectedNode = selectedNode;
            }
        }

        private void bInit_Click(object sender, EventArgs e)
        {
            TreeNode initNode = this.GetInitNode(null);
            this.tree.SelectedNode.Nodes.Add(initNode);
            this.tree.SelectedNode = initNode;
        }

        private void bSpeech_Click(object sender, EventArgs e)
        {
            if (this.tree.SelectedNode != null)
            {
                DialogChoice selection = this.m_Selection as DialogChoice;
                TreeNode speechNode = this.GetSpeechNode(selection);
                this.tree.SelectedNode.Nodes.Add(speechNode);
                this.tree.SelectedNode = speechNode;
            }
        }

        private void bTest_Click(object sender, EventArgs e)
        {
            this.Test(this.m_Selection as DialogInit);
        }

        private TreeNode BuildChoiceNode(DialogChoice choice, DialogSpeech parent)
        {
            TreeNode choiceNode = this.GetChoiceNode(choice, parent);
            if ((choice.ChoiceID != Guid.Empty) && (this.m_Dialog.SpeechList[choice.ChoiceID] != null))
            {
                DialogSpeech speech = this.m_Dialog.SpeechList[choice.ChoiceID] as DialogSpeech;
                if (speech.Parent == choice.ID)
                {
                    choiceNode.Nodes.Add(this.BuildSpeechNode(speech));
                }
            }
            return choiceNode;
        }

        private bool BuildDialog()
        {
            this.m_Dialog.Init.Clear();
            this.m_Dialog.ChoiceList.Clear();
            this.m_Dialog.SpeechList.Clear();
            this.m_Dialog.Start.Clear();
            TreeNode node = this.tree.Nodes[1];
            TreeNode node2 = this.tree.Nodes[2];
            string err = null;
            if (node.Nodes.Count == 0)
            {
                MessageBox.Show("There are no initial reactions defined");
                this.tree.SelectedNode = node;
                return false;
            }
            foreach (TreeNode node3 in node.Nodes)
            {
                DialogInit tag = node3.Tag as DialogInit;
                TreeNode node4 = this.m_Nodes[tag.Speech] as TreeNode;
                if ((node4 == null) || (node4.TreeView == null))
                {
                    tag.Speech = Guid.Empty;
                }
                if (!tag.Validate(ref err))
                {
                    MessageBox.Show("The current configuration isn't correct. The following error has been reported:\n\n" + err);
                    this.tree.SelectedNode = node3;
                    return false;
                }
                this.m_Dialog.Init.Add(tag);
            }
            foreach (DialogChoice choice in this.GetChoices())
            {
                TreeNode node5 = this.m_Nodes[choice.ChoiceID] as TreeNode;
                if ((node5 == null) || (node5.TreeView == null))
                {
                    choice.ChoiceID = Guid.Empty;
                }
                if (!choice.Validate(ref err))
                {
                    MessageBox.Show("The current configuration isn't correct. The following error has been reported:\n\n" + err);
                    this.tree.SelectedNode = this.m_Nodes[choice.ID] as TreeNode;
                    return false;
                }
                this.m_Dialog.ChoiceList.Add(choice.ID, choice);
            }
            foreach (DialogSpeech speech in this.GetSpeeches())
            {
                ArrayList list3 = new ArrayList();
                foreach (Guid guid in speech.Choices)
                {
                    TreeNode node6 = this.m_Nodes[guid] as TreeNode;
                    if ((node6 == null) || (node6.TreeView == null))
                    {
                        list3.Add(guid);
                    }
                }
                foreach (Guid guid2 in list3)
                {
                    speech.Choices.Remove(guid2);
                }
                if (!speech.Validate(ref err))
                {
                    MessageBox.Show("The current configuration isn't correct. The following error has been reported:\n\n" + err);
                    this.tree.SelectedNode = this.m_Nodes[speech.ID] as TreeNode;
                    return false;
                }
                TreeNode node7 = this.m_Nodes[speech.ID] as TreeNode;
                if (node7.Parent == node2)
                {
                    speech.Parent = Guid.Empty;
                }
                else
                {
                    speech.Parent = (node7.Parent.Tag as DialogChoice).ID;
                }
                this.m_Dialog.SpeechList.Add(speech.ID, speech);
            }
            if (this.m_Dialog.Outfit.CustomOutfit && (this.m_Dialog.Outfit.Name.Length == 0))
            {
                MessageBox.Show("The current configuration isn't correct:\n\nWhen using a custom outfit you must specify a name");
                this.tree.SelectedNode = this.tree.Nodes[3];
                return false;
            }
            if (this.m_Dialog.Outfit.CustomFunction && ((this.m_Dialog.Outfit.FunctionType.Length == 0) || (this.m_Dialog.Outfit.Function.Length == 0)))
            {
                MessageBox.Show("The current configuration isn't correct:\n\nWhen using a custom outfit function you must specify its type and name.");
                this.tree.SelectedNode = this.tree.Nodes[3];
                return false;
            }
            if (!this.m_Dialog.Props.Validate(ref err))
            {
                MessageBox.Show(err);
                this.tree.SelectedNode = this.tree.Nodes[4];
                return false;
            }
            foreach (TreeNode node8 in node2.Nodes)
            {
                DialogSpeech speech2 = node8.Tag as DialogSpeech;
                speech2.Parent = Guid.Empty;
                this.m_Dialog.Start.Add(speech2.ID);
            }
            return true;
        }

        private TreeNode BuildSpeechNode(DialogSpeech speech)
        {
            TreeNode speechNode = this.GetSpeechNode(speech, null);
            foreach (Guid guid in speech.Choices)
            {
                DialogChoice choice = this.m_Dialog.ChoiceList[guid] as DialogChoice;
                if (choice != null)
                {
                    speechNode.Nodes.Add(this.BuildChoiceNode(choice, speech));
                }
            }
            return speechNode;
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            if (this.tree.SelectedNode != null)
            {
                TreeNode parent = this.tree.SelectedNode.Parent;
                TreeNode selectedNode = this.tree.SelectedNode;
                int index = parent.Nodes.IndexOf(selectedNode);
                parent.Nodes.Remove(selectedNode);
                parent.Nodes.Insert(--index, selectedNode);
                this.tree.SelectedNode = selectedNode;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogSpeech selection = this.m_Selection as DialogSpeech;
            if (selection != null)
            {
                Clipboard.SetDataObject(selection.ID.ToString(), true);
            }
        }

        private void bVerify_Click(object sender, EventArgs e)
        {
            if (this.BuildDialog())
            {
                MessageBox.Show("Verification succesful. This dialog can be used to configure a NPC.");
            }
        }

        private void ChoiceTitleChanged(object sender, EventArgs e)
        {
            ChoiceEditor editor = sender as ChoiceEditor;
            if ((editor != null) && (this.tree.SelectedNode != null))
            {
                this.tree.SelectedNode.Text = editor.Choice.Text;
            }
        }

        private void DialogEditor_Closing(object sender, CancelEventArgs e)
        {
            if (!this.Overwrite())
            {
                e.Cancel = true;
            }
        }

        private void DialogEditor_Load(object sender, EventArgs e)
        {
            string directoryName;
            if (Client.GetFilePath("unifont1.mul") == null)
            {
                MessageBox.Show("Please locate the file unifont1.mul");
                this.OpenFile.Filter = "unifont1.mul|unifont1.mul";
                if (this.OpenFile.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Access to unifont1.mul is required for this program to function correctly. Closing.");
                    Application.Exit();
                    return;
                }
                directoryName = Path.GetDirectoryName(this.OpenFile.FileName);
                Client.Directories.Insert(0, directoryName);
            }
            if (Client.GetFilePath("hues.mul") == null)
            {
                MessageBox.Show("Please locate the file hues.mul");
                this.OpenFile.Filter = "hues.mul|hues.mul";
                if (this.OpenFile.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Access to hues.mul is required for this program to function correctly. Closing.");
                    Application.Exit();
                    return;
                }
                directoryName = Path.GetDirectoryName(this.OpenFile.FileName);
                Client.Directories.Insert(0, directoryName);
            }
            this.DoNewDialog();
        }

        private void DialogTitleChanged(object sender, EventArgs e)
        {
            DialogOptionsEditor editor = sender as DialogOptionsEditor;
            if ((editor != null) && (this.tree.SelectedNode != null))
            {
                this.tree.SelectedNode.Text = editor.Dialog.Title;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DoNewDialog()
        {
            this.m_Dialog = new Dialog();
            this.m_Changed = false;
            this.m_Nodes = new Hashtable();
            this.m_FileName = null;
            this.tree.BeginUpdate();
            this.tree.Nodes.Clear();
            TreeNode dialogNode = this.GetDialogNode(this.m_Dialog);
            this.tree.Nodes.Add(dialogNode);
            TreeNode node = new TreeNode("Reactions");
            node.Tag = "R";
            this.tree.Nodes.Add(node);
            TreeNode node3 = new TreeNode("Dialog Structure");
            node3.Tag = "D";
            this.tree.Nodes.Add(node3);
            TreeNode speechNode = this.GetSpeechNode(null);
            node3.Nodes.Add(speechNode);
            TreeNode initNode = this.GetInitNode(speechNode.Tag as DialogSpeech);
            node.Nodes.Add(initNode);
            TreeNode outfitNode = this.GetOutfitNode(this.m_Dialog.Outfit);
            this.tree.Nodes.Add(outfitNode);
            TreeNode propsNode = this.GetPropsNode(this.m_Dialog.Props);
            this.tree.Nodes.Add(propsNode);
            this.tree.EndUpdate();
        }

        private void edit_Changed(object sender, EventArgs e)
        {
            this.m_Changed = true;
        }

        private void edit_ViewChoice(object sender, EventArgs e)
        {
            ChoiceEditor editor = sender as ChoiceEditor;
            if (editor != null)
            {
                TreeNode node = this.m_Nodes[editor.Choice.ChoiceID] as TreeNode;
                if ((node == null) || (node.TreeView == null))
                {
                    editor.MakeChoiceInvalid();
                }
                else
                {
                    this.tree.SelectedNode = node;
                }
            }
        }

        private void edit_ViewSpeech(object sender, EventArgs e)
        {
            InitEditor editor = sender as InitEditor;
            if (editor != null)
            {
                TreeNode node = this.m_Nodes[editor.Init.Speech] as TreeNode;
                if ((node == null) || (node.TreeView == null))
                {
                    editor.MakeSpeechInvalid();
                }
                else
                {
                    this.tree.SelectedNode = node;
                }
            }
        }

        private void FindObjects(TreeNode node, System.Type type, ArrayList results)
        {
            if ((node.Tag != null) && (node.Tag.GetType() == type))
            {
                results.Add(node.Tag);
            }
            foreach (TreeNode node2 in node.Nodes)
            {
                this.FindObjects(node2, type, results);
            }
        }

        private TreeNode GetChoiceNode(DialogSpeech parent)
        {
            return this.GetChoiceNode(new DialogChoice(), parent);
        }

        private TreeNode GetChoiceNode(DialogChoice choice, DialogSpeech parent)
        {
            if (!parent.Choices.Contains(choice.ID))
            {
                parent.Choices.Add(choice.ID);
            }
            TreeNode node = new TreeNode(choice.Text);
            node.ForeColor = Color.Blue;
            node.Tag = choice;
            this.m_Nodes.Add(choice.ID, node);
            return node;
        }

        private ArrayList GetChoices()
        {
            ArrayList results = new ArrayList();
            TreeNode node = this.tree.Nodes[2];
            this.FindObjects(node, typeof(DialogChoice), results);
            return results;
        }

        private TreeNode GetDialogNode(Dialog dialog)
        {
            TreeNode node = new TreeNode(dialog.Title);
            node.Tag = dialog;
            return node;
        }

        private TreeNode GetInitNode(DialogSpeech target)
        {
            return this.GetInitNode(new DialogInit(), target);
        }

        private TreeNode GetInitNode(DialogInit init, DialogSpeech target)
        {
            TreeNode node = new TreeNode("NPC Reaction Triggers");
            node.Tag = init;
            node.ForeColor = Color.Indigo;
            if (target != null)
            {
                init.Speech = target.ID;
            }
            return node;
        }

        private ArrayList GetInits()
        {
            ArrayList results = new ArrayList();
            TreeNode node = this.tree.Nodes[1];
            this.FindObjects(node, typeof(DialogInit), results);
            return results;
        }

        private TreeNode GetOutfitNode(Outfit outfit)
        {
            TreeNode node = new TreeNode("Outfit");
            node.Tag = outfit;
            return node;
        }

        private TreeNode GetPropsNode(NPCProps props)
        {
            TreeNode node = new TreeNode("Properties");
            node.Tag = props;
            return node;
        }

        private ArrayList GetSpeeches()
        {
            ArrayList results = new ArrayList();
            TreeNode node = this.tree.Nodes[2];
            this.FindObjects(node, typeof(DialogSpeech), results);
            return results;
        }

        private TreeNode GetSpeechNode(DialogChoice parent)
        {
            return this.GetSpeechNode(new DialogSpeech(), parent);
        }

        private TreeNode GetSpeechNode(DialogSpeech speech, DialogChoice parent)
        {
            if (parent != null)
            {
                parent.ChoiceID = speech.ID;
            }
            TreeNode node = new TreeNode(speech.Title);
            node.ForeColor = Color.DarkOliveGreen;
            node.NodeFont = this.NodeFont;
            node.Tag = speech;
            this.m_Nodes.Add(speech.ID, node);
            return node;
        }

        private void InitializeComponent()
        {
            this.menu = new MainMenu();
            this.mFile = new MenuItem();
            this.mFileNew = new MenuItem();
            this.mFileOpen = new MenuItem();
            this.mFileSave = new MenuItem();
            this.mFileSaveAs = new MenuItem();
            this.menuItem6 = new MenuItem();
            this.mFileExit = new MenuItem();
            this.panelLow = new Panel();
            this.panelTop = new Panel();
            this.tree = new TreeView();
            this.panelLeft = new Panel();
            this.bVerify = new Button();
            this.bTest = new Button();
            this.bDown = new Button();
            this.bUp = new Button();
            this.bDelete = new Button();
            this.bInit = new Button();
            this.bSpeech = new Button();
            this.bChoice = new Button();
            this.OpenFile = new OpenFileDialog();
            this.SaveFile = new SaveFileDialog();
            this.bCopy = new Button();
            this.panelTop.SuspendLayout();
            this.panelLeft.SuspendLayout();
            base.SuspendLayout();
            this.menu.MenuItems.AddRange(new MenuItem[] { this.mFile });
            this.mFile.Index = 0;
            this.mFile.MenuItems.AddRange(new MenuItem[] { this.mFileNew, this.mFileOpen, this.mFileSave, this.mFileSaveAs, this.menuItem6, this.mFileExit });
            this.mFile.Text = "File";
            this.mFile.Popup += new EventHandler(this.mFile_Popup);
            this.mFileNew.Index = 0;
            this.mFileNew.Text = "New";
            this.mFileNew.Click += new EventHandler(this.mFileNew_Click);
            this.mFileOpen.Index = 1;
            this.mFileOpen.Text = "Open";
            this.mFileOpen.Click += new EventHandler(this.mFileOpen_Click);
            this.mFileSave.Index = 2;
            this.mFileSave.Text = "Save";
            this.mFileSave.Click += new EventHandler(this.mFileSave_Click);
            this.mFileSaveAs.Index = 3;
            this.mFileSaveAs.Text = "Save As..";
            this.mFileSaveAs.Click += new EventHandler(this.mFileSaveAs_Click);
            this.menuItem6.Index = 4;
            this.menuItem6.Text = "-";
            this.mFileExit.Index = 5;
            this.mFileExit.Text = "Exit";
            this.mFileExit.Click += new EventHandler(this.mFileExit_Click);
            this.panelLow.BackColor = SystemColors.Control;
            this.panelLow.Dock = DockStyle.Bottom;
            this.panelLow.Location = new Point(0, 0x10d);
            this.panelLow.Name = "panelLow";
            this.panelLow.Size = new Size(0x250, 0xec);
            this.panelLow.TabIndex = 0;
            this.panelTop.BackColor = Color.LightSlateGray;
            this.panelTop.Controls.Add(this.tree);
            this.panelTop.Controls.Add(this.panelLeft);
            this.panelTop.Dock = DockStyle.Fill;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new Size(0x250, 0x10d);
            this.panelTop.TabIndex = 1;
            this.tree.Dock = DockStyle.Fill;
            this.tree.HideSelection = false;
            this.tree.ImageIndex = -1;
            this.tree.Location = new Point(0x54, 0);
            this.tree.Name = "tree";
            this.tree.SelectedImageIndex = -1;
            this.tree.Size = new Size(0x1fc, 0x10d);
            this.tree.TabIndex = 1;
            this.tree.MouseDown += new MouseEventHandler(this.tree_MouseDown);
            this.tree.MouseUp += new MouseEventHandler(this.tree_MouseUp);
            this.tree.AfterSelect += new TreeViewEventHandler(this.tree_AfterSelect);
            this.tree.MouseMove += new MouseEventHandler(this.tree_MouseMove);
            this.panelLeft.BackColor = SystemColors.Control;
            this.panelLeft.Controls.Add(this.bCopy);
            this.panelLeft.Controls.Add(this.bVerify);
            this.panelLeft.Controls.Add(this.bTest);
            this.panelLeft.Controls.Add(this.bDown);
            this.panelLeft.Controls.Add(this.bUp);
            this.panelLeft.Controls.Add(this.bDelete);
            this.panelLeft.Controls.Add(this.bInit);
            this.panelLeft.Controls.Add(this.bSpeech);
            this.panelLeft.Controls.Add(this.bChoice);
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Location = new Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new Size(0x54, 0x10d);
            this.panelLeft.TabIndex = 0;
            this.bVerify.FlatStyle = FlatStyle.System;
            this.bVerify.Location = new Point(4, 180);
            this.bVerify.Name = "bVerify";
            this.bVerify.Size = new Size(0x4c, 0x17);
            this.bVerify.TabIndex = 7;
            this.bVerify.Text = "Verify Dialog";
            this.bVerify.TextAlign = ContentAlignment.MiddleLeft;
            this.bVerify.Click += new EventHandler(this.bVerify_Click);
            this.bTest.Enabled = false;
            this.bTest.FlatStyle = FlatStyle.System;
            this.bTest.Location = new Point(4, 0x9c);
            this.bTest.Name = "bTest";
            this.bTest.Size = new Size(0x4c, 0x17);
            this.bTest.TabIndex = 6;
            this.bTest.Text = "Test";
            this.bTest.TextAlign = ContentAlignment.MiddleLeft;
            this.bTest.Click += new EventHandler(this.bTest_Click);
            this.bDown.Enabled = false;
            this.bDown.FlatStyle = FlatStyle.System;
            this.bDown.Location = new Point(4, 0x68);
            this.bDown.Name = "bDown";
            this.bDown.Size = new Size(0x4c, 0x17);
            this.bDown.TabIndex = 5;
            this.bDown.Text = "Move Down";
            this.bDown.TextAlign = ContentAlignment.MiddleLeft;
            this.bDown.Click += new EventHandler(this.bDown_Click);
            this.bUp.Enabled = false;
            this.bUp.FlatStyle = FlatStyle.System;
            this.bUp.Location = new Point(4, 80);
            this.bUp.Name = "bUp";
            this.bUp.Size = new Size(0x4c, 0x17);
            this.bUp.TabIndex = 4;
            this.bUp.Text = "Move Up";
            this.bUp.TextAlign = ContentAlignment.MiddleLeft;
            this.bUp.Click += new EventHandler(this.bUp_Click);
            this.bDelete.Enabled = false;
            this.bDelete.FlatStyle = FlatStyle.System;
            this.bDelete.Location = new Point(4, 0x80);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new Size(0x4c, 0x17);
            this.bDelete.TabIndex = 3;
            this.bDelete.Text = "Delete";
            this.bDelete.TextAlign = ContentAlignment.MiddleLeft;
            this.bDelete.Click += new EventHandler(this.bDelete_Click);
            this.bInit.Enabled = false;
            this.bInit.FlatStyle = FlatStyle.System;
            this.bInit.Location = new Point(4, 0x34);
            this.bInit.Name = "bInit";
            this.bInit.Size = new Size(0x4c, 0x17);
            this.bInit.TabIndex = 2;
            this.bInit.Text = "Add Startup";
            this.bInit.TextAlign = ContentAlignment.MiddleLeft;
            this.bInit.Click += new EventHandler(this.bInit_Click);
            this.bSpeech.Enabled = false;
            this.bSpeech.FlatStyle = FlatStyle.System;
            this.bSpeech.Location = new Point(4, 0x1c);
            this.bSpeech.Name = "bSpeech";
            this.bSpeech.Size = new Size(0x4c, 0x17);
            this.bSpeech.TabIndex = 1;
            this.bSpeech.Text = "Add Speech";
            this.bSpeech.TextAlign = ContentAlignment.MiddleLeft;
            this.bSpeech.Click += new EventHandler(this.bSpeech_Click);
            this.bChoice.Enabled = false;
            this.bChoice.FlatStyle = FlatStyle.System;
            this.bChoice.Location = new Point(4, 4);
            this.bChoice.Name = "bChoice";
            this.bChoice.Size = new Size(0x4c, 0x17);
            this.bChoice.TabIndex = 0;
            this.bChoice.Text = "Add Choice";
            this.bChoice.TextAlign = ContentAlignment.MiddleLeft;
            this.bChoice.Click += new EventHandler(this.bChoice_Click);
            this.SaveFile.Filter = "Xml Files (*.xml)|*.xml";
            this.bCopy.FlatStyle = FlatStyle.System;
            this.bCopy.Location = new Point(4, 0xcc);
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new Size(0x4c, 0x17);
            this.bCopy.TabIndex = 8;
            this.bCopy.Text = "Copy GUID";
            this.bCopy.TextAlign = ContentAlignment.MiddleLeft;
            this.bCopy.Click += new EventHandler(this.button1_Click);
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x250, 0x1f9);
            base.Controls.Add(this.panelTop);
            base.Controls.Add(this.panelLow);
            base.Menu = this.menu;
            base.Name = "DialogEditor";
            this.Text = "Dialog Editor";
            base.Closing += new CancelEventHandler(this.DialogEditor_Closing);
            base.Load += new EventHandler(this.DialogEditor_Load);
            this.panelTop.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private bool LoadFile()
        {
            Dialog dialog = Dialog.Load(this.m_FileName);
            if (dialog != null)
            {
                this.m_Dialog = dialog;
            }
            else
            {
                MessageBox.Show("Failed to load the selected file");
            }
            return (dialog != null);
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new Arya.DialogEditor.DialogEditor());
        }

        private void mFile_Popup(object sender, EventArgs e)
        {
            this.mFileSave.Enabled = this.m_FileName != null;
        }

        private void mFileExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void mFileNew_Click(object sender, EventArgs e)
        {
            if (this.Overwrite())
            {
                this.DoNewDialog();
            }
        }

        private void mFileOpen_Click(object sender, EventArgs e)
        {
            if (this.Overwrite())
            {
                this.OpenFile.Filter = "Xml Files (*.xml)|*.xml";
                if (this.OpenFile.ShowDialog() == DialogResult.OK)
                {
                    this.m_FileName = this.OpenFile.FileName;
                    if (this.LoadFile())
                    {
                        this.Rebuild();
                    }
                }
            }
        }

        private void mFileSave_Click(object sender, EventArgs e)
        {
            if (this.m_FileName != null)
            {
                this.Save();
            }
            else
            {
                this.mFileSaveAs.PerformClick();
            }
        }

        private void mFileSaveAs_Click(object sender, EventArgs e)
        {
            if (this.SaveFile.ShowDialog() == DialogResult.OK)
            {
                this.m_FileName = this.SaveFile.FileName;
                this.Save();
            }
        }

        private bool Overwrite()
        {
            if (!this.m_Changed)
            {
                return true;
            }
            switch (MessageBox.Show(this, "The current project hasn't been saved and all changes will be lost. Would you like to save it?", "Current project not saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Cancel:
                    return false;

                case DialogResult.No:
                    return true;
            }
            if (this.m_FileName == null)
            {
                if (this.SaveFile.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                this.m_FileName = this.SaveFile.FileName;
            }
            return this.Save();
        }

        private void Rebuild()
        {
            this.m_Nodes = new Hashtable();
            this.tree.BeginUpdate();
            this.tree.Nodes.Clear();
            TreeNode dialogNode = this.GetDialogNode(this.m_Dialog);
            this.tree.Nodes.Add(dialogNode);
            TreeNode node = new TreeNode("Reactions");
            node.Tag = "R";
            this.tree.Nodes.Add(node);
            TreeNode node3 = new TreeNode("Dialog Structure");
            node3.Tag = "D";
            this.tree.Nodes.Add(node3);
            int count = this.m_Dialog.ChoiceList.Count;
            foreach (DialogInit init in this.m_Dialog.Init)
            {
                node.Nodes.Add(this.GetInitNode(init, null));
            }
            foreach (Guid guid in this.m_Dialog.Start)
            {
                DialogSpeech speech = this.m_Dialog.SpeechList[guid] as DialogSpeech;
                if (speech != null)
                {
                    node3.Nodes.Add(this.BuildSpeechNode(speech));
                }
            }
            TreeNode outfitNode = this.GetOutfitNode(this.m_Dialog.Outfit);
            this.tree.Nodes.Add(outfitNode);
            TreeNode propsNode = this.GetPropsNode(this.m_Dialog.Props);
            this.tree.Nodes.Add(propsNode);
            this.tree.EndUpdate();
        }

        private void RemoveNode(TreeNode node)
        {
            if (node.Tag is DialogChoice)
            {
                Guid iD = (node.Tag as DialogChoice).ID;
                this.m_Nodes.Remove(iD);
            }
            else if (node.Tag is DialogSpeech)
            {
                Guid key = (node.Tag as DialogSpeech).ID;
                this.m_Nodes.Remove(key);
            }
            foreach (TreeNode node2 in node.Nodes)
            {
                this.RemoveNode(node2);
            }
        }

        private bool Save()
        {
            if (!this.SaveBuildDialog())
            {
                return false;
            }
            if (!this.m_Dialog.Save(this.m_FileName))
            {
                MessageBox.Show("An error occurred during the save");
                return false;
            }
            this.m_Changed = false;
            return true;
        }

        private bool SaveBuildDialog()
        {
            this.m_Dialog.Init.Clear();
            this.m_Dialog.ChoiceList.Clear();
            this.m_Dialog.SpeechList.Clear();
            this.m_Dialog.Start.Clear();
            TreeNode node = this.tree.Nodes[2];
            ArrayList choices = this.GetChoices();
            ArrayList speeches = this.GetSpeeches();
            foreach (DialogSpeech speech in speeches)
            {
                TreeNode node2 = this.m_Nodes[speech.ID] as TreeNode;
                if (node2.Parent == node)
                {
                    speech.Parent = Guid.Empty;
                    continue;
                }
                speech.Parent = (node2.Parent.Tag as DialogChoice).ID;
                bool flag = false;
                foreach (DialogChoice choice in choices)
                {
                    if ((choice.ChoiceID == speech.ID) && !choice.EndDialog)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    MessageBox.Show("An error has occurred in the dialog structure. A speech element isn't referenced by any choice, therefore it cannot be saved.");
                    this.tree.SelectedNode = node2;
                    return false;
                }
            }
            this.m_Dialog.AddSpeeches(speeches);
            this.m_Dialog.AddChoices(choices);
            this.m_Dialog.Init = this.GetInits();
            foreach (TreeNode node3 in node.Nodes)
            {
                DialogSpeech tag = node3.Tag as DialogSpeech;
                this.m_Dialog.Start.Add(tag.ID);
            }
            return true;
        }

        private void SpeechTitleChanged(object sender, EventArgs e)
        {
            SpeechEdit edit = sender as SpeechEdit;
            if ((edit != null) && (this.tree.SelectedNode != null))
            {
                this.tree.SelectedNode.Text = edit.Speech.Title;
            }
        }

        private void Test(DialogInit init)
        {
            TreeNode speech = this.m_Nodes[init.Speech] as TreeNode;
            this.Test(speech);
        }

        private void Test(TreeNode speech)
        {
            if ((speech == null) || (speech.TreeView == null))
            {
                MessageBox.Show("The speech corresponding to the selected reaction no longer exists. Testing aborted.");
            }
            else
            {
                DialogSpeech tag = speech.Tag as DialogSpeech;
                ArrayList choices = new ArrayList();
                foreach (TreeNode node in speech.Nodes)
                {
                    DialogChoice choice = node.Tag as DialogChoice;
                    choices.Add(choice);
                }
                int num = 0;
                DialogPreviewForm form = new DialogPreviewForm();
                form.DisplayDialog(this.m_Dialog.Title, tag.Title, tag.Text, choices, this.m_Dialog.AllowClose);
                num = form.Choice;
                if (num != -1)
                {
                    DialogChoice choice2 = choices[num] as DialogChoice;
                    if (choice2.EndDialog)
                    {
                        MessageBox.Show("The selected choice ends the conversation. Testing ended.");
                    }
                    else
                    {
                        TreeNode node2 = this.m_Nodes[choice2.ChoiceID] as TreeNode;
                        if ((node2 == null) || (node2.TreeView == null))
                        {
                            MessageBox.Show("The selected choice leads to a speech that doesn't exist. Please use the Verify tool to ensure that the dialog structure is consistent");
                        }
                        else
                        {
                            this.Test(node2);
                        }
                    }
                }
            }
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                this.m_Selection = e.Node.Tag;
            }
            else
            {
                this.m_Selection = null;
            }
            this.UpdatePanel();
            this.UpdateButtons();
        }

        private void tree_MouseDown(object sender, MouseEventArgs e)
        {
            this.m_DragStart = new Point(e.X, e.Y);
        }

        private void tree_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_DragStart != new Point(-100, -100))
            {
                int num = Math.Abs((int) (e.X - this.m_DragStart.X));
                int num2 = Math.Abs((int) (e.Y - this.m_DragStart.Y));
                if ((num + num2) > 8)
                {
                    TreeNode nodeAt = this.tree.GetNodeAt(this.m_DragStart);
                    if ((nodeAt != null) && (nodeAt.Tag is DialogSpeech))
                    {
                        base.DoDragDrop(nodeAt.Tag, DragDropEffects.Link);
                    }
                    this.m_DragStart = new Point(-100, -100);
                }
            }
        }

        private void tree_MouseUp(object sender, MouseEventArgs e)
        {
            this.m_DragStart = new Point(-100, -100);
        }

        private void UpdateButtons()
        {
            if (this.tree.SelectedNode == null)
            {
                foreach (ButtonBase base2 in this.panelLeft.Controls)
                {
                    base2.Enabled = false;
                }
                this.bVerify.Enabled = true;
            }
            else
            {
                this.bChoice.Enabled = (this.m_Selection is DialogSpeech) && (this.tree.SelectedNode.Nodes.Count < 8);
                this.bSpeech.Enabled = ((this.m_Selection is DialogChoice) && (this.tree.SelectedNode.Nodes.Count == 0)) || ((this.m_Selection is string) && ((this.m_Selection as string) == "D"));
                this.bInit.Enabled = (this.m_Selection is string) && ((this.m_Selection as string) == "R");
                bool flag = true;
                if (this.tree.SelectedNode.Tag is DialogChoice)
                {
                    if (this.tree.SelectedNode.Parent.Nodes.IndexOf(this.tree.SelectedNode) == 0)
                    {
                        flag = false;
                    }
                    else if (this.tree.SelectedNode.Parent.Nodes.Count == 0)
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
                this.bUp.Enabled = flag;
                bool flag2 = true;
                if (this.tree.SelectedNode.Tag is DialogChoice)
                {
                    if (this.tree.SelectedNode.Parent.Nodes.IndexOf(this.tree.SelectedNode) == (this.tree.SelectedNode.Parent.Nodes.Count - 1))
                    {
                        flag2 = false;
                    }
                    else if (this.tree.SelectedNode.Parent.Nodes.Count == 0)
                    {
                        flag2 = false;
                    }
                }
                else
                {
                    flag2 = false;
                }
                this.bDown.Enabled = flag2;
                this.bDelete.Enabled = ((this.m_Selection is DialogChoice) || (this.m_Selection is DialogSpeech)) || (this.m_Selection is DialogInit);
                if (this.m_Selection is DialogInit)
                {
                    TreeNode node = this.m_Nodes[(this.m_Selection as DialogInit).Speech] as TreeNode;
                    this.bTest.Enabled = (node != null) && (node.TreeView != null);
                }
                else
                {
                    this.bTest.Enabled = false;
                }
                this.bCopy.Enabled = this.m_Selection is DialogSpeech;
            }
        }

        private void UpdatePanel()
        {
            if (this.panelLow.Controls.Count > 0)
            {
                OutfitEditor editor = this.panelLow.Controls[0] as OutfitEditor;
                PropertiesEditor editor2 = this.panelLow.Controls[0] as PropertiesEditor;
                if (editor != null)
                {
                    editor.UpdateValues();
                }
                if (editor2 != null)
                {
                    editor2.UpdateValues();
                }
            }
            this.panelLow.Controls.Clear();
            if (this.m_Panel != null)
            {
                this.m_Panel.Dispose();
                this.m_Panel = null;
            }
            if ((this.m_Selection != null) && !(this.m_Selection is string))
            {
                if (this.m_Selection is Dialog)
                {
                    DialogOptionsEditor editor3 = new DialogOptionsEditor();
                    editor3.Location = new Point(0, 0);
                    editor3.Dialog = this.m_Selection as Dialog;
                    this.panelLow.Controls.Add(editor3);
                    editor3.Changed += new EventHandler(this.edit_Changed);
                    editor3.TitleChanged += new EventHandler(this.DialogTitleChanged);
                    this.m_Panel = editor3;
                }
                else if (this.m_Selection is DialogSpeech)
                {
                    SpeechEdit edit = new SpeechEdit();
                    edit.Location = new Point(0, 0);
                    edit.Speech = this.m_Selection as DialogSpeech;
                    this.panelLow.Controls.Add(edit);
                    edit.Changed += new EventHandler(this.edit_Changed);
                    edit.TitleChanged += new EventHandler(this.SpeechTitleChanged);
                    this.m_Panel = edit;
                }
                else if (this.m_Selection is DialogChoice)
                {
                    ChoiceEditor editor4 = new ChoiceEditor();
                    editor4.Location = new Point(0, 0);
                    editor4.Choice = this.m_Selection as DialogChoice;
                    this.panelLow.Controls.Add(editor4);
                    editor4.Changed += new EventHandler(this.edit_Changed);
                    editor4.TitleChanged += new EventHandler(this.ChoiceTitleChanged);
                    editor4.ViewChoice += new EventHandler(this.edit_ViewChoice);
                    this.m_Panel = editor4;
                }
                else if (this.m_Selection is DialogInit)
                {
                    InitEditor editor5 = new InitEditor();
                    editor5.Location = new Point(0, 0);
                    editor5.Init = this.m_Selection as DialogInit;
                    this.panelLow.Controls.Add(editor5);
                    editor5.Changed += new EventHandler(this.edit_Changed);
                    editor5.ViewSpeech += new EventHandler(this.edit_ViewSpeech);
                    this.m_Panel = editor5;
                }
                else if (this.m_Selection is Outfit)
                {
                    OutfitEditor editor6 = new OutfitEditor(this.m_Selection as Outfit);
                    editor6.Location = new Point(0, 0);
                    this.panelLow.Controls.Add(editor6);
                    editor6.Changed += new EventHandler(this.edit_Changed);
                }
                else if (this.m_Selection is NPCProps)
                {
                    PropertiesEditor editor7 = new PropertiesEditor(this.m_Selection as NPCProps);
                    editor7.Location = new Point(0, 0);
                    this.panelLow.Controls.Add(editor7);
                    editor7.Changed += new EventHandler(this.edit_Changed);
                }
            }
        }

        private Font NodeFont
        {
            get
            {
                return new Font(this.tree.Font.FontFamily.Name, this.tree.Font.Size, FontStyle.Bold);
            }
        }
    }
}

