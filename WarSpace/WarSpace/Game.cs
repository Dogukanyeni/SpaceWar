using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarSpace
{
    public class Game
    {
        private Spaceship _spaceship;
        private List<Bullet> _bullets;
        private List<Bullet> _enemyBullets; // Düşman mermileri
        private List<Enemy> _enemies;
        private List<PowerUp> _powerUps;
        public int Score { get; private set; } // Skoru tutan property
        private int Level;
        private int spawnTimer;        // Meteor oluşturma zamanlayıcısı
        private int spawnDelay = 200; // Başlangıçta 200 milisaniye gecikme
        private List<Meteor> _meteors; // Meteor listesi
        public event Action OnGameOver;
        private bool _isGameOver; // Oyunun bitip bitmediğini kontrol eden flag
        public bool IsGameOver => _isGameOver; // Dışarıdan kontrol edebilmek için property
        private Random _random;
        private Image _backgroundImage;
        private int ScreenWidth = 500; // Oyun penceresi genişliği
        private int ScreenHeight = 500; // Oyun penceresi yüksekliği

        public Game()
        {
            _backgroundImage = Image.FromFile("background.png");
            _spaceship = new Spaceship(50, 250, 50, 50, 10);
            _bullets = new List<Bullet>();
            _enemies = new List<Enemy>();
            _powerUps = new List<PowerUp>();
            _random = new Random();
            _enemyBullets = new List<Bullet>();

            _meteors = new List<Meteor>();
            Score = 0;
            Level = 1;
            _isGameOver = false; // Başlangıçta oyun devam ediyor
        }
        private Color GetEnemyBulletColor(Enemy enemy)
        {
            if (enemy is BasicEnemy) return Color.Green;
            if (enemy is StrongEnemy) return Color.Blue;
            if (enemy is BossEnemy) return Color.Purple;
            return Color.Gray; // Varsayılan renk
        }
        public void Update()
        {
            UpdateEnemies();
            UpdateBullets();
            UpdateEnemyBullets();
            UpdatePowerUps();
            CheckCollisions();
            UpdateMeteors();

            if (Score > Level * 125)
            {
                Level++;
                IncreaseDifficulty();
                foreach (var enemy in _enemies)
                    enemy.IncreaseSpeed();
            }

            if (_isGameOver)
                return;
        }

        private void UpdateBullets()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].Move(false); // Uzay gemisi mermisi sağa doğru hareket eder

                // Ekran dışına çıkan mermileri kaldır
                if (_bullets[i].IsOffScreen(800, 576))
                {
                    _bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        private void UpdateEnemyBullets()
        {
            for (int i = 0; i < _enemyBullets.Count; i++)
            {
                _enemyBullets[i].Move(true); // Düşman mermisi sola doğru hareket eder

                if (_enemyBullets[i].IsOffScreen(800, 576)) // Ekran dışına çıkan mermileri kaldır
                {
                    _enemyBullets.RemoveAt(i);
                    i--;
                }
            }
        }

        private void UpdateEnemies()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                // Düşman hareketi
                _enemies[i].Move();

                // Belirli aralıklarla ateş et
                if (_enemies[i].CanShoot())
                {
                    var newBullet = new Bullet(
                        _enemies[i].Position.X,
                        _enemies[i].Position.Y + _enemies[i].Position.Height / 2, // Mermi, düşmanın orta noktasından çıkar
                        10, // Genişlik
                        5,  // Yükseklik
                        5,  // Hız (pozitif olmalı)
                        GetEnemyBulletColor(_enemies[i]) // Düşman türüne göre mermi rengi
                    );

                    _enemyBullets.Add(newBullet); // Yeni mermiyi düşman mermileri listesine ekle
                }

                // Eğer düşman ekran dışına çıktıysa kaldır
                if (_enemies[i].IsOffScreen())
                {
                    _enemies.RemoveAt(i);
                    i--;
                }
            }
       
            // Yeni düşman oluşturma ihtimali
            if (_random.Next(100) < 3) // %3 ihtimalle yeni düşman oluştur
            {
                int y = _random.Next(0, 500); // Rastgele y pozisyonu
                int type = _random.Next(5);  // Düşman türü (0-4)

                Enemy newEnemy = null;

                // Düşman türüne göre düşman oluştur
                switch (type)
                {
                    case 0:
                        newEnemy = new BasicEnemy(800, y, 50, 50, 2);
                        break;
                    case 1:
                        newEnemy = new FastEnemy(800, y, 50, 50, 3);
                        break;
                    case 2:
                        newEnemy = new StrongEnemy(800, y, 70, 70, 1);
                        break;
                    case 3:
                    case 4: 
                        if (Level >= 2) // BossEnemy yalnızca 2 seviyeden sonra gelebilir
                        {
                            int bossChance = Level < 3  ? 10 : 20; // Seviye 5'ten sonra ihtimal artar

                            if (_random.Next(100) < bossChance) // %10-20 ihtimalle BossEnemy
                            {
                                newEnemy = new BossEnemy(800, y, 100, 100, 1);
                            }
                        }

                        break;
                }

                // Eğer yeni düşman oluşturulduysa, çakışma kontrolü yap ve ekle
                if (newEnemy != null)
                {
                    bool overlaps = _enemies.Any(e => CollisionDetector.CheckCollision(e.Position, newEnemy.Position)) ||
                                    _meteors.Any(m => CollisionDetector.CheckCollision(m.Position, newEnemy.Position));

                    if (!overlaps)
                    {
                        _enemies.Add(newEnemy);
                    }
                }
            } 
        }





        private void UpdatePowerUps()
        {
            for (int i = 0; i < _powerUps.Count; i++)
            {
                // Power-up hareket eder
                _powerUps[i].Move();

                // Power-up uzay gemisiyle çarpışırsa
                if (CollisionDetector.CheckCollision(_spaceship.Position, _powerUps[i].Position))
                {
                    ApplyPowerUp(_powerUps[i]);
                    _powerUps.RemoveAt(i);
                    i--;
                }
            }

            // Rastgele power-up oluştur
            if (_random.Next(1000) < 2) // %0.2 ihtimal
            {
                int y = _random.Next(0, 500);
                string[] types = { "health", "speed", "damage" };
                string type = types[_random.Next(3)];
                _powerUps.Add(new PowerUp(800, y, 30, 30, 5, type));
            }
        }

        private void UpdateMeteors()
        {
            // Meteorları hareket ettir
            for (int i = 0; i < _meteors.Count; i++)
            {
                _meteors[i].Move();

                // Ekrandan çıkan meteorları kaldır
                if (_meteors[i].IsOffScreen(800))
                {
                    _meteors.RemoveAt(i);
                    i--;
                }
            }

            // Meteor oluşturma zamanlaması
            spawnTimer++;
            if (spawnTimer >= spawnDelay)
            {
                spawnTimer = 0; // Sayaç sıfırlanır

                // Yeni bir meteor oluştur
                int y = _random.Next(0, 500);
                _meteors.Add(new Meteor(800, y, 50, 50, 2));
            }
        }

        private void IncreaseDifficulty()
        {
            // Zorluk seviyesine göre meteor oluşturma gecikmesini azalt
            if (spawnDelay > 50) // Minimum bir limit belirleyebilirsiniz
            {
                spawnDelay -= 20; // Zorluk arttıkça daha sık meteor oluşur
            }

            // Düşman hızlarını artır
            foreach (var enemy in _enemies)
            {
                enemy.IncreaseSpeed();
            }
            // Tüm düşmanların ateş sıklığını artır
            foreach (var enemy in _enemies)
            {
                enemy.DecreaseShootInterval();
            }
        }

        private void CheckCollisions()
        {
            // Mermi-Düşman Çarpışması
            for (int i = 0; i < _bullets.Count; i++)
            {
                for (int j = 0; j < _enemies.Count; j++)
                {
                    if (CollisionDetector.CheckCollision(_bullets[i].Bounds, _enemies[j].Bounds))
                    {
                        if (_enemies[j] is BasicEnemy basicEnemy)
                        {
                            basicEnemy.TakeDamage();
                            if (basicEnemy.IsDestroyed())
                            {
                                Score += 10;
                                _enemies.RemoveAt(j);
                                j--;
                            }
                        }
                        else if (_enemies[j] is FastEnemy fastEnemy)
                        {
                            fastEnemy.TakeDamage();
                            if (fastEnemy.IsDestroyed())
                            {
                                Score += 20;
                                _enemies.RemoveAt(j);
                                j--;
                            }
                        }
                        else if (_enemies[j] is StrongEnemy strongEnemy)
                        {
                            strongEnemy.TakeDamage();
                            if (strongEnemy.IsDestroyed())
                            {
                                Score += 20;
                                _enemies.RemoveAt(j);
                                j--;
                            }
                        }
                        else if (_enemies[j] is BossEnemy bossEnemy)
                        {
                            bossEnemy.TakeDamage();
                            if (bossEnemy.IsDestroyed())
                            {
                                Score += 50;
                                _enemies.RemoveAt(j);
                                j--;
                            }
                        }

                        _bullets.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            // Mermi-Meteor Çarpışması
            for (int i = 0; i < _bullets.Count; i++)
            {
                for (int j = 0; j < _meteors.Count; j++)
                {
                    if (CollisionDetector.CheckCollision(_bullets[i].Bounds, _meteors[j].Position))
                    {
                        _bullets.RemoveAt(i); // Mermiyi yok et
                        i--; // İndeksi düzelt
                        break; // Aynı merminin birden fazla meteora çarpmaması için
                    }
                }
            }
            // FastEnemy ile uzay gemisinin çarpışması
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] is FastEnemy && CollisionDetector.CheckCollision(_spaceship.Position, _enemies[i].Bounds))
                {
                    _spaceship.TakeDamage(1); // Sağlık biraz azalır
                    _enemies.RemoveAt(i); // FastEnemy yok edilir
                    i--;

                    if (_spaceship.IsDestroyed())
                    {
                        _isGameOver = true; // Oyun biter
                        return;
                    }
                }
            }

            // Meteor ve uzay gemisi çarpışması
            foreach (var meteor in _meteors)
            {
                if (CollisionDetector.CheckCollision(_spaceship.Position, meteor.Position))
                {
                    _isGameOver = true; // Oyunu bitir
                    return;
                }
            }
            // Uzay gemisinin düşman mermisiyle çarpışması
            for (int i = 0; i < _enemyBullets.Count; i++)
            {
                if (CollisionDetector.CheckCollision(_spaceship.Position, _enemyBullets[i].Bounds))
                {
                    _spaceship.TakeDamage(1); // Sağlık azalır
                    _enemyBullets.RemoveAt(i);
                    i--;

                    if (_spaceship.IsDestroyed())
                    {
                        SetGameOver(); // Oyun biter
                        return;
                    }
                }
            }
        }

        public void SetGameOver()
        {
            _isGameOver = true; // Oyun sonlandırma durumu
        }
        private void ApplyPowerUp(PowerUp powerUp)
        {
            if (powerUp.Type == "health")
                _spaceship.IncreaseHealth();
            else if (powerUp.Type == "speed")
                _spaceship.IncreaseSpeed();
            else if (powerUp.Type == "damage")
                _spaceship.IncreaseDamage();
        }

        public void Draw(Graphics g, int width, int height)
        {
            // Arka planı çiz
            g.DrawImage(_backgroundImage, 0, 0, width, height);
            _spaceship.Draw(g);

            foreach (var bullet in _bullets)
                bullet.Draw(g);

            foreach (var bullet in _enemyBullets)
                bullet.Draw(g);

                foreach (var meteor in _meteors)
                meteor.Draw(g);

            foreach (var enemy in _enemies)
                enemy.Draw(g);

            foreach (var powerUp in _powerUps)
                powerUp.Draw(g);

            // Skor sağ üstte kalıyor
            string scoreText = $"Skor: {Score}";
            SizeF scoreSize = g.MeasureString(scoreText, new Font("Arial", 16));
            float marginRight = 10;
            g.DrawString(scoreText, new Font("Arial", 16), Brushes.White, new PointF(width - scoreSize.Width - marginRight, 10));

            // Level sol üstte olacak şekilde ayarlandı
            string levelText = $"Zorluk: {Level}";
            g.DrawString(levelText, new Font("Arial", 16), Brushes.White, new PointF(10, 10));
        }



        public void controlPlayer(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    _spaceship.Move("up");
                    break;
                case Keys.Down:
                    _spaceship.Move("down");
                    break;
                case Keys.Left:
                    _spaceship.Move("left");
                    break;
                case Keys.Right:
                    _spaceship.Move("right");
                    break;
                case Keys.Space:
                    _bullets.Add(new Bullet(_spaceship.Position.X + _spaceship.Position.Width, _spaceship.Position.Y + _spaceship.Position.Height / 2 - 5, 10, 5, 15, Color.Red)); break;
            }
        }
    }



}
