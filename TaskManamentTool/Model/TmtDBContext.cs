using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManamentTool.Model
{
    class TmtDBContext : DbContext
    {
        // register class Status to DBContext
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
