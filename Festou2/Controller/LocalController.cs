using Festou2.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Festou2.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocalController(AppDbContext context)
        {
            _context = context;
        }

        //        [Authorize(Roles = "Usuario")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Local.ToListAsync();
            return Ok(model);
        }

        //        [Authorize(Roles = "Administrador,Usuario")]

        [HttpPost]
        public async Task<ActionResult> Create(Local model)
        {
            if (model.QtdPessoas <= 50 || model.tipoFesta <= 0)
            {
                return BadRequest(new { message = "Quantidade de pessoas deve ser maior do que 50 pessoas e Tipo de Festa são obrigatórios" });
            }

            _context.Local.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Local
                  .FirstOrDefaultAsync(c => c.LocalId == id);

            if (model == null) return NotFound();

            GerarLinks(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Local model)
        {
            if (id != model.LocalId) return BadRequest();
            var modeloDb = await _context.Local.AsNoTracking()
                .FirstOrDefaultAsync(c => c.LocalId == id);
            if (modeloDb == null) return NotFound();

            _context.Local.Update(model);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Local.FindAsync(id);

            if (model == null) return NotFound();

            _context.Local.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        private void GerarLinks(Local model)

        {
            model.Links.Add(new LinkDto(model.LocalId, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.LocalId, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.LocalId, Url.ActionLink(), rel: "delete", metodo: "Delete"));
        }
    }
}

