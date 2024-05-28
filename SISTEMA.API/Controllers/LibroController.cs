using BLL;
using ENTITY;
using ENTITY.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SISTEMA.API.DBContextDapper;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Collections.Generic;

namespace SISTEMA.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibroController(DapperCNN _DapperCNN) : ControllerBase
{
    [HttpPost]
    [Route("Guardar")]
    public IActionResult Guardar([FromBody] Libro libro)
    {

        List<string> lstValidaciones = BLL_Libro.ValidacionDatos(_DapperCNN.Connection(), libro, 1);

        if (lstValidaciones.Count == 0)
        {
            List<string> lstDatos = BLL_Libro.GuardarDatos(_DapperCNN.Connection(), libro);

            if (lstDatos[0] == "00")
            {
                var accountSid = "AC4b63356c5fecd8c94a5cf9103545c75f";
                var authToken = "78510ec6d251562b6b7e226b783436c5";
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                body: "Se ha registrado el libro con éxito",
                from: new Twilio.Types.PhoneNumber("+13198953226"),
                to: new Twilio.Types.PhoneNumber("+528132460072"));
                return Ok(new { Estatus = "00", Mensaje = lstDatos[1], message.Sid});

            }
            else
            {
                return Ok(new { Estatus = lstDatos[0], Mensaje = lstDatos[1] });
            }
        }
        else
        {
            return Ok(new { Estatus = 14, Mensaje = lstValidaciones[0] });
        }
    }

    [HttpGet]
    [Route("ListarLibro")]
    public IActionResult ListarLibro()
    {
        List<DTOconsultLibro> ltsConsulLibro = BLL_Libro.ConsultaLibro(_DapperCNN.Connection());

        if (ltsConsulLibro.Count > 0)
        {
            return Ok(new { status = "00", value = ltsConsulLibro });
        }
        else
        {
            return BadRequest(new { status = 14, value = "" });
        }
    }

    [HttpGet]
    [Route("ListarLibro/Texto")]
    public IActionResult ListarLibro(string Texto)
    {
        List<DTOconsultLibro> ltsConsulLibro = BLL_Libro.ConsultaLibro(_DapperCNN.Connection(), Texto);

        if (ltsConsulLibro.Count > 0)
        {
            return Ok(new { status = "00", value = ltsConsulLibro });
        }
        else
        {
            return BadRequest(new { status = 14, value = "" });
        }
    }

    [HttpPut]
    [Route("Editar")]
    public IActionResult Editar([FromBody] Libro libro)
    {
        List<string> lstValidaciones = BLL_Libro.ValidacionDatos(_DapperCNN.Connection(), libro, 2);

        if (lstValidaciones.Count == 0)
        {
            List<string> lstDatos = BLL_Libro.ActualizarDatos(_DapperCNN.Connection(), libro);

            if (lstDatos[0] == "00")
            {
                return Ok(new { Estatus = "00", Mensaje = lstDatos[1] });
            }
            else
            {
                return Ok(new { Estatus = lstDatos[0], Mensaje = lstDatos[1] });
            }
        }
        else
        {
            return Ok(new { Estatus = 14, Mensaje = lstValidaciones[0] });
        }
    }

    [HttpPut]
    [Route("Eliminar")]
    public IActionResult Eliminar([FromBody] Libro libro)
    {
        List<string> lstValidaciones = BLL_Libro.ValidacionDatos(_DapperCNN.Connection(), libro, 2);

        if (lstValidaciones.Count == 0)
        {
            List<string> lstDatos = BLL_Libro.EliminarLibro(_DapperCNN.Connection(), libro);

            if (lstDatos[0] == "00")
            {
                return Ok(new { Estatus = "00", Mensaje = lstDatos[1] });
            }
            else
            {
                return Ok(new { Estatus = lstDatos[0], Mensaje = lstDatos[1] });
            }
        }
        else
        {
            return Ok(new { Estatus = 14, Mensaje = lstValidaciones[0] });
        }
    }


}
