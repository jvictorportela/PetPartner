using FluentValidation;
using PetPartner.Communication.Requests.Pet;

namespace PetPartner.Application.UseCases.Pet;

// Vai ser usado para registrar e atualizar.
public class PetValidator : AbstractValidator<RequestPetJson>
{
    public PetValidator()
    {
        RuleFor(pet => pet.Name)
            .NotEmpty().WithMessage("The pet's name cannot be empty.");

        RuleFor(pet => pet.Species)
            .NotEmpty().WithMessage("The species cannot be empty.");

        RuleFor(pet => pet.Breed)
            .NotEmpty().WithMessage("The breed cannot be empty.");

        RuleFor(pet => pet.Gender)
            .NotEmpty().WithMessage("The gender cannot be empty."); //Obrigar no front a ter apenas male - female

        RuleFor(pet => pet.BirthDate)
            .LessThan(DateTime.UtcNow).WithMessage("The birth date must be in the past.");

        //RuleFor(pet => pet.Weight)
        //    .GreaterThan(0).When(pet => pet.Weight.HasValue)
        //    .WithMessage("The weight must be a positive number.");

        RuleFor(pet => pet.Price)
            .GreaterThan(0).When(pet => pet.AvailableForSale == true)
            .WithMessage("The price must be greater than zero.");

        RuleFor(pet => pet.AvailableForAdoption)
            .NotNull().WithMessage("The adoption availability cannot be null.");

        RuleFor(pet => pet.AvailableForSale)
            .NotNull().WithMessage("The sale availability cannot be null.");

        RuleFor(pet => pet.BreedingNotes)
            .NotEmpty().WithMessage("Breeding notes must be provided if the pet is available for breeding.")
            .When(pet => pet.AvailableForBreeding);
    }
}