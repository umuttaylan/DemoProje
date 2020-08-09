using DemoProje.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Abstract
{
    public interface IStatusService
    {
        ResponseViewModel Get(int id);
        ResponseViewModel Add(StatusDto statusDto);
        ResponseViewModel Update(StatusDto statusDto);
        ResponseViewModel Delete(int id);
    }
}
