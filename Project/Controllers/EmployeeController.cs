using Project.Models;
using Project.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class EmployeeController : Controller
    {
        EmpRepository EmpRepo = new EmpRepository();
        // GET: Employee
        public ActionResult GetAllEmpDetails()
        {
            EmpRepository EmpRepo = new EmpRepository();
            ModelState.Clear();
            return View(EmpRepo.GetAllEmployees());
        }
        // GET: Employee/AddEmployee
        public ActionResult AddEmployee()
        {
            return View();
        }
        // POST: Employee/AddEmployee 
        [HttpPost]
        public  ActionResult AddEmployee(EmpModel Emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpRepository EmpRepo = new EmpRepository();
                    if (EmpRepo.AddEmployee(Emp))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        // GET: Employee/EditEmpDetails/5   
        public ActionResult EditEmpDetails(int Id)
        {
            EmpRepository EmpRepo = new EmpRepository();
            return View(EmpRepo.GetAllEmployees().Find(Emp => Emp.EmpId == Id));
        }

        // POST: Employee/EditEmpDetails/5 
        public ActionResult EditEmpDetails(int Id,EmpModel Emp)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();
                EmpRepo.UpdateEmployee(Emp);
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();
            }
        }
        // GET: Employee/DeleteEmp/5 
        public ActionResult DeleteEmp(int id)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();
                if (EmpRepo.DeleteEmployee(id))
                {
                    ViewBag.AlertMsg = "Employee details deleted successfully";
                }
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EmpDetail(int Id)
        {
            EmpModel empModel = new EmpModel();
            empModel = EmpRepo.getdetail(Id);
            return View(empModel);
        }
       
        }
    }