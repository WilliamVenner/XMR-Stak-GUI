using System;

namespace XMR_Stak_GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ExitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.StopXMRStak = new System.Windows.Forms.ToolStripMenuItem();
            this.GPUConfigsDropdown = new System.Windows.Forms.ToolStripDropDownButton();
            this.SetXMRStakLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigFileLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.MinerSelector = new System.Windows.Forms.ToolStripMenuItem();
            this.CPUSelector = new System.Windows.Forms.ToolStripMenuItem();
            this.AMDSelector = new System.Windows.Forms.ToolStripMenuItem();
            this.NVIDIASelector = new System.Windows.Forms.ToolStripMenuItem();
            this.CurrencySelector = new System.Windows.Forms.ToolStripMenuItem();
            this.MoneroSelector = new System.Windows.Forms.ToolStripMenuItem();
            this.AeonSelector = new System.Windows.Forms.ToolStripMenuItem();
            this.AddConfigButton = new System.Windows.Forms.ToolStripMenuItem();
            this.NoConfigsIndicator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.CheckForUpdates_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.GitHub = new System.Windows.Forms.ToolStripMenuItem();
            this.Wiki = new System.Windows.Forms.ToolStripMenuItem();
            this.madeByBillyVennerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XMRStakLocation = new System.Windows.Forms.OpenFileDialog();
            this.AddMinerConfigFile = new System.Windows.Forms.OpenFileDialog();
            this.OpenConfigFile = new System.Windows.Forms.OpenFileDialog();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.OutputBox);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(727, 291);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(727, 316);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // OutputBox
            // 
            this.OutputBox.BackColor = System.Drawing.Color.Black;
            this.OutputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputBox.ForeColor = System.Drawing.Color.White;
            this.OutputBox.Location = new System.Drawing.Point(0, 0);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputBox.ShortcutsEnabled = false;
            this.OutputBox.Size = new System.Drawing.Size(727, 291);
            this.OutputBox.TabIndex = 0;
            this.OutputBox.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.GPUConfigsDropdown,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(142, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitButton,
            this.StopXMRStak});
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripButton1.Text = "File";
            // 
            // ExitButton
            // 
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(153, 22);
            this.ExitButton.Text = "Exit";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // StopXMRStak
            // 
            this.StopXMRStak.Enabled = false;
            this.StopXMRStak.Name = "StopXMRStak";
            this.StopXMRStak.Size = new System.Drawing.Size(153, 22);
            this.StopXMRStak.Text = "Stop XMR-Stak";
            this.StopXMRStak.Click += new System.EventHandler(this.StopXMRStak_Click);
            // 
            // GPUConfigsDropdown
            // 
            this.GPUConfigsDropdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.GPUConfigsDropdown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetXMRStakLocation,
            this.ConfigFileLocation,
            this.MinerSelector,
            this.CurrencySelector,
            this.AddConfigButton,
            this.NoConfigsIndicator});
            this.GPUConfigsDropdown.Image = ((System.Drawing.Image)(resources.GetObject("GPUConfigsDropdown.Image")));
            this.GPUConfigsDropdown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GPUConfigsDropdown.Name = "GPUConfigsDropdown";
            this.GPUConfigsDropdown.Size = new System.Drawing.Size(56, 22);
            this.GPUConfigsDropdown.Text = "Config";
            // 
            // SetXMRStakLocation
            // 
            this.SetXMRStakLocation.Name = "SetXMRStakLocation";
            this.SetXMRStakLocation.Size = new System.Drawing.Size(208, 22);
            this.SetXMRStakLocation.Text = "Set XMR-Stak Location...";
            this.SetXMRStakLocation.Click += new System.EventHandler(this.XMRStakLocation_Click);
            // 
            // ConfigFileLocation
            // 
            this.ConfigFileLocation.Name = "ConfigFileLocation";
            this.ConfigFileLocation.Size = new System.Drawing.Size(208, 22);
            this.ConfigFileLocation.Text = "Set Config File Location...";
            this.ConfigFileLocation.Click += new System.EventHandler(this.ConfigFileLocation_Click);
            // 
            // MinerSelector
            // 
            this.MinerSelector.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CPUSelector,
            this.AMDSelector,
            this.NVIDIASelector});
            this.MinerSelector.Name = "MinerSelector";
            this.MinerSelector.Size = new System.Drawing.Size(208, 22);
            this.MinerSelector.Text = "Miner Config Type";
            // 
            // CPUSelector
            // 
            this.CPUSelector.Name = "CPUSelector";
            this.CPUSelector.Size = new System.Drawing.Size(112, 22);
            this.CPUSelector.Text = "CPU";
            this.CPUSelector.Click += new System.EventHandler(this.CPUSelector_Click);
            // 
            // AMDSelector
            // 
            this.AMDSelector.Name = "AMDSelector";
            this.AMDSelector.Size = new System.Drawing.Size(112, 22);
            this.AMDSelector.Text = "AMD";
            this.AMDSelector.Click += new System.EventHandler(this.AMDSelector_Click);
            // 
            // NVIDIASelector
            // 
            this.NVIDIASelector.Name = "NVIDIASelector";
            this.NVIDIASelector.Size = new System.Drawing.Size(112, 22);
            this.NVIDIASelector.Text = "NVIDIA";
            this.NVIDIASelector.Click += new System.EventHandler(this.NVIDIASelector_Click);
            // 
            // CurrencySelector
            // 
            this.CurrencySelector.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoneroSelector,
            this.AeonSelector});
            this.CurrencySelector.Name = "CurrencySelector";
            this.CurrencySelector.Size = new System.Drawing.Size(208, 22);
            this.CurrencySelector.Text = "Currency";
            // 
            // MoneroSelector
            // 
            this.MoneroSelector.Name = "MoneroSelector";
            this.MoneroSelector.Size = new System.Drawing.Size(116, 22);
            this.MoneroSelector.Text = "Monero";
            this.MoneroSelector.Click += new System.EventHandler(this.MoneroSelector_Click);
            // 
            // AeonSelector
            // 
            this.AeonSelector.Name = "AeonSelector";
            this.AeonSelector.Size = new System.Drawing.Size(116, 22);
            this.AeonSelector.Text = "Aeon";
            this.AeonSelector.Click += new System.EventHandler(this.AeonSelector_Click);
            // 
            // AddConfigButton
            // 
            this.AddConfigButton.Name = "AddConfigButton";
            this.AddConfigButton.Size = new System.Drawing.Size(208, 22);
            this.AddConfigButton.Text = "Add Miner Config(s)...";
            this.AddConfigButton.Click += new System.EventHandler(this.AddConfigButton_Click);
            // 
            // NoConfigsIndicator
            // 
            this.NoConfigsIndicator.Enabled = false;
            this.NoConfigsIndicator.Name = "NoConfigsIndicator";
            this.NoConfigsIndicator.Size = new System.Drawing.Size(208, 22);
            this.NoConfigsIndicator.Text = "< no configs >";
            this.NoConfigsIndicator.Visible = false;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CheckForUpdates_Item,
            this.GitHub,
            this.Wiki,
            this.madeByBillyVennerToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(45, 22);
            this.toolStripDropDownButton1.Text = "Help";
            // 
            // CheckForUpdates_Item
            // 
            this.CheckForUpdates_Item.Name = "CheckForUpdates_Item";
            this.CheckForUpdates_Item.Size = new System.Drawing.Size(184, 22);
            this.CheckForUpdates_Item.Text = "Check for updates...";
            this.CheckForUpdates_Item.Click += new System.EventHandler(this.CheckForUpdates_Click);
            // 
            // GitHub
            // 
            this.GitHub.Name = "GitHub";
            this.GitHub.Size = new System.Drawing.Size(184, 22);
            this.GitHub.Text = "GitHub";
            this.GitHub.Click += new System.EventHandler(this.GitHub_Click);
            // 
            // Wiki
            // 
            this.Wiki.Name = "Wiki";
            this.Wiki.Size = new System.Drawing.Size(184, 22);
            this.Wiki.Text = "Wiki";
            this.Wiki.Click += new System.EventHandler(this.Wiki_Click);
            // 
            // madeByBillyVennerToolStripMenuItem
            // 
            this.madeByBillyVennerToolStripMenuItem.Enabled = false;
            this.madeByBillyVennerToolStripMenuItem.Name = "madeByBillyVennerToolStripMenuItem";
            this.madeByBillyVennerToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.madeByBillyVennerToolStripMenuItem.Text = "Made by Billy Venner";
            // 
            // XMRStakLocation
            // 
            this.XMRStakLocation.DefaultExt = "exe";
            this.XMRStakLocation.FileName = "XMRStakLocation";
            this.XMRStakLocation.Filter = "xmr-stak.exe|xmr-stak.exe";
            this.XMRStakLocation.FileOk += new System.ComponentModel.CancelEventHandler(this.XMRStakLocation_FileOk);
            // 
            // AddMinerConfigFile
            // 
            this.AddMinerConfigFile.DefaultExt = "txt";
            this.AddMinerConfigFile.FileName = "AddMinerConfigFile";
            this.AddMinerConfigFile.Filter = "cpu.txt, amd.txt, nvidia.txt|*.txt";
            this.AddMinerConfigFile.Multiselect = true;
            this.AddMinerConfigFile.FileOk += new System.ComponentModel.CancelEventHandler(this.AddConfigFile_FileOk);
            // 
            // OpenConfigFile
            // 
            this.OpenConfigFile.FileName = "OpenConfigFile";
            this.OpenConfigFile.Filter = "config.txt|config.txt";
            this.OpenConfigFile.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenConfigFile_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 316);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "XMR-Stak GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem ExitButton;
        private System.Windows.Forms.OpenFileDialog XMRStakLocation;
        private System.Windows.Forms.ToolStripDropDownButton GPUConfigsDropdown;
        private System.Windows.Forms.OpenFileDialog AddMinerConfigFile;
        private System.Windows.Forms.ToolStripMenuItem SetXMRStakLocation;
        private System.Windows.Forms.ToolStripMenuItem AddConfigButton;
        private System.Windows.Forms.ToolStripMenuItem NoConfigsIndicator;
        private System.Windows.Forms.ToolStripMenuItem CurrencySelector;
        private System.Windows.Forms.ToolStripMenuItem MoneroSelector;
        private System.Windows.Forms.ToolStripMenuItem AeonSelector;
        private System.Windows.Forms.ToolStripMenuItem MinerSelector;
        private System.Windows.Forms.ToolStripMenuItem CPUSelector;
        private System.Windows.Forms.ToolStripMenuItem AMDSelector;
        private System.Windows.Forms.ToolStripMenuItem NVIDIASelector;
        private System.Windows.Forms.ToolStripMenuItem StopXMRStak;
        private System.Windows.Forms.ToolStripMenuItem ConfigFileLocation;
        private System.Windows.Forms.OpenFileDialog OpenConfigFile;
        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem CheckForUpdates_Item;
        private System.Windows.Forms.ToolStripMenuItem Wiki;
        private System.Windows.Forms.ToolStripMenuItem madeByBillyVennerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GitHub;
    }
}

