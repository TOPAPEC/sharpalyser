
namespace WindowsFormsApp5
{
    partial class FormMain
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
            this.textBoxCsvPath = new System.Windows.Forms.TextBox();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.buttonLoadCsv = new System.Windows.Forms.Button();
            this.flowLayoutPanelDataGrid = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemHistogram = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHistCol = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPlotXY = new System.Windows.Forms.ToolStripMenuItem();
            this.meanValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.varToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxInput.SuspendLayout();
            this.flowLayoutPanelDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStripDataGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxCsvPath
            // 
            this.textBoxCsvPath.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxCsvPath.Location = new System.Drawing.Point(3, 16);
            this.textBoxCsvPath.Name = "textBoxCsvPath";
            this.textBoxCsvPath.Size = new System.Drawing.Size(690, 20);
            this.textBoxCsvPath.TabIndex = 0;
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.buttonLoadCsv);
            this.groupBoxInput.Controls.Add(this.textBoxCsvPath);
            this.groupBoxInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxInput.Location = new System.Drawing.Point(0, 0);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(784, 40);
            this.groupBoxInput.TabIndex = 1;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Open Csv";
            // 
            // buttonLoadCsv
            // 
            this.buttonLoadCsv.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonLoadCsv.Location = new System.Drawing.Point(694, 16);
            this.buttonLoadCsv.Name = "buttonLoadCsv";
            this.buttonLoadCsv.Size = new System.Drawing.Size(87, 21);
            this.buttonLoadCsv.TabIndex = 1;
            this.buttonLoadCsv.Text = "Load Csv";
            this.buttonLoadCsv.UseVisualStyleBackColor = true;
            this.buttonLoadCsv.Click += new System.EventHandler(this.ButtonLoadCsv_Click);
            // 
            // flowLayoutPanelDataGrid
            // 
            this.flowLayoutPanelDataGrid.Controls.Add(this.dataGridView);
            this.flowLayoutPanelDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelDataGrid.Location = new System.Drawing.Point(0, 40);
            this.flowLayoutPanelDataGrid.Name = "flowLayoutPanelDataGrid";
            this.flowLayoutPanelDataGrid.Size = new System.Drawing.Size(784, 721);
            this.flowLayoutPanelDataGrid.TabIndex = 3;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ContextMenuStrip = this.contextMenuStripDataGrid;
            this.dataGridView.Location = new System.Drawing.Point(3, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView.Size = new System.Drawing.Size(778, 718);
            this.dataGridView.TabIndex = 0;
            // 
            // contextMenuStripDataGrid
            // 
            this.contextMenuStripDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHistogram,
            this.toolStripMenuItemHistCol,
            this.toolStripMenuItemPlotXY,
            this.meanValueToolStripMenuItem,
            this.medianToolStripMenuItem,
            this.sdToolStripMenuItem,
            this.varToolStripMenuItem});
            this.contextMenuStripDataGrid.Name = "contextMenuStrip1";
            this.contextMenuStripDataGrid.Size = new System.Drawing.Size(239, 180);
            // 
            // toolStripMenuItemHistogram
            // 
            this.toolStripMenuItemHistogram.Name = "toolStripMenuItemHistogram";
            this.toolStripMenuItemHistogram.Size = new System.Drawing.Size(238, 22);
            this.toolStripMenuItemHistogram.Text = "Show histogram rectangle";
            this.toolStripMenuItemHistogram.Click += new System.EventHandler(this.ToolStripMenuItemHistogramRectangle_Click);
            // 
            // toolStripMenuItemHistCol
            // 
            this.toolStripMenuItemHistCol.Name = "toolStripMenuItemHistCol";
            this.toolStripMenuItemHistCol.Size = new System.Drawing.Size(238, 22);
            this.toolStripMenuItemHistCol.Text = "Show histogram single column";
            this.toolStripMenuItemHistCol.Click += new System.EventHandler(this.ToolStripMenuItemHistCol_Click);
            // 
            // toolStripMenuItemPlotXY
            // 
            this.toolStripMenuItemPlotXY.Name = "toolStripMenuItemPlotXY";
            this.toolStripMenuItemPlotXY.Size = new System.Drawing.Size(238, 22);
            this.toolStripMenuItemPlotXY.Text = "Make plot X->Y";
            this.toolStripMenuItemPlotXY.Click += new System.EventHandler(this.ToolStripMenuItemPlotXY_Click);
            // 
            // meanValueToolStripMenuItem
            // 
            this.meanValueToolStripMenuItem.Name = "meanValueToolStripMenuItem";
            this.meanValueToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.meanValueToolStripMenuItem.Text = "Mean value";
            this.meanValueToolStripMenuItem.Click += new System.EventHandler(this.MeanValueToolStripMenuItem_Click);
            // 
            // medianToolStripMenuItem
            // 
            this.medianToolStripMenuItem.Name = "medianToolStripMenuItem";
            this.medianToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.medianToolStripMenuItem.Text = "Median";
            this.medianToolStripMenuItem.Click += new System.EventHandler(this.MedianToolStripMenuItem_Click);
            // 
            // sdToolStripMenuItem
            // 
            this.sdToolStripMenuItem.Name = "sdToolStripMenuItem";
            this.sdToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.sdToolStripMenuItem.Text = "Standart deviation";
            this.sdToolStripMenuItem.Click += new System.EventHandler(this.SdToolStripMenuItem_Click);
            // 
            // varToolStripMenuItem
            // 
            this.varToolStripMenuItem.Name = "varToolStripMenuItem";
            this.varToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.varToolStripMenuItem.Text = "Varience";
            this.varToolStripMenuItem.Click += new System.EventHandler(this.VarienceToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.flowLayoutPanelDataGrid);
            this.Controls.Add(this.groupBoxInput);
            this.MaximumSize = new System.Drawing.Size(800, 800);
            this.MinimumSize = new System.Drawing.Size(800, 800);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.flowLayoutPanelDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStripDataGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCsvPath;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.Button buttonLoadCsv;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDataGrid;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGrid;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHistogram;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHistCol;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlotXY;
        private System.Windows.Forms.ToolStripMenuItem meanValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem varToolStripMenuItem;
    }
}

