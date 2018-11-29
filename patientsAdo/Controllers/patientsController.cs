using patientsAdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace patientsAdo.Controllers
{
    public class patientsController : Controller
    {
        // GET: patients
        patientsCrud pcrud = new patientsCrud();

        public ActionResult Index()
        {
            return View(pcrud.getAll());
        }

        // GET: patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: patients/Create
        [HttpPost]
        public ActionResult Create(patientsModel patient)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                if (pcrud.Add(patient))
                {
                    return RedirectToAction("Index");
                }


            }

            return View();
        }

        // GET: patients/Edit/5
        public ActionResult Edit(int id)
        {
            patientsModel patient = pcrud.getById(id);
            return View(patient);
        }

        // POST: patients/Edit/5
        [HttpPost]
        public ActionResult Edit(patientsModel patient)
        {
            if (pcrud.update(patient))
            {
                return RedirectToAction("Index");
            }

            return View();

        }

        // GET: patients/Delete/5
        public ActionResult Delete(int id)
        {
            var patient = pcrud.getById(id);
            return View(patient);
        }

        // POST: patients/Delete/5
        [HttpPost]
        public ActionResult Delete(patientsModel patient)
        {
           
                // TODO: Add delete logic here
                if(pcrud.delete(patient.ID))
                {
                    return RedirectToAction("Index");
                }
                
            return View();

        }
    }
}
