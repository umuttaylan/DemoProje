using DemoProje.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Abstract
{
    public interface IVehicleService
    {
        ResponseViewModel Get(int id);
        ResponseViewModel Add(VehicleDto vehicleDto);
        ResponseViewModel Update(VehicleDto vehicleDto);
        ResponseViewModel Delete(int id); 
    }
}
