using IMDBAssignment.Models.DAL;
using IMDBAssignment.Models.Requests;
using IMDBAssignment.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Controllers
{
    [ApiController]
    public class ActorController : BaseController
    {
        #region ctro

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public ActorController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        #endregion

        /// <summary>
        /// List of all Actor.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="500">Returns details of the error that occurred.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpPost("ListActor")]
        public IActionResult ListActor()
        {
            using (var repository = CreateRepository<IActorRepository>())
            {
                using (var scope = repository.BeginTransaction())
                {
                    IEnumerable<Actor> actorList = repository.List();
                    IList<ActorRequest> responseList = new List<ActorRequest>();

                    foreach (var actor in actorList)
                    {
                        responseList.Add(new ActorRequest()
                        {
                            Id = actor.Id,
                            Bio = actor.Bio,
                            ActorName = actor.ActorName,
                            DateBirth = actor.DateBirth,
                            Gender = actor.Gender,
                        });
                    }

                    return Ok(responseList);
                }
            }
        }

        /// <summary>
        /// Add a Actor.
        /// </summary>
        /// <param name="actorResponse">The Actor Add request</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Returns details of the error that occurred.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpPost("AddActor")]
        public IActionResult AddActor([FromBody] ActorRequest actorRequest)
        {
            using (var repository = CreateRepository<IActorRepository>())
            {
                var addedActor = repository.Add(ParseActorResponse(actorRequest));
                repository.SaveChanges();

                return Ok(addedActor.Id);
            }
        }

        /// <summary>
        /// Parse the ActorResponse object.
        /// </summary>
        /// <param name="actorRequest">The object to parse.</param>
        /// <returns>returns the actor object with filled details</returns>
        private Actor ParseActorResponse(ActorRequest actorRequest)
        {
            return new Actor()
            {
                ActorName = actorRequest.ActorName,
                Bio = actorRequest.Bio,
                DateBirth = actorRequest.DateBirth,
                Gender = actorRequest.Gender,
            };
        }

    }
}
