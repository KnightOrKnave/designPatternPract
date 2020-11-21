using System;
using System.Collections.Generic;

namespace Flyweight
{

    public class BigChar
    {
        private char _charname;
        private string _fontdata;

        public BigChar(char charname)
        {
            _charname = charname;
            //フォントデータ作るの省くので，そのまま文字列化
            _fontdata = $"{charname}";
        }

        public void print()
        {
            Console.WriteLine(_fontdata);
        }
    }

    public class BigCharFactory
    {
        private Dictionary<char, BigChar> _pool;

        private static BigCharFactory _singleton = new BigCharFactory();

        private BigCharFactory() { _pool = new Dictionary<char, BigChar>(); }

        public static BigCharFactory getInstance()
        {
            return _singleton;
        }

        public BigChar getBigChar(char charname)
        {
            if (!_pool.ContainsKey(charname))
            {
                _pool.Add(charname, new BigChar(charname));
            }
            return _pool.GetValueOrDefault(charname, null);
        }
    }
    public class BigString
    {
        private List<BigChar> _bigchars;

        public BigString(string str)
        {
            _bigchars = new List<BigChar>();
            var factry = BigCharFactory.getInstance();

            foreach(char c in str)
            {
                _bigchars.Add(factry.getBigChar(c));
            }
        }

        public void print()
        {
            foreach(var c in _bigchars)
            {
                c.print();
            }
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            var s = new BigString("hogehoge123");
            s.print();

        }
    }
}
