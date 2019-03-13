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
    public class ContractTypeDAO
    {
        #region Connection

        //string connectionString = "Data Source=LFREIRER\\SQLEXPRESS;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210;";
        //string connectionString = "Data Source=LFREIRE-PC;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210*;";
        string connectionString = ConfigurationManager.ConnectionStrings["TAConnection"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionTA"].ToString();

        #endregion

        #region ContractType

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractType"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveContractType(ContractTypeDTO contractType, string operation)
        {
            DataTable dataTable = new DataTable();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            MessageDTO message = new MessageDTO();

            try
            {
                connector.BeginTransaction();

                //Todos los registros
                object[] parameters = new object[] { contractType.Company.CompanyId, contractType.ContractTypeId,  
                                                     contractType.User.UserId, contractType.ContractTypeDescription, 
                                                     contractType.ContractTypeStatus, operation };
                //connector.ExecuteNonQuery(CommandType.StoredProcedure, "SaveCharge", parameters);
                dataTable = connector.ExecuteDataTable(CommandType.StoredProcedure, "SaveContractType", parameters);

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
        public List<ContractTypeDTO> GetContractTypes(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var contractTypes = new List<ContractTypeDTO>();

            int companyId = Convert.ToInt32(whereClause["companyId"]);
            int pageSize = Convert.ToInt32(whereClause["pageSize"]);
            int pageNumber = Convert.ToInt32(whereClause["pageNumber"]);
            string sortColumn = whereClause["sortColumn"].ToString();
            string sortOrder = whereClause["sortOrder"].ToString();


            try
            {
                //Todos los registros
                object[] parameters = new object[] { companyId, pageSize, pageNumber, sortColumn, sortOrder };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetContractTypes", parameters);

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    dataTable = dataSet.Tables[0];

                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        ContractTypeDTO contractType = new ContractTypeDTO
                        {
                            Company = new CompanyDTO { CompanyId = Convert.ToInt32(row["COMPANY_ID"]) },
                            ContractTypeDescription = row["CONTRACT_TYPE_DESCRIPTION"].ToString(),
                            ContractTypeId = Convert.ToInt32(row["CONTRACT_TYPE_ID"]),
                            ContractTypeStatus = row["CONTRACT_TYPE_STATUS"].ToString()
                        };
                        contractTypes.Add(contractType);
                    }
                }
                else
                {
                    contractTypes = null;
                }

            }
            catch (Exception exception)
            {
                contractTypes = null;
            }
            finally
            {
                connector.Dispose();
            }
            return contractTypes;
        }

        #endregion

    }
}
