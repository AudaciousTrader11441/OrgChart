using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using OrgChart.Models;

namespace OrgChart.Controllers
{
    
    public class HomeController : Controller
    {


        

        public ActionResult Index()
        {

            Employee root=SqlDb.GetEmployeeDetails(11285);
            List<Employee> child = SqlDb.GetListSubordinates(11285);
            ViewBag.root = root;
            ViewBag.child = child;


            return View();
        }

        public ActionResult GetSubEmployee(int id= 10866)
        {
            Employee root = SqlDb.GetEmployeeDetails(id);
            List<Employee> child = SqlDb.GetListSubordinates(id);
            ViewBag.root = root;
            ViewBag.child = child;
            return PartialView("_subEmployee", child);

        }


    }
}