using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiInlämning.Data;
using WebApiInlämning.DTO;

namespace WebApiInlämning.Controllers
{[Route("api/[controller]")]
    [ApiController]
    public class AddsController :ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAdds()
        {
            return Ok(_context.Advertisements.Select(a => _mapper.Map<AdvertisementsDTO>(a)));

            //return Ok(_context.Advertisements.Select(a => new AdvertisementDTO()
            //{
            //    Name = a.Name,
            //    Description = a.Description,
            //    Id = a.Id
            //}).ToList());
        }
        [HttpGet]
        [Route("{Id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAd(int Id)
        {
            var advertisement = _context.Advertisements.FirstOrDefault(a => a.Id == Id);
            if (advertisement == null)
                return NotFound();
            return Ok(_mapper.Map<AdvertisementDTO>(advertisement));
            //var ret = new AdvertisementDTO()
            //{
            //    Id = advertisement.Id,
            //    Name = advertisement.Name,
            //    Description = advertisement.Description,
            //};
            //return Ok(ret);
        }

        [HttpPost]
        public IActionResult Create(CreateAdvertisementDTO advertisement)
        {
            var mapAd = _mapper.Map<Advertisement>(advertisement);
            //var ad = new Advertisement()
            //{
            //    Name = advertisement.Name,
            //    Description = advertisement.Description
            //};
            _context.Advertisements.Add(mapAd);
            _context.SaveChanges();
            var adMapDto = _mapper.Map<Advertisement>(mapAd);
            //var adDTO = new AdvertisementDTO()
            //{
            //    Name = ad.Name,
            //    Description = ad.Description,
            //    Id = ad.Id,
            //};
            
            return CreatedAtAction(nameof(GetAd), new {Id = mapAd.Id}, adMapDto);
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult Update(UpdateAdvertisementDTO advertisement, int Id)
        {
            var getAd = _context.Advertisements.FirstOrDefault(a => a.Id == Id);
            if (getAd == null) return NotFound();
            _mapper.Map(advertisement, getAd);
            //getAd.Name = advertisement.Name;
            //getAd.Description = advertisement.Description;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch]
        [Route("{Id}")]
        public IActionResult Patch([FromBody]JsonPatchDocument<Advertisement> adUpdate, int Id)
        {
            var getAd = _context.Advertisements.FirstOrDefault(a => a.Id == Id);
            if (getAd == null) return NotFound();
            adUpdate.ApplyTo(getAd);
            //_context.Update(getAd);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(int Id)
        {
            var ad = _context.Advertisements.FirstOrDefault(a => a.Id == Id);
            if (ad == null) return NotFound();
            _context.Advertisements.Remove(ad);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
