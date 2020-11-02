namespace AbstructFactory
{
    internal class ListTray : Tray
    {
        public ListTray(string caption) : base(caption)
        {
        }

        public override string makeHTML()
        {
            string str="";
            str += "<li>\n";
            str += $"{caption}\n";
            str += "<ul>\n";
            foreach (var i in this.tray)
            {
                str += $"{i.makeHTML()}\n";
            }
            str += "</ul>\n";
            str += "</li>\n";
            return str;
        }
    }
}