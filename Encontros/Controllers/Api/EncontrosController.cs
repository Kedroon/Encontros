using Encontros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Encontros.Controllers.Api
{
    public class EncontrosController : ApiController
    {
        private ApplicationDbContext _context;
        public EncontrosController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult ExcluirEncontro(int id)
        {
            var encontroInDb = _context.Encontros.SingleOrDefault(c => c.Id == id);
            if (encontroInDb == null)
            {
                return NotFound();
            }
            _context.Encontros.Remove(encontroInDb);
            _context.SaveChanges();
            return Ok();

        }
        

    }
}
