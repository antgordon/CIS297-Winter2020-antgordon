using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class StraightFlushHand : PokerHand
    {
        public PokerHand.HandType Type => PokerHand.HandType.STRAIGHT_FLUSH;

        public StraightFlushHand(int rank, PlayingCard.Suit suit, OrderedCardSet cards)
        {
            CardSet = cards;
            CardRank = rank;
            CardSuit = suit;
        }

        public int CardRank { get; }
        public PlayingCard.Suit CardSuit { get; }

        public OrderedCardSet CardSet { get; }

        public int CompareTo([AllowNull] PokerHand other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                int compare = PokerHand.compareHandRank(this, other);

                if ( compare == 0)
                {
                    StraightFlushHand otherHand = other as StraightFlushHand;
                    if (other == null)
                    {
                        throw new ArgumentException("Not valid hand");
                    }

                    compare = this.CardRank - otherHand.CardRank;



                    return compare;

                }
                else
                {
                    return compare;
                }
            }
        }
    }
}
