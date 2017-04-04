using System;
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
            Console.WriteLine("Return rate: " + returnRate);
            Console.WriteLine("Chance: " + chance);
            if (returnRate < 0.8)
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
            else if (returnRate < 1)
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
            else if (returnRate < 1.3)
            {
                if (chance > 70)
                {
                    return new PlayerAction(Name, "Bet1", "call", 0);
                }
                else
                {
                    return new PlayerAction(Name, "Bet1", "raise", betAmount);
                }
            }
            return new PlayerAction(Name, "Bet1", "bet", betAmount);
            //return new PlayerAction(Name, "Bet1", "bet", betAmount); 
            //throw new NotImplementedException();
        }

        public override PlayerAction BettingRound2(List<PlayerAction> actions, Card[] hand)
        {
            throw new NotImplementedException();
        }

        public override PlayerAction Draw(Card[] hand)
        {
            throw new NotImplementedException();
        }

        public float CalculatePotOdds(float bet, float pot)
        {
            float odds = 0f;
            if (bet + pot > 0)
            {
                odds = bet / (bet + pot);
            }
            return odds;
        }

        public float CalculateRateOfReturn(float handStrength, float odds)
        {
            float rate = 0f;
            rate = (handStrength/10) / odds;
            return rate;
        }


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
            return bet;
        }

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

