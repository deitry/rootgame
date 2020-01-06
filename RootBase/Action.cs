using System.Collections.Generic;

namespace RootBase
{
    public class Action
    {
        public Action(string effect) { this.Effect = effect; }

        // FIXME: пока я не понимаю, как это сделать правильно, действие представляет из себя
        // слегка формализированную строку ~ псевдо скритовый язык.
        // Возможно правила так и стоит описывать, чтобы иметь возможность их легко подменять,
        // но типизация - это тоже очень круто

        // синтаксис
        // add - добавить объект
        // mandatory - обязательно к выполнению, нельзя пропустить
        // name= - может быть опущено (?) - имя объекта
        // site= - место, куда надо поставить
        // объекты вне карты имеет смысл рассчитывать исходя из того, что стоит на карте,
        // ибо максимальное количество ограничено
        // site=each - везде, если разрешено restriction
        // choose - возможность выбора
        // restriction= - ограничение на выбор
        // resource= - сколько нужно ресурса для осуществления действия
        // function() - рассчитывает значение
        // opposed(name=citadel) - ищет объект citadel, берёт его "координаты", находит противоположную клетку
        public readonly string Effect;

        // некоторые действия имеют постэффект, который является тоже похожим действием
        public readonly string PostEffect;

        public bool MustChoose { get => this.Effect.Contains("choose"); }
    }
}
