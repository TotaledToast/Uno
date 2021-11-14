using System;
using System.Collections.Generic;

namespace Open_Evening
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<Card> deck1 = new List<Card>();
            List<Card> deck2 = new List<Card>();
            List<Card> deck3 = new List<Card>();
            List<Card> deck4 = new List<Card>();
            OrderOfPlay Order = new OrderOfPlay();
            Order.Order = false;
            Order.Plus = 0;
            Order.player = 0;
            Order.max = 3;
            Card LastCardPlayed = new Card();
            LastCardPlayed.number = 0;
            LastCardPlayed.color = ConsoleColor.Green;
            GenerateHand(deck1);
            GenerateHand(deck2);
            GenerateHand(deck3);
            GenerateHand(deck4);
            while (deck1.Count != 0 && deck2.Count != 0)
            {
                switch (Order.player)
                {
                    case 0:
                        PlayRound(deck1, LastCardPlayed, 1, Order);
                        break;
                    case 1:
                        PlayRound(deck2, LastCardPlayed, 2, Order);
                        break;
                    case 2:
                        PlayRound(deck3, LastCardPlayed, 3, Order);
                        break;
                    case 3:
                        PlayRound(deck4, LastCardPlayed, 4, Order);
                        break;
                }
                if (Order.Order == false)
                {
                    Order.player++;
                }
                else
                {
                    Order.player--;
                }
                if (Order.player < Order.min)
                {
                    Order.player = Order.max;
                }
                else if (Order.player > Order.max)
                {
                    Order.player = Order.min;
                }
            }
            if (deck1.Count == 0)
            {
                Console.WriteLine("Congrats on winning Player1");
            }
            else if (deck2.Count == 0)
            {
                Console.WriteLine("Congrats on winning Player2");
            }
            else if (deck3.Count == 0)
            {
                Console.WriteLine("Congrats on winning Player3");
            }
            else if (deck4.Count == 0)
            {
                Console.WriteLine("Congrats on winning Player4");
            }
        }

        static void PlayRound(List<Card> deck, Card LastCardPlayed, int Player, OrderOfPlay Order)
        {
            Console.Clear();
            int Counter = 0, PickedCard;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--Last Card Played--");
            Console.ForegroundColor = LastCardPlayed.color;
            if (LastCardPlayed.number <= 9)
            {
                Console.WriteLine(LastCardPlayed.number);
            }
            else
            {
                Console.WriteLine(LastCardPlayed.specialType);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n");
            Console.WriteLine("--Player" + Player + "'s Deck--");
            foreach(Card item in deck)
            {
                item.PlaceInDeck = Counter;
                Console.ForegroundColor = item.color;
                if (item.number  <= 9)
                {
                    Console.WriteLine(item.number + "\t\t ----------  " + Counter);
                }
                else if (item.number == 14)
                {
                    Console.WriteLine(item.specialType + "\t ----------  " + Counter);
                }
                else
                {
                    Console.WriteLine(item.specialType + "\t\t ----------  " + Counter);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Counter++;
                
            }
            Console.WriteLine("What card will you play?");
            PickedCard = Convert.ToInt32(Console.ReadLine());
            foreach(Card item in deck)
            {
                if (item.PlaceInDeck == PickedCard)
                {
                    Fight(LastCardPlayed, item, deck, Order);
                    break;
                }
            }
        }

        static void Fight(Card card1, Card card2, List<Card> deck, OrderOfPlay Order)
        {
            if (Order.Plus > 0)
            {
                switch (card2.number)
                {
                    case 10:
                        Order.Plus = Order.Plus + 2;
                        card1.number = card2.number;
                        card1.color = card2.color;
                        card1.specialType = card2.specialType;
                        card1.cardType = card2.cardType;
                        deck.Remove(card2);
                        break;
                    case 11:
                        Order.Plus = Order.Plus + 4;
                        int color;
                        Console.WriteLine("What should the new color be?");
                        Console.WriteLine("GREEN\t ------- 0");
                        Console.WriteLine("RED\t ------- 1");
                        Console.WriteLine("BLUE\t ------- 2");
                        Console.WriteLine("YELLOW\t ------- 3");
                        color = Convert.ToInt32(Console.ReadLine());
                        switch (color)
                        {
                            case 0:
                                card1.color = ConsoleColor.Green;
                                card1.specialType = "GREEN";
                                break;
                            case 1:
                                card1.color = ConsoleColor.Red;
                                card1.specialType = "RED";
                                break;
                            case 2:
                                card1.color = ConsoleColor.Blue;
                                card1.specialType = "BLUE";
                                break;
                            case 3:
                                card1.color = ConsoleColor.Yellow;
                                card1.specialType = "YELLOW";
                                break;
                        }
                        card1.number = card2.number;
                        card1.cardType = card2.cardType;
                        deck.Remove(card2);
                        break;
                    default:
                        for (int i = 0; i < Order.Plus; i++)
                        {
                            GenerateCard(deck);
                        }
                        card1.number = 0;
                        Order.Plus = 0;
                        break;
                }
            }
            else if (card2.specialType == "Plus 4" || card2.specialType == "Pick Color")
            {
                switch (card2.number)
                {
                    case 11:
                        Order.Plus = Order.Plus + 4;
                        break;
                    case 14:
                        break;
                }
                int color;
                Console.WriteLine("What should the new color be?");
                Console.WriteLine("GREEN\t ------- 0");
                Console.WriteLine("RED\t ------- 1");
                Console.WriteLine("BLUE\t ------- 2");
                Console.WriteLine("YELLOW\t ------- 3");
                color = Convert.ToInt32(Console.ReadLine());
                switch (color)
                {
                    case 0:
                        card1.color = ConsoleColor.Green;
                        card1.specialType = "GREEN";
                        break;
                    case 1:
                        card1.color = ConsoleColor.Red;
                        card1.specialType = "RED";
                        break;
                    case 2:
                        card1.color = ConsoleColor.Blue;
                        card1.specialType = "BLUE";
                        break;
                    case 3:
                        card1.color = ConsoleColor.Yellow;
                        card1.specialType = "YELLOW";
                        break;
                }
                card1.number = card2.number;
                card1.cardType = card2.cardType;
                deck.Remove(card2);

            }
            else if (card1.number == card2.number || card1.color == card2.color)
            {
                switch(card2.number)
                {
                    case 10:
                        Order.Plus = Order.Plus + 2;
                        break;
                    case 11:
                        if(card1.number == 10 || card1.number == 11)
                        {
                            Order.Plus = 0;
                        }
                        break;
                    case 12:
                        if (Order.Order == false)
                        {
                            if (Order.player == Order.max)
                            {
                                Order.player = Order.min;
                            }
                            else
                            {
                                Order.player++;
                            }
                        }
                        else
                        {
                            if (Order.player == Order.min)
                            {
                                Order.player = Order.max;
                            }
                            else
                            {
                                Order.player--;
                            }
                        }
                        break;
                    case 13:
                        if (Order.Order == false)
                        {
                            Order.Order = true;
                        }
                        else
                        {
                            Order.Order = false;
                        }
                        break;
                    default:
                        break;
                }
                card1.number = card2.number;
                card1.color = card2.color;
                card1.specialType = card2.specialType;
                card1.cardType = card2.cardType;
                deck.Remove(card2);

            }
            else
            {
                GenerateCard(deck);
            }
        }

        static List<Card> GenerateHand(List<Card> deck)
        {
            Random RandCard = new Random();

            for (int i = 0; i < 11; i++)
            {
                Card card = new Card();
                card.number = RandCard.Next(0, 15);
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
                            card.specialType = "Skip";
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
            return deck;
        }

        public static void GenerateCard(List<Card> deck)
        {
            Random rand = new Random();
            Card card = new Card();
            card.cardType = Open_Evening.Card.CardType.Number;
            card.number = rand.Next(0, 15);
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
                        card.specialType = "Skip";
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
        public static void GenerateColor(Card item)
        {
            int random;
            Random RandColor = new Random();
            random = RandColor.Next(1, 5);
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

    public class OrderOfPlay
    {
        public bool Order;
        public int Plus;
        public int player;
        public int min = 0;
        public int max;
    }
    public class Card
    {
        public ConsoleColor color;
        public int number;
        public CardType cardType;
        public string specialType;
        public int PlaceInDeck;
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
