using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TP2.Models.Repositories
{
	public class SqlProductRepository : IRepository<Product>
	{
		private readonly AppDbContext context;
		private IEnumerable<object> lproducts;
		//private List<Product> lproduct;


		public SqlProductRepository(AppDbContext context)
		{
			this.context = context;
		}

		public Product Add(Product P)
		{
			context.Products.Add(P);
			context.SaveChanges();
			return P;
		}

		public Product Delete(int Id)
		{
			Product P = context.Products.Find(Id);
			if (P != null)
			{
				context.Products.Remove(P);
				context.SaveChanges();
			}
			return P;
		}

		public IEnumerable<Product> GetAll()
		{
			return context.Products;
		}

		public Product Get(int Id)
		{
			return context.Products.Find(Id);
		}

		public Product Update(Product P)
		{
			context.Products.Attach(P);
			context.Entry(P).State = EntityState.Modified;
			context.SaveChanges();
			return P;
		}

		public Product FindByID(int id)
		{
			throw new NotImplementedException();
		}

		public Product Update(int id, Product t)
		{
			throw new NotImplementedException();
		}



		public List<Product> Search(string term)
		{
			if (!string.IsNullOrEmpty(term))
				return context.Products.Where(a => a.Désignation.Contains(term)).ToList();
			else
				return context.Products.ToList();
		}


		public string? FindById(int id)
		{
			throw new NotImplementedException();
		}
	}
}
