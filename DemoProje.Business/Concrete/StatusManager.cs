using DemoProje.Business.Abstract;
using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class StatusManager : IStatusService
    {
        private readonly IStatusDal _statusDal;
        private readonly IUserDal _userDal;
        public StatusManager(IStatusDal statusDal,
                             IUserDal userDal)
        {
            _statusDal = statusDal;
            _userDal = userDal;
        }
        public ResponseViewModel Add(StatusDto statusDto)
        {
            var response = new ResponseViewModel();

            if (statusDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)statusDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (statusDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)statusDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "modifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var status = new Status()
            {
                Name = statusDto.Name,
                CreateDate = DateTime.Now,
                CreatedBy = statusDto.CreatedBy,
                ModifyDate = DateTime.Now,
                ModifiedBy = statusDto.ModifiedBy
            };

            _statusDal.Add(status);
            var saving = _statusDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "Status ekleme işlemi sırasında hata oluştu.";

                return response;
            }

            return response;
        }

        public ResponseViewModel Delete(int id)
        {
            var response = new ResponseViewModel();

            var status = _statusDal.Get(p => p.Id == id);

            if (status == null)
            {
                response.IsSuccess = false;
                response.Message = "Status bulunamadı.";
                return response;
            }

            status.IsDeleted = true;

            _statusDal.Update(status);
            var saving = _statusDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "Status silme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = status;

            return response;
        }

        public ResponseViewModel Get(int id)
        {
            var response = new ResponseViewModel();

            var status = _statusDal.Get(p => p.Id == id);

            if (status == null)
            {
                response.IsSuccess = false;
                response.Message = "Status bulunamadı.";
                return response;
            }

            var statusDto = new StatusDto()
            {
                Name = status.Name,
                CreateDate = status.CreateDate,
                CreatedBy = status.CreatedBy,
                ModifyDate = status.ModifyDate,
                ModifiedBy = status.ModifiedBy,
                IsDeleted = status.IsDeleted
            };

            response.Data = statusDto;

            return response;
        }

        public ResponseViewModel Update(StatusDto statusDto)
        {
            var response = new ResponseViewModel();

            if (statusDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)statusDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (statusDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)statusDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "modifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var status = new Status()
            {
                Id = statusDto.Id,
                Name = statusDto.Name,
                CreateDate = DateTime.Now,
                CreatedBy = statusDto.CreatedBy,
                ModifyDate = DateTime.Now,
                ModifiedBy = statusDto.ModifiedBy,
                IsDeleted = statusDto.IsDeleted
            };

            _statusDal.Update(status);
            var saving = _statusDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "Status güncelleme işlemi sırasında hata oluştu.";

                return response;
            }

            return response;
        }

        private bool IsUserHave(int userID)
        {
            var user = _userDal.Get(x => x.Id == userID);

            if (user == null) return false;
            else return true;
        }
    }
}
