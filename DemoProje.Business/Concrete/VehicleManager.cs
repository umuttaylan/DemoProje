using DemoProje.Business.Abstract;
using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class VehicleManager : IVehicleService
    {
        private readonly IVehicleDal _vehicleDal;
        private readonly IVehicleTypeDal _vehicleTypeDal;
        private readonly IUserDal _userDal;
        public VehicleManager(IVehicleDal vehicleDal,
                              IVehicleTypeDal vehicleTypeDal,
                              IUserDal userDal)
        {
            _vehicleDal = vehicleDal;
            _vehicleTypeDal = vehicleTypeDal;
            _userDal = userDal;
        }
        public ResponseViewModel Add(VehicleDto vehicleDto)
        {
            var response = new ResponseViewModel();

            var vehicletypeId = IsVehicleTypeHave(vehicleDto.VehicleTypeId);
            if (!vehicletypeId)
            {
                response.IsSuccess = false;
                response.Message = "vehicletypeId VehicleType tablosunda bulunamadı";

                return response;
            }

            if (vehicleDto.UserId != null)
            {
                var userId = IsUserHave((int)vehicleDto.UserId);
                if (!userId)
                {
                    response.IsSuccess = false;
                    response.Message = "UserID User tablosunda bulunamadı";

                    return response;
                }
            }

            if (vehicleDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)vehicleDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (vehicleDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)vehicleDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "ModifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var vehicle = new Vehicle()
            {
                PlateNo = vehicleDto.PlateNo,
                VehicleTypeId = vehicleDto.VehicleTypeId,
                UserId = vehicleDto.UserId,
                CreateDate = DateTime.Now,
                CreatedBy = vehicleDto.CreatedBy,
                ModifyDate = vehicleDto.ModifyDate,
                ModifiedBy = vehicleDto.ModifiedBy
            };

            _vehicleDal.Add(vehicle);
            var saving = _vehicleDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "Vehicle ekleme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = "Id : " + vehicle.Id;

            return response;
        }

        public ResponseViewModel Delete(int id)
        {
            var response = new ResponseViewModel();

            var vehicle = _vehicleDal.GetVehicle(p => p.Id == id);

            if (vehicle == null)
            {
                response.IsSuccess = false;
                response.Message = "Vehicle bulunamadı.";
                return response;
            }

            vehicle.IsDeleted = true;

            _vehicleDal.Update(vehicle);
            var saving = _vehicleDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "Vehicle silme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = vehicle;

            return response;
        }

        public ResponseViewModel Get(int id)
        {
            var response = new ResponseViewModel();

            var vehicle = _vehicleDal.GetVehicle(p => p.Id == id);

            if (vehicle == null)
            {
                response.IsSuccess = false;
                response.Message = "Vehicle bulunamadı.";
                return response;
            }

            var vehicleDto = new VehicleDto()
            {
                Id = vehicle.Id ,
                PlateNo = vehicle.PlateNo,
                VehicleTypeId = vehicle.VehicleTypeId,
                UserId = vehicle.UserId,
                CreateDate = vehicle.CreateDate,
                CreatedBy = vehicle.CreatedBy,
                ModifyDate = vehicle.ModifyDate,
                ModifiedBy = vehicle.ModifiedBy,
                IsDeleted = vehicle.IsDeleted
            };

            response.Data = vehicleDto;

            return response;
        }

        public ResponseViewModel Update(VehicleDto vehicleDto)
        {
            var response = new ResponseViewModel();

            var vehicletypeId = IsVehicleTypeHave(vehicleDto.VehicleTypeId);
            if (!vehicletypeId)
            {
                response.IsSuccess = false;
                response.Message = "vehicletypeId VehicleType tablosunda bulunamadı";

                return response;
            }

            if (vehicleDto.UserId != null)
            {
                var userId = IsUserHave((int)vehicleDto.UserId);
                if (!userId)
                {
                    response.IsSuccess = false;
                    response.Message = "UserID User tablosunda bulunamadı";

                    return response;
                }
            }

            if (vehicleDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)vehicleDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (vehicleDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)vehicleDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "ModifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var vehicle = new Vehicle()
            {
                Id = vehicleDto.Id,
                PlateNo = vehicleDto.PlateNo,
                VehicleTypeId = vehicleDto.VehicleTypeId,
                UserId = vehicleDto.UserId,
                CreateDate = vehicleDto.CreateDate,
                CreatedBy = vehicleDto.CreatedBy,
                ModifyDate = vehicleDto.ModifyDate,
                ModifiedBy = vehicleDto.ModifiedBy,
                IsDeleted = vehicleDto.IsDeleted
            };

            _vehicleDal.Update(vehicle);
            var saving = _vehicleDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "Vehicle güncelleme işlemi sırasında hata oluştu.";

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

        private bool IsVehicleTypeHave(int vehicleTypeId)
        {
            var vehicleType = _vehicleTypeDal.GetVehicleType(x => x.Id == vehicleTypeId);

            if (vehicleType == null) return false;
            else return true;
        }
    }
}
