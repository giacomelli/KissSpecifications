using System;
using System.Web.Mvc;
using KissSpecifications.Samples.Domain;

namespace KissSpecifications.Samples.Mvc.Controllers
{
	public class CustomersController : Controller
	{		
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Customer customer)
		{
			try
			{
				if (ModelState.IsValid)
				{
					CustomerService.CreateCustomer(customer);
				}

				return View();
			}
			catch(Exception ex)
			{
				ViewBag.ErrorMessage = ex.Message;

				return View();
			}
		}		
	}
}
