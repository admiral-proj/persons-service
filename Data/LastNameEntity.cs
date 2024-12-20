using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Data
{
    public class LastNameEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        public IEnumerable<PersonalDataEntity> PersonalData { get; set; }
    }
}
