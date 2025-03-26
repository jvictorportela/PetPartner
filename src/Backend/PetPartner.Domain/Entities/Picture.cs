namespace PetPartner.Domain.Entities;

public class Picture : EntityBase
{
    public int PetId { get; set; }
    //public Pet? Pet { get; set; }
    public string Url { get; set; } = string.Empty; // Caminho da imagem armazenada
}
