using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Facade
{
    public class Database
    {
        private Database() { }

        public static IDictionary<string,string> getProperties(string dbname)
        {
            var dict = new Dictionary<string, string>()
            {
                {"hyuki@yuki.com","Hiroshi Yuki" },
                {"hanako@yuki.com","Hanako Sato" },
                {"tomura@yuki.com","Tomura" },
                {"mamoru@yuki.com","Mamoru Takahashi" },
            };

            return dict;
        }
    }

    public class HtmlWriter
    {
        private StreamWriter writer;
        public HtmlWriter(StreamWriter writer)
        {
            this.writer = writer;
        }

        public async Task title(string title)
        {
            await writer.WriteAsync("<html>");
            await writer.WriteAsync("<head>");
            await writer.WriteAsync($"<title>{title}</title>");
            await writer.WriteAsync("</head>");
            await writer.WriteLineAsync("<body>");
            await writer.WriteLineAsync($"<h1>{title}</h1>");
        }

        public async Task paragraph(string msg)
        {
            await writer.WriteAsync($"<p>{msg}</p>");
        }

        public async Task link(string href,string caption)
        {
            await writer.WriteAsync($@"<a href=""{href}"">{caption}</a>");
        }

        public async Task mailto(string mailaddr,string username)
        {
            await link($"mailto:{mailaddr}", username);
        }

        public async Task close()
        {
            await writer.WriteLineAsync("</body>");
            await writer.WriteLineAsync("</html>");
            writer.Close();
        }
    }

    public class PageMaker
    {
        private PageMaker() { }

        public static async Task makeWelcomPage(string mailaddr,string filename)
        {
            var mailprop = Database.getProperties("aaa");
            if(!mailprop.TryGetValue(mailaddr,out var ov))
            {
                Console.WriteLine("error");
                return;
            }
            string username = ov;
            HtmlWriter wt = new HtmlWriter(new StreamWriter(filename, true));
            await wt.title($"Welcom to {username} `s page!");
            await wt.paragraph($"{username}のページへようこそ");
            await wt.paragraph("メールまってます");
            await wt.mailto(mailaddr, username);
            await wt.close();
        }
    }



    class Program
    {
        static async Task Main(string[] args)
        {
            await PageMaker.makeWelcomPage("hyuki@yuki.com", "welcome.html");
        }
    }
}
