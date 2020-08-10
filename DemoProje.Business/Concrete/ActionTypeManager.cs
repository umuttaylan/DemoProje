using DemoProje.Business.Abstract;
using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Concrete
{
    public class ActionTypeManager : IActionTypeService
    {
        private readonly IActionTypeDal _actionTypeDal;
        private readonly IUserDal _userDal;
        public ActionTypeManager(IActionTypeDal actionTypeDal, IUserDal userDal)
        {
            _actionTypeDal = actionTypeDal;
            _userDal = userDal;
        }
        public ResponseViewModel Add(ActionTypeDto actionTypeDto)
        {
            var response = new ResponseViewModel();

            var user = IsUserHave(actionTypeDto.CreatedBy);

            if (!user)
            {
                response.IsSuccess = false;
                response.Message = "User bulunamadı";

                return response;
            }

            var actionType = new ActionType()
            {
                Name = actionTypeDto.Name,
                CreateDate = DateTime.Now,
                CreatedBy = actionTypeDto.CreatedBy,
            };
            _actionTypeDal.Add(actionType);
            

            var saving = _actionTypeDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "ActionType kaydedilirken bir hata oluştu";
                response.Data = actionType;
            }

            return response;
        }

        public ResponseViewModel Delete(int id)
        {
            var response = new ResponseViewModel();

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

        public ResponseViewModel GetActionType(int id)
        {
            var response = new ResponseViewModel();

            var actionType = _actionTypeDal.Get(p => p.Id == id);

            if (actionType == null)
            {
                response.IsSuccess = false;
                response.Message = "ActionType bulunamadı.";
                return response;
            }

            var actionTypeDto = new ActionTypeDto()
            {
               Name = actionType.Name,
               CreateDate = actionType.CreateDate,
               CreatedBy = actionType.CreatedBy,
               ModifyDate = actionType.ModifyDate,
               ModifiedBy = actionType.ModifiedBy,
               IsDeleted = actionType.IsDeleted
            };

            response.Data = actionTypeDto;

            return response;
        }

        public ResponseViewModel Update(ActionTypeDto actionTypeDto)
        {
            var response = new ResponseViewModel();

            var user = IsUserHave(actionTypeDto.CreatedBy);

            if (!user)
            {
                response.IsSuccess = false;
                response.Message = "User bulunamadı";

                return response;
            }

            var actionType = new ActionType()
            {
                Id = actionTypeDto.Id,
                Name = actionTypeDto.Name,
                CreatedBy = actionTypeDto.CreatedBy,
                ModifiedBy = actionTypeDto.ModifiedBy,
                ModifyDate = DateTime.Now,
                IsDeleted = actionTypeDto.IsDeleted
            };

            _actionTypeDal.Update(actionType);
            var saving = _actionTypeDal.SaveChanges();
            if (!saving)
            {
                response.IsSuccess = false;
                response.Message = "ActionType bilgisi güncellenirken hata oluştu.";
            }

            return response;
        }

        private bool IsUserHave(int userID)
        {
            var user = _userDal.Get(x => x.Id == actionTypeDto.CreatedBy);

            if (user == null) return false;
            else return true;
        }
    }
}
