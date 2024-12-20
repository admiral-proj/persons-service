using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Data
{
    public class PersonsView
    {
        public int Id { get; set; }

        public string PersonGuid { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
