using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SponsorCQRS.Commandes
{
    public class DeleteSponsorCommandeHandler : IRequestHandler<DeleteSponsorCommandeRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment webHostEnvironment;

		public DeleteSponsorCommandeHandler(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
			this.webHostEnvironment = webHostEnvironment;
		}
        public async Task Handle(DeleteSponsorCommandeRequest request, CancellationToken cancellationToken)
        {
            var existingSponsor = await _unitOfWork.Sponsor.GetByIdAsync(request.Id);
			var ImagName = $"Img_Sponsor_{existingSponsor.SponsorId}";
			var ImagExten = Path.GetExtension(existingSponsor.ImagesponsorPath);
			var urlOldImg = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot/UplaodSponsorImage/", $"{ImagName}{ImagExten}");

			if (existingSponsor != null)
            {
                // Supprimer l'ancien fichier PDF s'il existe
                if (System.IO.File.Exists(urlOldImg))
                {
                    System.IO.File.Delete(urlOldImg);
                }

                // Supprimer l'entité dans la base de données
                _unitOfWork.Sponsor.Remove(existingSponsor);
                await _unitOfWork.SaveAsync();
            }

        }
    }
}
