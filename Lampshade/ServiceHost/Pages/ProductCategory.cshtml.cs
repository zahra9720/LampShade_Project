using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        public ProductCategoryQuerModel ProductCategory;
        private readonly IProductCategoryQuery _productCategoryQuery;
        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }
        public void OnGet(string id)
        {
            ProductCategory = _productCategoryQuery.GetProductCategoryWithProdctsBy(id);
        }
    }
}
