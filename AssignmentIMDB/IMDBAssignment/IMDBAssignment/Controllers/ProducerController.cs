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
    public class ProducerController : BaseController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public ProducerController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        /// <summary>
        /// List of all Producer.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="500">Returns details of the error that occurred.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpPost("ListProducer")]
        public IActionResult ListProducer()
        {
            using (var repository = CreateRepository<IProducerRepository>())
            {
                using (var scope = repository.BeginTransaction())
                {
                    IEnumerable<Producer> producerList = repository.List();
                    IList<ProducerRequest> responseList = new List<ProducerRequest>();

                    foreach (var producer in producerList)
                    {
                        responseList.Add(new ProducerRequest()
                        {
                            Id = producer.Id,
                            Bio = producer.Bio,
                            ProducerName = producer.ProducerName,
                            CompanyName = producer.CompanyName,
                            DateBirth = producer.DateBirth,
                            Gender = producer.Gender,
                        });
                    }

                    return Ok(responseList);
                }
            }
        }

        /// <summary>
        /// Add a Producer.
        /// </summary>
        /// <param name="producerRequest">The Producer Add request</param>
        /// <response code="200">Success.</response>
        /// <response code="500">Returns details of the error that occurred.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpPost("AddProducer")]
        public IActionResult AddProducer([FromBody] ProducerRequest producerRequest)
        {
            using (var repository = CreateRepository<IProducerRepository>())
            {
                var addedProducer = repository.Add(ParseProducerResponse(producerRequest));
                repository.SaveChanges();

                return Ok(addedProducer.Id);
            }
        }

        /// <summary>
        /// Parse the ProducerResponse object.
        /// </summary>
        /// <param name="producerRequest">The object to parse.</param>
        /// <returns>returns the producer object with filled details</returns>
        private Producer ParseProducerResponse(ProducerRequest producerRequest)
        {
            return new Producer()
            {
                ProducerName = producerRequest.ProducerName,
                CompanyName = producerRequest.CompanyName,
                Bio = producerRequest.Bio,
                DateBirth = producerRequest.DateBirth,
                Gender = producerRequest.Gender,
            };
        }
    }
}
