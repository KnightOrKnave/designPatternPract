using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace Visitor
{

    public abstract class Visitor
    {
        public abstract void visit(MyFile file);
        public abstract void visit(MyDirectory drectory);
    }

    public interface Element
    {
        public abstract void accept(Visitor v);
    }

    public abstract class Entry : Element
    {
        public abstract string getName();
        public abstract int getSize();
        public Entry add(Entry entry)
        {
            throw new Exception();
        }

        public IEnumerable<Entry> iterator()
        {
            throw new Exception();
        }

        public string toString()
        {
            return $"{getName()}({getSize()})";
        }
    }

    public class MyFile : Entry
    {
        private string name;
        private int size;

        public MyFile(string n,int s)
        {
            name = n;
            size = s;
        }

        public override string getName()
        {
            return name;
        }

        public override int getSize()
        {
            return size;
        }

        public void accept(Visitor v)
        {
            v.visit(this);
        }
    }

    public class MyDirectory : Entry
    {
        private string name;
        private List<Entry> ArrayList;

        public MyDirectory(string n)
        {
            name = n;
            ArrayList = new List<Entry>();
        }
        public override string getName()
        {
            return name;
        }

        public override int getSize()
        {
            int size = 0;
            foreach(var i in ArrayList)
            {
                size += i.getSize();
            }
            return size;
        }

        public new Entry add(Entry ent)
        {
            ArrayList.Add(ent);
            return this;
        }

        public new IEnumerable<Entry> iterator()
        {
            return ArrayList;
        }

        public void accept (Visitor v)
        {
            v.visit(this);
        }
    }

    public class ListVisitor : Visitor
    {
        private string currentdir = "";
        public override void visit(MyFile file)
        {
            Console.WriteLine($"{currentdir}/{file.toString()}");
        }

        public override void visit(MyDirectory drectory)
        {
            Console.WriteLine($"{currentdir}/{drectory.toString()}");
            string savedir = currentdir;
            currentdir = $"{currentdir}/{drectory.getName()}";
            var i = drectory.iterator();
            foreach(var ii in i)
            {
                ii.accept(this);
            }
            currentdir = savedir;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MyDirectory root = new MyDirectory("root");
                MyDirectory bin = new MyDirectory("bin");
                MyDirectory tmp = new MyDirectory("tmp");
                MyDirectory usr = new MyDirectory("user");

                root.add(bin);
                root.add(tmp);
                root.add(tmp);
                root.add(usr);
                bin.add(new MyFile("vi", 1000));
                bin.add(new MyFile("latex", 2000));
                tmp.add(new MyFile("aaa", 333));

                root.accept(new ListVisitor());

                var yuki = new MyDirectory("yjki");
                var h = new MyDirectory("hanako");
                var t = new MyDirectory("tomura");

                usr.add(yuki);
                usr.add(h);
                usr.add(t);
                yuki.add(new MyFile("aaaaa", 210));
                yuki.add(new MyFile("bbb", 210));
                h.add(new MyFile("ccc", 210));
                t.add(new MyFile("dddd", 349));
                t.add(new MyFile("eee", 333));

                root.accept(new ListVisitor());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
