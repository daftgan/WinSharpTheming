using AdvancedThemeManager.HookManager;
using AdvancedThemeManager.ThemeManager.Selection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdvancedThemeManager.ThemeManager
{
    public partial class DlgControlReferencer : Form
    {
        /// <summary>
        /// Base node
        /// </summary>
        private TreeNode _baseNode;

        /// <summary>
        /// Referenced types
        /// </summary>
        private List<Type> _types;

        /// <summary>
        /// Selection Form
        /// </summary>
        private SelectionForm _selectionForm;

        /// <summary>
        /// Constructor
        /// </summary>
        public DlgControlReferencer(List<Type> typesToReference)
        {
            InitializeComponent();
            _types = typesToReference;
            MouseHookComponent.MouseClick += MouseHookComponent_MouseClick;
        }

        /// <summary>
        /// Loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlgControlReferencer_Load(object sender, EventArgs e)
        {
            mouseHookComponent.EnableHook();
            List<TreeNode> treeNodes = CreateTreeNodes();
            _baseNode = CreateHierarchie(treeNodes);
            treeViewReferencer.Nodes.Add(_baseNode);
        }

        /// <summary>
        /// Create link between all treenode and return the base node
        /// </summary>
        private TreeNode CreateHierarchie(List<TreeNode> treeNodes)
        {
            TreeNode baseNode = null;
            foreach (TreeNode treenode in treeNodes)
            {
                if (((Type)treenode.Tag) == typeof(Control))
                {
                    baseNode = treenode;
                }
                Type inheritType = ((Type)treenode.Tag).BaseType;
                TreeNode nodeParent = treeNodes.Where(treeNode => (treeNode.Tag as Type) == inheritType).FirstOrDefault();
                if (nodeParent != null)
                {
                    nodeParent.Nodes.Add(treenode);
                }
            }

            return baseNode;
        }

        /// <summary>
        /// Creating treenode
        /// </summary>
        /// <returns>list of tree Nodes</returns>
        private List<TreeNode> CreateTreeNodes()
        {
            List<TreeNode> treeNodes = new List<TreeNode>();

            foreach (Type type in _types)
            {
                TreeNode node = new TreeNode(type.Name)
                {
                    Tag = type
                };
                if (type.IsAbstract)
                {
                    node.ForeColor = Color.Red;
                }
                treeNodes.Add(node);
            }

            return treeNodes;
        }

        private void treeViewReferencer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            groupBoxPreview.Controls.Clear();
            Type typeSelected = e.Node.Tag as Type;
            if (!typeSelected.IsAbstract)
            {
                Control instance = (Control)Activator.CreateInstance(typeSelected);
                instance.Dock = DockStyle.Fill;
                groupBoxPreview.Controls.Add(instance);
                propertyGridType.SelectedObject = instance;
            }
        }

        private void DlgControlReferencer_FormClosed(object sender, FormClosedEventArgs e)
        {
            mouseHookComponent.DisableHook();
        }

        /// <summary>
        /// Mouse hook, getting control
        /// </summary>
        private void MouseHookComponent_MouseClick(object sender, MouseHookComponent.MouseHookEventArgs arg)
        {
            _selectionForm?.Close();
            Control controlClicked = GetClickedControl(arg);
            if(controlClicked != null)
            {
                _selectionForm = new SelectionForm()
                {
                    StartPosition = FormStartPosition.Manual,
                    Bounds = new Rectangle(controlClicked.PointToScreen(Point.Empty) , new Size(100,0)),
                };
                _selectionForm.Height = 5;
                _selectionForm.Show();
                Console.WriteLine(_selectionForm.Size);
                propertyGridType.SelectedObject = controlClicked;
            }
        }

        /// <summary>
        /// Get where is the clicked button
        /// </summary>
        private Control GetClickedControl(MouseHookComponent.MouseHookEventArgs e)
        {
            Control controlFinal = null;
            foreach (Form form in Application.OpenForms)
            {
                if(form == this || 
                    form.GetType().FullName.Contains("PropertyGridView"))
                {
                    continue;
                }

                Point point = new Point(e.Point.x, e.Point.y);
                bool bConained = form.ClientRectangle.Contains(form.PointToClient(point));
                if (!bConained)
                {
                    continue;
                }
                controlFinal = GetControlRecurciveAtPos(form, point);
                break;
            }
            return controlFinal;
        }

        /// <summary>
        /// Get recursively whicht is the clicked control
        /// </summary>
        private Control GetControlRecurciveAtPos(Control control, Point point)
        {
            foreach (Control controlChild in control.Controls)
            {
                bool bContained = controlChild.ClientRectangle.Contains(controlChild.PointToClient(point));
                if (!bContained)
                {
                    continue;
                }
                return GetControlRecurciveAtPos(controlChild, point);
            }
            return control;
        }
    }
}
