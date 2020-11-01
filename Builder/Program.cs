using System;
using System.Buffers;
using System.Collections.Generic;

namespace Builder
{

    public abstract class Builder
    {
        public abstract void makeTitle(string title);
        public abstract void makeSTring(string str);
        public abstract void makeItems(IEnumerable<string> items);
        public abstract void close();
    }

    public class Director
    {
        private Builder builder;
        public Director(Builder builder)
        {
            this.builder = builder;
        }

        public void construct()
        {
            builder.makeTitle("Greeting");
            builder.makeSTring("朝から昼にかけて");
            builder.makeItems(new string[] { "おはようございます", "こんにちは" });
            builder.makeSTring("夜に");
            builder.makeItems(new string[] { "こんばんわ", "おやすみなさい" ,"さようなら"});
            builder.close();
        }
    }

    public class TextBuilder : Builder
    {
        private string buffer;


        public string getResult()
        {
            return buffer;
        }

        public override void close()
        {
            buffer += "=====================================\n";
        }

        public override void makeItems(IEnumerable<string> items)
        {
            foreach(string s in items)
            {
                buffer += $" ・{s}\n";
            }
            buffer += "\n";
        }

        public override void makeSTring(string str)
        {
            buffer += $"■{str}\n";
        }

        public override void makeTitle(string title)
        {
            buffer += "=====================================\n";
            buffer += $"『{title}』\n";
            buffer += "\n";

        }
    }

    public class HTMLBuilder : Builder
    {

        private string buffer;

        public override void close()
        {
            buffer += "</body></html>";
        }

        public override void makeItems(IEnumerable<string> items)
        {
            buffer += "<ul>";
            foreach(var i in items)
            {
                buffer += $"<li>{i}</li>";
            }
            buffer += "</ul>";
        }

        public override void makeSTring(string str)
        {
            buffer += $"<p>{str}</p>";
        }

        public override void makeTitle(string title)
        {
            buffer += $"<html><head><title>{title}</title></head><body>";
            buffer += $"<h1>{title}</h1>";
        }

        public string getResult()
        {
            return buffer;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TextBuilder textbuilder = new TextBuilder();
            Director director = new Director(textbuilder);
            director.construct();
            string res = textbuilder.getResult();
            Console.WriteLine(res);

            HTMLBuilder htmlbuilder = new HTMLBuilder();
            Director director2 = new Director(htmlbuilder);
            director2.construct();
            string rres = htmlbuilder.getResult();
            Console.WriteLine(rres);
        }
    }
}
