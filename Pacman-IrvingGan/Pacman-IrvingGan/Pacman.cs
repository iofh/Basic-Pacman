//this is the pacman class. the pacman class only moves the pacman depending on the direction which the user sets
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_IrvingGan
{
    public class Pacman : Character
    {
        //constance
        private const int edgeofscreenl = 19;
        private const int edgeofscreenr = 0;

        //fields
        private int kibbleeaten;
        private bool powerup;
        private SoundPlayer kibbleate;
        private SoundPlayer cherryate;

        //constructor
        public Pacman(int[,] pacmap, Point position, int type)
            : base(pacmap, type, position)
        {
            this.position = position;
            powerup = false;
            kibbleate = new SoundPlayer(Properties.Resources.KibbleEat);
            cherryate = new SoundPlayer(Properties.Resources.FruitEat);
        }

        public void PacMove(int key)//this method is used to move the pacman
        {
            switch (key)
            {
                case (int)eDirection.LEFT:
                    if (position.Y == edgeofscreenr)
                    {
                        position.Y = edgeofscreenl;
                        pacmap[position.X, edgeofscreenr] = (int)eBlocks.BLANK;
                    }
                    if (pacmap[position.X, position.Y - 1] == (int)eBlocks.WALL)//if next step is wall then keep the position
                    {
                        pacmap[position.X, position.Y] = type;
                    }
                    else
                    {
                        if (pacmap[position.X, position.Y - 1] == (int)eBlocks.KIBBLE)//if next step is kibble then keep plus the kibble count
                        {
                            kibbleeaten += 1;
                            kibbleate.Play();
                        }
                        if (pacmap[position.X, position.Y - 1] == (int)eBlocks.CHERRY)//if next step is kibble then keep plus the kibble count and turn on powerup mode
                        {
                            powerup = true;
                            kibbleeaten += 1;
                            cherryate.PlaySync();
                        }
                        pacmap[position.X, position.Y] = (int)eBlocks.BLANK;//leaving every step behind pacman a blank
                        position.Y -= 1;
                        pacmap[position.X, position.Y] = type;
                    }
                    break;
                case (int)eDirection.RIGHT:
                    if (position.Y == edgeofscreenl)
                    {
                        position.Y = edgeofscreenr;
                        pacmap[position.X, edgeofscreenl] = (int)eBlocks.BLANK;
                    }
                    if (pacmap[position.X, position.Y + 1] == (int)eBlocks.WALL)
                    {
                        pacmap[position.X, position.Y] = type;
                    }
                    else
                    {
                        if (pacmap[position.X, position.Y + 1] == (int)eBlocks.KIBBLE)
                        {
                            kibbleeaten += 1;
                            kibbleate.Play();
                        }
                        if (pacmap[position.X, position.Y + 1] == (int)eBlocks.CHERRY)
                        {
                            powerup = true;
                            kibbleeaten += 1;
                            cherryate.PlaySync();
                        }
                        pacmap[position.X, position.Y] = (int)eBlocks.BLANK;
                        position.Y += 1;
                        pacmap[position.X, position.Y] = type;

                    }
                    break;
                case (int)eDirection.DOWN:
                    if (pacmap[position.X + 1, position.Y] == (int)eBlocks.WALL)
                    {
                        pacmap[position.X, position.Y] = type;
                    }
                    else
                    {
                        if (pacmap[position.X + 1, position.Y] == (int)eBlocks.KIBBLE)
                        {
                            kibbleeaten += 1;
                            kibbleate.Play();
                        }
                        if (pacmap[position.X + 1, position.Y] == (int)eBlocks.CHERRY)
                        {
                            powerup = true;
                            kibbleeaten += 1;
                            cherryate.PlaySync();
                        }
                        pacmap[position.X, position.Y] = (int)eBlocks.BLANK;
                        position.X += 1;
                        pacmap[position.X, position.Y] = type;
                    }
                    break;
                case (int)eDirection.UP:
                    if (pacmap[position.X - 1, position.Y] == (int)eBlocks.WALL)
                    {
                        pacmap[position.X, position.Y] = type;
                    }
                    else
                    {
                        if (pacmap[position.X - 1, position.Y] == (int)eBlocks.KIBBLE)
                        {
                            kibbleeaten += 1;
                            kibbleate.Play();
                        }
                        if (pacmap[position.X - 1, position.Y] == (int)eBlocks.CHERRY)
                        {
                            powerup = true;
                            kibbleeaten += 1;
                            cherryate.PlaySync();
                        }
                        pacmap[position.X, position.Y] = (int)eBlocks.BLANK;
                        position.X -= 1;
                        pacmap[position.X, position.Y] = type;
                    }
                    break;
            }
        }

        public Point Position { get => position; set => position = value; }
        public int Kibbleeaten { get => kibbleeaten; set => kibbleeaten = value; }
        public bool Powerup { get => powerup; set => powerup = value; }
    }
}