using System.Collections.Generic;

namespace RootBase
{
    /**
    Единый класс для управления картами в общих зонах
    */
    internal class Cards
    {
        internal List<Card> Deck;
        internal List<Card> Discard;

        // создаём список со всеми картами
        // TODO: поскольку все карты по сути делятся на несколько типов,
        // надо создать параметризуемый билдер
        static public List<Card> Initialize()
        {
            // некоторые карты могут быть в нескольких экзмеплярах
            return new List<Card>{
                new Card(
                    "Походное снаряжение",
                    CardSuit.Fox,
                    CardType.Instant,
                    new TotalCost(0, 1, 0),
                    new Action("Скрафтить сапог", (GameActionInterface gameAi, Player player) =>
                    {
                        gameAi.ChangeScore(player, 1);
                        player.Faction.AddObject(
                            // TODO: InventoryObject?
                            new GameObject("Сапоги", "Сапоги")
                            // сам по себе объект ничего не делает, всё решает как его используют
                        );
                    })),
                new Card(
                    "Командная нора",
                    CardSuit.Rabbit,
                    CardType.Continuous,
                    new TotalCost(0, 2, 0),
                    // TODO: завести отдельный билдер для типовых карт, чтобы не копировать одно и то же
                    new Action("В начале дня можете устроить 1 сражение",
                        (GameActionInterface gameAi, Player player) =>
                    {
                        player.Faction.Effects.Add(
                            new CardEffect{
                                Description="В начале дня можете устроить 1 сражение",
                                From=TurnStep.Daylight0,
                                UpTo=TurnStep.Daylight0,
                                Effect=new PassableAction(
                                    "В начале дня можете устроить 1 сражение",
                                    // NOTE: добавили Again, потому что такие имена уже встречаются в данной области видимости
                                    // FIXME: так или иначе избавиться от "Again"
                                    (GameActionInterface gameAiAgain, Player playerAgain) => {
                                        var where = playerAgain.Controller.PickSite(gameAiAgain.Interface.InhabitedBy(playerAgain.Faction));
                                        var enemy = playerAgain.Controller.PickPlayer(gameAiAgain.Interface.Enemies(where, playerAgain));
                                        gameAiAgain.Fight(where, playerAgain, enemy);
                                    }
                                )
                            }
                        )
                    })),
            };
        }
    }

    /**
    Эффект постоянной карты
    */
    internal class CardEffect
    {
        internal string Description;
        internal TurnStep From; // с какого момента можно применить
        internal TurnStep UpTo; // по какой
        internal Action Effect;
    }
}
