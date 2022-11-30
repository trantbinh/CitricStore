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
        public ActionResult HomePage()
        {
            var kh = database.CUSTOMERs.ToList();
            return View(kh);
        }

        private List<OVERALL> GetGame_UpdateDate(int quantity)
        {
            return database.OVERALLs.Where(s => s.SoftOrGame == "Game").OrderByDescending(game => game.UpdateDate).Take(quantity).ToList();
        }
        public ActionResult HomePage_NewGame()
        {
            var listNewGame = GetGame_UpdateDate(8);
            return PartialView(listNewGame);
        }

        //Lọc Game theo đánh giá -> Game Đề Xuất
        private List<OVERALL> GetGame_Rating(int quantity)
        {
            return database.OVERALLs.Where(s => s.SoftOrGame == "Game").OrderByDescending(game => game.Rating).Take(quantity).ToList();
        }
        public ActionResult HomePage_SuggestGame()
        {
            var listSuggestGame = GetGame_Rating(8);
            return PartialView(listSuggestGame);
        }

        //Lọc App theo ngày cập nhật -> App Mới
        private List<OVERALL> GetSoft_UpdateDate(int quantity)
        {
            return database.OVERALLs.Where(s => s.SoftOrGame == "Soft").OrderByDescending(app => app.UpdateDate).Take(quantity).ToList();
        }
        public ActionResult HomePage_NewSoftware()
        {
            var listNewSoft = GetSoft_UpdateDate(8);
            return PartialView(listNewSoft);
        }

        //Lọc App theo đánh giá -> App Đề Xuất
        private List<OVERALL> GetSoft_Rating(int quantity)
        {
            return database.OVERALLs.Where(s => s.SoftOrGame == "Soft").OrderByDescending(app => app.Rating).Take(quantity).ToList();
        }
        public ActionResult HomePage_SuggestSoftware()
        {
            var listSuggestSoft = GetSoft_Rating(8);
            return PartialView(listSuggestSoft);
        }

        //Dropdown Navbar
        public ActionResult HomePage_GetCategorySoftList()
        {
            var listCat = database.CATEGORies.Where(s => s.SoftOrGame == "Soft").ToList();
            return PartialView(listCat);
        }
        public ActionResult HomePage_GetCategoryGameList()
        {
            var listCat = database.CATEGORies.Where(s => s.SoftOrGame == "Game").ToList();
            return PartialView(listCat);
        }
        
        //Trang theo thể loại ứng dụng
        public ActionResult Page_Category(int id, string sortOrder)
        {
            ViewBag.RateSortParm = String.IsNullOrEmpty(sortOrder) ? "rate_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var apps = from s in database.OVERALLs
                        where s.IDCat == id
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
                    apps = apps.OrderBy(s => s.IDOverall);
                    break;
            } 
            return View(apps);
        }


        //Trang tìm kiếm
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

        //--------------------------GET NAME--------------------------

        //Lấy tên của nhà phát hành
        public ActionResult Extension_GetNamePublisher(int idnph)
        {
            var name = database.PUBLISHERs.Where(g => g.IDPublisher == idnph).ToList();
            return PartialView(name);
        }

        //Lấy tên ngôn ngữ
        public ActionResult Extension_GetNameLanguage(int idlang)
        {
            var name = database.LANGUAGEs.Where(g => g.IDLanguage == idlang).ToList();
            return PartialView(name);
        }

        //Lấy tên hệ điều hành
        public ActionResult Extension_GetNamePlatform(int idplat)
        {
            var name = database.PLATFORMs.Where(g => g.IDPlatform == idplat).ToList();
            return PartialView(name);
        }

        public ActionResult Extension_GetNameCategory(int idcat)
        {
            var name = database.CATEGORies.Where(g => g.IDCat == idcat).ToList();
            return PartialView(name);
        }

        //--------------------------DETAILS OVERALL--------------------------
        public ActionResult Details_Overall(int id)
        {
            var ud = database.OVERALLs.FirstOrDefault(s => s.IDOverall == id);
            return View(ud);
        }
        private List<OVERALL> GetOverall_UpdateDate(int quantity)
        {
            return database.OVERALLs.OrderByDescending(ud => ud.UpdateDate).Take(quantity).ToList();
        }
        public ActionResult Details_Overall_GetNew()
        {
            var listNewOverall = GetOverall_UpdateDate(8);
            return PartialView(listNewOverall);
        }

        private List<OVERALL> GetOverall_Rating(int quantity)
        {
            return database.OVERALLs.OrderByDescending(ud => ud.Rating).Take(quantity).ToList();
        }

        public ActionResult Details_Overall_GetSuggest()
        {
            var listSuggestApp = GetOverall_Rating(8);
            return PartialView(listSuggestApp);
        }

        public ActionResult Details_Overall_SameCategory(int idcat)
        {
            var listCat = database.OVERALLs.Where(ud => ud.IDCat == idcat).ToList();
            return PartialView(listCat);
        }

        public ActionResult Extension_Slide()
        {
            var game = GetOverall_UpdateDate(5);
            return PartialView(game);
        }

        public ActionResult Page_FAQ()
        {
            return View();
        }
    }
}