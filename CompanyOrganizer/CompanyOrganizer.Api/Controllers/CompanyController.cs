using CompanyOrganizer.Infrastructure.DTO;
using CompanyOrganizer.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace CompanyOrganizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        //1.
        [Authorize]
        [HttpPost("create")]
        [Authorize]
        public ActionResult Post([FromBody] CompanyDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var key = _companyService.Create(model);
            return Created($"api/company/" + key, key);
        }

        //2.
        [HttpPost("search")]
        public ActionResult Post([FromBody] SearchDto model)
        {
            var searchCompanys = _companyService.Search(model);
            return Ok(searchCompanys);
        }

        //3.
        [Authorize]
        [HttpPut("<id>")]
        public ActionResult Put(long id, [FromBody] CompanyDto model)
        {
            _companyService.Update(id, model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        //4.
        [Authorize]
        [HttpDelete("<id>")]
        public ActionResult Delete(long id)
        {
            _companyService.Delete(id);
            return NoContent();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Get()
        {
            var companys = _companyService.GetAll();
            return Ok(companys);
        }

        [Authorize]
        [HttpGet("id")]
        public ActionResult<CompanyDto> Get(long id)
        {
            var company = _companyService.Get(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }
    }
}

