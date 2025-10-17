using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Core.FrameWork.Interface
{
    
        /// <summary>
        /// Définit le contrat pour la gestion des transactions (Unit of Work).
        /// Regroupe toutes les opérations de persistance dans une seule unité de travail.
        /// </summary>
        public interface IUnitOfWork : IDisposable
        {
            /// <summary>
            /// Sauvegarde toutes les modifications en attente dans la base de données.
            /// </summary>
            Task<int> SaveChangesAsync();

            /// <summary>
            /// Committe la transaction en cours.
            /// </summary>
            Task CommitAsync();

            /// <summary>
            /// Annule la transaction en cours.
            /// </summary>
            Task RollbackAsync();
        }
    }

