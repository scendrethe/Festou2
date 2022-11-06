using Festou2.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Festou2.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class LocadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocadorController(AppDbContext context)
        {
            _context = context;
        }

        //        [Authorize(Roles = "Usuario")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Locador.ToListAsync();

            return Ok(model);
        }

        //        [Authorize(Roles = "Administrador,Usuario")]

        [HttpPost]
        public async Task<ActionResult> Create(Locador model)
        {
            if (model.LocadorCPF <= 0 || model.LocadorPreco <= 0)
            {
                return BadRequest(new { message = "Preço e CPF são obrigatórios" });
            }

            _context.Locador.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.LocadorId }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Locador
                .Include(t => t.Locais)
                .FirstOrDefaultAsync(c => c.LocadorId == id);

            if (model == null) return NotFound();

            GerarLinks(model);
            return Ok(model);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Locador model)
        {
            if (id != model.LocadorId) return BadRequest();
            var modeloDb = await _context.Locador.AsNoTracking()
                .FirstOrDefaultAsync(c => c.LocadorId == id);
            if (modeloDb == null) return NotFound();

            _context.Locador.Update(model);
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Locador.FindAsync(id);

            if (model == null) return NotFound();

            _context.Locador.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        private void GerarLinks(Locador model)
        {
            model.Links.Add(new LinkDto(model.LocadorId, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.LocadorId, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.LocadorId, Url.ActionLink(), rel: "delete", metodo: "Delete"));

        }
    }
}
