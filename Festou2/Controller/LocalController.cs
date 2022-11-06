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

    }
}

