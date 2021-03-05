using DAL.Domain;
using DAL.UnitOfWork.Context;
using DAL.UnitOfWork.Repository.Implementation;
using DAL.UnitOfWork.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UnitOfWork
{
    // referenza https://referbruv.com/blog/posts/understanding-and-implementing-unitofwork-pattern-in-aspnet-core
    // devono condividere lo stesso context.
    public class UnitOfWork : IDisposable
    {
        private EFContext _context;
        private IStudentRepository _studentRepository;

        public UnitOfWork(EFContext context)
        {
            _context = context;
            _studentRepository = new StudentRepository(_context);
        }

        public IStudentRepository StudentRepository
        {
            get
            {
                return _studentRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
