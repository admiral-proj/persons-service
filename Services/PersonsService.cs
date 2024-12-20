using PersonsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonsWebApi.Data;
using Microsoft.EntityFrameworkCore;
using PersonsWebApi.Sql;
using PersonsWebApi.Exceptions;

namespace PersonsWebApi.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly AppDbContext context;

        public PersonsService(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Отправляет данные в БД
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id добавленного лица</returns>
        public int Create(PersonalDataModel model)
        {
            AddPersonalDataSqlCommand command = new AddPersonalDataSqlCommand(model);
            context.Database.ExecuteSqlRaw(command.SqlString, command.Parameters);
            return (int)command.Parameters.FirstOrDefault(e => e.ParameterName == "@id").Value;
        }

        /// <summary>
        /// Получает из БД конкретное лицо по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Экземпляр PersonModel</returns>
        public PersonModel Find(int id)
        {
            // Находим лицо по его Id
            //PersonEntity person = context.Persons.Include(e => e.Person_2_PersonalData).ThenInclude(e => e.PersonalData).SingleOrDefault(e => e.Id == id);

            PersonsView[] persons = context.Persons_View.Where(e => e.Id == id).ToArray();

            if (persons.Length > 0)
            {
                return new PersonModel()
                {
                    Id = persons[0].Id,
                    PersonGuid = persons[0].PersonGuid,
                    PersonalData = persons.Select(e => new PersonalDataModel()
                    {
                        LastName = e.LastName,
                        FirstName = e.FirstName,
                        Patronymic = e.Patronymic,
                        DateOfBirth = e.DateOfBirth
                    }).ToArray()
                };
            }

            else throw new PersonNotFoundException();

            // Создаём экземпляр PersonModel,
            // заполняем массив PersonalData связанными с данным лицом ФИО
            //return new PersonModel()
            //{
            //    Id = person.Id,
            //    PersonGuid = person.PersonGuid,
            //    PersonalData = person.Person_2_PersonalData.Select(e => new PersonalDataModel()
            //    {
            //        LastName = e.PersonalData.LastName.LastName,
            //        FirstName = e.PersonalData.FirstName.FirstName,
            //        Patronymic = e.PersonalData.Patronymic.Patronymic,
            //        DateOfBirth = e.PersonalData.DateOfBirth.DateOfBirth
            //    }).ToArray()
            //};
        }

        /// <summary>
        /// Получает данные из БД
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Массив лиц</returns>
        public PersonModel[] Read(PersonalDataModel model)
        {
            // Получаем ФИО лица из БД.
            // Поскольку одни ФИО могут быть связаны с лицом не более одного раза,
            // то каждая связь из массива Person2PersonalData соответствует одному лицу
            PersonalDataEntity personalData = context.PersonalData.AsNoTracking()
                .Include(e => e.LastName).Include(e => e.FirstName).Include(e => e.Patronymic).Include(e => e.DateOfBirth)
                .Include(e => e.Person2PersonalData).ThenInclude(e => e.Person)
                .SingleOrDefault(e =>
                e.LastName.LastName == model.LastName
                && e.FirstName.FirstName == model.FirstName
                && e.Patronymic.Patronymic == model.Patronymic
                && e.DateOfBirth.DateOfBirth == model.DateOfBirth);

            if (personalData == null) throw new PersonNotFoundException();

            // Поскольку каждая связь соответствует одному лицу,
            // Перебираем все связи и из каждой создаём экземпляр PersonModel,
            // Для каждого экземпляра PersonModel получаем все связанные с ним ФИО и заполняем ими массив PersonalData
            return personalData.Person2PersonalData.Select(e => new PersonModel()
                {
                    Id = e.Person.Id,
                    PersonGuid = e.Person.PersonGuid,
                    PersonalData = e.Person.Person_2_PersonalData.Select(p => new PersonalDataModel()
                        {
                            LastName = p.PersonalData.LastName.LastName,
                            FirstName = p.PersonalData.FirstName.FirstName,
                            Patronymic = p.PersonalData.Patronymic.Patronymic,
                            DateOfBirth = p.PersonalData.DateOfBirth.DateOfBirth
                        }).ToArray()
                }
                ).ToArray();
        }
    }
}
