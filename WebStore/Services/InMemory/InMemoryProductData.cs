using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IEnumerable<Product> querry = TestData.Products;

            if (Filter?.SectionId != null)
                querry = querry.Where(p => p.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                querry = querry.Where(p => p.BrandId == Filter.BrandId);
            return querry;
        }

        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
