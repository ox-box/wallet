using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OX.Wallets.Base.DNP
{
    public partial class UrlPort : UserControl
    {
        public string Url;
        public UrlPort(string url)
        {
            this.Url = url;
            InitializeComponent();
        }

        private void UrlPort_Load(object sender, EventArgs e)
        {
            this.darkLabel1.Text = Url;
            this.bt_copy.Text = UIHelper.LocalString("复制", "Copy");
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(this.Url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            this.pictureBox1.Image = qrCodeImage;
        }

        private void bt_copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.Url);
        }
    }
}
