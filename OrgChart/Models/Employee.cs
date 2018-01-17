using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrgChart.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string WorkLocation { get; set; }
        public string DepartmentName { get; set; }
        public int Reporties { get; set; }

    }

}