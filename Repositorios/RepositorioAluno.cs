using CursoIdiomasAPI.Data;
using CursoIdiomasAPI.Entitites;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomasAPI.Repositorios
{
    public class RepositorioAluno : IAlunoRepositorio
    {

        private readonly IdiomasDB _context; // Injeção de dependencia do banco de dados 



        public RepositorioAluno(IdiomasDB context)
        {
            _context = context;

        }

        public async Task<List<Aluno>> Listar()
        {
            return await _context.Alunos.Include(x => x.Turmas).ToListAsync(); // LISTAR TODOS OS ALUNOS E MOSTRAR INFORMAÇÕES DA TURMA QUE ELE ESTÁ CADASTRADO
        }


        public async Task<Aluno> Cadastrar(Aluno aluno) // AO CADASTRAR UM ALUNO, TENTAREMOS VALIDAR ALGUMAS INFORMAÇÕES
        {

            // => VAI VERIFICAR SE O CPF DO ALUNO EXISTE 

            var alunoExistente = await _context.Alunos
                .Include(a => a.Turmas)
                .FirstOrDefaultAsync(x => x.CPF == aluno.CPF);

            if (alunoExistente == null)
            {
                // => SE NÃO EXISTIR, ADICIONA 

                _context.Alunos.Add(aluno);
            }
            else
            {
                // => SE EXISTIR, VERIFICA AS TURMAS

                foreach (var turma in aluno.Turmas)
                {
                    // => BUSCANDO AS TURMAS PELO CÓDIGO
                    var turmaExistente = await _context.Turmas
                        .FirstOrDefaultAsync(t => t.Codigo == turma.Codigo);

                    if (turmaExistente != null)
                    {
                        // VERIFICANDO SE ELE TA MATRICULADO EM OUTRA TURMA

                        if (alunoExistente.Turmas.Any(t => t.Id == turmaExistente.Id))
                        {
                            // SE TIVER, LANÇA UMA EXCEÇÃO:

                            throw new Exception($"O aluno já está matriculado na turma {turma.Codigo}.");
                        }



                        // SE NÃO TIVER MATRICULADO, ADICIONA
                        alunoExistente.Turmas.Add(turmaExistente);
                    }
                    else
                    {
                        throw new Exception($"A turma com código {turma.Codigo} não existe.");
                    }
                }
            }

            await _context.SaveChangesAsync();
            return alunoExistente ?? aluno; // RETORNA O ALUNO

        }

        public async Task<Aluno> Editar(Aluno aluno)
        {

            var alunoExistente = await _context.Alunos
                .Include(a => a.Turmas)
                .FirstOrDefaultAsync(a => a.Id == aluno.Id);

            if (alunoExistente == null)
            {
                throw new Exception("Aluno não encontrado.");
            }

            // Verifica o CPF 

            var alunoComCpfExistente = await _context.Alunos
                .Where(a => a.CPF == aluno.CPF && a.Id != aluno.Id)
                .FirstOrDefaultAsync();

            if (alunoComCpfExistente != null)
            {
                throw new Exception("Já existe um aluno cadastrado com esse CPF.");
            }

            // Atualiza os dados 

            alunoExistente.Nome = aluno.Nome;
            alunoExistente.CPF = aluno.CPF;
            alunoExistente.Email = aluno.Email;

            // Atualiza as turmas 

            alunoExistente.Turmas.Clear(); // Remove as turmas existentes

            // Adiciona  novas turmas

            var turmasExistentes = await _context.Turmas
                .Where(t => aluno.Turmas.Select(at => at.Id).Contains(t.Id))
                .ToListAsync();

            alunoExistente.Turmas.AddRange(turmasExistentes);

            try
            {
                await _context.SaveChangesAsync();
                return alunoExistente;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Erro ao atualizar o aluno.", ex.InnerException);
            }



        }



        public async Task<bool> Deletar(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);


            if (aluno == null)
            {
                throw new Exception("O aluno não foi encontrado!");
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<Aluno> BuscarporId(int id)
        {

            var aluno = await _context.Alunos
                .Include(a => a.Turmas)
                .FirstOrDefaultAsync(a => a.Id == id);


            return aluno;
        }
    }
}
