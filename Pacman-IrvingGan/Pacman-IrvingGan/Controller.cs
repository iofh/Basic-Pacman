//this is the game controller class. the purpose of this class is to gather all the other class such as maze, pacman, ghouls class
// in one controller class. so the form will only need to talk to the controller.
// the controller class also does most of the processing on the game
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman_IrvingGan
{
    public class Controller
    {
        //constance fields
        private const int NEXTSTEP = 1;
        private const int TOTALKIBBLE = 189;
        private const int PACORIX = 12;
        private const int PACORIY = 9;
        private const int REDORIX = 10;
        private const int REDORIY = 9;
        private const int ORGORIX = 9;
        private const int ORGORIY = 9;
        private const int PINKORIX = 10;
        private const int PINKORIY = 8;
        private const int GREENORIX = 10;
        private const int GREENORIY = 10;
        private const int KIBBLEPOINT = 10;
        private const int GHOULPOINT = 50;
        private const int LIFE=3;
        //private const int TOTALKIBBLE = 28;

        //fields
        private Point redStartPosition;
        private Point greenStartPosition;
        private Point pinkStartPosition;
        private Point orangeStartPosition;
        private Maze maze;
        private Pacman pac;
        private int score;
        private bool isDead;
        private bool stillnotwin;
        private int ghouliseatencount;
        private List<Ghouls> ghoulses;
        private bool mouthopen;
        private int life;
        private bool chasetime;
        private SoundPlayer chomp;
        private SoundPlayer collided;

        //constructor
        public Controller()
        {
            life = LIFE;
            redStartPosition = new Point(REDORIX, REDORIY);
            greenStartPosition = new Point(GREENORIX, GREENORIX);
            pinkStartPosition = new Point(PINKORIX, PINKORIY);
            orangeStartPosition = new Point(ORGORIX, ORGORIY);
            maze = new Maze();
            pac = new Pacman(maze.Pacmap, new Point(PACORIX, PACORIY), (int)eBlocks.PACMAN);
            ghoulses = new List<Ghouls>();
            ghoulses.Add(new Ghouls(maze.Pacmap, redStartPosition, (int)eBlocks.RED, (int)eBlocks.BLANK));//0
            ghoulses.Add(new Ghouls(maze.Pacmap, pinkStartPosition, (int)eBlocks.PINK, (int)eBlocks.BLANK));//1
            ghoulses.Add(new Ghouls(maze.Pacmap, orangeStartPosition, (int)eBlocks.ORANGE, (int)eBlocks.BLANK));//2
            ghoulses.Add(new Ghouls(maze.Pacmap, greenStartPosition, (int)eBlocks.GREEN, (int)eBlocks.BLANK));//3
            isDead = false;
            stillnotwin = true;
            mouthopen = true;
            chasetime = false;
            ghouliseatencount = 0;
            chomp = new SoundPlayer(Properties.Resources.pacman_chomp);
            collided = new SoundPlayer(Properties.Resources.roblox_death_sound_effect);
        }

        public void LoseOneLife()//this method is to move pacman and the ghouls back to the original position when pacman is eaten by the ghouls
        {
            CleanUpGrid();
            pac.Position = new Point(PACORIX, PACORIY);
            ghoulses[0].Position = new Point(REDORIX, REDORIY);
            ghoulses[1].Position = new Point(PINKORIX, PINKORIY);
            ghoulses[2].Position = new Point(ORGORIX, ORGORIY);
            ghoulses[3].Position = new Point(GREENORIX, GREENORIY);
            isDead = false;
            stillnotwin = true;
            mouthopen = true;
        }

        public void CleanUpGrid()//the purpose of this method is to reset the block type value of the ghouls so that the spawn area of the ghouls will not have new kibble popping out
        {
            for (int i = 0; i < ghoulses.Count; i++)
            {
                maze.Pacmap[ghoulses[i].Position.X, ghoulses[i].Position.Y] = ghoulses[i].Current;
                ghoulses[i].Current = (int)eBlocks.BLANK;
                ghoulses[i].Previousstepblocktype = (int)eBlocks.BLANK;
            }
            maze.Pacmap[pac.Position.X, pac.Position.Y] = (int)eBlocks.BLANK;
            maze.Pacmap[9, 8] = (int)eBlocks.BLANK;//blank area of the spawn 
            maze.Pacmap[9, 10] = (int)eBlocks.BLANK;
            maze.Pacmap[8, 8] = (int)eBlocks.BLANK;
            maze.Pacmap[8, 9] = (int)eBlocks.BLANK;
            maze.Pacmap[8, 10] = (int)eBlocks.BLANK;
        }

        public void Run()//the timertick on the form will run this method. this method is used to gather other method is the controller class and run it in one single method in the timer
        {
            maze.Draw();       
            pacmanAnimation();            
            CheckforCollided();
            checkscore();
            checkForPowerUP();
            checkforwin();           
        }

        public void CheckforCollided()//this method checks for the pacman and ghouls position and whether they collided or not
        {
            for (int i = 0; i < ghoulses.Count; i++)
            {
                if (pac.Position == ghoulses[i].Position)
                {                
                    if (pac.Powerup == true)
                    {
                        if (ghoulses[i].Current == (int)eBlocks.KIBBLE || ghoulses[i].Current == (int)eBlocks.CHERRY)
                        {
                            pac.Kibbleeaten++;
                        }
                        ghoulses[i].Position = redStartPosition;
                        ghoulses[i].Current = (int)eBlocks.BLANK;
                        ghoulses[i].Previousstepblocktype = (int)eBlocks.BLANK;
                        ghouliseatencount += 1;
                    }
                    if (pac.Powerup == false)
                    {
                        isDead = true;
                        collided.PlaySync();
                        life -= 1;
                        if (ghoulses[i].Current == (int)eBlocks.KIBBLE || ghoulses[i].Current == (int)eBlocks.CHERRY)
                        {
                            pac.Kibbleeaten++;
                        }
                    }                   
                }
            }
        }

        private void checkForPowerUP()//this method checks for whether or not pacman has eaten the cherry or not
        {
            if (pac.Powerup == true)
            {
                chasetime = true;
                maze.GhoulsRunImage(true);
            }
            if (pac.Powerup == false)
            {
                maze.GhoulsRunImage(false);
            }
        }

        private void checkscore()//this method updates the score
        {
            score = (pac.Kibbleeaten * KIBBLEPOINT) + (ghouliseatencount * GHOULPOINT);           
        }

        private void checkforwin()//ths method checks for whether the pacman has eaten all the kibble or not
        {
            if (pac.Kibbleeaten == TOTALKIBBLE)
            {
                stillnotwin = false;
            }
        }

        public void Chase()//this method makes the ghouls chase pacman
        {
            for (int i = 0; i < ghoulses.Count; i++)
            {
                if (ghoulses[i].Position.Y < pac.Position.Y)
                {
                    ghoulses[i].GhoulsMove((int)eDirection.RIGHT);
                }
                if (ghoulses[i].Position.Y > pac.Position.Y)
                {
                    ghoulses[i].GhoulsMove((int)eDirection.LEFT);
                }
                if (ghoulses[i].Position.X > pac.Position.X)
                {
                    ghoulses[i].GhoulsMove((int)eDirection.UP);
                }
                if (ghoulses[i].Position.X < pac.Position.X)
                {
                    ghoulses[i].GhoulsMove((int)eDirection.DOWN);
                }
            }
        }

        public void GhoulsRun()//this method is used when pacman eats the cherry and the ghouls will run away from pacman
        {
            for (int i = 0; i < ghoulses.Count; i++)
            {
                if (ghoulses[i].Position.Y > pac.Position.Y)
                {
                    ghoulses[i].GhoulsMove((int)eDirection.RIGHT);
                }
                if (ghoulses[i].Position.Y < pac.Position.Y)
                {
                    ghoulses[i].GhoulsMove((int)eDirection.LEFT);
                }
                if (ghoulses[i].Position.X < pac.Position.X)
                {
                    ghoulses[i].GhoulsMove((int)eDirection.UP);
                }
                if (ghoulses[i].Position.X > pac.Position.X)
                {
                    ghoulses[i].GhoulsMove((int)eDirection.DOWN);
                }
            }
        }

        private void pacmanAnimation()//this method is used to just animate the pacman mouth
        {
            mouthopen = !mouthopen;
            maze.PacAnimation(mouthopen);
            chomp.Play();
        }

        //properties
        public Pacman Pac { get => pac; set => pac = value; }
        internal Maze Maze { get => maze; set => maze = value; }
        public bool IsDead { get => isDead; set => isDead = value; }
        public bool Stillnotwin { get => stillnotwin; set => stillnotwin = value; }
        public int Score { get => score; set => score = value; }
        public List<Ghouls> Ghoulses { get => ghoulses; set => ghoulses = value; }
        public int Life { get => life; set => life = value; }
        public bool Chasetime { get => chasetime; set => chasetime = value; }
    }
}
