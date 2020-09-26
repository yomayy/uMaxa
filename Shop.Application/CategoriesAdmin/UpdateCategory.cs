using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Shop.Application.CategoriesAdmin
{
	[Service]
	public class UpdateCategory
	{
		private readonly ICategoryManager _categoryManager;

		public UpdateCategory(ICategoryManager categoryManager) {
			_categoryManager = categoryManager;
		}

		public async Task<Response> DoAsync(Request request) {

			var category = _categoryManager.GetCategoryById(request.Id, x => x);

			category.Name = request.Name;
			category.Description = request.Description;
			category.ModifiedOn = DateTime.UtcNow;

			await _categoryManager.UpdateCategory(category);

			return new Response {
				Id = category.Id,
				Name = category.Name,
				Description = category.Description,
				ModifiedOn = category.ModifiedOn
			};
		}

		public class Request
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
		}

		public class Response :DbBase
		{
			public string Name { get; set; }
			public string Description { get; set; }
		}
	}
}
