using DemoProje.Core.Entities;
using System;
using System.Collections.Generic;

namespace DemoProje.Entities.Models
{
    public partial class PictureGroup : IEntity
    {
        public PictureGroup()
        {
            Maintenance = new HashSet<Maintenance>();
        }

        public int Id { get; set; }
        public string PictureImage { get; set; }
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
