using System;
namespace Acudir.Domain.DTOs
{
	public class PersonasGetRandomResponse
	{
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Provincia { get; set; } = null!;

        public int DNI { get; set; }

        public int Telefono { get; set; }

        public string Mail { get; set; } = null!;
    }
}

