using CursoIdiomasAPI.Entitites;

namespace CursoIdiomasAPI.Repositorios
{
    public interface ITurmaRepositorio // INTERFACE DE TURMA QUE IREMOS IMPLEMENTAR NA CONTROLLER 
    {
        Task<Turma> Cadastrar(Turma turma);
        Task<List<Turma>> Listar();
        Task<bool> Deletar(int id);
    }
}
