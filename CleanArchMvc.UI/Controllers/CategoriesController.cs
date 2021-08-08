using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchMvc.UI.Controllers
{
	public class ProductsController : Controller
	{

		private readonly IProductService _service;
		public ProductsController(IProductService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var entities = await _service.GetProducts();
			return View(entities);
		}
	}
}
