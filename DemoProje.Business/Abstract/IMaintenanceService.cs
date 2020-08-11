using DemoProje.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Abstract
{
    public interface IMaintenanceService
    {
        ResponseViewModel Get(int id);
        ResponseViewModel GetAllBreakdownNotifications();
        ResponseViewModel GetBreakdownNotificationDetail(int id);
        ResponseViewModel Add(MaintenanceDto maintenanceDto);
        ResponseViewModel Update(MaintenanceDto maintenanceDto);
        ResponseViewModel Delete(int id);
    }
}
