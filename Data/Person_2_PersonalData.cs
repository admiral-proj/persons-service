using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Data
{
    public class Person_2_PersonalData
    {
        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int PersonalDataId { get; set; }

        public PersonEntity Person { get; set; }

        public PersonalDataEntity PersonalData { get; set; }
    }
}
