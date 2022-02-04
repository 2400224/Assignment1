using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Form1 : Form
    {
        //random object
        Random rnd = new Random();

        //create the 2D array of buttons
        Button[,] btn = new Button[6,7];

        //Keep track of turns (false = Player 1, True = Player 2
        bool turn = false;

        

        //boolean used in win condition
        bool hasWon = false;

        public Form1()
        {
            InitializeComponent();
            
            for (int x = 0; x < btn.GetLength(0); x++)//x loop
            {
                for (int y = 0; y < btn.GetLength(1); y++)//y loop
                {
                    btn[x, y] = new Button(); //create the button
                    btn[x,y].SetBounds(55+(55 * x), 55+(55* y), 45, 45); //set size and position of the buttons
                    btn[x, y].BackColor = Color.White; //set colour of buttons to white
                    Controls.Add(btn[x,y]); //add buttons to GUI
                    //link to event handler
                    btn[x, y].Click += new EventHandler(this.btnEvent_Click);
                    Controls.Add(btn[x, y]);

                }
            }
        }

        void btnEvent_Click(object sender, EventArgs e)//Event handler for  the buttons
        {
            //validate user entry
           if  (!validateUserChoice(sender))
            {
                MessageBox.Show("Button has already been chosen", "Invalid Input", MessageBoxButtons.OK);
                return;
            }

            if(turn == false)
            {
                
                ((Button)sender).BackColor = Color.Green;
            }
            else if(turn == true)
            {
                ((Button)sender).BackColor = Color.Red;
            }

            //update turn boolean
            turn = !turn;

        }

        public bool validateUserChoice(object sender)
        {
            if (((Button)sender).BackColor == Color.White)
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
