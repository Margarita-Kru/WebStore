using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public Brand GetBrandById(int Id)
        {
            return TestData.Brands.FirstOrDefault(b => b.Id == Id);
        }

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IEnumerable<Product> querry = TestData.Products;

            if (Filter?.SectionId != null)
                querry = querry.Where(p => p.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                querry = querry.Where(p => p.BrandId == Filter.BrandId);
            return querry;
        }

        public Product GetProductById(int Id)
        {
            return TestData.Products.FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Section> GetSections() => TestData.Sections;

        public Section GetSectionById(int Id)
        {
            return TestData.Sections.FirstOrDefault(s => s.Id == Id);
        }
    }
}
