using System;

namespace Acudir.Domain.Interfaces
{
	public interface ISoftDelete
	{
		bool Activo { get; set; }
	}
}