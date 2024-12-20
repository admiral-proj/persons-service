using PersonsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Exceptions
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException() : base("Лицо с указанными данными не найдено.") { }
    }
}
