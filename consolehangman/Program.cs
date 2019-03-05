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
            string test = "Dragon Ball Super: Broly";
            //////TEST CASE

            //Player interactions with GDIDrawer here
            PlayWindow(ref HangingPost, test);

            //Return selected letter
            Point Rclick;
            do
            {
                Console.Write(SelectLetter(ref HangingPost));
            } while (!HangingPost.GetLastMouseRightClick(out Rclick));


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
            //rays... y= m(x-a) + b
            for (double theta = 0; theta < 2 * Math.PI; theta += Math.PI / 6)
            {
                for (int r = 50; r <= 70; r++)
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
            LetterButtons(ref HangingPost);
            // Draw blank spaces if word fits in screen.  Text centered at 300 < x < 800, 50 < y < 550
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
            // Longer titles
            else if (length > 20 & length <= 35)
            {
                // Search for space in title close to index 15, and split string into 2
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

        // Draw letter buttons
        static void LetterButtons(ref CDrawer HangingPost)
        {
            // Create Buttons A-M
            for (int a = 0; a < 13; a++)
            {
                HangingPost.AddRectangle((a * 40) + 250, 10, 30, 30, Color.Gray);
                HangingPost.AddText(((char)(a + 65)).ToString() + " ", 16, (a*40) + 250, 10, 30, 30, Color.Black);
            }
            // Create Buttons N-Z
            for (int a = 0; a < 13; a++)
            {
                HangingPost.AddRectangle((a * 40) + 250, 50, 30, 30, Color.Gray);
                HangingPost.AddText(((char)(a + 78)).ToString() + " ", 16, (a * 40) + 250, 50, 30, 30, Color.Black);
            }
        }

        // Player chooses letter
        static string SelectLetter(ref CDrawer HangingPost)
        {
            string selection = "";
            string[] letter = new string[26];
            int[] xmin_coord = new int[26];
            int[] ymin_coord = new int[26];
            int[] xmax_coord = new int[26];
            int[] ymax_coord = new int[26];
            // Populate string array with A-Z
            for (int i = 0; i < 26; i++)
            {
                letter[i] = ((char)(i + 65)).ToString();
            }
            int counter = 0;
            int xinit = 250;
            // Create 4 arrays that store the button locations with each indeci corresponding to previous A-Z array
            // ie: letter[0] = A, and xmin_coord[0] = left side of button
            while (counter < 13)
            {
                xmin_coord[counter] = xinit;
                xmax_coord[counter] = xinit + 30;
                xmin_coord[counter + 13] = xinit;
                xmax_coord[counter + 13] = xinit + 30;
                counter++;
                xinit += 40;
            }
            for (int i = 0; i < 13; i++)
            {
                ymin_coord[i] = 10;
                ymin_coord[i + 13] = 50;
                ymax_coord[i] = 40;
                ymax_coord[i + 13] = 80;
            }
            // Upon clicking, iterate through arrays and return selected character
            Point Lclick;
            bool valid_click = false;
            while (!valid_click)
            {
                if (HangingPost.GetLastMouseLeftClick(out Lclick))
                {
                    for (int i = 0; i < 26; i++)
                    {
                        if (Lclick.X > xmin_coord[i] && Lclick.X < xmax_coord[i] && Lclick.Y > ymin_coord[i] && Lclick.Y < ymax_coord[i])
                        {
                            HangingPost.AddLine(xmin_coord[i], ymin_coord[i], xmax_coord[i], ymax_coord[i], Color.Red, 2);
                            HangingPost.AddLine(xmax_coord[i], ymin_coord[i], xmin_coord[i], ymax_coord[i], Color.Red, 2);
                            HangingPost.Render();
                            selection = letter[i];
                            valid_click = true;
                        }
                    }
                }
                
            }
            return selection;
        }
    }
}
