using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Web_Api.CustomActionFilters;
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

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //*********** Create Walk ********
        //POST: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            
            {  // Map Dto to Domain model
                var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);
                await walkRepository.CreateAsyc(walkDomainModel);


                return Ok(mapper.Map<Walk>(walkDomainModel));
            }
            
           
        }

        //********** Get All Walks
        //GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery]bool? isAsending,
             [FromQuery] int pageNumber = 1,[FromQuery]int pageSize =1000)

        {
            var walksDomainModel = await walkRepository.GetallAsync(filterOn, filterQuery, sortBy,
                isAsending?? true,pageNumber,pageSize);

            // map domain model to dto
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));

        }


        //******** Get Walk By ID ********
        //Get: api/Walks/{id}

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            var walkDominModel = await walkRepository.GetByIdAsync(id);
            if (walkDominModel == null)
            {
                return NotFound();
            }

            //Map Domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDominModel));
        }

        // ***** Update Walk By id
        //PUT:api/Walks/{id}
        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateWalkRequestDto updateWalkRequestDto)
        {

           
            {
                // map dto to domain model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                    return NotFound();
                //map domain model  to dto 
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
           
            
        }

        //***** delet walk
        //DELETE: api/walk{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
          var  deletedWalkDomainModel= await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();

            }
            // map domain to dto
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
        
          
         




}
}
