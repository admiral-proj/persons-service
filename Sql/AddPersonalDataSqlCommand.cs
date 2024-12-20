using Microsoft.Data.SqlClient;
using PersonsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Sql
{
    /// <summary>
    /// Создаёт запрос и параметры для вызова хранимой процедуры
    /// </summary>
    public class AddPersonalDataSqlCommand
    {
        public AddPersonalDataSqlCommand(PersonalDataModel model)
        {
            SqlString = "AddPersonProc @lastName, @firstName, @patronymic, @dateOfBirth, @id out";
            Parameters = new List<SqlParameter>()
            {
                new SqlParameter("@lastName", model.LastName),
                new SqlParameter("@firstName", model.FirstName),
                new SqlParameter("@patronymic", model.Patronymic),
                new SqlParameter("@dateOfBirth", model.DateOfBirth),
                new SqlParameter("@id", SqlDbType.Int, 0, ParameterDirection.Output, false, 0,0, null, DataRowVersion.Current, null)
            };
        }

        public string SqlString { get; private set; }

        public IEnumerable<SqlParameter> Parameters { get; private set; }
    }
}
