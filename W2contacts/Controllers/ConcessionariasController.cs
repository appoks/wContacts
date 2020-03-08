using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W2contacts;
using W2contacts.Models;

namespace W2contacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcessionariasController : ControllerBase
    {
        private readonly ContactsContext _context;

        public ConcessionariasController(ContactsContext context)
        {
            _context = context;
        }

        // GET: api/Concessionarias
        [HttpGet]
        public IEnumerable<Concessionaria> GetConcessionarias()
        {
            return _context.Concessionarias;
        }

        // GET: api/Concessionarias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcessionaria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var concessionaria = await _context.Concessionarias.FindAsync(id);

            if (concessionaria == null)
            {
                return NotFound();
            }

            return Ok(concessionaria);
        }

        // PUT: api/Concessionarias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcessionaria([FromRoute] int id, [FromBody] Concessionaria concessionaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != concessionaria.ID)
            {
                return BadRequest();
            }

            _context.Entry(concessionaria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConcessionariaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return Ok(concessionaria);
            // WORKAROUND > Para facilitar no front end da forma que fiz, retornando o objeto atualizado...
            //return NoContent();
        }

        // POST: api/Concessionarias
        [HttpPost]
        public async Task<IActionResult> PostConcessionaria([FromBody] Concessionaria concessionaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Concessionarias.Add(concessionaria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConcessionaria", new { id = concessionaria.ID }, concessionaria);
        }

        // DELETE: api/Concessionarias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcessionaria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var concessionaria = await _context.Concessionarias.FindAsync(id);
            if (concessionaria == null)
            {
                return NotFound();
            }

            _context.Concessionarias.Remove(concessionaria);
            await _context.SaveChangesAsync();

            return Ok(concessionaria);
        }

        private bool ConcessionariaExists(int id)
        {
            return _context.Concessionarias.Any(e => e.ID == id);
        }
    }
}