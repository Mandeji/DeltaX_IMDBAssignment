using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Models.Requests
{
    public class ActorRequest
    {

        /// <summary>
        /// Gets or sets for Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets for ActorName
        /// </summary>
        public string ActorName { get; set; }

        /// <summary>
        /// Gets or sets for DateBirth
        /// </summary>
        public DateTime DateBirth { get; set; }

        /// <summary>
        /// Gets or sets for Bio
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets for Gender
        /// </summary>
        public string Gender { get; set; }

    }
}
