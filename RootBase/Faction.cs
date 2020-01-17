using System.Collections.Generic;

namespace RootBase
{
    public enum FactionType
    {
        // число означает очерёдность хода
        // TODO: перенести очерёдность хода в билдер?
        None = 0,
        MarquiseDeKote = 1,
        EyrieDinasties = 2,
        WoodlandAlliance = 3,
        Vagabond1 = 4,
        Vagabond2 = 5,
        ClockworkMarquise = 6,
    }

    // класс, отвечающий за всю совокупность доступных фракций
    class FactionBuilder
    {
        public static IFaction Get(FactionType faction)
        {
            // FIXME: поскольку в рамках одной игры фракции есть только в единичном экземпляре,
            // здесь можно создать их только один раз и каждый последующий тупо брать готовое
            // NOTE: а бродяг может быть два.

            switch (faction)
            {
                case FactionType.MarquiseDeKote: return new Marquise();
                case FactionType.WoodlandAlliance: return new Alliance();
                default:
                    throw new System.Exception("Not implemented yet");
            }
        }
    }

    // FIXME: интерфейс и наследование нужны по сути только для разделения на несколько файлов.
    // для каждой фракции достаточно по сути отдельного инстанса.
    // Фракции должны быть оформлены максимально одинаково, чтобы движок
    // мог единообразно обрабатывать все правила
    // TODO: Если останется абстрактным классом, переименовать в BaseFaction
    public abstract class IFaction
    {
        // По сути, нам нужна коллекция AvailableFactions, где элементами
        // бы были "карточки" каждой из фракций. Для этого не нужно наследование,
        // но каждый элемент следовало бы хранить отдельно

        internal List<CardEffect> Effects;

        // список доступных действий для текущей фазы
        abstract internal List<Action> GetActions(TurnStep phase);

        // список правил/ограничений/постоянных эффектов, накладываемых данной фракцией
        // TODO: для каждого типа объектов предоставлять правило limit <name> <count>
        // Если такого правила нет, добавлять объект такого типа в хранилище нельзя
        // NOTE: поскольку от общего хранилища я похоже отказываюсь, контролировать лимиты сможет сама фракция
        internal abstract Rules GetRules();

        // TODO: вынести в отдельные классы
        internal virtual List<Card> Hand { get; private protected set; }
        internal virtual List<GameObject> Inventory { get; private protected set; }

        // работа с картами - одинаковая  для всех фракций.
        // При необъодимости реализацию можно заменить в подклассах

        internal List<Card> Discard(uint amount)
        {
            var discarded = new List<Card> { };

            for (uint i = 0; i < amount; i++)
            {
                if (Hand.Count == 0) break;

                // случайный vs pick
                discarded.Add(Hand[0]);
                Hand.RemoveAt(0);
            }
            return discarded;
        }

        // инвентарь
        internal void AddObject(GameObject obj) { Inventory.Add(obj); }
        internal GameObject TakeObject(GameObject obj)
        {
            if (Inventory.Contains(obj) && Inventory.Remove(obj))
                return obj;

            return null;
        }

        public abstract int ResourceAmount(ResourceType type);

        // в качестве типа параметра сознательно оставлен знаковый инт:
        // можно "потратить" -1, тем самым добавив 1 ресурса до конца хода
        internal abstract bool SpendResource(ResourceType type, int amount);

        // Dictionary<SiteSuit, int>
        internal bool SpendResource(TotalCost cost)
        {
            // оценить, есть ли столько ресурсов
            return false;
        }
    }
}
