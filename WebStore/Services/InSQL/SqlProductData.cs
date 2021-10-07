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

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> querry = _db.Products;

            if (Filter?.SectionId != null)
                querry = querry.Where(p => p.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                querry = querry.Where(p => p.BrandId == Filter.BrandId);
            return querry;
        }

        public IEnumerable<Section> GetSections() => _db.Sections;
    }
}
