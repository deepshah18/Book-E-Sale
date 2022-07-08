using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/publisher")]
    public class PublisherController : ControllerBase
    {
        private readonly PublisherRepository _publisherRepository = new PublisherRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetPublishers(string keyword)
        {
            List<Publisher> publisher = _publisherRepository.GetPublishers(keyword);
            IEnumerable<PublisherModel> publisherModels = publisher.Select(c => new PublisherModel(c));
            return Ok(publisherModels);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPublisher(int id)
        {
            try
            {
                var publisher = _publisherRepository.GetPublisher(id);
                if (publisher == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), publisher);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }

        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddPublisher(PublisherModel model)
        {
            if (model == null)
                return BadRequest();

            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact
            };
            publisher = _publisherRepository.AddPublisher(publisher);
            PublisherModel publisherModel = new PublisherModel(publisher);
            return Ok(publisherModel);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdatePublisher(PublisherModel model)
        {
            if (model == null)
                return BadRequest();

            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact
            };
            publisher = _publisherRepository.UpdatePublisher(publisher);
            PublisherModel publisherModel = new PublisherModel(publisher);
            return Ok(publisherModel);
        }

        [HttpDelete]
        [Route("delete/{id}")] //"/{id}"
        public IActionResult DeletePublisher(int id)
        {
            if (id == 0)
                return BadRequest();

            bool response = _publisherRepository.DeletePublisher(id);
            return Ok(response);
        }
    }
}
