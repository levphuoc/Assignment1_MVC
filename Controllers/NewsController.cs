using Assignment1_MVC.ViewModels;
using AutoMapper;
using FUNewsManagementSystem.BLL.DTOs;
using FUNewsManagementSystem.BLL.Service.IService;
using FUNewsManagementSystem.BLL.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1_MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService; 
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewsController(INewsService newsService, ICategoryService categoryService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(int? page)
        {
            if (!IsStaffOrLecturerOrAdmin()) return RedirectToAction("Login", "Account");

            int pageSize = 10;
            int pageNumber = page ?? 1;
            var newsArticlesDTO = await _newsService.GetNewsArticlesAsync(pageNumber, pageSize);
            var itemsVM = _mapper.Map<List<NewsArticleViewModel>>(newsArticlesDTO.Items.ToList());
            var paginatedListVM = new PaginatedList<NewsArticleViewModel>(itemsVM, newsArticlesDTO.TotalCount, pageNumber, pageSize);
            return View(paginatedListVM);
        }

    

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm, int? page)
        {
            if (!IsStaffOrLecturerOrAdmin()) return RedirectToAction("Login", "Account");
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var newsArticlesDTO = await _newsService.SearchNewsArticlesAsync(searchTerm ?? "", pageNumber, pageSize);
            var itemsVM = _mapper.Map<List<NewsArticleViewModel>>(newsArticlesDTO.Items.ToList());
            var paginatedListVM = new PaginatedList<NewsArticleViewModel>(itemsVM, newsArticlesDTO.TotalCount, pageNumber, pageSize);
            ViewBag.SearchTerm = searchTerm;
            return View("Index", paginatedListVM);
        }
          

        private bool IsAdmin() => GetSessionInt("AccountRole") == 0;
        private bool IsStaff() => GetSessionInt("AccountRole") == 1;
        private bool IsStaffOrLecturerOrAdmin() => new[] { 0, 1, 2 }.Contains(GetSessionInt("AccountRole") ?? -1);

        private int? GetSessionInt(string key) => _httpContextAccessor.HttpContext?.Session.GetInt32(key);
        private string GetSessionString(string key) => _httpContextAccessor.HttpContext?.Session.GetString(key) ?? "";
    }
}