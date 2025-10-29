using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV.CvEnum;

namespace EmguCV_NesneTanima
{
    public partial class Form1 : Form
    {
        // 1. SINIF DÜZEYÝNDE DEÐÝÞKENLER
        private VideoCapture _capture = null;
        private CascadeClassifier _faceCascade = null;
        private bool _kameraCalisiyor = false;

        public Form1()
        {
            InitializeComponent();

            btnBaslat.Click += btnBaslat_Click;
            this.FormClosing += Form1_FormClosing;

            // Görüntü güzel otursun
            pbGoruntu.SizeMode = PictureBoxSizeMode.StretchImage;

            // 2. BAÞLATMA KODLARI (Form Açýlýrken Çalýþýr)
            try
            {
                // 0 varsayýlan kamera demektir.
                _capture = new VideoCapture(0);

                // Haar Cascade dosyasýný yükle
                // Dosyanýn .exe dosyasýnýn yanýnda olduðundan emin olun.
                _faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");

                // Görüntü yakalama olayýný ProcessFrame metoduna baðla
                _capture.ImageGrabbed += ProcessFrame;

                // Butonun baþlangýç metnini ayarla
                btnBaslat.Text = "Kamera Baþlat";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Baþlatma Hatasý: Kamera baþlatýlamadý veya Model dosyasý bulunamadý. Hata: " + ex.Message);
            }
        }

        // 3. KAMERA KARE ÝÞLEME METODU (Nesne Tanýma Burada Yapýlýr)
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (_capture == null) return;

            using (Mat frame = new Mat())
            {
                _capture.Retrieve(frame);
                if (frame.IsEmpty) return;

                using (Mat grayFrame = new Mat())
                {
                    CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);
                    CvInvoke.EqualizeHist(grayFrame, grayFrame);

                    Rectangle[] faces = _faceCascade?.DetectMultiScale(
                        grayFrame,
                        1.1,
                        10,
                        System.Drawing.Size.Empty
                    ) ?? Array.Empty<Rectangle>();

                    foreach (Rectangle face in faces)
                        CvInvoke.Rectangle(frame, face, new MCvScalar(0, 0, 255), 2);
                }

                // UI thread güvenli güncelleme
                var bmp = frame.ToBitmap();
                if (pbGoruntu.InvokeRequired)
                    pbGoruntu.BeginInvoke(new Action(() =>
                    {
                        var old = pbGoruntu.Image;
                        pbGoruntu.Image = bmp;
                        old?.Dispose();
                    }));
                else
                {
                    var old = pbGoruntu.Image;
                    pbGoruntu.Image = bmp;
                    old?.Dispose();
                }
            }
        }

        // 4. BAÞLAT/DURDUR BUTON KODU
        // Bu metot, btnBaslat isimli butonun Click olayýna baðlanmalýdýr.
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (_capture == null) return;

            if (!_kameraCalisiyor)
            {
                _capture.Start();
                _kameraCalisiyor = true;
                btnBaslat.Text = "Kamera Durdur";
            }
            else
            {
                _capture.Pause(); // Akýþý duraklatmak için Pause() daha iyi olabilir.
                _kameraCalisiyor = false;
                btnBaslat.Text = "Kamera Baþlat";

                var old = pbGoruntu.Image;
                pbGoruntu.Image = null; // Görüntüyü temizle
                old?.Dispose();
            }
        }

        // 5. KAYNAKLARI SERBEST BIRAKMA METODU
        // Bu metot, Form'un FormClosing olayýna baðlanmalýdýr.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_capture != null)
                {
                    _capture.ImageGrabbed -= ProcessFrame;
                    _capture.Stop();
                    _capture.Dispose();
                    _capture = null;
                }

                _faceCascade?.Dispose();
                _faceCascade = null;

                var old = pbGoruntu.Image;
                pbGoruntu.Image = null;
                old?.Dispose();
            }
            catch { /* yut */ }
        }

        // Form1.cs dosyanýza aþaðýdaki metodu ekleyin:
        private void Form1_Load(object sender, EventArgs e)
        {
            // Gerekli baþlangýç iþlemlerini buraya yazabilirsiniz.
        }
    }
}