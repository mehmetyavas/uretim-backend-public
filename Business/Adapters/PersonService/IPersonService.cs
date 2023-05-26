﻿using Entities.Dtos.DevArchDtos;
using System.Threading.Tasks;

namespace Business.Adapters.PersonService
{
    public interface IPersonService
    {
        Task<bool> VerifyCid(Citizen citizen);
    }
}