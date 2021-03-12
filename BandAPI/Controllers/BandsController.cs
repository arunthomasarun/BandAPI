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

        [HttpGet]
        [HttpHead]
        public IActionResult GetBands()
        {
            var bands = _bandAlbumRepository.GetBands();

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

        [HttpGet("{bandIda}")]
        public IActionResult GetBand(Guid bandId)
        {
            var band = _bandAlbumRepository.GetBand(bandId);
            return Ok(band);
        }
    }
}
