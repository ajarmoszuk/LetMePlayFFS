using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SKYPE4COMLib;
using System.Runtime.InteropServices;

namespace LetMePlayFFS
{
    public partial class Form1 : MetroForm
    {
        private bool metroTile1WasClicked = false;
        private bool skypeAttached = false;
        private bool _initiallyMinimised;
        private bool _enableOnStart;
        private bool _consoleEnabled;
        [DllImport("kernel32")]
        static extern int AllocConsole();

        public Form1()
        {
            InitializeComponent();
        }


        static bool CheckIfProcessIsRunning(string nameSubstring)
        {
            return Process.GetProcesses().Any(p => p.ProcessName.Contains(nameSubstring));
        }

        static bool gameRunning()
        {
            int gameRun = 0; 
            string[] games = System.IO.File.ReadAllLines(@"gameList.txt");
            Process[] processes = Process.GetProcesses();
            string[] proc = processes.Select(o => o.ToString()).ToArray();
            var intersect = games.Intersect(proc);
                
                foreach (string g in games)
                {
                    if (CheckIfProcessIsRunning(g))
                    {
                        gameRun++;
                        Console.WriteLine(gameRun);
                        Console.WriteLine(g);
                        return true;
                    }
                }
            if (gameRun > 0) { return true; } else { return false; }
        }

        private Timer timer1;
        public void EnableTick()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(EnableThread);
            timer1.Interval = 5000;
            timer1.Start();
        }

        private void EnableThread(object sender, EventArgs e)
        {
            var skype = new Skype();
            
            if (metroTile1WasClicked == true)
            {
                if (skypeAttached == false)
                {
                    metroTile4.Visible = false;
                    skype.Attach(5, true);
                    skypeAttached = true;
                    Console.WriteLine(skypeAttached);
                }
                Console.WriteLine(gameRunning());
                if (gameRunning()) {
                    if (metroComboBox1.Text == "Away") {
                        skype.ChangeUserStatus(TUserStatus.cusAway);
                    } else if (metroComboBox1.Text == "Do Not Disturb") {
                        skype.ChangeUserStatus(TUserStatus.cusDoNotDisturb);
                    } else if (metroComboBox1.Text == "Invisible") {
                        skype.ChangeUserStatus(TUserStatus.cusInvisible);
                    }
                    else {
                        skype.ChangeUserStatus(TUserStatus.cusOffline);
                    }
                    if (metroToggle2.Checked == true)
                    {
                            skype.CurrentUserProfile.MoodText = metroTextBox1.Text;
                    }
                    metroTile3.Text = "Running";
                    metroTile3.Style = MetroFramework.MetroColorStyle.Blue;
                }
                else
                {
                    skype.ChangeUserStatus(TUserStatus.cusOnline);
                    if (metroToggle2.Checked == true)
                    {
                        skype.CurrentUserProfile.MoodText = "";
                    }
                    metroTile3.Text = "Enabled";
                    metroTile3.Style = MetroFramework.MetroColorStyle.Green;
                }
                //}
            }
            else
            {
                skype.ChangeUserStatus(TUserStatus.cusOnline);
                skype.CurrentUserProfile.MoodText = "";
            }
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/delta360");
        }

        private void metroToggle1_Click(object sender, EventArgs e)
        {
            if (metroToggle1.Checked == false)
            {
                metroTextBox1.Visible = true;
                metroLabel9.Visible = true;
            }
            else
            {
                metroTextBox1.Visible = false;
                metroLabel9.Visible = false;
            }
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            metroTile1WasClicked = true;
            metroTile3.Text = "Enabled";
            metroTile3.Style = MetroFramework.MetroColorStyle.Green;
            EnableTick();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            metroTile1WasClicked = false;
            metroTile3.Text = "Disabled";
            metroTile3.Style = MetroFramework.MetroColorStyle.Red;
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel3_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 1; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg[0] == '-')
                {
                    switch (arg)
                    {
                        case "-min":
                            _initiallyMinimised = true;
                            break;
                        case "-enabled":
                            _enableOnStart = true;
                            break;
                        case "-console":
                            _consoleEnabled = true;
                            break;
                    }
                }
            }

            if (_initiallyMinimised == true)
            {
                this.WindowState = FormWindowState.Minimized;
                notifyIcon1.Visible = true;
            }

            if (_enableOnStart == true)
            {
                metroTile1WasClicked = true;
                metroTile3.Text = "Enabled";
                metroTile3.Style = MetroFramework.MetroColorStyle.Green;
                EnableTick();
            }

            if (_consoleEnabled == true)
            {
                AllocConsole();
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BringToFront();
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }
    }
}
