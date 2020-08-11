using DemoProje.Business.Abstract;
using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class VehicleTypeManager : IVehicleTypeService
    {
        private readonly IVehicleTypeDal _vehicleTypeDal;
        private readonly IUserDal _userDal;
        public VehicleTypeManager(IVehicleTypeDal vehicleTypeDal,
                                  IUserDal userDal)
        {
            _vehicleTypeDal = vehicleTypeDal;
            _userDal = userDal;
        }
        public ResponseViewModel Add(VehicleTypeDto vehicleTypeDto)
        {
            var response = new ResponseViewModel();

            if (vehicleTypeDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)vehicleTypeDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (vehicleTypeDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)vehicleTypeDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "ModifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var vehicleTpye = new VehicleType()
            {
                Name = vehicleTypeDto.Name,
                CreateDate = vehicleTypeDto.CreateDate,
                CreatedBy = vehicleTypeDto.CreatedBy,
                ModifyDate = vehicleTypeDto.ModifyDate,
                ModifiedBy = vehicleTypeDto.ModifiedBy
            };

            _vehicleTypeDal.Add(vehicleTpye);
            var saving = _vehicleTypeDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "VehicleType ekleme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = "Id : " + vehicleTpye.Id;

            return response;
        }

        public ResponseViewModel Delete(int id)
        {
            var response = new ResponseViewModel();

            var vehicleType = _vehicleTypeDal.GetVehicleType(p => p.Id == id);

            if (vehicleType == null)
            {
                response.IsSuccess = false;
                response.Message = "vehicleType bulunamadı.";
                return response;
            }

            vehicleType.IsDeleted = true;

            _vehicleTypeDal.Update(vehicleType);
            var saving = _vehicleTypeDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "vehicleType silme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = vehicleType;

            return response;
        }

        public ResponseViewModel Get(int id)
        {
            var response = new ResponseViewModel();

            var vehicleType = _vehicleTypeDal.GetVehicleType(p => p.Id == id);

            if (vehicleType == null)
            {
                response.IsSuccess = false;
                response.Message = "vehicleType bulunamadı.";
                return response;
            }

            var vehicleTypeDto = new VehicleTypeDto()
            {
                Id = vehicleType.Id,
                Name = vehicleType.Name,
                CreateDate = vehicleType.CreateDate,
                CreatedBy = vehicleType.CreatedBy,
                ModifyDate = vehicleType.ModifyDate,
                ModifiedBy = vehicleType.ModifiedBy,
                IsDeleted = vehicleType.IsDeleted
            };

            response.Data = vehicleTypeDto;

            return response;
        }

        public ResponseViewModel Update(VehicleTypeDto vehicleTypeDto)
        {
            var response = new ResponseViewModel();

            if (vehicleTypeDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)vehicleTypeDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (vehicleTypeDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)vehicleTypeDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "ModifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var vehicleTpye = new VehicleType()
            {
                Id = vehicleTypeDto.Id,
                Name = vehicleTypeDto.Name,
                CreateDate = vehicleTypeDto.CreateDate,
                CreatedBy = vehicleTypeDto.CreatedBy,
                ModifyDate = vehicleTypeDto.ModifyDate,
                ModifiedBy = vehicleTypeDto.ModifiedBy,
                IsDeleted = vehicleTypeDto.IsDeleted
            };

            _vehicleTypeDal.Update(vehicleTpye);
            var saving = _vehicleTypeDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "VehicleType güncelleme işlemi sırasında hata oluştu.";

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
    }
}
