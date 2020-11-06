using System;

namespace Strategy
{
    public interface Strategy
    {
        public abstract Hand nextHand();
        public abstract void study(bool win);
    }

    public class WinningStrategy : Strategy
    {
        private Random random;
        private bool won = false;
        private Hand prevHand;

        public WinningStrategy(int seed)
        {
            random = new Random(seed);
        }

        public Hand nextHand()
        {
            if(!won)
            {
                prevHand = Hand.getHand(random.Next(3));
            }
            return prevHand;
        }

        public void study(bool win)
        {
            this.won = win;
        }
    }

    public class ProbStrategy : Strategy
    {
        private int[,] history;
        private Random random;
        private int prevHandValue = 0;
        private int currentHandVaue = 0;
        public ProbStrategy(int seed)
        {
            history = new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            random = new Random(seed);
        }

        public Hand nextHand()
        {
            int bet = random.Next(getSum(currentHandVaue));
            int handvalue = 0;
            if(bet< history[1,1])
            {
                handvalue = 0;
            }
            else if(bet < history[currentHandVaue,0]+history[currentHandVaue,1])
            {
                handvalue = 1;
            }
            else
            {
                handvalue = 2;
            }
            prevHandValue = currentHandVaue;
            currentHandVaue = handvalue;
            return Hand.getHand(handvalue);
        }

        private int getSum(int hv)
        {
            int sum = 0;
            for(int i=0;i<3;i++)
            {
                sum += history[hv,i];
            }
            return sum;
        }

        public void study(bool win)
        {
            if(win)
            {
                history[prevHandValue,currentHandVaue]++;
            }
            else
            {
                history[prevHandValue, (currentHandVaue+1)%3]++;
                history[prevHandValue, (currentHandVaue + 2) % 3]++;
            }
        }
    }

    public class Player
    {
        private string name;
        private Strategy strategy;
        private int wincount;
        private int losecount;
        private int gamecount;
        public Player(string name,Strategy strategy)
        {
            this.name = name;
            this.strategy = strategy;
        }

        public Hand nextHand()
        {
            return strategy.nextHand();
        }

        public void win()
        {
            this.strategy.study(true);
            wincount++;
            gamecount++;
        }

        public void lose()
        {
            strategy.study(false);
            losecount++;
            gamecount++;
        }

        public void even()
        {
            gamecount++;
        }

        public string toString()
        {
            return $"[{name}:{gamecount} games,{wincount} win,{losecount} lose]";
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            int seed1 = r.Next(1000);
            int seed2 = r.Next(1000);

            Player player1 = new Player("player1", new WinningStrategy(seed1));
            Player player2 = new Player("player2", new WinningStrategy(seed2));

            for(int i=0;i<10000;i++)
            {
                Hand nexthand1 = player1.nextHand();
                Hand nexthand2 = player2.nextHand();
                if(nexthand1.isStrongerThan(nexthand2))
                {
                    Console.WriteLine($"Winner:{ player1.toString() }");
                    player1.win();
                    player2.lose();
                }
                else if(nexthand2.isStrongerThan(nexthand1))
                {
                    Console.WriteLine($"Winner:{ player2.toString() }");
                    player2.win();
                    player1.lose();
                }
                else
                {
                    Console.WriteLine("Even");
                    player1.even();
                    player2.even();
                }
            }
            Console.WriteLine("Result");
            Console.WriteLine(player1.toString());
            Console.WriteLine(player2.toString());
        }
    }
}
