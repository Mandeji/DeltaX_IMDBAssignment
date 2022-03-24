using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IMDBAssignment.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface.
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Save the changes made to the repository.
        /// </summary>
        void SaveChanges();

    }
}