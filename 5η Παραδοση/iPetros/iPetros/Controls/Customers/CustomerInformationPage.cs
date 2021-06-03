using Atom.Core;
using Atom.Windows.Controls;

using System;
using System.Threading.Tasks;

namespace iPetros
{
    [Documentation("Αντιρποσωπεύει τη σελίδα πληροφοριών του πελάτη")]
    public class CustomerInformationPage : BaseDataFormPage<CustomerDataModel>
    {
        [Documentation("Το μοντέλο του πελάτη")]
        public CustomerDataModel Model { get; }

        [Documentation("Κατασκευαστής με χρήση μοντέλου πελάτη")]
        public CustomerInformationPage([ParameterDocumentation("Το μοντέλο του πελάτη")] CustomerDataModel model) : base("Πελάτης")
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));

            Form.Model = model;
            Form.UpdateFormValues();
        }

        #region Protected Methods

        [Documentation("Επιστρέφει τη φόρμα συμπλήρωσης πληροφοριών πελάτη")]
        protected override DataForm<CustomerDataModel> CreateDataForm()
        {
            return iPetrosDataModelHelpers.CreateCustomerDataModelDataForm();
        }

        [Documentation("Αποθηκεύει τις αλλαγές που έγιναν στον μοντέλο του πελάτη")]
        protected override async Task<IFailable> SaveChangesAsync([ParameterDocumentation("Το μοντέλο του πελάτη")] CustomerDataModel model)
        {
            return await iPetrosDI.GetDataStorage.UpdateCustomerAsync(model);
        }

        #endregion
    }
}
