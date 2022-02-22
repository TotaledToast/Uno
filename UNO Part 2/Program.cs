using System;
using System.Collections.Generic;

namespace UNO_Part_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Card Test = new Card();
            Effects EffectTracker = new Effects();
            List<Card> Deck1 = new List<Card>();
            List<Card> Deck2 = new List<Card>();
            List<Card> Deck3 = new List<Card>();
            List<Card> Deck4 = new List<Card>();
            EffectTracker.LastCardPLayed = EffectTracker.AddLastCard();
            Deck1 = Test.MakeDeck(7);
            Deck2 = Test.MakeDeck(7);
            Deck3 = Test.MakeDeck(7);
            Deck4 = Test.MakeDeck(7);       //Generates All Decks and Effect Tracker
            do
            {
                switch (EffectTracker.CurrentPlayer)
                {
                    case 1:
                        PlayRound(Deck1, EffectTracker.CurrentPlayer, EffectTracker);
                        break;
                    case 2:
                        PlayRound(Deck2, EffectTracker.CurrentPlayer, EffectTracker);
                        break;
                    case 3:
                        PlayRound(Deck3, EffectTracker.CurrentPlayer, EffectTracker);
                        break;
                    case 4:
                        PlayRound(Deck4, EffectTracker.CurrentPlayer, EffectTracker);   
                        break;                                                          //Allows each player to play, Will Stop When A Player Wins
                }
                Console.Clear();
            } while (Deck1.Count != 0 && Deck2.Count != 0 && Deck3.Count != 0 && Deck4.Count != 0);
            Console.Clear();
            if(Deck1.Count == 0)
            {
                Console.WriteLine("Player1 WINS!");
            }
            else if (Deck2.Count == 0)
            {
                Console.WriteLine("Player2 WINS!");
            }
            else if (Deck3.Count == 0)
            {
                Console.WriteLine("Player3 WINS!");
            }
            else
            {
                Console.WriteLine("Player4 WINS!");     //declares which player wins, definitely better way of doing this
            }
            Console.ReadLine();
        }
        static void PlayRound(List<Card> Deck, int Player, Effects Effect)
        {
            int CardPicked = -1;        //variable for the picked card
            bool ValidPick = false;
            DisplayDeck(Deck, Player, Effect);  //shows deck
            Console.Write("Pick a Card to play: ");
            do
            {
                try
                {
                    CardPicked = Convert.ToInt32(Console.ReadLine());
                    if(CardPicked < Deck.Count && CardPicked > -1)
                    {
                        ValidPick = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please Try Again");
                }  
            } while (ValidPick == false);  
            
            //picks a card from available deck
            CardPlayed(Deck[CardPicked], Effect, Deck); // plays the card
            Effect.CurrentPlayer = RotatePlayer(Effect.CurrentPlayer, Effect.DirectionClockwise, Effect.Skipped);
            Effect.Skipped = false;                                                                                 //rotates to the next players and skips players is necessary
            
        }
        static int RotatePlayer(int CurrentPlayer, bool Clockwise, bool skipped)
        {
            int OutPlayer = 0;
            if(Clockwise == true)
            {
                if(skipped == true)
                {
                    OutPlayer = CurrentPlayer + 2;
                }
                else
                {
                    OutPlayer = CurrentPlayer + 1;      //add a certain amount to the current players
                }
                if (OutPlayer > 4)
                {
                    OutPlayer = OutPlayer - 4;      //if the player isnt in the accepable range it will loop back around
                }
            }
            else if(Clockwise == false)
            {
                if(skipped == true)
                {
                    OutPlayer = CurrentPlayer - 2;
                }
                else
                {
                    OutPlayer = CurrentPlayer - 1;   //has to minus for reversed play
                }
                if (OutPlayer < 1)
                {
                    OutPlayer = OutPlayer + 4;  //loops back when outside acceptable range
                }
            }
            return OutPlayer;
        }
        static Effects CardPlayed(Card CardPlayed, Effects Effect, List<Card> Deck)
        {
            Card DrawHelp = new Card();
            if (Effect.CardPlus > 0 && (CardPlayed.Number != 10 && CardPlayed.Number != 11))
            {
                DrawHelp.AddCard(Deck, Effect.CardPlus);
                Console.WriteLine("You Picked Up " + Effect.CardPlus + " Cards!");
                Console.ReadLine();
                Effect.CardPlus = 0;    //checks if an acceptable card was played after a +2/+4 and will respond accordingly
                return Effect;
            }
            if(CardPlayed.Number == 11 || CardPlayed.Number == 12)
            {
                bool ColorPicked = false;
                int ColourVar = -1;
                if(CardPlayed.Number == 11)
                {
                    Effect.CardPlus = Effect.CardPlus + 4;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("RED: 0");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("GREEN: 1");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("BLUE: 2");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("YELLOW: 3");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Pick The Colour For The Next Card:");
                do
                {
                    try
                    {
                        ColourVar = Convert.ToInt32(Console.ReadLine());
                        if(ColourVar < 4 && ColourVar > -1)
                        {
                            ColorPicked = true;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Try Again");
                    }    
                } while (ColorPicked == false);     
                
                //allows player to pick the next colour
                switch (ColourVar)
                {
                    case 0:
                        Effect.LastCardPLayed.CardColour = ConsoleColor.Red;
                        break;
                    case 1:
                        Effect.LastCardPLayed.CardColour = ConsoleColor.Green;
                        break;
                    case 2:
                        Effect.LastCardPLayed.CardColour = ConsoleColor.Blue;
                        break;
                    case 3:
                        Effect.LastCardPLayed.CardColour = ConsoleColor.Yellow; //sets colour of new last card played
                        break;
                }
                Effect.LastCardPLayed.Number = 0;
                Effect.LastCardPLayed.Type = Card.CardType.Number;      //sets basic next card played
                Deck.Remove(CardPlayed);      //removes card played from deck
            }
            else if(CardPlayed.Number == Effect.LastCardPLayed.Number || CardPlayed.CardColour == Effect.LastCardPLayed.CardColour)
            {
                switch (CardPlayed.Number)
                {
                    case 10:
                        Effect.CardPlus = Effect.CardPlus + 2;
                        break;
                    case 13:
                        Effect.Skipped = true;
                        break;
                    case 14:
                        if (Effect.DirectionClockwise == true)
                        {
                            Effect.DirectionClockwise = false;
                        }
                        else
                        {
                            Effect.DirectionClockwise = true;
                        }
                        break;                                  //stores each special effect for the special cards
                }
                Effect.LastCardPLayed = CardPlayed; 
                Deck.Remove(CardPlayed);            //sets new last card played and removes old card from deck
            }
            else
            {
                DrawHelp.AddCard(Deck, 1);
                Console.WriteLine("You Picked Up 1 Card!"); //if the player can play a card they pick up a card
                Console.ReadLine();
            }
            return Effect;
        }
        static void DisplayDeck(List<Card> Deck, int Player, Effects Effect)
        {
            int Count = 0;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Player " + Player + "'s Turn:");
            Console.Write("Last Card Played Was: ");
            Console.ForegroundColor = Effect.LastCardPLayed.CardColour;
            if (Effect.LastCardPLayed.Type == Card.CardType.Number)
            {
                Console.Write(Effect.LastCardPLayed.Number + "\n\n");
            }
            else
            {
                Console.Write(Effect.LastCardPLayed.Type + "\n\n");     //differentiates between number cards and specials
            }
            foreach (Card x in Deck)
            {
                Console.ForegroundColor = x.CardColour;
                if (x.Type == Card.CardType.Number)
                {
                    Console.WriteLine(x.Number + "\t:" + Count);
                }
                else
                {
                    Console.WriteLine(x.Type + "\t:" + Count);      //goes through every card in the deck
                }
                Count++;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

}
