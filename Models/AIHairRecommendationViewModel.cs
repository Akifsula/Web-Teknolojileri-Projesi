﻿using System.ComponentModel.DataAnnotations;

namespace KuaforYonetim.ViewModels
{
    public class AIHairRecommendationViewModel
    {
        [Required(ErrorMessage = "Lütfen bir geçerli fotoğraf URL'si girin.")]
        [RegularExpression(@"(https?:\/\/.*\.(?:png|jpg|jpeg))", ErrorMessage = "Geçerli bir resim URL'si giriniz.")]
        public string ImageUrl { get; set; }

      
        public string? EditingType { get; set; }

        public string? ColorDescription { get; set; }

        public string? HairstyleDescription { get; set; }

        // Sonuçlar için eklenen özellikler
        // Sonuçlar için eklenen özellikler
        public string? OriginalImageUrl { get; set; } // Opsiyonel hale getirildi
        public string? ProcessedImageUrl { get; set; } // Opsiyonel hale getirildi
        public string? Suggestions { get; set; } // Opsiyonel hale getirildi

        public string? ResultImageUrl { get; set; } // Opsiyonel hale getirildi
    }
}