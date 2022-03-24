using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Repositories.Interfaces
{
    /// <summary>
    /// Repository factory interface.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Create a repository instance.
        /// </summary>
        /// <typeparam name="T">The type of repository to created.</typeparam>
        /// <param name="securityContext">The security context.</param>
        /// <returns>An instance of the agent.</returns>
        T Create<T>() where T : class, IRepository;
    }
}
