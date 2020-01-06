using System.Collections.Generic;

namespace RootBase
{
    enum LinkType
    {
        Road,
        River,
        RiverBridge,
        // ForestRoad, // связь с чащей
        // вместо ForestRoad будем использовать просто Road,
        // а для проверки проходиости надо будет оценивать, является ли конечный пункт лесом
    }

    class Link
    {
        public Link(LinkType type, string site1, string site2)
        {
            this.Type = type;
            this.Site1 = site1;
            this.Site2 = site2;
        }

        // properties
        public LinkType Type { get; private set; }

        public string Site1;
        public string Site2;
    }

    enum SiteType
    {
        Village,
        Forest,
    }

    enum SiteSuit
    {
        None, // для чащи
        Mouse,
        Rabbit,
        Fox,
    }

    class Site : GameObject
    {
        public Site(SiteType type, string Name, SiteSuit suit = SiteSuit.None)
            : base(Name, "map site")
        {
            this.Type = type;
            if (this.Type == SiteType.Village)
            {
                if (suit == SiteSuit.None)
                    throw new System.Exception("Site must have suit other than None");

                // пока что всем деревням даём по два слота
                this.Slots = 2;

                // FIXME: руины раскидывать случайным образом?
            }
        }

        // имя пока не разрешаем менять в процессе игры, потому что идентификатор
        // - в дальнейшем можно разрешить это делать тем, кто владеет поляной

        public SiteType Type { get; }
        public SiteSuit Suit { get; }

        // максимальное количество слотов под здания
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

        public static Map Map1()
        {
            // самая простая карта, которую можно придумать
            // TODO: не забыть научиться указывать, какие клетки являются противоположными

            return new Map(
                new List<Site>
                {
                    new Site(SiteType.Village, "Village1", SiteSuit.Fox),
                    new Site(SiteType.Village, "Village2", SiteSuit.Mouse),
                    new Site(SiteType.Village, "Forest1", SiteSuit.Mouse),
                    new Site(SiteType.Village, "Forest2", SiteSuit.Mouse),
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
