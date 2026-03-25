using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Gimnacio.Models
{
    public class RolPermiso
    {
        public int RolId { get; set; }
        public Roles Rol { get; set; } = null!;

        public int PermisoId { get; set; }
        public Permisos Permiso { get; set; } = null!;
    }
}