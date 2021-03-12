using AutoMapper;
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
    [Route("api/bands/{bandId}/albums")]
    public class AlbumsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public AlbumsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            this._bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentException(nameof(bandAlbumRepository));
            this._mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AlbumsDto>> GetAlbums(Guid bandId)
        {
            var albumsFromRepo = _bandAlbumRepository.GetAlbums(bandId);

            return Ok(_mapper.Map<IEnumerable<AlbumsDto>>(albumsFromRepo));
        }
    }
}
