using DemoProje.Core.Entities;
using System;
using System.Collections.Generic;

namespace DemoProje.Entities.Models
{
    public partial class User : IEntity
    {
        public User()
        {
            ActionTypeCreatedByNavigation = new HashSet<ActionType>();
            ActionTypeModifiedByNavigation = new HashSet<ActionType>();
            InverseCreatedByNavigation = new HashSet<User>();
            InverseModifiedByNavigation = new HashSet<User>();
            MaintenanceCreatedByNavigation = new HashSet<Maintenance>();
            MaintenanceHistoryCreatedByNavigation = new HashSet<MaintenanceHistory>();
            MaintenanceHistoryModifiedByNavigation = new HashSet<MaintenanceHistory>();
            MaintenanceModifiedByNavigation = new HashSet<Maintenance>();
            MaintenanceResponsibleUser = new HashSet<Maintenance>();
            MaintenanceUser = new HashSet<Maintenance>();
            PictureGroupCreatedByNavigation = new HashSet<PictureGroup>();
            PictureGroupModifiedByNavigation = new HashSet<PictureGroup>();
            StatusCreatedByNavigation = new HashSet<Status>();
            StatusModifiedByNavigation = new HashSet<Status>();
            VehicleCreatedByNavigation = new HashSet<Vehicle>();
            VehicleModifiedByNavigation = new HashSet<Vehicle>();
            VehicleTypeCreatedByNavigation = new HashSet<VehicleType>();
            VehicleTypeModifiedByNavigation = new HashSet<VehicleType>();
            VehicleUser = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string Profilepicture { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }
        public virtual ICollection<ActionType> ActionTypeCreatedByNavigation { get; set; }
        public virtual ICollection<ActionType> ActionTypeModifiedByNavigation { get; set; }
        public virtual ICollection<User> InverseCreatedByNavigation { get; set; }
        public virtual ICollection<User> InverseModifiedByNavigation { get; set; }
        public virtual ICollection<Maintenance> MaintenanceCreatedByNavigation { get; set; }
        public virtual ICollection<MaintenanceHistory> MaintenanceHistoryCreatedByNavigation { get; set; }
        public virtual ICollection<MaintenanceHistory> MaintenanceHistoryModifiedByNavigation { get; set; }
        public virtual ICollection<Maintenance> MaintenanceModifiedByNavigation { get; set; }
        public virtual ICollection<Maintenance> MaintenanceResponsibleUser { get; set; }
        public virtual ICollection<Maintenance> MaintenanceUser { get; set; }
        public virtual ICollection<PictureGroup> PictureGroupCreatedByNavigation { get; set; }
        public virtual ICollection<PictureGroup> PictureGroupModifiedByNavigation { get; set; }
        public virtual ICollection<Status> StatusCreatedByNavigation { get; set; }
        public virtual ICollection<Status> StatusModifiedByNavigation { get; set; }
        public virtual ICollection<Vehicle> VehicleCreatedByNavigation { get; set; }
        public virtual ICollection<Vehicle> VehicleModifiedByNavigation { get; set; }
        public virtual ICollection<VehicleType> VehicleTypeCreatedByNavigation { get; set; }
        public virtual ICollection<VehicleType> VehicleTypeModifiedByNavigation { get; set; }
        public virtual ICollection<Vehicle> VehicleUser { get; set; }
    }
}
