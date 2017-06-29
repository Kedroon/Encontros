using Encontros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;

namespace Encontros.Controllers.Api
{
    public class LocaisController : ApiController
    {
        private ApplicationDbContext _context;
        public LocaisController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult ExcluirLocal(int id)
        {
            var customerInDb = _context.Locais.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }
            _context.Locais.Remove(customerInDb);
            _context.SaveChanges();
            return Ok();

        }

        [HttpPost]
        public IHttpActionResult InserirLocal(Local local)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Locais.Add(local);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri.ToString()), local);
        }

        public IHttpActionResult GetLocais(string query = null)
        {
            var locaisQuery = _context.Locais.Where(l => l.Nome.Contains(""));

            if (!String.IsNullOrWhiteSpace(query))
            {
                locaisQuery = locaisQuery.Where(l => l.Nome.Contains(query));
            }
                

            var locais = locaisQuery
                .ToList();

            return Ok(locais);
        }
    }
}
