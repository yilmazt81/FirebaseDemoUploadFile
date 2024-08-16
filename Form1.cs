using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirebaseDemoApp
{
    public partial class Form1 : Form
    {
        FireStorage fireStorage = null;
        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                fireStorage = new FireStorage();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in openFileDialog.FileNames)
                    {
                        var fileStream = File.OpenRead(file);
                        var uploadPath = await fireStorage.SaveFileToStorageAsync("test", Path.GetFileName(file), fileStream);
                        listViewFiles.Items.Add(uploadPath);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 0)
                return;
            try
            {
                var imagePath = listViewFiles.SelectedItems[0].Text;
                pictureBox1.Load(imagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
