using Atom.Core;
using Atom.Relational;
using Atom.Windows.Controls;
using Atom.Windows.Controls.Abstractions;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPetros
{
    [Documentation("Αντιπροσωπεύει τη σελίδα των πελατών")]
    public class CustomersPage : StandardConfigurableDataPresenterPaginatedPage<CustomerDataModel, DateDataStorageArgs>
    {
        #region Protected Properties

        [Documentation("Αντιπροσωπεύει το κουμπί προσθήκης")]
        protected IconButton AddButton { get; private set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public CustomersPage(DateDataStorageArgs args = null) : base(args)
        {
            CreateGUI();
        }

        #endregion

        #region Protected Methods

        [Documentation("Επιστρέφει το πλέγμα παρουσίασης δεδομένων")]
        protected override IDataPresenter<CustomerDataModel> CreateDataPresenterCore()
        {
            var dataGrid = iPetrosDataModelHelpers.CreateCustomerDataModelDataGrid();

            dataGrid.ConfigureFilters((container, grid) =>
            {
                container.AddSearchFilter(value =>
                {
                    Args.Search = value;

                    Update();
                });
            });

            dataGrid.ConfigureOptions((container, grid, row, model) =>
            {
                container.AddEditOption(
                    "Επεξεργασία πελάτη",
                    null,
                    () => iPetrosDataModelHelpers.CreateCustomerDataModelDataForm(),
                    async model => await iPetrosDI.GetDataStorage.UpdateCustomerAsync(model));
                container.AddDeleteOption(
                    "Διαγραφή πελάτη",
                    null,
                    async model => await iPetrosDI.GetDataStorage.DeleteCustomerAsync(model.Id),
                    Args);
            });

            dataGrid.SetOpenSaveablePageClickCommand(x => x.FirstName + " " + x.LastName,
                                                     x => IconPaths.AccountGroupPath,
                                                     model => new CustomerPage(model),
                                                     model => $"customer{model.Id}");

            return dataGrid;
        }

        [Documentation("Επιστρέφει τα δεδομένα παρουσίασης")]
        protected override async Task<IFailable<IEnumerable<CustomerDataModel>>> GetDataAsync(int pageIndex)
        {
            return await iPetrosDI.GetDataStorage.GetCustomersAsync(pageIndex, Args);
        }

        [Documentation("Επιστρέφει τον χάρτη αντιστοίχισης")]
        protected override PropertyMapper<CustomerDataModel> GetPropertyMapper()
        {
            return iPetrosDataModelHelpers.CustomerDataModelMapper.Value;
        }

        [Documentation("Επιστρέφει τον χειριστή των δεδομένων")]
        protected override PropertyOptionsDataStorage<CustomerDataModel> GetPropertyOptionsDataStorage()
        {
            return iPetrosDI.GetDataStorage.CustomersPropertyOptionsDataStorage.Value;
        }

        [Documentation("Επιστρέφει τον μεταφραστή")]
        protected override Translator<CustomerDataModel> GetTranslator()
        {
            return iPetrosDataModelHelpers.CustomerDataModelTranslator.Value;
        }

        #endregion

        #region Private Methods

        [Documentation("Δημιουργεί και προσθέτει τμήματα του UI")]
        private void CreateGUI()
        {
            AddButton = ControlsFactory.CreateStandardAddCircularButton(
                "Προσθήκη υπαλλήλου",
                null,
                DataPresenter,
                () =>
                {
                    return iPetrosDataModelHelpers.CreateCustomerDataModelDataForm();
                },
                () => new CustomerDataModel(),
                async model => await iPetrosDI.GetDataStorage.AddCustomerAsync(model),
                Args);

            ContentGrid.Children.Add(AddButton);
        }

        #endregion
    }
}
