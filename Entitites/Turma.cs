using System.Text.Json.Serialization;

namespace CursoIdiomasAPI.Entitites
{
    public class Turma
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nivel { get; set; }



        [JsonIgnore]
        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

        
        

    }
}
