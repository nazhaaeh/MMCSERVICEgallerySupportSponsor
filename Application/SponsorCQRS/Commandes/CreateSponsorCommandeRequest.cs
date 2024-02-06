using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SponsorCQRS.Commandes
{
   public class CreateSponsorCommandeRequest :IRequest<SponsorCreateDto>
    {
        public SponsorCreateDto SponsorCreateRequest { get; set; }
        public CreateSponsorCommandeRequest(SponsorCreateDto sponsorCreateRequest)
        {
            SponsorCreateRequest = sponsorCreateRequest;
        }
    }
}
