using DemoProje.Entities.Dto;

namespace DemoProje.Business.Abstract
{
    public interface IActionTypeService
    {
        ResponseViewModel GetActionType(int id);
        ResponseViewModel Add(ActionTypeDto actionTypeDto);
        ResponseViewModel Update(ActionTypeDto actionTypeDto);
        ResponseViewModel Delete(int id);
    }
}
