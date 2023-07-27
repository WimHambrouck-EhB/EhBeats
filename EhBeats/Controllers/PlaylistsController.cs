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
    public class PlaylistsController : Controller
    {
        private readonly EhBeatsContext _context;

        public PlaylistsController(EhBeatsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(MyPlaylists));
        }

        public async Task<IActionResult> MyPlaylists ()
        {
              return _context.Playlists != null ? 
                          View(await _context.Playlists.ToListAsync()) :
                          Problem("Entity set 'EhBeatsContext.Playlists'  is null.");
        }

        public async Task<IActionResult> AddSongsToPlaylist(int? id, AddSongsToPlaylistViewModel? viewModel)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var songsInPlaylist = _context.PlaylistSongs
                    .Include(sip => sip.Song)
                    .Where(sip => sip.PlaylistId == id && sip.Song!.ArtistId == viewModel!.ArtistId)
                    .Select(sip => sip.Song)
                    ;

                var notInPlaylist = _context.Songs
                    .Where(s => s.ArtistId == viewModel!.ArtistId && !songsInPlaylist.Contains(s))
                    .ToList()
                    ;

                return RedirectToAction(nameof(MyPlaylists));
            }   

            ViewData["ArtistId"] = new SelectList(await _context.Artists.OrderBy(a => a.LastName).ToListAsync(), nameof(Artist.Id), nameof(Artist.FullName), viewModel.ArtistId);

            return View();
        }


        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // GET: Playlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MyPlaylists));
            }
            return View(playlist);
        }

        private bool PlaylistExists(int id)
        {
          return (_context.Playlists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
