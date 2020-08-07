using Movie.Core.Entities;
using System;
using System.Collections.Generic;

namespace DemoProje.Entities.Models
{
    public partial class Status : IEntity
    {
        public Status()
        {
            Maintenance = new HashSet<Maintenance>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }
        public virtual ICollection<Maintenance> Maintenance { get; set; }
    }
}
