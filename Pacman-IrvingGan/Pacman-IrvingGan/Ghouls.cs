//this is the ghouls class. the ghouls class moves the ghouls object depending on the direction
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_IrvingGan
{
    public class Ghouls : Character
    {
        //constance
        private const int edgeofscreenl=19;
        private const int edgeofscreenr=0;

        //fields
        private int previousstepblocktype;
        private int current;

        //constructor
        public Ghouls(int[,] pacmap, Point position, int type, int previousstepblocktype)
            : base(pacmap, type, position)
        {
            this.position = position;
            this.previousstepblocktype = previousstepblocktype;
        }

        public void GhoulsMove(int key)// this method is to move the ghouls
        {
            switch (key)
            {
                case (int)eDirection.LEFT:
                    if (position.Y == edgeofscreenr)
                    {
                        position.Y = edgeofscreenl;//19 is the edge of the 
                        pacmap[position.X, edgeofscreenr] = (int)eBlocks.BLANK;
                        previousstepblocktype = (int)eBlocks.BLANK;
                        current = (int)eBlocks.BLANK;
                    }
                    if (pacmap[position.X, position.Y - 1] == (int)eBlocks.WALL)//when the ghoul next move is a wall, then it keeps the position
                    {
                        pacmap[position.X, position.Y] = type;//setting the position of the ghouls in the map
                    }
                    else
                    {
                        //this is used when the next move is a ghoul, it will also keep its position
                        if (pacmap[position.X, position.Y - 1] == (int)eBlocks.GREEN ||
                            pacmap[position.X, position.Y - 1] == (int)eBlocks.RED ||
                            pacmap[position.X, position.Y - 1] == (int)eBlocks.PINK ||
                            pacmap[position.X, position.Y - 1] == (int)eBlocks.ORANGE)
                        {
                            pacmap[position.X, position.Y] = type;
                        }
                        else
                        {
                            if (pacmap[position.X, position.Y - 1] == (int)eBlocks.PACMAN)//this is initiated when the next move is a pacman
                            {
                                pacmap[position.X, position.Y] = previousstepblocktype;//put the previous type of block in the current position
                                position.Y -= 1;//move the position of the ghouls
                                previousstepblocktype = (int)eBlocks.BLANK;//putting the previous block blank so if a previous block is a kibble, it will not show up in the spawning area of the ghoul
                                current = (int)eBlocks.BLANK;//putting the current block blank so if a previous block is a kibble, it will not show up in the spawning area of the ghoul
                                pacmap[position.X, position.Y] = type;//putting the value of the type of character in the new position in the maze
                            }
                            else
                            {
                                if (pacmap[position.X, position.Y - 1] == (int)eBlocks.KIBBLE)//if the next step is a kibble
                                {
                                    pacmap[position.X, position.Y] = previousstepblocktype;//put the previous type of block in the current position
                                    current = pacmap[position.X, position.Y - 1];//making the next step a current block
                                    position.Y -= 1;//then move the position
                                    previousstepblocktype = current;//put the current as the previous block for future use.
                                    pacmap[position.X, position.Y] = type;//move the position of the character and set the position is the maze
                                }
                                else
                                {
                                    pacmap[position.X, position.Y] = previousstepblocktype;//if the next step is a blank
                                    current = pacmap[position.X, position.Y - 1];//use same algorithm as the kibble. 
                                    position.Y -= 1;
                                    previousstepblocktype = current;
                                    pacmap[position.X, position.Y] = type;
                                }
                            }
                        }
                    }
                    break;
                case (int)eDirection.RIGHT:
                    if (position.Y == edgeofscreenl)
                    {
                        position.Y = edgeofscreenr;
                        pacmap[position.X, edgeofscreenl] = (int)eBlocks.BLANK;
                        previousstepblocktype = (int)eBlocks.BLANK;
                        current = (int)eBlocks.BLANK;
                    }
                    if (pacmap[position.X, position.Y + 1] == (int)eBlocks.WALL)
                    {
                        pacmap[position.X, position.Y] = type;

                    }
                    else
                    {
                        if (pacmap[position.X, position.Y + 1] == (int)eBlocks.GREEN ||
                            pacmap[position.X, position.Y + 1] == (int)eBlocks.RED ||
                            pacmap[position.X, position.Y + 1] == (int)eBlocks.PINK ||
                            pacmap[position.X, position.Y + 1] == (int)eBlocks.ORANGE)
                        {
                            pacmap[position.X, position.Y] = type;
                        }
                        else
                        {
                            if (pacmap[position.X, position.Y + 1] == (int)eBlocks.PACMAN)
                            {
                                pacmap[position.X, position.Y] = previousstepblocktype;
                                position.Y += 1;
                                previousstepblocktype = (int)eBlocks.BLANK;
                                current = (int)eBlocks.BLANK;
                                pacmap[position.X, position.Y] = type;
                            }
                            else
                            {
                                if (pacmap[position.X, position.Y + 1] == (int)eBlocks.KIBBLE)
                                {
                                    current = pacmap[position.X, position.Y + 1];
                                    pacmap[position.X, position.Y] = previousstepblocktype;
                                    position.Y += 1;
                                    previousstepblocktype = current;
                                    pacmap[position.X, position.Y] = type;
                                }
                                else
                                {
                                    current = pacmap[position.X, position.Y + 1];
                                    pacmap[position.X, position.Y] = previousstepblocktype;
                                    position.Y += 1;
                                    previousstepblocktype = current;
                                    pacmap[position.X, position.Y] = type;
                                }
                            }
                        }
                    }
                    break;
                case (int)eDirection.DOWN:
                    if (pacmap[position.X + 1, position.Y] == (int)eBlocks.WALL)
                    {
                        pacmap[position.X, position.Y] = type;

                    }
                    else
                    {
                        if (pacmap[position.X + 1, position.Y] == (int)eBlocks.GREEN ||
                            pacmap[position.X + 1, position.Y] == (int)eBlocks.RED ||
                            pacmap[position.X + 1, position.Y] == (int)eBlocks.PINK ||
                            pacmap[position.X + 1, position.Y] == (int)eBlocks.ORANGE)
                        {
                            pacmap[position.X, position.Y] = type;
                        }
                        else
                        {
                            if (pacmap[position.X + 1, position.Y] == (int)eBlocks.PACMAN)
                            {
                                pacmap[position.X, position.Y] = previousstepblocktype;
                                position.X += 1;
                                previousstepblocktype = (int)eBlocks.BLANK;
                                current = (int)eBlocks.BLANK;
                                pacmap[position.X, position.Y] = type;
                            }
                            else
                            {
                                if (pacmap[position.X + 1, position.Y] == (int)eBlocks.KIBBLE)
                                {
                                    current = pacmap[position.X + 1, position.Y];
                                    pacmap[position.X, position.Y] = previousstepblocktype;
                                    position.X += 1;
                                    previousstepblocktype = current;
                                    pacmap[position.X, position.Y] = type;
                                }
                                else
                                {
                                    current = pacmap[position.X + 1, position.Y];
                                    pacmap[position.X, position.Y] = previousstepblocktype;
                                    position.X += 1;
                                    previousstepblocktype = current;
                                    pacmap[position.X, position.Y] = type;
                                }
                            }
                        }
                    }
                    break;
                case (int)eDirection.UP:
                    if (pacmap[position.X - 1, position.Y] == (int)eBlocks.WALL)
                    {
                        pacmap[position.X, position.Y] = type;

                    }
                    else
                    {

                        if (pacmap[position.X - 1, position.Y] == (int)eBlocks.GREEN ||
                            pacmap[position.X - 1, position.Y] == (int)eBlocks.RED ||
                            pacmap[position.X - 1, position.Y] == (int)eBlocks.PINK ||
                            pacmap[position.X - 1, position.Y] == (int)eBlocks.ORANGE)
                        {
                            pacmap[position.X, position.Y] = type;
                        }
                        else
                        {
                            if (pacmap[position.X - 1, position.Y] == (int)eBlocks.PACMAN)
                            {
                                pacmap[position.X, position.Y] = previousstepblocktype;
                                position.X -= 1;
                                previousstepblocktype = (int)eBlocks.BLANK;
                                current = (int)eBlocks.BLANK;
                                pacmap[position.X, position.Y] = type;
                            }
                            else
                            {
                                if (pacmap[position.X - 1, position.Y] == (int)eBlocks.KIBBLE)
                                {
                                    current = pacmap[position.X - 1, position.Y];
                                    pacmap[position.X, position.Y] = previousstepblocktype;
                                    position.X -= 1;
                                    previousstepblocktype = current;
                                    pacmap[position.X, position.Y] = type;
                                }
                                else
                                {
                                    current = pacmap[position.X - 1, position.Y];
                                    pacmap[position.X, position.Y] = previousstepblocktype;
                                    position.X -= 1;
                                    previousstepblocktype = current;
                                    pacmap[position.X, position.Y] = type;
                                }
                            }
                        }
                    }
                    break;
            }
        }
        //properties
        public Point Position { get => position; set => position = value; }
        public int Previousstepblocktype { get => previousstepblocktype; set => previousstepblocktype = value; }
        public int Current { get => current; set => current = value; }
    }
}
