using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security.Principal;
using System.Windows.Forms;

namespace XMR_Stak_GUI
{
    public partial class MainForm : Form
    {
        public double version = 2.1;

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
            version_request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);
            try
            {
                WebResponse response = version_request.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    double latest_version = Convert.ToDouble(stream.ReadToEnd().Trim());
                    if (latest_version > version)
                    {
                        this.Text = "XMR-Stak GUI v" + version + " (outdated!)";
                        DialogResult open_github = MessageBox.Show(this, "You are running an outdated version of XMR-Stak GUI!\n\nWould you like to open the GitHub releases page?", "Outdated", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                this.Text = "XMR-Stak GUI v" + version;

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
            if (Properties.Settings.Default.XMRStakLocation.Length > 0 && Properties.Settings.Default.ConfigFileLocation.Length > 0 && SelectedMiningConfig != null)
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
                if (SelectedMiningConfigType == "CPU")
                {
                    arguments += "--noAMD --noNVIDIA --cpu \"" + SelectedMiningConfig + "\"";
                }
                else if (SelectedMiningConfigType == "AMD")
                {
                    arguments += "--noNVIDIA --noCPU --amd \"" + SelectedMiningConfig + "\"";
                }
                else if (SelectedMiningConfigType == "NVIDIA")
                {
                    arguments += "--noAMD --noCPU --nvidia \"" + SelectedMiningConfig + "\"";
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

        public string SelectedMiningConfig;
        public string SelectedMiningConfigType;
        private List<ToolStripMenuItem> config_selectors = new List<ToolStripMenuItem>();
        public void UpdateConfigDropdown()
        {
            config_selectors.Clear();

            GPUConfigsDropdown.DropDownItems.Clear();
            GPUConfigsDropdown.DropDownItems.AddRange(new ToolStripItem[] {
                SetXMRStakLocation,
                ConfigFileLocation,
                new ToolStripSeparator(),
                AddConfigButton,
                NoConfigsIndicator
            });
            ConfigFileLocation.Checked = Properties.Settings.Default.ConfigFileLocation.Length > 0;
            SetXMRStakLocation.Checked = Properties.Settings.Default.XMRStakLocation.Length > 0;

            NoConfigsIndicator.Visible = Properties.Settings.Default.Configs.Count == 0;
            foreach (string ConfigLocationUnparsed_foreach in Properties.Settings.Default.Configs)
            {
                string ConfigLocationUnparsed = ConfigLocationUnparsed_foreach;
                string[] ConfigLocationParsed = ConfigLocationUnparsed.Split(';');
                string ConfigLocation = ConfigLocationParsed[0];
                string ConfigType = ConfigLocationParsed[1];

                ToolStripMenuItem ConfigItem = new ToolStripMenuItem();
                config_selectors.Add(ConfigItem);
                ConfigItem.Size = new System.Drawing.Size(203, 22);
                ConfigItem.Name = ConfigLocation;
                ConfigItem.Text = Path.GetFileNameWithoutExtension(ConfigLocation);
                ConfigItem.Checked = SelectedMiningConfig == ConfigLocation;
                GPUConfigsDropdown.DropDownItems.Add(ConfigItem);

                ToolStripMenuItem CPUType = new ToolStripMenuItem();
                CPUType.Size = new System.Drawing.Size(203, 22);
                CPUType.Name = ConfigLocation + "_cpu";
                CPUType.Text = "CPU";
                if (ConfigType == "CPU") CPUType.Checked = true;

                ToolStripMenuItem AMDType = new ToolStripMenuItem();
                AMDType.Size = new System.Drawing.Size(203, 22);
                AMDType.Name = ConfigLocation + "_amd";
                AMDType.Text = "AMD";
                if (ConfigType == "AMD") AMDType.Checked = true;

                ToolStripMenuItem NVIDIAType = new ToolStripMenuItem();
                NVIDIAType.Size = new System.Drawing.Size(203, 22);
                NVIDIAType.Name = ConfigLocation + "_nvidia";
                NVIDIAType.Text = "NVIDIA";
                if (ConfigType == "NVIDIA") NVIDIAType.Checked = true;

                ToolStripMenuItem Type = new ToolStripMenuItem();
                Type.Size = new System.Drawing.Size(203, 22);
                Type.Name = ConfigLocation + "_type";
                Type.Text = "Type";
                Type.DropDownItems.AddRange(new ToolStripItem[] {
                    CPUType,
                    AMDType,
                    NVIDIAType
                });

                ToolStripMenuItem Remove = new ToolStripMenuItem();
                Remove.Size = new System.Drawing.Size(203, 22);
                Remove.Name = ConfigLocation + "_remove";
                Remove.Text = "Remove";
                Remove.Click += (s, e) =>
                {
                    if (SelectedMiningConfig == ConfigLocation)
                    {
                        SelectedMiningConfig = null;
                    }
                    Properties.Settings.Default.Configs.Remove(ConfigLocationUnparsed);
                    Properties.Settings.Default.Save();
                    GPUConfigsDropdown.DropDownItems.Remove(ConfigItem);
                    Console.WriteLine(Properties.Settings.Default.Configs.Count);
                    NoConfigsIndicator.Visible = Properties.Settings.Default.Configs.Count == 0;
                };

                ToolStripMenuItem Select = new ToolStripMenuItem();
                Select.Size = new System.Drawing.Size(203, 22);
                Select.Name = ConfigLocation + "_select";
                Select.Text = "Use";
                Select.Click += (s, e) =>
                {
                    SelectedMiningConfig = ConfigLocation;
                    SelectedMiningConfigType = ConfigType;
                    foreach (ToolStripMenuItem ConfigItem_Uncheck in config_selectors)
                    {
                        ConfigItem_Uncheck.Checked = false;
                    }
                    ConfigItem.Checked = true;
                    UpdateXMRStakProcess();
                };
                if (CPUType.Checked == false && AMDType.Checked == false && NVIDIAType.Checked == false)
                {
                    Select.Enabled = false;
                }

                CPUType.Click += (s, e) =>
                {
                    Properties.Settings.Default.Configs.Remove(ConfigLocationUnparsed);
                    Properties.Settings.Default.Configs.Add(ConfigLocation + ";" + "CPU");
                    Properties.Settings.Default.Save();

                    ConfigLocationUnparsed = ConfigLocation + ";" + "CPU";
                    ConfigType = "CPU";
                    
                    Select.Enabled = true;
                    CPUType.Checked = true;
                    AMDType.Checked = false;
                    NVIDIAType.Checked = false;
                };
                AMDType.Click += (s, e) =>
                {
                    Properties.Settings.Default.Configs.Remove(ConfigLocationUnparsed);
                    Properties.Settings.Default.Configs.Add(ConfigLocation + ";" + "AMD");
                    Properties.Settings.Default.Save();

                    ConfigLocationUnparsed = ConfigLocation + ";" + "AMD";
                    ConfigType = "AMD";

                    Select.Enabled = true;
                    CPUType.Checked = false;
                    AMDType.Checked = true;
                    NVIDIAType.Checked = false;
                };
                NVIDIAType.Click += (s, e) =>
                {
                    Properties.Settings.Default.Configs.Remove(ConfigLocationUnparsed);
                    Properties.Settings.Default.Configs.Add(ConfigLocation + ";" + "NVIDIA");
                    Properties.Settings.Default.Save();

                    ConfigLocationUnparsed = ConfigLocation + ";" + "NVIDIA";
                    ConfigType = "NVIDIA";

                    Select.Enabled = true;
                    CPUType.Checked = false;
                    AMDType.Checked = false;
                    NVIDIAType.Checked = true;
                };

                ConfigItem.DropDownItems.AddRange(new ToolStripItem[] {
                    Select,
                    Remove,
                    Type
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
            UpdateConfigDropdown();
            MessageBox.Show(this, "Saved XMR-Stak location.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddConfigButton_Click(object sender, EventArgs e)
        {
            AddMinerConfigFile.ShowDialog();
        }
        private void AddConfigFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach(string FileName in AddMinerConfigFile.FileNames)
            {
                Properties.Settings.Default.Configs.Add(FileName + ";" + "UNKNOWN");
            }
            Properties.Settings.Default.Save();
            UpdateConfigDropdown();
            MessageBox.Show(this, "Added miner configuration(s).\n\nRemember: you'll need to set the type of this mining configuration (CPU/AMD/NVIDIA) before you can use it.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
