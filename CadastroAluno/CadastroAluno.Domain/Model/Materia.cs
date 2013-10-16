using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroAluno.Domain
{
    public class Materia : EntityBase<Materia>
    {
        public virtual string Descricao { get; set; }
    }
}
