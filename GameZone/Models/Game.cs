﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Models
{
	public class Game : BaseEntity
	{
		[MaxLength(2500)]
		public string Description { get; set; } = string.Empty;

		[MaxLength(250)]
		public string Cover { get; set; } = string.Empty;
		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
		public Category Category { get; set; } = default!;
		public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();

	}
}
