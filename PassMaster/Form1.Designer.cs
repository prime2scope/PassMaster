using System.Drawing;

namespace PassMaster
{
    partial class PassMasterForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassMasterForm));
            this.ColorStatusPanel = new System.Windows.Forms.Panel();
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.MainPanel1 = new System.Windows.Forms.Panel();
            this.ClearButton = new System.Windows.Forms.Button();
            this.OptionsButton = new System.Windows.Forms.Button();
            this.OptionPanel = new System.Windows.Forms.Panel();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ShowAllPWCheckBox = new System.Windows.Forms.CheckBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.PWListGrid = new System.Windows.Forms.DataGridView();
            this.Website = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PW_Prompt = new System.Windows.Forms.Label();
            this.MasterPWbox = new System.Windows.Forms.TextBox();
            this.ColorStatusPanel.SuspendLayout();
            this.LoginPanel.SuspendLayout();
            this.MainPanel1.SuspendLayout();
            this.OptionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PWListGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ColorStatusPanel
            // 
            this.ColorStatusPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorStatusPanel.BackColor = System.Drawing.Color.Red;
            this.ColorStatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ColorStatusPanel.Controls.Add(this.LoginPanel);
            this.ColorStatusPanel.Location = new System.Drawing.Point(16, 16);
            this.ColorStatusPanel.Margin = new System.Windows.Forms.Padding(7);
            this.ColorStatusPanel.Name = "ColorStatusPanel";
            this.ColorStatusPanel.Size = new System.Drawing.Size(882, 530);
            this.ColorStatusPanel.TabIndex = 0;
            // 
            // LoginPanel
            // 
            this.LoginPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LoginPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoginPanel.Controls.Add(this.MainPanel1);
            this.LoginPanel.Controls.Add(this.PW_Prompt);
            this.LoginPanel.Controls.Add(this.MasterPWbox);
            this.LoginPanel.Location = new System.Drawing.Point(6, 6);
            this.LoginPanel.Margin = new System.Windows.Forms.Padding(6);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(866, 514);
            this.LoginPanel.TabIndex = 0;
            // 
            // MainPanel1
            // 
            this.MainPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.MainPanel1.Controls.Add(this.ClearButton);
            this.MainPanel1.Controls.Add(this.OptionsButton);
            this.MainPanel1.Controls.Add(this.OptionPanel);
            this.MainPanel1.Controls.Add(this.SaveButton);
            this.MainPanel1.Controls.Add(this.ExitButton);
            this.MainPanel1.Controls.Add(this.ShowAllPWCheckBox);
            this.MainPanel1.Controls.Add(this.SearchLabel);
            this.MainPanel1.Controls.Add(this.SearchTextBox);
            this.MainPanel1.Controls.Add(this.PWListGrid);
            this.MainPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel1.Location = new System.Drawing.Point(0, 0);
            this.MainPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel1.Name = "MainPanel1";
            this.MainPanel1.Size = new System.Drawing.Size(864, 512);
            this.MainPanel1.TabIndex = 2;
            this.MainPanel1.Visible = false;
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.BackColor = System.Drawing.Color.White;
            this.ClearButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ClearButton.FlatAppearance.BorderSize = 0;
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearButton.Location = new System.Drawing.Point(616, 20);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(0);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(30, 25);
            this.ClearButton.TabIndex = 14;
            this.ClearButton.TabStop = false;
            this.ClearButton.Text = "X";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Visible = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // OptionsButton
            // 
            this.OptionsButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OptionsButton.BackColor = System.Drawing.Color.Silver;
            this.OptionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptionsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptionsButton.Location = new System.Drawing.Point(380, 451);
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.Size = new System.Drawing.Size(108, 48);
            this.OptionsButton.TabIndex = 14;
            this.OptionsButton.Text = "Options";
            this.OptionsButton.UseVisualStyleBackColor = false;
            this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // OptionPanel
            // 
            this.OptionPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OptionPanel.Controls.Add(this.DeleteButton);
            this.OptionPanel.Controls.Add(this.AddButton);
            this.OptionPanel.Controls.Add(this.EditButton);
            this.OptionPanel.Location = new System.Drawing.Point(200, 365);
            this.OptionPanel.Margin = new System.Windows.Forms.Padding(200);
            this.OptionPanel.Name = "OptionPanel";
            this.OptionPanel.Size = new System.Drawing.Size(464, 74);
            this.OptionPanel.TabIndex = 13;
            this.OptionPanel.Visible = false;
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.Color.Silver;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.Location = new System.Drawing.Point(325, 15);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(15);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(124, 44);
            this.DeleteButton.TabIndex = 13;
            this.DeleteButton.Text = "Delete Row";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.Silver;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.Location = new System.Drawing.Point(17, 15);
            this.AddButton.Margin = new System.Windows.Forms.Padding(15);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(124, 44);
            this.AddButton.TabIndex = 11;
            this.AddButton.Text = "Add New+";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.BackColor = System.Drawing.Color.Silver;
            this.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditButton.Location = new System.Drawing.Point(171, 15);
            this.EditButton.Margin = new System.Windows.Forms.Padding(15);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(124, 44);
            this.EditButton.TabIndex = 12;
            this.EditButton.Text = "Edit Cell";
            this.EditButton.UseVisualStyleBackColor = false;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(726, 451);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(108, 48);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Location = new System.Drawing.Point(30, 451);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(108, 48);
            this.ExitButton.TabIndex = 9;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ShowAllPWCheckBox
            // 
            this.ShowAllPWCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowAllPWCheckBox.AutoSize = true;
            this.ShowAllPWCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowAllPWCheckBox.Location = new System.Drawing.Point(661, 20);
            this.ShowAllPWCheckBox.Name = "ShowAllPWCheckBox";
            this.ShowAllPWCheckBox.Size = new System.Drawing.Size(173, 28);
            this.ShowAllPWCheckBox.TabIndex = 8;
            this.ShowAllPWCheckBox.Text = "Show Passwords";
            this.ShowAllPWCheckBox.UseVisualStyleBackColor = true;
            this.ShowAllPWCheckBox.CheckedChanged += new System.EventHandler(this.ShowAllPWCheckBox_CheckedChanged);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchLabel.Location = new System.Drawing.Point(30, 20);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(80, 24);
            this.SearchLabel.TabIndex = 7;
            this.SearchLabel.Text = "Search :";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTextBox.Location = new System.Drawing.Point(113, 18);
            this.SearchTextBox.Margin = new System.Windows.Forms.Padding(1, 3, 10, 3);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(535, 29);
            this.SearchTextBox.TabIndex = 6;
            this.SearchTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchTextBox_KeyUp);
            // 
            // PWListGrid
            // 
            this.PWListGrid.AllowUserToAddRows = false;
            this.PWListGrid.AllowUserToDeleteRows = false;
            this.PWListGrid.AllowUserToResizeColumns = false;
            this.PWListGrid.AllowUserToResizeRows = false;
            this.PWListGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PWListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PWListGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.PWListGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PWListGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PWListGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PWListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PWListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Website,
            this.Username,
            this.Password});
            this.PWListGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PWListGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.PWListGrid.Location = new System.Drawing.Point(30, 64);
            this.PWListGrid.Margin = new System.Windows.Forms.Padding(30);
            this.PWListGrid.MultiSelect = false;
            this.PWListGrid.Name = "PWListGrid";
            this.PWListGrid.RowHeadersVisible = false;
            this.PWListGrid.RowHeadersWidth = 30;
            this.PWListGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PWListGrid.RowTemplate.Height = 30;
            this.PWListGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PWListGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PWListGrid.Size = new System.Drawing.Size(804, 375);
            this.PWListGrid.TabIndex = 5;
            this.PWListGrid.TabStop = false;
            this.PWListGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.PWListGrid_CellEndEdit);
            this.PWListGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.PWListGrid_CellFormatting);
            this.PWListGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.PWListGrid_CellValidating);
            this.PWListGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.PWListGrid_EditingControlShowing);
            this.PWListGrid.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.PWListGrid_RowValidated);
            this.PWListGrid.SelectionChanged += new System.EventHandler(this.PWListGrid_SelectionChanged);
            this.PWListGrid.Click += new System.EventHandler(this.PWListGrid_Click);
            this.PWListGrid.MouseLeave += new System.EventHandler(this.PWListGrid_MouseLeave);
            this.PWListGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PWListGrid_MouseMove);
            // 
            // Website
            // 
            this.Website.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Website.FillWeight = 33F;
            this.Website.HeaderText = "Website";
            this.Website.MaxInputLength = 256;
            this.Website.MinimumWidth = 20;
            this.Website.Name = "Website";
            this.Website.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Username
            // 
            this.Username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Username.FillWeight = 33F;
            this.Username.HeaderText = "Username";
            this.Username.MaxInputLength = 256;
            this.Username.MinimumWidth = 20;
            this.Username.Name = "Username";
            this.Username.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Password
            // 
            this.Password.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Password.FillWeight = 33F;
            this.Password.HeaderText = "Password";
            this.Password.MaxInputLength = 256;
            this.Password.MinimumWidth = 20;
            this.Password.Name = "Password";
            this.Password.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Password.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PW_Prompt
            // 
            this.PW_Prompt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PW_Prompt.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PW_Prompt.Location = new System.Drawing.Point(20, 201);
            this.PW_Prompt.Margin = new System.Windows.Forms.Padding(20);
            this.PW_Prompt.Name = "PW_Prompt";
            this.PW_Prompt.Size = new System.Drawing.Size(824, 35);
            this.PW_Prompt.TabIndex = 1;
            this.PW_Prompt.Text = "Enter Master Password";
            this.PW_Prompt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MasterPWbox
            // 
            this.MasterPWbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MasterPWbox.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MasterPWbox.Location = new System.Drawing.Point(319, 239);
            this.MasterPWbox.Margin = new System.Windows.Forms.Padding(200, 170, 200, 200);
            this.MasterPWbox.Name = "MasterPWbox";
            this.MasterPWbox.PasswordChar = '*';
            this.MasterPWbox.Size = new System.Drawing.Size(224, 29);
            this.MasterPWbox.TabIndex = 0;
            this.MasterPWbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterPWbox_KeyDown);
            // 
            // PassMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 562);
            this.Controls.Add(this.ColorStatusPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(930, 600);
            this.Name = "PassMasterForm";
            this.Text = "PassMaster";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PassMasterForm_FormClosing);
            this.Load += new System.EventHandler(this.PassMasterForm_Load);
            this.ColorStatusPanel.ResumeLayout(false);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.MainPanel1.ResumeLayout(false);
            this.MainPanel1.PerformLayout();
            this.OptionPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PWListGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ColorStatusPanel;
        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.TextBox MasterPWbox;
        private System.Windows.Forms.Label PW_Prompt;
        private System.Windows.Forms.Panel MainPanel1;
        private System.Windows.Forms.DataGridView PWListGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Website;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.CheckBox ShowAllPWCheckBox;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button OptionsButton;
        private System.Windows.Forms.Panel OptionPanel;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ClearButton;
    }


}

