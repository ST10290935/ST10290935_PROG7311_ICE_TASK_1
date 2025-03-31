using System.ComponentModel.DataAnnotations;

namespace PROG_3C_Task_Manager.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public string ReportName { get; set; }

        [Required]
        public DateTime ReportDate { get; set; }

        [Required]
        public string GeneratedBy { get; set; }

        [Required]
        public string ReportData
        { get; set; }
    }
}
