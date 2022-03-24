using IMDBAssignment.Models.DAL;
using IMDBAssignment.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Repositories.Interfaces
{
    /// <summary>
    /// Movie Repository interface.
    /// </summary>
    interface IMovieRepository : IRepository<Movie>
    {
    }
}
