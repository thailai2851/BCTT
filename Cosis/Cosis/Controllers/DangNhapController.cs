using Cosis.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Cosis.Controllers
{
    public class DangNhapController : Controller
    {
        [HttpGet("/login")]
        public IActionResult Index()
        {
            return View("DangNhap");
        }
       [HttpPost("/login")]
        public async Task<IActionResult> logGin(TaiKhoang tk)
        {
            FormCosisContext context = new FormCosisContext();
            TaiKhoang t = context.TaiKhoang.Find(tk.TaiKhoang1);

            if (t == null)
            {
                TempData["ThongBaoDN"] = "Đăng nhập thất bại";
                return RedirectToAction("Index");
            }
            else
            {
                if (t.MatKhau.Trim() == tk.MatKhau)
                {
                     await SignInUser(t);
                     return RedirectToAction("Index","Home");
                }
                else
                {
                    TempData["ThongBaoDN"] = "Đăng nhập thất bại";
                    return RedirectToAction("Index");
                }
            }
        }
        private async Task SignInUser(TaiKhoang tk)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, tk.TaiKhoang1.Trim()),
                        new Claim(ClaimTypes.Role, tk.IdvaiTro.Trim()),
                    };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
        }
        [HttpGet("/logout")]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
