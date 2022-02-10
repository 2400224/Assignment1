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
    public partial class startScreen : Form
    {
        //create the start button
        Button start = new Button();

        //create exit button
        Button exit = new Button();

        //create a label for the title
        Label title = new Label();

        //create filepath for high score table
        string path = @"score.txt";

        //create new label Objects
        Label p1BestMoves = new Label();
        Label pBest2Moves = new Label();
        Label noMoves = new Label();

        //create variables to store best scores
        public int p1BestScore;
        public int p2BestScore;

        //create variable to choose between playing against a player or the computer.
        public bool vsPlayer = true;

        public startScreen()
        {
            InitializeComponent();

            //add title to form
            title.BackColor = Color.Blue;//label colour
            title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;//set text alignment
            title.Size = new Size(400, 100);//label size
            title.Font = new Font("Berlin Sans FB", 50);//set font of label
            title.Text = "Connect 4";//text in label
            title.Location = new Point(110, 5);//location of label
            Controls.Add(title);//add to form

            //add start button to form
            start.BackColor = Color.Gray;//button colour
            start.Text = "Start";
            start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;//text alignment
            start.SetBounds(200, 200, 200, 70);//location and size
            start.Font = new Font("Berlin Sans FB", 10);//font
            start.Click += new EventHandler(this.title_Click);//link to event handler
            Controls.Add(start);//add to form

            //add exit button to form
            exit.BackColor = Color.Gray;//button colour
            exit.Text = "Exit";
            exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;//text alignment
            exit.SetBounds(200, 280, 200, 70);//location and size
            exit.Font = new Font("Berlin Sans FB", 10);//font
            exit.Click += new EventHandler(this.exit_Click);//link to event handler
            Controls.Add(exit);//add to form

            //add noMoves to form
            noMoves = labelDesign(noMoves);//call function that changes design settings 
            noMoves.Location = new Point(150, 130);//location
            noMoves.Text = "Play some games to see your highscores!";//inital text
            noMoves.AutoSize = true;
            Controls.Add(noMoves);//add to form

            //add p1Moves to form
            p1BestMoves = labelDesign(p1BestMoves);//call function that changes design settings 
            p1BestMoves.Location = new Point(190, 105);//location
            p1BestMoves.Text = "P1 Best Moves: ";//inital text
            Controls.Add(p1BestMoves);//add to form

            //add p2Moves to form
            pBest2Moves = labelDesign(pBest2Moves);//call function that changes design settings 
            pBest2Moves.Location = new Point(300, 105);//location
            pBest2Moves.Text = "P2 Best Moves: ";//inital text
            Controls.Add(pBest2Moves);//add to form

            vsPlayer = false;
        }

        public void title_Click(object sender, EventArgs e)
        {
            //create instance of form1
            Form1 gameScreen = new Form1();

            //load the game screen
            gameScreen.Show();
            this.Hide();

        }
        
        public void exit_Click(object sender, EventArgs e)
        {
            //exit the game
            Close();
        }

        public void loadScore()
        {
            // if the file doesn't exist dont show the scores yet
            if (!File.Exists(path))
            {
                p1BestMoves.Hide();
                pBest2Moves.Hide();
                noMoves.Show();
                return;
            }

            // opens a file for reading
            string[] readScores = new string[3];
            StreamReader s = File.OpenText(path);
            string read = null;

            // reads each line of the file, storing it in an array
            int i = 0;
            while ((read = s.ReadLine()) != null)
            {
                readScores[i] = read;
                i++;
            }
            s.Close();

            // splits the stored lines into different columns
            string[] fileData = new string[i];
            string[,] scores = new string[3, 2];
            for (int j = 0; j < readScores.Length; j++)
            {
                if (readScores[j] != null)
                {
                    fileData = readScores[j].Split(' ');
                    scores[j, 0] = fileData[0];
                    scores[j, 1] = fileData[1];
                }
            }

            // assign the values from the array to the score labels
            for (int k = 0; k < scores.GetLength(0); k++)
            {
                if (scores[k,0] == "1")
                {
                    p1BestMoves.Text = "P1 Best Moves: " + scores[k,1];
                    p1BestScore = Int32.Parse(scores[k, 1]);
                }
                else if (scores[k,0] == "2")
                {
                    pBest2Moves.Text = "P2 Best Moves: " + scores[k, 1];
                    p2BestScore = Int32.Parse(scores[k, 1]);
                }
            }

            //show the score table if there are values present
            if (p1BestScore == 0 && p2BestScore == 0)
            {
                p1BestMoves.Hide();
                pBest2Moves.Hide();
                noMoves.Show();
            }
            else
            {
                noMoves.Hide();
                p1BestMoves.Show();
                pBest2Moves.Show();
            }

        }

        public Label labelDesign(Label l)//function used to modify design options for the label objects 
        {
            Font textFont = new Font("Berlin Sans FB", 14);

            l.BackColor = Color.Blue;//label colour 
            l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;//set text alignment
            l.Size = new Size(110, 50);//label size
            l.Font = textFont;//set font of label
            return l;
        }

        private void startScreen_Load(object sender, EventArgs e)
        {
            //loads in the highscore file before start screen is displayed
            loadScore();
        }
    }
}
