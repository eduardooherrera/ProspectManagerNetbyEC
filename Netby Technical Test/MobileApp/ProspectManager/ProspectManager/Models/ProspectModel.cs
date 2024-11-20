using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspectManager.Models
{
    public class ProspectModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string CellPhoneNumber { get; set; }
        public required string Email { get; set; }
        public List<ActivityModel>? Activities { get; set; }
    }
}
