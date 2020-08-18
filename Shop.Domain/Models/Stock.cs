﻿using Shop.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
	public class Stock : DbBase
	{
		public string Description { get; set; }
		public int Quantity { get; set; }

		public Guid ProductId { get; set; }
		public Product Product { get; set; }
	}
}
