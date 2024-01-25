using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal2.Models;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortal2.Controllers
{
    public class AccountController : Controller
    {
        IUsersServices _usersServices;
        IMusicClipCervices _clipCervices;

        public AccountController(IUsersServices usersServices, IMusicClipCervices clipCervices)
        {
            _clipCervices = clipCervices;
            _usersServices = usersServices;
        }

        public async Task<IActionResult> Users()
        {

            return View(await _usersServices.GetUser());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UsersConfirm(int id)
        {
            var us = await _usersServices.GetUser(id);
            if (us != null)
            {
                us.IsСonfirm = true;
                _usersServices.Update(us);
                
                return RedirectToAction(nameof(Users));
            }
            return RedirectToAction(nameof(Users));
        }
        public ActionResult Login()

        {

            return View();
        }
        public ActionResult LogOut()

        {
            Response.Cookies.Delete("Login");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logon)
        {

            if (ModelState.IsValid)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(30);
                
                if ( _usersServices.GetUser().Result.Count() == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var users =  _usersServices.GetUser().Result.Where(a => a.Login == logon.Login).FirstOrDefault();
                if (users == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                //var user = users.First();
                string? salt = users.Salt;

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));
                if (!users.IsСonfirm)
                {
                    ModelState.AddModelError("", "Ваша регистрация ещё не подтверждена, попробуйте позже!");
                    return View(logon);
                }
                if (users.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                HttpContext.Session.SetString("UserID", users.Id.ToString());
                HttpContext.Session.SetString("FirstName", users.FirstName);
                HttpContext.Session.SetString("LastName", users.LastName);
                HttpContext.Session.SetString("Login", users.Login);

                Response.Cookies.Append("UserID", users.Id.ToString(), option);
                Response.Cookies.Append("FirstName", users.FirstName, option);
                Response.Cookies.Append("LastName", users.LastName, option);
                Response.Cookies.Append("Login", users.Login, option);
                //return View("~/Views/MusicClips/Create.cshtml");
                var clip_models = await _clipCervices.GetClip();

                return RedirectToAction("Index", "MusicClips", clip_models);
            }
            return View(logon);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegistratModel reg)
        {
            if (ModelState.IsValid)
            {
                UsersDTO user = new UsersDTO();
                user.FirstName = reg.FirstName;
                user.LastName = reg.LastName;
                user.Login = reg.Login;
                user.IsСonfirm = false;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                _usersServices.Create(user);
               
                return RedirectToAction("Login");
            }

            return View(reg);
        }
    }
}
