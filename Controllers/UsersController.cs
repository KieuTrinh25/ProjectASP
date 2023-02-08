using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Data;
using ProjectASP.Models;

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
        public async Task<IActionResult> Login(User login)
        {
            if (ModelState.IsValid)
            {
                var result = _context.users.FirstOrDefault(x => x.username == login.username);
                if (result != null)
                {
                    if (result.password == login.password)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    } 
                }
                ModelState.AddModelError(string.Empty, "Tên Đăng Nhập Hoặc Mật Khẩu Không Chính Xác!!!");
            }
            return View(login);
        }

    }
}
