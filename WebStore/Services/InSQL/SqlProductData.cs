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

            if (Filter?.Ids?.Length > 0)
                querry = querry.Where(product => Filter.Ids.Contains(product.Id));
            else
            {
                if (Filter?.SectionId is { } section_id)
                    querry = querry.Where(p => p.SectionId == section_id);

                if (Filter?.BrandId is { } brand_id)
                    querry = querry.Where(p => p.BrandId == brand_id);
            }

            return querry;
        }

        public Section GetSectionById(int Id) => _db.Sections.SingleOrDefault(s => s.Id == Id);

        public IEnumerable<Section> GetSections() => _db.Sections;
    }
}
