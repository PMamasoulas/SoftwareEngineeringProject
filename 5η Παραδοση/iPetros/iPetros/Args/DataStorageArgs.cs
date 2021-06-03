using Atom.Core;

using System.Collections.Generic;

namespace iPetros
{
    [Documentation("Περιέχει τους βασικούς κανόνες για την αναζήτηση δεδομένων")]
    public class DataStorageArgs : IOffsetable
    {
        #region Private Members

        private string mSearch;

        #endregion

        #region Public Properties

        [Documentation("Η αποκλήση")]
        public int Offset { get; set; }

        [Documentation("Περιορίζει τα αποτελέσματα χρησιμοποιώντας ένα κείμενο")]
        public string Search
        {
            get => mSearch?.ToUpper();

            set => mSearch = value;
        }

        [Documentation("Περιορίζει τα αποτελέσματα χρησιμοποιώντας συγκεκριμένα αναγνωριστικά")]
        public IEnumerable<int> Include { get; set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public DataStorageArgs()
        {

        }

        #endregion
    }
}
