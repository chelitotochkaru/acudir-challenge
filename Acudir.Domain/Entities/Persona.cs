using Acudir.Domain.Interfaces;

namespace Acudir.Domain;

public class Persona : ISoftDelete
{
    #region Properties

    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Provincia { get; set; }

    public int DNI { get; set; }

    public int Telefono { get; set; }

    public string Mail { get; set; }

    public bool Activo { get;  set; }

    #endregion
}

