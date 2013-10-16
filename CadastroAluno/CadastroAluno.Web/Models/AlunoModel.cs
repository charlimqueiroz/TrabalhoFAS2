using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CadastroAluno.Domain;

namespace CadastroAluno.Web.Models
{
    public class AlunoModel
    {
        public IList<Aluno> Alunos { get; set; }
        public IList<Materia> Materias { get; set; }
        public IList<Materia> MateriasSelecionadas { get; set; }

        public Aluno Aluno { get; set; }
        
        public AlunoModel()
        {
            Alunos = new List<Aluno>();
            Materias = new List<Materia>();
        }
    }
}