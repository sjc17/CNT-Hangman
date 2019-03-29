using System;
using System.Drawing;
using GDIDrawer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolehangman
{
    class Drawings
    {
        // Set up drawing canvas
        public static CDrawer CreateDrawingWindow()
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
        public static void MainMenu(ref CDrawer HangingPost)
        {
            bool play_game = false;
            Point click;

            HangingPost.Clear();
            ////Draw hanged man, can use this for reference later
            //HangingPost.AddCenteredEllipse(270, 215, 50, 50, Color.Black);      //head
            //HangingPost.AddLine(250, 215, 245, 285, Color.Black, 5);            //torso
            //HangingPost.AddLine(250, 215, 230, 305, Color.Black, 5);            //right arm
            //HangingPost.AddLine(250, 215, 270, 305, Color.Black, 5);            //left arm
            //HangingPost.AddLine(245, 285, 230, 365, Color.Black, 5);            //right leg
            //HangingPost.AddLine(245, 285, 265, 365, Color.Black, 5);            //left leg

            ////Add peanut gallery
            //AddStickMan(60, 380, "Black", ref HangingPost, true);
            //AddStickMan(160, 420, "Black", ref HangingPost, true);
            //AddStickMan(300, 400, "Black", ref HangingPost, true);
            //AddStickMan(420, 420, "Black", ref HangingPost, true);

            //HangingPost.AddText("Let's Hang Someone!", 36, 0, 0, 800, 100, Color.DarkRed);

            //PG VERSION of Hangman
            HangingPost.AddCenteredEllipse(250, 245, 60, 60, Color.Empty, 5, Color.Black);      //head
            HangingPost.AddLine(250, 275, 250, 350, Color.Black, 5);                            //torso
            HangingPost.AddLine(250, 275, 220, 360, Color.Black, 5);                            //left arm
            HangingPost.AddLine(250, 275, 280, 360, Color.Black, 5);                            //right arm
            HangingPost.AddLine(250, 350, 230, 460, Color.Black, 5);                            //left leg
            HangingPost.AddLine(250, 350, 270, 460, Color.Black, 5);                            //right leg

            HangingPost.AddText("Let's Play Hangman!", 36, 0, 0, 800, 100, Color.DarkRed);
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
            //// Idle peanut gallery
            //AddStickMan(60, 380, "Black", ref HangingPost, false);
            //AddStickMan(160, 420, "Black", ref HangingPost, false);
            //AddStickMan(300, 400, "Black", ref HangingPost, false);
            //AddStickMan(420, 420, "Black", ref HangingPost, false);
            HangingPost.Render();
        }

        // Add a stick figure to drawing window with head at (x, y) in cheering pose or idle pose
        public static void AddStickMan(int x, int y, string color, ref CDrawer HangingPost, bool cheer)
        {
            HangingPost.AddCenteredEllipse(x, y, 50, 50, Color.FromName(color));            //head
            HangingPost.AddLine(x, y + 25, x, y + 95, Color.FromName(color), 5);            //torso
            HangingPost.AddLine(x, y + 95, x - 15, y + 180, Color.FromName(color), 5);      //left leg
            HangingPost.AddLine(x, y + 95, x + 15, y + 180, Color.FromName(color), 5);      //right leg

            if (cheer)
            {
                HangingPost.AddLine(x, y + 40, x - 40, y - 5, Color.FromName(color), 5);        //left upper arm
                HangingPost.AddLine(x - 40, y - 5, x - 60, y - 40, Color.FromName(color), 5);   //left lower arm
                HangingPost.AddLine(x, y + 40, x + 40, y - 5, Color.FromName(color), 5);        //right upper arm
                HangingPost.AddLine(x + 40, y - 5, x + 60, y - 40, Color.FromName(color), 5);   //right lower arm
            }
            else
            {
                HangingPost.AddLine(x, y + 30, x - 25, y + 105, Color.FromName(color), 5);      //left arm
                HangingPost.AddLine(x, y + 30, x + 25, y + 105, Color.FromName(color), 5);      //right arm
            }
        }

        // Set up playing window
        public static void PlayWindow(ref CDrawer HangingPost, string title)
        {
            title = title.ToUpper();
            int length = title.Length;
            LetterButtons(ref HangingPost);
            if (length <= 20)
            {
                int xpos = 300;
                int ypos = 200;
                foreach (char ch in title)
                {
                    if (char.IsLetter(ch))
                    {
                        HangingPost.AddText("_", 28, xpos, ypos, 28, 28, Color.Black);
                    }
                    else
                    {
                        HangingPost.AddText(ch.ToString(), 24, xpos, ypos - 4, 24, 24, Color.Black);
                    }
                    xpos += 28;
                }
            }
            else if (length < 35)
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

                int xpos = 300;
                int ypos1 = 200;
                int ypos2 = 260;

                foreach (char ch in line1)
                {
                    if (char.IsLetter(ch))
                    {
                        HangingPost.AddText("_", 28, xpos, ypos1, 28, 28, Color.Black);
                    }
                    else
                    {
                        HangingPost.AddText(ch.ToString(), 22, xpos, ypos1 - 4, 24, 24, Color.Black);
                    }
                    xpos += 28;
                }
                xpos = 300 - 28;
                foreach (char ch in line2)
                {
                    if (char.IsLetter(ch))
                    {
                        HangingPost.AddText("_", 28, xpos, ypos2, 28, 28, Color.Black);
                    }
                    else
                    {
                        HangingPost.AddText(ch.ToString(), 22, xpos, ypos2 - 4, 24, 24, Color.Black);
                    }
                    xpos += 28;
                }
            }
            else if (length <= 55)
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

                int xpos = 300;
                int ypos1 = 180;
                int ypos2 = 240;
                int ypos3 = 300;

                foreach (char ch in line1)
                {
                    if (char.IsLetter(ch))
                    {
                        HangingPost.AddText("_", 28, xpos, ypos1, 28, 28, Color.Black);
                    }
                    else
                    {
                        HangingPost.AddText(ch.ToString(), 22, xpos, ypos1 - 4, 24, 24, Color.Black);
                    }
                    xpos += 28;
                }
                xpos = 300 - 28;
                foreach (char ch in line2)
                {
                    if (char.IsLetter(ch))
                    {
                        HangingPost.AddText("_", 28, xpos, ypos2, 28, 28, Color.Black);
                    }
                    else
                    {
                        HangingPost.AddText(ch.ToString(), 22, xpos, ypos2 - 4, 24, 24, Color.Black);
                    }
                    xpos += 28;
                }
                xpos = 300 - 28;
                foreach (char ch in line3)
                {
                    if (char.IsLetter(ch))
                    {
                        HangingPost.AddText("_", 28, xpos, ypos3, 28, 28, Color.Black);
                    }
                    else
                    {
                        HangingPost.AddText(ch.ToString(), 22, xpos, ypos3 - 4, 24, 24, Color.Black);
                    }
                    xpos += 28;
                }
            }
            HangingPost.Render();
        }

        // Draw letter buttons
        public static void LetterButtons(ref CDrawer HangingPost)
        {
            // Create Buttons A-M
            for (int a = 0; a < 13; a++)
            {
                HangingPost.AddRectangle((a * 40) + 250, 10, 30, 30, Color.Gray);
                HangingPost.AddText(((char)(a + 65)).ToString() + " ", 16, (a * 40) + 250, 10, 30, 30, Color.Black);
            }
            // Create Buttons N-Z
            for (int a = 0; a < 13; a++)
            {
                HangingPost.AddRectangle((a * 40) + 250, 50, 30, 30, Color.Gray);
                HangingPost.AddText(((char)(a + 78)).ToString() + " ", 16, (a * 40) + 250, 50, 30, 30, Color.Black);
            }
        }

        // Draw Hanged Man 1 bodypart at a time
        public static void DisplayHangedMan(ref CDrawer HangingPost, int body_count, bool PGversion)
        {
            if (PGversion)
            {
                switch (body_count)
                {
                    case 1:
                        HangingPost.AddCenteredEllipse(250, 245, 60, 60, Color.Empty, 5, Color.Black);
                        break;
                    case 2:
                        HangingPost.AddLine(250, 275, 250, 350, Color.Black, 5);
                        break;
                    case 3:
                        HangingPost.AddLine(250, 275, 220, 360, Color.Black, 5);
                        break;
                    case 4:
                        HangingPost.AddLine(250, 275, 280, 360, Color.Black, 5);
                        break;
                    case 5:
                        HangingPost.AddLine(250, 350, 230, 460, Color.Black, 5);
                        break;
                    case 6:
                        HangingPost.AddLine(250, 350, 270, 460, Color.Black, 5);
                        break;
                }
            }
            else
            {
                switch (body_count)
                {
                    case 1:
                        HangingPost.AddCenteredEllipse(270, 215, 50, 50, Color.Black);
                        break;
                    case 2:
                        HangingPost.AddLine(250, 215, 245, 285, Color.Black, 5);
                        break;
                    case 3:
                        HangingPost.AddLine(250, 215, 230, 305, Color.Black, 5);
                        break;
                    case 4:
                        HangingPost.AddLine(250, 215, 270, 305, Color.Black, 5);
                        break;
                    case 5:
                        HangingPost.AddLine(245, 285, 230, 365, Color.Black, 5);
                        break;
                    case 6:
                        HangingPost.AddLine(245, 285, 265, 365, Color.Black, 5);
                        break;
                }
            }
            HangingPost.Render();
        }

        public static void DrawVictoryScreen(ref CDrawer HangingPost)
        {
            HangingPost.AddText("YOU WIN", 60, Color.Magenta);
            HangingPost.Render();
        }

        public static void DrawLoserScreen(ref CDrawer HangingPost)
        {
            HangingPost.AddText("YOU LOSE", 60, Color.DarkRed);
            HangingPost.Render();
        }
    }
}
