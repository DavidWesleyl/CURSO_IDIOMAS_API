using CursoIdiomasAPI.Data;
using CursoIdiomasAPI.Entitites;
using CursoIdiomasAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaRepositorio _turmaRepositorio;


        public TurmaController(ITurmaRepositorio turmaRepositorio)
        {
            _turmaRepositorio = turmaRepositorio;
            
        }

     

        [HttpGet] // MOSTRAR TODAS AS TURMAS CADASTRADAS
        public async Task<ActionResult<List<Turma>>> Listar()
        {
            var turmas = await _turmaRepositorio.Listar();
            return Ok(turmas);
        }

        [HttpPost] // CADASTRAR NOVA TURMA
        public async Task<ActionResult<Turma>> Cadastrar(Turma turma)
        {
            try
            {
                var resultado = await _turmaRepositorio.Cadastrar(turma);
                return CreatedAtAction(nameof(Listar), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // ELETAR TURMA, SE NÃO POSSUIR ALUNOS MATRICULADOS
        public async Task<ActionResult<bool>> Deletar(int id)
        {
            try
            {
                var resultado = await _turmaRepositorio.Deletar(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

