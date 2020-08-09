using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Entities.Dto
{
    public class MaintenanceHistoryDto
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
    }
}
