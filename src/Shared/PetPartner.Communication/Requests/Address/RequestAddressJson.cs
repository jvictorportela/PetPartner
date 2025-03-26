namespace PetPartner.Communication.Requests.Address;

public class RequestAddressJson
{
    public string Street { get; set; } = string.Empty; //RUA
    public string Number { get; set; } = string.Empty; //NÚMERO
    public string Neighborhood { get; set; } = string.Empty; //BAIRRO
    public string Complement { get; set; } = string.Empty; //COMPLEMENTO
    public string Country { get; set; } = string.Empty; //País
    public string City { get; set; } = string.Empty; //CIDADE
    public string State { get; set; } = string.Empty; //ESTADO
    public string ZipCode { get; set; } = string.Empty; //CEP
}
