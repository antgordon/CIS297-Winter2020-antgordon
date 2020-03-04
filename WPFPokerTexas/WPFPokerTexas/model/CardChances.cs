using System;
using System.Collections.Generic;
using System.Text;
using WPFPokerTexas.model.pokerhands;

namespace WPFPokerTexas.model
{
    public static class CardChances
    {

        //private static PokerHand.HandValidator[] testOrder;
       static CardChances() {
            //           STRAIGHT_FLUSH, FOUR_KIND, FULL_HOUSE, FLUSH, STRAIGHT, THREE_KIND, TWO_PAIR, ONE_PAIR, HIGH_CARD
            /*testOrder = new PokerHand.HandValidator[] {
                StraightFlushHand.StraightFlushValidator,
                FourKindHand.FourKindValidator,
                FullHouseHand.FullHouseValidator,
                FlushHand.FlushValidator,
                StraightHand.StraightValidator,
                ThreeKindHand.ThreeKindValidator,
                TwoPairHand.TwoPairValidator,
                OnePairHand.OnePairValidator,
                HighCardHand.HighCardValidator
            };*/
        }


        public static List<OrderedCardSet> GetCombinationOfPokerHands(OrderedCardSet handCards, OrderedCardSet community) {
            List<PlayingCard> totalCards = new List<PlayingCard>();
            totalCards.AddRange(handCards.asList());
            totalCards.AddRange(community.asList());

            List<OrderedCardSet> possibleSets = new List<OrderedCardSet>();
            int len = totalCards.Count;


            for (int t1 = 0; t1 < len; t1 += 1) {
                for (int t2 = t1 +1; t2 < len; t2 += 1)
                {
                    for (int t3 = t2+1; t3 < len; t3 += 1)
                    {
                        for (int t4 = t3 + 1; t4 < len; t4 += 1)
                        {
                            for (int t5 = t4 + 1; t5 < len; t5 += 1)
                            {

                                OrderedCardSet set = new OrderedCardSet(new List<PlayingCard> { totalCards[t1], totalCards[t2], totalCards[t3], totalCards[t4], totalCards[t5] });
                                possibleSets.Add(set);
                            }
                        }
                    }
                }
            }

            return possibleSets;
        }

