using PetPartner.Communication.Requests.Picture;

namespace PetPartner.Communication.Requests.Pet;

public class RequestPetJson
{
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty; // Ex: "Dog", "Cat", "Horse", "Cattle"
    public string Breed { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = string.Empty; // "Male" or "Female"
    public bool? HasPedigree { get; set; } // True = has pedigree
    public string? Color { get; set; }
    //public decimal? Weight { get; set; } // In Kg

    // Health Information
    public bool Vaccinated { get; set; } = true;
    public bool Dewormed { get; set; } = true; //Vermifugado
    public string HealthNotes { get; set; } = string.Empty;

    // Availability for Sale or Adoption
    public bool AvailableForSale { get; set; } = true;
    public bool AvailableForAdoption { get; set; } = true;
    public double? Price { get; set; } // Nullable, since it may be for adoption

    // Breeding Information (if the pet is a breeder)
    public bool AvailableForBreeding { get; set; } = true;
    public string BreedingNotes { get; set; } = string.Empty;

    // Fotos e mídia
    public List<RequestPictureJson>? Pictures { get; set; } = [];
}
