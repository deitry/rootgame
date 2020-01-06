using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RootBase
{
    public class Player
    {
        public IFaction Faction { get; private set; }
        public List<Card> Hand { get; private set; }

        public Player(FactionType faction) {
            this.Faction = FactionBuilder.Get(faction);
        }
    }
}
