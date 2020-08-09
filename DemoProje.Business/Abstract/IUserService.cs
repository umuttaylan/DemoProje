using DemoProje.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Abstract
{
    public interface IUserService
    {
        ResponseViewModel Get(int id);
        ResponseViewModel Add(UserDto userDto);
        ResponseViewModel Update(UserDto userDto);
        ResponseViewModel Delete(int id);
    }
}
