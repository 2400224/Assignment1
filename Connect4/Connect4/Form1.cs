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

                //check for win
                winCheckerP1();
                
                    
                
            }
            else if(turn == true)
            {
                ((Button)sender).BackColor = Color.Red;

                //check for win
                winCheckerP2();
                
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

        public void winCheckerP1()
        {
            //Horizontal Check 
            for (int x = 0; x < btn.GetLength(0) - 3; x++)//-3 because you cant have 4 in a row on the last 3 positions so no need to check them 
            {
                for (int y = 0; y < btn.GetLength(1); y++)
                {
                    if(btn[x,y].BackColor == Color.Green && btn[x + 1, y].BackColor == Color.Green && btn[x + 2, y].BackColor == Color.Green && btn[x + 3, y].BackColor == Color.Green)
                    {
                        MessageBox.Show("Player 1 Wins!", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }

            //Vertical Check
            for (int y = 0; y < btn.GetLength(1) - 3; y++)//-3 because you cant have 4 in a row on the last 3 positions so no need to check them 
            {
                for (int x = 0; x < btn.GetLength(0); x++)
                {
                    if (btn[x, y].BackColor == Color.Green && btn[x, y+1].BackColor == Color.Green && btn[x, y + 2].BackColor == Color.Green && btn[x, y + 3].BackColor == Color.Green)
                    {
                        MessageBox.Show("Player 1 Wins!", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }

            //Top to bottom diagonal check

        }

        public void winCheckerP2()
        {
            //Horizontal Check 
            for (int x = 0; x < btn.GetLength(0) - 3; x++)//-3 because you cant have 4 in a row on the last 3 positions so no need to check them 
            {
                for (int y = 0; y < btn.GetLength(1); y++)
                {
                    if (btn[x, y].BackColor == Color.Red && btn[x + 1, y].BackColor == Color.Red && btn[x + 2, y].BackColor == Color.Red && btn[x + 3, y].BackColor == Color.Red)
                    {
                        MessageBox.Show("Player 2 Wins!", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }

            //Vertical Check
            for (int y = 0; y < btn.GetLength(1) - 3; y++)//-3 because you cant have 4 in a row on the last 3 positions so no need to check them 
            {
                for (int x = 0; x < btn.GetLength(0); x++)
                {
                    if (btn[x, y].BackColor == Color.Red && btn[x, y + 1].BackColor == Color.Red && btn[x, y + 2].BackColor == Color.Red && btn[x, y + 3].BackColor == Color.Red)
                    {
                        MessageBox.Show("Player 2 Wins!", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
