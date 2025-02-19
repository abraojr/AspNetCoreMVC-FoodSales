using SalesFood.Models;

namespace SalesFood.Repositories.Interfaces;
public interface IOrderRepository
{
    void CreateOrder(Order order);
}