using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application.CategoriesAdmin
{
	[Service]
	public class CreateCategory
	{
		private ICategoryManager _categoryManager;

		public CreateCategory(ICategoryManager categoryManager) {
			_categoryManager = categoryManager;
		}

		public async Task<Response> DoAsync(Request request) {
			var category = new Category {
				Name = request?.Name,
				Description = request?.Description
			};
			if(await _categoryManager.CreateCategory(category) <= 0) {
				throw new Exception("Failed to create category");
			}
			return new Response {
				Id = category.Id,
				CreatedOn = category?.CreatedOn,
				ModifiedOn = category?.ModifiedOn,
				Name = category?.Name,
				Description = category?.Description
			};
		}

		public class Request
		{
			public string Name { get; set; }
			public string Description { get; set; }
		}

		public class Response : DbBase
		{
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}
