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
    public partial class Form1 : Form
    {
        List<PictureBox> walls = new List<PictureBox>();
        List<PictureBox> apples = new List<PictureBox>();
        List<PictureBox> enemies = new List<PictureBox>();
        int point = 0;
        public Form1()
        {      

            InitializeComponent();

            for (int i = 0; i < this.Controls.Count; i++)
            {
                var control = this.Controls[i];
                if (control is PictureBox p)
                {
                    if (p.Tag!=null&&p.Tag.ToString()=="wall")
                    {
                        walls.Add(p);
                    }
                    else if (p.Tag!=null&& p.Tag.ToString()=="apple")
                    {
                        apples.Add(p);
                    }
                    else if (p.Tag!=null && p.Tag.ToString()== "enemy")
                    {
                        enemies.Add(p);
                    }
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Point playerTargetLocation = player.Location;

            if (e.KeyCode == Keys.Right)
            {
                playerTargetLocation.X += 10;
            }
            else if (e.KeyCode == Keys.Left)
            {
                playerTargetLocation.X -= 10;
            }
            else if (e.KeyCode == Keys.Down)
            {
                playerTargetLocation.Y += 10;
            }
            else if (e.KeyCode == Keys.Up)
            {
                playerTargetLocation.Y -= 10;
            }

            Rectangle playerTargetRect = new Rectangle(playerTargetLocation, player.Size);
            bool canMove = true;

            //duvarları dolaş çarpma kontrolü yap.
            for (int i = 0; i < walls.Count; i++)
            {
                Rectangle rWall = walls[i].Bounds;
                if (playerTargetRect.IntersectsWith(rWall)==true)
                {
                    canMove = false;
                    break;
                }
            }

            //elmaları dolaş, çarpma kontrolü yap.
            for (int i = 0; i < apples.Count; i++)
            {
                Rectangle rApple = apples[i].Bounds;
                if (playerTargetRect.IntersectsWith(rApple))
                {
                    this.Controls.Remove(apples[i]);
                    apples.RemoveAt(i);
                    point += 10;
                    lblPoint.Text = "Puan: " + point;
                    break;
                }
            }

            //canavarları dolaş
            for(int i = 0;i < enemies.Count;i++) {
            
            Rectangle rEnemy= enemies[i].Bounds;
                if (playerTargetRect.IntersectsWith(rEnemy))
                {
                    MessageBox.Show("Yandın!");
                    this.Close();
                }
            }

            if (canMove==true)
            {
player.Location = playerTargetLocation;
            }
            
        }

    }
}
