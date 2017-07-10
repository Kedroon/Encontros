using Encontros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Encontros.ViewModels;
using System.IO;
using System.Net;

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
            {
                _context.Encontros.Add(encontro);
            }
            else
            {
                var encontroInDb = _context.Encontros.Single(e => e.Id == encontro.Id);

                encontroInDb.NomeEncontro = encontro.NomeEncontro;
                encontroInDb.LocalId = encontro.LocalId;
                encontroInDb.DataDoEncontro = encontro.DataDoEncontro;
            }

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
        public ActionResult UploadFiles(int id)
        {

            var validImageTypes = new string[]
                {
                 "image/gif",
                 "image/jpeg",
                 "image/pjpeg",
                 "image/png"
                };
            var fotos = new List<FotoEncontro>();
            foreach (string file in Request.Files)
            {
                
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                {
                    ModelState.AddModelError("ImageUpload", "This field is required");
                }

                if (!validImageTypes.Contains(hpf.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                }
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 400;
                    return Content("Arquivo não é uma imagem!");
                }
                var fotoDir = "~/Fotos";
                var imagePath = Path.Combine(Server.MapPath(fotoDir), Path.GetFileName(hpf.FileName));
                var imageUrl = Path.Combine(fotoDir, Path.GetFileName(hpf.FileName));
                hpf.SaveAs(imagePath);


                fotos.Add(new FotoEncontro()
                {
                    Name = hpf.FileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType,
                    CreatedDate = DateTime.Now,
                    ImageUrl = imageUrl,
                    EncontroId = id
                });
            }

            _context.FotoEncontros.AddRange(fotos);
            _context.SaveChanges();
            // Returns json
            return Content("{\"name\":\"" + fotos[0].Name + "\",\"type\":\"" + fotos[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", fotos[0].Length) + "\"}", "application/json");

        }
    }
}