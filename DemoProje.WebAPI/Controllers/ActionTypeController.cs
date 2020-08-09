using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProje.Business.Abstract;
using DemoProje.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProje.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActionTypeController : ControllerBase
    {
        private readonly IActionTypeService _actionTypeService;
        public ActionTypeController(IActionTypeService actionTypeService)
        {
            _actionTypeService = actionTypeService;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var response = new ResponseViewModel();
            response = _actionTypeService.GetActionType(id);

            if (!response.IsSuccess)
            {
                if (response.Data == null)
                {
                    return NotFound(response);
                }
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(ActionTypeDto actionTypeDto)
        {
            var response = new ResponseViewModel();
            response = _actionTypeService.Add(actionTypeDto);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public IActionResult Edit(ActionTypeDto actionTypeDto)
        {
            var response = new ResponseViewModel();
            response = _actionTypeService.Update(actionTypeDto);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = new ResponseViewModel();
            response = _actionTypeService.Delete(id);

            if (!response.IsSuccess)
            {
                if (response.Data == null)
                {
                    return NotFound(response);
                }
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
