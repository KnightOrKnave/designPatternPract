using System;
using System.Collections.Generic;

namespace Memento
{

    public class Memento
    {
        public int _money;
        public List<string> _fruits;

        public int GetMoney()
        {
            return _money;
        }

        public Memento(int money)
        {
            _money = money;
            _fruits = new List<string>();
        }

        public void AddFruit(string fruit)
        {
            _fruits.Add(fruit);
        }

        IEnumerable<string>GetFruits()
        {
            return _fruits;
        }
    }

    public class Gamer
    {
        private int _money;
        private List<string> _fruits;
        private Random _random;
        private static string[] fruitsame =
        {
            "リンゴ","ぶどう","バナナ","みかん"
        };
        public Gamer(int money)
        {
            _money = money;
            _fruits = new List<string>();
            _random = new Random();
        }

        public int GetMoney()
        {
            return _money;
        }

        public void Bet()
        {
            int dice = _random.Next(1, 6);
            if (dice == 1)
            {
                _money += 100;
                Console.WriteLine("所持金が増えました");
            }
            else if (dice == 2)
            {
                _money /= 2;
                Console.WriteLine("所持金が半分になりました");
            }
            else if(dice==6)
            {
                string f = GetFruit();
                Console.WriteLine($"フルーツ({f})をもらいました");
            }
            else
            {
                Console.WriteLine("何も起こりませんでした");
            }
        }

        public Memento CreateMemento()
        {
            Memento m = new Memento(_money);
            foreach(var f in _fruits)
            {
                if (f.StartsWith("おいしい"))
                {
                    m.AddFruit(f);
                }
            }
            return m;
        }

        public void RestoreMemento(Memento memento)
        {
            _money = memento._money;
            _fruits = memento._fruits;
        }

        public override string ToString()
        {
            return $"money = {_money} fruits = {_fruits}";
        }
        private string GetFruit()
        {
            string prefix = "";
            if (_random.Next(0, 1) != 0)
            {
                prefix = "おいしい";
            }
            return $"{prefix}{fruitsame[_random.Next(0, fruitsame.Length - 1)]}";
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
