using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Acudir.Domain.Interfaces;

namespace Acudir.Domain;

public class Persona : IEntity, ISoftDelete
{
    #region Properties

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public int DNI { get; set; }

    public int Telefono { get; set; }

    public string Mail { get; set; } = null!;

    public bool Activo { get;  set; }

    #endregion
}

