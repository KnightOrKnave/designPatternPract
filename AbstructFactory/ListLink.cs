namespace AbstructFactory
{
    internal class ListLink : Link
    {
        public ListLink(string caption, string url) : base(caption, url)
        {
        }

        public override string makeHTML()
        {
            return $@"<li><a href=""{url}"">{caption}</a></li>\n";
        }
    }
}