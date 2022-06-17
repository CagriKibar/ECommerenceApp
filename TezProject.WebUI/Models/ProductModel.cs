using Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TezProject.WebUI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Lütfen Ürün İsmini doldurunuz.")]
        
        public string ProductName { get; set; }
        [Required(ErrorMessage ="Lütfen Ürünle İlgili Fotoğraf Yükleyiniz.")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage ="Lütfen Açıklama Kısmını Boş Bırakmayınız.")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Ürün ismi maksimum 250, minimum 10 karakter olmadılır.")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Fiyat Belirtiniz")]
        public decimal? Price { get; set; }
        public List<Category> SelectedCategories { get; set; }


    }
}
