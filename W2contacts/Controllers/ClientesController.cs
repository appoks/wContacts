using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using W2contacts;
using W2contacts.Models;

namespace W2contacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ContactsContext _context;

        public ClientesController(ContactsContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public IEnumerable<Cliente> GetClientes()
        {
            return _context.Clientes;
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente([FromRoute] int id, [FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.ID)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.ID }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ID == id);
        }
    }
}