using System;

namespace TaskManamentTool.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; } // Nullable
        public int StatusId { get; set; } // Foreign Key attribute
        public Status Status { get; set; } // Navigation property, set up relationship between Task and Status. It is NOT created in DB.

        public Task(string name, int statusId, DateTime dueDate)
        {
            Name = name;
            StatusId = statusId;
            DueDate = dueDate;
        }
    }
}