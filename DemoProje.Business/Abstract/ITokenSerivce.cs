using DemoProje.Entities.Dto;
using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Abstract
{
    public interface ITokenSerivce
    {
        ResponseViewModel Authenticate(AuthenticateDto authenticateDto);
    }
}
