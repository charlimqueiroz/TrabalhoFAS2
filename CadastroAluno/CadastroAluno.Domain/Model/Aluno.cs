using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroAluno.Domain
{
    public class Aluno : EntityBase<Aluno>
    {
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime DataNascimento { get; set; }
        public virtual string Telefone { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual IList<Materia> Materias { get; set; }
    }
}

