//this is the maze class which holds the layout of the map. and it also controls how the pacman will look when the pacman changes direction
//it will also control how the ghouls look when its on normal mode or when its on running away from pacman mode
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Pacman_IrvingGan
{
    class Maze : DataGridView
    {
        //const
        private const int NROWSCOLUMNS = 20;// Number of cells in each row and column
        private const int CELLSIZE = 27;//the size of the cell
        private const int SPACESIZE = 4;//space between the cells

        //fields
        private int direction;//the direction of pacman
        private int[,] pacmap;//the whole layout of the maze
        private Bitmap wall;
        private Bitmap kibble;
        private Bitmap blank;
        private Bitmap green;
        private Bitmap red;
        private Bitmap pink;
        private Bitmap orange;
        private Bitmap origreen;
        private Bitmap orired;
        private Bitmap oripink;
        private Bitmap oriorange;
        private Bitmap pacmanl1;
        private Bitmap pacmanl2;
        private Bitmap pacmanr1;
        private Bitmap pacmanr2;
        private Bitmap pacmand1;
        private Bitmap pacmand2;
        private Bitmap pacmanu1;
        private Bitmap pacmanu2;
        private Bitmap pacman;
        private Bitmap ghoulrunImg;
        private Bitmap redvelvet;

        //constructor
        public Maze()
            : base()
        {
            //initialise fields            
            pacmap = new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},//1
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},//2
                {1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1},//3
                {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1},//4
                {1, 8, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 8, 1},//5
                {1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1},//6
                {1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1},//7
                {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},//8
                {1, 0, 1, 1, 1, 1, 0, 1, 6, 6, 6, 1, 0, 1, 1, 0, 1, 0, 0, 1},//9
                {6, 0, 0, 0, 0, 0, 0, 1, 6, 4, 6, 1, 0, 1, 1, 0, 0, 1, 0, 6},//10
                {1, 0, 1, 0, 1, 1, 0, 1, 3, 5, 2, 1, 0, 0, 0, 0, 1, 0, 0, 1},//11
                {1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1},//12
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},//13
                {1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1},//14
                {1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1},//15
                {1, 8, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 1, 8, 1},//16
                {1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1},//17
                {1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1},//18
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},//19                                  
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}//20
            };
            this.kibble = Properties.Resources.kibble;
            this.wall = Properties.Resources.wall__3_;
            this.blank = Properties.Resources.Blank;
            this.green = Properties.Resources.green1__2_;
            this.red = Properties.Resources.red1__2_;
            this.pink = Properties.Resources.purple1__2_;
            this.orange = Properties.Resources.orange1;
            this.pacmanl1 = Properties.Resources.pacman1left;
            this.pacmanl2 = Properties.Resources.pacman2left;
            this.pacmanr1 = Properties.Resources.pacman1right;
            this.pacmanr2 = Properties.Resources.pacman2right;
            this.pacmanu1 = Properties.Resources.pacman1up;
            this.pacmanu2 = Properties.Resources.pacman2up;
            this.pacmand1 = Properties.Resources.pacman1down;
            this.pacmand2 = Properties.Resources.pacman2down;
            this.redvelvet = Properties.Resources.cherry;
            this.ghoulrunImg = Properties.Resources.red1;
            origreen = green;
            oriorange = orange;
            oripink = pink;
            orired = red;

            // set position of maze on the Form
            Top = 0;
            Left = 0;

            // setup the columns to display images. We want to display images, so we set 5 columns worth of Image columns
            for (int x = 0; x < NROWSCOLUMNS; x++)
            {
                Columns.Add(new DataGridViewImageColumn());
            }
            // then we can tell the grid the number of rows we want to display
            RowCount = NROWSCOLUMNS;

            // set the properties of the Maze(which is a DataGridView object)
            Height = NROWSCOLUMNS * CELLSIZE + SPACESIZE;
            Width = NROWSCOLUMNS * CELLSIZE + SPACESIZE;
            ScrollBars = ScrollBars.None;
            ColumnHeadersVisible = false;
            RowHeadersVisible = false;

            // set size of cells:
            foreach (DataGridViewRow a in this.Rows)
                a.Height = CELLSIZE;

            foreach (DataGridViewColumn c in this.Columns)
                c.Width = CELLSIZE;

            // rows and columns should never resize themselves to fit cell contents
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // prevent user from resizing rows or columns
            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;

            //putting image in pacman first
            pacman = pacmanr1;
        }

        public void Draw()//draw the maze and switching the type of image depending on the value of the map
        {
            for (int r = 0; r < NROWSCOLUMNS; r++)
            {
                for (int c = 0; c < NROWSCOLUMNS; c++)
                {
                    switch (pacmap[r, c])
                    {
                        case (int)eBlocks.WALL:
                            Rows[r].Cells[c].Value = wall;
                            break;
                        case (int)eBlocks.KIBBLE:
                            Rows[r].Cells[c].Value = kibble;
                            break;
                        case (int)eBlocks.BLANK:
                            Rows[r].Cells[c].Value = blank;
                            break;
                        case (int)eBlocks.GREEN:
                            Rows[r].Cells[c].Value = green;
                            break;
                        case (int)eBlocks.PINK:
                            Rows[r].Cells[c].Value = pink;
                            break;
                        case (int)eBlocks.ORANGE:
                            Rows[r].Cells[c].Value = orange;
                            break;
                        case (int)eBlocks.RED:
                            Rows[r].Cells[c].Value = red;
                            break;
                        case (int)eBlocks.PACMAN:
                            Rows[r].Cells[c].Value = pacman;
                            break;
                        case 8:
                            Rows[r].Cells[c].Value = redvelvet;
                            break;
                        default:
                            MessageBox.Show("Unidentified value in string");
                            break;
                    }
                }
            }
        }

        public void PacAnimation(bool mouthopen)//changing the image of pacman according to the direction
        {
            if (mouthopen == true)
            {
                switch (direction)
                {
                    case (int)eDirection.LEFT:
                        pacman = pacmanl1;
                        break;
                    case (int)eDirection.RIGHT:
                        pacman = pacmanr1;
                        break;
                    case (int)eDirection.DOWN:
                        pacman = pacmand1;
                        break;
                    case (int)eDirection.UP:
                        pacman = pacmanu1;
                        break;
                }
            }
            if (mouthopen == false)
            {
                switch (direction)
                {
                    case (int)eDirection.LEFT:
                        pacman = pacmanl2;
                        break;
                    case (int)eDirection.RIGHT:
                        pacman = pacmanr2;
                        break;
                    case (int)eDirection.DOWN:
                        pacman = pacmand2;
                        break;
                    case (int)eDirection.UP:
                        pacman = pacmanu2;
                        break;
                }
            }
        }

        public void GhoulsRunImage(bool runtime)//swapping the ghoul image between runtime image or normal image depending on when the pacman eats the cherry
        {
            if (runtime)
            {
                red = ghoulrunImg;
                green = ghoulrunImg;
                pink = ghoulrunImg;
                orange = ghoulrunImg;
            }
            if (runtime == false)
            {
                red = orired;
                green = origreen;
                pink = oripink;
                orange = oriorange;
            }
        }

        //properties
        public int[,] Pacmap { get => pacmap; set => pacmap = value; }
        public int Direction { get => direction; set => direction = value; }
        public Bitmap Pacman { get => pacman; set => pacman = value; }
    }
}
