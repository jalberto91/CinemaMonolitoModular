namespace CP.Portal.Users.Module.Data.Domain;

public sealed class CartMovie
{
    public CartMovie(
        Guid movieId,
        string description,
        int quantity,
        decimal unitPrice
    )
    {

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero");
        }

        MovieId = movieId;
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public string UserId { get; private set; } = default!;
    public AppUser? User { get; private set; } = default!;
    public Guid MovieId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public int  Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    
    internal void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero");
        }

        Quantity = quantity;
    }

    internal void UpdateDescription(string description)
    {
        Description = description;
    }

    internal void UpdateUnitPrice(decimal unitPrice)
    {
        if (unitPrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative");
        }

        UnitPrice = unitPrice;
    }
}