using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Data
{
    public class PatronymicEntity
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Patronymic { get; set; }

        public IEnumerable<PersonalDataEntity> PersonalData { get; set; }
    }
}
