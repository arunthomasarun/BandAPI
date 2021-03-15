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

        [HttpGet("{albumId}", Name = nameof(GetAlbumForBand))]
        public ActionResult<AlbumsDto> GetAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var album = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (album == null)
                return NotFound();

            return Ok(_mapper.Map<AlbumsDto>(album));
        }

        [HttpPost]
        public ActionResult<AlbumsDto> CreateAlbumForBand(Guid bandId, [FromBody] AlbumForCreatingDto album)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumEntity = _mapper.Map<Entities.Album>(album);

            _bandAlbumRepository.AddAlbum(bandId, albumEntity);
            _bandAlbumRepository.Save();

            var albumToReturn = _mapper.Map<AlbumsDto>(albumEntity);
            return CreatedAtRoute(nameof(GetAlbumForBand), new { bandId = bandId, albumId = albumToReturn.Id }, albumToReturn); 
        }
    }
}
