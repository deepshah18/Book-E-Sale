using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository _repository = new UserRepository();
    

        [HttpGet]
        [Route("list")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            try
            {
                var users = _repository.GetUsers(pageIndex, pageSize, keyword);
                if (users == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), users);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _repository.GetUser(id);
                if (user == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(UserModel model)
        {
            try
            {
                if (model != null)
                {
                    var user = _repository.GetUser(model.Id);
                    if (user == null)
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                    user.Firstname = model.Firstname;
                    user.Lastname = model.Lastname;
                    user.Email = model.Email;

                    var isSaved = _repository.UpdateUser(user);
                    if (isSaved)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail updated successfully");
                    }
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    var user = _repository.GetUser(id);
                    if (user == null)
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                    var isDeleted = _repository.DeleteUser(user);
                    if (isDeleted)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail deleted successfully");
                    }
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

       // [ProducesResponseType(typeof(ListResponse<RoleModel>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("roles")]
        public IActionResult GetRoles()
        {
           
            var roles = _repository.GetRoles();
            
            ListResponse<RoleModel> listResponse = new()
            {
                Results = roles.Results.Select(c => new RoleModel(c)),
                TotalRecords = roles.TotalRecords,
            };
            return Ok(listResponse);
        }
    }
}



/* public IActionResult GetRoles()
        {
            try
            {
                var roles = _repository.GetRoles();
                //RoleModel roleModel = new RoleModel(roles);
                if (roles == null)
                    //return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "No Roles Available");
                    return BadRequest();

                // return StatusCode(HttpStatusCode.OK.GetHashCode(), roles);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }*/