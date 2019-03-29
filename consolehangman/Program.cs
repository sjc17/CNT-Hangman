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
            bool game_win = false;
            bool game_over = false;
            bool good_guess = false;
            int counter = 0;

            List<int> letter_indexes = new List<int>();

            // Get movie title
            string Title = MovieAPI.GetWord();
            System.Threading.Thread.Sleep(1000);        //Add delay to let API get proper movie title

            /* INSERT TEST MOVIE TITLE HERE **
            //string test = "Pirates of the Carribean: Curse of the Black Pearl";
            ** INSERT TEST MOVIE TITLE HERE */

            //Create GDIDrawer Display Canvas
            CDrawer HangingPost = Drawings.CreateDrawingWindow();

            //Game's Title Screen Art
            Drawings.MainMenu(ref HangingPost);

            //Main Playing Window
            Drawings.PlayWindow(ref HangingPost, Title);

            //Return selected letter
            do
            {
                char user_selection = (SelectLetter(ref HangingPost));

                WhatDoLetter(user_selection, Title, out good_guess, out letter_indexes, ref counter);

                if (good_guess)
                {
                    //DisplayLetters();
                }
                else
                {
                    Drawings.DisplayHangedMan(ref HangingPost, counter, true);
                    if (counter >= 6)
                        game_over = true;
                }

            } while (!game_win && !game_over);

            if (game_win)
                Drawings.DrawVictoryScreen(ref HangingPost);
            if (game_over)
                Drawings.DrawLoserScreen(ref HangingPost);

            Console.Read();

        }

        // Declare Button class
        class Button
        {
            public char alpha { get; set; }
            public int xMin_coord { get; set; }
            public int xMax_coord { get; set; }
            public int yMin_coord { get; set; }
            public int yMax_coord { get; set; }
        }

        // Button Constructor/Controller; User selects button and returns a char
        static char SelectLetter(ref CDrawer HangingPost)
        {
            char selection = ' ';
            Point click;
            bool valid_click = false;
            int counter = 0;
            // Generate buttons
            var button = new Button[26];                //create array of class Button
            for (int i = 0; i < 26; i++)
            {
                button[i] = new Button();               //construct new Button for each element in array
                button[i].alpha = (char)(i + 65);
                if (i < 13)
                {
                    button[i].xMin_coord = 250 + counter*40;
                    button[i].xMax_coord = button[i].xMin_coord + 30;
                    button[i].yMin_coord = 10;
                    button[i].yMax_coord = 40;                        
                }
                else
                {
                    button[i].xMin_coord = 250 + counter * 40;
                    button[i].xMax_coord = button[i].xMin_coord + 30;
                    button[i].yMin_coord = 50;
                    button[i].yMax_coord = 80;
                }
                counter++;
                if (counter >= 13)
                {
                    counter = 0;
                }
            }
            while (!valid_click)
            {
                if (HangingPost.GetLastMouseLeftClick(out click))
                {
                    for (int i = 0; i < 26; i++)
                    {
                        int xmin = button[i].xMin_coord;
                        int xmax = button[i].xMax_coord;
                        int ymin = button[i].yMin_coord;
                        int ymax = button[i].yMax_coord;
                        if (click.X > xmin && click.X < xmax && click.Y > ymin && click.Y < ymax)
                        {
                            HangingPost.AddLine(xmin, ymin, xmax, ymax, Color.Red, 2);
                            HangingPost.AddLine(xmax, ymin, xmin, ymax, Color.Red, 2);
                            HangingPost.Render();
                            selection = button[i].alpha;
                            valid_click = true;
                        }
                    }
                }
            }
            return selection;
        }

        // Letter Processing.....
        static void WhatDoLetter(char letter, string word, out bool good_guess, out List<int> letter_indexes, ref int counter)
        {
            word = word.ToUpper();
            good_guess = false;
            letter_indexes = new List<int>();
            foreach (char ch in word)
            {
                int index = 0;
                if (ch == letter)
                {
                    good_guess = true;
                    index = word.IndexOf(ch);
                    letter_indexes.Add(index);
                }
                else
                {
                    good_guess = false;
                    counter++;
                    break;
                }
            }
            
        }
    }
}
