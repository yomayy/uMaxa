using Shop.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Shop.Application.CategoriesAdmin
{
	[Service]
	public class DeleteCategory
	{
		private ICategoryManager _categoryManager;

		public DeleteCategory(ICategoryManager categoryManager) {
			_categoryManager = categoryManager;
		}

		public Task<int> DoAsync(Guid id) {
			return _categoryManager.DeleteCategory(id);
		}
	}
}
