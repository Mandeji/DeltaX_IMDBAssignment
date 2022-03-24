﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from Scaffolding.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

#nullable disable

namespace IMDBAssignment.Models.DAL
{
    public partial class MovieActor
    {
        /// <summary>
        /// Gets or sets for Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets for Movie Id.
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets for Actor Id.
        /// </summary>
        public int ActorId { get; set; }

        /// <summary> Read-Only Navigation Property to the Actor. </summary>
        public virtual Actor Actor { get; private set; }

        /// <summary> Read-Only Navigation Property to the Movie. </summary>
        public virtual Movie Movie { get; private set; }
    }
}
