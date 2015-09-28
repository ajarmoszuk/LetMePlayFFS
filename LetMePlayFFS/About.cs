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
using MetroFramework;

namespace LetMePlayFFS
{
    public partial class About : MetroForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            Process.Start("http://alex.jarmosz.uk/");
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/delta360");
        }

        private void About_KeyUp(object sender, KeyEventArgs e)
        {   
        }

        private void About_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void metroUserControl1_KeyDown(object sender, KeyEventArgs e)
        {
           MetroMessageBox.Show(this, "These are not the droids you are looking for.", "Move along...");
        }


    }
}
