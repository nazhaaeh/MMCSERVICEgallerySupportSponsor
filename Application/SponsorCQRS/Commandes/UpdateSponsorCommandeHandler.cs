using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.SponsorCQRS.Commandes
{
    public class UpdateSponsorCommandeHandler : IRequestHandler<UpdateSponsorCommandeRequest, SponsorUpdateDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateSponsorCommandeHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<SponsorUpdateDto> Handle(UpdateSponsorCommandeRequest request, CancellationToken cancellationToken)
        {
            var existingSponsor = await _unitOfWork.Sponsor.GetByIdAsync(request.Id);

            if (existingSponsor == null)
            {
                return null;
            }


			var ImagName = $"Img_Sponsor_{existingSponsor.SponsorId}";
            var ImagExten = Path.GetExtension(existingSponsor.ImagesponsorPath);
			var urlOldImg = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/uploadsGallery/", $"{ImagName}{ImagExten}");
            // Supprimer l'ancien fichier image s'il existe
            if (System.IO.File.Exists(urlOldImg))
            {
                System.IO.File.Delete(urlOldImg);
            }
            string hosturl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}";

            // Enregistrer le nouveau fichier image s'il est fourni
            if (request.SponsorUpdateRequest.SponsorImage != null)
            {
                // Enregistrez l'image dans le dossier wwwroot/UplaodSponsorImage
              
                var localeFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/UplaodSponsorImage/", $"{ImagName}{ImagExten}");

                using (var fileStream = new FileStream(localeFilePath, FileMode.Create))
                {
                    await request.SponsorUpdateRequest.SponsorImage.CopyToAsync(fileStream);
                }

                existingSponsor.ImagesponsorPath = $"{hosturl}/UplaodSponsorImage/{ImagName}{ImagExten}";
            }


            // Mettre à jour l'entité dans la base de données
            _unitOfWork.Sponsor.Update(existingSponsor);
            await _unitOfWork.SaveAsync();

            // Mapper l'entité mise à jour vers le DTO de réponse
            var updatedSponsorDto = _mapper.Map<SponsorUpdateDto>(existingSponsor);

            return updatedSponsorDto;
        }
    }
}
