using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Entities.Dto
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
