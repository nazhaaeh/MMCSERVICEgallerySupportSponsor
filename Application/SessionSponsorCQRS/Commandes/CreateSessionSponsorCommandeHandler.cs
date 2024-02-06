using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SessionSponsorCQRS.Commandes
{
    public class CreateSessionSponsorCommandeHandler : IRequestHandler<CreateSessioSponsorCommandeRequest, SessionSponsorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSessionSponsorCommandeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SessionSponsorDto> Handle(CreateSessioSponsorCommandeRequest request, CancellationToken cancellationToken)
        {
            var CreatedSessionSponsor = _mapper.Map<SessionSponsor>(request);
            CreatedSessionSponsor.SessionSponsorId = Guid.NewGuid();
           await _unitOfWork.SessionSponsor.AddAsync(CreatedSessionSponsor);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<SessionSponsorDto>(CreatedSessionSponsor);
        }
    }
}
