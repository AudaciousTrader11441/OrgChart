using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrgChart.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Employee
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonIgnore]
        public string Role { get; set; }
        [JsonIgnore]
        public string WorkLocation { get; set; }
        [JsonIgnore]
        public string DepartmentName { get; set; }
        
        [JsonProperty]
        public int Reporties { get; set; }
        [JsonProperty]
        public string relationship { get; set; }

    }

    public class EmployeeSearch
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class EmployeeTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Reporties { get; set; }

    }

}