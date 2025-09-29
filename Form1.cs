using System;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        // --- Oyun için değişkenler ---
        int gravity = 2;        // kuşun aşağı düşme hızı
        int jump = -15;         // space basınca yukarı çıkış gücü
        int birdVelocity = 0;   // kuşun anlık hızı
        int score = 0;          // skor

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // formun tuşları yakalaması için
        }

        // Timer her tick çalışır (20ms'de bir)
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // kuşun hızına yerçekimi ekle
            birdVelocity += gravity;
            flappyBird.Top += birdVelocity;

            // boruları sola kaydır
            pipeTop.Left -= 5;
            pipeBottom.Left -= 5;

            // borular ekran dışına çıkınca yeniden sağdan gelsin
            if (pipeTop.Right < 0)
            {
                Random rand = new Random();
                int pipeHeight = rand.Next(80, 250);

                pipeTop.Height = pipeHeight;
                pipeBottom.Top = pipeHeight + 120; // aradaki boşluk
                pipeBottom.Height = this.ClientSize.Height - pipeBottom.Top;

                pipeTop.Left = this.ClientSize.Width;
                pipeBottom.Left = this.ClientSize.Width;

                // ✅ Skoru artır
                score++;
                scoreText.Text = "Score: " + score;
            }

            // yere çarptı mı?
            if (flappyBird.Bottom >= this.ClientSize.Height)
            {
                gameTimer.Stop();
                MessageBox.Show("Oyun Bitti! Skorun: " + score);
            }

            // çarpışma kontrolü
            if (flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds))
            {
                gameTimer.Stop();
                MessageBox.Show("Çarptın! Oyun Bitti! Skorun: " + score);
            }
        }

        // Klavyeden tuş basılınca
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                birdVelocity = jump; // space basınca yukarı zıplat
            }
        }

        private void flappyBird_Click(object sender, EventArgs e)
        {

        }
    }
}
