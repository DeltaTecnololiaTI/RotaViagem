using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RotaViagem.Interface;
using RotaViagem.Repositorys;
using RotaViagemModel.Model;

namespace RotaViagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotasController : ControllerBase
    {
        private readonly IRotaBusiness _rota;
        public RotasController(IRotaBusiness rota)
        {
            _rota = rota;
        }
        /// <summary>
        /// POST: api/Rotas
        /// </summary>
        /// <param name="rota"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateRota(TabRota rota)
        {
            if (rota == null) { return BadRequest(); }
            return Ok(_rota.Incluir(rota));
        }

        /// <summary>
        /// GET: api/Rotas/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetRotaById(int id)
        {
            TabRota rota = _rota.BuscarPorId(id);
            if (rota == null) 
                return NotFound(); 
            return Ok(rota);
        }

        /// <summary>
        /// GET: api/Rotas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRotas([FromQuery] string origem )
        {
            return Ok(_rota.BuscarOrigem(origem));
        }

        /// <summary>
        /// PUT: api/Rotas/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rota"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateRota(int id, TabRota rota)
        {
            if (rota == null) { return BadRequest(); }
            return Ok(_rota.Atualizar(id,rota));


        }

        /// <summary>
        /// DELETE: api/Rotas/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteRota(int id)
        {
            _rota.Excluir(id);
            return NoContent();
            
        }
        /// <summary>
        /// GET: api/Rotas
        /// </summary>
        /// <returns></returns>
        [HttpGet("consultar-rotas")]
        public IActionResult ConsultarRotas([FromQuery] string origem, [FromQuery] string destino)
        {
            var rotasRet = _rota.MontarRotas(origem, destino);
            return Ok("** " + rotasRet + " **");
        }

    }
}
