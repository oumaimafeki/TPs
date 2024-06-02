using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using TP2.Models;
using TP2.Models.Repositories;
using TP2.ViewModels;

namespace TP2.Controllers
{
	public class ProductController : Controller
	{
		private readonly IRepository<Product> ProductRepository;
		private readonly IWebHostEnvironment hostingEnvironment;

		public ProductController(IRepository<Product> ProdRepository, IWebHostEnvironment hostingEnvironment)
		{
			ProductRepository = ProdRepository;
			this.hostingEnvironment = hostingEnvironment;
		}

		public ActionResult Index()
		{
			var Products = ProductRepository.GetAll();
			return View("Index", Products);
		}

		public ActionResult Details(int id)
		{
			var Product = ProductRepository.Get(id);
			return View(Product);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				string uniqueFileName = null;
				if (model.ImagePath != null)
				{
					string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
					uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
					string filePath = Path.Combine(uploadsFolder, uniqueFileName);
					model.ImagePath.CopyTo(new FileStream(filePath, FileMode.Create));
				}
				Product newProduct = new Product
				{
					Désignation = model.Désignation,
					Prix = model.Prix,
					Quantite = model.Quantite,
					Image = uniqueFileName
				};
				ProductRepository.Add(newProduct);
				return RedirectToAction("details", new { id = newProduct.Id });
			}
			return View();
		}

		public ActionResult Edit(int id)
		{
			Product product = ProductRepository.Get(id);
			EditViewModel productEditViewModel = new EditViewModel
			{
				Id = product.Id,
				Désignation = product.Désignation,
				Prix = product.Prix,
				Quantite = product.Quantite,
				ExistingImagePath = product.Image
			};
			return View(productEditViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditViewModel model)
		{
			if (ModelState.IsValid)
			{
				Product product = ProductRepository.Get(model.Id);
				product.Désignation = model.Désignation;
				product.Prix = model.Prix;
				product.Quantite = model.Quantite;

				if (model.ImagePath != null)
				{
					if (model.ExistingImagePath != null)
					{
						string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
						System.IO.File.Delete(filePath);
					}
					product.Image = ProcessUploadedFile(model);
				}

				Product updatedProduct = ProductRepository.Update(product);
				if (updatedProduct != null)
					return RedirectToAction("Index");
				else
					return NotFound();
			}
			return View(model);
		}

		[NonAction]
		private string ProcessUploadedFile(EditViewModel model)
		{
			string uniqueFileName = null;
			if (model.ImagePath != null)
			{
				string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					model.ImagePath.CopyTo(fileStream);
				}
			}
			return uniqueFileName;
		}

		public ActionResult Delete(int id)
		{
			var Product = ProductRepository.Get(id);
			return View(Product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			try
			{
				ProductRepository.Delete(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
		public ActionResult Search(string term)
		{
			if (string.IsNullOrEmpty(term))
			{
				return RedirectToAction("Index");
			}

			var result = ProductRepository.Search(term);
			return View("Index", result);
		}


	}
}
