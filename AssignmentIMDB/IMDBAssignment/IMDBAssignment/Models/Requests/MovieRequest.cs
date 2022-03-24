using IMDBAssignment.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Models.Requests
{
    public class MovieRequest
    {
        /// <summary>
        /// Gets or sets for Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets for Movie Name.
        /// </summary>
        public string MovieName { get; set; }

        /// <summary>
        /// Gets or sets for Date Release.
        /// </summary>
        public DateTime DateRelease { get; set; }

        /// <summary>
        /// Gets or sets for Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets for Poster Path.
        /// </summary>
        public string PosterPath { get; set; }

        /// <summary>
        /// Get or Set Producer.
        /// </summary>
        public ProducerRequest Producer { get; set; }

        /// <summary>
        /// Get or Set Actors list.
        /// </summary>
        public IEnumerable<ActorRequest> Actors { get; set; }
    }
}
