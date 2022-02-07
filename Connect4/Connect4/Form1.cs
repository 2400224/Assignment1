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

        //create new label Objects
        Label lbl = new Label();
        Label p1Moves = new Label();
        Label p2Moves = new Label();

        //ints used to count the turns
        int p1MoveCounter = 0;
        int p2MoveCounter = 0;
       

        //create an new Font object
        Font textFont = new Font("Berlin Sans FB", 14);

        public Form1()
        {
            InitializeComponent();

            //add p1Moves to form
            p1Moves = labelDesign(p1Moves);//call function that changes design settings 
            p1Moves.Location = new Point(56, 5);//location
            p1Moves.Text = "P1 Moves: 0";//inital text
            Controls.Add(p1Moves);//add to form

            //add p2Moves to form
            p2Moves = labelDesign(p2Moves);//call function that changes design settings 
            p2Moves.Location = new Point(496, 5);//location
            p2Moves.Text = "P2 Moves: 0";//inital text
            Controls.Add(p2Moves);//add to form


            //add lbl to form
            lbl = labelDesign(lbl);//call function that changes design settings
            lbl.Location = new Point(270, 5);//location of label
            lbl.Text = "Player 1's Turn";//set initial text
            Controls.Add(lbl);//add to form


           
            //For loops that design and add the buttons to the form 
            for (int x = 0; x < btn.GetLength(0); x++)//x loop
            {
                for (int y = 0; y < btn.GetLength(1); y++)//y loop
                {
                    btn[x, y] = new Button(); //create the button
                    btn[x,y].SetBounds(55+(55 * x), 55+(55* y), 45, 45); //set size and position of the buttons
                    btn[x, y].BackColor = Color.White; //set colour of buttons to white
                    btn[x, y].Tag = x + "," + y;//used later for getting the index of buttons
                    Controls.Add(btn[x,y]); //add buttons to form
                    //link to event handler
                    btn[x, y].Click += new EventHandler(this.btnEvent_Click);
                    Controls.Add(btn[x, y]);

                }
            }

           
        }

        void btnEvent_Click(object sender, EventArgs e)//Event handler for  the buttons
        {

            //get index of button clicked on - https://stackoverflow.com/questions/41285748/click-event-for-two-dimensional-button-array
            Button b = sender as Button;
            string[] index = b.Tag.ToString().Split(',');
            
            //validate user entry
            if  (!validateUserChoice(sender))
            {
                MessageBox.Show("Button has already been chosen", "Invalid Input", MessageBoxButtons.OK);
                return;
            }


            if(turn == false)
            {
                //update the moves counter 
                p1MoveCounter = p1MoveCounter + 1;
                p1Moves.Text = "P1 Moves: " + p1MoveCounter;


                //call function to place colour
                int i = Int32.Parse(index[0]);
                int j = Int32.Parse(index[1]);

                
                placeColour(i,j);

                //check for win
                winCheckerP1();
                
                    
                
            }
            else if(turn == true)
            {
                //update the moves counter 
                p2MoveCounter = p2MoveCounter + 1;
                p2Moves.Text = "P2 Moves: " + p2MoveCounter;

                //call function to place colour
                int i = Int32.Parse(index[0]);
                int j = Int32.Parse(index[1]);
                
                 placeColour(i, j);

                //check for win
                winCheckerP2();
                
            }

            //update turn boolean
            turn = !turn;

            if (turn == false)
            {
                //update turn indiator
                lbl.Text = "Player 1's Turn";
            }
            else if (turn == true)
            {
                //update turn indicator
                lbl.Text = "Player 2's Turn";
            }

        }

        public void placeColour(int i, int j)//function that ensures colours fall to the bottom when placed 
        {

            
            if ( j == btn.GetLength(1))//if user is placing on the bottom row
            {
                if (!turn)//set to either yellow or red depending on turn boolean
                {
                    btn[i, j - 1].BackColor = Color.Yellow;
                }
                else
                {
                    btn[i, j - 1].BackColor = Color.Red;
                }
                return;
            }
            else//if there is a blank space below where the player currently wants to place theirs
            {
                
                 while(btn[i,j].BackColor == Color.White)
                 {
                    j++;
                    if(j == btn.GetLength(1))//if bottom is reached
                    {
                        if (!turn)//set to either yellow or red depending on turn boolean
                        {
                            btn[i, j - 1].BackColor = Color.Yellow;
                        }else
                        {
                            btn[i, j - 1].BackColor = Color.Red;
                        }
                        return;
                    }
                 }

                if (!turn)//set to either yellow or red depending on turn boolean
                {
                    btn[i, j - 1].BackColor = Color.Yellow;
                }
                else
                {
                    btn[i, j - 1].BackColor = Color.Red;
                }

            }
            
        }

        public Label labelDesign(Label l)//function used to modify design options for the label objects 
        {
            l.BackColor = Color.Blue;//label colour 
            l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;//set text alignment
            l.Size = new Size(110, 50);//label size
            l.Font = textFont;//set font of label
            return l;
        }
        

        public bool validateUserChoice(object sender)//funtion that checks if a colour has already been placed in the currently selected square
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

        public void winCheckerP1()//function that checks if Player 1 has won 
        {
            //Horizontal Check 
            for (int x = 0; x < btn.GetLength(0) - 3; x++)//-3 because you cant have 4 in a row on the last 3 positions so no need to check them 
            {
                for (int y = 0; y < btn.GetLength(1); y++)
                {
                    if(btn[x,y].BackColor == Color.Yellow && btn[x + 1, y].BackColor == Color.Yellow && btn[x + 2, y].BackColor == Color.Yellow && btn[x + 3, y].BackColor == Color.Yellow)
                    {
                        MessageBox.Show("Player 1 Wins with " + p1MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
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
                        MessageBox.Show("Player 1 Wins with " + p1MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
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
                        MessageBox.Show("Player 1 Wins with " + p1MoveCounter + " moves! ", "Congratulations", MessageBoxButtons.OK);
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
                        MessageBox.Show("Player 1 Wins with " + p1MoveCounter + " moves! ", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }


        }

        public void winCheckerP2()//function that checks if player 2 has won
        {
            //Horizontal Check 
            for (int x = 0; x < btn.GetLength(0) - 3; x++)//-3 because you cant have 4 in a row on the last 3 positions so no need to check them 
            {
                for (int y = 0; y < btn.GetLength(1); y++)
                {
                    if (btn[x, y].BackColor == Color.Red && btn[x + 1, y].BackColor == Color.Red && btn[x + 2, y].BackColor == Color.Red && btn[x + 3, y].BackColor == Color.Red)
                    {
                        MessageBox.Show("Player 2 Wins with " + p2MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
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
                        MessageBox.Show("Player 2 Wins with " + p2MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
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
                        MessageBox.Show("Player 2 Wins with " + p2MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
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
                        MessageBox.Show("Player 2 Wins with " + p2MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
                    }
                }
            }


        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
