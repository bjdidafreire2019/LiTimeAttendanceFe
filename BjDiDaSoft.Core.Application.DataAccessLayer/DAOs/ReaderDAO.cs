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
    public class ReaderDAO
    {
        #region Connection

        //string connectionString = "Data Source=LFREIRER\\SQLEXPRESS;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210;";
        //string connectionString = "Data Source=LFREIRE-PC;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210*;";
        string connectionString = ConfigurationManager.ConnectionStrings["TAConnection"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionTA"].ToString();

        #endregion

        #region Reader

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveReader(ReaderDTO reader, string operation)
        {
            DataTable dataTable = new DataTable();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            MessageDTO message = new MessageDTO();

            try
            {
                connector.BeginTransaction();

                //Todos los registros
                object[] parameters = new object[] { reader.Company.CompanyId, reader.ReaderId, reader.User.UserId.ToString(),
                                                     reader.ReaderSerialNumber, reader.ReaderName, reader.ReaderType, 
                                                     reader.ReaderStatusComm, reader.ReaderStatus, operation };
                //connector.ExecuteNonQuery(CommandType.StoredProcedure, "SaveReader", parameters);
                dataTable = connector.ExecuteDataTable(CommandType.StoredProcedure, "SaveReader", parameters);

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
        public List<ReaderDTO> GetReaders(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var readers = new List<ReaderDTO>();

            int companyId = Convert.ToInt32(whereClause["companyId"]);
            int pageSize = Convert.ToInt32(whereClause["pageSize"]);
            int pageNumber = Convert.ToInt32(whereClause["pageNumber"]);
            string sortColumn = whereClause["sortColumn"].ToString();
            string sortOrder = whereClause["sortOrder"].ToString();

            try
            {
                //Todos los registros
                object[] parameters = new object[] { companyId, pageSize, pageNumber, sortColumn, sortOrder };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetReaders", parameters);

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    dataTable = dataSet.Tables[0];

                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        ReaderDTO reader = new ReaderDTO
                        {
                            Company = new CompanyDTO { CompanyId = Convert.ToInt32(row["COMPANY_ID"]) },
                            ReaderId = Convert.ToInt32(row["READER_ID"]),
                            ReaderName = row["READER_NAME"].ToString(),
                            ReaderSerialNumber = row["READER_SERIAL_NUMBER"].ToString(),
                            ReaderStatus = row["READER_STATUS"].ToString(),
                            ReaderStatusComm = row["READER_STATUS_COMM"].ToString(),
                            ReaderType = row["READER_TYPE"].ToString()
                        };
                        readers.Add(reader);
                    }
                }
                else
                {
                    readers = null;
                }

            }
            catch (Exception exception)
            {
                readers = null;
            }
            finally
            {
                connector.Dispose();
            }
            return readers;
        }

        #endregion
    }
}
