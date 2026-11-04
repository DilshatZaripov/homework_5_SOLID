using homework_5_SOLID.Domain.Models;

namespace homework_5_SOLID.Domain.Contracts.UI
{
    public interface IMenuUI
    {
        /// <summary>
        /// Показывает меню и возращает номер выбранного пункта (1-based)<br/>
        /// Валидность ввода регулируется самим UI, повторяет запрос до получения валидного ввода.<br/>
        /// Гарантированно не кидает исключения.
        /// </summary>
        /// <param name="model">Модель данных меню</param>
        /// <returns>Номер пункта меню (1-based)</returns>
        int ShowMenu(MenuModel model);
        void ShowMessage(string message);
    }
}
