using ChessGame.Data;
using ChessGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChessGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Rank()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Check_Login()
        {
            var data = _context.Account1.ToList();
            
            return Json(data);
        }
        public IActionResult TopRank()
        {
            try
            {
                // Lấy danh sách tất cả tài khoản
                var accounts = _context.Account1.ToList();

                // Sắp xếp danh sách theo tiêu chí Win - Lost (Win trừ Lost) từ cao đến thấp
                var sortedAccounts = accounts.OrderByDescending(a => (a.win - a.lost)).ToList();

                return Json(sortedAccounts);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }


        [HttpPost]
        public IActionResult Check_Signup([FromBody] Account account)
        {
            try
            {


                // Tạo một đối tượng Account mới
                var newAccount = new Account
                {
                    username = account.username.ToString(),
                    pass = account.pass.ToString(),
                    email = account.email.ToString(),
                    avatar = account.avatar,
                    win = account.win,
                    lost = account.lost
                };

                // Thêm tài khoản mới vào cơ sở dữ liệu
                _context.Account1.Add(newAccount);
                _context.SaveChanges();

                return Json(new { success = true, message = "Tài khoản đã được tạo thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        public IActionResult UpdateAccount(string username, string column, string value)
        {
            try
            {
                // Tìm tài khoản theo username
                var existingAccount = _context.Account1.SingleOrDefault(a => a.username == username);

                if (existingAccount == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy tài khoản có username tương ứng." });
                }

                if(column == "pass")
                {
                    existingAccount.pass = value.ToString();
                }else if(column == "email")
                {
                    existingAccount.email = value.ToString();
                }else if(column == "win")
                {
                    existingAccount.win++;
                }else if(column == "lost")
                {
                    existingAccount.lost++;
                }else if(column == "avatar")
                {
                    existingAccount.avatar = value.ToString();
                }

                _context.SaveChanges();

                return Json(new { success = true, message = "Cập nhật thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }


    }
}
