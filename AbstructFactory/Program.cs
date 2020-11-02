using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;

namespace AbstructFactory
{

    public abstract class Item
    {
        protected string caption;
        public Item(string caption)
        {
            this.caption = caption;
        }

        public abstract string makeHTML();
    }

    public abstract class Link : Item
    {

        protected string url;
        public Link(string caption, string url) : base(caption)
        {
            this.url = url;
        }
    }

    public abstract class Tray : Item
    {
        protected List<Item> tray;
        public Tray(string caption):base(caption)
        {
            tray = new List<Item>();
        }
        public void add(Item item)
        {
            tray.Add(item);
        }
    }

    public abstract class Page
    {
        protected string title;
        protected string autor;
        protected List<Item> content;
        public Page(string title,string autor)
        {
            this.title = title;
            this.autor = autor;
        }
        public void add(Item item)
        {
            content.Add(item);
        }

        public void output()
        {
            try
            {
                string s="";
                s += this.makeHTML();
            }catch(Exception e)
            {

            }
        }
        public abstract string makeHTML();
    }

    public abstract class Factory
    {
        public static Factory GetFactory(string classname)
        {
            Factory factory = (Factory)Activator.CreateInstance(Type.GetType(classname));
            return factory;
        }
        public abstract Link createLink(string caption, string url);
        public abstract Tray createTray(string caption);
        public abstract Page createPage(string title, string autor);
    }


    /// <summary>
    /// 実態クラス
    /// Listxxx は別ファイル
    /// </summary>
    /// 

    public class ListFactory : Factory
    {
        public override Link createLink(string caption, string url)
        {
            return new ListLink(caption, url);
        }

        public override Tray createTray(string caption)
        {
            return new ListTray(caption);
        }

        public override Page createPage(string title, string autor)
        {
            return new ListPage(title, autor);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = Factory.GetFactory("ListFactory");
            Link asahi = factory.createLink("aaaa", "http://aaaa.com/");
            Link yomiuri = factory.createLink("yyyy", "http://yyyy.com/");
            Link uyahoo = factory.createLink("yahoo", "http://yahoo.com/");
            Link jyahoo = factory.createLink("yahooj","http://yahoo.co.jp/");
            Link excite = factory.createLink("exite", "http://excite.com/");
            Link google = factory.createLink("google", "http://google.com/");

            Tray traynews = factory.createTray("newspapaer");
            traynews.add(asahi);
            traynews.add(yomiuri);

            Tray yahoo = factory.createTray("yahoo");
            yahoo.add(uyahoo);
            yahoo.add(jyahoo);

            Tray trayengin = factory.createTray("search engine");
            trayengin.add(yahoo);
            trayengin.add(excite);
            trayengin.add(google);

            Page page = factory.createPage("LinkPage", "aaauuutttooorrr");
            page.add(traynews);
            page.add(trayengin);
            page.output();
        }
    }
}
