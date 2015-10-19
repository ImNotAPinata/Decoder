using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace BarDecoder
{
    public partial class Decodificador : Form
    {
        public Decodificador()
        {
            InitializeComponent();
        }

        private void btnGetPath_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtPath.Text = openFileDialog.FileName;
            bool IsValid = ValidateFile(txtPath.Text);
            if (IsValid)
            {
                var codeBitMap = (Bitmap) Bitmap.FromFile(txtPath.Text); 
                pictureBox.Image = codeBitMap;

                IBarcodeReader reader = new BarcodeReader();
                var code = reader.Decode(codeBitMap);
                if (code != null)
                {
                    lblcode.Text = code.Text.ToString();
                }
                else { lblcode.Text = "*** FAILED ON DECODING A CODE FROM IMAGE ***"; }
            }
            else
            {
                lblcode.Text = "*** ERROR LOADING VALID IMAGE ***";
            }
        }

        private bool ValidateFile(string p)
        {
            var extension = System.IO.Path.GetExtension(p);
            List<String> ImagesExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

            var IsValidExtension = ImagesExtensions.Contains(extension.ToUpper());
            return IsValidExtension;
        }



    }
}
