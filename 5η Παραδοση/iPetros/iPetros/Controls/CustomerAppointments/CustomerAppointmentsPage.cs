using Atom.Core;
using Atom.Relational;
using Atom.Windows.Controls;
using Atom.Windows.Controls.Abstractions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPetros
{
    [Documentation("Αντιπροσωπεύει τη σελίδα των ραντεβού των πελατών")]
    public class CustomerAppointmentsPage : StandardConfigurableDataPresenterPaginatedPage<CustomerAppointmentDataModel, CustomerAppointmentDataStorageArgs>
    {
        #region Protected Properties

        [Documentation("Αντιπροσωπεύει το κουμπί προσθήκης")]
        protected IconButton AddButton { get; private set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public CustomerAppointmentsPage(CustomerAppointmentDataStorageArgs args = null) : base(args)
        {
            CreateGUI();
        }

        #endregion

        #region Protected Properties

        [Documentation("Επιστρέφει το πλέγμα παρουσίασης δεδομένων")]
        protected override IDataPresenter<CustomerAppointmentDataModel> CreateDataPresenterCore()
        {
            var dataGrid = iPetrosDataModelHelpers.CreateCustomerAppointmentDataModelDataGrid();

            dataGrid.ConfigureFilters((container, grid) =>
            {
                container.AddSearchFilter(value =>
                {
                    Args.Search = value;

                    Update();
                });

                container.AddDaySpanFilter("Περιθώριο Έναρξης - Λήξης", value =>
                {
                    if (value == null)
                    {
                        Args.DateStartAfter = null;
                        Args.DateStartBefore = null;
                    }
                    else
                    {
                        Args.DateStartAfter = value.Value.StartingDate;
                        Args.DateStartBefore = value.Value.EndingDate;
                    }

                    Update();
                });
            });

            dataGrid.ConfigureOptions((container, grid, row, model) =>
            {
                container.AddEditOption(
                    "Επεξεργασία ραντεβού πελάτη",
                    null,
                    () => iPetrosDataModelHelpers.CreateCustomerAppointmentDataModelDataForm(),
                    async model => await iPetrosDI.GetDataStorage.UpdateCustomerAppointmentAsync(model));
                container.AddDeleteOption(
                    "Διαγραφή ραντεβού πελάτη",
                    null,
                    async model => await iPetrosDI.GetDataStorage.DeleteCustomerAppointmentAsync(model.Id),
                    Args);
            });

            return dataGrid;
        }

        [Documentation("Επιστρέφει τα δεδομένα παρουσίασης")]
        protected override async Task<IFailable<IEnumerable<CustomerAppointmentDataModel>>> GetDataAsync(int pageIndex)
        {
            return await iPetrosDI.GetDataStorage.GetCustomerAppointmentsAsync(pageIndex, Args);
        }

        [Documentation("Επιστρέφει τον χάρτη αντιστοίχισης")]
        protected override PropertyMapper<CustomerAppointmentDataModel> GetPropertyMapper()
        {
            return iPetrosDataModelHelpers.CustomerAppointmentDataModelMapper.Value;
        }

        [Documentation("Επιστρέφει τον χειριστή των δεδομένων")]
        protected override PropertyOptionsDataStorage<CustomerAppointmentDataModel> GetPropertyOptionsDataStorage()
        {
            return iPetrosDI.GetDataStorage.CustomerAppointmentsPropertyOptionsDataStorage.Value;
        }

        [Documentation("Επιστρέφει τον μεταφραστή")]
        protected override Translator<CustomerAppointmentDataModel> GetTranslator()
        {
            return iPetrosDataModelHelpers.CustomerAppointmentDataModelTranslator.Value;
        }

        #endregion

        #region Private Methods

        [Documentation("Δημιουργεί και προσθέτει τμήματα του UI")]
        private void CreateGUI()
        {
            AddButton = ControlsFactory.CreateStandardAddCircularButton(
                "Προσθήκη ραντεβού πελάτη",
                null,
                DataPresenter,
                () =>
                {
                    var form = iPetrosDataModelHelpers.CreateCustomerAppointmentDataModelDataForm();

                    form.ShowSearchInput<CustomerDataModel, int>(x => x.CustomerId, async search => await iPetrosDI.GetDataStorage.GetCustomersAsync(0, new DateDataStorageArgs() { Search = search }), x => x.FirstName + " " + x.LastName, x => x.Id, x => null);
                    form.ShowSearchInput<StaffMemberDataModel, int>(x => x.StaffMemberId, async search => await iPetrosDI.GetDataStorage.GetStaffMembersAsync(0, new DateDataStorageArgs() { Search = search }), x => x.FirstName + " " + x.LastName, x => x.Id, x => null);

                    return form;
                },
                () => new CustomerAppointmentDataModel() { DateStart = DateTimeOffset.Now, DateEnd = DateTimeOffset.Now.AddHours(1) },
                async model => await iPetrosDI.GetDataStorage.AddCustomerAppointmentAsync(model),
                Args);

            ContentGrid.Children.Add(AddButton);
        }

        #endregion
    }
}
