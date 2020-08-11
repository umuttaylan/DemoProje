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
    public class PictureGroupController : ControllerBase
    {
        private readonly IPictureGroupService _pictureGroupService;
        public PictureGroupController(IPictureGroupService pictureGroupService)
        {
            _pictureGroupService = pictureGroupService;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var response = new ResponseViewModel();
            response = _pictureGroupService.Get(id);

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
        public IActionResult Add(PictureGroupDto pictureGroupDto)
        {
            var response = new ResponseViewModel();
            response = _pictureGroupService.Add(pictureGroupDto);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public IActionResult Edit(PictureGroupDto pictureGroupDto)
        {
            var response = new ResponseViewModel();
            response = _pictureGroupService.Update(pictureGroupDto);

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
            response = _pictureGroupService.Delete(id);

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
