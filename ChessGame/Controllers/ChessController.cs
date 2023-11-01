using ChessGame.Data;
using ChessGame.Models;
using Microsoft.AspNetCore.Mvc;
namespace ChessGame.Controllers
{
    
    public class ChessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChessController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult White()
        { 

            return View("White");
        }
        public IActionResult Black()
        {

            return View("Black");
        }
        public IActionResult Offline()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult GetData()
        {
            // Truy vấn cơ sở dữ liệu và trả về dữ liệu
            var data = _context.maps.ToList(); 

            return Json(data);
        }
        

        [HttpPost]
        public IActionResult UpdateData([FromBody] List<MapEntity> newData)
        {
            try
            {
                foreach (var item in newData)
                {
                    var existingData = _context.maps.FirstOrDefault(p => p.Idmaps == item.Idmaps && p.X == item.X && p.Y == item.Y);
                    if (existingData != null)
                    {
                        existingData.Value = item.Value;
                    }
                    else
                    {
                        _context.maps.Add(item);
                    }
                }

                _context.SaveChanges();

                return Json(new { success = true, message = "Dữ liệu đã được cập nhật." });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        public IActionResult NewGame(int ID)
        {
            // Tìm và cập nhật bản ghi với (x = 1, y = 2)
            for(int i = 1; i <= 8; i++)
            {
                for(int j = 3; j <= 6; j++)
                {
                    var record = _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == j && r.Y == i);
                    if (record != null)
                    {
                        record.Value = 0;
                    }
                }
                _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 2 && r.Y == i).Value = 1;
                _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 7 && r.Y == i).Value = 11;

            }
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 1 && r.Y == 1).Value = 5;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 1 && r.Y == 2).Value = 2;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 1 && r.Y == 3).Value = 3;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 1 && r.Y == 4).Value = 9;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 1 && r.Y == 5).Value = 10;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 1 && r.Y == 6).Value = 3;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 1 && r.Y == 7).Value = 2;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 1 && r.Y == 8).Value = 5;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 8 && r.Y == 1).Value = 15;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 8 && r.Y == 2).Value = 12;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 8 && r.Y == 3).Value = 13;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 8 && r.Y == 4).Value = 19;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 8 && r.Y == 5).Value = 20;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 8 && r.Y == 6).Value = 13;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 8 && r.Y == 7).Value = 12;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 8 && r.Y == 8).Value = 15;

            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 1 && r.Y == 9).Value = 1;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 2 && r.Y == 9).Value = 1;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 3 && r.Y == 9).Value = 1000;
            _context.maps.FirstOrDefault(r => r.Idmaps == ID && r.X == 4 && r.Y == 9).Value = 1000;

            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return Json(new { success = true, message = "Dữ liệu đã được cập nhật." });
        }


        

    }
}
