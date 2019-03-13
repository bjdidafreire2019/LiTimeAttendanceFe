using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;
using BjDiDaSoft.Core.Application.UniversalConnector.Core;
using BjDiDaSoft.Core.Application.UniversalConnector.Servers;

using System;
using System.Collections.Generic;
using System.Configuration; 
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DAOs
{
    public class SpendingCenterDAO
    {
        #region Connection

        //string connectionString = "Data Source=LFREIRER\\SQLEXPRESS;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210;";
        //string connectionString = "Data Source=LFREIRE-PC;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210*;";
        string connectionString = ConfigurationManager.ConnectionStrings["TAConnection"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionTA"].ToString();

        #endregion

        #region SpendingCenter

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveSpendingCenter(SpendingCenterDTO spendingCenter, string operation)
        {
            DataTable dataTable = new DataTable();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            MessageDTO message = new MessageDTO();

            try
            {
                connector.BeginTransaction();

                //Todos los registros
                object[] parameters = new object[] { spendingCenter.SpendingCenterId, spendingCenter.Company.CompanyId, 
                                                     spendingCenter.User.UserId, "", spendingCenter.SpendingCenterDescription, 
                                                     spendingCenter.SpendingCenterStatus, operation };
                //connector.ExecuteNonQuery(CommandType.StoredProcedure, "SaveSpendingCenter", parameters);
                dataTable = connector.ExecuteDataTable(CommandType.StoredProcedure, "SaveSpendingCenter", parameters);

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
        public List<SpendingCenterDTO> GetSpendingCenters(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var spendingCenters = new List<SpendingCenterDTO>();

            int companyId = Convert.ToInt32(whereClause["companyId"]);
            int pageSize = Convert.ToInt32(whereClause["pageSize"]);
            int pageNumber = Convert.ToInt32(whereClause["pageNumber"]);
            string sortColumn = whereClause["sortColumn"].ToString();
            string sortOrder = whereClause["sortOrder"].ToString();


            try
            {
                //Todos los registros
                object[] parameters = new object[] { companyId, pageSize, pageNumber, sortColumn, sortOrder };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetSpendingCenter", parameters);

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    dataTable = dataSet.Tables[0];

                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        SpendingCenterDTO spendingCenter = new SpendingCenterDTO
                        {
                            Company = new CompanyDTO { CompanyId = Convert.ToInt32(row["COMPANY_ID"]) },
                            SpendingCenterDescription = row["SPENDING_CENTER_DESCRIPTION"].ToString(),
                            SpendingCenterId = Convert.ToInt32(row["SPENDING_CENTER_ID"]),
                            SpendingCenterStatus = row["SPENDING_CENTER_STATUS"].ToString()
                        };
                        spendingCenters.Add(spendingCenter);
                    }
                }
                else
                {
                    spendingCenters = null;
                }

            }
            catch (Exception exception)
            {
                spendingCenters = null;
            }
            finally
            {
                connector.Dispose();
            }
            return spendingCenters;
        }

        #endregion
    }
}
