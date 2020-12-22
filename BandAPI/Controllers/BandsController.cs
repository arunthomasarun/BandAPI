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

        public BandsController(IBandAlbumRepository bandAlbumRepository)
        {
            this._bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentException(nameof(bandAlbumRepository));
        }

        [HttpGet]
        public IActionResult GetBands()
        {
            var bands = _bandAlbumRepository.GetBands();
            return Ok(bands);
        }

        [HttpGet("{bandId}")]
        public IActionResult GetBand(Guid bandId)
        {
            var band = _bandAlbumRepository.GetBand(bandId);
            return Ok(band);
        }
    }
}
