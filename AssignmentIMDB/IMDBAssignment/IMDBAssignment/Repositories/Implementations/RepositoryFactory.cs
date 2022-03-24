using IMDBAssignment.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Repositories.Implementations
{
    public class RepositoryFactory : IRepositoryFactory
    {

        /// <summary>
        /// Defines a mechanism for retrieving a service object; that is, an object that
        /// provides custom support to other objects.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceProvider">The service provider to retrieve the repositories from.</param>
        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Create a repository instance.
        /// </summary>
        /// <typeparam name="T">The type of repository to created.</typeparam>
        /// <param name="securityContext">The security context.</param>
        /// <returns>An instance of the repository.</returns>
        public T Create<T>() where T : class, IRepository
        {
            return _serviceProvider.GetService<T>();
        }

    }
}
