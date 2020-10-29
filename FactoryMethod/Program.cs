using System;
using System.Collections;
using System.Collections.Generic;

namespace FactoryMethod
{
    public abstract class Product
    {
        public abstract void use();
    }

    public abstract class Factory
    {
        public Product create(string owner)
        {
            Product p = createProduct(owner);
            registerProduct(p);
            return p;
        }

        protected abstract Product createProduct(string owner);
        protected abstract void registerProduct(Product product);
    }

    public class IDCard:Product
    {
        private string owner;

        public IDCard(string owner)
        {
            Console.WriteLine($"{owner}のカードを作ります");
            this.owner = owner;
        }

        public override void use()
        {
            Console.WriteLine($"{this.owner}のカードを使います");
        }

        public string getOwner()
        {
            return this.owner;
        }
    }

    public class IdCardFactory : Factory
    {
        private List<string> owners = new List<string>();

        protected override Product createProduct(string owner)
        {
            return new IDCard(owner);
        }

        protected override void registerProduct(Product product)
        {
            owners.Add(((IDCard)product).getOwner());
        }

        public IEnumerable getOwners()
        {
            return owners;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Factory fc = new IdCardFactory();
            Product card1 = fc.create("あああ");
            Product card2 = fc.create("いいい");
            Product card3 = fc.create("ううう");

            card1.use();
            card2.use();
            card3.use();
        }
    }
}
