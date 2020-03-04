using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFPokerTexas.model.pokerhands;
using WPFPokerTexas.model;
using System.Collections.Generic;

namespace UnitTestPokwer
{
    [TestClass]
    public class UnitTest1
    {


        private PokerHand.HandType GetType(PlayingCard one, PlayingCard two, PlayingCard three, PlayingCard four, PlayingCard five) {
            OrderedCardSet cards = new OrderedCardSet(new List<PlayingCard> { one, two, three, four, five });
            PokerHand hand = CardChances.IdentitfyHand(cards);
          
            return hand.Type;
        }

        private PokerHand GetHand(PlayingCard one, PlayingCard two, PlayingCard three, PlayingCard four, PlayingCard five)
        {
            OrderedCardSet cards = new OrderedCardSet(new List<PlayingCard> { one, two, three, four, five });
            PokerHand hand = CardChances.IdentitfyHand(cards);
            return hand;
        }


        [TestMethod]
        public void TestCardIdentificationFlush()
        {
            {//Flush

                PokerHand.HandType expected = PokerHand.HandType.FLUSH;
                PokerHand.HandType actual = GetType(
                    new PlayingCard(PlayingCard.Rank.JACK, PlayingCard.Suit.DIAMONDS),
                 new PlayingCard(PlayingCard.Rank.NINE, PlayingCard.Suit.DIAMONDS),
                   new PlayingCard(PlayingCard.Rank.EIGHT, PlayingCard.Suit.DIAMONDS),
                     new PlayingCard(PlayingCard.Rank.FOUR, PlayingCard.Suit.DIAMONDS),
                       new PlayingCard(PlayingCard.Rank.THREE, PlayingCard.Suit.DIAMONDS));

                Assert.AreEqual(expected, actual, "Not same type");

            }

        }

        [TestMethod]
        public void TestCardIdentificationStraightFlush()
        {
            {//Straight flush 

                PokerHand.HandType expected = PokerHand.HandType.STRAIGHT_FLUSH;
                PokerHand.HandType actual = GetType(new PlayingCard(PlayingCard.Rank.JACK, PlayingCard.Suit.CLUBS),
                 new PlayingCard(PlayingCard.Rank.TEN, PlayingCard.Suit.CLUBS),
                   new PlayingCard(PlayingCard.Rank.NINE, PlayingCard.Suit.CLUBS),
                     new PlayingCard(PlayingCard.Rank.EIGHT, PlayingCard.Suit.CLUBS),
                       new PlayingCard(PlayingCard.Rank.SEVEN, PlayingCard.Suit.CLUBS));

                Assert.AreEqual(expected, actual, "Not same type");

            }

        }

        [TestMethod]
        public void TestCardIdentificationFourKind()
        {
            {//Four kind 

                PokerHand.HandType expected = PokerHand.HandType.FOUR_KIND;
                PokerHand.HandType actual = GetType(
                    new PlayingCard(PlayingCard.Rank.FIVE, PlayingCard.Suit.CLUBS),
                 new PlayingCard(PlayingCard.Rank.FIVE, PlayingCard.Suit.DIAMONDS),
                   new PlayingCard(PlayingCard.Rank.FIVE, PlayingCard.Suit.HEARTS),
                     new PlayingCard(PlayingCard.Rank.FIVE, PlayingCard.Suit.SPADES),
                       new PlayingCard(PlayingCard.Rank.TWO, PlayingCard.Suit.DIAMONDS));

                Assert.AreEqual(expected, actual, "Not same type");

            }

        }

        [TestMethod]
        public void TestCardIdentificationFullHouse()
        {
            {//Full house

                PokerHand.HandType expected = PokerHand.HandType.FULL_HOUSE;
                PokerHand.HandType actual = GetType(
                    new PlayingCard(PlayingCard.Rank.SIX, PlayingCard.Suit.SPADES),
                 new PlayingCard(PlayingCard.Rank.SIX, PlayingCard.Suit.HEARTS),
                   new PlayingCard(PlayingCard.Rank.SIX, PlayingCard.Suit.DIAMONDS),
                     new PlayingCard(PlayingCard.Rank.KING, PlayingCard.Suit.SPADES),
                       new PlayingCard(PlayingCard.Rank.KING, PlayingCard.Suit.HEARTS));

                Assert.AreEqual(expected, actual, "Not same type");

            }

        }

