using System;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{
    public class DefaultCardDealer : ICardDealer
    {
        private OrderedCardSet communityCards;
        private OrderedCardSet leftOverDeck;
        private OrderedCardSet fullDeck;

         public DefaultCardDealer() {

            fullDeck = generateFullDeck();
            communityCards = new OrderedCardSet(new List<PlayingCard>());
            leftOverDeck = new OrderedCardSet(new List<PlayingCard>());
        }

        private OrderedCardSet generateFullDeck() {

            List<PlayingCard> cards = new List<PlayingCard>();
            for (int suit = 0; suit < 4; suit += 1) {
                for (int rank= 0; rank < 13; rank += 1)
                {
                    PlayingCard.Rank cardRank = (PlayingCard.Rank)rank;
                    PlayingCard.Suit cardSuit = (PlayingCard.Suit)suit;
                    cards.Add(new PlayingCard(cardRank, cardSuit));
                }
            }


            return new OrderedCardSet(cards);
        }

        public OrderedCardSet CommunityCards => communityCards;

        public OrderedCardSet RemainingDeck => leftOverDeck;

        public OrderedCardSet FullDeck => fullDeck;

        public void ShuffleAndDealDeck(PokerPlayer playerOne, PokerPlayer playerTwo)
        {
            Random random = new Random();
            List<PlayingCard> cards = new List<PlayingCard>(fullDeck.asList());
            
            //Player one
            List<PlayingCard> oneCards = new List<PlayingCard>();
            oneCards.Add(randomRemove(cards, random));
            oneCards.Add(randomRemove(cards, random));
            playerOne.HandCards = new OrderedCardSet(oneCards);

            //Player two
            List<PlayingCard> twoCards = new List<PlayingCard>();
            twoCards.Add(randomRemove(cards, random));
            twoCards.Add(randomRemove(cards, random));
            playerTwo.HandCards = new OrderedCardSet(twoCards);

            //Community Cards
            List<PlayingCard> comCards = new List<PlayingCard>();
            comCards.Add(randomRemove(cards, random));
            comCards.Add(randomRemove(cards, random));
            comCards.Add(randomRemove(cards, random));
            comCards.Add(randomRemove(cards, random));
            comCards.Add(randomRemove(cards, random));
            communityCards = new OrderedCardSet(comCards);

            leftOverDeck = new OrderedCardSet(cards);

        }

        private PlayingCard randomRemove(List<PlayingCard> cards, Random random) {
            int index = random.Next(cards.Count);
            PlayingCard sel = cards[index];
            cards.RemoveAt(index);

            return sel;
            
        }
    }
}
