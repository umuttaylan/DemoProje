using DemoProje.Business.Abstract;
using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public ResponseViewModel Add(UserDto userDto)
        {
            var response = new ResponseViewModel();
            if (userDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)userDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (userDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)userDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "ModifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var user = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address,
                CreateDate = DateTime.Now,
                CreatedBy = userDto.CreatedBy,
                ModifyDate = userDto.ModifyDate,
                ModifiedBy = userDto.ModifiedBy,
                Profilepicture = userDto.Profilepicture
            };

            _userDal.Add(user);
            var saving = _userDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "User ekleme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = "Id : " + user.Id;

            return response;
        }

        public ResponseViewModel Delete(int id)
        {
            var response = new ResponseViewModel();

            var user = _userDal.GetUser(p => p.Id == id);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "User bulunamadı.";
                return response;
            }

            user.IsDeleted = true;

            _userDal.Update(user);
            var saving = _userDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "User silme işlemi sırasında hata oluştu.";

                return response;
            }

            response.Data = user;

            return response;
        }

        public ResponseViewModel Get(int id)
        {
            var response = new ResponseViewModel();

            var user = _userDal.GetUser(p => p.Id == id);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "User bulunamadı.";
                return response;
            }

            var userDto = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                CreateDate = user.CreateDate,
                CreatedBy = user.CreatedBy,
                ModifyDate = user.ModifyDate,
                ModifiedBy = user.ModifiedBy,
                IsDeleted = user.IsDeleted,
                Profilepicture = user.Profilepicture
            };

            response.Data = userDto;

            return response;
        }

        public ResponseViewModel Update(UserDto userDto)
        {
            var response = new ResponseViewModel();
            if (userDto.CreatedBy != null)
            {
                var createdBy = IsUserHave((int)userDto.CreatedBy);
                if (!createdBy)
                {
                    response.IsSuccess = false;
                    response.Message = "createdBy User tablosunda bulunamadı";

                    return response;
                }
            }

            if (userDto.ModifiedBy != null)
            {
                var modifiedBy = IsUserHave((int)userDto.ModifiedBy);
                if (!modifiedBy)
                {
                    response.IsSuccess = false;
                    response.Message = "ModifiedBy User tablosunda bulunamadı";

                    return response;
                }
            }

            var user = new User()
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address,
                CreateDate = DateTime.Now,
                CreatedBy = userDto.CreatedBy,
                ModifyDate = userDto.ModifyDate,
                ModifiedBy = userDto.ModifiedBy,
                IsDeleted = userDto.IsDeleted,
                Profilepicture = userDto.Profilepicture
            };

            _userDal.Update(user);
            var saving = _userDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "User güncelleme işlemi sırasında hata oluştu.";

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
