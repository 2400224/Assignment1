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
        //create the 2D array of buttons
        Button[,] btn = new Button[6,7];

        //create int that keeps track of whose turn it is
        // false = Players turn, true = AI's Turn
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
            if (turn == false)
            {
                if (((Button)sender).BackColor == Color.Red || ((Button)sender).BackColor == Color.Blue)
                {
                    MessageBox.Show("This sqaure has already been chosen", "Invalid Move", MessageBoxButtons.OK);

                    //return so that it is still the player's turn
                    return;
                }
                else
                {
                    ((Button)sender).BackColor = Color.Blue;
                }

            }

             if (turn == true)
             {
               ((Button)sender).BackColor = Color.Red;
             }

            turn = !turn;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
