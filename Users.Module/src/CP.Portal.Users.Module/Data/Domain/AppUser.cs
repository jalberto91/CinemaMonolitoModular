using Microsoft.AspNetCore.Identity;

namespace CP.Portal.Users.Module.Data.Domain;

public sealed class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    private readonly List<CartMovie> _cartMovies = [];
    public IReadOnlyList<CartMovie> CartItems => _cartMovies.AsReadOnly();

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

    internal void RemoveMovieFromCart(Guid movieId)
    {
        var itemFromCart = _cartMovies.FirstOrDefault(m => m.MovieId == movieId);
        if (itemFromCart is null)
        {
            return;
        }
    
        _cartMovies.Remove(itemFromCart);
    }

    public void ClearCart() => _cartMovies.Clear();

}
