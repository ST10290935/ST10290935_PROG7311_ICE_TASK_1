﻿namespace PROG_3C_Task_Manager.Models
{
    public class task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public DateTime Duration { get; set; }

    }
}
