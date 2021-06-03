using Atom.Core;
using Atom.Relational;
using Atom.Windows.Controls;
using Atom.Windows.Controls.Abstractions;

using System.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iPetros
{
    [Documentation("Αντιπροσωπεύει τη σελίδα των υπαλλήλων")]
    public class StaffMembersPage : StandardConfigurableDataPresenterPaginatedPage<StaffMemberDataModel, DateDataStorageArgs>
    {
        #region Protected Properties

        [Documentation("Αντιπροσωπεύει το κουμπί προσθήκης")]
        protected IconButton AddButton { get; private set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public StaffMembersPage([ParameterDocumentation("Κανόνες αναζήτησης")] DateDataStorageArgs args = null) : base(args)
        {
            CreateGUI();
        }

        #endregion

        #region Protected Methods

        [Documentation("Επιστρέφει το πλέγμα παρουσίασης δεδομένων")]
        protected override IDataPresenter<StaffMemberDataModel> CreateDataPresenterCore()
        {
            var dataGrid = iPetrosDataModelHelpers.CreateStaffMemberDataModelDataGrid();

            dataGrid.ConfigureFilters((container, grid) =>
            {
                container.AddSearchFilter(value =>
                {
                    Args.Search = value;

                    Update();
                });
            });

            if (iPetrosDI.ConnectedUser.Type == StaffMemberType.Admin)
            {

                dataGrid.ConfigureOptions((container, grid, row, model) =>
                {
                    container.AddEditOption(
                        "Επεξεργασία υπαλλήλου",
                        null,
                        () => iPetrosDataModelHelpers.CreateStaffMemberDataModelDataForm(),
                        async model => await iPetrosDI.GetDataStorage.UpdateStaffMemberAsync(model));
                    container.AddDeleteOption(
                        "Διαγραφή υπαλλήλου",
                        null,
                        async model => await iPetrosDI.GetDataStorage.DeleteStaffMemberAsync(model.Id),
                        Args);
                });
            }

            return dataGrid;
        }

        [Documentation("Επιστρέφει τα δεδομένα παρουσίασης")]
        protected override async Task<IFailable<IEnumerable<StaffMemberDataModel>>> GetDataAsync(int pageIndex)
        {
            return await iPetrosDI.GetDataStorage.GetStaffMembersAsync(pageIndex, Args);
        }

        [Documentation("Επιστρέφει τον χάρτη αντιστοίχισης")]
        protected override PropertyMapper<StaffMemberDataModel> GetPropertyMapper()
        {
            return iPetrosDataModelHelpers.StaffMemberDataModelMapper.Value;
        }

        [Documentation("Επιστρέφει τον χειριστή των δεδομένων")]
        protected override PropertyOptionsDataStorage<StaffMemberDataModel> GetPropertyOptionsDataStorage()
        {
            return iPetrosDI.GetDataStorage.StaffMembersPropertyOptionsDataStorage.Value;
        }

        [Documentation("Επιστρέφει τον μεταφραστή")]
        protected override Translator<StaffMemberDataModel> GetTranslator()
        {
            return iPetrosDataModelHelpers.StaffMemberDataModelTranslator.Value;
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
                    return iPetrosDataModelHelpers.CreateStaffMemberDataModelDataForm()
                     .ShowSelectEnumSingleOptionInput(x => x.Type, (dataForm, propertyInfo) => new DropDownMenuEnumOptionsFormInput<StaffMemberType>(dataForm, propertyInfo, new List<StaffMemberType>() { StaffMemberType.Moderator, StaffMemberType.StaffMember }, x => x.ToLocalizedString()), settings => settings.IsRequired = true);
                },
                () => new StaffMemberDataModel(),
                async model => await iPetrosDI.GetDataStorage.AddStaffMemberAsync(model),
                Args);

            AddButton.Visibility = iPetrosDI.ConnectedUser.Type == StaffMemberType.Admin ? Visibility.Visible : Visibility.Collapsed;

            ContentGrid.Children.Add(AddButton);
        }

        #endregion
    }
}
