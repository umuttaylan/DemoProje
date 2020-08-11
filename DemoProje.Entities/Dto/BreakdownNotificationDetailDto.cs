using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Entities.Dto
{
    public class BreakdownNotificationDetailDto
    {
        public string PlateNo { get; set; }
        public string VehicleTypeName { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPhone { get; set; }
        public string PictureImage { get; set; }
        public DateTime? ExpectedTimeToFix { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public string HistoryText { get; set; }
        public string ActionTypeName { get; set; }
    }
}
