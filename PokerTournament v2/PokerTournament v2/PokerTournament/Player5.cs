using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    class Player5 : Player
    {
        public override PlayerAction BettingRound1(List<PlayerAction> actions, Card[] hand)
        {
            throw new NotImplementedException();
        }

        public override PlayerAction BettingRound2(List<PlayerAction> actions, Card[] hand)
        {
            throw new NotImplementedException();
        }

        public override PlayerAction Draw(Card[] hand)
        {
            throw new NotImplementedException();
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

