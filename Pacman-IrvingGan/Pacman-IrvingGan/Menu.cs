//menu form 
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
    public partial class Menu : Form
    {
        //fields
        private Form1 form1;
        private SoundPlayer startgame;

        public Menu()//constuctor
        {
            InitializeComponent();
            Left = 0;
            Top = 0;
            label2.Hide();
            button4.Hide();
            startgame = new SoundPlayer(Properties.Resources.intro);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1 = new Form1();//when start game create a new form1 which is the pacman game
            form1.Show();
            this.Hide();
            Application.DoEvents();
            startgame.PlaySync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();//exit the program
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Show();//when the main menu is click, show the other button
            button2.Show();
            button3.Show();
            button4.Hide();
            label1.Show();
            label2.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Hide();//when the instruction is click, show the instructions and hide the other buttons and label
            button2.Hide();
            button3.Hide();
            label1.Hide();
            label2.Show();
            button4.Show();
        }
    }
}
