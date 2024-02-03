using CRUD.Context;
using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonDbContext _context;

        public PersonController(PersonDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult> GetPersons()
        {
            //var personList =  await _context.Persons.ToListAsync();
            //return Ok(personList);
            return Ok(await _context.Persons.ToListAsync()); //Kısa temiz

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult> GetPerson([FromRoute] Guid id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound("Kullanıcı Bulunamadı!");
            }

            return Ok(person);
        }


        [HttpPost]
        public async Task<ActionResult> AddPerson(AddPersonRequest request)
        {
            //if(!ModelState.IsValid)
            //{
            //    return NotFound("Kullanıcı Bulunamadı!");
            //}

            var person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address
            };

            await _context.AddAsync(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdatePerson([FromRoute] Guid id, UpdatePersonRequest request)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person != null)
            {
                person.FirstName = request.FirstName;
                person.Email = request.Email;
                person.Phone = request.Phone;
                person.Address = request.Address;

                await _context.SaveChangesAsync();
                return Ok(person);
            }


            return NotFound("Başarısız");
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeletePerson([FromRoute] Guid id)
        {
            var deletePerson = await  _context.Persons.FindAsync(id);

            if(deletePerson == null)
            {
                return NotFound("Silinecek Kullanıcı Bulunamadı!");
            }

            _context.Persons.Remove(deletePerson);
            await _context.SaveChangesAsync();
            return Ok(deletePerson);
        }

    }
}
