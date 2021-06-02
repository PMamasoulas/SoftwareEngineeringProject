using Atom.Core;

namespace iPetros
{
    [Documentation("Περιέχει τους βασικούς κανόνες για την αναζήτηση δεδομένων")]
    public class DataStorageArgs : IOffsetable
    {
        #region Public Properties

        [Documentation("Η αποκλήση")]
        public int Offset { get; set; }

        [Documentation("Περιορίζει τα αποτελέσματα χρησιμοποιώντας ένα κείμενο")]
        public string Search { get; set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public DataStorageArgs()
        {

        }

        #endregion
    }
}
