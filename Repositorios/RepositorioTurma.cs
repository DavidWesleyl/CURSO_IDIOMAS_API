using CursoIdiomasAPI.Data;
using CursoIdiomasAPI.Entitites;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomasAPI.Repositorios
{
    public class RepositorioTurma : ITurmaRepositorio
    {
        private readonly IdiomasDB _context;

        public RepositorioTurma(IdiomasDB context)
        {
            _context = context;

        }


        public async Task<Turma> Cadastrar(Turma turma)
        {
            if (turma.Alunos.Count >= 5) // VERIFICA SE A TURMA POSSUI 5 ALUNOS 
            {
                throw new Exception("A Turma já possui 5 alunos!");
            }

            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
            return turma;

        }

        public async Task<bool> Deletar(int id)
        {
            Turma turmas = await _context.Turmas.Include(x => x.Alunos).FirstOrDefaultAsync(x => x.Id == id);

            if (turmas == null)
            {
                throw new Exception("A turma não foi encontrada");


            }

            if (turmas.Alunos.Count > 0)
            {
                throw new Exception("Existem alunos cadastrados nessa turma. Não é possível excluir");
            }

            _context.Remove(turmas);
            await _context.SaveChangesAsync();
            return true;


        }


        public async Task<List<Turma>> Listar()
        {
            return await _context.Turmas.Include(x => x.Alunos).AsNoTracking().ToListAsync();
        }
    }




}
