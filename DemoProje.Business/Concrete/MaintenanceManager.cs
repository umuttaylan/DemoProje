using DemoProje.Business.Abstract;
using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class MaintenanceManager : IMaintenanceService
    {
        private readonly IMaintenanceDal _maintenanceDal;
        private readonly IMaintenanceHistoryDal _maintenanceHistoryDal;
        private readonly IActionTypeDal _actionTypeDal;
        private readonly IUserDal _userDal;
        private readonly IVehicleDal _vehicleDal;
        private readonly IVehicleTypeDal _vehicleTypeDal;
        private readonly IPictureGroupDal _pictureGroupDal;
        private readonly IStatusDal _statusDal;
        public MaintenanceManager(IMaintenanceDal maintenanceDal,
                                  IMaintenanceHistoryDal maintenanceHistoryDal,
                                  IActionTypeDal actionTypeDal,
                                  IUserDal userDal,
                                  IVehicleDal vehicleDal,
                                  IVehicleTypeDal vehicleTypeDal,
                                  IPictureGroupDal pictureGroupDal,
                                  IStatusDal statusDal)
        {
            _maintenanceDal = maintenanceDal;
            _maintenanceHistoryDal = maintenanceHistoryDal;
            _actionTypeDal = actionTypeDal;
            _userDal = userDal;
            _vehicleDal = vehicleDal;
            _vehicleTypeDal = vehicleTypeDal;
            _pictureGroupDal = pictureGroupDal;
            _statusDal = statusDal;
        }

        public ResponseViewModel Add(MaintenanceDto maintenanceDto)
        {
            var response = new ResponseViewModel();

            var vehicleId = IsVehicleHave(maintenanceDto.VehicleId);
            if (!vehicleId)
            {
                response.IsSuccess = false;
                response.Message = "VehicleID Vehicle tablosunda bulunamadı ";

                return response;
            }

            var userId = IsUserHave(maintenanceDto.UserId);
            if (!userId)
            {
                response.IsSuccess = false;
                response.Message = "UserID User tablosunda bulunamadı ";

                return response;
            }

            if (maintenanceDto.PictureGroupId != null)
            {
                var pictureGroupId = IsPictureGroupHave((int)maintenanceDto.PictureGroupId);

                if (!pictureGroupId)
                {
                    response.IsSuccess = false;
                    response.Message = "pictureGroupId pictureGroup tablosunda bulunamadı ";

                    return response;
                }
            }

            if (maintenanceDto.ResponsibleUserId != null)
            {
                var responsibleUserId = IsUserHave((int)maintenanceDto.ResponsibleUserId);
                if (!responsibleUserId)
                {
                    response.IsSuccess = false;
                    response.Message = "responsibleUserId User tablosunda bulunamadı ";

                    return response;
                }
            }

            var statusId = IsStatusHave(maintenanceDto.StatusId);
            if (!statusId)
            {
                response.IsSuccess = false;
                response.Message = "statusId Status tablosunda bulunamadı ";

                return response;
            }

            if (maintenanceDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)maintenanceDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı ";

                    return response;
                }
            }


            var maintenance = new Maintenance()
            {
                VehicleId = maintenanceDto.VehicleId,
                UserId = maintenanceDto.UserId,
                Description = maintenanceDto.Description,
                PictureGroupId = maintenanceDto.PictureGroupId,
                ExpectedTimeToFix = maintenanceDto.ExpectedTimeToFix,
                ResponsibleUserId = maintenanceDto.ResponsibleUserId,
                LocationLongitude = maintenanceDto.LocationLongitude,
                LocationLatitude = maintenanceDto.LocationLatitude,
                StatusId = maintenanceDto.StatusId,
                CreateDate = DateTime.Now,
                CreatedBy = maintenanceDto.CreatedBy,
            };

            _maintenanceDal.Add(maintenance);
            var saving = _maintenanceDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "Maintenance kaydedilirken bir hata oluştu";
                response.Data = maintenance;
            }

            response.Data = "Id : " + maintenance.Id;

            return response;
        }

        public ResponseViewModel Delete(int id)
        {
            var response = new ResponseViewModel();

            var maintenance = _maintenanceDal.GetMaintenance(p => p.Id == id);

            if (maintenance == null)
            {
                response.IsSuccess = false;
                response.Message = "Maintenance bulunamadı.";
                return response;
            }

            maintenance.IsDeleted = true;

            _maintenanceDal.Update(maintenance);
            var saving = _maintenanceDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "Maintenance silme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = maintenance;

            return response;
        }

        public ResponseViewModel GetAllBreakdownNotifications()
        {
            var response = new ResponseViewModel();
            List<BreakdownNotificationDto> breakdownNotificationDtos = new List<BreakdownNotificationDto>();
            var maintenances = _maintenanceDal.GetMaintenanceList(p=>p.IsDeleted == false);

            foreach (var maintenance in maintenances)
            {
                var breakdownNotificationDto = new BreakdownNotificationDto();
                var user = GetUser(maintenance.UserId);

                breakdownNotificationDto.PlateNo = GetPlateNo(maintenance.VehicleId);
                breakdownNotificationDto.VehicleTypeName = GetVehicleTypeName(maintenance.VehicleId);
                breakdownNotificationDto.Description = maintenance.Description;
                breakdownNotificationDto.UserName = user.FirstName;
                breakdownNotificationDto.UserSurname = user.LastName;
                breakdownNotificationDto.UserPhone = user.PhoneNumber;
                breakdownNotificationDto.PictureImage = GetPictureImage(maintenance.PictureGroupId);

                breakdownNotificationDtos.Add(breakdownNotificationDto);
            }

            response.Data = breakdownNotificationDtos;

            return response;
        }

        public ResponseViewModel GetBreakdownNotificationDetail(int id)
        {
            var response = new ResponseViewModel();
            var breakdownNotificationDetail = new BreakdownNotificationDetailDto();
            var maintenance = _maintenanceDal.GetMaintenance(p => p.Id == id);

            var user = GetUser(maintenance.UserId);

            breakdownNotificationDetail.PlateNo = GetPlateNo(maintenance.VehicleId);
            breakdownNotificationDetail.VehicleTypeName = GetVehicleTypeName(maintenance.VehicleId);
            breakdownNotificationDetail.Description = maintenance.Description;
            breakdownNotificationDetail.ExpectedTimeToFix = maintenance.ExpectedTimeToFix;
            breakdownNotificationDetail.LocationLatitude = maintenance.LocationLatitude;
            breakdownNotificationDetail.LocationLongitude = maintenance.LocationLongitude;
            breakdownNotificationDetail.UserName = user.FirstName;
            breakdownNotificationDetail.UserSurname = user.LastName;
            breakdownNotificationDetail.UserPhone = user.PhoneNumber;
            breakdownNotificationDetail.ActionTypeName = GetActionTypeName(id);
            breakdownNotificationDetail.HistoryText = GetHistoryText(id);
            breakdownNotificationDetail.PictureImage = GetPictureImage(maintenance.PictureGroupId);


            response.Data = breakdownNotificationDetail;

            return response;
        }

        public ResponseViewModel Get(int id)
        {
            var response = new ResponseViewModel();

            var maintenance = _maintenanceDal.GetMaintenance(p => p.Id == id);

            if (maintenance == null)
            {
                response.IsSuccess = false;
                response.Message = "Maintenance bulunamadı.";
                return response;
            }

            var maintenanceDto = new MaintenanceDto()
            {
                Id = maintenance.Id,
                VehicleId = maintenance.VehicleId,
                UserId = maintenance.UserId,
                Description = maintenance.Description,
                PictureGroupId = maintenance.PictureGroupId,
                ExpectedTimeToFix = maintenance.ExpectedTimeToFix,
                ResponsibleUserId = maintenance.ResponsibleUserId,
                LocationLatitude = maintenance.LocationLatitude,
                LocationLongitude = maintenance.LocationLongitude,
                StatusId = maintenance.StatusId,
                CreateDate = maintenance.CreateDate,
                CreatedBy = maintenance.CreatedBy,
                ModifiedBy = maintenance.ModifiedBy,
                ModifyDate = maintenance.ModifyDate,
                IsDeleted = maintenance.IsDeleted
            };

            response.Data = maintenanceDto;

            return response;
        }

        public ResponseViewModel Update(MaintenanceDto maintenanceDto)
        {
            var response = new ResponseViewModel();

            var vehicleId = IsVehicleHave(maintenanceDto.VehicleId);
            if (!vehicleId)
            {
                response.IsSuccess = false;
                response.Message = "VehicleID Vehicle tablosunda bulunamadı ";

                return response;
            }

            var userId = IsUserHave(maintenanceDto.UserId);
            if (!userId)
            {
                response.IsSuccess = false;
                response.Message = "UserID User tablosunda bulunamadı ";

                return response;
            }

            if (maintenanceDto.PictureGroupId != null)
            {
                var pictureGroupId = IsPictureGroupHave((int)maintenanceDto.PictureGroupId);

                if (!pictureGroupId)
                {
                    response.IsSuccess = false;
                    response.Message = "pictureGroupId pictureGroup tablosunda bulunamadı ";

                    return response;
                }
            }

            if (maintenanceDto.ResponsibleUserId != null)
            {
                var responsibleUserId = IsUserHave((int)maintenanceDto.ResponsibleUserId);
                if (!responsibleUserId)
                {
                    response.IsSuccess = false;
                    response.Message = "responsibleUserId User tablosunda bulunamadı ";

                    return response;
                }
            }

            var statusId = IsStatusHave(maintenanceDto.StatusId);
            if (!statusId)
            {
                response.IsSuccess = false;
                response.Message = "statusId Status tablosunda bulunamadı ";

                return response;
            }

            if (maintenanceDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)maintenanceDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı ";

                    return response;
                }
            }

            if (maintenanceDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)maintenanceDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "ModifiedBy User tablosunda bulunamadı ";

                    return response;
                }
            }

            var maintenance = new Maintenance()
            {
                Id = maintenanceDto.Id,
                VehicleId = maintenanceDto.VehicleId,
                UserId = maintenanceDto.UserId,
                Description = maintenanceDto.Description,
                PictureGroupId = maintenanceDto.PictureGroupId,
                ExpectedTimeToFix = maintenanceDto.ExpectedTimeToFix,
                ResponsibleUserId = maintenanceDto.ResponsibleUserId,
                LocationLongitude = maintenanceDto.LocationLongitude,
                LocationLatitude = maintenanceDto.LocationLatitude,
                StatusId = maintenanceDto.StatusId,
                CreateDate = DateTime.Now,
                CreatedBy = maintenanceDto.CreatedBy,
                ModifiedBy = maintenanceDto.ModifiedBy,
                ModifyDate = maintenanceDto.ModifyDate,
                IsDeleted = maintenanceDto.IsDeleted,
            };

            _maintenanceDal.Update(maintenance);

            var saving = _maintenanceDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "Maintenance güncellenirken bir hata oluştu";
                response.Data = maintenance;
            }

            return response;
        }

        private bool IsUserHave(int userID)
        {
            var user = _userDal.GetUser(x => x.Id == userID);

            if (user == null) return false;
            else return true;
        }

        private bool IsVehicleHave(int vehicleId)
        {
            var vehicle = _vehicleDal.GetVehicle(x => x.Id == vehicleId);

            if (vehicle == null) return false;
            else return true;
        }

        private bool IsPictureGroupHave(int pictureGroupId)
        {
            var pictureGroup = _pictureGroupDal.GetPictureGroup(x => x.Id == pictureGroupId);

            if (pictureGroup == null) return false;
            else return true;
        }

        private bool IsStatusHave(int statusId)
        {
            var status = _statusDal.GetStatus(x => x.Id == statusId);

            if (status == null) return false;
            else return true;
        }

        private string GetPlateNo(int vehicleId)
        {
            var vehicle = _vehicleDal.GetVehicle(x => x.Id == vehicleId);
            if (vehicle != null) return vehicle.PlateNo;
            else return null;

        }

        private string GetVehicleTypeName(int vehicleId)
        {
            var vehicle = _vehicleDal.GetVehicle(p => p.Id == vehicleId);
            if (vehicle != null)
            {
                var vehicleType = _vehicleTypeDal.GetVehicleType(p => p.Id == vehicle.VehicleTypeId);
                return vehicleType.Name;
            }
            else return null;

        }

        private string GetMaintenanceDescription(int maintenanceID)
        {
            var maintenance = _maintenanceDal.GetMaintenance(p => p.Id == maintenanceID);

            return maintenance.Description;
        }

        private string GetPictureImage(int? pictureGroupId)
        {
            var pictureGroup = _pictureGroupDal.GetPictureGroup(p => p.Id == pictureGroupId);

            return pictureGroup.PictureImage;
        }

        private User GetUser(int userId)
        {
            var user = _userDal.GetUser(p => p.Id == userId);

            return user;
        }

        private string GetActionTypeName(int maintenanceID)
        {
            var maintenanceHistory = _maintenanceHistoryDal.GetMaintenanceHistory(p => p.MaintenanceId == maintenanceID);
            var actionType = _actionTypeDal.GetActionType(p => p.Id == maintenanceHistory.ActionTypeId);

            return actionType.Name;
        }

        private string GetHistoryText(int maintenanceId)
        {
            var maintenanceHistory = _maintenanceHistoryDal.GetMaintenanceHistory(p => p.Id == maintenanceId);

            return maintenanceHistory.Text;
        }
    }
}
