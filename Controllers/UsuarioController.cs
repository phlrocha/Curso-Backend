using CursoBackEnd.Models.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoBackEnd.Models;
using CursoBackEnd.Filters;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using CursoBackEnd.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using CursoBackEnd.Business.Entities;
using CursoBackEnd.Business.Repositories;
using CursoBackEnd.Infraestrutura.Data.Repositories;
using Microsoft.Extensions.Configuration;
using CursoBackEnd.Configurations;
using curso.api.Models.Usuarios;

namespace CursoBackEnd.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

       private readonly IUsuarioRepository _usuarioRepository;
 
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration, IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;

        }
        /// teste
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns></returns>
        /// 

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logon")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logon(LoginViewModelInput loginViewModelInput)
        {
           
                var usuario =  _usuarioRepository.ObterUsuario(loginViewModelInput.Login);

                if (usuario == null)
                {
                    return BadRequest("Houve um erro ao tentar acessar.");
                }

                //if (usuario.Senha != loginViewModel.Senha.GerarSenhaCriptografada())
                //{
                //    return BadRequest("Houve um erro ao tentar acessar.");
                //}

                var usuarioViewModelOutput = new UsuarioViewModelOutput()
                {
                    Codigo = usuario.Codigo,
                    Login = loginViewModelInput.Login,
                    Email = usuario.Email
                };

                var token = _authenticationService.GerarToken(usuarioViewModelOutput);

                return Ok(new LoginViewModelOutput
                {
                    Token = token,
                    Usuario = usuarioViewModelOutput
                });
         }
           
        /// <summary>
        /// Este serviço permite cadastrar um usuário cadastrado não existente.
        /// </summary>
        /// <param name="loginViewModelInput">View model do registro de login</param>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar", Type = typeof(RegistroViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        public IActionResult Registrar(RegistroViewModelInput loginViewModelImput)
        {


            var usuario = new Usuario();
            usuario.Login = loginViewModelImput.Login;
            usuario.Senha = loginViewModelImput.Senha;
            usuario.Email = loginViewModelImput.Email;        
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();



            return Created("", loginViewModelImput);
        }
    }
}
