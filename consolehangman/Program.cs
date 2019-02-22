using System;
using System.Drawing;
using GDIDrawer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolehangman
{
    class Program
    {
        static void Main(string[] args)
        {    
            //Create Display Canvas
            CDrawer HangingPost = CreateDrawingWindow();

            //Game Title Screen
            MainMenu(ref HangingPost);

            // <MOVIE TITLE> = GetWord();

            //////TEST CASE
            string test = "Pirates of the Caribbean: Curse of the Black Pearl";
            //////TEST CASE

            //Player interactions with GDIDrawer here
            PlayWindow(ref HangingPost, test);

            Console.Read();


            /*SAMPLE TEMPLATE OF MAIN PROGRAM
            //do
            //{
            //    Console.Clear();
            //    Console.WriteLine("\t\t\tLet's Hang Someone!\n");

            //    MainMenu();                     //Dispaly Main Screen of Game

            //    ChooseWord();                   //RNG a word or phrase from wordbank

            //    DrawHangingPost();              //Draw base hanging template and spaces based on word chosen

            //    do
            //    {
            //        UserInput();                //Get and check user input. Check to see if user had already guessed the letter before.

            //        UpdateHangingPost();        //Fill in spaces / hang more man =======> Assign game_won flag here

            //    } while (!game_over);

            //} while (play_game);
            */
        }

        // Set up drawing canvas
        static CDrawer CreateDrawingWindow()
        {
            CDrawer Canvas = new CDrawer();
            Canvas.ContinuousUpdate = false;
            Canvas.RedundaMouse = true;
            Canvas.Scale = 1;
            for (int row = 0; row < 800; row++)
            {
                for (int col = 0; col < 600; col++)
                {
                    Canvas.SetBBPixel(row, col, Color.SkyBlue);
                }
            }
            //Some sandy ground color
            for (int row = 0; row < 800; row++)
            {
                for (int col = 400; col < 600; col++)
                {
                    Canvas.SetBBPixel(row, col, Color.SandyBrown);
                }
            }
            //A sun, perhaps?
            int a = 85, b = 85;
            for (int r = 1; r <= 40; r++)
            {
                for (double theta = 0.0; theta < 2 * Math.PI; theta += 0.0174533 / 2)
                {
                    Canvas.SetBBPixel((int)(r * Math.Cos(theta) + a), (int)(r * Math.Sin(theta) + b), Color.Yellow);
                }
            }
            //Draw hanging post, SetBBPixel?
            for (int x = 245; x <= 255; x++)
            {
                for (int y = 125; y <= 215; y++)
                {
                    Canvas.SetBBPixel(x, y, Color.Brown);
                }
            }
            for (int x = 150; x <= 250; x++)
            {
                for (int y = 125; y <= 135; y++)
                {
                    Canvas.SetBBPixel(x, y, Color.Brown);
                }
            }
            for (int x = 145; x <= 155; x++)
            {
                for (int y = 125; y <= 525; y++)
                {
                    Canvas.SetBBPixel(x, y, Color.Brown);
                }
            }
            for (int x = 100; x <= 200; x++)
            {
                for (int y = 520; y <= 530; y++)
                {
                    Canvas.SetBBPixel(x, y, Color.Brown);
                }
            }
            return Canvas;
        }

        // Main Menu Screen
        static void MainMenu(ref CDrawer HangingPost)
        {
            bool play_game = false;
            Point click;

            HangingPost.Clear();
            //Draw hanged man, can use this for reference later
            HangingPost.AddCenteredEllipse(270, 215, 50, 50, Color.Black);      //head
            HangingPost.AddLine(250, 215, 245, 285, Color.Black, 5);            //torso
            HangingPost.AddLine(250, 215, 230, 305, Color.Black, 5);            //right arm
            HangingPost.AddLine(250, 215, 270, 305, Color.Black, 5);            //left arm
            HangingPost.AddLine(245, 285, 230, 365, Color.Black, 5);            //right leg
            HangingPost.AddLine(245, 285, 265, 365, Color.Black, 5);            //left leg

            ///Add peanut gallery
            AddStickMan(60, 380, "Black", ref HangingPost, true);
            AddStickMan(160, 420, "Black", ref HangingPost, true);
            AddStickMan(300, 400, "Black", ref HangingPost, true);
            AddStickMan(420, 420, "Black", ref HangingPost, true);

            HangingPost.AddText("Let's Hang Someone!", 36, 0, 0, 800, 100, Color.DarkRed);
            HangingPost.AddText("Click anywhere to begin...", 12, 200, 500, 800, 100);
            HangingPost.Render();

            while (!play_game)
            {
                if (HangingPost.GetLastMouseLeftClick(out click))
                {
                    play_game = true;
                }
            }
            HangingPost.Clear();
            // Idle peanut gallery
            AddStickMan(60, 380, "Black", ref HangingPost, false);
            AddStickMan(160, 420, "Black", ref HangingPost, false);
            AddStickMan(300, 400, "Black", ref HangingPost, false);
            AddStickMan(420, 420, "Black", ref HangingPost, false);
            HangingPost.Render();
        }

        // Add a stick figure to drawing window with head at (x, y) in cheering pose or idle pose
        static void AddStickMan(int x, int y, string color, ref CDrawer HangingPost, bool cheer)
        {
            if (cheer)
            {
                HangingPost.AddCenteredEllipse(x, y, 50, 50, Color.FromName(color));            //head
                HangingPost.AddLine(x, y + 25, x, y + 95, Color.FromName(color), 5);            //torso
                HangingPost.AddLine(x, y + 40, x - 40, y - 5, Color.FromName(color), 5);        //left upper arm
                HangingPost.AddLine(x - 40, y - 5, x - 60, y - 40, Color.FromName(color), 5);   //left lower arm
                HangingPost.AddLine(x, y + 40, x + 40, y - 5, Color.FromName(color), 5);        //right upper arm
                HangingPost.AddLine(x + 40, y - 5, x + 60, y - 40, Color.FromName(color), 5);   //right lower arm
                HangingPost.AddLine(x, y + 95, x - 15, y + 180, Color.FromName(color), 5);      //left leg
                HangingPost.AddLine(x, y + 95, x + 15, y + 180, Color.FromName(color), 5);      //right leg
            }
            if (!cheer)
            {
                HangingPost.AddCenteredEllipse(x, y, 50, 50, Color.FromName(color));            //head
                HangingPost.AddLine(x, y + 25, x, y + 95, Color.FromName(color), 5);            //torso
                HangingPost.AddLine(x, y + 30, x - 25, y + 105, Color.FromName(color), 5);      //left arm
                HangingPost.AddLine(x, y + 30, x + 25, y + 105, Color.FromName(color), 5);      //right arm
                HangingPost.AddLine(x, y + 95, x - 15, y + 180, Color.FromName(color), 5);      //left leg
                HangingPost.AddLine(x, y + 95, x + 15, y + 180, Color.FromName(color), 5);      //right leg
            }
        }

        // Set up playing window
        static void PlayWindow(ref CDrawer HangingPost, string title)
        {
            title = title.ToUpper();
            int length = title.Length;

            //Draw blank spaces if word fits in screen.  Text centered at 300 < x < 800, 50 < y < 550
            if (length <= 20)
            {
                string blanks = "";
                foreach (char ch in title)
                {
                    if (ch == ' ')
                    {
                        blanks += "   ";
                    }
                    else if (ch == ':')
                    {
                        blanks += ":";
                    }
                    else if (char.IsDigit(ch))
                    {
                        blanks += ch;
                    }
                    else
                    {
                        blanks += "_ ";
                    }
                }
                HangingPost.AddText(blanks, 22, 300, 50, 500, 400, Color.Black);
            }
            //Slightly longer titles
            else if (length > 20 & length <= 35)
            {
                // search for space in title close to index 15, and split string into 2
                string line1, line2;
                int space_index = title.IndexOf(' ', 15);
                if (space_index > 20 || space_index == -1)
                {
                    space_index = title.IndexOf(' ', 13);
                    if (space_index > 20)
                    {
                        space_index = title.IndexOf(' ', 11);        //This is worst case.... I think.
                    }
                }
                line1 = title.Substring(0, space_index);
                line2 = title.Substring(space_index);

                string blanks1 = "";
                foreach (char ch in line1)
                {
                    if (ch == ' ')
                    {
                        blanks1 += "   ";
                    }
                    else if (ch == ':')
                    {
                        blanks1 += ":";
                    }
                    else if (char.IsDigit(ch))
                    {
                        blanks1 += ch;
                    }
                    else
                    {
                        blanks1 += "_ ";
                    }
                }
                HangingPost.AddText(blanks1, 22, 300, 50, 500, 250, Color.Black);
                string blanks2 = "";
                foreach (char ch in line2)
                {
                    if (ch == ' ')
                    {
                        blanks2 += "   ";
                    }
                    else if (ch == ':')
                    {
                        blanks2 += ":";
                    }
                    else if (char.IsDigit(ch))
                    {
                        blanks2 += ch;
                    }
                    else
                    {
                        blanks2 += "_ ";
                    }
                }
                HangingPost.AddText(blanks2, 22, 300, 50, 500, 400, Color.Black);
            }
            //Really long titles, do I need to go more?
            else if (length > 35 && length <= 55)
            {
                string line1, line2, line3;
                int space_index1, space_index2;
                space_index1 = title.IndexOf(' ', 15);
                if (space_index1 > 20 || space_index1 == -1)
                {
                    space_index1 = title.IndexOf(' ', 12);
                    if (space_index1 > 20)
                    {
                        space_index1 = title.IndexOf(' ', 10);           //This really should never happen....
                    }
                }
                space_index2 = title.IndexOf(' ', space_index1 + 20);
                if (space_index2 > space_index1 + 20)
                {
                    space_index2 = title.IndexOf(' ', space_index1 + 15);
                    if (space_index2 > space_index1 + 20)
                    {
                        space_index2 = title.IndexOf(' ', space_index1 + 10);
                    }
                }
                line1 = title.Substring(0, space_index1);
                line2 = title.Substring(space_index1, space_index2 - space_index1);
                line3 = title.Substring(space_index2);

                string blanks1 = "";
                foreach (char ch in line1)
                {
                    if (ch == ' ')
                    {
                        blanks1 += "   ";
                    }
                    else if (ch == ':')
                    {
                        blanks1 += ":";
                    }
                    else if (char.IsDigit(ch))
                    {
                        blanks1 += ch;
                    }
                    else
                    {
                        blanks1 += "_ ";
                    }
                }
                HangingPost.AddText(blanks1, 22, 300, 50, 500, 250, Color.Black);
                string blanks2 = "";
                foreach (char ch in line2)
                {
                    if (ch == ' ')
                    {
                        blanks2 += "   ";
                    }
                    else if (ch == ':')
                    {
                        blanks2 += ":";
                    }
                    else if (char.IsDigit(ch))
                    {
                        blanks2 += ch;
                    }
                    else
                    {
                        blanks2 += "_ ";
                    }
                }
                HangingPost.AddText(blanks2, 22, 300, 50, 500, 400, Color.Black);
                string blanks3 = "";
                foreach (char ch in line3)
                {
                    if (ch == ' ')
                    {
                        blanks3 += "   ";
                    }
                    else if (ch == ':')
                    {
                        blanks3 += ":";
                    }
                    else if (char.IsDigit(ch))
                    {
                        blanks3 += ch;
                    }
                    else
                    {
                        blanks3 += "_ ";
                    }
                }
                HangingPost.AddText(blanks3, 22, 300, 50, 500, 550, Color.Black);
            }
        }
    }
    }
}
