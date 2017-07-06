using Encontros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Encontros.ViewModels;
using System.IO;

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
            var encontro = new EncontroFormViewModel();

            return View("EncontroForm", encontro);
        }

        [HttpPost]
        public ActionResult Save(Encontro encontro)
        {
            if (!ModelState.IsValid)
            {
                return View("EncontroForm", new EncontroFormViewModel(encontro));
            }
            if (encontro.Id == 0)
                _context.Encontros.Add(encontro);
            _context.SaveChanges();

            return RedirectToAction("Index", "Encontros");
        }
        
        public ActionResult Edit(int id)
        {
            var encontro = _context.Encontros.Include(l => l.Local).SingleOrDefault(e => e.Id == id);
            EncontroFormViewModel viewModel = new EncontroFormViewModel()
            {
                NomeEncontro = encontro.NomeEncontro,
                DataDoEncontro = encontro.DataDoEncontro,
                Id = encontro.Id,
                LocalId = encontro.LocalId,
                LocalNome = encontro.Local.Nome
            };
            return View("EncontroForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            var encontro = _context.Encontros.Include(c => c.Local).SingleOrDefault(c => c.Id == id);

            if (encontro == null)
                return HttpNotFound();

            return View(encontro);
        }

        [HttpPost]
        public ContentResult UploadFiles()
        {
            var r = new List<FotoEncontro>();

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName); // Save the file

                r.Add(new FotoEncontro()
                {
                    Name = hpf.FileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType
                });
            }
            // Returns json
            return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
        }
    }
}