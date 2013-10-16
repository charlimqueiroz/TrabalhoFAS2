using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CadastroAluno.Infra;

namespace CadastroAluno.Domain
{
    public class EntityBase<T> where T : class
    {
        public virtual long ID { get; set; }
        public virtual string EncryptID
        {
            get
            {
                return Security.encrypt(this.ID.ToString());
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    this.ID = long.Parse(Security.decrypt(value));
            }
        }
    }
}
