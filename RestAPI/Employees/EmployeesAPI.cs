using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chetch.RestAPI.Network;

namespace Chetch.RestAPI.Employees
{
    public class EmployeesAPI : APIService
    {
        public class Employee : DataObject
        {

        }

        public class Employees : DataObjectCollection<Employee>
        {

        }

        public const String EMPLOYEES_SERVICE_NAME = "Employees";

        static public EmployeesAPI Create()
        {
            return NetworkAPI.CreateAPIService<EmployeesAPI>(EMPLOYEES_SERVICE_NAME);
        }

        public EmployeesAPI() : base() { }

        public EmployeesAPI(String baseURL) : base(baseURL)
        {

        }
    }
}
