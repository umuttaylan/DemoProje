using DemoProje.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Abstract
{
    public interface IMaintenanceHistoryService
    {
        ResponseViewModel Get(int id);
        ResponseViewModel Add(MaintenanceHistoryDto maintenanceHistoryDto);
        ResponseViewModel Update(MaintenanceHistoryDto maintenanceHistoryDto );
        ResponseViewModel Delete(int id);
    }
}
