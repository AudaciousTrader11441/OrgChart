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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}