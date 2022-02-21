using System;
using System.Collections.Generic;
using System.Text;

namespace UNO_Part_2
{
    class Effects
    {
        public int CardPlus = 0, CurrentPlayer = 1;
        public Card LastCardPLayed;
        public bool Skipped = false, DirectionClockwise = true, FirstTurn = true, Draw = false; //adds all relevant game data to the effects class
        public Card AddLastCard()
        {
            List<Card> Temp = new List<Card>();
            Card Temp2 = new Card();
            Random Rand = new Random();
            int Number, ColourNumber;
            ConsoleColor Colour;
            Number = Rand.Next(0, 10);
            ColourNumber = Rand.Next(0, 4);
            switch (ColourNumber)
            {
                case 0:
                    Colour = ConsoleColor.Red;
                    break;
                case 1:
                    Colour = ConsoleColor.Green;
                    break;
                case 2:
                    Colour = ConsoleColor.Yellow;
                    break;
                default:
                    Colour = ConsoleColor.Blue;
                    break;
            }
            Temp.Add(Temp2.MakeCard(Number, Colour));   //uses a modified AddCard function as the starting card shouldn't be a special card
            return Temp[0];
        }
    }
}