        [TestMethod]
        public void TestCardIdentificationStraight()
        {


            {//Straight

                PokerHand.HandType expected = PokerHand.HandType.STRAIGHT;
                PokerHand.HandType actual = GetType(
                    new PlayingCard(PlayingCard.Rank.TEN, PlayingCard.Suit.DIAMONDS),
                 new PlayingCard(PlayingCard.Rank.NINE, PlayingCard.Suit.SPADES),
                   new PlayingCard(PlayingCard.Rank.EIGHT, PlayingCard.Suit.HEARTS),
                     new PlayingCard(PlayingCard.Rank.SEVEN, PlayingCard.Suit.DIAMONDS),
                       new PlayingCard(PlayingCard.Rank.SIX, PlayingCard.Suit.CLUBS));

                Assert.AreEqual(expected, actual, "Not same type");

            }

        }

        [TestMethod]
        public void TestCardIdentificationThreeKind()
        {


            {//Three of a Kind

                PokerHand.HandType expected = PokerHand.HandType.THREE_KIND;
                PokerHand.HandType actual = GetType(
                    new PlayingCard(PlayingCard.Rank.QUEEN, PlayingCard.Suit.CLUBS),
                 new PlayingCard(PlayingCard.Rank.QUEEN, PlayingCard.Suit.SPADES),
                   new PlayingCard(PlayingCard.Rank.QUEEN, PlayingCard.Suit.HEARTS),
                     new PlayingCard(PlayingCard.Rank.NINE, PlayingCard.Suit.HEARTS),
                       new PlayingCard(PlayingCard.Rank.TWO, PlayingCard.Suit.SPADES));

                Assert.AreEqual(expected, actual, "Not same type");

            }

        }

        [TestMethod]
        public void TestCardIdentificationTwoPair()
        {


            {//Two Pair

                PokerHand.HandType expected = PokerHand.HandType.TWO_PAIR;
                PokerHand.HandType actual = GetType(
                    new PlayingCard(PlayingCard.Rank.JACK, PlayingCard.Suit.HEARTS),
                 new PlayingCard(PlayingCard.Rank.JACK, PlayingCard.Suit.SPADES),
                   new PlayingCard(PlayingCard.Rank.THREE, PlayingCard.Suit.CLUBS),
                     new PlayingCard(PlayingCard.Rank.THREE, PlayingCard.Suit.SPADES),
                       new PlayingCard(PlayingCard.Rank.TWO, PlayingCard.Suit.HEARTS));

                Assert.AreEqual(expected, actual, "Not same type");

            }

        }

        [TestMethod]
        public void TestCardIdentificationOnePair()
        {


            {//One Pair

                PokerHand.HandType expected = PokerHand.HandType.ONE_PAIR;
                PokerHand.HandType actual = GetType(
                    new PlayingCard(PlayingCard.Rank.TEN, PlayingCard.Suit.SPADES),
                 new PlayingCard(PlayingCard.Rank.TEN, PlayingCard.Suit.HEARTS),
                   new PlayingCard(PlayingCard.Rank.EIGHT, PlayingCard.Suit.SPADES),
                     new PlayingCard(PlayingCard.Rank.SEVEN, PlayingCard.Suit.HEARTS),
                       new PlayingCard(PlayingCard.Rank.FOUR, PlayingCard.Suit.CLUBS));

                Assert.AreEqual(expected, actual, "Not same type");

            }

        }


        [TestMethod]
        public void TestCardIdentificationHighCard()
        {


            {//HighCard 

                PokerHand.HandType expected = PokerHand.HandType.HIGH_CARD;
                PokerHand.HandType actual = GetType(
                    new PlayingCard(PlayingCard.Rank.KING, PlayingCard.Suit.DIAMONDS),
                 new PlayingCard(PlayingCard.Rank.QUEEN, PlayingCard.Suit.DIAMONDS),
                   new PlayingCard(PlayingCard.Rank.SEVEN, PlayingCard.Suit.SPADES),
                     new PlayingCard(PlayingCard.Rank.FOUR, PlayingCard.Suit.SPADES),
                       new PlayingCard(PlayingCard.Rank.THREE, PlayingCard.Suit.HEARTS));

                Assert.AreEqual(expected, actual, "Not same type");

            

              }

        }

