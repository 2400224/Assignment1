using System;
using System.IO;
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
        //create menu object
        MenuStrip menu = new MenuStrip();

        //create instance of start screen
        startScreen start = new startScreen();

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

        //create filepath for high score table
        string path = @"score.txt";

        //variable for computer move
        Random r = new Random();

        public Form1()
        {
            InitializeComponent();

            //configure menu options
            menu.BackColor = Color.LightGray;//color 
            menu.ForeColor = Color.White;//text color
            menu.Font = textFont;//font used in menu
            MainMenuStrip = menu;
            Controls.Add(menu);//add to form
            menu.Name = "Menu";//set name
            menu.Dock = DockStyle.Bottom;//set menu to be at bottom

            //add about item to menu
            ToolStripMenuItem about = new ToolStripMenuItem("About");//create new item
            about.BackColor = Color.Gray;//colour
            about.Font = textFont;//font
            menu.Items.Add(about);//add to form
            about.Click += new System.EventHandler(this.about_Click);//link to event handler

            //add how to play item to menu
            ToolStripMenuItem htp = new ToolStripMenuItem("How to Play");//create new item
            htp.BackColor = Color.Gray;//colour
            htp.Font = textFont;//font
            menu.Items.Add(htp);//add to form
            htp.Click += new System.EventHandler(this.htp_Click);//link to event handler

            //add reset game item to menu
            ToolStripMenuItem reset = new ToolStripMenuItem("Restart Game");//create new item
            reset.BackColor = Color.Gray;//colour
            reset.Font = textFont;//font
            menu.Items.Add(reset);//add to form
            reset.Click += new System.EventHandler(this.reset_Click);//link to event handler

            //add quit item to menu
            ToolStripMenuItem quit = new ToolStripMenuItem("Quit to Menu");//create new item
            quit.BackColor = Color.Gray;//colour
            quit.Font = textFont;//font
            menu.Items.Add(quit);//add to form
            quit.Click += new System.EventHandler(this.quit_Click);//link to event handler

            
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

            DoTurns(index);

        }

        private void DoTurns(string[] coords)
        {
            if (turn == false)//if Player 1's turn
            {
                //update the moves counter 
                p1MoveCounter = p1MoveCounter + 1;
                p1Moves.Text = "P1 Moves: " + p1MoveCounter;


                //covert index to int
                int i = Int32.Parse(coords[0]);
                int j = Int32.Parse(coords[1]);

                //call funtion to place colours
                placeColour(i, j);

                //check for win
                winCheckerP1();



            }
            else if (turn == true)//if Player 2's turn
            {
                //update the moves counter 
                p2MoveCounter = p2MoveCounter + 1;
                p2Moves.Text = "P2 Moves: " + p2MoveCounter;

                //covert index to int
                int i = Int32.Parse(coords[0]);
                int j = Int32.Parse(coords[1]);

                //call function to place colour
                placeColour(i, j);

                //check for win
                winCheckerP2();

            }

            //update turn boolean
            turn = !turn;

            if (turn && !start.vsPlayer)
            {
                //update the moves counter 
                p2MoveCounter = p2MoveCounter + 1;
                p2Moves.Text = "Comp Moves: " + p2MoveCounter;

                //computer does its move
                CompMove();

                //check for win
                winCheckerP2();

                //update turn boolean
                turn = !turn;
            }

            if (turn == false)//if Player 1's turn
            {
                //update turn indiator
                lbl.Text = "Player 1's Turn";
            }
            else if (turn == true)//if Player 2's turn
            {
                //update turn indicator
                lbl.Text = "Player 2's Turn";
            }
        }

        public void about_Click(object sender, System.EventArgs e)//event hander for 'about' item in the menu
        {
            MessageBox.Show("A simple connect 4 game made in C# for the first AC22005 Assignment © 2022 - Team 12", "About", MessageBoxButtons.OK);
        }

        public void htp_Click(object sender, System.EventArgs e)//event hander for 'How to Play' item in the menu
        {
            MessageBox.Show("This is a 2 Player Game where each player takes turns placing their colour on the board. The first player to get four of their colours in a row diagonally, horizontally, or vertically will win. The less moves it takes, the better!", "How to Play", MessageBoxButtons.OK);
        }

        public void quit_Click(object sender, System.EventArgs e)//event hander for 'quit' item in the menu
        {


            //show start screen
            start.Show();
            Close();
        }

        public void reset_Click(object sender, System.EventArgs e)//event handler for 'reset' item in the menu
        {
            //reset variables and lables
            turn = false;
            p1MoveCounter = 0;
            p2MoveCounter = 0;
            lbl.Text = "Player 1's Turn";
            p1Moves.Text = "P1 Moves: 0";
            p2Moves.Text = "P2 Moves: 0";
            

            //change all buttons back to white
            for (int x = 0; x < btn.GetLength(0); x++)//x loop
            {
                
                for (int y = 0; y < btn.GetLength(1); y++)//y loop
                {
                    btn[x, y].BackColor = Color.White; //set colour of buttons to white 
                }
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
                
                 while(btn[i,j].BackColor == Color.White)//keep incrementing j unil a square that isn't white is reached
                 {
                    j++;
                    if(j == btn.GetLength(1))//if bottom is reached while incrementing j
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
                        saveScore(p1MoveCounter);
                        //show start screen
                        start.Show();
                        Close();
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
                        saveScore(p1MoveCounter);
                        //show start screen
                        start.Show();
                        Close();
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
                        saveScore(p1MoveCounter);
                        //show start screen
                        start.Show();
                        Close();
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
                        saveScore(p1MoveCounter);
                        //show start screen
                        start.Show();
                        Close();
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
                        if (start.vsPlayer)
                        {
                            MessageBox.Show("Player 2 Wins with " + p2MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
                            saveScore(p2MoveCounter);
                            //show start screen
                            start.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Computer has won!", "LOST", MessageBoxButtons.OK);
                            //show start screen
                            start.Show();
                            Close();
                        }
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
                        if (start.vsPlayer)
                        {
                            MessageBox.Show("Player 2 Wins with " + p2MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
                            saveScore(p2MoveCounter);
                            //show start screen
                            start.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Computer has won!", "LOST", MessageBoxButtons.OK);
                            //show start screen
                            start.Show();
                            Close();
                        }
                    }

                }
            }

            // top left to bottom right diagonal check
            for (int x = 0; x < btn.GetLength(0) - 3; x++)
            {
                for (int y = 0; y < btn.GetLength(1) - 3; y++)
                {
                    if (btn[x, y].BackColor == Color.Yellow && btn[x + 1, y + 1].BackColor == Color.Yellow && btn[x + 2, y + 2].BackColor == Color.Yellow && btn[x + 3, y + 3].BackColor == Color.Yellow)
                    {
                        if (start.vsPlayer)
                        {
                            MessageBox.Show("Player 2 Wins with " + p2MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
                            saveScore(p2MoveCounter);
                            //show start screen
                            start.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Computer has won!", "LOST", MessageBoxButtons.OK);
                            //show start screen
                            start.Show();
                            Close();
                        }
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
                        if (start.vsPlayer)
                        {
                            MessageBox.Show("Player 2 Wins with " + p2MoveCounter + " moves!", "Congratulations", MessageBoxButtons.OK);
                            saveScore(p2MoveCounter);
                            //show start screen
                            start.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Computer has won!", "LOST", MessageBoxButtons.OK);
                            //show start screen
                            start.Show();
                            Close();
                        }
                    }
                }
            }


        }

        public void saveScore(int moveCounter) // function to save highscore to file
        {
            //no highscores against the computer
            if (!start.vsPlayer)
            {
                return;
            }

            start.loadScore();

            // Check the current turn
            // Then check if the winning score is better than previous scores.
            if (!turn)
            {
                if (moveCounter < start.p1BestScore || start.p1BestScore == 0)
                {
                    FileInfo f = new FileInfo(path);
                    StreamWriter w = f.CreateText();

                    start.p1BestScore = moveCounter;
                    w.WriteLine("1 " + moveCounter);
                    w.WriteLine("2 " + start.p2BestScore);
                    w.Close();
                }
            }
            else
            {
                if (moveCounter < start.p2BestScore || start.p2BestScore == 0)
                {
                    FileInfo f = new FileInfo(path);
                    StreamWriter w = f.CreateText();

                    start.p2BestScore = moveCounter;
                    w.WriteLine("1 " + start.p1BestScore);
                    w.WriteLine("2 " + moveCounter);
                    w.Close();
                }
            }
        }

        private void CompMove()
        {
            // Is valid when playing against the computer
            // and its the computer's turn.
            if (turn && !start.vsPlayer)
            {
                int winningMove = CompDetectMoves(Color.Orange);
                int blockingMove = CompDetectMoves(Color.Blue);

                if (winningMove != -1)
                {
                    placeColour(winningMove, 0);
                }
                else if (blockingMove != -1)
                {
                    placeColour(blockingMove, 0);
                }
                else
                {
                    int compMove = r.Next(btn.GetLength(0));

                    while (btn[compMove, 0].BackColor != Color.White)
                    {
                        compMove = r.Next(btn.GetLength(0));
                    }

                    placeColour(compMove, 0);
                }
            }
        }

        private int CompDetectMoves(Color c)
        {
            int matching;
            int empty;
            int x = -1;
            int y = 0;

            //check horizontals to block/win
            for (int i = 0; i < btn.GetLength(0) - 3; i++)
            {
                for (int j = 0; j < btn.GetLength(1); j++)
                {
                    matching = 0;
                    empty = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        if (btn[i + k, j].BackColor == c)
                        {
                            matching++;
                        }
                        else if (btn[i + k, j].BackColor == Color.White)
                        {
                            empty++;
                            x = i + k;
                            y = j;
                        }
                    }

                    if (matching == 3 && empty == 1)
                    {
                        //stop the computer from helping the player win
                        //by only blocking when on the same row
                        if ((y < 5 && btn[x, y + 1].BackColor != Color.White) || y == 5)
                        {
                            return x;
                        }
                        else
                        {
                            int move = r.Next(btn.GetLength(0));
                            if (move == x)
                            {
                                if (x == 6)
                                {
                                    x = r.Next(x);
                                }
                                else
                                {
                                    x += 1;
                                }
                            }
                            return x;
                        }
                    }
                }
            }


            //check verticals to block/win
            for (int i = 0; i < btn.GetLength(0); i++)
            {
                for (int j = 0; j < btn.GetLength(1) - 3; j++)
                {
                    if (btn[i, j].BackColor == Color.White &&
                        btn[i, j + 1].BackColor == c &&
                        btn[i, j + 2].BackColor == c &&
                        btn[i, j + 3].BackColor == c)
                    {
                        x = i;
                        return x;
                    }
                }
            }

            //check depressing diagonals to block/win
            for (int i = 0; i < btn.GetLength(0) - 3; i++)
            {
                for (int j = 0; j < btn.GetLength(1) - 3; j++)
                {
                    matching = 0;
                    empty = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        if (btn[i + k, j + k].BackColor == c)
                        {
                            matching++;
                        }
                        else if (btn[i + k, j + k].BackColor == Color.White)
                        {
                            empty++;
                            x = i + k;
                            y = j;
                        }
                    }

                    if (matching == 3 && empty == 1)
                    {
                        //stop the computer from helping the player win
                        //by only blocking when on the same row
                        if ((y < 5 && btn[x, y + 1].BackColor != Color.White) || y == 5)
                        {
                            return x;
                        }
                        else
                        {
                            int move = r.Next(btn.GetLength(0));
                            if (move == x)
                            {
                                if (x == 6)
                                {
                                    x = r.Next(x);
                                }
                                else
                                {
                                    x += 1;
                                }
                            }
                            return x;
                        }
                    }
                }
            }

            //check rising diagonals to block/win
            for (int i = btn.GetLength(0) - 1; i > 2; i--)
            {
                for (int j = 0; j < btn.GetLength(1) - 3; j++)
                {
                    matching = 0;
                    empty = 0;
                    for (int k = 3; k > -1; k--)
                    {
                        if (btn[i - k, j + k].BackColor == c)
                        {
                            matching++;
                        }
                        else if (btn[i - k, j + k].BackColor == Color.White)
                        {
                            empty++;
                            x = i - k;
                            y = j + k;
                        }
                    }

                    if (matching == 3 && empty == 1)
                    {
                        //stop the computer from helping the player win
                        //by only blocking when on the same row
                        if ((y < 5 && btn[x, y + 1].BackColor != Color.White) || y == 5)
                        {
                            return x;
                        }
                        else
                        {
                            int move = r.Next(btn.GetLength(0));
                            if (move == x)
                            {
                                if (x == 6)
                                {
                                    x = r.Next(x);
                                }
                                else
                                {
                                    x -= 1;
                                }
                            }
                            return x;
                        }
                    }
                }
            }

            x = -1;
            return x;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
