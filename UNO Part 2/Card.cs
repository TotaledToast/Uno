using System;
using System.Collections.Generic;
using System.Text;

namespace UNO_Part_2
{
    class Card
    {
        public int Number;
        public ConsoleColor CardColour;
        public CardType Type;
        public enum CardType
        {
            Number,
            Plus2,
            Plus4,
            Wild,
            Skip,
            Reverse                 //sets types of card as well as other variables
        }
        public Card MakeCard(int Number, ConsoleColor Colour)
        {
            Card NewCard = new Card();
            NewCard.Number = Number;
            NewCard.CardColour = Colour;    //sets the number and color of the new card
            switch (Number)
            {
                case 10:
                    NewCard.Type = CardType.Plus2;
                    break;
                case 11:
                    NewCard.Type = CardType.Plus4;
                    NewCard.CardColour = ConsoleColor.White;
                    break;
                case 12:
                    NewCard.Type = CardType.Wild;
                    NewCard.CardColour = ConsoleColor.White;
                    break;
                case 13:
                    NewCard.Type = CardType.Skip;
                    break;
                case 14:
                    NewCard.Type = CardType.Reverse;
                    break;
                default:
                    NewCard.Type = CardType.Number;
                    break;                                   //gives the new card a type if required, if not the type is number
            }
            return NewCard;
        }
        public List<Card> MakeDeck(int Length)
        {
            List<Card> OutList = new List<Card>();
            AddCard(OutList, Length);           //adds a card a certain amount of times for a deck
            return OutList;
        }
        public List<Card> AddCard(List<Card> OutList, int length)
        {
            Random Rand = new Random();
            int Number, ColourNumber;
            ConsoleColor Colour;
            for (int i = 0; i < length; i++)
            {
                Number = Rand.Next(0, 15);//rand isn't inclusive on upper bound
                ColourNumber = Rand.Next(0, 4); //gives cards a random number and colour
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
                OutList.Add(MakeCard(Number, Colour));
            }
            return OutList;
        }
    }
}
