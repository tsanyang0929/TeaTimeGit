using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaTimeDemo.DataAccess.Data;
using TeaTimeDemo.DataAccess.Repository.IRepository;
using TeaTimeDemo.Models;
using TeaTimeDemo.Utility;

namespace TeaTimeDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin+","+SD.Role_Manager)]
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _db;
        //private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)//ICategoryRepository db)  //ApplicationDbContext db)
        {
            // _db = db;
            //_categoryRepo = db;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList(); //_categoryRepo.GetAll().ToList();//_db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) //自訂欄位檢查
            {
                ModelState.AddModelError("name", "類別名稱不能跟顯示順序一致");
            }

            if (ModelState.IsValid)
            {
                //_categoryRepo.Add(obj);
                //_categoryRepo.Save();
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "類別新增成功!"; //顯示訊息
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id); //_categoryRepo.Get(u => u.Id == id);//_db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost] //忘記加會造成送出後，不知道找哪個Edit
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                //_categoryRepo.Update(obj);
                //_categoryRepo.Save();
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "類別編輯成功!"; //顯示訊息
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);//_categoryRepo.Get(u => u.Id == id);//_db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")] //Action與View名稱不同需指定ActionName
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);//_categoryRepo.Get(u => u.Id == id);//_db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            //_categoryRepo.Remove(obj);
            //_categoryRepo.Save();
            TempData["success"] = "類別刪除成功!"; //顯示訊息
            return RedirectToAction("Index");
        }
    }
}
