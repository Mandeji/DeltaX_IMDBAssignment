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
    /// Actor Repository
    /// </summary>
    public class ActorRepository : RepositoryBase<Actor>, IActorRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        public ActorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
