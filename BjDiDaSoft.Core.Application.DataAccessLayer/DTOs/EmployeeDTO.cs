using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de Employee
    /// </summary>
    [DataContract]
    public class EmployeeDTO
    {
        /// <summary>
        /// Identificador único de empleado
        /// </summary>
        [DataMember]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Identificador único de departamento
        /// </summary>
        [DataMember]
        public DepartmentDTO Department { get; set; }

        /// <summary>
        /// Identificador único de tipo de contrato
        /// </summary>
        [DataMember]
        public ContractTypeDTO ContractType { get; set; }

        /// <summary>
        /// Identificador único de cargo
        /// </summary>
        [DataMember]
        public ChargeDTO Charge { get; set; }

        /// <summary>
        /// Identificador único de horario
        /// </summary>
        [DataMember]
        public ScheduleDTO Schedule { get; set; }

        /// <summary>
        /// Identificador único de usuario
        /// </summary>
        [DataMember]
        public UserDTO User { get; set; }

        /// <summary>
        /// Identificador único de tipo de identificación
        /// </summary>
        [DataMember]
        public IdentificationTypeDTO IdentificationType { get; set; }

        /// <summary>
        /// Identificador único de sexo
        /// </summary>
        [DataMember]
        public SexDTO Sex { get; set; }

        /// <summary>
        /// Identificador único de sector
        /// </summary>
        [DataMember]
        public int SectorId { get; set; }

        /// <summary>
        /// Número identificación de empleado
        /// </summary>
        [DataMember]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Nombres completos de empleado
        /// </summary>
        [DataMember]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Apellidos completos de empleado
        /// </summary>
        [DataMember]
        public string EmployeeLastName { get; set; }

        /// <summary>
        /// Apellidos y nombres completos de empleado
        /// </summary>
        [DataMember]
        public string EmployeeFullName { get; set; }

        /// <summary>
        /// Tipo de horario: personal (P) o por departamento (D)
        /// </summary>
        [DataMember]
        public string ScheduleType { get; set; }

        /// <summary>
        /// Código huella digital asignada al empleado
        /// </summary>
        [DataMember]
        public string EmployeeFingerPrint { get; set; }

        /// <summary>
        /// Número de tarjeta asiganda al empleado
        /// </summary>
        [DataMember]
        public string EmployeeCardNumber { get; set; }

        /// <summary>
        /// Fecha nacimiento de empleado
        /// </summary>
        [DataMember]
        public DateTime EmployeeBirthDate { get; set; }

        /// <summary>
        /// Salario del empleado
        /// </summary>
        [DataMember]
        public Decimal EmployeeSalary { get; set; }

        /// <summary>
        /// Fecha de ingreso del empleado a la empresa
        /// </summary>
        [DataMember]
        public DateTime EmployeeEntryDate { get; set; }

        /// <summary>
        /// Fecha modificación de datos del empleado
        /// </summary>
        [DataMember]
        public DateTime EmployeeModificationDate { get; set; }

        /// <summary>
        /// Fecha inicio vigencia de empleado
        /// </summary>
        [DataMember]
        public DateTime EmployeeValidityStartDate { get; set; }

        /// <summary>
        /// Fecha fin vigencia de empleado
        /// </summary>
        [DataMember]
        public DateTime EmployeeValidityEndDate { get; set; }

        /// <summary>
        /// Empleado es tomado en cuenta para cálculo de faltas injustificado
        /// </summary>
        [DataMember]
        public string EmployeeTruancy { get; set; }

        /// <summary>
        /// Empleado usa la tarjeta para el ingreso / salida: S (usa tarjeta)  N (usa huella)
        /// </summary>
        [DataMember]
        public string EmployeeUsedCard { get; set; }

        /// <summary>
        /// Dirección de residencia del empleado
        /// </summary>
        [DataMember]
        public string EmployeeStreetAddress { get; set; }

        /// <summary>
        /// Estado de empleado: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string EmployeeStatus { get; set; }
    }
}
