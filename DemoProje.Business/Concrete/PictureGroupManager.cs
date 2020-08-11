using DemoProje.Business.Abstract;
using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class PictureGroupManager : IPictureGroupService
    {
        private readonly IPictureGroupDal _pictureGroupDal;
        private readonly IUserDal _userDal;
        public PictureGroupManager(IPictureGroupDal pictureGroupDal,
                                   IUserDal userDal)
        {
            _pictureGroupDal = pictureGroupDal;
            _userDal = userDal;
        }
        public ResponseViewModel Add(PictureGroupDto pictureGroupDto)
        {
            var response = new ResponseViewModel();
            if (pictureGroupDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)pictureGroupDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı ";

                    return response;
                }
            }

            var pictureGroup = new PictureGroup()
            {
                PictureImage = pictureGroupDto.PictureImage,
                CreateDate = DateTime.Now,
                CreatedBy = pictureGroupDto.CreatedBy,
            };

            _pictureGroupDal.Add(pictureGroup);
            var saving = _pictureGroupDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "PictureGroup kaydedilirken bir hata oluştu";
                response.Data = pictureGroup;
            }

            response.Data = "Id : " + pictureGroup.Id;

            return response;
        }

        public ResponseViewModel Delete(int id)
        {
            var response = new ResponseViewModel();

            var pictureGroup = _pictureGroupDal.GetPictureGroup(p => p.Id == id);

            if (pictureGroup == null)
            {
                response.IsSuccess = false;
                response.Message = "PictureGroup bulunamadı.";
                return response;
            }

            pictureGroup.IsDeleted = true;

            _pictureGroupDal.Update(pictureGroup);
            var saving = _pictureGroupDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "PictureGroup silme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = pictureGroup;

            return response;
        }

        public ResponseViewModel Get(int id)
        {
            var response = new ResponseViewModel();

            var pictureGroup = _pictureGroupDal.GetPictureGroup(p => p.Id == id);

            if (pictureGroup == null)
            {
                response.IsSuccess = false;
                response.Message = "PictureGroup bulunamadı.";
                return response;
            }

            var pictureGroupDto = new PictureGroupDto()
            {
                Id = pictureGroup.Id,
                PictureImage = pictureGroup.PictureImage,
                CreateDate = pictureGroup.CreateDate,
                CreatedBy = pictureGroup.CreatedBy,
                ModifyDate = pictureGroup.ModifyDate,
                ModifiedBy = pictureGroup.ModifiedBy,
                IsDeleted = pictureGroup.IsDeleted,
            };

            response.Data = pictureGroupDto;

            return response;
        }

        public ResponseViewModel Update(PictureGroupDto pictureGroupDto)
        {
            var response = new ResponseViewModel();
            if (pictureGroupDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)pictureGroupDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı ";

                    return response;
                }
            }

            if (pictureGroupDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)pictureGroupDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "ModifiedBy User tablosunda bulunamadı ";

                    return response;
                }
            }

            var pictureGroup = new PictureGroup()
            {
                Id = pictureGroupDto.Id,
                PictureImage = pictureGroupDto.PictureImage,
                CreateDate = DateTime.Now,
                CreatedBy = pictureGroupDto.CreatedBy,
                ModifyDate = pictureGroupDto.ModifyDate,
                ModifiedBy = pictureGroupDto.ModifiedBy,
                IsDeleted = pictureGroupDto.IsDeleted
            };

            _pictureGroupDal.Update(pictureGroup);
            var saving = _pictureGroupDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "PictureGroup güncellenirken bir hata oluştu";
                response.Data = pictureGroup;
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
