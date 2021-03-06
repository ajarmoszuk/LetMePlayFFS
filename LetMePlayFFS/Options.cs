﻿using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetMePlayFFS
{
    public partial class Options : MetroForm
    {
        public Options()
        {
            InitializeComponent();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"gameList.txt"))
            {
                Process.Start(@"gameList.txt");
            }
            else
            {
                UpdateWindow updateForm = new UpdateWindow();
                updateForm.Show();
            }
            
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            UpdateWindow updateForm = new UpdateWindow();
            updateForm.Show();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            Process.Start(@"updateApp.exe");
            System.Windows.Forms.Application.Exit();
        }
    }
}
