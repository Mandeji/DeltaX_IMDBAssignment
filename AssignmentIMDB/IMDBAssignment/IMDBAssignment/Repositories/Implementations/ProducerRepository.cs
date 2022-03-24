using IMDBAssignment.Models.DAL;
using IMDBAssignment.Repositories.Context;
using IMDBAssignment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Repositories.Implementations
{
    /// <summary>
    /// Producer Repository
    /// </summary>
    public class ProducerRepository : RepositoryBase<Producer>, IProducerRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        public ProducerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    
    }
}
