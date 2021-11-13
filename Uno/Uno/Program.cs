﻿using System;
using System.Collections.Generic;
// very quick tester change

namespace Open_Evening
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> deck1 = new List<Card>();
            List<Card> deck2 = new List<Card>();
            GenerateHand(deck1);
            Console.WriteLine("------------------------");
            GenerateHand(deck2);
        }

        static List<Card> GenerateHand(List<Card> deck)
        {
            Random RandCard = new Random();
            int color;

            for (int i = 0; i < 11; i++)
            {
                Card card = new Card();
                card.number = RandCard.Next(0, 14);
                card.cardType = Open_Evening.Card.CardType.Number;
                if (card.number > 9)
                {
                    card.cardType = Open_Evening.Card.CardType.Special;
                    switch (card.number)
                    {
                        case 10:
                            card.specialType = "Plus 2";
                            break;
                        case 11:
                            card.specialType = "Plus 4";
                            break;
                        case 12:
                            card.specialType = "Stop";
                            break;
                        case 13:
                            card.specialType = "Reverse";
                            break;
                        case 14:
                            card.specialType = "Pick Color";
                            break;
                    }
                }
                GenerateColor(card);
                deck.Add(card);
            }

            foreach (Card item in deck)
            {
                if (item.cardType == Open_Evening.Card.CardType.Special)
                {
                    Console.ForegroundColor = item.color;
                    Console.WriteLine(item.specialType);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    color = RandCard.Next(1, 4);
                    Console.ForegroundColor = item.color;
                    Console.WriteLine(item.number);
                    Console.ForegroundColor = ConsoleColor.White;

                }
            }

            return deck;
        }
        public static void GenerateColor(Card item)
        {
            int random;
            Random RandColor = new Random();
            random = RandColor.Next(1, 4);
            if (item.specialType != "Plus 4" && item.specialType != "Pick Color")
            {
                switch (random)
                {
                    case 1:
                        item.color = ConsoleColor.Yellow;
                        break;
                    case 2:
                        item.color = ConsoleColor.Red;
                        break;
                    case 3:
                        item.color = ConsoleColor.Blue;
                        break;
                    case 4:
                        item.color = ConsoleColor.Green;
                        break;
                }
            }
            else
            {
                item.color = ConsoleColor.White;
            }
        }
    }

    public class Card
    {
        public ConsoleColor color;
        public int number;
        public CardType cardType;
        public string specialType;
        public Card(int number, ConsoleColor color, string SpecialType)
        {
            this.number = number;
            this.color = color;
            this.specialType = SpecialType;
        }

        public Card()
        {
        }

        public enum CardType
        {
            Number,
            Special
        }
    }
}
