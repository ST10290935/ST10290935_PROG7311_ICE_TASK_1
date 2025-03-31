using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PROG_3C_Task_Manager.Models;

namespace PROG_3C_Task_Manager.Data
{
    public class PROG_3C_Task_ManagerContext : DbContext
    {
        public PROG_3C_Task_ManagerContext (DbContextOptions<PROG_3C_Task_ManagerContext> options)
            : base(options)
        {
        }

        public DbSet<PROG_3C_Task_Manager.Models.task> task { get; set; } = default!;
        public DbSet<PROG_3C_Task_Manager.Models.Report> Report { get; set; } = default!;
    }
}
