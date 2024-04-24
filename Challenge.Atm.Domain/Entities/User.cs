using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Domain.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Rol { get; set; }

        public int CardId { get; set; }
    }
}
