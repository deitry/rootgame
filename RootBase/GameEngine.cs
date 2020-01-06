using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RootBase.Tests")]
namespace RootBase
{
    // TODO: перейти от строк к чему-то более умному
    public class Rules : List<string> {}

    // перечисляем все типы правил. Не факт, что будем использовать сам енам, но всё же
    enum RuleType
    {
        ObjectDefinition, // объявляем тип объекта, указываем его максимальное количество в стейте
        Trigger, // срабатывающее правило, добавляющее очки при выполнении условия
        Modificator, //
    }

    // Возможные состояния движка
    enum EngineState
    {
        PlayerTurn, // игрок выбирает действие согласно текущей фазе
        Battle, // кто-то где-то сражается (разыгрывать как действие?)
    }

    // движок должен представлять собой конечный автомат состояний
    public class GameEngine
    {
        // максимальное количество очков, которое нужно набрать для победы
        const int MaxScore = 30;
        bool Running = true;

        // полностью открыт, чтобы легко тестить
        public GameState State = null;

        // храниться должны "конкретизированные" действия, с осуществлёнными выборами по choose
        public List<Action> history;

        public bool IsInitialized { get { return this.State != null; } }

        // как движок обрабатывает действие
        internal void ProcessAction(Action action)
        {
            throw new Exception("Not implemented yet");

            if (action.Effect.Contains("choose"))
            {
                // FIXME: здесь должен быть делегат
                // Ask("site", this.State.CurrentPlayer);
            }
        }

        public void Initialize(Player[] players)
        {
            this.State = new GameState(players);
        }

        public List<Action> GetActionsForCurrentPlayer()
        {
            var actions = this.State.GetCurrentAvailableActions();
            var possible = new List<Action> { };

            // если игрок не пропускает текущую фазу
            if (true)
            {
                // проверяем действия на выполнимость
                // также возвращаем пусто, если игрок принял решение пасануть
                foreach (var action in actions)
                {
                    if (this.IsProcessable(action)) possible.Add(action);
                }
            }

            return actions;
        }

        void MainCycle()
        {
            // спрашиваем доступные действия
            // (те, что предлагаются данному игроку на данной фазе с учётом их выполнимости в данный момент)
            // - совершаем обязательные действия (которые можно реализовать без участия игрока)
            // - предлагаем сделать выбор по тем, которые нельзя разрешить автоматически
            // - совершаем все эффекты и связанные выборы
            // - - засады, например?
            // - пропустить ход тоже действие
            // если действий больше нет или они пропущены, переходим к следующему шагу
            // если шаг кончился, переходим к следующему игроку ...

            while (this.Running)
            {
                List<Action> availableActions;
                while ((availableActions = this.GetActionsForCurrentPlayer()).Count > 0)
                {
                    foreach (var action in availableActions)
                    {
                        // если действие обязательное и не требует выбора, выполняем его
                    }

                    availableActions.RemoveAll(item => item.Effect.Contains("mandatory") && !item.Effect.Contains("choose"));
                    // - удаляем из списка выполненные обязательные действия
                    // - предлагаем игроку выбор по всем остальным действиям
                }

                // переходим на следующую фазу/к следующему игроку
                this.State.NextStep();
            }
        }

        // является ли переданное действие выполнимым в данный момент
        bool IsProcessable(Action action)
        {
            // TODO: implement
            return true;
        }

        // проверяет игровое состояние на соответствие правилам.
        // Если правила нарушены, откатить к предыдущему состоянию
        void CheckRules()
        {
            if (false) throw new Exception("Rules violation detected");
        }
    }
}
