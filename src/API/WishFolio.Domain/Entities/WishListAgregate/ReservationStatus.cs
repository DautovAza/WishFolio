namespace WishFolio.Domain;

public enum ReservationStatus
{
    None =0,
    Available = 2,
    Reserved = 4,
    ReservedAnonymous = 8,
    Clsoed = 16,
}