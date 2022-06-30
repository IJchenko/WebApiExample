using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.EF;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public JournalsController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Journal>>> GetJournals()
        {
            if (_context.Journals == null)
            {
                return NotFound();
            }
            return await _context.Journals.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Journal>> GetJournal(Guid id)
        {
            if (_context.Journals == null)
            {
                return NotFound();
            }
            var journal = await _context.Journals.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }
            return journal;
        }
        [HttpPost]
        public async Task<ActionResult<Journal>> PostJournal(Journal journal)
        {
            if (_context.Journals == null)
            {
                return Problem("Entity set 'ApplicationContext.Journals'  is null.");
            }
            _context.Journals.Add(journal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJournal), new { id = journal.Id }, journal);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJournal(Guid id)
        {
            if (_context.Journals == null)
            {
                return NotFound();
            }
            var journal = await _context.Journals.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }
            _context.Journals.Remove(journal);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJournal(Guid id, [FromBody] JsonPatchDocument<Journal> journal)
        {
            var model = await _context.Journals.FindAsync(id);
            if(model == null)
            {
                return BadRequest();
            }
            journal.ApplyTo(model, ModelState);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
