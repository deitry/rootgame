using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RootBase
{
    // объединяет в себе что и кем управляется
    // а также содержит в себе общие для всех игроков функции
    // Возможно общее стоит перенести куда-нибудь ещё, например
    // в IFaction и сделать его абстрактным
    public class Player
    {
        internal Player(IController controller, IFaction faction)
        { this.Controller = controller; this.Faction = faction; }

        internal IController Controller { get; private set; }
        public readonly IFaction Faction;

        // зоны и ресурсы, локальные для игрока
        // Вынести отдельно, чтобы не мешались?

        public List<Card> Hand { get; private set; }
        public List<Card> Discard(uint amount)
        {
            var discarded = new List<Card> { };

            for (uint i = 0; i < amount; i++)
            {
                if (Hand.Count == 0) break;

                // случайный vs pick
                discarded.Add(Hand[0]);
                Hand.RemoveAt(0);
            }
            return discarded;
        }

        public int ResourceAmount(ResourceType type)
        {
            // ресурсы мест () зависят от фракции,
            // а остальных может вообще не быть!
            return 0;
        }

        // в качестве типа параметра сознательно оставлен знаковый инт:
        // можно "потратить" -1, тем самым добавив 1 ресурса до конца хода
        internal bool SpendResource(ResourceType type, int amount)
        {
            return false;
        }

        // Dictionary<SiteSuit, int>
        internal bool SpendResource(TotalCost cost)
        {
            // оценить, есть ли столько ресурсов
            return false;
        }
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
    }
}
