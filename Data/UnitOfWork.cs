using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HappyMilkContext _context;
        private IDbContextTransaction _transaction = null!;

        public UnitOfWork(HappyMilkContext context)
        {
            _context = context;
        }

        public ICategoryRepository _category = null!;

        public ICategoryRepository Category
        {
            get { return _category ??= new CategoryRepository(_context); }
        }

        public IProductCategoryRepository _productCateogry = null!;

        public IProductCategoryRepository ProductCategory
        {
            get { return _productCateogry ??= new ProductCategoryRepository(_context); }
        }

        public ICartRepository _cart = null!;

        public ICartRepository Cart
        {
            get { return _cart ??= new CartRepository(_context); }
        }

        public ICartItemRepository _cartItem = null!;

        public ICartItemRepository CartItem
        {
            get { return _cartItem ??= new CartItemRepository(_context); }
        }

        public IOrderRepository _order = null!;

        public IOrderRepository Order
        {
            get { return _order ??= new OrderRepository(_context); }
        }

        public IOrderDetailRepository _orderDetail = null!;

        public IOrderDetailRepository OrderDetail
        {
            get { return _orderDetail ??= new OrderDetailRepository(_context); }
        }

        public IFeedbackRepository _feedback = null!;

        public IFeedbackRepository Feedback
        {
            get { return _feedback ??= new FeedbackRepository(_context); }
        }

        public IAdminRepository _admin = null!;

        public IAdminRepository Admin
        {
            get { return _admin ??= new AdminRepository(_context); }
        }

        public ICustomerRepository _customer = null!;

        public ICustomerRepository Customer
        {
            get { return _customer ??= new CustomerRepository(_context); }
        }

        public IProductRepository _product = null!;

        public IProductRepository Product
        {
            get { return _product ??= new ProductRepository(_context); }
        }

        public ITransactionRepository _eTransaction = null!;
        public ITransactionRepository Transaction
        {
            get { return _eTransaction ??= new TransactionRepository(_context); }
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null!;
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null!;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
