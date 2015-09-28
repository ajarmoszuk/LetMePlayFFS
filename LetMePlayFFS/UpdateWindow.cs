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
using System.IO;
using MetroFramework;

namespace LetMePlayFFS
{
    public partial class UpdateWindow : MetroForm
    {
        public UpdateWindow()
        {
            InitializeComponent();
        }

        private void UpdateWindow_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string sUrlToReadFileFrom = "https://raw.githubusercontent.com/delta360/LetMePlayFFS/master/updateFolder/gameList.txt";
            int iLastIndex = sUrlToReadFileFrom.LastIndexOf('/');
            string sDownloadFileName = sUrlToReadFileFrom.Substring(iLastIndex + 1, (sUrlToReadFileFrom.Length - iLastIndex - 1));
            string sFilePathToWriteFileTo = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + sDownloadFileName;
            Uri url = new Uri(sUrlToReadFileFrom);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            response.Close();
            Int64 iSize = response.ContentLength;
            Int64 iRunningByteTotal = 0;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                using (System.IO.Stream streamRemote = client.OpenRead(new Uri(sUrlToReadFileFrom)))
                {
                    using (Stream streamLocal = new FileStream(sFilePathToWriteFileTo, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        int iByteSize = 0;
                        byte[] byteBuffer = new byte[iSize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;
                            double dIndex = (double)(iRunningByteTotal);
                            double dTotal = (double)byteBuffer.Length;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);
                            backgroundWorker1.ReportProgress(iProgressPercentage);
                        }
                        streamLocal.Close();
                    }
                    streamRemote.Close();
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            metroProgressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MetroMessageBox.Show(this, "Gamelist has been successfully updated.", "Successfully Updated");
            UpdateWindow.ActiveForm.Hide();
        }

        
    }
}
