using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SessionSponsorCQRS.Commandes
{
    public class UpdateSessionSponsorCommandeHandler : IRequestHandler<UpdateSessionSponsorCommandeRequest, SessionSponsorDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateSessionSponsorCommandeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<SessionSponsorDto> Handle(UpdateSessionSponsorCommandeRequest request, CancellationToken cancellationToken)
        {
            var existingSessionSponsor = await unitOfWork.SessionSponsor.GetByIdAsync(request.Id);
            mapper.Map(request.SessionSponsorUpdateRequest, existingSessionSponsor); 
            unitOfWork.SessionSponsor.Update(existingSessionSponsor);
            await unitOfWork.SaveAsync();
            return mapper.Map<SessionSponsorDto>(existingSessionSponsor);


        }
    }
}
