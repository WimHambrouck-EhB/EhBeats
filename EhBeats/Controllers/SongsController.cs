using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EhBeats.Data;
using EhBeats.Models;
using Microsoft.AspNetCore.Authorization;

namespace EhBeats.Controllers
{
    public class SongsController : Controller
    {
        private readonly EhBeatsContext _context;

        public SongsController(EhBeatsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Create));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["ArtistId"] = new SelectList(await _context.Artists.OrderBy(a => a.LastName).ToListAsync(), nameof(Artist.Id), nameof(Artist.FullName));
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Title,BPM,Danceability,ReleaseDate,Explicit,ArtistId")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(await _context.Artists.OrderBy(a => a.LastName).ToListAsync(), nameof(Artist.Id), nameof(Artist.FullName), song.ArtistId);
            return View(song);
        }

    }
}
