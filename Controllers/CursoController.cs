using CursoBackEnd.Business.Entities;
using CursoBackEnd.Business.Repositories;
using CursoBackEnd.Models;
using CursoBackEnd.Models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CursoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        /// <summary>
        /// teste
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns></returns>
        /// 

        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar curso", Type = typeof(CursoViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]

        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            Curso curso = new Curso();
            curso.Nome = cursoViewModelInput.Nome;
            curso.Descricao = cursoViewModelInput.Descricao;         
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.CodigoUsuario = codigoUsuario;
            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();

            return Created("", cursoViewModelInput);
        }


        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]

        public async Task<IActionResult> Get()
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario)
                .Select(s => new CursoViewModelOutput()
                {
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Login = s.Usuario.Login,

                });
            return Ok(cursos);
        }
    }

}