using WebStore.ViewModels;

namespace WebStore.Services.Interfaces
{
    interface ICartService
    {
        void Add(int Id);
        void Decrement(int Id);
        void Remove(int Id);
        void Clear();
        CartViewModel GetViewModel();
    }
}
