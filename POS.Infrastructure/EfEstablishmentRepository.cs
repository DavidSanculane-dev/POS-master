﻿using System;
using System.Data;
using System.Linq;
using POS.Domain.Abstract;
using POS.Domain.Model;

namespace POS.Infrastructure
{
    public class EfEstablishmentRepository : GenericRepository<EfDbContext, Establishment>, IEstablishmentRepository
    {
        #region Fields

        private readonly EfDbContext context = new EfDbContext();

        #endregion

        #region Public Properties

        public IQueryable<Establishment> Establishments
        {
            get { return context.Establishments; }
        }

        #endregion

        private bool _disposed;

        #region IEstablishmentRepository Members

        public void DeleteEstablishment(Establishment establishment)
        {
            context.Establishments.Remove(establishment);
            context.SaveChanges();
        }

        public void SaveEstablishment(Establishment establishment)
        {
            if (establishment.EstablishmentId == 0)
            {
                context.Establishments.Add(establishment);
            }
            else
            {
                context.Entry(establishment).State = (System.Data.Entity.EntityState)EntityState.Modified;
            }

            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}