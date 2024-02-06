﻿using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SessionSponsorCQRS.Queries
{
    public class GetByIdSessionSponsorQueryRequest : IRequest<SessionSponsorDto>
    {
        public Guid IdSessionsponsr { get; set; }
    }
}
