using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SessionSponsorCQRS.Commandes
{
   public class DeleteSessionSponsorCommandeRequest:IRequest
    {
        public Guid Id { get; set; }
    }
}
