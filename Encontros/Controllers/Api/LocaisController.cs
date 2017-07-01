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
            var localInDb = _context.Locais.SingleOrDefault(c => c.Id == id);
            if (localInDb == null)
            {
                return NotFound();
            }
            _context.Locais.Remove(localInDb);
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

        public IEnumerable<Local> GetLocais(string query = null)
        {
            var locaisQuery = _context.Locais.ToList();
            

            if (!String.IsNullOrWhiteSpace(query))
            {
                query = query.ToLower();
                locaisQuery = locaisQuery.Where(l => l.Nome.ToLower().Contains(query)).ToList();
            }
                

            var locais = locaisQuery.ToList();
            return locais;
        }
    }
}
