using IMDBAssignment.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Controllers
{
    /// <summary>
    /// Base class for Gateway controllers.
    /// </summary>
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <exception cref="ArgumentNullException">Throws exception if service provider is null</exception>
        public BaseController(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            repositoryFactory = serviceProvider.GetService<IRepositoryFactory>();
        }

        /// <summary>
        /// Get the Repository Factory 
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        /// Create a repository instance.
        /// </summary>
        protected T CreateRepository<T>() where T : class, IRepository
        {
            return repositoryFactory.Create<T>();
        }
    }
}
