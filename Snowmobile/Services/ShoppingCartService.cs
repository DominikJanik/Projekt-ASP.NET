using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Data;
using SnowmobileShop.Models;

namespace SnowmobileShop.Services
{
    public interface IShoppingCartService
    {
        void AddItem(ShoppingCart shoppingCart, Product product, int quantity, RentalTime rentalHour);
        void Clear(ShoppingCart shoppingCart);
        decimal ComputeTotalValue(ShoppingCart shoppingCart);
        void RemoveLine(ShoppingCart shoppingCart, Product product, RentalTime rentalTime);
    }

    public class ShoppingCartService : IShoppingCartService
    {
        public void AddItem(ShoppingCart shoppingCart, Product product, int quantity, RentalTime rentalHour)
        {
            var lines = shoppingCart.Lines.ToList();

            foreach (var line in lines)
            {
                if(line.RentalTime.Id == rentalHour.Id)
                    return;
            }

            lines.Add(new ProductLine { ProductId = product.Id, Product = product, Quantity = quantity, RentalTime = rentalHour });
            shoppingCart.Lines = lines;
            
        }

        public void RemoveLine(ShoppingCart shoppingCart, Product product, RentalTime rentalTime)
        {
            var lineCollection = shoppingCart.Lines.ToList();

            lineCollection.RemoveAll(cl => cl.ProductId == product.Id && cl.RentalTime.Id == rentalTime.Id);
            shoppingCart.Lines = lineCollection;
        }

        public decimal ComputeTotalValue(ShoppingCart shoppingCart)
        {
            var lineCollection = shoppingCart.Lines.ToList();

            return lineCollection.Sum(cl => cl.Product.Price * cl.Quantity);
        }

        public void Clear(ShoppingCart shoppingCart)
        {
            var lineCollection = shoppingCart.Lines.ToList();
            lineCollection.Clear();
            shoppingCart.Lines = lineCollection;
        }
    }
}


