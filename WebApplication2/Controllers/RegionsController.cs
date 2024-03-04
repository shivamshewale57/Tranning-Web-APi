using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Web_Api.CustomActionFilters;
using Web_Api.Data;
using Web_Api.Models.DTO;
using Web_Api.Repositories;
using WebApplication2.Models.Domain;

namespace Web_Api.Controllers
{


    //[Route("api/[controller]")]
    [Route("api/Regions")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly WebApiDbContext dbContext;
        // after injected repository we used this insted of Dbcontext
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(WebApiDbContext dbContext, IRegionRepository regionRepository, 
            IMapper mapper,ILogger<RegionsController>logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        //**** Get All Regions *****//
        //https://localhost:7251/api/Regions
        [HttpGet]
        public async Task<IActionResult> Getall()
        {

            /// ************** Logging ***********
            //logger.LogInformation("region method is invoked");
            //logger.LogWarning("this is warning ");
            //logger.LogError("its error");

            //1. data from database
            //2. after implimentation of repositery pattern  we use  regionRepository[layer between data & application]insted of dbContext.
            
            {
                
                var regionsDomain = await regionRepository.GetAllAsync();

                logger.LogInformation($"all region Data :{JsonSerializer.Serialize(regionsDomain)}");






                // return dto
                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
            }
           
            
        }

        //******* Get Region By Id ******
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            // get region domain model from database
            //var regionDomain = await dbContext.Regions.FindAsync(id); : another aproch
            // 2. after implemnting repository pattern we use regionRepository over dbContext
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            // covert region domain model to region dto
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // return dto back to client
            return Ok(regionDto);


        }

        //********* Create New Region ********
        //Url: http://localhost:portno/api/regions
        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

           
            { // mapper implimaintation
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                //after imlementing repositry pattern 
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

            }
           


           
        }


        //******* Update Region ******
        //PUT:Url: http://localhost:portno/api/regions/{id}
        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            
            {
                //map DTO to domain Model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                // check if region is exist or not 
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // pass dto back to the client 
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
            }
            
                
        }


        //**** Delete Region *************** 
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // check if region is exist or not 
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // return deleted region back
            //map domain model to dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
            //return Ok(RegionDto);

        }

    }
}
