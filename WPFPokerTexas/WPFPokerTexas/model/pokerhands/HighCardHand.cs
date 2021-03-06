﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class HighCardHand : PokerHand
    {

 
        public PokerHand.HandType Type => PokerHand.HandType.HIGH_CARD;

        public HighCardHand(OrderedCardSet cards) {
            CardSet = cards;
        
        }


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
                    HighCardHand otherHand = other as HighCardHand;
                    if (other == null)
                    {
                        throw new ArgumentException("Not valid hand");
                    }

                    OrderedCardSet self = CardSet;
                    OrderedCardSet outter = otherHand.CardSet;
                    for (int high = CardSet.Count - 1; high >= 0; high -= 1)
                    {
                        PlayingCard first = self.asList()[high];
                        PlayingCard second = outter.asList()[high];

                        compare = first.CompareTo(second);

                        if (compare != 0)
                        {
                            return compare;

                        }

                    }

                    return 0;

                }
                else
                {
                    return compare;
                }
            }
        }
    }
}
