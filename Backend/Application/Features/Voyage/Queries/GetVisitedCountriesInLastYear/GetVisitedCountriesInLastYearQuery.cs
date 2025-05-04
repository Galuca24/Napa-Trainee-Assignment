using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Queries.GetVisitedCountriesInLastYear
{
    public class GetVisitedCountriesQuery : IRequest<List<VisitedCountryDTO>> 
    { 
    }

}
