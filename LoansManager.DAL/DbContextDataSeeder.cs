using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace LoansManager.DAL
{
    public static class DbContextDataSeeder
    {
        public static void SeedData<T>(this IApplicationBuilder app)
            where T : DbContext
        {
            if (app == null)
            {
                throw new ArgumentNullException($"{nameof(app)} can not be null");
            }

            var context = (T)app.ApplicationServices.GetService(typeof(T));

            context.Database.Migrate();
        }
    }
}
