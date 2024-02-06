
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SessionSponsorCQRS.Commandes
{
    public class DeleteSessionSponsorCommandeHandler : IRequestHandler<DeleteSessionSponsorCommandeRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteSessionSponsorCommandeHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteSessionSponsorCommandeRequest request, CancellationToken cancellationToken)
        {
            var existingSessionSponsor = await unitOfWork.SessionSponsor.GetByIdAsync(request.Id);

            if (existingSessionSponsor != null)
            {
          
                unitOfWork.SessionSponsor.Remove(existingSessionSponsor);
                await unitOfWork.SaveAsync();
            }
        }
    }
}
