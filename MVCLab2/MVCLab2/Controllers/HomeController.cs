using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCLab2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;

namespace MVCLab2.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private MVCLab2.Models.Ramunisrealt db;	       // класс, описывающий БД	
        static public string MsgErr = "";              //сообщение на странице 

        public static string RES = "";

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>                             // список из одного claim -  имени пользователя 
       {  new Claim ( ClaimsIdentity.DefaultNameClaimType,  userName )
       };
            ClaimsIdentity id = new ClaimsIdentity(claims,    // создание объекта ClaimsIdentity
                                                    "ApplicationCookie",
                                                     ClaimsIdentity.DefaultNameClaimType,
                                                     ClaimsIdentity.DefaultRoleClaimType
                                                   );          // Объект задает только типы последних аргументов,
                                                               // но значении  аргументов не определяет.    
            await HttpContext.SignInAsync(
                         CookieAuthenticationDefaults.AuthenticationScheme,
                         new ClaimsPrincipal(id));
            // Метод HttpContext.SignInAsync() создает Cookies для текущего пользователя
            // на основе схемы, определяемой 1-ым аргументом. Этот аргумент содержит 
            // 1-ым аргументом. Этот аргумент является значением по умолчанию для
            // Cookies-аутентификации. 2-ой аргумент определяет новый объект  ClaimsPrincipal, т.е 
            // пользователя, характеризуемого объектом identity.

            RES = "Текущий пользователь:  " + userName + ", аутентификация  ";
            if (id.IsAuthenticated) RES += "выполнена.";
            else RES += " не выполнена.";
          
        }

        //

        [HttpGet]
        [AllowAnonymous]
        [Route("LOGIN")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ReadDB", "Home");
            }
            MsgErr = "HttpGet";
            return View();
        }



        [HttpPost]
        [Route("LOGIN")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginModel model)       // перегрузка метода                                                                // Login
        {
            if (this.ModelState.IsValid)
            {

                if (db == null) db = new MVCLab2.Models.Ramunisrealt();         //Свойство modelState базового класса					    //  содержит всю передаваемую в
                                                                                // контроллер информацию
                if (model.Username == null || model.PW == null) return View(model);
                var U = db.Clients.
                           Where(u => u.Username == model.Username && u.PW == sha1(model.PW)).
                           Select(u => u);

                //  главная функция
                if (U != null && U.Count() >= 1)
                {
                    await Authenticate(model.Username);                   // аутентификация

                    return RedirectToAction("Index", "Home");
                }

                // проверка вошел ли пользователь

                //if (U != null && U.Count() >= 1)
                //{          //   await Authenticate(model.Email);      // аутентификация пока не используется
                //    return RedirectToAction("Index", "Home");
                //}
                ModelState.AddModelError("", "Некорректный логин (и/или пароль");
                MsgErr += "Некорректный логин (и/или пароль.  ";
                MsgErr += "Попытайтесь еще раз ";

            }
            return View(model);         //опять отобразить окно входа
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("REGISTR")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ReadDB", "Home");
            }
            return View();
        }

        string sha1(string input)
        {
            byte[] hash;
            using (var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider())
                hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
            var sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        [HttpPost]
        [Route("REGISTR")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        //   model - данные,  полученные от страницы регистрции 
        {
            if (ModelState.IsValid)
            {
                if (db == null) db = new MVCLab2.Models.Ramunisrealt();
                if (db == null)
                {
                    MsgErr += "БД: ошибка открытия  ; "; return View();
                }
            }
            if (model.Username == null || model.PW == null || model.ConfirmPassword == null)
                return View(model);


            var U = db.Clients
                 .Where(u => u.Username == model.Username && u.PW == model.PW)
   .Select(u => u);

            if (U.Count() == 0)                                        // добавление нового пользователя в БД
            {
                String hash = sha1(model.PW);

                db.Clients.Add(new MVCLab2.Models.Clients { Username = model.Username, PW = hash, 
                    F = model.F, I = model.I, O = model.O, DR = model.DR, City = model.City,
                    Adres = model.Adres, Phone = model.Phone, Email = model.Email }
                                                        );
                await db.SaveChangesAsync();
                RES = "Новый пользователь " + model.Username + " успешно зарегистрирован.  ";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные логин и/или пароль.");
                RES = "";
                MsgErr += "Не могу добавить запись в БД.";
            }

            return View(model);                     //опять отобразить окно регистрации
        }

        //

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }

        [Authorize]
        public IActionResult Index()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome "+result;
            }
            return View();
        }

        public IActionResult MVC()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome "+result;
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ReadDB(Clients users)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.Clients.Where(u => u.Username == User.Identity.Name).ToListAsync());
        }

        public async Task<IActionResult> ComeForm(Contracts users)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            var userid = db.Clients.Where(u => u.Username == User.Identity.Name).Select(u => u.Username).FirstOrDefault();

            // return View(await db.Contracts.Where(u => u.Client == userid).ToListAsync());
            return View(await db.myboard.Where(u => u.Username == userid).ToListAsync());
        }

        public async Task<IActionResult> Deal(int id)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            ViewBag.Numer = "Сделка № " + id;

            //
            SqlConnection connection = new SqlConnection(@"Server=ASUSH510M-K; Database=Ramunisrealt; Trusted_Connection = True;");

            try
            {
                connection.Open();
                // Label6.Text = "Соединение установлено!";
            }
            catch (Exception)
            {
                // Label6.Text = "Соединение не установлено!";
            }

            var query = "SELECT Clients.F, Realts.F, Districts.District, Servics.Servic FROM Contracts INNER JOIN Clients ON Contracts.Client = Clients.ID INNER JOIN Realts ON Contracts.Realtor = Realts.ID INNER JOIN Districts ON Contracts.District = Districts.ID INNER JOIN Servics ON Contracts.Servic = Servics.ID WHERE Contracts.ID=" + id + "";
            // Создать объект Command.
            SqlCommand cmd = new SqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = query;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        ViewBag.q1 = reader[0].ToString();
                        ViewBag.q2 = reader[1].ToString();
                        ViewBag.q3 = reader[2].ToString();
                        ViewBag.q4 = reader[3].ToString();
                    }
                }
            }
            //

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            var userid = db.Clients.Where(u => u.Username == User.Identity.Name).Select(u => u.ID).FirstOrDefault();

            return View(await db.Contracts.Where(u => u.Client == userid).Take(1).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Rent1(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.rentboard.OrderBy(u =>  u.Price).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Rent2(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.rentboard.OrderByDescending(u => u.Price).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Rent3(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.rentboard.OrderBy(u => u.Sq).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Rent4(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.rentboard.OrderByDescending(u => u.Sq).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Rent5(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }
            if (db == null) db = new MVCLab2.Models.Ramunisrealt();
            IEnumerable<Districts> au = (IEnumerable<Districts>)db.Districts.ToList();
            ViewBag.Au = new SelectList(au, "ID", "District");

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Rent6(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }       

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.Contracts.Where(u => u.Servic == 3).Where(u => u.District == ads.District).ToListAsync());

        }

        [Authorize]
        public async Task<IActionResult> Rent(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            //var ad = from u in db.Contracts
            //            join c in db.Clients on u.Client equals c.ID
            //            join d in db.Realts on u.Realtor equals d.ID
            //           join e in db.Districts on u.District equals e.ID
            //           join f in db.Servics on u.Servic equals f.ID
            //         select new { ID = u.ID, DZ = u.DZ,  Client = c.F, Realtor = d.F, District = e.District, Servic = f.Servic, Sq = u.Sq , DS = u.DS, Term = u.Term, Price =u.Price, Pay = u.Pay, Repair = u.Repair };
            //return View(await ad.Where(f => f.Servic == "Сдача").ToListAsync());

            //return View(await db.Contracts.Where(u => u.Servic == 3).ToListAsync());
            return View(await db.rentboard.ToListAsync());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Rented(int id)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }
            ViewBag.Numer = "Договор № "+id;

            //
            SqlConnection connection = new SqlConnection(@"Server=ASUSH510M-K; Database=Ramunisrealt; Trusted_Connection = True;");

            try
            {
                connection.Open();
                // Label6.Text = "Соединение установлено!";
            }
            catch (Exception)
            {
                // Label6.Text = "Соединение не установлено!";
            }

            var query = "SELECT Clients.F, Realts.F, Districts.District, Servics.Servic FROM Contracts INNER JOIN Clients ON Contracts.Client = Clients.ID INNER JOIN Realts ON Contracts.Realtor = Realts.ID INNER JOIN Districts ON Contracts.District = Districts.ID INNER JOIN Servics ON Contracts.Servic = Servics.ID WHERE Contracts.ID=" + id + "";
            // Создать объект Command.
            SqlCommand cmd = new SqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = query;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        ViewBag.q1 = reader[0].ToString();
                        ViewBag.q2 = reader[1].ToString();
                        ViewBag.q3 = reader[2].ToString();
                        ViewBag.q4 = reader[3].ToString();
                    }
                }
            }
            //

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();
           
            return View(await db.Contracts.Where(u => u.ID == id).ToListAsync());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Rented(Contracts model,int id)
        {
            DateTime now = DateTime.Now;
            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            var userid = db.Clients.Where(u => u.Username == User.Identity.Name).Select(u => u.ID).FirstOrDefault();
            IEnumerable<Contracts> cont = (IEnumerable<Contracts>)db.Contracts.Where(u => u.ID == id).ToList();
   
            foreach (var item in cont)
            {
                db.Contracts.Add(new MVCLab2.Models.Contracts { DZ = now, Client = userid, Realtor = 1, District = item.District, Servic = 1, Sq = item.Sq, DS = item.DS, Term = item.Term, Price = item.Price, Pay = item.Pay, Repair = item.Repair }
                                                         );
                await db.SaveChangesAsync();
            }
            if (id != null)
            {
                Contracts con = await db.Contracts.FirstOrDefaultAsync(p => p.ID == id);
                if (con != null)
                {
                    db.Contracts.Remove(con);
                    await db.SaveChangesAsync();            
                }
            }
            return RedirectToAction("Success", "Home", new { @serv = 1 });
        }

        [Authorize]
        public async Task<IActionResult> Buy1(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.buyboard.OrderBy(u => u.Price).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Buy2(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.buyboard.OrderByDescending(u => u.Price).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Buy3(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.buyboard.OrderBy(u => u.Sq).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Buy4(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.buyboard.OrderByDescending(u => u.Sq).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Buy5(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }
            if (db == null) db = new MVCLab2.Models.Ramunisrealt();
            IEnumerable<Districts> au = (IEnumerable<Districts>)db.Districts.ToList();
            ViewBag.Au = new SelectList(au, "ID", "District");

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Buy6(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.Contracts.Where(u => u.Servic == 2).Where(u => u.District == ads.District).ToListAsync());
        }



        [Authorize]
        public async Task<IActionResult> Buy(Contracts ads)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            //return View(await db.Contracts.Where(u => u.Servic == 2).ToListAsync());
            return View(await db.buyboard.ToListAsync());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Buyed(int id)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            ViewBag.Numer = "Договор № " + id;

            //
            SqlConnection connection = new SqlConnection(@"Server=ASUSH510M-K; Database=Ramunisrealt; Trusted_Connection = True;");

            try
            {
                connection.Open();
               // Label6.Text = "Соединение установлено!";
            }
            catch (Exception)
            {
               // Label6.Text = "Соединение не установлено!";
            }

            var query = "SELECT Clients.F, Realts.F, Districts.District, Servics.Servic FROM Contracts INNER JOIN Clients ON Contracts.Client = Clients.ID INNER JOIN Realts ON Contracts.Realtor = Realts.ID INNER JOIN Districts ON Contracts.District = Districts.ID INNER JOIN Servics ON Contracts.Servic = Servics.ID WHERE Contracts.ID=" + id+"";
            // Создать объект Command.
            SqlCommand cmd = new SqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = query;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        ViewBag.q1 = reader[0].ToString();
                        ViewBag.q2 = reader[1].ToString();
                        ViewBag.q3 = reader[2].ToString();
                        ViewBag.q4 = reader[3].ToString();
                    }
                }
            }
                        //

                        if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            return View(await db.Contracts.Where(u => u.ID == id).ToListAsync());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Buyed(Contracts model, int id)
        {
            DateTime now = DateTime.Now;

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();

            var userid = db.Clients.Where(u => u.Username == User.Identity.Name).Select(u => u.ID).FirstOrDefault();

            IEnumerable<Contracts> cont = (IEnumerable<Contracts>)db.Contracts.Where(u => u.ID == id).ToList();

            foreach (var item in cont)
            {
                db.Contracts.Add(new MVCLab2.Models.Contracts { DZ = now, Client = userid, Realtor = 1, District = item.District, Servic = 4, Sq = item.Sq, DS = item.DS, Term = item.Term, Price = item.Price, Pay = item.Pay, Repair = item.Repair }
                                                         );
                await db.SaveChangesAsync();

            }

            if (id != null)
            {
                Contracts con = await db.Contracts.FirstOrDefaultAsync(p => p.ID == id);
                if (con != null)
                {
                    db.Contracts.Remove(con);
                    await db.SaveChangesAsync();
                }
            }

            return RedirectToAction("Success", "Home", new { @serv = 4 });
        }


        [Authorize]
        [HttpGet]
        public IActionResult WriteDB()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();
            IEnumerable<Districts> regions = (IEnumerable<Districts>)db.Districts.ToList();

            ViewBag.Regions = new SelectList(regions, "ID","District");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WriteDB(Contracts model)
        {

            DateTime now = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (db == null) db = new MVCLab2.Models.Ramunisrealt();
                if (db == null)
                {
                    MsgErr += "БД: ошибка открытия  ; "; return View();
                }
            }

            if (model.District == null || model.Sq == null || model.DS == null || model.Term == null || model.Price == null || model.Pay == null || model.Repair == null)
                return View(model);

            var userid = db.Clients.Where(u => u.Username == User.Identity.Name).Select(u => u.ID).FirstOrDefault();

            db.Contracts.Add(new MVCLab2.Models.Contracts { DZ = now, Client = userid, Realtor = 1, District = model.District, Servic = 3, Sq = model.Sq, DS = model.DS, Term = model.Term, Price = model.Price, Pay = model.Pay, Repair = model.Repair }
                                                        );
            await db.SaveChangesAsync();
            return RedirectToAction("Success", "Home", new { @serv = 3 });

        }

        [Authorize]
        [HttpGet]
        public IActionResult WriteDB2()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            if (db == null) db = new MVCLab2.Models.Ramunisrealt();
            IEnumerable<Districts> regions2 = (IEnumerable<Districts>)db.Districts.ToList();

            ViewBag.Regions2 = new SelectList(regions2, "ID", "District");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WriteDB2(Contracts model)
        {
            DateTime now = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (db == null) db = new MVCLab2.Models.Ramunisrealt();
                if (db == null)
                {
                    MsgErr += "БД: ошибка открытия  ; "; return View();
                }
            }

            if (model.District == null || model.Sq == null || model.DS == null || model.Price == null || model.Pay == null || model.Repair == null)
                return View(model);

            var userid = db.Clients.Where(u => u.Username == User.Identity.Name).Select(u => u.ID).FirstOrDefault();

            db.Contracts.Add(new MVCLab2.Models.Contracts { DZ = now, Client = userid, Realtor = 1, District = model.District, Servic = 2, Sq = model.Sq, DS = model.DS, Term = 9999999, Price = model.Price, Pay = model.Pay, Repair = model.Repair }
                                                        );
            await db.SaveChangesAsync();
            return RedirectToAction("Success", "Home", new { @serv = 2});
        }

        public async Task<IActionResult> Success(int serv)
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                ViewData["sessionlogin"] = "Welcome " + result;
            }

            string service="";

            if (serv == 1) service = "Аренды";
            if (serv == 2) service = "Продажи";
            if (serv == 3) service = "Сдачи";
            if (serv == 4) service = "Покупки";

            ViewBag.Info = "Договор "+service+" был успешно заключён!";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
