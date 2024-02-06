using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SessionSponsorCQRS.Commandes
{
    public class UpdateSessionSponsorCommandeRequest :IRequest<SessionSponsorDto>
    {
        public Guid Id { get; set; }
        public SessionSponsorDto SessionSponsorUpdateRequest { get; set; }

    }
}
