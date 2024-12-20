using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Models
{
    public class PersonalDataModel
    {
        [Required, MaxLength(25)]
        public string LastName { get; set; }

        [Required, MaxLength(25)]
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
