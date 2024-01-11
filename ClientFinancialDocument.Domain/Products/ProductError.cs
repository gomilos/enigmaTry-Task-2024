using ClientFinancialDocument.Domain.Abstraction;

namespace ClientFinancialDocument.Domain.Products
{
    public static class ProductError
    {
        public static Error NotFound = new(
        "Booking.NotFound",
        "The booking with the specific identifier was not found");
    }
}
