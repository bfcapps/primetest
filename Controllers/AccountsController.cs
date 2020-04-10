using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primetest.Data;
using Primetest.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Primetest.Services;

namespace Primetest.Controllers
{
    [Route("v1/accounts")]
    public class AccountsController : Controller
    {
        [HttpGet]
        [Route("")]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<List<Accounts>>> Get([FromServices] DataContext context)
        {
            var accounts = await context
                .Accounts
                .AsNoTracking()
                .ToListAsync();
            return accounts;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        // [Authorize(Roles = "manager")]
        public async Task<ActionResult<Accounts>> Post(
            [FromServices] DataContext context,
            [FromBody]Accounts model)
        {
            // Verifica se os dados s√£o v√°lidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // ForÁa o usuario a ser sempre "funcionario"
                model.Role = "employee";

                context.Accounts.Add(model);
                await context.SaveChangesAsync();

                // Esconde a senha
                model.Password = "";
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Nao foi possi≠vel criar o usuario" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Accounts>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody]Accounts model)
        {
            // Verifica se os dados sao validos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se o ID informado È o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Usuario nao encontrado" });

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Nao foi possi≠vel criar o usuario" });

            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
                    [FromServices] DataContext context,
                    [FromBody]Accounts model)
        {
            var accounts = await context.Accounts
                .AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (accounts == null)
                return NotFound(new { message = "Usuario ou senha invalidos" });

            var token = TokenService.GenerateToken(accounts);
            // Esconde a senha
            accounts.Password = "";
            return new
            {
                accounts = accounts,
                token = token
            };
        }
    }
}