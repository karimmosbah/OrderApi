using OrderApi.Models;

namespace OrderApi.Services
{
    public interface IOrderServices
    {
        Task<List<Order>> GetAll();
        Task<Order> GetById(int id);
        Task<Order> Add(Order order);
        Order Update(Order order);
        Order Delete(Order order);
    }
}
