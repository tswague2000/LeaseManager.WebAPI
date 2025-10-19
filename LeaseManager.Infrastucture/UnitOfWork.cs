using LeaseManager.Core.FrameWork.Interface;
using LeaseManager.Core.Infrastuctures.Data;
using LeaseManager.Infrastucture.Interfaces;
using LeaseManager.WebAPI.Application.Repository;
using Microsoft.EntityFrameworkCore.Storage;


namespace LeaseManager.Core.FrameWork
{
    /// <summary>
    /// Implémente le pattern Unit of Work pour gérer les transactions et le cycle de vie du DbContext.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Champs privés
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;
        private bool _disposed = false;
        #endregion

        #region champs publics
        public IDocumentRepository DocumentRepository { get; }
        public ILeaseRepository LeaseRepository { get; }
        public IPropertyRepository PropertyRepository { get; }
        public IOwnerRepository OwnerRepository { get; }
        public ITenantRepository TenantRepository { get; }
        public IMaintenanceRequestRepository MaintenanceRequestRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IPropertyImageRepository PopertyImageRepository { get; }

        #endregion

        #region Constructeur
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            DocumentRepository = new DocumentRepository(_context);
            LeaseRepository = new LeaseRepository(_context);
            PropertyRepository = new PropertyRepository(_context);
            OwnerRepository = new OwnerRepository(_context);
            TenantRepository = new TenantRepository(_context);
            MaintenanceRequestRepository = new MaintenanceRequestRepository(_context);
            PaymentRepository = new PaymentRepository(_context);
            PopertyImageRepository = new PropertyImageRepository(_context);


        }
        #endregion

        #region Méthodes principales

        /// <summary>
        /// Démarre une nouvelle transaction.
        /// </summary>
        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Sauvegarde les modifications dans la base de données.
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Valide la transaction en cours.
        /// </summary>
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        /// <summary>
        /// Annule la transaction en cours.
        /// </summary>
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        #endregion

        #region IDisposable
        /// <summary>
        /// Libère les ressources non managées et le DbContext.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
