using System.ComponentModel.DataAnnotations;

namespace BlogSitesi.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!"), StringLength(150)]
        public string Title { get; set; }
        [Display(Name = "İçerik")]
        public string? Content { get; set; }
        [Display(Name = "Yazar"), StringLength(50)]
        public string? Author { get; set; }
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Resim"), StringLength(100)]
        public string? Image { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "Kategori")]
        public Category? Category { get; set; }
    }
}
