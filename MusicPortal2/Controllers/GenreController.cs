using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Models;
using MusicPortal2.Models;

namespace MusicPortal2.Controllers
{
    public class GenreController : Controller
    {
        
        IMusicClipCervices _clipCervices;
        IGenreService _genreService;
        public GenreController(IMusicClipCervices clipCervices,  IGenreService genreService)
        {
            _clipCervices = clipCervices;
            _genreService = genreService;
        }
        public async Task<IActionResult> CreateGenre()
        {
            var list_genre_dto = await _genreService.GetGenre();
            List<Genre> genreList = new List<Genre>();
            foreach(var it in list_genre_dto)
            {
                genreList.Add(new Genre
                {
                    Id = it.Id,
                    Genre_name = it.Genre_name
                });
            }
            GenreView genreView = new GenreView();
            genreView.GenreList = genreList;
           
            return View(genreView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateGenre([Bind("Id,Genre_name")] GenreDTO genre)
        {

            if (ModelState.IsValid)
            {
                foreach (var item in await _genreService.GetGenre())
                {
                    if (genre.Genre_name == item.Genre_name)
                    {
                        ModelState.AddModelError("", "Такой жанр уже существует!");
                        return View(genre);
                    }

                }

                await _genreService.Create(genre);
               
                var clip_models = await _clipCervices.GetClip();
                return View("~/Views/MusicClips/Index.cshtml", clip_models);
            }
            return RedirectToAction("CreateGenre", "Genre", genre);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(int id, [Bind("Id,Genre_name")] GenreDTO genre, string original_name)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    GenreDTO edit_genre = GetGenre(original_name);
                    edit_genre.Genre_name = genre.Genre_name;
                   await _genreService.Update(edit_genre);
                   
                }
                catch (DbUpdateConcurrencyException)
                {

                    return NotFound();



                }
                return RedirectToAction(nameof(CreateGenre));
            }
            return View(genre);
        }

        [HttpPost, ActionName("DeleteGenre")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string original_name)
        {
            var ganre_ = GetGenre(original_name);
            if (ganre_ != null)
            {
                await _genreService.Delete(ganre_.Id);
            }

           
            return RedirectToAction(nameof(CreateGenre));
        }
        public GenreDTO GetGenre(string name)
        {
            GenreDTO genre = null;
            foreach (var item in _genreService.GetGenre().Result)
            {
                if (name.Contains(item.Genre_name))
                    return item;
            }
            return null;
        }
    }
}
