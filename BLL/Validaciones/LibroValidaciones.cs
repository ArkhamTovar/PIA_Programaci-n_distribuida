using ENTITY;
using FluentValidation;

namespace BLL.Validaciones;

public class LibroValidaciones : AbstractValidator<Libro>
{

    public LibroValidaciones() //LibroValidaciones
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("Revise que el nombre no este vacio")
                              .MinimumLength(2).WithMessage("Revise que el nombre sea mayor a 2 caracteres")
                              .MaximumLength(50).WithMessage("El nombre de tu libro tiene que tener entre 2 a 50 caracteres");

        RuleFor(x => x.ISBN).NotEmpty().WithMessage("Revise que el ISBN no este vacio")
                           .Length(12).WithMessage("El ISBN debe tener una longitud de 12 caracteres");
                           
    }

}
