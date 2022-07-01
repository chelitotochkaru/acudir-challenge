using System.Diagnostics.CodeAnalysis;
using Acudir.Domain;
using Acudir.Domain.DTOs;
using Acudir.Domain.Interfaces;
using Acudir.Infrastructure;
using Acudir.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Acudir.Services;

public class PersonasService : IPersonasService
{
    #region Readonly Fields

    private readonly ApplicationDbContext _context;

    #endregion

    public PersonasService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PersonasGetRandomResponse?> GetRandom()
    {
        //TODO: esto se debe mejorar; funciona solo para este challenge    :(
        int randId = Random.Shared.Next(1, 1000);

        Persona persona = await _context.Personas
            .FirstOrDefaultAsync(p => p.Id == randId) ?? throw new Exception("Persona no encontrada");

        return new PersonasGetRandomResponse() {
            Id = persona.Id,
            Nombre = persona.Nombre,
            Apellido = persona.Apellido,
            Provincia = persona.Provincia,
            DNI = persona.DNI,
            Telefono = persona.Telefono,
            Mail = persona.Mail
        };
    }
}

