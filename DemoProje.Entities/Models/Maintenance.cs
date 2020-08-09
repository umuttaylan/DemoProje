using DemoProje.Core.Entities;
using System;
using System.Collections.Generic;

namespace DemoProje.Entities.Models
{
    public partial class Maintenance : IEntity
    {
        public Maintenance()
        {
            MaintenanceHistory = new HashSet<MaintenanceHistory>();
        }

        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public int? PictureGroupId { get; set; }
        public DateTime? ExpectedTimeToFix { get; set; }
        public int? ResponsibleUserId { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }
        public virtual PictureGroup PictureGroup { get; set; }
        public virtual User ResponsibleUser { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<MaintenanceHistory> MaintenanceHistory { get; set; }
    }
}
