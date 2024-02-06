using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SupportsCQRS.Commandes
{
    public class DeleteSupportCommandeHandler : IRequestHandler<DeleteSupportCommandeRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public DeleteSupportCommandeHandler(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}
        public async Task Handle(DeleteSupportCommandeRequest request, CancellationToken cancellationToken)
        {			
			var existingSupport = await _unitOfWork.Support.GetByIdAsync(request.Id);
			var ImagName = $"Img_Support_{existingSupport.SupportId}";
			var ImagExten = Path.GetExtension(existingSupport.FilePath);
			var urlOldImg = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/uploads/", $"{ImagName}{ImagExten}");
			// Supprimer l'ancien fichier image s'il existe
			if (System.IO.File.Exists(urlOldImg))
			{
				System.IO.File.Delete(urlOldImg);
			}

			// Supprimer l'entité dans la base de données
			_unitOfWork.Support.Remove(existingSupport);
                await _unitOfWork.SaveAsync();
            }

        }
}

