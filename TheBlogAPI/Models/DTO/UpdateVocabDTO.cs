﻿using System;
namespace TheBlogAPI.Models.DTO
{
	public class UpdateVocabDTO
	{
        public string Word { get; set; }

        public string? VN { get; set; }

        public string? EN { get; set; }

        public string? Sound { get; set; }

        public string? Pronunciation { get; set; }

        public string? Image { get; set; }

        public string? Example { get; set; }

        public Guid? SetId { get; set; }
    }
}
