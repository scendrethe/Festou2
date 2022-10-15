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
            var model = await _context.Local.ToListAsync();
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

    }
}
