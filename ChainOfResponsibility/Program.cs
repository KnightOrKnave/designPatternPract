using System;
using System.Collections;
using System.Linq;

namespace ChainOfResponsibility
{

    public class Trouble
    {
        private int number;
        public Trouble(int number)
        {
            this.number = number;
        }

        public int getNumber()
        {
            return number;
        }

        public override string ToString()
        {
            return $"[Trouble {number}]";
        }
    }

    public abstract class Support
    {
        private string name;
        private Support next;

        public Support(string name)
        {
            this.name = name;
        }

        public Support setNext(Support next)
        {
            this.next = next;
            return next;
        }

        public void support(Trouble trouble)
        {
            if(resolve(trouble))
            {
                done(trouble);
            }
            else if(next !=null)
            {
                next.support(trouble);
            }
            else
            {
                fail(trouble);
            }
        }
        public override string ToString()
        {
            return $"[{name}]";
        }

        protected abstract bool resolve(Trouble trouble);
        protected void done(Trouble trouble)
        {
            Console.WriteLine($"{trouble} is resolved by {this}");
        }
        protected void fail(Trouble trouble)
        {
            Console.WriteLine($"{trouble} is cannot be resolved");
        }
    }

    public class NoSupport : Support
    {
        public NoSupport(string name):base(name)
        {
        }

        protected override bool resolve(Trouble trouble)
        {
            return false;
        }
    }

    public class LimitSupport : Support
    {
        private int limit;
        public LimitSupport(string name,int limit):base(name)
        {
            this.limit = limit;
        }

        protected override bool resolve(Trouble trouble)
        {
            if(trouble.getNumber()<limit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class OddSupport : Support
    {

        public OddSupport(string name) : base(name)
        {

        }

        protected override bool resolve(Trouble trouble)
        {
            if(trouble.getNumber()%2==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class EvenSupport : Support
    {

        public EvenSupport(string name) : base(name)
        {

        }

        protected override bool resolve(Trouble trouble)
        {
            if (trouble.getNumber() % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class SpecialSupport : Support
    {
        private int number;
        public SpecialSupport(string name,int number) : base(name)
        {
            this.number = number;
        }

        protected override bool resolve(Trouble trouble)
        {
            if (trouble.getNumber() == this.number)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Support alice = new NoSupport("Alice");
            Support bob = new LimitSupport("Bob", 100);
            Support charlie = new SpecialSupport("Charlie", 429);
            Support diana = new LimitSupport("Diana", 200);
            Support elmo = new OddSupport("Elmo");
            Support fred = new LimitSupport("Fred", 300);
            alice.setNext(bob).setNext(charlie).setNext(diana).setNext(elmo).setNext(elmo).setNext(fred);

            
            for(int i=0;i<500;i+=33)
            {
                alice.support(new Trouble(i));
            }
        }
    }
}
