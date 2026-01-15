using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarSpace
{
    public partial class Oyun : Form
    {
        private Game _game;
        private Timer _gameTimer;
        private bool _isGameStarted; // Oyun başlangıç durumu

        public Oyun()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 576;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            _game = new Game();

            _gameTimer = new Timer();
            _gameTimer.Interval = 16; // Yaklaşık 60 FPS
            _gameTimer.Tick += GameLoop;

            _isGameStarted = false; // Oyun duraklatılmış halde başlar

            this.KeyDown += Oyun_KeyDown;
        }

        private void Oyun_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_isGameStarted)
            {
                // İlk tuşa basıldığında oyun başlasın
                _isGameStarted = true;
                _gameTimer.Start();
            }
            else
            {
                _game.controlPlayer(e.KeyCode);
            }
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!_isGameStarted)
                return;

            // Oyunu güncelle
            _game.Update();

            // Oyunun bitip bitmediğini kontrol et
            if (_game.IsGameOver)
            {
                _gameTimer.Stop();
                MessageBox.Show($"Kaybettin!\nSkor : {_game.Score}", "Game Over", MessageBoxButtons.OK);
                Application.Exit();
            }

            // Ekranı yeniden çiz
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Arka plan ve tüm oyun elemanlarını çiz
            _game.Draw(e.Graphics, this.ClientSize.Width, this.ClientSize.Height);

            if (!_isGameStarted)
            {
                // Başlangıç mesajını çiz
                DrawStartMessage(e.Graphics);
            }
        }

        private void DrawStartMessage(Graphics g)
        {
            // Ortada "Press Any Key to Start" mesajı
            string message = "Press Any Key to Start";
            Font font = new Font("Arial", 32, FontStyle.Bold);
            SizeF textSize = g.MeasureString(message, font);
            float x = (this.ClientSize.Width - textSize.Width) / 2;
            float y = (this.ClientSize.Height - textSize.Height) / 2;

            // Metni gölgeli şekilde çizin
            g.DrawString(message, font, Brushes.Black, x + 2, y + 2);
            g.DrawString(message, font, Brushes.White, x, y);
        }
    }
}
