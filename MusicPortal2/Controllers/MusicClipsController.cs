using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Models;
using MusicPortal2.Models;

namespace MusicPortal2.Controllers
{
    public class MusicClipsController : Controller
    {
       
        IWebHostEnvironment _appEnvironment;
        IMusicClipCervices _clipCervices;
        IGenreService _genreService;
        public MusicClipsController(IMusicClipCervices clipCervices, IWebHostEnvironment appEnvironment, IGenreService genreService)
        {
            _clipCervices = clipCervices;
            _appEnvironment = appEnvironment;
            _genreService = genreService;
        }

        // GET: MusicClips
        public async Task<IActionResult> Index()
        {
            return View(await _clipCervices.GetClip());
        }


        public async Task<IActionResult> Create()
        {
            MusicClipView model = new MusicClipView();
            model.GenreList = await _genreService.GetGenre();
            return View(model);
        }
        public async Task<IActionResult> SelectedVideo(int id)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(30);
            var tmp = _clipCervices.GetClip().Result.ToList();
            int selected_index = 0;
            for (int i = 0; i < tmp.Count(); i++)
            {
                if (tmp[i].Id == id)
                    selected_index = i;
            }
            int previous_video, next_video = 0;
            if (selected_index != 0)
            {
                previous_video = tmp[selected_index - 1].Id;
            }
            else
            {
                previous_video = tmp[tmp.Count - 1].Id;
            }
            if (selected_index == tmp.Count - 1)
            {
                next_video = tmp[0].Id;
            }
            else if (selected_index == 0 && tmp.Count == 1) { next_video = id; }
            else if (selected_index >= 0 && selected_index < tmp.Count - 1) { next_video = tmp[selected_index + 1].Id; }
            Response.Cookies.Append("Selected_video", id.ToString(), option);
            Response.Cookies.Append("previous_video", previous_video.ToString(), option);
            Response.Cookies.Append("next_video", next_video.ToString(), option);
            return RedirectToAction("Index", await _clipCervices.GetClip());
        }
        // POST: MusicClips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ReleaseDate,Artist,Genre,Id_user")] MusicClipDTO musicClip, IFormFile? uploadedFile)
        {
            if (uploadedFile != null)
            {
                // Путь к папке Files
                string path = "/video/" + uploadedFile.FileName; // имя файла

                // Сохраняем файл в папку images в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }




                musicClip.Path_Video = uploadedFile.FileName;
            }
            if (ModelState.IsValid)
            {
                musicClip.Genre = musicClip.Genre.Trim();
                await _clipCervices.Create(musicClip);
                
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Create", "MusicClips", musicClip);
        }




        public async Task<IActionResult> Delete(int id)
        {
            var musicClip = await _clipCervices.GetClip(id);
            if (musicClip != null)
            {
                await _clipCervices.Delete(id);
            }

            
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicClip = await _clipCervices.GetClip(id);
            if (musicClip != null)
            {
                await _clipCervices.Delete(id);
            }

            
            return RedirectToAction(nameof(Index));
        }
    }
}
