using System;

namespace ToDoListTeldat.Models
{
    public class Item
    {
        public Item() { }
        public Item(string title, DateTime finishTime)
        {
            Title = title;
            FinishTime = finishTime;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime FinishTime { get; set; }

    }
}
