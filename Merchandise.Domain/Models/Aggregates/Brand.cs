﻿using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class Brand : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
		public string LogoUrl { get; private set; } = string.Empty;
        public string? Description { get; private set; } = string.Empty;

        public Brand() { }

        public Brand(string name, string logo, string? description) : base()
        {
            Name = name;
            LogoUrl = logo;
            Description = description;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string? description)
        {
            Description = description;
        }

        public void SetLogoUrl(string logoUrl)
        {
            LogoUrl = logoUrl;
        }
    }
}
