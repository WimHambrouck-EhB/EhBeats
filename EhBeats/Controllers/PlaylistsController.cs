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
using NuGet.Packaging;

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

        [Authorize]
        public async Task<IActionResult> MyPlaylists()
        {
            return _context.Playlists != null ?
                        View(await _context.Playlists.ToListAsync()) :
                        Problem("Entity set 'EhBeatsContext.Playlists'  is null.");
        }

        [Authorize]
        public async Task<IActionResult> AddSongsToPlaylist(int? id, AddSongsToPlaylistViewModel? viewModel)
        {
            if (id == null || viewModel == null || _context.Playlists == null)
            {
                return NotFound();
            }

            int playlistId = id.Value;

            if (ModelState.IsValid)
            {
                viewModel.PlaylistId = playlistId;

                var inPlaylist = _context.PlaylistSongs
                    .Include(sip => sip.Song)
                    .Where(sip => sip.PlaylistId == playlistId && sip.Song!.ArtistId == viewModel.ArtistId)
                    .Select(sip => sip.Song)
                    ;

                var notInPlaylist = _context.Songs
                    .Where(s => s.ArtistId == viewModel.ArtistId && !inPlaylist.Contains(s))
                    ;

                var songsNotInPlaylist = await notInPlaylist.ToDictionaryAsync(s => s, s => false);
                var songsInPlaylist = await inPlaylist.ToDictionaryAsync(s => s!, s => true);

                viewModel.Songs = new Dictionary<Song, bool>();
                viewModel.Songs.AddRange(songsNotInPlaylist);
                viewModel.Songs.AddRange(songsInPlaylist);
            }

            ViewData["ArtistId"] = new SelectList(await _context.Artists.OrderBy(a => a.LastName).ToListAsync(), nameof(Artist.Id), nameof(Artist.FullName), viewModel.ArtistId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddSong(int? playlistId, int? songId)
        {
            if (playlistId == null || songId == null || _context.Playlists == null || _context.Songs == null)
            {
                return NotFound();
            }

            Song? song = await _context.Songs.FindAsync(songId.Value);

            if (!PlaylistExists(playlistId.Value) || song == null)
            {
                return NotFound();
            }

            await _context.AddAsync(new PlaylistSong { PlaylistId = playlistId.Value, SongId = songId.Value });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AddSongsToPlaylist), new { id = playlistId, artistId = song.ArtistId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> RemoveSong(int? playlistId, int? songId)
        {
            if (playlistId == null || songId == null || _context.Playlists == null || _context.Songs == null)
            {
                return NotFound();
            }

            PlaylistSong? playlistSong = await _context.PlaylistSongs.Where(ps => ps.PlaylistId == playlistId.Value && ps.SongId == songId.Value)
                .Include(ps => ps.Song)
                .SingleOrDefaultAsync();

            if(playlistSong == null)
            {
                return NotFound();
            }

            _context.PlaylistSongs.Remove(playlistSong);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AddSongsToPlaylist), new { id = playlistId, artistId = playlistSong.Song!.ArtistId });
        }


        // GET: Playlists/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.Songs!)
                .ThenInclude(ps => ps.Song)
                .ThenInclude(s => s!.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // GET: Playlists/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
