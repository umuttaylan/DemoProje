using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoProje.Business.Abstract;
using DemoProje.Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProje.WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MaintenanceHistoryController : ControllerBase
    {
        private readonly IMaintenanceHistoryService _maintenanceHistoryService;
        public MaintenanceHistoryController(IMaintenanceHistoryService maintenanceHistoryService)
        {
            _maintenanceHistoryService = maintenanceHistoryService;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var response = new ResponseViewModel();
            response = _maintenanceHistoryService.Get(id);

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
        public IActionResult Add(MaintenanceHistoryDto maintenanceHistoryDto)
        {
            var response = new ResponseViewModel();
            response = _maintenanceHistoryService.Add(maintenanceHistoryDto);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public IActionResult Edit(MaintenanceHistoryDto maintenanceHistoryDto)
        {
            var response = new ResponseViewModel();
            response = _maintenanceHistoryService.Update(maintenanceHistoryDto);

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
            response = _maintenanceHistoryService.Delete(id);

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
