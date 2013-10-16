using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace CadastroAluno.Domain.Mappings
{
    public class MateriaMap : ClassMap<Materia>
    {
        public MateriaMap()
        {
            Table("Materia");

            Id(x => x.ID)
                .GeneratedBy.Identity();

            Map(x => x.Descricao)
               .Column("descricao")
               .Not.Nullable();

        }
    }
}
