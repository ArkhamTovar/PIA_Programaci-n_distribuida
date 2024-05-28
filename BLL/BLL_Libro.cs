using ENTITY;
using System.Data;
using DAL;
using ENTITY.DTO;
using FluentValidation;
using BLL.Validaciones;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Globalization;

namespace BLL;

public class BLL_Libro
{   

    public static List<string> ValidacionDatos(string Cadena, Libro PLibro, int opc)
    {   //Se crea una lista, de la clase PokemonValidaciones traemos la lista de validaciones
        //Lo que traiga esa lista lo pone en PokemonResultado y lo pone en lstValidaciones para mandar los errores
        //Mandamos lstValidaciones que es lo que nos mandara los mensajes de error
        List<string> lstValidaciones = [];

        LibroValidaciones LibroValidacion = new();

        var LibroResultado = LibroValidacion.Validate(PLibro);

        if (!LibroResultado.IsValid)
        {
            lstValidaciones = LibroResultado.Errors.Select(x => x.ErrorMessage).ToList();
        }

        if (opc == 1)
        { //No puedo haber 2 libros con mismo ISBN
            if (ValidaISBNLibro(Cadena, PLibro.ISBN)) ///REVISAR *************
            {
                lstValidaciones.Add("Ya hay un libro con ese ISBN");
            }
        }

        return lstValidaciones;
    }

    private static bool ValidaISBNLibro(string Cadena, string ISBN)  ////POSIBLE ERROR REVISAR NombrePokemon
    {
        bool Validacion = false;

        var dpParametros = new
        {
            P_Accion = 1,
            P_ISBN = ISBN
        };

        DataTable Dt = Context.Funcion_StoreDB(Cadena, "spConsulValidaLibro", dpParametros);

        if (Dt.Rows.Count > 0)
        {
            Validacion = true;
        }
        return Validacion;
    }

    //GUARDAR DATOS
    public static List<string> GuardarDatos(string Cadena, Libro PLibro)
    {
        List<string> lstDatos = [];
        try
        {
            var dpParametros = new
            {
                P_Nombre = PLibro.Nombre.ToUpper(),
                P_Genero = PLibro.Genero.ToUpper(),
                P_ISBN = PLibro.ISBN,
                P_Editorial = PLibro.Editorial,
                P_Precio = PLibro.Precio
                
            };

            Context.Procedimiento_StoreDB(Cadena, "sp_InsertarLibro", dpParametros);
            lstDatos.Add("00");
            lstDatos.Add("Libro Guardado con éxito");
        }
        catch (Exception e)
        {
            lstDatos.Add("14");
            lstDatos.Add(e.Message);
        }
        return lstDatos;
    }

    public static List<DTOconsultLibro> ConsultaLibro(string Cadena)
    {
        List<DTOconsultLibro> ltsConsulLibro = [];

        var dpParametros = new
        {
            P_Accion = 1
        };

        DataTable Dt = Context.Funcion_StoreDB(Cadena, "spConsultaLibro", dpParametros);

        if (Dt.Rows.Count > 0)
        {
            ltsConsulLibro = [
               .. from item in Dt.AsEnumerable()
                  select new DTOconsultLibro
                  {
                      IDLibro = item.Field<int>("IDLibro"),
                      Nombre = item.Field<string>("Nombre"),
                      Genero= item.Field<string>("Genero"),
                      ISBN= item.Field<string>("ISBN"),
                      Editorial= item.Field<string>("Editorial"),
                      //Precio= item.Field<int>("Precio")
                  }
            ];
        }
        return ltsConsulLibro;
    }

    public static List<DTOconsultLibro> ConsultaLibro(string Cadena, string Texto)
    {
        List<DTOconsultLibro> ltsConsulLibro = [];

        var dpParametros = new
        {
            P_Accion = 2,
            P_Texto = Texto
        };

        DataTable Dt = Context.Funcion_StoreDB(Cadena, "spConsultaLibro", dpParametros);

        if (Dt.Rows.Count > 0)
        {
            ltsConsulLibro = [
                .. from item in Dt.AsEnumerable()
                select new DTOconsultLibro
                {
                      IDLibro = item.Field<int>("IDLibro"),
                      Nombre = item.Field<string>("Nombre"),
                      Genero= item.Field<string>("Genero"),
                      ISBN= item.Field<string>("ISBN"),
                      Editorial= item.Field<string>("Editorial"),
                      //Precio= item.Field<int>("Precio")
                }
            ];
        }

        return ltsConsulLibro;
    }

    public static List<string> ActualizarDatos(string Cadena, Libro PLibro)
    {
        List<string> lstDatos = [];
        try
        {
            var dpParemtros = new
            {
                P_IDLibro = PLibro.IDLibro,
                P_Nombre = PLibro.Nombre.ToUpper(),
                P_Genero = PLibro.Genero.ToUpper(),
                P_ISBN = PLibro.ISBN,
                P_Editorial = PLibro.Editorial,
                P_Precio = PLibro.Precio
            };

            Context.Procedimiento_StoreDB(Cadena, "sp_ActualizarLibro", dpParemtros);
            lstDatos.Add("00");
            lstDatos.Add("Libro Actualizado con éxito");
        }
        catch (Exception e)
        {
            lstDatos.Add("14");
            lstDatos.Add(e.Message);
        }
        return lstDatos;
    }
    public static List<string> EliminarLibro(string Cadena, Libro PLibro)
    {
        List<string> lstDatos = [];
        try
        {
            var dpParamtros = new
            {
                P_IDLibro = PLibro.IDLibro
            };
            Context.Procedimiento_StoreDB(Cadena, "sp_EliminarLibro", dpParamtros);
            lstDatos.Add("00");
            lstDatos.Add("Libro Eliminado con éxito");
        }
        catch (Exception e)
        {
            lstDatos.Add("14");
            lstDatos.Add(e.Message);
        }
        return lstDatos;
    }

}
