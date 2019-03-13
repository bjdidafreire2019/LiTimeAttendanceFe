using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;

namespace BjDiDaSoft.Core.Application.TimeAttendance.GridSettings
{
    /// <summary>
    /// Descripción breve de JQGridJsonResponse
    /// </summary>
    public class JQGridJsonResponse
    {
        #region Passive attributes.

        private int _pageCount;
        private int _currentPage;
        private int _recordCount;
        private List<JQGridItem> _items;

        #endregion

        #region Properties

        /// <summary>
        /// Cantidad de páginas del JQGrid.
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }
        /// <summary>
        /// Página actual del JQGrid.
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; }
        }
        /// <summary>
        /// Cantidad total de elementos de la lista.
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
            set { _recordCount = value; }
        }
        /// <summary>
        /// Lista de elementos del JQGrid.
        /// </summary>
        public List<JQGridItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        #endregion

        #region Active attributes

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<object> listObject)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();

            List<EmployeeDTO> employees = (List<EmployeeDTO>)listObject.ToArray().GetValue(0);

            foreach (EmployeeDTO employee in employees)
            {
                _items.Add(new JQGridItem(employee.EmployeeId,
                                          new List<string> { employee.EmployeeId.ToString(), employee.IdentificationNumber, 
                                                             employee.EmployeeName, employee.EmployeeBirthDate.ToShortDateString(), 
                                                             employee.EmployeeWeight.ToString(), employee.EmployeeStatus }));
            }

        }

        #endregion
    }
}