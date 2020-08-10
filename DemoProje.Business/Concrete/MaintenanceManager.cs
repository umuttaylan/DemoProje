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
        private readonly IUserDal _userDal;
        private readonly IVehicleDal _vehicleDal;
        private readonly IPictureGroupDal _pictureGroupDal;
        private readonly IStatusDal _statusDal;
        public MaintenanceManager(IMaintenanceDal maintenanceDal,
                                  IUserDal userDal,
                                  IVehicleDal vehicleDal,
                                  IPictureGroupDal pictureGroupDal,
                                  IStatusDal statusDal)
        {
            _maintenanceDal = maintenanceDal;
            _userDal = userDal;
            _vehicleDal = vehicleDal;
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

            return response;
        }

        public ResponseViewModel Delete(int id)
        {
            var response = new ResponseViewModel();

            var maintenance = _maintenanceDal.Get(p => p.Id == id);

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

        public ResponseViewModel Get(int id)
        {
            var response = new ResponseViewModel();

            var maintenance = _maintenanceDal.Get(p => p.Id == id);

            if (maintenance == null)
            {
                response.IsSuccess = false;
                response.Message = "Maintenance bulunamadı.";
                return response;
            }

            var maintenanceDto = new MaintenanceDto()
            {
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
            var user = _userDal.Get(x => x.Id == userID);

            if (user == null) return false;
            else return true;
        }

        private bool IsVehicleHave(int vehicleId)
        {
            var vehicle = _vehicleDal.Get(x => x.Id == vehicleId);

            if (vehicle == null) return false;
            else return true;
        }

        private bool IsPictureGroupHave(int pictureGroupId)
        {
            var pictureGroup = _pictureGroupDal.Get(x => x.Id == pictureGroupId);

            if (pictureGroup == null) return false;
            else return true;
        }

        private bool IsStatusHave(int statusId)
        {
            var status = _statusDal.Get(x => x.Id == statusId);

            if (status == null) return false;
            else return true;
        }


    }
}
