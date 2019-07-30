using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DataAccessLayer.Models;
using DataAcessLayer;

namespace WebAPIDemo.Controllers {

    //Cors enabled for GET verb only
    [EnableCors("*","*","GET")]
    public class EmployeesController : ApiController {

        private IEmployeesModel _employeesRepo;

        public EmployeesController(IEmployeesModel employeesModel) {
            _employeesRepo = employeesModel;
        }

        [BasicAuthentication]
        [HttpGet]
        public HttpResponseMessage GetAllEmployees(
            [FromUri]string gender = "all", // to show usage of query string
            [FromUri]string department = "all"//Also shows parameter attribute
        ) {
            IQueryable<Employee_Master> employees = _employeesRepo.GetAllEmployees();
            if (!gender.Equals("all")) {
                employees = 
                    employees.Where(
                        x => x.Gender.Equals(gender, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!department.Equals("all")) {
                employees = employees.Where(
                        x => x.EmployeeDepartment.Equals(department, StringComparison.InvariantCultureIgnoreCase));
            }
            return Request.CreateResponse(HttpStatusCode.OK, employees.ToList());
        }

        [HttpGet]
        [Route("{id:int}",Name = "GetEmployeeById")] 
        public HttpResponseMessage FindEmployeeById(int id) {
            try {
                var employee = _employeesRepo.GetEmployeeById(id);
                if (employee != null) {
                    return Request.CreateResponse(
                        HttpStatusCode.OK,  _employeesRepo.GetEmployeeById(id));
                }
            } catch (Exception) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,$"No data found with Id: {id}");
        }

        /// <summary>
        /// Examplifies route usage where default route becomes cumbersome.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/employees/{id}/skills")]
        public IEnumerable<string> GetEmployeeSkills(int id) {
            if (id == 1) {
                return new List<string>{"C#","Asp.Net"};
            }
            if (id == 2) {
                return new List<string>{"C#","Asp.Net core"};
            }
            return new List<string>();
        }

        [RequireHttps]
        public HttpResponseMessage Post([FromBody]Employee_Master employee) {
            try {
                _employeesRepo.CreateEmployee(employee);
            } catch (Exception) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            var message = Request.CreateResponse(HttpStatusCode.OK,employee);
            // This will show the url for the route set - for FindEmployeeById
            message.Headers.Location = new Uri(Url.Link("GetEmployeeById",new {id=employee.EmployeeId}));
            return message;
        }
        
        [RequireHttps]
        public HttpResponseMessage Put(int id, [FromBody]Employee_Master employee) {
            try {
                _employeesRepo.UpdateEmployee(id, employee);
            } catch (Exception) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [RequireHttps]
        [DisableCors]
        public HttpResponseMessage Delete(int id) {
            try {
                _employeesRepo.DeleteEmployee(id);
            } catch (Exception) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}