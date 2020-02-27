using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model
{
    public class PlayingCard: IComparable<PlayingCard> { 


        public enum Rank {  TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE}
        public enum Suit { CLUBS, DIAMONDS, HEARTS, SPADES}

        public Rank CardRank { get; }
        public Suit CardSuit { get; }


        public PlayingCard(Rank rank, Suit suit) {
            CardRank = rank;
            CardSuit = suit;
        
        }

        public int CompareTo([AllowNull] PlayingCard other)
        {
            if (other == null)
            {
                return 1;
            }
            else {
                if (CardRank != other.CardRank)
                {
                    return (int)CardRank - (int)other.CardRank;
                }
                else {
                    return (int)CardSuit - (int)other.CardSuit;
                }
            }
        }
    }


}
