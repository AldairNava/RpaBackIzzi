namespace WebApplication1.Models
{
    public class PermisosUpdateDto
    {
        public List<string> Permisos { get; set; } = new List<string>();
        public string Area { get; set; } = "";
        public string Departamento { get; set; } = "";
        public string Staff { get; set; } = "";
    }


    public class RoleCreateDto
    {
        public string Role { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public string Staff { get; set; } 
    }



    public class PermisoCreateDto
    {
        public string Key_Permiso { get; set; } = "";
        public string Area { get; set; } = "";
        public string Departamento { get; set; } = "";
        public string Staff { get; set; } = "";
    }


    public class RoleDto
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public string Staff { get; set; }
        public int Status { get; set; }
    }
    public class UserDto
    {
        public int id_users { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool? Active { get; set; }
        public string? Name { get; set; }
        public string? Rol { get; set; }
        public string? Staff { get; set; }
        public string? Area { get; set; }
        public string? Departamento { get; set; }
    }
    // Models/UsuarioCxUpdateDto.cs
    public class UsuarioCxUpdateDto
    {
        public int id_users { get; set; }
        public string user { get; set; } = "";
        public string email { get; set; } = "";
        public string pass { get; set; } = "";
        public int active { get; set; }
        public string name { get; set; } = "";
        public string rol { get; set; } = "";
        public string staff { get; set; } = "";
        public string area { get; set; } = "";
        public string departamento { get; set; } = "";
    }

}