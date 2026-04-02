using Microsoft.AspNetCore.Identity;

namespace CP.Portal.Users.Module.Data.Domain;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    private readonly List<CartMovie> _cartMovies = [];
    public IReadOnlyList<CartMovie> CartMovies => _cartMovies;

    public AppUser(string userName, string fullName) : base(userName)
    {
        UpdateFullName(fullName);
    }

    internal void UpdateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Full name cannot be empty", nameof(fullName));
        }

        FullName = fullName;
    }

    internal CartMovie AddMovieToCard(CartMovie item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        var itemFromCart = _cartMovies.FirstOrDefault(m => m.MovieId == item.MovieId);
        if (itemFromCart is not null)
        {
            var newQuantity = itemFromCart.Quantity + item.Quantity;
            if (newQuantity <= 0)
            {
                throw new InvalidOperationException("Total quantity must be greater than zero");
            }

            itemFromCart.UpdateQuantity(newQuantity);
            itemFromCart.UpdateDescription(item.Description);
            itemFromCart.UpdateUnitPrice(item.UnitPrice);

            return itemFromCart;
        }

       _cartMovies.Add(item);

        return item;
    }
}


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