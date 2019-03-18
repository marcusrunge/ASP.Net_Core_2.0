using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPIApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/Ticket")]
    public class TicketController : Controller
    {
        TicketContext _context;
        public TicketController(TicketContext context)
        {
            _context = context;
            if (_context.TicketItems.Count() == 0)
            {
                _context.TicketItems.Add(new TicketItem { Concert = "Beyonce" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TicketItem> GetAll()
        {
            return _context.TicketItems.AsNoTracking().ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(long id)
        {
            var ticket = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound(); //404
            }
            return new ObjectResult(ticket); //200
        }

        [HttpPost]
        public IActionResult Create([FromBody]TicketItem ticket)
        {
            if (ticket == null)
            {
                return BadRequest(); //400
            }
            _context.TicketItems.Add(ticket);
            _context.SaveChanges();
            //return "/Ticket/" + ticket.Id
            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]TicketItem ticket)
        {
            if (ticket == null || ticket.Id != id)
            {
                return BadRequest(); //400
            }
            var ticketFromContext = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (ticketFromContext == null)
            {
                return NotFound();
            }
            ticketFromContext.Concert = ticket.Concert;
            ticketFromContext.Available = ticket.Available;
            ticketFromContext.Artist = ticket.Artist;
            _context.TicketItems.Update(ticket);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ticketFromContext = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (ticketFromContext == null)
            {
                return NotFound();
            }
            _context.TicketItems.Remove(ticketFromContext);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}