using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Data
{
    public class PersonEntity
    {
        public int Id { get; set; }

        [Required]
        public string PersonGuid { get; set; }

        public IEnumerable<Person_2_PersonalData> Person_2_PersonalData { get; set; }

    }
}
