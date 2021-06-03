using Atom.Core;
using Atom.Windows.Controls;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace iPetros
{
    [Documentation("Αντιπροσωπεύει τη γενική σελίδα του πελάτη")]
    public class CustomerPage : BaseDataVerticalMenuInitializablePage<CustomerDataModel>
    {
        [Documentation("Κατασκευαστής με χρήση μοντέλου πελάτη")]
        public CustomerPage(CustomerDataModel model) : base(model)
        {
        }

        [Documentation("Κατασκευαστής με χρήση id μοντέλου πελάτη")]
        public CustomerPage(string modelId) : base(modelId)
        {
        }

        [Documentation("Επιστρέφει τον πελάτη με το συγκεκριμένο id")]
        protected override async Task<IFailable<CustomerDataModel>> GetDataAsync([ParameterDocumentation("Το id του πελάτη")]string modelId)
        {
            return await iPetrosDI.GetDataStorage.GetCustomerAsync(modelId.ToInt());
        }

        [Documentation("Επιλέγει το id του πελάτη από το συγκεκριμένο μοντέλο")]
        protected override string GetId([ParameterDocumentation("Το μοντέλο του πελάτη")]CustomerDataModel model)
        {
            return model.Id.ToString();
        }

        [Documentation("Χρησιμοποιείτε μετά την αρχικοποίηση της σελίδας για να προσθέσει τα απαραίτητα στοιχεία του UI")]
        protected override Task OnSuccessfulInitializationAsync(CustomerDataModel model)
        {
            AddSaveable("Πληροφορίες", IconPaths.InformationPath, (menu, button) => Task.FromResult(new CustomerInformationPage(model)));
            Add("Ραντεβού", IconPaths.MapMarkerPath, (menu, button) => Task.FromResult<FrameworkElement>(new CustomerAppointmentsPage(new CustomerAppointmentDataStorageArgs() { Customers = new List<int>() { model.Id } })));
            Add("Ιατρικό ιστορικό", IconPaths.AccountHeartPath, (menu, button) => Task.FromResult<FrameworkElement>(new Grid()));
            Add("Χρεώσεις", IconPaths.CashRefundPath, (menu, button) => Task.FromResult<FrameworkElement>(new Grid()));
            Add("Πληρωμές", IconPaths.CashCheckPath, (menu, button) => Task.FromResult<FrameworkElement>(new Grid()));
            Add("Logs", IconPaths.BookOpenPagePath, (menu, button) => Task.FromResult<FrameworkElement>(new Grid()));

            return Task.CompletedTask;
        }
    }
}
