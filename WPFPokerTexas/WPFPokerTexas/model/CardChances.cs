using System;
using System.Collections.Generic;
using System.Text;
using WPFPokerTexas.model.pokerhands;

namespace WPFPokerTexas.model
{
    public static class CardChances
    {

        private static PokerHand.HandValidator[] testOrder;
       static CardChances() {
            //           STRAIGHT_FLUSH, FOUR_KIND, FULL_HOUSE, FLUSH, STRAIGHT, THREE_KIND, TWO_PAIR, ONE_PAIR, HIGH_CARD
            testOrder = new PokerHand.HandValidator[] {
                StraightFlushHand.StraightFlushValidator,
                FourKindHand.FourKindValidator,
                FullHouseHand.FullHouseValidator,
                FlushHand.FlushValidator,
                StraighthHand.StraightValidator,
                ThreeKindHand.ThreeKindValidator,
                TwoPairHand.TwoPairValidator,
                OnePairHand.OnePairValidator,
                HighCardHand.HighCardValidator
            };
        }


        public static List<OrderedCardSet> GetCombinationOfHandCards(OrderedCardSet handCards, OrderedCardSet community) {

            return null;
        }

        public static PokerHand ChooseBestHand(OrderedCardSet handCards, OrderedCardSet playingCards) {

            PokerHand best = null;

            List<OrderedCardSet> createHands = GetCombinationOfHandCards(handCards, playingCards);


            foreach (OrderedCardSet hand in createHands) {
                PokerHand pHand = IdentitfyHand(hand);

                if (best == null) {
                    best = pHand;
                } else if (pHand.CompareTo(best) > 0) {
                    best = pHand;

                }

            }


            return best;
        }


        public static List<OrderedCardSet> GetOpponentCombinationOfHandCards(OrderedCardSet otherCards) {

            return null;
        }

        public static HandPlayResult TestHandChances(OrderedCardSet otherCards, PokerHand hand)
        {
            int wins = 0, losses= 0;
            List<OrderedCardSet> allHands = GetOpponentCombinationOfHandCards(otherCards);
            foreach (OrderedCardSet iHand in allHands) {

                PokerHand otherHand = IdentitfyHand(iHand);
                int compare = hand.CompareTo(otherHand);
                if (compare >0 )
                {
                    wins += 1;
                }
                else if (compare < 0) {

                    losses += 1;
                }

            }

            return new HandPlayResult(allHands.Count, wins, losses);
        }


        public static PokerHand IdentitfyHand(OrderedCardSet cards) {

            PokerHand hand = null;

            foreach (PokerHand.HandValidator validator in testOrder) {

                if (validator(cards, out hand)) {
                    break;
                }
            }

            return hand;
            
        }


        public class HandPlayResult { 
        
            public int SampleCount { get; }
            public int Wins { get; }

            public int Losses { get; }

            public HandPlayResult(int sampleCount, int wins, int losses) {
                this.SampleCount = sampleCount;
                this.Wins = wins;
                this.Losses = losses;
            }


          
        }
    }
}
