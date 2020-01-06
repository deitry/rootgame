using System.Collections.Generic;

namespace RootBase
{
    // Пример оформления "карточки" фракции.
    public class Marquise : IFaction
    {
        public FactionType Type() { return FactionType.MarquiseDeKote; }

        public List<Action> GetActions(TurnPhase phase)
        {
            var actions = new List<Action> { };
            switch (phase)
            {
                // все действия в рамках одной фазы выполняются сразу если могут,
                // если не могут, то спрашивают выбор у игрока,
                // выбор произволен, если не сказано "mandatory", то действие можно пропустить

                case TurnPhase.Setup1:
                    // TODO: считывать конструкции из Json
                    // NOTE: в оригинале цитадель называется The Keep, приберегаем это слово на случай, если захотим использовать
                    actions.Add(new Action("add citadel site=choose restriction=corner mandatory"));
                    break;

                case TurnPhase.Setup2:
                    // - поставить воина на каждую поляну, кроме той, что противоположна цитадели
                    // если нету choose, то ставятся автоматически.
                    // чтобы получить место, противоположное цитадели, надо сделать что-то типа функции
                    actions.Add(new Action($"add { this.UnitName() } site=each restriction=corner,opposed(Map.Site.found(name=citadel))"));
                    // поскольку choose нет, действие обязательно, выполняется автоматически

                    actions.Add(new Action("add sawmill site=choose restriction=Map.Site.ownedby(marquise),Map.Site.haveSlot() mandatory"));
                    actions.Add(new Action($"add { this.CraftSource() } site=choose restriction=Map.Site.ownedby(marquise),Map.Site.haveSlot() mandatory"));
                    actions.Add(new Action("add recruiter site=choose restriction=Map.Site.ownedby(marquise),Map.Site.haveSlot() mandatory"));
                    break;
                case TurnPhase.Birdsong1:
                    // нужно ли wood считать отдельным объектом? по сути это счётчик
                    // actions.Add(new Action("add wood foreach=sawmill site=choose restriction=Map.Site.ownedby(marquise) mandatory"));
                    actions.Add(new Action("inc wood foreach=sawmill"));
                    break;
                case TurnPhase.Daylight1:
                    // TODO: как ограничить тремя действиями
                    actions.Add(new Action("fight site=choose restriction=haveWarriors(marquise)"));
                    actions.Add(new Action("move site=choose restriction=haveWarriors(marquise) distance=2"));
                    actions.Add(new Action($"add {this.UnitName()} foreach=recruiter"));

                    actions.Add(new Action("add building type=choose site=choose restriction"));
                    break;

                default:
                    break;
            };

            return actions;
        }

        // number - какое по счёту строение такого типа мы строим
        // TODO: переписать в Rules?
        int Cost(int number)
        {
            switch (number)
            {
                case 0:
                    return 0;
                case 1:
                    return 1;
                case 2: case 3:
                    return 2;
                case 4: case 5:
                    return 3;
            }

            throw new System.Exception($"Unexpected building count {number}");
        }

        public string UnitName() { return "cat"; }
        public string CraftSource() { return "workshop"; }

        public Rules GetRules()
        {
            return new Rules {
                $"limit { this.UnitName() } 25",
                $"limit { this.CraftSource() } 6",
                "limit recruiter 6",
                "limit sawmill 6",
                "limit citadel 1",

                // триггеры:
                // - пример присуждения победного очка
                // добавить 1 победное очко, если количество лесопилок становится равным 2
                "on count(sawmill)=2 inc score 1",
                // глобальное умение: если воин маркизы убран с поля, можно потратить карту, совпадающую с
                // мастью места, где была битва, чтобы вернуть воина в цитадель
                // FIXME: с помощью текущего синтаксиса "скриптового языка" сложно описать "место, откуда был убран воин"
                // Нужно понятие события
                $"on rm({ this.UnitName() }) add { this.UnitName() } cost=card(1)"
            };
        }
    }
}
