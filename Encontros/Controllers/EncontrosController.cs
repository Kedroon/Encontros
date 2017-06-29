using Encontros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Encontros.ViewModels;

namespace Encontros.Controllers
{
    public class EncontrosController : Controller
    {
        private ApplicationDbContext _context;
        public EncontrosController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var encontros = _context.Encontros.Include(l => l.Local).ToList();
            return View(encontros);
        }

        public ActionResult New()
        {
            var encontro = new EncontroFormViewModel() { Id=0};

            return View("EncontroForm", encontro);
        }

        [HttpPost]
        public ActionResult Save(Encontro encontro)
        {
            if (!ModelState.IsValid)
            {
                return View("EncontroForm", encontro);
            }
            if (encontro.Id == 0)
                _context.Encontros.Add(encontro);
            _context.SaveChanges();

            return RedirectToAction("Index", "Encontros");
        }

        public ActionResult Details(int id)
        {
            var encontro = _context.Encontros.Include(c => c.Local).SingleOrDefault(c => c.Id == id);

            if (encontro == null)
                return HttpNotFound();

            return View(encontro);
        }
    }
}