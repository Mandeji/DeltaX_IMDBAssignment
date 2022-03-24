using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Models.Requests
{
    public class ProducerRequest
    {
        /// <summary>
        /// Gets or sets for Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets for Producer Name
        /// </summary>
        public string ProducerName { get; set; }

        /// <summary>
        /// Gets or sets for Date Birth.
        /// </summary>
        public DateTime DateBirth { get; set; }

        /// <summary>
        /// Gets or sets for Company Name
        /// </summary>
        public string CompanyName { get; set; }

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
