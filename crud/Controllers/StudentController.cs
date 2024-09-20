using Tutorial.Data;
using Tutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace Tutorial.Controllers
{
    public class studentController : Controller
    {
        private readonly ApplicationDBContext _db;

        public studentController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult index()
        {
            IEnumerable <Student> allStudent = _db.Students;
            return View(allStudent);
        }
        // GET METHOD
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create(Student obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(obj);
        }

        public IActionResult edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            var obj = _db.Students.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult edit(Student obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(obj);
        }

        public IActionResult delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //ค้นหาข้อมูล
            var obj = _db.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Students.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
