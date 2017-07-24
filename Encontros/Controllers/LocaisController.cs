using Encontros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Encontros.Controllers
{
    public class LocaisController : ApplicationBaseController
    {
        private ApplicationDbContext _context;

        public LocaisController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Locais
        public ActionResult Index()
        {
            var locais = _context.Locais.ToList();
            return View(locais);
        }
        
    }
}