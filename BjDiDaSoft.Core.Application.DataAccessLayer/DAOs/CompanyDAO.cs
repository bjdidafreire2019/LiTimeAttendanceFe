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
    public class CompanyDAO
    {
        #region Connection

        //string connectionString = "Data Source=LFREIRER\\SQLEXPRESS;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210;";
        //string connectionString = "Data Source=LFREIRE-PC;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210*;";
        string connectionString = ConfigurationManager.ConnectionStrings["TAConnection"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionTA"].ToString();

        #endregion

        #region Charge

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveCompany(CompanyDTO company, string operation)
        {
            DataTable dataTable = new DataTable();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            MessageDTO message = new MessageDTO();

            try
            {
                connector.BeginTransaction();
 
                //Todos los registros
                object[] parameters = new object[] { company.CompanyId, company.User.UserId, company.CompanyName, 
                                                     company.CompanyShortName, company.CompanyStatus, operation };
                //connector.ExecuteNonQuery(CommandType.StoredProcedure, "SaveCompany", parameters);
                dataTable = connector.ExecuteDataTable(CommandType.StoredProcedure, "SaveCompany", parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    message.ErrorCode = row["ERROR_NUMBER"].ToString();
                    message.ErrorMessage = row["ERROR_DESCRIPTION"].ToString();
                }

                if (message.ErrorCode == "0")
                {
                    connector.Commit();
                }
                else
                {
                    connector.RollBack();
                }
            }
            catch (Exception exception)
            {
                message.ErrorCode = "-1";
                message.ErrorMessage = exception.ToString();
                connector.RollBack(); 
            }
            finally
            {
                connector.Dispose();
            }
            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<CompanyDTO> GetCompanies(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var companies = new List<CompanyDTO>();

            int pageSize = Convert.ToInt32(whereClause["pageSize"]);
            int pageNumber = Convert.ToInt32(whereClause["pageNumber"]);
            string sortColumn = whereClause["sortColumn"].ToString();
            string sortOrder = whereClause["sortOrder"].ToString();


            try
            {
                //Todos los registros
                object[] parameters = new object[] { pageSize, pageNumber, sortColumn, sortOrder };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetCompanies", parameters);

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    dataTable = dataSet.Tables[0];

                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        CompanyDTO company = new CompanyDTO
                        {
                            CompanyId = Convert.ToInt32(row["COMPANY_ID"]),
                            CompanyName = row["COMPANY_NAME"].ToString(),
                            CompanyShortName = row["COMPANY_SHORT_NAME"].ToString(),
                            CompanyStatus = row["COMPANY_STATUS"].ToString()
                        };
                        companies.Add(company);
                    }
                }
                else
                {
                    companies = null;
                }

            }
            catch (Exception exception)
            {
                companies = null;
            }
            finally
            {
                connector.Dispose();
            }
            return companies;
        }

        #endregion
    }
}
