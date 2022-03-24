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
    /// Movie Actor Repository
    /// </summary>
    public class MovieActorRepository : RepositoryBase<MovieActor>, IMovieActorRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        public MovieActorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
                
        }
    }
}
