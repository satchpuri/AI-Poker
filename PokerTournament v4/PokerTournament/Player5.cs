﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PokerTournament
{
    class Player5 : Player
    {
        Random rand;
        public Player5(int idNum, string name, int money) : base(idNum, name, money)
        {
            rand = new Random();
        }
        public override PlayerAction BettingRound1(List<PlayerAction> actions, Card[] hand)
        {
            PokerTournament.Card highCard;
            int handEval = Evaluate.RateAHand(this.Hand, out highCard);
            int pot = CurrentPot(actions, "Bet1");
            int betAmount = GetCurrentBet(actions, "Bet1");
            float odds = CalculatePotOdds(betAmount, pot);
            float returnRate = CalculateRateOfReturn(handEval, odds);
            float chance = rand.Next(0,100);
            ListTheHand(hand);
            Console.WriteLine("Return rate: " + returnRate);
            //Console.ReadLine();
            //Console.WriteLine("Chance: " + chance);
            //Console.WriteLine("Pairs: " + CheckPair().Count);
            if (returnRate < 4)
            {
                if(chance > 94)
                {
                    return new PlayerAction(Name, "Bet1", "raise", betAmount);             
                }
                else
                {
                    return new PlayerAction(Name, "Bet1", "fold", 0); 
                }
            }
            else if (returnRate < 20)
            {
                if (chance > 94)
                {
                    return new PlayerAction(Name, "Bet1", "call", 0);
                }
                else if (chance > 79)
                {
                    return new PlayerAction(Name, "Bet1", "raise", betAmount);
                }
                else
                {
                    return new PlayerAction(Name, "Bet1", "fold", 0);
                }
            }
            else
            {
                if (returnRate == float.PositiveInfinity)
                {
                    int res;
                    res = 5 * handEval;
                    if (res > Money)
                    {
                        res = Money;
                    }
                    return new PlayerAction(Name, "Bet1", "bet", res);
                }
                if (chance > 70)
                {
                    return new PlayerAction(Name, "Bet1", "call", 0);
                }
                else
                {
                    float fres = betAmount * (returnRate);
                    int res;
                    res = (int)fres;
                    if (res > Money)
                    {
                        res = Money;
                    }
                    return new PlayerAction(Name, "Bet1", "raise", res);
                }
            }
            //return new PlayerAction(Name, "Bet1", "bet", 0);
            //return new PlayerAction(Name, "Bet1", "bet", betAmount); 
        }

        public override PlayerAction BettingRound2(List<PlayerAction> actions, Card[] hand)
        {
            PokerTournament.Card highCard;
            int handEval = Evaluate.RateAHand(this.Hand, out highCard);
            int pot = CurrentPot(actions, "Bet2");
            int betAmount = GetCurrentBet(actions, "Bet2");
            float odds = CalculatePotOdds(betAmount, pot);
            float returnRate = CalculateRateOfReturn(handEval, odds);
            float chance = rand.Next(0, 100);
            ListTheHand(hand);
            Console.WriteLine("Return rate: " + returnRate);
            //Console.WriteLine("Chance: " + chance);
            //Console.WriteLine("Pairs: " + CheckPair().Count);
            //Console.ReadLine();
            if (returnRate < 4)
            {
                if (chance > 94)
                {
                    return new PlayerAction(Name, "Bet2", "raise", betAmount);
                }
                else
                {
                    return new PlayerAction(Name, "Bet2", "fold", 0);
                }
            }
            else if (returnRate < 20)
            {
                if (chance > 94)
                {
                    return new PlayerAction(Name, "Bet2", "call", 0);
                }
                else if (chance > 79)
                {
                    return new PlayerAction(Name, "Bet2", "raise", betAmount);
                }
                else
                {
                    return new PlayerAction(Name, "Bet2", "fold", 0);
                }
            }
            else
            {
                if (returnRate == float.PositiveInfinity)
                {
                    int res;
                    res = 5 * handEval;
                    if (res > Money)
                    {
                        res = Money;
                    }
                    return new PlayerAction(Name, "Bet2", "bet", res);
                }
                if (chance > 70)
                {
                    return new PlayerAction(Name, "Bet2", "call", 0);
                }
                else
                {
                    float fres = betAmount * (returnRate);
                    int res;
                    res = (int)fres;
                    if (res > Money)
                    {
                        res = Money;
                    }
                    return new PlayerAction(Name, "Bet2", "raise", res);
                }
            }
            //return new PlayerAction(Name, "Bet2", "bet", 0);
        }

        public override PlayerAction Draw(Card[] hand)
        {
            List<int> cardsToDelete = new List<int>();
            List<int> pair = new List<int>();
            pair = CheckPair();
            PlayerAction pa = null;
            if (pair.Count == 0)
            {
                pa = new PlayerAction(Name, "Draw", "stand pat", 0);
                //Should keep ace or two highest cards
            }
            else
            {
                for (int r = 0; r < pair.Count; r++)
                {
                    for (int i = 0; i < Hand.Length; i++)
                    {
                        if (Hand[i].Value != pair[r])
                        {
                            pa = new PlayerAction(Name, "Draw", "draw", i); //Discard all cards that aren't part of a pair
                        }
                    }
                }
            }
            return pa;
        }

        //Returns a list of values that have pairs or more
        public List<int> CheckPair()
        {
            List<int> valCount = new List<int>();
            for (int r = 2; r < 15; r++)
            {
                int tempCount = Evaluate.ValueCount(r, Hand);
                if (tempCount > 1)
                {
                    valCount.Add(r);
                }
            }
            return valCount;
        }

        //Returns odds of bet compared to the current pot
        public float CalculatePotOdds(float bet, float pot)
        {
            float odds = 0f;
            if (bet + pot > 0)
            {
                odds = bet / (bet + pot);
            }
            return odds;
        }

        //Returns the rate of return of continuing to bet
        public float CalculateRateOfReturn(float handStrength, float odds)
        {
            float rate = 0f;
            rate = (handStrength) / odds;
            return rate;
        }

        //Returns the current value of bets
        public int GetCurrentBet(List<PlayerAction> actions, string phase)
        {
            int bet = 0;
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].ActionPhase.Equals(phase))
                {
                    if (actions[i].ActionName.Equals("bet") || actions[i].ActionName.Equals("raise"))
                    {
                        bet = actions[i].Amount;
                    }
                }
            }
            if (bet > Money)
            {
                bet = Money;
            }
            return bet;
        }

        //Gets the value of the current pot
        public int CurrentPot(List<PlayerAction> actions, string phase)
        {
            int pot = 0;
            for(int i = 0; i < actions.Count; i++)
            {
                if (actions[i].ActionPhase.Equals(phase))
                {
                    if (actions[i].ActionName.Equals("bet") || actions[i].ActionName.Equals("raise"))
                    {
                        pot += actions[i].Amount;
                    }
                }
            }
            return pot;
        }

        // helper method - list the hand
        private void ListTheHand(Card[] hand)
        {
            // evaluate the hand
            Card highCard = null;
            int rank = Evaluate.RateAHand(hand, out highCard);

            // list your hand
            Console.WriteLine("\nName: " + Name + " Your hand:   Rank: " + rank);
            for (int i = 0; i < hand.Length; i++)
            {
                Console.Write(hand[i].ToString() + " ");
            }
            Console.WriteLine();
        }
    }
}



//child class of player
//Logic stuff:
//what is the value of my hand?
//What is the potential value of my hand?
//What if the likelyhood of the other player having a higher value?
//Which of my cards do i remove from my hand? if any?
//Do i fold or continue?

//interpretation of data 
//Rank hands (ex: High card = 1.... Royal Flush = 10)
//Find out the probability of getting each hand.
//Probability for hands
//http://www.math.hawaii.edu/~ramsey/Probability/PokerHands.html 
    //Conditions: 
            //What cards do i have?
            //How many cards have been drawn from the deck?
            //Store discarded cards in an array/list and compare it to the deck to find out cards not played

