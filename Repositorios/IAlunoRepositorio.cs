using CursoIdiomasAPI.Entitites;

namespace CursoIdiomasAPI.Repositorios
{
    public interface IAlunoRepositorio // INTERFACE DE ALUNO QUE IREMOS IMPLEMENTAR NA CONTROLLER
    {
        Task<Aluno> Cadastrar(Aluno aluno);
        Task<Aluno> Editar(Aluno aluno);
        Task<List<Aluno>> Listar();
        Task<bool> Deletar(int id);

        Task<Aluno> BuscarporId(int id);
    }
}
