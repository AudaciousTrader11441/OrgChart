﻿using System;
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
        List<Employee> emp = new List<Employee>();
        
        public ActionResult search(string searchTerm = "sri")
        {
            List<Employee> Employee = SqlDb.GetAllEmployee();
            var lable = Employee.Where(r=>r.Name.ToLower().Contains(searchTerm.ToLower())).Select(r => new EmployeeSearch { Name = r.Name,Id=r.Id });
            //return Json(lable, JsonRequestBehavior.AllowGet);
            return PartialView("_Search", lable);
        }

        public ActionResult search1(string searchTerm = "giri")
        {
            //List<Employee> Employee = SqlDb.GetAllEmployee();
            //var lable = Employee.Where(r => r.Name.ToLower().Contains(searchTerm.ToLower())).Select(r => new EmployeeSearch { Name = r.Name, Id = r.Id });
            ////return Json(lable, JsonRequestBehavior.AllowGet);
            searchTerm=searchTerm.Replace(" ", "%");
            List<EmployeeSearch> res =SqlDb.SearchEmployee(searchTerm);
            if(res[0].Id==0)
            {
                string html = "No Employee with " + searchTerm;
                return Content(html);
            }
            return PartialView("_Search",res);
        }

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult GetSubEmployee(int id= 11358)
        {
            List<Employee> child = SqlDb.GetListSubordinates(id);
            var children = new { children = child };
           //string s= JsonConvert.SerializeObject(child);
            return Json(children, JsonRequestBehavior.AllowGet);
            //return PartialView("_subEmployee", child);

        }
        public JsonResult GetManager(int id = 11358)
        {
            Employee root = SqlDb.GetManagerDetails(id);
            //string s = JsonConvert.SerializeObject(root);
            return Json(root, JsonRequestBehavior.AllowGet);
            //return PartialView("_subEmployee", child);

        }

        public ActionResult GetSibling(int id = 11358)
        {
            List<Employee> child = SqlDb.GetSiblings(id);
            //string s = JsonConvert.SerializeObject(child);
            var siblings = new { siblings = child };
            return Json(siblings, JsonRequestBehavior.AllowGet);
            //return PartialView("_subEmployee", child);

        }

        public JsonResult Init(int id= 11358)    
        {
            //Employee root = SqlDb.GetInit();
            //string s = JsonConvert.SerializeObject(root);
            Employee root = SqlDb.GetEmployeeDetails(id);
            List<Employee> child = SqlDb.GetListSubordinates(id);
            var family = new
            {
                Id = root.Id,
                Name = root.Name,
                Role = root.Role,
                WorkLocation = root.WorkLocation,
                DepartmentName = root.DepartmentName,
                Reporties = root.Reporties,
                relationship = root.relationship,
                children = child
            };


            return Json(family, JsonRequestBehavior.AllowGet);
            //return PartialView("_subEmployee", child);

        }

        public JsonResult GetFamily(int id= 11358)
        {
            Employee root = SqlDb.GetManagerDetails(id);
            List<Employee> child = SqlDb.GetSiblings(id);
            var family = new
            { Id=root.Id,
            Name=root.Name, 
            Role=root.Role, 
            WorkLocation=root.WorkLocation, 
            DepartmentName=root.DepartmentName, 
            Reporties=root.Reporties, 
            relationship=root.relationship,
            children = child
            };
            //string s = JsonConvert.SerializeObject(root);
            return Json(family, JsonRequestBehavior.AllowGet);
            //return PartialView("_subEmployee", child);

        }

        public JsonResult Autocomplet(string term="sri")
        {
            term = term.Replace(" ", "%");
            List<EmployeeSearch> res = SqlDb.SearchEmployee(term);
            var autosuggest = res.Take(10).Select(r => new { label = r.Name, value = r.Id });
            
            return Json(autosuggest, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Tree(int id= 10000)
        {
            List<EmployeeTree> child = SqlDb.GetTreeChild(id);
            return PartialView("_Tree", child);
        }


    }
}