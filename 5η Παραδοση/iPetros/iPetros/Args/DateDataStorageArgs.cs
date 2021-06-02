
using System;

namespace iPetros
{
    [Documentation("Περιέχει τους βασικούς κανόνες για την αναζήτηση δεδομένων χρησιμοποιώντας την ημερομηνία δημιουργίας")]
    public class DateDataStorageArgs : DataStorageArgs
    {
        #region Public Properties

        [Documentation("Περιορίζει τα δεδομένα χρησιμοποιόντας μια μεταγενέστερη ημερομηνία δημιουργίας")]
        public DateTimeOffset After { get; set; }

        [Documentation("Περιορίζει τα δεδομένα χρησιμοποιόντας μια προγενέστερη ημερομηνία δημιουργίας")]
        public DateTimeOffset Before { get; set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public DateDataStorageArgs()
        {

        }

        #endregion
    }
}
