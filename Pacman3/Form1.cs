﻿using System;
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

            for (int i = 0; i < walls.Count; i++)
            {
                Rectangle rWall = walls[i].Bounds;
                if (playerTargetRect.IntersectsWith(rWall)==true)
                {
                    canMove = false;
                }
            }

            if (canMove==true)
            {
player.Location = playerTargetLocation;
            }
            
        }

    }
}