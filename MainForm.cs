using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Windows.Forms;

namespace XMR_Stak_GUI
{
    public partial class MainForm : Form
    {
        public string version = "v1";

        private string select_config = "Please select a miner config.\r\nTo capture the output of xmr-stak, you must set \"flush_stdout\" to true in your config file.";

        Process XMRStakProcess;

        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }

        public bool CheckForUpdates(bool verbose = false)
        {
            CheckForUpdates_Item.Enabled = false;
            HttpWebRequest version_request = (HttpWebRequest)WebRequest.Create("https://cdn.rawgit.com/WilliamVenner/XMR-Stak-GUI/master/VERSION");
            try
            {
                WebResponse response = version_request.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    string latest_version = stream.ReadToEnd().Trim();
                    if (latest_version != version)
                    {
                        this.Text = "XMR-Stak GUI " + version + " (outdated!)";
                        DialogResult open_github = MessageBox.Show(this, "You are running an outdated version of XMR-Stak GUI!\nWould you like to open the GitHub releases page?", "Outdated", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (open_github == DialogResult.Yes)
                        {
                            Process.Start("https://github.com/WilliamVenner/XMR-Stak-GUI/releases");
                        }
                        CheckForUpdates_Item.Enabled = true;
                    } else if (verbose == true)
                    {
                        MessageBox.Show(this, "XMR-Stak GUI is up-to-date!", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CheckForUpdates_Item.Enabled = true;
                    } else
                    {
                        CheckForUpdates_Item.Enabled = true;
                    }
                }
            } catch (System.Net.WebException e)
            {
                OutputBox.AppendText("\r\n\r\nWhile checking for updates, XMR-Stak GUI encountered this HTTP error:\r\n" + e.Message);
                CheckForUpdates_Item.Enabled = true;
            }
            return false;
        }

        public MainForm()
        {
            if (!IsAdministrator())
            {
                MessageBox.Show(this, "You must be running this as an administrator!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new MethodInvoker(this.Close));
            } else
            {
                InitializeComponent();
                UpdateConfigDropdown();

                OutputBox.Text = select_config;
                this.Text = "XMR-Stak GUI " + version;

                Process[] xmr_staks = Process.GetProcessesByName("xmr-stak");
                if (xmr_staks.Length > 0)
                {
                    DialogResult close_xmr_stak = MessageBox.Show(this, "xmr-stak.exe appears to already be running, would you like to kill it?", "XMR-Stak already running!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (close_xmr_stak == DialogResult.Yes)
                    {
                        foreach(Process xmr_stak in xmr_staks)
                        {
                            try
                            {
                                xmr_stak.Kill();
                            } catch(System.ComponentModel.Win32Exception) {}
                        }
                    }
                }

                CheckForUpdates();
            }
        }
        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (XMRStakProcess != null)
            {
                if (!XMRStakProcess.HasExited)
                {
                    XMRStakProcess.Kill();
                }
            }
        }
        
        public void UpdateXMRStakProcess()
        {
            if (Properties.Settings.Default.XMRStakLocation.Length > 0 && Properties.Settings.Default.ConfigFileLocation.Length > 0 && SelectedGPUConfig != null)
            {
                if (XMRStakProcess != null)
                {
                    if (!XMRStakProcess.HasExited)
                    {
                        XMRStakProcess.Kill();
                    }
                }

                OutputBox.Text = "Starting XMR-Stak...";

                string arguments = "--config \"" + Properties.Settings.Default.ConfigFileLocation + "\" --noUAC ";
                if (Properties.Settings.Default.MinerBackend == "CPU")
                {
                    arguments += "--noAMD --noNVIDIA --cpu \"" + SelectedGPUConfig + "\"";
                }
                else if (Properties.Settings.Default.MinerBackend == "AMD")
                {
                    arguments += "--noNVIDIA --noCPU --amd \"" + SelectedGPUConfig + "\"";
                }
                else if (Properties.Settings.Default.MinerBackend == "NVIDIA")
                {
                    arguments += "--noAMD --noCPU --nvidia \"" + SelectedGPUConfig + "\"";
                }

                ProcessStartInfo startInfo = new ProcessStartInfo(Properties.Settings.Default.XMRStakLocation);
                startInfo.Arguments = arguments;
                startInfo.RedirectStandardError = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                XMRStakProcess = Process.Start(startInfo);
                XMRStakProcess.Exited += (object s, EventArgs e) =>
                {
                    Invoke((Action)(() =>
                    {
                        OutputBox.Text = select_config;
                        StopXMRStak.Enabled = false;
                        foreach (ToolStripMenuItem ConfigItem_Uncheck in config_selectors)
                        {
                            ConfigItem_Uncheck.Checked = false;
                        }
                    }));
                };
                XMRStakProcess.OutputDataReceived += (object s, DataReceivedEventArgs e) =>
                {
                    Invoke((Action)(() =>
                    {
                        OutputBox.AppendText("\r\n" + e.Data);
                    }));
                };

                XMRStakProcess.BeginOutputReadLine();
                XMRStakProcess.BeginErrorReadLine();

                StopXMRStak.Enabled = true;
            }
            else
            {
                MessageBox.Show(this, "We tried to run XMR-Stak, but you've either not set your xmr-stak.exe location, config file location or not selected a miner config.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string SelectedGPUConfig;
        private List<ToolStripMenuItem> config_selectors = new List<ToolStripMenuItem>();
        public void UpdateConfigDropdown()
        {
            config_selectors.Clear();

            GPUConfigsDropdown.DropDownItems.Clear();
            GPUConfigsDropdown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                SetXMRStakLocation,
                ConfigFileLocation,
                CurrencySelector,
                MinerSelector,
                new ToolStripSeparator(),
                AddConfigButton,
                NoConfigsIndicator
            });
            ConfigFileLocation.Checked = Properties.Settings.Default.ConfigFileLocation.Length > 0;
            SetXMRStakLocation.Checked = Properties.Settings.Default.XMRStakLocation.Length > 0;
            MoneroSelector.Checked     = Properties.Settings.Default.Currency == "Monero";
            AeonSelector.Checked       = Properties.Settings.Default.Currency == "Aeon";
            CPUSelector.Checked        = Properties.Settings.Default.MinerBackend == "CPU";
            AMDSelector.Checked        = Properties.Settings.Default.MinerBackend == "AMD";
            NVIDIASelector.Checked     = Properties.Settings.Default.MinerBackend == "NVIDIA";

            NoConfigsIndicator.Visible = Properties.Settings.Default.Configs.Count == 0;
            foreach (string ConfigLocation in Properties.Settings.Default.Configs)
            {
                ToolStripMenuItem ConfigItem = new ToolStripMenuItem();
                config_selectors.Add(ConfigItem);
                ConfigItem.Size = new System.Drawing.Size(203, 22);
                ConfigItem.Name = ConfigLocation;
                ConfigItem.Text = Path.GetFileNameWithoutExtension(ConfigLocation);
                ConfigItem.Checked = SelectedGPUConfig == ConfigLocation;
                GPUConfigsDropdown.DropDownItems.Add(ConfigItem);

                ToolStripMenuItem Remove = new ToolStripMenuItem();
                Remove.Size = new System.Drawing.Size(203, 22);
                Remove.Name = ConfigLocation + "_remove";
                Remove.Text = "Remove";
                Remove.Click += (s, e) =>
                {
                    if (SelectedGPUConfig == ConfigLocation)
                    {
                        SelectedGPUConfig = null;
                    }
                    Properties.Settings.Default.Configs.Remove(ConfigLocation);
                    Properties.Settings.Default.Save();
                    GPUConfigsDropdown.DropDownItems.Remove(ConfigItem);
                    NoConfigsIndicator.Visible = Properties.Settings.Default.Configs.Count == 0;
                };

                ToolStripMenuItem Select = new ToolStripMenuItem();
                Select.Size = new System.Drawing.Size(203, 22);
                Select.Name = ConfigLocation + "_select";
                Select.Text = "Use";
                Select.Click += (s, e) =>
                {
                    SelectedGPUConfig = ConfigLocation;
                    foreach (ToolStripMenuItem ConfigItem_Uncheck in config_selectors)
                    {
                        ConfigItem_Uncheck.Checked = false;
                    }
                    ConfigItem.Checked = true;
                    UpdateXMRStakProcess();
                };

                ConfigItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    Select,
                    Remove
                });
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void XMRStakLocation_Click(object sender, EventArgs e)
        {
            XMRStakLocation.ShowDialog();
        }
        private void XMRStakLocation_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.XMRStakLocation = XMRStakLocation.FileName;
            Properties.Settings.Default.Save();
            MessageBox.Show(this, "Saved XMR-Stak location.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddConfigButton_Click(object sender, EventArgs e)
        {
            AddMinerConfigFile.ShowDialog();
        }
        private void AddConfigFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Configs.AddRange(AddMinerConfigFile.FileNames);
            Properties.Settings.Default.Save();
            UpdateConfigDropdown();
            MessageBox.Show(this, "Added miner configuration(s).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CPUSelector_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MinerBackend = "CPU";
            Properties.Settings.Default.Save();
            UpdateConfigDropdown();
        }
        private void AMDSelector_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MinerBackend = "AMD";
            Properties.Settings.Default.Save();
            UpdateConfigDropdown();
        }
        private void NVIDIASelector_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MinerBackend = "NVIDIA";
            Properties.Settings.Default.Save();
            UpdateConfigDropdown();
        }

        private void MoneroSelector_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Currency = "Monero";
            Properties.Settings.Default.Save();
            UpdateConfigDropdown();
        }
        private void AeonSelector_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Currency = "Aeon";
            Properties.Settings.Default.Save();
            UpdateConfigDropdown();
        }

        private void StopXMRStak_Click(object sender, EventArgs e)
        {
            if (XMRStakProcess != null)
            {
                if (!XMRStakProcess.HasExited)
                {
                    XMRStakProcess.Kill();
                    XMRStakProcess = null;
                    OutputBox.Text = select_config;
                    StopXMRStak.Enabled = false;
                    foreach (ToolStripMenuItem ConfigItem_Uncheck in config_selectors)
                    {
                        ConfigItem_Uncheck.Checked = false;
                    }
                }
            }
        }

        private void ConfigFileLocation_Click(object sender, EventArgs e)
        {
            OpenConfigFile.ShowDialog();
        }
        private void OpenConfigFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.ConfigFileLocation = OpenConfigFile.FileName;
            Properties.Settings.Default.Save();
            UpdateConfigDropdown();
            MessageBox.Show(this, "Set configuration file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Wiki_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/WilliamVenner/XMR-Stak-GUI/wiki");
        }
        private void GitHub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/WilliamVenner/XMR-Stak-GUI/");
        }
        private void CheckForUpdates_Click(object sender, EventArgs e)
        {
            CheckForUpdates(true);
        }
    }
}