        public static PokerHand ChooseBestHand(OrderedCardSet handCards, OrderedCardSet playingCards) {

            PokerHand best = null;

            List<OrderedCardSet> createHands = GetCombinationOfPokerHands(handCards, playingCards);


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

        public static List<OrderedCardSet> GetOpponentCombinationOfHandCards(OrderedCardSet fullDeck, OrderedCardSet playerCards, OrderedCardSet communityCards)
        {
            List<PlayingCard> remaining = new List<PlayingCard>(fullDeck.asList());
            remaining.RemoveAll(card => playerCards.asList().Contains(card));
            remaining.RemoveAll(card => communityCards.asList().Contains(card));
            return GetOpponentCombinationOfHandCards(new OrderedCardSet(remaining));
        }

        public static List<OrderedCardSet> GetOpponentCombinationOfHandCards(OrderedCardSet otherCards) {
            IList<PlayingCard> hand = otherCards.asList();
            List<OrderedCardSet> results = new List<OrderedCardSet>();

            for (int prim = 0; prim < hand.Count; prim += 1) {

                for (int sec= prim +1 ; sec < hand.Count; sec += 1)
                {
                    PlayingCard primCard = hand[prim];
                    PlayingCard secondCard = hand[sec];
                    OrderedCardSet cardSet = new OrderedCardSet(new List<PlayingCard> { primCard, secondCard });
                    results.Add(cardSet);
                }
            }



            return results;
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


        public static PokerHand IdentitfyHand(OrderedCardSet cards)
        {

            /*PokerHand hand = null;

             foreach (PokerHand.HandValidator validator in testOrder) {

                 if (validator(cards, out hand)) {
                     break;
                 }
             }

             return hand;*/

            int[] cardFreq = new int[13];
            foreach (PlayingCard card in cards)
            {
                int rank = (int)card.CardRank;
                cardFreq[rank] += 1;


            }

            {//Straight n Flush

                PokerHand hand = DetectStraightNFlush(cardFreq, cards);
                if (hand != null)
                    return hand;

            }


            {//FourKind

                PokerHand hand = DetectFourKind(cardFreq);
                if (hand != null)
                    return hand;

            }

            {//Full House

                PokerHand hand = DetectFullHouse(cardFreq);
                if (hand != null)
                    return hand;

            }
            {//Three Kind

                PokerHand hand = DetectThreeKind(cardFreq);
                if (hand != null)
                    return hand;

            }

            {//Two Pair

                PokerHand hand = DetectTwoPair(cardFreq);
                if (hand != null)
                    return hand;

            }

            {//One Pair

                PokerHand hand = DetectOnePair(cardFreq);
                if (hand != null)
                    return hand;

            }



            return new HighCardHand(cards);
        }


        public static PokerHand DetectStraightNFlush(int[] cardFreq, OrderedCardSet cards) {

            //Flush
            PlayingCard.Suit? flushSuit = null;
            {

                foreach (PlayingCard card in cards)
                {

                    if (flushSuit.HasValue)
                    {
                        if (card.CardSuit != flushSuit.Value)
                        {
                            flushSuit = null;
                            break;
                        }
                    }
                    else
                    {
                        flushSuit = card.CardSuit;
                    }
                }
            }

            //Straight
            int? straightRank = null;

       for (int high = cardFreq.Length - 1; high >= 4; high -= 1)
            {

                for (int index = high; index > high - 5; index -= 1)
                {
                    if (cardFreq[index] < 1)
                    {
                        goto skip;
                    }
                }

                straightRank = high;
                break;
            skip:;
            }


            if (straightRank.HasValue && flushSuit.HasValue)
            {

                return new StraightFlushHand(straightRank.Value, flushSuit.Value);
            }
            else
            {
                if (straightRank.HasValue)
                {
                    return new StraightHand(straightRank.Value);
                }
                else if (flushSuit.HasValue)
                {
                    return new FlushHand(flushSuit.Value, cards);
                }
            }

            return null;

        }
        public static PokerHand DetectFourKind(int[] cardFreq) {
            int? four = null;
            int? one = null;
            for (int index = 0; index < cardFreq.Length; index += 1)
            {

                if (cardFreq[index] == 4 && !four.HasValue)
                {
                    four = index;
                 
                }
                else if (cardFreq[index] == 1 && !one.HasValue)
                {
                    one = index;
                 
                } 

               
            }


   
            if (four.HasValue && one.HasValue)
            {
                return new FourKindHand(four.Value, one.Value);
            }
            else {
                return null;

            }
        }

        public static PokerHand DetectFullHouse(int[] cardFreq)
        {
            int? three = null;
            int? two = null;

            for (int index = 0; index < cardFreq.Length; index += 1)
            {

                if (cardFreq[index] == 3 && !three.HasValue)
                {
                    three = index;

                }
                else if (cardFreq[index] == 2 && !two.HasValue)
                {
                    two = index;

                }
            }



            if (three.HasValue && two.HasValue)
            {
                return new FullHouseHand(three.Value, two.Value);
            }
            else
            {
                return null;

            }
        }


        public static PokerHand DetectThreeKind(int[] cardFreq)
        {
            int? three = null;
            int? low = null;
            int? high = null;

            for (int index = 0; index < cardFreq.Length; index += 1)
            {

                if (cardFreq[index] == 3 && !three.HasValue)
                {
                    three = index;

                }
                else if (cardFreq[index] == 1)
                {
                    if (!low.HasValue)
                    {
                        low = index;
                    }
                    else if (!high.HasValue) {
                        high = index;
                    }

                }
            }



            if (three.HasValue && low.HasValue && high.HasValue)
            {
                return new ThreeKindHand(three.Value, high.Value, low.Value);
            }
            else
            {
                return null;

            }
        }

        public static PokerHand DetectTwoPair(int[] cardFreq)
        {
            int? twoHigh = null;
            int? twoLow = null;
            int? kicker = null;

            for (int index = 0; index < cardFreq.Length; index += 1)
            {

                if (cardFreq[index] == 1 && !kicker.HasValue)
                {
                    kicker = index;

                }
                else if (cardFreq[index] == 2)
                {
                    if (!twoLow.HasValue)
                    {
                        twoLow = index;
                    }
                    else if (!twoHigh.HasValue)
                    {
                        twoHigh = index;
                    }

                }
            }



            if (twoHigh.HasValue && twoLow.HasValue && kicker.HasValue)
            {
                return new TwoPairHand(twoHigh.Value, twoLow.Value, kicker.Value);
            }
            else
            {
                return null;

            }
        }

        public static PokerHand DetectOnePair(int[] cardFreq)
        {
            int? two = null;
            int? kickLow = null;
            int? kickMed = null;
            int? kickHigh = null;

            for (int index = 0; index < cardFreq.Length; index += 1)
            {

                if (cardFreq[index] ==2 && !two.HasValue)
                {
                    two = index;

                }
                else if (cardFreq[index] == 1)
                {
                    if (!kickLow.HasValue)
                    {
                        kickLow = index;
                    }
                    else if (!kickMed.HasValue)
                    {
                        kickMed = index;
                    }
                    else if (!kickHigh.HasValue)
                    {
                        kickHigh = index;
                    }

                }
            }



            if (two.HasValue && kickLow.HasValue && kickMed.HasValue && kickHigh.HasValue)
            {
                return new OnePairHand(two.Value, kickHigh.Value, kickMed.Value, kickLow.Value);
            }
            else
            {
                return null;

            }
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


        private static IEnumerator<ValueTuple<int, int>> forwardLoop(int[] freq) {

            for (int index = 0; index < freq.Length; index+=1) {
                yield return (index, freq[index]);
            }
        }

        private static IEnumerator<ValueTuple<int, int>> backwardLoop(int[] freq)
        {

            for (int index = freq.Length-1; index >=0; index -= 1)
            {
                yield return (index, freq[index]);
            }
        }


        private static int[] getCardFrequency(OrderedCardSet set) {
            int[] freq = new int[13 + 1];//13 ranks + 1 for high ace

            foreach (PlayingCard card in set) { 
                freq[ (int)card.CardRank] +=1;

                if (card.CardRank == PlayingCard.Rank.ACE) {
                    freq[13] += 1;
                }
            }
            return freq;

        }
    }
}
