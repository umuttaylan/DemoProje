using System;
using System.Collections.Generic;

namespace DemoProje.Entities.Models
{
    public partial class MaintenanceHistory
    {
        public int Id { get; set; }
        public int MaintenanceId { get; set; }
        public int ActionTypeId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string Text { get; set; }

        public virtual ActionType ActionType { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual Maintenance Maintenance { get; set; }
        public virtual User ModifiedByNavigation { get; set; }
    }
}
