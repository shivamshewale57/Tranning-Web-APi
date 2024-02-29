using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Web_Api.Models.DTO;
using Web_Api.Repositories;
using WebApplication2.Models.Domain;

namespace Web_Api.Controllers
{

    // api Walks
    //[Route("api/[controller]")]
    [Route("api/Walks")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //*********** Create Walk ********
        //POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddWalksRequestDto addWalksRequestDto)
        {
            // Map Dto to Domain model
           var walkDomainModel= mapper.Map<Walk>(addWalksRequestDto);
            await walkRepository.CreateAsyc(walkDomainModel);


            return Ok(mapper.Map<Walk>(walkDomainModel));
        }

        //********** Get All Walks
        //GET: /api/walks
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
           var walksDomainModel= await walkRepository.GetallAsync();

            // map domain model to dto
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));

        }
       
            
    }
}
