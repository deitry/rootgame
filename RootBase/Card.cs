using System.Collections.Generic;

namespace RootBase
{
    // определяем цену по количеству объектов для крафта заданного типа
    // using TotalCost = Dictionary<SiteSuit, int>;
    internal class TotalCost
    {
        // в стоимости карт используются только масти мест
        internal readonly Dictionary<SiteSuit, int> Suits;
        internal TotalCost(int foxAmount, int rabbitAmount, int mouseAmount)
        {
            this.Suits = new Dictionary<SiteSuit, int>(){
                {SiteSuit.Fox, foxAmount},
                {SiteSuit.Rabbit, rabbitAmount},
                {SiteSuit.Mouse, mouseAmount},
            };
        }

        // internal TotalCost(Dictionary<SiteSuit, int> suits) { this.Suits = suits; }
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
        internal readonly string Name;
        internal readonly CardSuit Suit;
        internal readonly CardType Type;
        internal readonly TotalCost Cost;
        internal Action Effect { get; private set; }

        internal Card(string name, CardSuit suit, CardType type, TotalCost cost, Action effect)
        {
            this.Name = name;
            this.Suit = suit;
            this.Type = type;
            this.Cost = cost;
            this.Effect = effect;
        }
    }
}
