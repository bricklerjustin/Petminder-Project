using System.Linq;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Petminder_RestApi.Data;
using Petminder_RestApi.Dtos;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Controllers
{
    //api/authenticate
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateRepo _repository;
        private readonly IMapper _mapper;

        public AuthenticateController(IAuthenticateRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //This is a bad way to do authentication, needs to be fixed when I have more time
        //GET api/authenticate
        [HttpGet]
        public ActionResult <AuthenticateReadDto> GetUserToken()
        {
            var re = Request;
            var headers = re.Headers;
            Users user = null;

            if (headers.ContainsKey("username") && headers.ContainsKey("password"))
            {
                var username =  headers["username"];
                var password =  headers["password"];
                
                user = _repository.GetUserByUserAuth(username, password);
            }
            else
            {               
                return BadRequest("The request must contain 'username' and 'password' headers");
            }

            if (user != null)
            {
                return _mapper.Map<AuthenticateReadDto>(user);
            }

            return Unauthorized();
        }

        [HttpGet("token")]
        public ActionResult ValidateToken()
        {
            var re = Request;
            var headers = re.Headers;
            var auth = "";

            if (headers.ContainsKey("Authorization"))
            {
                auth = headers["Authorization"];
            }
            else
            {
                return BadRequest();
            }

            var user = _repository.GetUserByToken(auth);

            if (user == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }

}
