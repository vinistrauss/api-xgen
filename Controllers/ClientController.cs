using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers {

    [Route ("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase {

        private readonly IEFCoreRepository _repo;
        public ClientController (IEFCoreRepository repo) {
            _repo = repo;
        }

        [EnableCors]
        [HttpGet]
        public async Task<IActionResult> Get () {

            try {
                var clients = await _repo.GetAllClients ();
                return Ok (clients);
            } catch (Exception ex) {
                return BadRequest ($"Erro: {ex}");
            }
        }

        [EnableCors]
        [HttpGet ("{id}", Name = "GetClient")]
        public async Task<IActionResult> Get (int id) {

            try {
                var clients = await _repo.GetClientById (id);
                return Ok (clients);
            } catch (Exception ex) {
                return BadRequest ($"Erro: {ex}");
            }
        }

        [EnableCors]
        [HttpPost]
        public async Task<IActionResult> Add (Client model) {

            try {
                _repo.Add (model);

                if (await _repo.SaveChangeAsync ()) {
                    return Ok ();
                }
                return BadRequest ($"Não salvou");

            } catch (Exception ex) {
                return BadRequest ($"Erro: {ex}");
            }

        }

        [EnableCors]
        [HttpPut ("{id}")]
        public async Task<IActionResult> Update (int id, Client model) {
            try {
                var client = await _repo.GetClientById (id);

                if (client != null) {
                    _repo.Update (model);
                    if (await _repo.SaveChangeAsync ())
                        return Ok ();

                }

                return BadRequest ("ID não encontrado");

            } catch (Exception ex) {
                return BadRequest ($"Erro: {ex}");
            }
        }

        [EnableCors]
        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (int id) {
            try {
                var client = await _repo.GetClientById (id);

                if (client != null) {
                    _repo.Delete (client);
                    if (await _repo.SaveChangeAsync ())
                        return Ok ();

                }

                return BadRequest ("ID não encontrado");

            } catch (Exception ex) {
                return BadRequest ($"Erro: {ex}");
            }
        }

        [EnableCors]
        [HttpDelete ("address/{id}", Name = "DeleteAddress")]
        public async Task<IActionResult> DeleteAddress (int id) {
            try {
                var address = await _repo.GetAddressById (id);

                if (address != null) {
                    _repo.Delete (address);
                    if (await _repo.SaveChangeAsync ())
                        return Ok ();

                }

                return BadRequest ("ID não encontrado");

            } catch (Exception ex) {
                return BadRequest ($"Erro: {ex}");
            }
        }
    }
}