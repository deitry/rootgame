using System.Collections.Generic;

namespace RootBase
{
    enum LinkType
    {
        Road,
        River,
        RiverBridge,
    }

    class Link
    {
        public Link(LinkType type, string site1, string site2)
        {
            this.Type = type;
            this.Site1 = site1;
            this.Site2 = site2;
        }

        public LinkType Type { get; private set; }

        public string Site1;
        public string Site2;
    }

    enum SiteSuit
    {
        Mouse,
        Rabbit,
        Fox,
    }

    internal class Site : GameObject
    {
        public Site(string name) : base(name, "map site") { }
    }

    class Forest : Site
    {
        public Forest(string name) : base(name) { }
    }

    class Village : Site
    {
        public Village(string name, SiteSuit suit)
            : base(name)
        {
            // пока что всем деревням даём по два слота
            this.Slots = 2;
            this.Suit = suit;
        }

        public SiteSuit Suit { get; }

        // максимальное количество строений, которое можно здесь разместить
        public int Slots { get; private set; }
    }

    public class Map
    {
        // разделяем пока места и связи между ними
        List<Site> Sites;
        List<Link> Links;

        Map(List<Site> sites, List<Link> links)
        {
            this.Sites = sites;
            this.Links = links;
        }

        // самая простая карта, которую можно придумать
        public static Map Map1()
        {
            return new Map(
                new List<Site>
                {
                    new Village("Village1", SiteSuit.Fox),
                    new Village("Village2", SiteSuit.Mouse),
                    new Forest("Forest1"),
                    new Forest("Forest2"),
                },
                new List<Link>
                {
                    // самая простая карта
                    new Link(LinkType.Road, "Village1", "Village2"),
                    new Link(LinkType.Road, "Village1", "Forest1"),
                    new Link(LinkType.Road, "Village2", "Forest2"),
                    new Link(LinkType.RiverBridge, "Forest1", "Forest2"),
                }
            );
        }
    }


}
