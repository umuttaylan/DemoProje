using DemoProje.Business.Abstract;
using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class MaintenanceHistoryManager : IMaintenanceHistoryService
    {
        private readonly IMaintenanceHistoryDal _maintenanceHistoryDal;
        private readonly IMaintenanceDal _maintenanceDal;
        private readonly IActionTypeDal _actionTypeDal;
        private readonly IUserDal _userDal;
        public MaintenanceHistoryManager(IMaintenanceHistoryDal maintenanceHistoryDal,
                                        IMaintenanceDal maintenanceDal,
                                        IActionTypeDal actionTypeDal,
                                        IUserDal userDal)
        {
            _maintenanceHistoryDal = maintenanceHistoryDal;
            _maintenanceDal = maintenanceDal;
            _actionTypeDal = actionTypeDal;
            _userDal = userDal;
        }
        public ResponseViewModel Add(MaintenanceHistoryDto maintenanceHistoryDto)
        {
            var response = new ResponseViewModel();

            var maintenanceId = IsMaintenanceHave(maintenanceHistoryDto.MaintenanceId);
            if (!maintenanceId)
            {
                response.IsSuccess = false;
                response.Message = "MaintenanceId Maintenance tablosunda bulunamadı";

                return response;
            }

            var actionTypeId = IsActionTypeHave(maintenanceHistoryDto.ActionTypeId);
            if (!actionTypeId)
            {
                response.IsSuccess = false;
                response.Message = "actionTypeId ActionType tablosunda bulunamadı";

                return response;
            }
            if (maintenanceHistoryDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)maintenanceHistoryDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (maintenanceHistoryDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)maintenanceHistoryDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "modifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var maintenanceHistory = new MaintenanceHistory()
            {
                MaintenanceId = maintenanceHistoryDto.MaintenanceId,
                ActionTypeId = maintenanceHistoryDto.ActionTypeId,
                CreateDate = DateTime.Now,
                CreatedBy = maintenanceHistoryDto.CreatedBy,
                ModifyDate = maintenanceHistoryDto.ModifyDate,
                ModifiedBy = maintenanceHistoryDto.ModifiedBy,
                Text = maintenanceHistoryDto.Text,
            };

            _maintenanceHistoryDal.Add(maintenanceHistory);

            var saving = _maintenanceHistoryDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "MaintenanceHistory ekleme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = "Id : " + maintenanceHistory.Id;

            return response;
        }

        public ResponseViewModel Delete(int id)
        {
            var response = new ResponseViewModel();

            var maintenanceHistory = _maintenanceHistoryDal.GetMaintenanceHistory(p => p.Id == id);

            if (maintenanceHistory == null)
            {
                response.IsSuccess = false;
                response.Message = "maintenanceHistory bulunamadı.";
                return response;
            }

            maintenanceHistory.IsDeleted = true;

            _maintenanceHistoryDal.Update(maintenanceHistory);
            var saving = _maintenanceHistoryDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "maintenanceHistory silme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = maintenanceHistory;

            return response;
        }

        public ResponseViewModel Get(int id)
        {
            var response = new ResponseViewModel();

            var maintenanceHistory = _maintenanceHistoryDal.GetMaintenanceHistory(p => p.Id == id);

            if (maintenanceHistory == null)
            {
                response.IsSuccess = false;
                response.Message = "maintenanceHistory bulunamadı.";
                return response;
            }

            var maintenanceHistoryDto = new MaintenanceHistoryDto()
            {
                Id = maintenanceHistory.Id,
                MaintenanceId = maintenanceHistory.MaintenanceId,
                ActionTypeId = maintenanceHistory.ActionTypeId,
                CreateDate = maintenanceHistory.CreateDate,
                CreatedBy = maintenanceHistory.CreatedBy,
                ModifyDate = maintenanceHistory.ModifyDate,
                ModifiedBy = maintenanceHistory.ModifiedBy,
                IsDeleted = maintenanceHistory.IsDeleted,
                Text = maintenanceHistory.Text
            };

            response.Data = maintenanceHistoryDto;

            return response;
        }

        public ResponseViewModel Update(MaintenanceHistoryDto maintenanceHistoryDto)
        {
            var response = new ResponseViewModel();

            var maintenanceId = IsMaintenanceHave(maintenanceHistoryDto.MaintenanceId);
            if (!maintenanceId)
            {
                response.IsSuccess = false;
                response.Message = "MaintenanceId Maintenance tablosunda bulunamadı";

                return response;
            }

            var actionTypeId = IsActionTypeHave(maintenanceHistoryDto.ActionTypeId);
            if (!actionTypeId)
            {
                response.IsSuccess = false;
                response.Message = "actionTypeId ActionType tablosunda bulunamadı";

                return response;
            }
            if (maintenanceHistoryDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)maintenanceHistoryDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (maintenanceHistoryDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)maintenanceHistoryDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "modifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var maintenanceHistory = new MaintenanceHistory()
            {
                Id = maintenanceHistoryDto.Id,
                MaintenanceId = maintenanceHistoryDto.MaintenanceId,
                ActionTypeId = maintenanceHistoryDto.ActionTypeId,
                CreateDate = maintenanceHistoryDto.CreateDate,
                CreatedBy = maintenanceHistoryDto.CreatedBy,
                ModifyDate = maintenanceHistoryDto.ModifyDate,
                ModifiedBy = maintenanceHistoryDto.ModifiedBy,
                IsDeleted = maintenanceHistoryDto.IsDeleted,
                Text = maintenanceHistoryDto.Text,
            };

            _maintenanceHistoryDal.Update(maintenanceHistory);

            var saving = _maintenanceHistoryDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "MaintenanceHistory güncelleme işlemi sırasında hata oluştu.";

                return response;
            }

            return response;
        }

        private bool IsUserHave(int userID)
        {
            var user = _userDal.GetUser(x => x.Id == userID);

            if (user == null) return false;
            else return true;
        }

        private bool IsMaintenanceHave(int maintenanceId)
        {
            var maintenance = _maintenanceDal.GetMaintenance(x => x.Id == maintenanceId);

            if (maintenance == null) return false;
            else return true;
        }

        private bool IsActionTypeHave(int actionTypeId)
        {
            var actionType = _actionTypeDal.GetActionType(x => x.Id == actionTypeId);

            if (actionType == null) return false;
            else return true;
        }
    }
}
