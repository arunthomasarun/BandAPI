using AutoMapper;
using BandAPI.Entities;
using BandAPI.Helpers;
using BandAPI.Models;
using BandAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Controllers
{
    [ApiController]
    [Route("/api/bands")]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            this._bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentException(nameof(bandAlbumRepository));
            this._mapper = mapper;
        }



        //http://localhost:3052/api/bands?mainGenre=Rock
        //http://localhost:3052/api/bands?mainGenre=Rock&searchQuery=Gu
        //http://localhost:3052/api/bands
        [HttpGet]
        [HttpHead]
        public IActionResult GetBands([FromQuery] string mainGenre, [FromQuery] string searchQuery)
        {
            var bands = _bandAlbumRepository.GetBands(mainGenre, searchQuery);

            //var bandsDto = new List<BandDto>();

            //foreach(var band in bands)
            //{
            //    bandsDto.Add(new BandDto
            //    {
            //        Id = band.Id,
            //        Name = band.Name,
            //        MainGenre = band.MainGenre,
            //        FoundedYearsAgo = $"{band.Founded.GetYearsAgo()} years ago."
            //    });
            //}

            //return Ok(bandsDto);

            return Ok(_mapper.Map<IEnumerable<BandDto>>(bands));
        }

        [HttpGet("{bandId}", Name = nameof(GetBand))]
        public IActionResult GetBand(Guid bandId)
        {
            var band = _bandAlbumRepository.GetBand(bandId);
            return Ok(band);
        }

        [HttpPost]
        public ActionResult<BandDto> CreateBand([FromBody] BandForCreatingDto band)
        {
            var bandEntity = _mapper.Map<Band>(band);

            if (bandEntity == null)
                return BadRequest("Band is Empty");

            _bandAlbumRepository.AddBand(bandEntity);
            _bandAlbumRepository.Save();

            var bandToReturn = _mapper.Map<BandDto>(bandEntity);
            return CreatedAtRoute(nameof(GetBand), new { bandId = bandToReturn.Id }, bandToReturn);
        }
    }
}
