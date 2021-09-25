using CursoBackEnd.Models.Usuarios;

namespace CursoBackEnd.Configurations
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}
