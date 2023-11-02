using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Pacman3
{
    public partial class Form2 : Form
    {
        List<PictureBox> walls = new List<PictureBox>();
        List<PictureBox> apples = new List<PictureBox>();
        List<PictureBox> monsters = new List<PictureBox>();
        int playerPoint = 0;
        public Form2()
        {
            InitializeComponent();

            //foreach döngüsü verilen listedeki her bir elemanı dolaşır. 
            //burada this.Controls listesindeki her bir kontrolü dolaşacaktır.
            foreach (var control in this.Controls)
            {
                //bu kontrol bir picturebox ise on p diyelim.
                if (control is PictureBox p)
                {
                    //bu p'nin tagı null değilse...
                    if (p.Tag != null)
                    {
                        //p'nin tagi wall ise walls listesine bu p'yi (PictureBox) ekle.
                        if (p.Tag.ToString() == "wall")
                        {
                            walls.Add(p);
                        }
                        //p'nin tagi monster ise monsters listesine ekle. 
                        else if (p.Tag.ToString()=="monster")
                        {
                            monsters.Add(p);
                        }
                        //p'nin tagi apple ise apples listesine ekle.
                        else if (p.Tag.ToString()=="apple")
                        {
                            apples.Add(p);
                        }
                    }
                }
            }


        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            //oyuncunun hareket edeceği konumun değişkeni
            var targetLocation = player.Location;
            //tuşlara göre yeni konum
            if (e.KeyCode == Keys.Right)
            {
                targetLocation.X += 10;
            }
            else if (e.KeyCode == Keys.Left)
            {
                targetLocation.X -= 10;
            }
            else if (e.KeyCode == Keys.Up)
            {
                targetLocation.Y -= 10;
            }
            else if (e.KeyCode == Keys.Down)
            {
                targetLocation.Y += 10;
            }
            //oyuncu hareket edebilir mi?
            bool canMove = true;
            //yeni konuma göre oyuncunun içinde bulunacağı hayali dörtgen.
            var targetRectangle = new Rectangle(targetLocation, player.Size);
            //tüm duvarları dolaş
            foreach (var wall in walls)
            {
                //oyuncunun içinde bulunacağı hayali konum bu duvarın dörtgeni ile çakişıyor mu?
                if (targetRectangle.IntersectsWith(wall.Bounds))
                {
                    //oyuncu hareket etmesin.
                    canMove = false;
                    //duvarlardan birine çarptık, diğerlerini kontrol etmeye gerek yok, döngüden çık
                    break;
                }
            }

            //tüm elmaları dolaş
            foreach (var apple in apples)
            {
                if (targetRectangle.IntersectsWith(apple.Bounds))
                {
                    //bir elmaya çarptık, oyuncu puanını artır, elmalar listesinden kaldır ve formdan çıkar.
                    playerPoint += 10;
                    apples.Remove(apple);
                    labelPoint.Text = "Puan:" + playerPoint.ToString();
                    this.Controls.Remove(apple); 
                    break;
                }
            }

            //tüm canavarları dolaş
            foreach (var monster in monsters)
            {
                if (targetRectangle.IntersectsWith(monster.Bounds))
                {
                    playerPoint -= 10;
                }
            }

            //hareket edebiliyorsam oyuncunun yeni konumunu ata.
            if (canMove)
            {
                player.Location = targetLocation;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var monster in monsters)
            {
                if (monster.Bounds.IntersectsWith(player.Bounds))
                {
                    timer1.Stop();
                    MessageBox.Show("Yandın");

                    player.Location = new Point(43, 35);
                    timer1.Start();
                    break;
            
                }


                bool canMoveY = true;
                bool canMoveX = true;
                //canavarın dikey hareket ederse yni konumu
                var nextLocationY = monster.Location;
                //canavar yatay hareket ederse yeni konumu
                var nextLocationX=monster.Location; 

                //oyuncu canavardan solda ise 
                if (player.Location.X<nextLocationX.X)
                {
                    nextLocationX.X -= 2;
                }
                else
                {
                    nextLocationX.X += 2;
                }

                //oyuncumuz canavardan yukarıda ise
                if (player.Location.Y==nextLocationY.Y)
                {
                    canMoveY = false;
                }
                else if (player.Location.Y<nextLocationY.Y)
                {
                    //canavar yukarı doğru hareket etsin.
                    nextLocationY.Y -= 2;
                }
                else
                {
                    nextLocationY.Y += 2;
                }


              
                //canavarın hareket edeceği Y konuma göre (yukarı-aşağı) hayali bir dörtgen oluştur.
                var monsterRectangleY = new Rectangle(nextLocationY, monster.Size);
                //canavarın hareket edeceği X konuma göre (sağ sol) hayali bir dörtgen oluştur.
                var monsterRectangleX = new Rectangle(nextLocationX, monster.Size);

                foreach (var wall in walls)
                {
                    if (monsterRectangleY.IntersectsWith(wall.Bounds))
                    {
                        canMoveY = false;                      
                    }
                    if (monsterRectangleX.IntersectsWith(wall.Bounds))
                    {
                        canMoveX = false;
                    }
                   
                }


                if (canMoveY) {
                    monster.Location = nextLocationY;
                }
                else if (canMoveX)
                {
                    monster.Location = nextLocationX;
                }
            }
        }
    }
}
