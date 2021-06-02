using Atom.Core;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace iPetros
{
    [Documentation("Παρέχει βοηθητικές μεθόδους επέκτασης για τις υπηρεσίες της εφαρμογής")]
    public static class FrameworkConstructionExtensions
    {
        [Documentation("Προσθέτει τις υπηρεσίες της εφαρμογής")]
        public static FrameworkConstruction AddiPetrosServices(this FrameworkConstruction construction)
        {
            construction.Services.AddDbContext<iPetrosDbContext>(options => options.UseSqlite("DataSource =iPetros.db"));
            construction.Services.AddTransient<iPetrosDataStorage>();

            return construction;
        }
    }
}
