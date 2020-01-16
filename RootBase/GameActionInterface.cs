using System;

namespace RootBase
{
    public class DiceRoll
    {
        public readonly uint First;
        public readonly uint Second;

        const int MAX_DICE_VALUE = 4;

        // TODO: random(3)
        public DiceRoll()
        {
            var rnd = new Random();
            this.First = (uint)rnd.Next(DiceRoll.MAX_DICE_VALUE);
            this.Second = (uint)rnd.Next(DiceRoll.MAX_DICE_VALUE);
        }
    }

    /*
    Класс предоставляет набор базовых действий, которые можно проводить с игрой.
    Конкретные Action могут их использовать.
    В отличие от функций в GameInterface, приведённые здесь методы изменяют
    состояние игры.
    */
    public class GameActionInterface
    {
        // properties
        public GameState State { get; private set; }
        public readonly GameInterface Interface;

        // ctor
        public GameActionInterface(GameState state, GameInterface gameIf)
        {
            this.State = state;
            this.Interface = new GameInterface(this.State);
        }

        // - - - - - - -
        // добавить новый объект
        public void AddObject(GameObject obj)
        {
            // не забыть проверить, можно ли вообще создать
            // this.State.Rules
            this.State.Objects.Add(obj);
        }

        public GameObject RemoveObject(GameObject obj)
        {
            if (this.State.Objects.Contains(obj))
            {
                this.State.Objects.Remove(obj);
                return obj;
            }
            return null;
        }

        // передвинуть фишку
        void Move(GameObject obj, Site from, Site to)
        {
            // проверить, есть ли данный объект в точке from
            // замерить расстояние между from и to, проверить, что у игрока достаточно очков перемещения
            // осуществить перемещение
        }

        void Fight(Site where, Player player, Player enemy)
        {
            var roll = new DiceRoll();

            void killFew(uint howMany, Player player1, Player player2)
            {
                for (uint i = 0; i < howMany; i++)
                {
                    // каждый раз получаем список заново, чтобы иметь актуальный
                    var objects = this.Interface.SelectPlayerObjects(where, player2);
                    if (objects.Count == 0) break;

                    this.RemoveObject(player1.Controller.PickObject(objects));
                };
            };

            killFew(roll.First, player, enemy);
            killFew(roll.Second, enemy, player);
        }

        // сбросить карту
        // uint - чтобы нельзя было сбросить < 0
        void DiscardCard(Player player, uint amount = 1)
        {
            // переносим карты на кладбище
            State.Discard.AddRange(player.Discard((uint)amount));
        }

        // под крафтом понимается розыгрыш карты
        void Craft(Player player, Card card)
        {
            // проверяем, есть ли у фракции достаточно источников для крафта
            if (player.SpendResource(card.Cost))
            {
                card.Effect.Handler(this, player.Controller);
            }
        }
    }
}
