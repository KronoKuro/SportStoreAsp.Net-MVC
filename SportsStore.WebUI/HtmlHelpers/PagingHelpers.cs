using System;
using System.Drawing.Printing;
using System.Text;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++) //цикл который считает и пока она меньше чем всего элементов
            {
                TagBuilder tag = new TagBuilder("a"); //Создание тегов а
                tag.MergeAttribute("href", pageUrl(i)); /*дает етим тегам путь href и i это номер элемента короче типо 
                <a href="1">
                 */
                tag.InnerHtml = i.ToString(); //значение ссылки = номер страницы и привети в String
                if (i == pagingInfo.CurrentPage) //если i == текущей странице
                {
                    tag.AddCssClass("selected"); 
                    tag.AddCssClass("btn-primary"); /*если i равно текцщей странице то к ссылке 
                    добавиться класс выделения*/
                }
                tag.AddCssClass("btn btn-default");//добавим ко всем ссылкам эти классы
                result.Append(tag.ToString());//берем  result по сути эту пустая строка и склеиваем ее с содержимым tag и приводим к string
            }
            return MvcHtmlString.Create(result.ToString()); /*вернем мы вот что у класса Mvc мы вызываем
            фцнкцию создать  и передаем туда result к строке приводим опять же тк result типа String Builder*/
        }
    }
}