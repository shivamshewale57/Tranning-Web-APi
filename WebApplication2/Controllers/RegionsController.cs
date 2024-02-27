using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api.Data;
using Web_Api.Models.DTO;
using Web_Api.Repositories;
using WebApplication2.Models.Domain;

namespace Web_Api.Controllers
{


    //[Route("api/[controller]")]
    [Route("api/Regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WebApiDbContext dbContext;
        // after injected repository we used this insted of Dbcontext
        private readonly IRegionRepository regionRepository;

        public RegionsController(WebApiDbContext dbContext,IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        //**** Get All Regions *****//
        //https://localhost:7251/api/Regions
        [HttpGet]
        public async Task<IActionResult> Getall()
        {

            // data from database
            var regionsDomain = await regionRepository.GetAllAsync

            // map domain models to dtos
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                    {
                    Id = regionDomain.Id,
                    Code= regionDomain.Code,
                    Name= regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl       
                });

            }
            // return dto
            return Ok(regionsDto);
        }






        //******* Get Region By Id ******
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id) 
        {

            // get region domain model from database
            var regionDomain = await dbContext.Regions.FindAsync(id);
            //var region = dbContext.regions.FirstOrDefault(x => x.Id == id);
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
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to domain model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl

            };


            //Use Domain Model To Create Region
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            // Map Domain model back to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }


        //******* Update Region ******
        //PUT:Url: http://localhost:portno/api/regions/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            // check if region is exist or not 
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();

            }
            // Map Dto To Domain Model
            regionDomainModel.Code = updateRegionRequest.Code;
            regionDomainModel.Name = updateRegionRequest.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequest.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            // Covert Domain Model to DTO
            var regionDto = new RegionDto()
            {
                Id=regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name =regionDomainModel.Name,
                RegionImageUrl=regionDomainModel.RegionImageUrl

            };

            // pass dto back to the client 
            return Ok(regionDto);
        }


        //**** Delete Region *************** 
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // check if region is exist or not 
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
            return NotFound();
            }

            // if found Delete it 
            dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();

            // return deleted region back
            //map domain model to dto
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };

            return Ok(new { Message = "Record deleted successfully", DeletedRegion = regionDto });
            //return Ok(regionDto);

        }
             
    }
}
