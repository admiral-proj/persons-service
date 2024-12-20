using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonsWebApi.Models;

namespace PersonsWebApi.Services
{
    public interface IPersonsService
    {
        int Create(PersonalDataModel model);

        PersonModel[] Read(PersonalDataModel model);

        PersonModel Find(int id);
    }
}
