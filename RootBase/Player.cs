using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RootBase
{
    // Объединяет в себе что (фракция) и кем (контроллер) управляется
    // а также содержит в себе общие для всех игроков функции
    // Возможно общее стоит перенести куда-нибудь ещё, например
    // в IFaction и сделать его абстрактным
    public class Player
    {
        internal Player(IController controller, IFaction faction)
        { this.Controller = controller; this.Faction = faction; }

        internal IController Controller { get; private set; }
        public readonly IFaction Faction;
    }

    /*
    Тот, кто контролирует фракцию.
    Предполагается, что подклассы этого будут соответственно Human, Ai, Mock
    */
    internal interface IController
    {
        // функции выбора - то, что должно быть диалогами.
        // Пока оставляем здесь, потом будем подменять делегатов.
        // - игроки должны выбирать через гуй
        // - ИИ будет выбирать
        // - мок-классы будут выбирать случайным образом
        // игрок выбирает место
        Site PickSite(List<Site> sites);
        Player PickPlayer(List<Player> players);
        GameObject PickObject(List<GameObject> objects);
    }

    internal class MockController : IController
    {
        public GameObject PickObject(List<GameObject> objects)
        {
            throw new NotImplementedException();
        }

        public Site PickSite(List<Site> sites)
        {
            throw new NotImplementedException();
        }

        public Player PickPlayer(List<Player> players)
        {
            throw new NotImplementedException();
        }
    }
}
