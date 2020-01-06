using System.Collections.Generic;

namespace RootBase
{
    public class Alliance : IFaction
    {
        public FactionType Type() { return FactionType.WoodlandAlliance; }

        public List<Action> GetActions(TurnPhase phase) {
            var actions = new List<Action>{ };
            switch (phase)
            {
                case TurnPhase.Birdsong1:
                default:
                    break;

            }
            return actions;
        }
    }
}
