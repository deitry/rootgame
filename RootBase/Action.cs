using System.Collections.Generic;

namespace RootBase
{
    public class Action
    {
        public readonly string Description;

        internal delegate void ActionHandler(GameActionInterface gameAi, Player player);
        public virtual bool Passable() { return false; }

        internal Action(string description, ActionHandler handler)
        {
            this.Description = description;
            this.Handler = handler;
        }

        internal ActionHandler Handler;
    }

    // чтобы прозрачнее обозначать вместо лишнего параметра в конструкторе
    internal class PassableAction : Action
    {
        internal PassableAction(string description, ActionHandler handler)
            : base(description, handler) { }

        public override bool Passable() { return true; }
    }
}
