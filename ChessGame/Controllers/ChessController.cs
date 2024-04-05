using ChessGame.Data;
using ChessGame.Models;
using ComputeSharp;
using ILGPU.Runtime.CPU;
using ILGPU.Runtime;
using ManagedCuda;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ILGPU.Runtime.Cuda;
using ILGPU;
using ILGPU.Runtime.OpenCL;
using TerraFX.Interop.WinRT;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Newtonsoft.Json;

namespace ChessGame.Controllers
{
	

	public class ChessController : Controller, IDisposable
    {


        private readonly ApplicationDbContext _context;

        public ChessController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
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
		public IActionResult AI()
		{

			return View("AI");
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

        
		[HttpPost]
		public ActionResult AI([FromBody] InputArrayModel inputArray)
        {
            using (ChessAI chessAI = new ChessAI())
            {
               

                InputArrayModel a = new InputArrayModel();

                int[][] resultArray = inputArray.InputArray.Select(innerList => innerList.ToArray()).ToArray();
                Stopwatch stopwatch = Stopwatch.StartNew();
                // Trả về mảng kết quả cho JavaScript
                List<int[][]> listOfArrays = chessAI.ValidMoves(resultArray, true);
               
                int temp = 10000000;
                Parallel.ForEach(listOfArrays, i =>
                {
                    int[][] maxtrang = chessAI.MaxVal(i, -1000000000, 1000000000, 2);
                    int diem = chessAI.SumArray(maxtrang, false);

                    if (temp > diem)
                    {
                        temp = diem;
                        resultArray = i;
                    }
                });
                Debug.WriteLine(temp);
                List<List<int>> listOfLists = new List<List<int>>();

                foreach (var row in resultArray)
                {
                    // Tạo danh sách từ mỗi dòng của mảng hai chiều
                    List<int> list = new List<int>(row);
                    listOfLists.Add(list);

                }
                a.InputArray = listOfLists;
                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;

                // In ra thời gian đã trôi qua
                Debug.WriteLine("Thời gian đã trôi qua: " + elapsedTime);
                temp = 10000000;
                return Json(a.InputArray);
            }
            
		}
	}



}
