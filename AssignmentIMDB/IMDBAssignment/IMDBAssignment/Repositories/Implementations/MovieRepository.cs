using IMDBAssignment.Models.DAL;
using IMDBAssignment.Models.Requests;
using IMDBAssignment.Repositories.Context;
using IMDBAssignment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Repositories.Implementations
{
    /// <summary>
    /// Movie Repository
    /// </summary>
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        public MovieRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
