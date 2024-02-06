using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.GalleryCQRS.Queries;
using MediatR;
using Application.GalleryCQRS.Commandes;
using Microsoft.AspNetCore.Authorization;

namespace MMCGallerySupportSponsor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public GalleryController( IMediator mediatR)
        {
             _mediatR = mediatR;

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllGallery()
        {
            var Query = new GetGalleryQueryRequest();
            var result = await _mediatR.Send(Query);
            return Ok(result);
        }

        [HttpGet ("{id}")]
		[Authorize]
		public async Task<IActionResult> GetGalleryById(Guid id )
        {
            var Query = new GetByIdGalleryQueryRequest();
            Query.id = id;
            var result = await _mediatR.Send(Query);
            return Ok(result);

        }

        [HttpPost]
		[Authorize (Roles ="Admin")]
		public async Task<IActionResult> Creategallery([FromForm] GalleryCreateDto createDto)
        {
            var Commandes = new  CreateGalleryCommandeRequest(createDto);

            var result = await _mediatR.Send(Commandes);
            return Ok(result);

        }
        [HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GalleryDelete(Guid id)
        {
            var command = new DeleteGalleryCommandeRequest
            {
                Id = id
            };

            await _mediatR.Send(command);

            return Ok("It's deleted");
        }
        [HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateGallery(Guid id, [FromForm] GalleryUpdateDto updateDto)
        {
            var command = new UpdateGalleryCommandeRequest
            {
                GalleryId = id,
                GalleryUpdateRequest = updateDto
            };

            var updatedGalleryDto = await _mediatR.Send(command);

            if (updatedGalleryDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    } 
}
