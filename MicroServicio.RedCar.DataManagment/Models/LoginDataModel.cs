namespace MicroServicio.RedCar.DataManagement.Models
{
    public class LoginDataModel
    {
        // =========================
        // IDENTIFICACIÓN DEL USUARIO
        // =========================
        public int id_usuario { get; set; }
        public Guid usuario_guid { get; set; }

        public string username { get; set; } = null!;
        public string correo { get; set; } = null!;

        // =========================
        // SEGURIDAD
        // =========================
        public string password_hash { get; set; } = null!;
        public string password_salt { get; set; } = null!;

        // =========================
        // ESTADO
        // =========================
        public string estado_usuario { get; set; } = null!;
        public bool es_eliminado { get; set; }
        public bool activo { get; set; }

        // =========================
        // RELACIÓN CON CLIENTE
        // =========================
        public int id_cliente { get; set; }

        // =========================
        // ROLES
        // =========================
        public IReadOnlyList<RolDataModel> roles { get; set; } = Array.Empty<RolDataModel>();
    }
}