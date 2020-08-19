namespace AdvancedThemeManager.ThemeManager
{
    partial class DlgControlReferencer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeViewReferencer = new System.Windows.Forms.TreeView();
            this.propertyGridType = new System.Windows.Forms.PropertyGrid();
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.mouseHookComponent = new AdvancedThemeManager.HookManager.MouseHookComponent(this.components);
            this.SuspendLayout();
            // 
            // treeViewReferencer
            // 
            this.treeViewReferencer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewReferencer.Location = new System.Drawing.Point(0, 0);
            this.treeViewReferencer.Name = "treeViewReferencer";
            this.treeViewReferencer.Size = new System.Drawing.Size(570, 350);
            this.treeViewReferencer.TabIndex = 0;
            this.treeViewReferencer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewReferencer_AfterSelect);
            // 
            // propertyGridType
            // 
            this.propertyGridType.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGridType.Location = new System.Drawing.Point(570, 0);
            this.propertyGridType.Name = "propertyGridType";
            this.propertyGridType.Size = new System.Drawing.Size(230, 350);
            this.propertyGridType.TabIndex = 1;
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxPreview.Location = new System.Drawing.Point(0, 350);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Size = new System.Drawing.Size(800, 100);
            this.groupBoxPreview.TabIndex = 2;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "Preview Component";
            // 
            // DlgControlReferencer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.treeViewReferencer);
            this.Controls.Add(this.propertyGridType);
            this.Controls.Add(this.groupBoxPreview);
            this.Name = "DlgControlReferencer";
            this.Text = "Controls Referencer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DlgControlReferencer_FormClosed);
            this.Load += new System.EventHandler(this.DlgControlReferencer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewReferencer;
        private System.Windows.Forms.PropertyGrid propertyGridType;
        private System.Windows.Forms.GroupBox groupBoxPreview;
        private HookManager.MouseHookComponent mouseHookComponent;
    }
}