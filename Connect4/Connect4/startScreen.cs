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
    public partial class startScreen : Form
    {
        //create the start button
        Button start = new Button();

        //create exit button
        Button exit = new Button();

        //create a label for the title
        Label title = new Label();

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

        private void startScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
