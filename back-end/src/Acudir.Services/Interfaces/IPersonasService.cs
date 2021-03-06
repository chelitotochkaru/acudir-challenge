using System;
using Acudir.Domain;
using Acudir.Domain.DTOs;

namespace Acudir.Services.Interfaces
{
	public interface IPersonasService
	{
        /// <summary>
        /// Returns a randomized entity Persona.
        /// </summary>
        /// <returns></returns>
        Task<PersonasGetRandomResponse?> GetRandom();

        /// <summary>
        /// Deletes an entity Persona.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);
    }
}

