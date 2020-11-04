//This is a parent class of the pacman and ghouls
// the purpose of this class is to only hold the fields of the pacman and ghouls
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_IrvingGan
{
    public abstract class Character
    {
        //fields
        protected Point position;
        protected int[,] pacmap;
        protected int type;//TYPE is the int value of the type of character in the 2d array maze

        //constructor
        public Character(int[,] pacmap, int type, Point position)
        {
            this.pacmap = pacmap;
            this.position = position;
            this.type = type;
        }
    }
}
