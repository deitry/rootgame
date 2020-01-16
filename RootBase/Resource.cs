namespace RootBase
{
    // ресурсы - некоторые величины, которые могут менять своё значение в середине
    // хода, но восстанавливаются к началу следующего
    public enum ResourceType
    {
        Fox,
        Rabbit,
        Mouse,
        MP, // movement point
        PossibleActions, // тоже может использоваться как ресурс, например у Маркизы
    }

    class SiteSuitToResource
    {
        private SiteSuitToResource() {}

        static ResourceType Convert(SiteSuit suit)
        {
            switch (suit)
            {
                case SiteSuit.Fox: return ResourceType.Fox;
                case SiteSuit.Rabbit: return ResourceType.Rabbit;
                case SiteSuit.Mouse: return ResourceType.Mouse;
                default: throw new System.Exception("Suit can not be converted!");
            }
        }
    }
}
