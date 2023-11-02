using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman3
{
    public partial class Form3 : Form
    {
        List<PictureBox> walls = new List<PictureBox>();
        List<PictureBox> monsters = new List<PictureBox>();
        List<PictureBox> apples = new List<PictureBox>();
        int playerPoint;
        public Form3()
        {
            InitializeComponent();

            foreach (var control in this.Controls)
            {
                if (control is PictureBox p)
                {
                    if (p.Tag != null)
                    {
                        if (p.Tag.ToString() == "wall")
                        {
                            walls.Add(p);
                        }
                        if (p.Tag.ToString() == "apple")
                        {
                            apples.Add(p);
                        }
                        if (p.Tag.ToString() == "monster")
                        {
                            monsters.Add(p);
                        }
                    }
                }
            }
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            var nextLocation = player.Location;
            if (e.KeyCode == Keys.Right)
            {
                nextLocation.X += 10;
            }
            else if (e.KeyCode == Keys.Left)
            {
                nextLocation.X -= 10;
            }
            else if (e.KeyCode == Keys.Up)
            {
                nextLocation.Y -= 10;
            }
            else if (e.KeyCode == Keys.Down)
            {
                nextLocation.Y += 10;
            }

            //duvarları kontrol et.
            bool canMove = true;
            Rectangle nextRectangle = new Rectangle(nextLocation, player.Size);
            foreach (var wall in walls)
            {
                if (nextRectangle.IntersectsWith(wall.Bounds))
                {
                    canMove = false;
                    break;
                }
            }

            //elmaları kontrol et
            foreach (var apple in apples)
            {
                if (nextRectangle.IntersectsWith(apple.Bounds))
                {
                    apples.Remove(apple);
                    this.Controls.Remove(apple);
                    playerPoint += 10;

                    break;
                }
            }
            if (apples.Count==0)
            {
                timer1.Stop();
                MessageBox.Show("KAZANDIN");
            }


            labelPuan.Text = "Puan:" + playerPoint;


            if (canMove == true)
            {
                player.Location = nextLocation;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            foreach (var monster in monsters)
            {
                Point monsterXLocation = monster.Location;
                Point monsterYLocation = monster.Location;
                //yatay hareket için
                if (monster.Left < player.Left)
                {
                    monsterXLocation.X += 1;
                }
                else if (monster.Left > player.Left)
                {
                    monsterXLocation.X -= 1;
                }
                //dikey hareket için
                if (monster.Top < player.Top)
                {
                    monsterYLocation.Y += 1;
                }
                else if (monster.Top > player.Top)
                {
                    monsterYLocation.Y -= 1;
                }


                bool canMoveX = true;
                bool canMoveY = true;

                Rectangle monsterXRectangle = new Rectangle(monsterXLocation, monster.Size);
                Rectangle monsterYRectangle = new Rectangle(monsterYLocation, monster.Size);

                foreach (var wall in walls)
                {
                    if (wall.Bounds.IntersectsWith(monsterXRectangle))
                    {
                        canMoveX = false;
                       
                    }
                    if (wall.Bounds.IntersectsWith(monsterYRectangle))
                    {
                        canMoveY = false;
                    }
                }

                //canavarlar çarpışmasın
                foreach (var otherMonster in monsters)
                {
                    if (otherMonster!=monster)
                    {
                        if (monsterXRectangle.IntersectsWith(otherMonster.Bounds))
                        {
                            canMoveX = false;
                        }
                        if (monsterYRectangle.IntersectsWith(otherMonster.Bounds))
                        {
                            canMoveY = false;
                        }
                    }
                }

                if (canMoveX && monster.Location.X!=monsterXLocation.X)
                {
                    monster.Location = monsterXLocation;
                }
                else if (canMoveY)
                {
                    monster.Location = monsterYLocation;
                }

                if (monster.Bounds.IntersectsWith(player.Bounds))
                {     timer1.Stop();
                    MessageBox.Show("Yandın");
                    player.Location = new Point(47, 34);
                }
            }
        }
    }
}
