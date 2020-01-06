using System.Collections.Generic;

namespace RootBase
{
    // Возможные состояния машины состояний игрового движка.
    // Мб проще будет разделить на категории и int~инициатива внутри категории
    public enum TurnPhase
    {
        Setup1,
        Setup2,
        Setup3,
        Birdsong1,
        Birdsong2,
        Birdsong3,
        Daylight1,
        Daylight2,
        Daylight3,
        Evening1,
        Evening2,
        Evening3,
        Draw,
    }

    // содержит всё, что необходимо для определения текущей игровой ситуации.
    // По идее, сюда же должна включаться карта. В виде объектов?
    // Состояние ничего не знает о том, какие действия над ним могут выполняться
    public class GameState
    {
        public GameState(Player[] players)
        {
            this.Players = players;
            this.Objects = new List<GameObject>();

            foreach (var player in players)
            {
                // this.Objects.AddRange(player.Faction.InitialObjects(player));
            }
        }

        public uint CurrentTurn { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public TurnPhase CurrentPhase { get; private set; }

        readonly public Player[] Players;

        // List выбран просто за удобство расширения/сокращения списка объектов
        // Список всех объектов в игре
        public List<GameObject> Objects;
        public Map Map = Map.Map1(); // FIXME: выбор карты

        // постоянные эффекты/ограничения/правила в игре.
        // В частности, если в правилах нет строчки вида limit <name> <count>,
        // считать, что такой объект в принципе нельзя добавлять.
        // Проверкой на правила будет заниматься движок.
        // Если не считать, что можно вводить дополнительные правила, то
        // список правил будет определяться картой + фракциями
        public List<string> Rules;

        internal void NextStep()
        {
            // переходим на следующую фазу
            // если фазы кончились, переходим к следующему игроку
            // если игроки кончились, начинаем сначала, увеличиваем счётчик ходов на 1
        }

        // все эти действия могут выполняться одновременно
        public List<Action> GetCurrentAvailableActions()
        {
            return this.CurrentPlayer.Faction.GetActions(this.CurrentPhase);
        }

        // игровые объекты меняться не могут, но может меняться их количество.
        // Помимо самого объекта через аргументы передаём сопутствующие сведения
        // TODO: где должны быть эти события - в стейте или в енжине?
        event ObjectEventHandler ObjectAdded;
        event ObjectEventHandler ObjectRemoved;
        // TODO: сериализация
    }
}
