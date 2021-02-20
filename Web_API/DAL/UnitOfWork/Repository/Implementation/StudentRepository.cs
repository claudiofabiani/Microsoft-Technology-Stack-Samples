using DAL.Domain;
using DAL.UnitOfWork.Context;
using DAL.UnitOfWork.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.UnitOfWork.Repository.Implementation
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository, IDisposable
    {
        private EFContext _context;

        public StudentRepository(EFContext context):base (context)
        {
            _context = context;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
