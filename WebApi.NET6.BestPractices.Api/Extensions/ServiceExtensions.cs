using BestPractices.Core.Account.Data;
using BestPractices.Core.Book.Data;

namespace BestPractices.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void LoadFakeData(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddSingleton<BooksInMemory>();
            serviceProvider.AddSingleton<UsersInMemory>();
        }
    }
}
