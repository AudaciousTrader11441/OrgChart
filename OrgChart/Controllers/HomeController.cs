using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
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
            List<Employee> child = SqlDb.GetListSubordinates(id);
           string s= JsonConvert.SerializeObject(child);
            return Json(s, JsonRequestBehavior.AllowGet);
            //return PartialView("_subEmployee", child);

        }
        public ActionResult GetManager(int id = 10866)
        {
            Employee root = SqlDb.GetManagerDetails(id);
            string s = JsonConvert.SerializeObject(root);
            return Json(s, JsonRequestBehavior.AllowGet);
            //return PartialView("_subEmployee", child);

        }

        public ActionResult GetSibling(int id = 10866)
        {
            List<Employee> child = SqlDb.GetSiblings(id);
            string s = JsonConvert.SerializeObject(child);
            return Json(s, JsonRequestBehavior.AllowGet);
            //return PartialView("_subEmployee", child);

        }

        public ActionResult Init()
        {
            Employee root = SqlDb.GetInit();
            //string s = JsonConvert.SerializeObject(root);
            return Json(root, JsonRequestBehavior.AllowGet);
            //return PartialView("_subEmployee", child);

        }




    }
}