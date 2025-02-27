using Assignment1_MVC.ViewModels;
using AutoMapper;
using FUNewsManagementSystem.BLL.DTOs;
using FUNewsManagementSystem.BLL.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assignment1_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAccountService accountService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var accountDTO = await _accountService.AuthenticateAsync(email, password);
            if (accountDTO == null)
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }

            var accountVM = _mapper.Map<SystemAccountViewModel>(accountDTO);
            SetSession("AccountID", accountVM.AccountID);
            SetSession("AccountRole", accountVM.AccountRole);
            SetSession("AccountName", accountVM.AccountName);

            return RedirectToAction("Index", "News");
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

     

        // ----------------- Helper Methods -----------------
        private int? GetSessionInt(string key)
        {
            return _httpContextAccessor.HttpContext?.Session.GetInt32(key);
        }

        private string GetSessionString(string key)
        {
            return _httpContextAccessor.HttpContext?.Session.GetString(key) ?? string.Empty;
        }

        private void SetSession(string key, int value)
        {
            _httpContextAccessor.HttpContext?.Session.SetInt32(key, value);
        }

        private void SetSession(string key, string value)
        {
            _httpContextAccessor.HttpContext?.Session.SetString(key, value);
        }

        private bool IsAdmin() => GetSessionInt("AccountRole") == 0; // Admin role = 0
    }
}
