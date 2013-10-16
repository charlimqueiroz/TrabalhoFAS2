using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace CadastroAluno.Domain.Mappings
{
    public class AlunoMap : ClassMap<Aluno>
    {
        public AlunoMap()
        {
            Table("Aluno");

            Id(x => x.ID)
                .GeneratedBy.Identity();

            Map(x => x.Nome)
               .Column("nome")
               .Not.Nullable();

            Map(x => x.Email)
               .Column("email")
               .Not.Nullable();

           Map(x => x.Telefone)
               .Column("telefone")
               .Nullable();

            Map(x => x.Ativo)
               .Column("ativo")
               .Not.Nullable();

            HasManyToMany<Materia>(x => x.Materias)
              .Table("MateriaAluno")
              .Not.LazyLoad()
              .ParentKeyColumn("id_aluno")
              .ChildKeyColumn("id_materia");

        }
    }
}
