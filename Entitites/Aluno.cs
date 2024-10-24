using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CursoIdiomasAPI.Entitites
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string? CPF { get; set; }

        [EmailAddress(ErrorMessage = "O Email invalido")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "O campo Turmas é obrigatório")]
       
        public List<Turma> Turmas { get; set; }  = new List<Turma>();

    }
}
