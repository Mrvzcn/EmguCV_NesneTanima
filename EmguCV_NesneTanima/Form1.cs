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
        // 1. SINIF D�ZEY�NDE DE���KENLER
        private VideoCapture _capture = null;
        private CascadeClassifier _faceCascade = null;
        private bool _kameraCalisiyor = false;

        public Form1()
        {
            InitializeComponent();

            btnBaslat.Click += btnBaslat_Click;
            this.FormClosing += Form1_FormClosing;

            // G�r�nt� g�zel otursun
            pbGoruntu.SizeMode = PictureBoxSizeMode.StretchImage;

            // 2. BA�LATMA KODLARI (Form A��l�rken �al���r)
            try
            {
                // 0 varsay�lan kamera demektir.
                _capture = new VideoCapture(0);

                // Haar Cascade dosyas�n� y�kle
                // Dosyan�n .exe dosyas�n�n yan�nda oldu�undan emin olun.
                _faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");

                // G�r�nt� yakalama olay�n� ProcessFrame metoduna ba�la
                _capture.ImageGrabbed += ProcessFrame;

                // Butonun ba�lang�� metnini ayarla
                btnBaslat.Text = "Kamera Ba�lat";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ba�latma Hatas�: Kamera ba�lat�lamad� veya Model dosyas� bulunamad�. Hata: " + ex.Message);
            }
        }

        // 3. KAMERA KARE ��LEME METODU (Nesne Tan�ma Burada Yap�l�r)
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

                // UI thread g�venli g�ncelleme
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

        // 4. BA�LAT/DURDUR BUTON KODU
        // Bu metot, btnBaslat isimli butonun Click olay�na ba�lanmal�d�r.
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
                _capture.Pause(); // Ak��� duraklatmak i�in Pause() daha iyi olabilir.
                _kameraCalisiyor = false;
                btnBaslat.Text = "Kamera Ba�lat";

                var old = pbGoruntu.Image;
                pbGoruntu.Image = null; // G�r�nt�y� temizle
                old?.Dispose();
            }
        }

        // 5. KAYNAKLARI SERBEST BIRAKMA METODU
        // Bu metot, Form'un FormClosing olay�na ba�lanmal�d�r.
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

        // Form1.cs dosyan�za a�a��daki metodu ekleyin:
        private void Form1_Load(object sender, EventArgs e)
        {
            // Gerekli ba�lang�� i�lemlerini buraya yazabilirsiniz.
        }
    }
}