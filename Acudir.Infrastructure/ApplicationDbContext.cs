using System;
using Acudir.Domain;
using Microsoft.EntityFrameworkCore;

namespace Acudir.Infrastructure
{
	public class ApplicationDbContext : DbContext
	{
        #region Properties

        public DbSet<Persona> Personas { get; set; }

        #endregion

        #region Constructor

        public ApplicationDbContext()
		{
            //
		}

        #endregion
    }
}

