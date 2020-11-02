using System.Buffers;

namespace AbstructFactory
{
    internal class ListPage : Page
    {
        public ListPage(string title, string autor) : base(title, autor)
        {
        }

        public override string makeHTML()
        {
            string str = "";
            str += $"<html><head><title>{title}</title></head>\n";
            str += "<body>";
            str += $"<h1>{title}</h1>\n";
            str += "<ul>\n";
            foreach(var i in content)
            {
                str += i.makeHTML();
            }
            str += "</ul>\n";
            str += $"<hr><address>{autor}</address>";
            str += $"</body></html>";
            return str;
        }
    }
}