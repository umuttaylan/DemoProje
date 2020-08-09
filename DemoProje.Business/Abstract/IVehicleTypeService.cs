using DemoProje.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Abstract
{
    public interface IVehicleTypeService
    {
        ResponseViewModel Get(int id);
        ResponseViewModel Add(VehicleTypeDto vehicleTypeDto);
        ResponseViewModel Update(VehicleTypeDto vehicleTypeDto);
        ResponseViewModel Delete(int id);
    }
}
