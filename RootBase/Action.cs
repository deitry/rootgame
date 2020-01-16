using System.Collections.Generic;

namespace RootBase
{
    public class Action
    {
        public readonly string Description;

        internal delegate void ActionHandler(GameActionInterface gameAi, IController controller);
        public readonly bool CanBePassed = true;

        internal Action(string description, ActionHandler handler)
        {
            this.Description = description;
            this.Handler = handler;
        }

        internal Action(string description, ActionHandler handler, bool canBePassed)
        {
            this.Description = description;
            this.Handler = handler;
            this.CanBePassed = canBePassed;
        }

        internal ActionHandler Handler;
    }

    // чтобы прозрачнее обозначать вместо лишнего параметра в конструкторе
    internal class MandatoryAction : Action
    {
        internal MandatoryAction(string description, ActionHandler handler)
            : base(description, handler, false) { }
    }
}
