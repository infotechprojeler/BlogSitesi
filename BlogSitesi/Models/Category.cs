using System.ComponentModel.DataAnnotations;

namespace BlogSitesi.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Adı"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Resim"), StringLength(100)]
        public string? Image { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)] // ScaffoldColumn(false) kodu admin panelinde ekranları oluştururken bu alanın ekranda oluşturulmasını engeller. Kayıt tarihi işlemini elle girişe kapatıp kayıt esnasında vermek için bu şekilde yapıyoruz.
        public DateTime? CreateDate { get; set; }
    }
}
