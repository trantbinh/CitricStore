using CitricStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Resources;

namespace CitricStore.Controllers
{
    public class CitricStoreController : Controller
    {

        CitricStoreEntities database = new CitricStoreEntities();


        //Lọc Game theo ngày cập nhật -> Game Mới
        public ActionResult Index()
        {
            var kh = database.CUSTOMERs.ToList();
            return View(kh);
        }

        private List<GAME> LayGameMoi(int soluong)
        {
            return database.GAMEs.OrderByDescending(game => game.UpdateDate).Take(soluong).ToList();
        }
        public ActionResult Index_GameMoi()
        {
            var dsGameMoi = LayGameMoi(8);
            return PartialView(dsGameMoi);
        }

        //Lọc Game theo đánh giá -> Game Đề Xuất
        private List<GAME> LayGameTheoDanhGia(int soluong)
        {
            return database.GAMEs.OrderByDescending(game => game.Rating).Take(soluong).ToList();
        }
        public ActionResult Index_GameTheoDanhGia()
        {
            var dsGameDeXuat = LayGameTheoDanhGia(8);
            return PartialView(dsGameDeXuat);
        }

        //Lọc App theo ngày cập nhật -> App Mới
        private List<SOFTWARE> LayAppMoi(int soluong)
        {
            return database.SOFTWAREs.OrderByDescending(app => app.UpdateDate).Take(soluong).ToList();
        }
        public ActionResult Index_AppMoi()
        {
            var dsAppMoi = LayAppMoi(8);
            return PartialView(dsAppMoi);
        }


        //Lọc App theo đánh giá -> App Đề Xuất
        private List<SOFTWARE> LayAppTheoDanhGia(int soluong)
        {
            return database.SOFTWAREs.OrderByDescending(app => app.Rating).Take(soluong).ToList();
        }
        public ActionResult Index_AppTheoDanhGia()
        {
            var dsAppDeXuat = LayAppTheoDanhGia(8);
            return PartialView(dsAppDeXuat);
        }

        //Dropdown Navbar
        public ActionResult Index_LayTheLoaiApp()
        {
            var dsTheLoai = database.CATEGORY_SOFTWARE.ToList();
            return PartialView(dsTheLoai);
        }
        public ActionResult Index_LayTheLoaiGame()
        {
            var dsTheLoai = database.CATEGORY_GAME.ToList();
            return PartialView(dsTheLoai);
        }

        public ActionResult Page_GameTheoTheLoai(int id, string sortOrder)
        {
            ViewBag.RateSortParm = String.IsNullOrEmpty(sortOrder) ? "rate_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var games = from s in database.GAMEs
                        where s.IDCatGame == id
                        select s;
            switch (sortOrder)
            {
                case "rate_desc":
                    games = games.OrderByDescending(s => s.Rating);
                    break;
                case "Date":
                    games = games.OrderBy(s => s.UpdateDate);
                    break;
                case "date_desc":
                    games = games.OrderByDescending(s => s.UpdateDate);
                    break;
                default:
                    games = games.OrderBy(s => s.IDGame);
                    break;
            }
            return View(games.ToList());
        }


        public ActionResult Page_AppTheoTheLoai(int id, string sortOrder)
        {
            ViewBag.RateSortParm = String.IsNullOrEmpty(sortOrder) ? "rate_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var apps = from s in database.SOFTWAREs
                        where s.IDCatSoft == id
                        select s;
            switch (sortOrder)
            {
                case "rate_desc":
                    apps = apps.OrderByDescending(s => s.Rating);
                    break;
                case "Date":
                    apps = apps.OrderBy(s => s.UpdateDate);
                    break;
                case "date_desc":
                    apps = apps.OrderByDescending(s => s.UpdateDate);
                    break;
                default:
                    apps = apps.OrderBy(s => s.IDSoft);
                    break;
            } 
            return View(apps);
        }




        //Trang search
        public ActionResult Page_Search(string searchString)
        {
            var game = from g in database.OVERALLs
                       select g;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                game = game.Where(g => g.NameOverall.ToLower().Contains(searchString));
            }

            Session["SearchKeyWord"] = searchString;
            return View(game);
        }






        public ActionResult DetailsApp(int id)
        {
            var app = database.SOFTWAREs.FirstOrDefault(s => s.IDSoft == id);
            return View(app);
        }

        //DetailsApp
        public ActionResult Details_AppMoi()
        {
            var dsAppMoi = LayAppMoi(8);
            return PartialView(dsAppMoi);
        }

