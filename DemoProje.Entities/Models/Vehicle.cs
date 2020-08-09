using DemoProje.Core.Entities;
using System;
using System.Collections.Generic;

namespace DemoProje.Entities.Models
{
    public partial class Vehicle : IEntity
    {
        public Vehicle()
        {
            Maintenance = new HashSet<Maintenance>();
        }

        public int Id { get; set; }
        public string PlateNo { get; set; }
        public int VehicleTypeId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<Maintenance> Maintenance { get; set; }
    }
}
