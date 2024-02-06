using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SessionSponsorCQRS.Commandes
{
   public class CreateSessioSponsorCommandeRequest :IRequest<SessionSponsorDto>
    {
        public Guid SponsorId { get; set; }
        public Guid SessionId { get; set; }


    }
}
