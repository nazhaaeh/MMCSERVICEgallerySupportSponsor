using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SessionSponsorCQRS.Queries
{
    public class GetSessionSponsorQueryHandler : IRequestHandler<GetSessionSponsorQueryRequest, List<SessionSponsorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSessionSponsorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<SessionSponsorDto>> Handle(GetSessionSponsorQueryRequest request, CancellationToken cancellationToken)
        {
            var SessionSponsor = await _unitOfWork.SessionSponsor.GetAllAsync();
            return _mapper .Map<List<SessionSponsorDto>>(SessionSponsor);
        }
    }
}
