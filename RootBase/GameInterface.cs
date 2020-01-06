using System.Collections.Generic;

namespace RootBase
{
    /*
    Здесь перечисляем все функции, которые будут доступны игрокам.

    Создаём этакий класс Б-га, чтобы понять, что вообще нам надо от игры.
    Потом уже будем рефакторить
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

        // возвращает клетку, которая противоположна заданной
        internal Site Opposite(Site from) { return null; }

        // кто владеет этой клеткой
        FactionType OwnedBy(Site where) { return FactionType.None; }

        // кто есть в указанной зоне
        List<FactionType> WhosThere(Site where) { return null; }

        // возвращает список мест, в которые можно попасть из указанной клетки
        List<Site> WhereToMove(Site from) { return null; }

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
                new Player(FactionType.MarquiseDeKote),
                new Player(FactionType.WoodlandAlliance),
            };
        }


        // ДЕЙСТВИЯ
        // FIXME: не факт, что действия должны быть аналогичным образом перечислены вот здесь или вообще
        void Move(GameObject obj, Site toWhere) { }
        void Fight(Site where, FactionType who, FactionType withWhom) { }

        // добавляет новый объект в базу, если выполняется условие для фракции...
        void Craft(FactionType who, GameObject what)
        {
            // проверяем, есть ли у фракции достаточно источников для крафта
            // если да, помечаем источники как activated
        }
    }
}
