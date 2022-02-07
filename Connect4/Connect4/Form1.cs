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

        //create a 2D array that contains Button objects
        Button[,] btn = new Button[10,10];

        //Boolean used to keep track of turns (false = Player 1, True = Player 2)
        bool turn = false;

        //create new label Object
        Label lbl = new Label();

        //create an new Font object
        Font textFont = new Font("Arial", 12);

        public Form1()
        {
            InitializeComponent();

            
            //set of instructions to design and add a label to the form
            lbl.Location = new Point(60, 20);//location of label
            lbl.BackColor = Color.LightBlue;//label colour 
            lbl.Size = new Size(100, 30);//label size
            lbl.Name = "turn_Indicator";//name label
            lbl.Font = textFont;//set font of label
            lbl.Text = "Player 1's Turn";//set initial text
            Controls.Add(lbl);//add label to form


           
            //For loops that design and add the buttons to the form 
            for (int x = 0; x < btn.GetLength(0); x++)//x loop
            {
                for (int y = 0; y < btn.GetLength(1); y++)//y loop
                {
                    btn[x, y] = new Button(); //create the button
                    btn[x,y].SetBounds(55+(55 * x), 55+(55* y), 45, 45); //set size and position of the buttons
                    btn[x, y].BackColor = Color.White; //set colour of buttons to white
                    Controls.Add(btn[x,y]); //add buttons to form
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
                //set colour
                ((Button)sender).BackColor = Color.Yellow;

                //check for win
                winCheckerP1();
                
                    
                
            }
            else if(turn == true)
            {
                //set colour
                ((Button)sender).BackColor = Color.Red;

                //check for win
                winCheckerP2();
                
            }

            //update turn boolean
            turn = !turn;

            if (turn == false)
            {
                lbl.Text = "Player 1's Turn";
            }
            else if (turn == true)
            {
                lbl.Text = "Player 2's Turn";
            }

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
                    if(btn[x,y].BackColor == Color.Yellow && btn[x + 1, y].BackColor == Color.Yellow && btn[x + 2, y].BackColor == Color.Yellow && btn[x + 3, y].BackColor == Color.Yellow)
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
                    if (btn[x, y].BackColor == Color.Yellow && btn[x, y+1].BackColor == Color.Yellow && btn[x, y + 2].BackColor == Color.Yellow && btn[x, y + 3].BackColor == Color.Yellow)
                    {
                        MessageBox.Show("Player 1 Wins!", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }

            // top left to bottom right diagonal check
            for (int x= 0; x < btn.GetLength(0)-3; x++)
            {
                for (int y = 0; y < btn.GetLength(1)-3; y++)
                {
                    if (btn[x, y].BackColor == Color.Yellow && btn[x + 1, y + 1].BackColor == Color.Yellow && btn[x + 2, y + 2].BackColor == Color.Yellow && btn[x + 3, y + 3].BackColor == Color.Yellow)
                    {
                        MessageBox.Show("Player 1 Wins!", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }

            //bottom left to top right diagonal check
            for (int x = 0; x < btn.GetLength(0) - 3; x++)
            {
                for (int y = 3; y < btn.GetLength(1); y++)
                {
                    if (btn[x, y].BackColor == Color.Yellow && btn[x + 1, y - 1].BackColor == Color.Yellow && btn[x + 2, y - 2].BackColor == Color.Yellow && btn[x + 3, y - 3].BackColor == Color.Yellow)
                    {
                        MessageBox.Show("Player 1 Wins!", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }


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

            // top left to bottom right diagonal check
            for (int x = 0; x < btn.GetLength(0) - 3; x++)
            {
                for (int y = 0; y < btn.GetLength(1) - 3; y++)
                {
                    if (btn[x, y].BackColor == Color.Red && btn[x + 1, y + 1].BackColor == Color.Red && btn[x + 2, y + 2].BackColor == Color.Red && btn[x + 3, y + 3].BackColor == Color.Red)
                    {
                        MessageBox.Show("Player 2 Wins!", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }

            //bottom left to top right diagonal check
            for (int x = 0; x < btn.GetLength(0) - 3; x++)
            {
                for (int y = 3; y < btn.GetLength(1); y++)
                {
                    if (btn[x, y].BackColor == Color.Red && btn[x + 1, y - 1].BackColor == Color.Red && btn[x + 2, y - 2].BackColor == Color.Red && btn[x + 3, y - 3].BackColor == Color.Red)
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
