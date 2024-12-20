using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Data
{
    public class PersonalDataEntity
    {
        public int Id { get; set; }

        [Required]
        public int LastNameId { get; set; }

        [Required]
        public int FirstNameId { get; set; }

        [Required]
        public int PatronymicId { get; set; }

        [Required]
        public int DateOfBirthId { get; set; }

        public LastNameEntity LastName { get; set; }

        public FirstNameEntity FirstName { get; set; }

        public PatronymicEntity Patronymic { get; set; }

        public DateOfBirthEntity DateOfBirth { get; set; }

        public ICollection<Person_2_PersonalData> Person2PersonalData { get; set; }
    }
}