        [TestMethod]
        public void TestCardComparison()
        {
            //Samples used are from wikipedia (https://en.wikipedia.org/wiki/List_of_poker_hands)
         
            PokerHand straightFlush = GetHand(new PlayingCard(PlayingCard.Rank.JACK, PlayingCard.Suit.CLUBS),
                 new PlayingCard(PlayingCard.Rank.TEN, PlayingCard.Suit.CLUBS),
                   new PlayingCard(PlayingCard.Rank.NINE, PlayingCard.Suit.CLUBS),
                     new PlayingCard(PlayingCard.Rank.EIGHT, PlayingCard.Suit.CLUBS),
                       new PlayingCard(PlayingCard.Rank.SEVEN, PlayingCard.Suit.CLUBS));

            PokerHand fourKind = GetHand(
                    new PlayingCard(PlayingCard.Rank.FIVE, PlayingCard.Suit.CLUBS),
                 new PlayingCard(PlayingCard.Rank.FIVE, PlayingCard.Suit.DIAMONDS),
                   new PlayingCard(PlayingCard.Rank.FIVE, PlayingCard.Suit.HEARTS),
                     new PlayingCard(PlayingCard.Rank.FIVE, PlayingCard.Suit.SPADES),
                       new PlayingCard(PlayingCard.Rank.TWO, PlayingCard.Suit.DIAMONDS));

            PokerHand fullHouse = GetHand(
                    new PlayingCard(PlayingCard.Rank.SIX, PlayingCard.Suit.SPADES),
                 new PlayingCard(PlayingCard.Rank.SIX, PlayingCard.Suit.HEARTS),
                   new PlayingCard(PlayingCard.Rank.SIX, PlayingCard.Suit.DIAMONDS),
                     new PlayingCard(PlayingCard.Rank.KING, PlayingCard.Suit.SPADES),
                       new PlayingCard(PlayingCard.Rank.KING, PlayingCard.Suit.HEARTS));


            PokerHand flush = GetHand(
                 new PlayingCard(PlayingCard.Rank.JACK, PlayingCard.Suit.DIAMONDS),
              new PlayingCard(PlayingCard.Rank.NINE, PlayingCard.Suit.DIAMONDS),
                new PlayingCard(PlayingCard.Rank.EIGHT, PlayingCard.Suit.DIAMONDS),
                  new PlayingCard(PlayingCard.Rank.FOUR, PlayingCard.Suit.DIAMONDS),
                    new PlayingCard(PlayingCard.Rank.THREE, PlayingCard.Suit.DIAMONDS));



            PokerHand straight = GetHand(
               new PlayingCard(PlayingCard.Rank.TEN, PlayingCard.Suit.DIAMONDS),
            new PlayingCard(PlayingCard.Rank.NINE, PlayingCard.Suit.SPADES),
              new PlayingCard(PlayingCard.Rank.EIGHT, PlayingCard.Suit.HEARTS),
                new PlayingCard(PlayingCard.Rank.SEVEN, PlayingCard.Suit.DIAMONDS),
                  new PlayingCard(PlayingCard.Rank.SIX, PlayingCard.Suit.CLUBS));

            PokerHand threeKind = GetHand(
                    new PlayingCard(PlayingCard.Rank.QUEEN, PlayingCard.Suit.CLUBS),
                 new PlayingCard(PlayingCard.Rank.QUEEN, PlayingCard.Suit.SPADES),
                   new PlayingCard(PlayingCard.Rank.QUEEN, PlayingCard.Suit.HEARTS),
                     new PlayingCard(PlayingCard.Rank.NINE, PlayingCard.Suit.HEARTS),
                       new PlayingCard(PlayingCard.Rank.TWO, PlayingCard.Suit.SPADES));

            PokerHand twoPair = GetHand(
                    new PlayingCard(PlayingCard.Rank.JACK, PlayingCard.Suit.HEARTS),
                 new PlayingCard(PlayingCard.Rank.JACK, PlayingCard.Suit.SPADES),
                   new PlayingCard(PlayingCard.Rank.THREE, PlayingCard.Suit.CLUBS),
                     new PlayingCard(PlayingCard.Rank.THREE, PlayingCard.Suit.SPADES),
                       new PlayingCard(PlayingCard.Rank.TWO, PlayingCard.Suit.HEARTS));

            PokerHand onePair = GetHand(
                    new PlayingCard(PlayingCard.Rank.TEN, PlayingCard.Suit.SPADES),
                 new PlayingCard(PlayingCard.Rank.TEN, PlayingCard.Suit.HEARTS),
                   new PlayingCard(PlayingCard.Rank.EIGHT, PlayingCard.Suit.SPADES),
                     new PlayingCard(PlayingCard.Rank.SEVEN, PlayingCard.Suit.HEARTS),
                       new PlayingCard(PlayingCard.Rank.FOUR, PlayingCard.Suit.CLUBS));

            PokerHand highCard =  GetHand(
                    new PlayingCard(PlayingCard.Rank.KING, PlayingCard.Suit.DIAMONDS),
                 new PlayingCard(PlayingCard.Rank.QUEEN, PlayingCard.Suit.DIAMONDS),
                   new PlayingCard(PlayingCard.Rank.SEVEN, PlayingCard.Suit.SPADES),
                     new PlayingCard(PlayingCard.Rank.FOUR, PlayingCard.Suit.SPADES),
                       new PlayingCard(PlayingCard.Rank.THREE, PlayingCard.Suit.HEARTS));

            List<PokerHand> hands = new List<PokerHand> { straightFlush, highCard, onePair, twoPair, flush, straight, threeKind, fourKind, fullHouse };
            hands.Sort();

            Assert.AreEqual(PokerHand.HandType.HIGH_CARD, hands[0].Type, "Failed high cardd");
            Assert.AreEqual(PokerHand.HandType.ONE_PAIR, hands[1].Type, "Failed one pair");
            Assert.AreEqual(PokerHand.HandType.TWO_PAIR, hands[2].Type, "Failed two pair");
            Assert.AreEqual(PokerHand.HandType.THREE_KIND, hands[3].Type, "Failed three kind");
            Assert.AreEqual(PokerHand.HandType.STRAIGHT, hands[4].Type, "Failed straight");
            Assert.AreEqual(PokerHand.HandType.FLUSH, hands[5].Type, "Failed flush");
            Assert.AreEqual(PokerHand.HandType.FULL_HOUSE, hands[6].Type, "Failed full house");
            Assert.AreEqual(PokerHand.HandType.FOUR_KIND, hands[7].Type, "Failed four kind");
            Assert.AreEqual(PokerHand.HandType.STRAIGHT_FLUSH, hands[8].Type, "Failed straight flush");


        }

