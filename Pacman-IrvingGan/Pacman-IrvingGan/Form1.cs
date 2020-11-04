/* Program name: 	    PacMan
   Project file name:
   Author:		    Irving Gan
   Date:	 
   Language:		    C#
   Platform:		    Microsoft Visual Studio 2013
   Purpose:		    This program lets the user control the pacman and eats kibble, and not let the ghouls eats the pacman
   Description:		    the game uses 1 timer to randomly move the ghouls. the game lets the users have powerup when the pacman eats the cherry
   Known Bugs:		    pacman move a few steps or not show the grid completely before the initial music plays, it depends on the processing speed of the computer. 
   Additional Features: pause, menus, powerup, sounds, extra lives, instructions, ghouls chase pacman, bonus points when pacman eats ghouls, portal
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman_IrvingGan
{
    public partial class Form1 : Form
    {
        //constance for the form height and width
        private const int FORMHEIGHT = 746;
        private const int FORMWIDTH = 560;
        private const int ghoulsMoveDuration=3;
        private const int ghoulsRunDuration =50;

        //fields
        private int ghoulsbehavior;
        private Controller controller;
        private Random random;
        private char autorun;
        private int ghoulsruncount;
        private int ghoulsmovecount;
        private Menu menu;
        
        private SoundPlayer endgame;
        private SoundPlayer winsound;

        public Form1()//constructor
        {
            InitializeComponent();            
            endgame = new SoundPlayer(Properties.Resources.EndGame);
            winsound = new SoundPlayer(Properties.Resources.win);
            Top = 0;
            Left = 0;
            Height = FORMHEIGHT;
            Width = FORMWIDTH;
            controller = new Controller();
            random = new Random();
            Controls.Add(controller.Maze);            
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            controller.Maze.CellBorderStyle = DataGridViewCellBorderStyle.None;
            controller.Maze.Enabled = false;
            autorun = 'r';
            timer1.Enabled = true;           
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            pacmanautomove();         
            controller.Run();            
            updateLabel();
            ghoulsmovecount++;
            if (ghoulsmovecount == ghoulsMoveDuration)
            {
                ghoulmovetick();
                ghoulsmovecount = 0;
            }
            if (controller.Chasetime == true)
            {
                ghoulsbehavior = 6;//6 is the bahaviour that makes the ghouls run away from pacman
                ghoulsruncount++;
                if(ghoulsruncount == ghoulsRunDuration)
                {
                    controller.Chasetime = false;
                    controller.Pac.Powerup = false;
                    ghoulsruncount = 0;
                }
            }
            else
            {
                ghoulbehaviortick();
            }
            checkifdeadOrwin();       
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    autorun = 'l';
                    break;
                case Keys.Right:
                    autorun = 'r';
                    break;
                case Keys.Down:
                    autorun = 'd';
                    break;
                case Keys.Up:
                    autorun = 'u';
                    break;
                case Keys.P:
                    timer1.Enabled = !timer1.Enabled;
                    break;
            }
        }

        public void GhoulsRandomMovement()//this method is use to move the ghouls randomly
        {
            for (int i = 0; i < controller.Ghoulses.Count; i++)
            {
                controller.Ghoulses[i].GhoulsMove(random.Next(1, 5));
            }
        }

        private void ghoulmovetick()//move the ghouls depending on the behavior
        {
            switch (ghoulsbehavior)
            {
                case 1:
                    controller.Chase();
                    break;
                case 6:
                    controller.GhoulsRun();
                    break;
                default:
                    GhoulsRandomMovement();
                    break;
            }
        }

        private void checkifdeadOrwin()//check whether the user lose or win
        {
            if (controller.IsDead == true)
            {
                if (controller.Life <= 0)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("you lost");
                    endgame.PlaySync();
                    menu = new Menu();
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    controller.LoseOneLife();

                }
            }
            if (controller.Stillnotwin == false)
            {
                timer1.Enabled = false;               
                MessageBox.Show("you won");
                winsound.PlaySync();
                menu = new Menu();
                menu.Show();
                this.Hide();
            }
        }
        private void updateLabel()//update the label with the score
        {
            label1.Text = "Score: " + controller.Score.ToString();
            label2.Text = "Lives: " + controller.Life.ToString();
        }

        private void pacmanautomove()//this method is use to make pacman move automaticly without having to keep pressing the arrow keys
        {
            switch (autorun)
            {
                case 'l':
                    controller.Pac.PacMove((int)eDirection.LEFT);
                    controller.Maze.Direction = (int)eDirection.LEFT;
                    autorun = 'l';
                    break;
                case 'r':
                    controller.Pac.PacMove((int)eDirection.RIGHT);
                    controller.Maze.Direction = (int)eDirection.RIGHT;
                    autorun = 'r';
                    break;
                case 'd':
                    controller.Pac.PacMove((int)eDirection.DOWN);
                    controller.Maze.Direction = (int)eDirection.DOWN;
                    autorun = 'd';
                    break;
                case 'u':
                    controller.Pac.PacMove((int)eDirection.UP);
                    controller.Maze.Direction = (int)eDirection.UP;
                    autorun = 'u';
                    break;
            }
        }

        private void ghoulbehaviortick()//this method is to change the ghouls behavior
        {
            ghoulsbehavior = random.Next(5);
        }
    }
}
