using Festou2.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Festou2.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        //        [Authorize(Roles = "Usuario")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Cliente.ToListAsync();
            return Ok(model);
        }

        //        [Authorize(Roles = "Administrador,Usuario")]

        [HttpPost]
        public async Task<ActionResult> Create(Cliente model)
        {
            if (model.ClienteCPF <= 0)
            {
                return BadRequest(new { message = "CPF é obrigatório" });
            }

            _context.Cliente.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.ClienteId }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Cliente
                .FirstOrDefaultAsync(c => c.ClienteId == id);

            if (model == null) NotFound();

            GerarLinks(model);
            return Ok(model);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Cliente model)
        {
            if (id != model.ClienteId) return BadRequest();
            var modeloDb = await _context.Cliente.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == id);
            if (modeloDb == null) return NotFound();

            _context.Cliente.Update(model);
            await _context.SaveChangesAsync();
            return NoContent();
        
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Cliente.FindAsync(id);
                
            if (model == null) NotFound();

            _context.Cliente.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        private void GerarLinks(Cliente model)
        {
            model.Links.Add(new LinkDto(model.ClienteId, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.ClienteId, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.ClienteId, Url.ActionLink(), rel: "delete", metodo: "Delete"));

        }
    }
}