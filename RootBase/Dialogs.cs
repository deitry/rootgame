using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RootBase
{
    // перечисляем все объекты, среди которых игроку придётся делать выбор
    // сам енам вряд ли будем использовать
    enum DialogTypes
    {
        Actions, // выбор действия
        Sites, // выбор места
        Buildings, // выбор из доступных для строительства зданий
        Cards, // выбор из карт - не обязательно тех, что на руке
    }

    // предварительный интерфейс для диалогов
    interface Dialog<Type>
    {
        // NB: можно спрашивать асинхронно
        Task<Type> Ask(List<Type> options);
    }

    // диалогозаменитель - возвращает случайное значение из списка
    class MockDialog<Type> : Dialog<Type>
    {
        public Task<Type> Ask(List<Type> options)
        {
            var a = new Random();
            return new Task<Type>(() => options[a.Next(options.Count)]);
        }
    }
}
