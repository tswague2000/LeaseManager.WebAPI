using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Infrastucture.Repositories
{
    /// <summary>
    /// Interface générique pour les opérations CRUD sur les entités.
    /// Fournit une abstraction commune pour tous les dépôts.
    /// </summary>
    /// <typeparam name="TEntity">Type de l'entité manipulée.</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Méthodes de récupération
        /// <summary>
        /// Récupère une entité par son identifiant.
        /// </summary>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Récupère toutes les entités.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Récupère les entités correspondant à une condition donnée.
        /// </summary>
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Méthodes de modification
        /// <summary>
        /// Ajoute une nouvelle entité.
        /// </summary>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Met à jour une entité existante.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Supprime une entité.
        /// </summary>
        void Delete(TEntity entity);
        #endregion
    }
}
