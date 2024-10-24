using CursoIdiomasAPI.Entitites;
using CursoIdiomasAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepositorio _alunoRepositorio;


        public AlunoController(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
            
        } 

        [HttpGet] // LISTAR ALUNOS
        public async Task<ActionResult<IEnumerable<Aluno>>> Listar()
        {
            return await _alunoRepositorio.Listar();
            
        }

        [HttpGet("{id}")] // BUSCAR INFORMAÇÕES DO ALUNO PELO ID
        public async Task<IActionResult> BuscarAlunoPorId(int id)
        {
            
            var aluno = await _alunoRepositorio.BuscarporId(id);

            
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            return Ok(aluno);
        }



        [HttpPost] // CADASTRAR ALUNO
        public async Task<ActionResult<Aluno>> Cadastrar([FromBody] Aluno aluno)
        {

            try
            {
                if (aluno == null)
                             
                {
                    return BadRequest("Aluno não pode ser nulo");
                    
                }

                var alunoCadastrado = await _alunoRepositorio.Cadastrar(aluno);
                return CreatedAtAction(nameof(Listar), new { id = alunoCadastrado.Id }, alunoCadastrado);

            }

            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }

        }



        [HttpPut] // EDITAR ALUNO
        public async Task<ActionResult<Aluno>> Editar(Aluno aluno)
        {
            try
            {
                return await _alunoRepositorio.Editar(aluno);
                

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")] // DELETAR ALUNO

        public async Task<ActionResult<bool>> Deletar(int id)
        {
            try
            {
                var deletarAluno = await _alunoRepositorio.Deletar(id);

                return Ok(deletarAluno);

            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
