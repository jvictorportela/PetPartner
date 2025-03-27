using System.ComponentModel.DataAnnotations.Schema;

namespace PetPartner.Domain.Entities;

[Table("Pictures")]
public class Picture : EntityBase
{
    public long PetId { get; set; }
    public string Url { get; set; } = string.Empty; // Caminho da imagem armazenada
}
