using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class FourKindHand : PokerHand
    {
    OrderedCardSet
        public PokerHand.HandType Type => PokerHand.HandType.FOUR_KIND;


        public FourKindHand(int fourCardRank, int kicker) {
            FourCardRank = fourCardRank;
            Kicker = kicker;
        }

        public int FourCardRank { get; }

        public int Kicker { get; }
        public int CompareTo([AllowNull] PokerHand other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {

                if (PokerHand.compareHandRank(this, other) == 0)
                {
                   FourKindHand otherHand = other as FourKindHand;
                    if (other == null)
                    {
                        throw new ArgumentException("Not valid hand");
                    }

                    OrderedCardSet self = CardSet;
                    OrderedCardSet outter = otherHand.CardSet;
                    for (int high = CardSet.Count - 1; high >= 0; high += 1)
                    {
                        PlayingCard first = self.asList()[high];
                        PlayingCard second = outter.asList()[high];

                        int compare = first.CompareTo(second);

                        if (compare != 0)
                        {
                            return compare;

                        }

                    }

                    return 0;

                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
