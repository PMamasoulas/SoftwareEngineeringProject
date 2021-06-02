using Atom.Core;

namespace iPetros
{
    [Documentation("Παρέχει εύκολη πρόσβαση στις υπηρεσίες της εφαρμογής")]
    public static class iPetrosDI
    {
        [Documentation("Ο συνδεμένος χρήστης")]
        public static StaffMemberDataModel ConnectedUser { get; set; }

        [Documentation("Επιστρέφει τον διαχειριστή της βάσης δεδομένων")]
        public static iPetrosDataStorage GetDataStorage => CoreDI.GetService<iPetrosDataStorage>();
    }
}
