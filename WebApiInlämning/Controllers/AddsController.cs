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

        public AddsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAdds()
        {
            return Ok(_context.Advertisements.Select(a => new AdvertisementDTO()
            {
                Name = a.Name,
                Description = a.Description,
                Id = a.Id
            }).ToList());
        }
        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetAd(int Id)
        {
            var advertisement = _context.Advertisements.FirstOrDefault(a => a.Id == Id);
            if (advertisement == null)
                return NotFound();
            var ret = new AdvertisementDTO()
            {
                Id = advertisement.Id,
                Name = advertisement.Name,
                Description = advertisement.Description,
            };
            return Ok(ret);
        }

        [HttpPost]
        public IActionResult Create(CreateAdvertisementDTO advertisement)
        {
            var ad = new Advertisement()
            {
                Name = advertisement.Name,
                Description = advertisement.Description
            };
            _context.Advertisements.Add(ad);
            _context.SaveChanges();
            var adDTO = new AdvertisementDTO()
            {
                Name = ad.Name,
                Description = ad.Description,
                Id = ad.Id,
            };
            
            return CreatedAtAction(nameof(GetAd), new {Id = ad.Id},adDTO);
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult Update(UpdateAdvertisementDTO advertisement, int Id)
        {
            var getAd = _context.Advertisements.FirstOrDefault(a => a.Id == Id);
            if (getAd == null) return NotFound();
            getAd.Name = advertisement.Name;
            getAd.Description = advertisement.Description;
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
