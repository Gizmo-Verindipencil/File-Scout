namespace FileScout.UI
{
    partial class ChoosingScoutForm
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
            this.MethodDataGridView = new System.Windows.Forms.DataGridView();
            this.ScoutLabel = new System.Windows.Forms.Label();
            this.MethodLabel = new System.Windows.Forms.Label();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.ScoutDataGridView = new System.Windows.Forms.DataGridView();
            this.NumberOfMethodsLabel = new System.Windows.Forms.Label();
            this.TargetDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.SelectButton = new System.Windows.Forms.Button();
            this.TargetDirectoryLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MethodDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScoutDataGridView)).BeginInit();
            this.SuspendLayout();
            //
            // MethodDataGridView
            //
            this.MethodDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MethodDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MethodDataGridView.Location = new System.Drawing.Point(626, 53);
            this.MethodDataGridView.MultiSelect = false;
            this.MethodDataGridView.Name = "MethodDataGridView";
            this.MethodDataGridView.RowHeadersWidth = 5;
            this.MethodDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.MethodDataGridView.RowTemplate.Height = 27;
            this.MethodDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MethodDataGridView.Size = new System.Drawing.Size(560, 400);
            this.MethodDataGridView.TabIndex = 5;
            //
            // ScoutLabel
            //
            this.ScoutLabel.AutoSize = true;
            this.ScoutLabel.Location = new System.Drawing.Point(21, 21);
            this.ScoutLabel.Name = "ScoutLabel";
            this.ScoutLabel.Size = new System.Drawing.Size(44, 18);
            this.ScoutLabel.TabIndex = 1;
            this.ScoutLabel.Text = "斥候";
            //
            // MethodLabel
            //
            this.MethodLabel.AutoSize = true;
            this.MethodLabel.Location = new System.Drawing.Point(623, 21);
            this.MethodLabel.Name = "MethodLabel";
            this.MethodLabel.Size = new System.Drawing.Size(44, 18);
            this.MethodLabel.TabIndex = 3;
            this.MethodLabel.Text = "項目";
            //
            // ExecuteButton
            //
            this.ExecuteButton.Location = new System.Drawing.Point(1111, 481);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(75, 34);
            this.ExecuteButton.TabIndex = 9;
            this.ExecuteButton.Text = "実行";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            //
            // ScoutDataGridView
            //
            this.ScoutDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ScoutDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScoutDataGridView.Location = new System.Drawing.Point(24, 53);
            this.ScoutDataGridView.MultiSelect = false;
            this.ScoutDataGridView.Name = "ScoutDataGridView";
            this.ScoutDataGridView.ReadOnly = true;
            this.ScoutDataGridView.RowHeadersWidth = 5;
            this.ScoutDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ScoutDataGridView.RowTemplate.Height = 27;
            this.ScoutDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScoutDataGridView.Size = new System.Drawing.Size(560, 400);
            this.ScoutDataGridView.TabIndex = 2;
            this.ScoutDataGridView.SelectionChanged += new System.EventHandler(this.ScoutDataGridView_SelectionChanged);
            //
            // NumberOfMethodsLabel
            //
            this.NumberOfMethodsLabel.AutoSize = true;
            this.NumberOfMethodsLabel.Location = new System.Drawing.Point(673, 21);
            this.NumberOfMethodsLabel.Name = "NumberOfMethodsLabel";
            this.NumberOfMethodsLabel.Size = new System.Drawing.Size(27, 18);
            this.NumberOfMethodsLabel.TabIndex = 4;
            this.NumberOfMethodsLabel.Text = "(0)";
            //
            // TargetDirectoryTextBox
            //
            this.TargetDirectoryTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.TargetDirectoryTextBox.Location = new System.Drawing.Point(153, 481);
            this.TargetDirectoryTextBox.Multiline = true;
            this.TargetDirectoryTextBox.Name = "TargetDirectoryTextBox";
            this.TargetDirectoryTextBox.ReadOnly = true;
            this.TargetDirectoryTextBox.Size = new System.Drawing.Size(857, 34);
            this.TargetDirectoryTextBox.TabIndex = 7;
            //
            // SelectButton
            //
            this.SelectButton.Location = new System.Drawing.Point(1030, 481);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(75, 34);
            this.SelectButton.TabIndex = 8;
            this.SelectButton.Text = "選択";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            //
            // TargetDirectoryLabel
            //
            this.TargetDirectoryLabel.AutoSize = true;
            this.TargetDirectoryLabel.Location = new System.Drawing.Point(21, 489);
            this.TargetDirectoryLabel.Name = "TargetDirectoryLabel";
            this.TargetDirectoryLabel.Size = new System.Drawing.Size(119, 18);
            this.TargetDirectoryLabel.TabIndex = 6;
            this.TargetDirectoryLabel.Text = "対象ディレクトリ";
            //
            // ChoosingScoutForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 539);
            this.Controls.Add(this.TargetDirectoryLabel);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.TargetDirectoryTextBox);
            this.Controls.Add(this.NumberOfMethodsLabel);
            this.Controls.Add(this.ScoutDataGridView);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.MethodLabel);
            this.Controls.Add(this.ScoutLabel);
            this.Controls.Add(this.MethodDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChoosingScoutForm";
            this.Opacity = 0.95D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FileScout";
            this.Load += new System.EventHandler(this.ChoosingScout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MethodDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScoutDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView MethodDataGridView;
        private System.Windows.Forms.Label ScoutLabel;
        private System.Windows.Forms.Label MethodLabel;
        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.DataGridView ScoutDataGridView;
        private System.Windows.Forms.Label NumberOfMethodsLabel;
        private System.Windows.Forms.TextBox TargetDirectoryTextBox;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Label TargetDirectoryLabel;
    }
}