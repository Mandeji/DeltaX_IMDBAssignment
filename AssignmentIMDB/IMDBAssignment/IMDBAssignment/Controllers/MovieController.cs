using IMDBAssignment.Models;
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
    /// <summary>
    /// Movie controller.
    /// </summary>
    [ApiController]
    public class MovieController : BaseController
    {
        #region ctor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MovieController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }

        #endregion

        /// <summary>
        /// List of all movies.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="500">Returns details of the error that occurred.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpPost("ListMovies")]
        public IActionResult ListMovies()
        {
            using (var repository = CreateRepository<IMovieRepository>())
            {
                using (var scope = repository.BeginTransaction())
                {
                    IEnumerable<Movie> movieList = repository.List();

                    IList<MovieRequest> responseList = new List<MovieRequest>();

                    foreach (var movie in movieList)
                    {
                        ProducerRequest producer = GetProducerById(movie.ProducerId);
                        IEnumerable<ActorRequest> actors = ListActorsByMovieId(movie.Id);
                        var movieReqest = serializeMovieRequest(movie, actors, producer);

                        responseList.Add(movieReqest);
                    }
                    scope.Commit();
                    return Ok(responseList);
                }
            }
        }

        /// <summary>
        /// Get a movie.
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Returns details of the error that occurred.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpGet("GetMovie/{id}")]
        public IActionResult GetMovie(int id)
        {
            using (var repository = CreateRepository<IMovieRepository>())
            {
                var movie = repository.Search(x => x.Id == id).FirstOrDefault();
                if (movie == null)
                {
                    return BadRequest("There No record presend with this ID");
                }
                var response = serializeMovieRequest(movie, ListActorsByMovieId(movie.Id), GetProducerById(movie.ProducerId));
                return Ok(response);
            }
        }

        /// <summary>
        /// Add a movie.
        /// </summary>
        /// <param name="movieRequest">The movie Add request</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Returns details of the error that occurred.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpPost("AddMovie")]
        public IActionResult AddMovie([FromBody] MovieRequest movieRequest)
        {
            using (var repository = CreateRepository<IMovieRepository>())
            {
                using (var scope = repository.BeginTransaction())
                {
                    var addedMovie = repository.Add(ParseMovieRequest(movieRequest));
                    repository.SaveChanges();

                    //Add Actors and Movie relation to Table
                    AddUpdateMovieActors(addedMovie.Id, movieRequest.Actors);

                    scope.Commit();
                    return Ok(addedMovie.Id);
                }
            }
        }

        /// <summary>
        /// Update movie.
        /// </summary>
        /// <param name="id">The movie Id to update</param>
        /// <param name="movieRequest">The movie Update request</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Returns details of the error that occurred.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpPut("UpdateMovie")]
        public IActionResult UpdateMovie([FromBody] MovieRequest movieRequest)
        {
            using (var repository = CreateRepository<IMovieRepository>())
            {
                using (var scope = repository.BeginTransaction())
                {
                    var movieToUpdate = ParseMovieRequest(movieRequest);
                    movieToUpdate.Id = movieRequest.Id; 
                    repository.Update(movieToUpdate);
                    repository.SaveChanges();

                    //Update Actors and Movie relation to Table
                    AddUpdateMovieActors(movieRequest.Id, movieRequest.Actors, true);
                    scope.Commit();
                }
            }
            return Ok();
        }

        /// <summary>
        /// Delete movie.
        /// </summary>
        /// <param name="id">The movie Id to delete</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Returns details of the error that occurred.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            using (var repository = CreateRepository<IMovieRepository>())
            {
                using (var scope = repository.BeginTransaction())
                {
                    Movie movie = repository.Search(x => x.Id == id).FirstOrDefault();
                    if (movie == null)
                        return BadRequest("There No record presend with this ID");

                    //Removie Movie Actor Entry .
                    removeMovieActor(id);
                    repository.Delete(movie);

                    repository.SaveChanges();
                    scope.Commit();
                }
            }
            return Ok();
        }

        #region Private Methods

        /// <summary>
        /// Adds/Update the movie and Actors realtion in MovieActors Table
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="actorsList"></param>
        private void AddUpdateMovieActors(int movieId, IEnumerable<ActorRequest> actorsList, bool isUpdate = false)
        {
            using (var repository = CreateRepository<IMovieActorRepository>())
            {
                if (isUpdate)
                {
                    var movieActorsList = repository.Search(x => x.MovieId == movieId);
                    actorsList = actorsList.Where(x => !movieActorsList.Any(y => y.ActorId == x.Id));
                }

                foreach (var actor in actorsList)
                {
                    MovieActor movieActor = new MovieActor()
                    {
                        ActorId = actor.Id,
                        MovieId = movieId
                    };
                    repository.Add(movieActor);
                }
                repository.SaveChanges();
            }
        }

        /// <summary>
        /// Parse the MovieRequest object.
        /// </summary>
        /// <param name="movieRequest">The object to parse.</param>
        /// <returns>returns the movie object with filled details</returns>
        private Movie ParseMovieRequest(MovieRequest movieRequest)
        {
            return new Movie()
            {
                MovieName = movieRequest.MovieName,
                Description = movieRequest.Description,
                DateRelease = movieRequest.DateRelease,
                ProducerId = movieRequest.Producer.Id,
                PosterPath = movieRequest.PosterPath,
            };
        }

        /// <summary>
        /// Serialize the MovieRequest object from Movie object.
        /// </summary>
        /// <param name="movie">The movie object to serialize with </param>
        /// <param name="actors">Actors list</param>
        /// <param name="producer">Producers object</param>
        /// <returns>returns the movie request object</returns>
        private MovieRequest serializeMovieRequest(Movie movie, IEnumerable<ActorRequest> actors, ProducerRequest producer)
        {
            return new MovieRequest()
            {
                Id = movie.Id,
                DateRelease = movie.DateRelease,
                MovieName = movie.MovieName,
                Description = movie.Description,
                PosterPath = movie.PosterPath,
                Producer = producer,
                Actors = actors
            };
        }

        /// <summary>
        /// Returns the list of <see cref="ActorResponse"/> list matchng the <paramref name="movieId"/>.
        /// </summary>
        /// <param name="movieId">The MovieId to match.</param>
        /// <returns>retrus the list of Actors matched with Movie Id</returns>
        private IEnumerable<ActorRequest> ListActorsByMovieId(int movieId)
        {
            using (var repository = CreateRepository<IMovieActorRepository>())
            {
                var movieActorList = repository.Search(x => x.MovieId == movieId).ToList();

                using (var actorRepository = CreateRepository<IActorRepository>())
                {
                    var actorList = actorRepository.List();

                    var result = (from ma in movieActorList
                                  join a in actorList on ma.ActorId equals a.Id
                                  select new ActorRequest
                                  {
                                      Id = a.Id,
                                      ActorName = a.ActorName,
                                      Bio = a.Bio,
                                      Gender = a.Gender,
                                      DateBirth = a.DateBirth,
                                  }).ToList();
                    return result;
                }
            }
        }
        /// <summary>
        /// Get the Producer by id.
        /// </summary>
        /// <param name="producerId">Id to match.</param>
        /// <returns>retruns the producer object</returns>
        private ProducerRequest GetProducerById(int producerId)
        {
            using (var repository = CreateRepository<IProducerRepository>())
            {
                var producers = repository.Search(x => x.Id == producerId).FirstOrDefault();
                return new ProducerRequest()
                {
                    Id = producers.Id,
                    Bio = producers.Bio,
                    ProducerName = producers.ProducerName,
                    CompanyName = producers.CompanyName,
                    DateBirth = producers.DateBirth,
                    Gender = producers.Gender,
                };
            }
        }

        /// <summary>
        /// Removes the MovieActor Entry matching the Movie Id.
        /// </summary>
        /// <param name="movieId">Movie Id to match.</param>
        private void removeMovieActor(int movieId)
        {
            using (var repository = CreateRepository<IMovieActorRepository>())
            {
                var movieActorsList = repository.Search(x => x.MovieId == movieId);
                foreach (var itemToDelete in movieActorsList)
                {
                    repository.Delete(itemToDelete);
                }
            }
        }

        #endregion
    }
}
