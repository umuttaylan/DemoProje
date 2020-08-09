using DemoProje.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.Business.Abstract
{
    public interface IPictureGroupService
    {
        ResponseViewModel Get(int id);
        ResponseViewModel Add(PictureGroupDto pictureGroupDto);
        ResponseViewModel Update(PictureGroupDto pictureGroupDto);
        ResponseViewModel Delete(int id);
    }
}
