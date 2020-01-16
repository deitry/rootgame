using System.Collections.Generic;

namespace RootBase
{
    /*
    Здесь перечисляем базовые функции получения сведений, которые будут доступны для Action.
    Перечисленные здесь функции не изменяют состояние игры, а только получают некоторые
    конкретные сведения, необходимые для выполнения действий.
    */
    public class GameInterface
    {
        public GameInterface(GameState state)
        {
            this.State = state;
        }

        // МЕТОДЫ, ДАЮЩИЕ СВЕДЕНИЯ

        // из этого класса состояние показываем только опосредованно, через методы
        private GameState State { get; set; }

        // возвращает клетку по метке
        internal Site FindSite(string name) { return null; }

        internal List<Site> GetCorners() { return null; }

        // возвращает клетку, которая противоположна заданной
        internal Site Opposite(Site from) { return null; }

        // кто владеет этой клеткой
        FactionType OwnedBy(Site where) { return FactionType.None; }

        // кто есть в указанной зоне
        internal List<GameObject> WhoIsThere(Site where) { return null; }

        internal List<GameObject> SelectPlayerObjects(Site where, Player player) { return null; }

        // возвращает список мест, в которые можно попасть из указанной клетки
        List<Site> WhereToMove(Site from) { return null; }

        public TurnPhase CurrentPhase { get { return this.State.CurrentPhase; } }

        // сколько есть объектов заданной масти для крафта у заданной фракции
        int HowManyCraftSources(FactionType faction, CardSuit suit)
        {
            // получаем количество объектов типа IFaction.CraftSource && !activated
            return 0;
        }

        // сколько есть объектов указанного типа
        int HowManyObjects(string objType)
        {
            return this.State.Objects.FindAll(item => item.Name == objType).Count;
        }

        // есть ли воины указанной фракции
        bool HasWarriors(FactionType faction, Site where)
        {
            return this.State.Objects.Find(item => item.Attributes["site"] == where.Name && item.Name == "warrior") != null;
        }

        // есть ли свободный слот под здание
        bool HasSlot(Site where) { return false; }

        // для тестирования - дефолтный набор игроков, если вдруг надо где подставить
        public static Player[] DefaultPlayerSet()
        {
            return new Player[] {
                new Player(new MockController(), FactionBuilder.Get(FactionType.MarquiseDeKote)),
                new Player(new MockController(), FactionBuilder.Get(FactionType.WoodlandAlliance)),
            };
        }
    }
}
