using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GalleryCQRS.Commandes
{
    public class DeleteGalleryCommandeHandler : IRequestHandler<DeleteGalleryCommandeRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public DeleteGalleryCommandeHandler(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
			this._webHostEnvironment = webHostEnvironment;
		}
        public async Task Handle(DeleteGalleryCommandeRequest request, CancellationToken cancellationToken)
        {
            var existingGallery = await _unitOfWork.Gallery.GetByIdAsync(request.Id);
			var ImagName = $"Img_Gallery_{existingGallery.GalleryId}";
			var ImagExten = Path.GetExtension(existingGallery.imageGalleryPath);
            var urlOldImg = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/uploadsGallery/", $"{ImagName}{ImagExten}");

			if (existingGallery != null)
            {
                // Supprimer l'ancien fichier PDF s'il existe
                if (System.IO.File.Exists(urlOldImg))
                {
                    System.IO.File.Delete(urlOldImg);
                }

                // Supprimer l'entité dans la base de données
                _unitOfWork.Gallery.Remove(existingGallery);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
