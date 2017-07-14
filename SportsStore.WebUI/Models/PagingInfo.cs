using  System;
namespace SportsStore.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; } //Всего элементо
        public int ItemsPerPage { get; set; } //Элементов на странице
        public int CurrentPage { get; set; } //Текущая страница

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }   //Всего страниц (берем кол элементов и делим их на элементы на странице приводим к целочисленному)
    }
}