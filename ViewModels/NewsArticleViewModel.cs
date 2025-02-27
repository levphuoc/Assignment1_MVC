using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment1_MVC.ViewModels
{
    public class NewsArticleViewModel
    {
        public int NewsArticleID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string NewsTitle { get; set; }

        public string Headline { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string NewsContent { get; set; }

        public string NewsSource { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryID { get; set; }

        public bool NewsStatus { get; set; }

      
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public string CreatedByName { get; set; } // Nếu cần hiển thị tên người tạo
    }
}