        [TestMethod]
        public void TestCardDealer()
        {
         


             PokerPlayer pl1 = new FakePokerPlayer();
            PokerPlayer pl2 = new FakePokerPlayer();
            ICardDealer dealer = new DefaultCardDealer();
            dealer.ShuffleAndDealDeck(pl1, pl2);
            

            Assert.AreEqual(52, dealer.FullDeck.Count);
            Assert.AreEqual(43, dealer.RemainingDeck.Count);
            Assert.AreEqual(5, dealer.CommunityCards.Count);
            Assert.AreEqual(2, pl1.HandCards.Count);
            Assert.AreEqual(2, pl2.HandCards.Count);
        }

        [TestMethod]
        public void TestCardCombo()
        {



            PokerPlayer pl1 = new FakePokerPlayer();
            PokerPlayer pl2 = new FakePokerPlayer();
            ICardDealer dealer = new DefaultCardDealer();
            dealer.ShuffleAndDealDeck(pl1, pl2);
            List<OrderedCardSet> p1PossHands = CardChances.GetOpponentCombinationOfHandCards(dealer.FullDeck, pl2.HandCards, dealer.CommunityCards);
            List<OrderedCardSet> p2PossHands = CardChances.GetOpponentCombinationOfHandCards(dealer.FullDeck, pl1.HandCards, dealer.CommunityCards);

            List<PlayingCard> totalCards = new List<PlayingCard>();
            totalCards.AddRange(pl1.HandCards);
            totalCards.AddRange(dealer.CommunityCards);
            Assert.AreEqual(7, totalCards.Count);

            int len = 7;
            int count = 0;

            for (int t1 = 0; t1 < len; t1 += 1)
            {
                for (int t2 = t1 + 1; t2 < len; t2 += 1)
                {
                    for (int t3 = t2 + 1; t3 < len; t3 += 1)
                    {
                        for (int t4 = t3 + 1; t4 < len; t4 += 1)
                        {
                            for (int t5 = t4 + 1; t5 < len; t5 += 1)
                            {

                                count += 1;
                            }
                        }
                    }
                }
            }

            Assert.AreEqual(21, count);

            List<OrderedCardSet> p1PokerHands = CardChances.GetCombinationOfPokerHands(pl1.HandCards, dealer.CommunityCards);
            List<OrderedCardSet> p2PokerHands = CardChances.GetCombinationOfPokerHands(pl2.HandCards, dealer.CommunityCards);

            Assert.AreEqual(990, p1PossHands.Count);
            Assert.AreEqual(990, p2PossHands.Count);
            Assert.AreEqual(21, p1PokerHands.Count);
            Assert.AreEqual(21, p2PokerHands.Count);
        }


        private class FakePokerPlayer : PokerPlayer
        {
            public int Id { get; }

            public int Money { get; set; }
            public PokerHand Hand { get; set; }
            public OrderedCardSet HandCards { get; set; }

            public void NotifyOnTurn(int id)
            {
                throw new System.NotImplementedException();
            }
        }


    }





}
