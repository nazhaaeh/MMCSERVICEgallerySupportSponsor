
using Application.DTO;
using Application.SessionSponsorCQRS.Commandes;
using Application.SessionSponsorCQRS.Queries;
using Application.SponsorCQRS.Commandes;
using Application.SponsorCQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MMCGallerySupportSponsor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionSponsorController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public SessionSponsorController(IMediator mediatr)
        {
            _mediatr = mediatr;

        }
        [HttpGet]
        public async Task<IActionResult> GetSessionSponsor()
        {
            var Query = new GetSessionSponsorQueryRequest();
            var results = await _mediatr.Send(Query);

            return Ok(results);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSessionSponsr(Guid id)
        {
            var Query = new GetByIdSessionSponsorQueryRequest();
            Query.IdSessionsponsr = id;
            var reselts = await _mediatr.Send(Query);
            return Ok(reselts);

        }
        [HttpPost]
        public async Task<IActionResult> CreateSessionSpons([FromBody] CreateSessioSponsorCommandeRequest createSessioSponsorCommandeRequest)
        {
            return Ok(await _mediatr.Send(createSessioSponsorCommandeRequest));

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSessionSponsor(Guid id ,[FromBody] SessionSponsorDto session)
        {
          var Commande = new UpdateSessionSponsorCommandeRequest
          {
              Id = id,
              SessionSponsorUpdateRequest   = session
          };

           return Ok( await _mediatr.Send(Commande));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionSponsor(Guid id)
        {
            var command = new DeleteSessionSponsorCommandeRequest
            {
                Id = id
            };

            await _mediatr.Send(command);

            return Ok("La session sponsor a été effectué avec succès.");
        }
    }
}
