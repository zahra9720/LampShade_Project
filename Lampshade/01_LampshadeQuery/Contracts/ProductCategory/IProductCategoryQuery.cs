using System.Collections.Generic;

namespace _01_LampshadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQuerModel GetProductCategoryWithProdctsBy(string slug);
        List<ProductCategoryQuerModel> GetProductCategories();
        List<ProductCategoryQuerModel> GetProductCategoriesWithProducts();
    }
}
