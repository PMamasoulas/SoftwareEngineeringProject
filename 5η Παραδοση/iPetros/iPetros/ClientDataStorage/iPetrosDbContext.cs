using Atom.Relational;

using Microsoft.EntityFrameworkCore;

namespace iPetros
{
    [Documentation("Περιέχει πληροφορίες σχετικά με τη δομή της βάσης δεδομένων")]
    public class iPetrosDbContext : StandardDbContext
    {
        #region Public Properties

        [Documentation("Αντιπροσωπεύει τον πίνακα του προσωπικού")]
        public DbSet<StaffMemberDataModel> StaffMembers { get; set; }

        [Documentation("Αντιπροσωπεύει τον πίνακα των πελατών")]
        public DbSet<CustomerDataModel> Customers { get; set; }

        [Documentation("Αντιπροσωπεύει τον πίνακα των ραντεβού")]
        public DbSet<CustomerAppointmentDataModel> CustomerAppointmets { get; set; }

        [Documentation("Αντιπροσωπεύει τον πίνακα των logs")]
        public DbSet<LogDataModel> Logs { get; set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public iPetrosDbContext([ParameterDocumentation("Πληροφορίες σχετικά με την αρχικοποίηση της βάσης δεδομένων")] DbContextOptions<iPetrosDbContext> options) : base(options)
        {
        }

        #endregion

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerAppointmentDataModel>()
                .HasOne(x => x.StaffMember)
                .WithMany()
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.StaffMemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomerAppointmentDataModel>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        #endregion
    }
}
