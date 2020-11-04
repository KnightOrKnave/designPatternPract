using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy
{
    class Hand
    {
        public static int HANDVALUE_GUU = 1;
        public static int HANDVALUE_CHO = 2;
        public static int HANDVALUE_PAA = 3;
        public static Hand[] hand =
        {
            new Hand(HANDVALUE_GUU),
            new Hand(HANDVALUE_CHO),
            new Hand(HANDVALUE_PAA)
        }
        private static string[] name = { "グー", "チョキ", "パー" };
        private int handvalue;

        private Hand(int handval)
        {
            this.handvalue = handval;
        }
        public static Hand getHand(int handvalue)
        {
            return hand[handvalue];
        }

        public bool isStrongerThan(Hand h)
        {
            return fight(h) == 1;
        }
        public bool isWeakerThan(Hand h)
        {
            return fight(h) == -1;
        }

        private int fight(Hand h)
        {
            if(this == h)
            {
                return 0;
            }
            else if((this.handvalue+1)%3==h.handvalue)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public string toString()
        {
            return name[handvalue];
        }
    }
}
