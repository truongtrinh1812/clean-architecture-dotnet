using System.Linq.Expressions;
using AM.Core.Specification;
using ProductService.Core.Core.Entities;

namespace ProductService.Core.Core.Specs
{
    public class ProductIsInStockSpec : SpecificationBase<Product>
    {
        private readonly Guid _productId;
        private readonly int _quantity;

        public ProductIsInStockSpec(Guid productId, int quantity)
        {
            _productId = productId;
            _quantity = quantity;
        }

        public override Expression<Func<Product, bool>> Criteria =>
            product => product.Id == _productId && product.Active
                                                && product.Quantity >= _quantity;
    }
}