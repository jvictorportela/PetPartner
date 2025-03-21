namespace PetPartner.Domain.Entities;

public class Address : EntityBase
{
    public string Street { get; private set; } = string.Empty; //RUA
    public string Number { get; private set; } = string.Empty; //NÚMERO
    public string Neighborhood { get; private set; } = string.Empty; //BAIRRO
    public string Complement { get; private set; } = string.Empty; //COMPLEMENTO
    public string Country { get; set; } = string.Empty; //País
    public string City { get; private set; } = string.Empty; //CIDADE
    public string State { get; private set; } = string.Empty; //ESTADO
    public string ZipCode { get; private set; } = string.Empty; //CEP
    public long UserId { get; private set; }
}
