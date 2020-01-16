using System.Collections.Generic;

namespace RootBase
{
    // определяем цену по количеству объектов для крафта заданного типа
    // using TotalCost = Dictionary<SiteSuit, int>;
    internal class TotalCost
    {
        // в стоимости карт используются только масти мест
        internal readonly Dictionary<SiteSuit, int> Suits;

        internal TotalCost(Dictionary<SiteSuit, int> suits) { this.Suits = suits; }
    }

    // масть самой карты
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
        internal CardSuit Suit;
        internal CardType Type;
        internal TotalCost Cost;
        internal Action Effect;
    }
}
