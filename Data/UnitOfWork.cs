﻿using Data.Repositories.Implementations;
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
