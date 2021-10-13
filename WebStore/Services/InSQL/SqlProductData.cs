using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db)
        {
            _db = db;
        }

        public Brand GetBrandById(int Id) => _db.Brands.SingleOrDefault(b => b.Id == Id);

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public Product GetProductById(int Id)
        {
            return _db.Products
                .Include(p=>p.Brand)
                .Include(p=>p.Section)
                .FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> querry = _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section);

            if (Filter?.SectionId != null)
                querry = querry.Where(p => p.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                querry = querry.Where(p => p.BrandId == Filter.BrandId);
            return querry;
        }

        public Section GetSectionById(int Id) => _db.Sections.SingleOrDefault(s => s.Id == Id);

        public IEnumerable<Section> GetSections() => _db.Sections;
    }
}
