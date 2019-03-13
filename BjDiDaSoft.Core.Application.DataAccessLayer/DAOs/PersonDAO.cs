using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;
using BjDiDaSoft.Core.Application.UniversalConnector.Core;
using BjDiDaSoft.Core.Application.UniversalConnector.Servers;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DAOs
{
    public class PersonDAO
    {
        #region Connection

        //string connectionString = "Data Source=LFREIRER\\SQLEXPRESS;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210;";
        //string connectionString = "Data Source=LFREIRE-PC;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210*;";
        string connectionString = ConfigurationManager.ConnectionStrings["TAConnection"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionTA"].ToString();

        #endregion

        #region Employee

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="operation"></param>
        public void SavePerson(PersonDTO person, string operation)
        {
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);

            try
            {
                //Todos los registros
                object[] parameters = new object[] { 1, person.PersonId, person.IdentificationNumber, person.PersonName, 
                                                     person.PersonBirthDate, person.PersonWeight, person.PersonStatus,
                                                     operation };
                connector.ExecuteNonQuery(CommandType.StoredProcedure, "SavePerson", parameters);

            }
            catch (Exception exception)
            {

            }
            finally
            {
                connector.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<PersonDTO> GetPersons(int pageSize, int pageNumber, string sortColumn, string sortOrder, ref DataTable dataTable)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var persons = new List<PersonDTO>();

            try
            {
                //Todos los registros
                object[] parameters = new object[] { pageSize, pageNumber, sortColumn, sortOrder };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetPersons", parameters);

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    dataTable = dataSet.Tables[0];

                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        PersonDTO person = new PersonDTO
                        {
                            PersonBirthDate = Convert.ToDateTime(row["PERSON_BIRTH_DATE"]),
                            PersonId = Convert.ToInt32(row["PERSON_ID"]),
                            PersonName = row["PERSON_NAME"].ToString(),
                            PersonStatus = row["PERSON_STATUS"].ToString(),
                            PersonWeight = Convert.ToDecimal(row["PERSON_WEIGHT"].ToString()),
                            IdentificationNumber = row["PERSON_INUMBER"].ToString()
                        };
                        persons.Add(person);
                    }
                }
                else
                {
                    persons = null;
                }

            }
            catch (Exception exception)
            {
                persons = null;
            }
            finally
            {
                connector.Dispose();
            }
            return persons;
        }

        #endregion

    }
}