        public ActionResult Details_AppTheoDanhGia()
        {
            var dsAppDeXuat = LayAppTheoDanhGia(8);
            return PartialView(dsAppDeXuat);
        }
        public ActionResult Details_AppCungTheLoai(int idtheloai)
        {
            var dsNPH = database.SOFTWAREs.Where(ud => ud.IDCatSoft == idtheloai).ToList();
            return PartialView(dsNPH);
        }
        //--------------------------DETAILS GAME--------------------------
        public ActionResult DetailsGame(int id)
        {
            var game = database.GAMEs.FirstOrDefault(s => s.IDGame == id);
            return View(game);
        }

        public ActionResult Details_GameTheoDanhGia()
        {
            var dsGameDeXuat = LayGameTheoDanhGia(8);
            return PartialView(dsGameDeXuat);
        }

        public ActionResult Details_GameMoi()
        {
            var dsGameMoi = LayGameMoi(8);
            return PartialView(dsGameMoi);
        }

        public ActionResult Details_GameCungTheLoai(int idtheloai)
        {
            var dsNPH = database.GAMEs.Where(ud => ud.IDCatGame == idtheloai).ToList();
            return PartialView(dsNPH);
        }


        //--------------------------GET NAME--------------------------\

        //Lấy tên thể loại app
        public ActionResult TenTheLoaiApp(int idtheloai)
        {
            var theloai = database.CATEGORY_SOFTWARE.Where(g => g.IDCatSoft == idtheloai).ToList();

            return PartialView(theloai);
        }
        public ActionResult TenTheLoaiGame(int idtheloai)
        {
            var theloai = database.CATEGORY_GAME.Where(g => g.IDCatGame == idtheloai).ToList();
            return PartialView(theloai);
        }
        //Lấy tên của nhà phát hành
        public ActionResult Details_TenNPH(int idnph)
        {
            var tennph = database.PUBLISHERs.Where(g => g.IDPublisher == idnph).ToList();
            return PartialView(tennph);
        }

        //Lấy tên ngôn ngữ
        public ActionResult Details_NameLanguage(int idngonngu)
        {
            var tenngonngu = database.LANGUAGEs.Where(g => g.IDLanguage == idngonngu).ToList();
            return PartialView(tenngonngu);
        }

        //Lấy tên hệ điều hành
        public ActionResult Details_TenHDH(int idhdh)
        {
            var tenhdh = database.PLATFORMs.Where(g => g.IDPlatform == idhdh).ToList();
            return PartialView(tenhdh);
        }

        public ActionResult Extension_Overall_TenTheLoai(int idtheloai)
        {
            var theloai = database.CATEGORies.Where(g => g.IDCat == idtheloai).ToList();
            return PartialView(theloai);
        }
        //TRANG PHÂN LOẠI
        public ActionResult Classify()
        {
            return View();
        }

        //--------------------------DETAILS OVERALL FOR SEARCH--------------------------
        public ActionResult Details_Overall(int id)
        {
            var ud = database.OVERALLs.FirstOrDefault(s => s.IDOverall == id);
            return View(ud);
        }
        private List<OVERALL> LayUngDungMoi(int soluong)
        {
            return database.OVERALLs.OrderByDescending(ud => ud.UpdateDate).Take(soluong).ToList();
        }
        private List<OVERALL> LayUngDungTheoDanhGia(int soluong)
        {
            return database.OVERALLs.OrderByDescending(ud => ud.Rating).Take(soluong).ToList();
        }

        public ActionResult Details_Overall_TheoDanhGia()
        {
            var dsUngDungDeXuat = LayUngDungTheoDanhGia(8);
            return PartialView(dsUngDungDeXuat);
        }

        public ActionResult Details_Overall_Moi()
        {
            var dsGameMoi = LayUngDungMoi(8);
            return PartialView(dsGameMoi);
        }

        public ActionResult Details_Overall_CungTheLoai(int idtheloai)
        {
            var dsTheLoai = database.OVERALLs.Where(ud => ud.IDCat == idtheloai).ToList();
            return PartialView(dsTheLoai);
        }



        public ActionResult Extension_Game_Slide()
        {
            var game = LayGameMoi(5);
            return PartialView(game);
        }

        public ActionResult Page_FAQ()
        {
            return View();
        }
        public ActionResult SP()
        {
            return View();
        }





    }
}