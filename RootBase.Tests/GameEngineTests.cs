using System;
using Xunit;
using System.Collections.Generic;

namespace RootBase.Tests
{
    public class GameEngineTests
    {
        [Fact]
        public void BasicTest()
        {
            Assert.True(true);
        }

        [Fact]
        public void EachFactionsAreDifferent()
        {
            GameState state = new GameState(GameInterface.DefaultPlayerSet());

            var factions = new HashSet<IFaction>();
            foreach (var player in state.Players)
            {
                Assert.False(factions.Contains(player.Faction));
                factions.Add(player.Faction);
            }
        }

        [Fact]
        public void TestInitialization()
        {
            var engine = new GameEngine();
            engine.Initialize(GameInterface.DefaultPlayerSet());
            Assert.True(engine.IsInitialized);
        }

        [Fact]
        public void TestActions()
        {
            var engine = new GameEngine();
            engine.Initialize(GameInterface.DefaultPlayerSet());

            var actions = new List<Action> {};

            // перебор по всем фазам
            foreach (var phase in (TurnPhase[]) Enum.GetValues(typeof(TurnPhase))) {
                // собираем все доступные действия для игроков в рамках данной фазы
                foreach (var player in engine.State.Players)
                {
                    actions.AddRange(player.Faction.GetActions(phase));
                }

                // пробуем, можем ли мы выполнить действие
                // FIXME: если в рамках действия игрок должен совершить выбор,
                // нужно выбрать случайный вариант
                foreach (var action in actions)
                {
                    var ex = Record.Exception(() => engine.ProcessAction(action));
                    Assert.Null(ex);
                }
            }
        }
    }
}
