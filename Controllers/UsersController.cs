using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Data;
using ProjectASP.Models;
using System;

namespace ProjectASP.Controllers
{
    public class UsersController : Controller
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            // kiểm tra dữ liệu hợp lệ
            if (ModelState.IsValid)
            {
                // trả về 1 object đúng username từ user
                var result = _context.users.FirstOrDefault(x => x.username == user.username);
                // nếu có trả về user
                if (result != null)
                {
                    // nếu đúng mật khẩu
                    if (result.password == user.password)
                    {
                        // trả về trang admin
                        TempData["_user"] = user.username; // giới hạn chỉ truyền đc kiểu tham trị chơ object qua là null
                        return RedirectToAction("Index", "Dashboard");
                    } 
                }
                ModelState.AddModelError(string.Empty, "Tên Đăng Nhập Hoặc Mật Khẩu Không Chính Xác!!!");
            }
            // sai thì giữ nguyên form login
            return View(user);
        }

    }
}
