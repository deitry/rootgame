using System.Collections.Generic;

namespace RootBase
{
    // public class Properties : Dictionary<string, object> { }
    using Properties = Dictionary<string, string>;

    // статические эффекты тоже объекты?
    public class GameObject
    {
        public GameObject(string name, string description, Properties attributes = null)
        {
            this.Name = name;
            this.Description = description;
            this.Attributes = (attributes != null) ? attributes : new Properties { };
        }

        readonly public string Name;
        readonly public string Description;

        public Properties Attributes { get; private set; }
    }

    class ObjectEventArgs
    {
        GameObject Object;
    }

    class PlaceableObjectEventArgs : ObjectEventArgs
    {
        Site Where;
    }

    class ResourceEventArgs : ObjectEventArgs
    {
        int Amount;
    }

    delegate void ObjectEventHandler(ObjectEventArgs args);

    // игровой объект, который может иметь хозяина
    // поскольку двух одинаковых фракций быть не может (два разных бродяги считаются разными фракциями),
    // в качестве владельца записываем просто тип фракции
    // class FactionObject : GameObject
    // {
    //     public FactionType Owner { get; private set; }

    //     public FactionObject(string name, string description, Properties attributes, FactionType owner)
    //         : base(name, description, attributes)
    //     {
    //         this.Owner = owner;
    //     }
    // }
}
