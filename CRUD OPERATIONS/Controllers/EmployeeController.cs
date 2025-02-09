﻿using CRUD_OPERATIONS.DataAccessLayer;
using CRUD_OPERATIONS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_OPERATIONS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_Dal _Dal;
        public EmployeeController(Employee_Dal dal)
        {
            _Dal = dal;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _Dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                }
                bool result = _Dal.Insert(model);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save data";
                    return View();
                }
                TempData["successMessage"] = "Employee data has saved";
                return RedirectToAction("Index");
            }


            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Employee employee = _Dal.GetByIdl(id);
                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Employee details not found with Id : {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                    return View();
                }
                bool result = _Dal.Update(model);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to Update data";
                    return View();
                }
                TempData["successMessage"] = "Employee data has Updated";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Employee employee = _Dal.GetByIdl(id);
                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Employee details not found with Id : {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Employee model)
        {
            try
            {
               
                bool result = _Dal.Delete(model.Id);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to Delete data";
                    return View();
                }
                TempData["successMessage"] = "Employee data has Deleted";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }
    }
}
