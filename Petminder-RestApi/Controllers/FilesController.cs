using Microsoft.AspNetCore.Mvc;

namespace Petminder_RestApi.Controllers
{
     //api/pets
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        // private readonly IPetRepo _repository;
        // private readonly IMapper _mapper;
        // private readonly IAuthenticateRepo _validate;

        // public PetsController(IPetRepo repository, IMapper mapper, IAuthenticateRepo validate)
        // {
        //     _repository = repository;
        //     _mapper = mapper;
        //     _validate = validate;
        // }

        //GET api/pets
        [HttpGet]
        public ActionResult <IEnumerable<PetReadDto>> GetAllUserPets()
        {
            // if (!Request.Headers.ContainsKey("Authorization"))
            // {
            //     return Unauthorized();
            // }

            // var auth = Request.Headers["Authorization"];
            // var accountModel = _validate.GetAccountByToken(auth);

            // if (accountModel == null)
            // { 
            //     return Unauthorized();
            // }

            // var PetItems = _repository.GetAllUserPets(accountModel.Id);

            // return Ok(_mapper.Map<IEnumerable<PetReadDto>>(PetItems));
        }
    }
}