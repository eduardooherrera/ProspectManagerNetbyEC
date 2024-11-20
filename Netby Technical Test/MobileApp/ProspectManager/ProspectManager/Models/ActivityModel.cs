using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspectManager.Models
{
    public class ActivityModel
    {
        public int Id { get; set; }
        public Guid ProspectId { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
        public required string Date { get; set; }
        public int Rating { get; set; }
    }

    public class ActivityModelInput
    {
        public int Id { get; set; }
        public Guid ProspectId { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
    }
}
