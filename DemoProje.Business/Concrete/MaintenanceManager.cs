using DemoProje.Business.Abstract;
using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class MaintenanceManager : IMaintenanceService
    {
        public MaintenanceManager()
        {
           
        }

        public ResponseViewModel Add(MaintenanceDto maintenanceDto)
        {
            var response = new ResponseViewModel();
        }

        public ResponseViewModel Delete(int id)
        {
            var actionType = _actionTypeDal.Get(p => p.Id == id);

            if (actionType == null)
            {
                response.IsSuccess = false;
                response.Message = "ActionType bulunamadı.";
                return response;
            }

            _actionTypeDal.Delete(actionType);
            var saving = _actionTypeDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "ActionType silme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = actionType;

            return response;
        }

        public ResponseViewModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel Update(MaintenanceDto maintenanceDto)
        {
            throw new NotImplementedException();
        }
    }
}
