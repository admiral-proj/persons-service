using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Models
{
    public class PersonModel
    {
        public int Id { get; set; }

        public string PersonGuid { get; set; }

        public PersonalDataModel[] PersonalData { get; set; }
    }
}
