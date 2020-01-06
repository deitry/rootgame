using System.Collections.Generic;

namespace RootBase
{
    // определяем цену по количеству объектов для крафта заданного типа
    using TotalCost = Dictionary<SiteSuit, int>;

    enum CardSuit
    {
        Mouse,
        Rabbit,
        Fox,
        Bird,
    }

    // тип карты - вряд ли будем использовать в таком виде,
    // но чтобы не забыть, перечисляем все возможные варианты
    enum CardType
    {
        Continuous, // создаёт продолжительный эффект, например "раз в ход можно смотреть руку оппонента"
        Instant, // единократный эффект, например итем + ПО
        Supremacy, // карты превосходства - аналогично Continuous?
        Ambush, // засада!
    }


    public class Card
    {
        CardSuit Suit;
        CardType Type;
        TotalCost Cost;
        Action Effect;
    }
}
