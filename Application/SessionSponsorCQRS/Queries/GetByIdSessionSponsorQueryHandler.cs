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
    public class GetByIdSessionSponsorQueryHandler : IRequestHandler<GetByIdSessionSponsorQueryRequest, SessionSponsorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetByIdSessionSponsorQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        public async Task<SessionSponsorDto> Handle(GetByIdSessionSponsorQueryRequest request, CancellationToken cancellationToken)
        {
            var SessionSpons = await _unitOfWork.SessionSponsor.GetByIdAsync(request.IdSessionsponsr);
            if (SessionSpons == null)
            {

                return null;
            }
            return _mapper.Map<SessionSponsorDto>(SessionSpons);
        }
    }
}
