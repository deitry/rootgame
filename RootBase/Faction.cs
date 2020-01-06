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
    public interface IFaction
    {
        // public Faction(FactionType type) { this.Type = type; }

        FactionType Type();

        // обрати внимание, что все объекты одного типа принадлежат одному игроку.
        // Можно различать принадлежность к фракциям по названиям объектов
        // Возможно, практичнее будет сделать что-то вроде фабрики,
        // которая бы возвращала значение по типу фракции.
        // По сути, нам нужна коллекция AvailableFactions, где элементами
        // бы были "карточки" каждой из фракций. Для этого не нужно наследование,
        // но каждый элемент следовало бы хранить отдельно

        // какие объекты являются источником крафта
        string CraftSource();

        // как называются юниты
        string UnitName();

        // список доступных действий для текущей фазы
        List<Action> GetActions(TurnPhase phase);

        // список правил/ограничений/постоянных эффектов, накладываемых данной фракцией
        // TODO: для каждого типа объектов предоставлять правило limit <name> <count>
        // Если такого правила нет, добавлять объект такого типа в хранилище нельзя
        Rules GetRules();
    }
}
