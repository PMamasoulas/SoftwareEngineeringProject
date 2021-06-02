using Atom.Core;
using Atom.Relational;
using Atom.Web.Accounts;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPetros
{
    [Documentation("Περιέχει βοηθητικές μεθόδους χρήσιμες για την επικοινωνία με τη βάση δεδομένων")]
    public class iPetrosDataStorage : BaseDataStorage<iPetrosDbContext>
    {
        #region Constants

        public const int PerPage = 10;

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public iPetrosDataStorage([ParameterDocumentation("Η δομή της βάσης δεδομένων η οποία θα χρησιμοποιηθεί")]iPetrosDbContext dbContext) : base(dbContext)
        {
        }

        #endregion

        #region Public Methods

        [Documentation("Επαναφέρει τη βάση δεδομένων στην αρχική της κατάσταση")]
        public override async Task<IFailable> ResetAsync()
        {
            await base.ResetAsync();

            DbContext.StaffMembers.Add(new StaffMemberDataModel() { FirstName = "admin", LastName = "admin", Username = "admin", Password = "admin" });

            await DbContext.SaveChangesAsync();

            return new Failable();
        }

        [Documentation("Επιβεβαιώνει την ορθότητα των στοιχείων σύνδεσης")]
        public async Task<Failable<StaffMemberDataModel>> LogInAsync(LogInRequestModel model)
        {
            try
            {
                var result = await DbContext.StaffMembers.FirstOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);

                return new Failable<StaffMemberDataModel>() 
                { 
                    ErrorMessage = result == null ? "Λανθασμένα στοιχεία σύνδεσης" : null,
                    Result = result
                };
            }
            catch(Exception ex)
            {
                return ex;
            }

        }

        #region Staff Members

        [Documentation("Προσθέτει ένα νέο μέλος πρωσπικού")]
        public Task<DataStorageResult<StaffMemberDataModel>> AddStaffMemberAsync([ParameterDocumentation("Το μέλος προσωπικού")] StaffMemberDataModel staffMember) 
            => AddAsync(x => x.StaffMembers, staffMember);

        [Documentation("Επιστρέφει τα μέλη προσωπικού")]
        public async Task<DataStorageResult<IEnumerable<StaffMemberDataModel>>> GetStaffMembersAsync([ParameterDocumentation("Ο δείκτης της σελίδας ξεκινώντας από το 0")] int page, [ParameterDocumentation("Οι κανόνες αναζήτησης")] DateDataStorageArgs args = null)
        {
            if (args == null)
                args = new DateDataStorageArgs();

            try
            {
                var query = (IQueryable<StaffMemberDataModel>)DbContext.StaffMembers;

                if (args.Search != null)
                    query = query.Where(x => x.Username.Contains(args.Search) || x.FirstName.Contains(args.Search) || x.LastName.Contains(args.Search));

                query = AttachParameters(query, args);

                return await query.OrderByDescending(x => x.Id).Skip((page * PerPage) + args.Offset).Take(PerPage).ToListAsync();
            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        [Documentation("Ενημερώνει το συγκεκριμένο μέλος προσωπικού")]
        public Task<DataStorageResult<StaffMemberDataModel>> UpdateStaffMemberAsync([ParameterDocumentation("Το μέλος προσωπικού")]StaffMemberDataModel model)
            => UpdateAsync(x => x.StaffMembers, x => x.Id == model.Id, x =>
            {
                x.FirstName = model.FirstName;
                x.LastName = model.LastName;
                x.Email = model.Email;
                x.PhoneNumber = model.PhoneNumber;
                x.Type = model.Type;
                x.Color = model.Color;
            });

        [Documentation("Διαγράφει το μέλος προσωπικού με το συγκεκριμένο id")]
        public Task<DataStorageResult<StaffMemberDataModel>> DeleteStaffMemberAsync([ParameterDocumentation("Το id του μέλους προσωπικού")] int id)
            => DeleteAsync(x => x.StaffMembers, x => x.Id == id);

        #endregion

        #region Customers

        [Documentation("Προσθέτει έναν νέο πελάτη")]
        public Task<DataStorageResult<CustomerDataModel>> AddCustomerAsync([ParameterDocumentation("Ο πελάτης")]CustomerDataModel model)
            => AddAsync(x => x.Customers, model);

        [Documentation("Επιστρέφει τους πελάτες")]
        public async Task<DataStorageResult<IEnumerable<CustomerDataModel>>> GetCustomersAsync([ParameterDocumentation("Ο δείκτης της σελίδας ξεκινώντας από το 0")] int page, [ParameterDocumentation("Οι κανόνες αναζήτησης")] DateDataStorageArgs args = null)
        {
            if (args == null)
                args = new DateDataStorageArgs();

            try
            {
                var query = (IQueryable<CustomerDataModel>)DbContext.Customers;

                if (args.Search != null)
                    query = query.Where(x => x.VAT.Contains(args.Search) || x.FirstName.Contains(args.Search) || x.LastName.Contains(args.Search));

                query = AttachParameters(query, args);

                return await query.OrderByDescending(x => x.Id).Skip((page * PerPage) + args.Offset).Take(PerPage).ToListAsync();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [Documentation("Ενημερώνει το συγκεκριμένο πελάτη")]
        public Task<DataStorageResult<CustomerDataModel>> UpdateCustomerAsync([ParameterDocumentation("Ο πελάτης")] CustomerDataModel model)
            => UpdateAsync(x => x.Customers, x => x.Id == model.Id, x =>
            {
                x.FirstName = model.FirstName;
                x.LastName = model.LastName;
                x.PhoneNumber = model.PhoneNumber;
                x.Email = model.Email;
                x.VAT = model.VAT;
            });

        [Documentation("Διαγράφει τον πελάτη με το συγκεκριμένο id")]
        public Task<DataStorageResult<CustomerDataModel>> DeleteCustomerAsync([ParameterDocumentation("Το id του πελάτη")] int id)
            => DeleteAsync(x => x.Customers, x => x.Id == id);

        #endregion

        #region Customer Appointments

        [Documentation("Προσθέτει ένα νέο ραντεβού πελάτη")]
        public Task<DataStorageResult<CustomerAppointmentDataModel>> AddCustomerAppointmentAsync([ParameterDocumentation("Το ραντεβού πελάτη")]CustomerAppointmentDataModel model)
            => AddAsync(x => x.CustomerAppointmets, model);

        [Documentation("Επιστρέφει τα ραντεβού πελατών")]
        public async Task<DataStorageResult<IEnumerable<CustomerAppointmentDataModel>>> GetCustomerAppointmentsAsync([ParameterDocumentation("Ο δείκτης της σελίδας ξεκινώντας από το 0")] int page, [ParameterDocumentation("Οι κανόνες αναζήτησης")] CustomerAppointmentDataStorageArgs args = null)
        {
            if (args == null)
                args = new CustomerAppointmentDataStorageArgs();

            try
            {
                var query = (IQueryable<CustomerAppointmentDataModel>)DbContext.CustomerAppointmets.Include(x => x.Customer).Include(x => x.StaffMember);

                if (args.Search != null)
                    query = query.Where(x => x.Customer.VAT.Contains(args.Search) || x.Customer.FirstName.Contains(args.Search) || x.Customer.LastName.Contains(args.Search) || x.Note.Contains(args.Search));

                if (args.DateStartAfter != null)
                    query = query.Where(x => x.DateStart >= args.DateStartAfter);

                if (args.DateStartBefore != null)
                    query = query.Where(x => x.DateStart <= args.DateStartBefore);

                query = AttachParameters(query, args);

                return await query.OrderByDescending(x => x.Id).Skip((page * PerPage) + args.Offset).Take(PerPage).ToListAsync();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [Documentation("Επιστρέφει όλα τα ραντεβού πελατών")]
        public async Task<DataStorageResult<IEnumerable<CustomerAppointmentDataModel>>> GetCustomerAppointmentsAsync([ParameterDocumentation("Οι κανόνες αναζήτησης")]CustomerAppointmentDataStorageArgs args = null)
        {
            if (args == null)
                args = new CustomerAppointmentDataStorageArgs();

            try
            {
                var query = (IQueryable<CustomerAppointmentDataModel>)DbContext.CustomerAppointmets.Include(x => x.Customer).Include(x => x.StaffMember);

                if (args.Search != null)
                    query = query.Where(x => x.Customer.VAT.Contains(args.Search) || x.Customer.FirstName.Contains(args.Search) || x.Customer.LastName.Contains(args.Search) || x.Note.Contains(args.Search));

                if (args.DateStartAfter != null)
                    query = query.Where(x => x.DateStart >= args.DateStartAfter);

                if (args.DateStartBefore != null)
                    query = query.Where(x => x.DateStart <= args.DateStartBefore);

                query = AttachParameters(query, args);

                return await query.OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [Documentation("Ενημερώνει το συγκεκριμένο ραντεβού πελάτη")]
        public Task<DataStorageResult<CustomerAppointmentDataModel>> UpdateCustomerAppointmentAsync([ParameterDocumentation("Το ραντεβού πελάτη")]CustomerAppointmentDataModel model)
            => UpdateAsync(x => x.CustomerAppointmets, x => x.Id == model.Id, x =>
            {
                x.DateStart = model.DateStart;
                x.DateEnd = model.DateEnd;
                x.Note = model.Note;
            });

        [Documentation("Διαγράφει το ραντεβού πελάτη με το συγκεκριμένο id")]
        public Task<DataStorageResult<CustomerAppointmentDataModel>> DeleteCustomerAppointmentAsync([ParameterDocumentation("Το id του ραντεβού προσωπικού")]int id)
            => DeleteAsync(x => x.CustomerAppointmets, x => x.Id == id);

        #endregion

        #endregion

        #region Private Methods

        private IQueryable<T> AttachParameters<T>(IQueryable<T> query, DateDataStorageArgs args)
            where T : BaseDateDataModel
        {
            if (args == null)
                args = new DateDataStorageArgs();

            if (args.After != null)
                query = query.Where(x => x.DateCreated >= args.After);

            if (args.Before != null)
                query = query.Where(x => x.DateCreated <= args.Before);

            return query;
        }
            

        #endregion
    }
}
