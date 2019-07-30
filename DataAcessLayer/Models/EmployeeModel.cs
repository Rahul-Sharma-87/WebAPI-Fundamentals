using System.Linq;
using DataAcessLayer;
using DataAcessLayer.Models;

namespace DataAccessLayer.Models {
    public interface IEmployeesModel {
        IQueryable<Employee_Master> GetAllEmployees();
        Employee_Master GetEmployeeById(int id);
        void CreateEmployee(Employee_Master employee);
        void UpdateEmployee(int empId, Employee_Master employee);
        void DeleteEmployee(int empId);
    }

    public class EmployeeModel:BaseContextProvider, IEmployeesModel {
        public EmployeeModel(PracticeSQLEntities employeeContext) : base(employeeContext) {
        }

        public IQueryable<Employee_Master> GetAllEmployees() {
            return DbContext.Employee_Master;
        }

        public Employee_Master GetEmployeeById(int id) {
            return DbContext.Employee_Master.FirstOrDefault(x => x.EmployeeId == id);
        }

        public void CreateEmployee(Employee_Master employee) {
            DbContext.Employee_Master.Add(employee);
            DbContext.SaveChanges();
        }

        public void UpdateEmployee(int empId, Employee_Master employee) {
            var employeeObj = 
                DbContext.Employee_Master.FirstOrDefault(x => x.EmployeeId == empId);
            if (employeeObj != null) {
                employeeObj.EmployeeDOJ = employee.EmployeeDOJ;
                employeeObj.EmployeeDepartment = employee.EmployeeDepartment;
                employeeObj.EmployeeDesignation = employee.EmployeeDesignation;
                employeeObj.EmployeeName = employee.EmployeeName;
                employeeObj.Gender = employee.Gender;
                DbContext.SaveChanges();
            }
        }

        public void DeleteEmployee(int empId) {
            var employeeObj = 
                DbContext.Employee_Master.FirstOrDefault(x => x.EmployeeId == empId);
            if (employeeObj != null) {
                DbContext.Employee_Master.Remove(employeeObj);
                DbContext.SaveChanges();
            }
        }

    }
}

