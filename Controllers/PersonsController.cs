using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonsWebApi.Models;
using PersonsWebApi.Services;
using PersonsWebApi.Exceptions;

namespace PersonsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService service;

        public PersonsController(IPersonsService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Проверяет данные на валидность, и, если они валидны отправляет их в PersonsService для добавления в БД
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id добавленного лица</returns>
        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create([FromBody] PersonalDataModel model)
        {
            if (ModelState.IsValid) return Ok(service.Create(model));
            else return BadRequest();
        }

        /// <summary>
        /// Проверяет данные на валидность, и, если они валидны, отправляет их в PersonsService для получения данных из БД.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Массив лиц</returns>
        [HttpGet]
        [Route(nameof(Read))]
        public IActionResult Read([FromQuery] PersonalDataModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    return Ok(service.Read(model));
                }
                catch (PersonNotFoundException)
                {
                    return NotFound();
                }

            else return BadRequest();
        }

        [HttpGet]
        [Route(nameof(Find))]
        public IActionResult Find([FromQuery] int id)
        {
            try
            {
                return Ok(service.Find(id));
            }
            catch (PersonNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
