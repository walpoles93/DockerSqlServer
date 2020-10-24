using DockerSqlServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DockerSqlServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController
    {
        private readonly AppDbContext _db;

        public NotesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var notes = await _db.Notes.ToListAsync();

            return new JsonResult(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);

            return new JsonResult(note);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Note note)
        {
            _db.Notes.Add(note);
            await _db.SaveChangesAsync();

            return new JsonResult(note.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, Note note)
        {
            var existingNote = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            existingNote.Text = note.Text;
            var success = (await _db.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            _db.Remove(note);
            var success = (await _db.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }
    }
}
