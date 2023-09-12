using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    //Struct for representing a card
    public struct Card
    {
        public string Suit { get; }
        public byte Value { get; }

        public Card(string suit, byte value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            string cardValue;
            if (Value == 1)
            {
                cardValue = "Ace";
            }
            else if (Value == 11)
            {
                cardValue = "Jack";
            }
            else if (Value == 12)
            {
                cardValue = "Queen";
            }
            else if (Value == 13)
            {
                cardValue = "King";
            }
            else
            {
                cardValue = Value.ToString();
            }
            return $"{cardValue} of {Suit}";
        }
    }
    //Struct for representing a hand
    public struct Hand
    {
        public List<Card> Cards { get; }
        public Hand(List<Card> cards)
        {
            Cards = cards;
        }
        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        //Change the value depending on if there are any aces in the hand and if the hand value surpasses 21
        public int CalculateHandValue()
        {
            int handValue = 0;
            int aceCount = 0;

            foreach (var card in Cards)
            {
                if (card.Value == 1)
                {
                    aceCount++;
                    handValue += 11;
                }
                else if (card.Value >= 10)
                {
                    handValue += 10;
                }
                else
                {
                    handValue += card.Value;
                }
            }
            while (aceCount > 0 && handValue > 21)
            {
                handValue -= 10;
                aceCount--;
            }

            return handValue;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Controller();
        }

        static void Controller()
        {
            //Makes and array with cards
            List<Card> deck = new List<Card>();
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

            foreach (string suit in suits)
            {
                for (byte value = 1; value <= 13; value++)
                {
                    deck.Add(new Card(suit, value));
                }
            }

            //Shuffle the cards
            Random rng = new Random();
            deck = deck.OrderBy(card => rng.Next()).ToList();

            //The player and dealers hand
            Hand playerHand = new Hand(new List<Card>());
            Hand dealerHand = new Hand(new List<Card>());

            //Give 2 cards to the player and the dealer
            playerHand.AddCard(deck[0]);
            deck.RemoveAt(0);
            dealerHand.AddCard(deck[0]);
            deck.RemoveAt(0);
            playerHand.AddCard(deck[0]);
            deck.RemoveAt(0);
            dealerHand.AddCard(deck[0]);
            deck.RemoveAt(0);

            View(playerHand, dealerHand);
        }
        static void Model()
        {

        }
        static void View(Hand playerHand, Hand dealerHand)
        {
            //Show the players hand
            Console.WriteLine("Your hand:");
            foreach (Card card in playerHand.Cards)
            {
                Console.WriteLine(card);
            }
            //Show the dealers visible cards
            Console.WriteLine("\nDealers visible cards:");
            Console.WriteLine(dealerHand.Cards[0]);
        }
    }
}
