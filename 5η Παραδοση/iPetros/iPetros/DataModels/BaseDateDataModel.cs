using Atom.Core;

using System;

namespace iPetros
{
    [Documentation("Η βάση όλων των μοντέλων μας που αποθηκεύουν πληροφρίες σχετικά με την ημερομηνία δημιουργίας και την ημερομηνία επεξεργασίας")]
    public class BaseDateDataModel : BaseDataModel, IDateCreateable, IDateModifiable
    {
        #region Public Properties

        [Documentation("Η ημερομηνία δημιουργίας")]
        public DateTimeOffset DateCreated { get; set; }

        [Documentation("Η ημερομηνία τελευταίας επεξεργασίας")]
        public DateTimeOffset DateModified { get; set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public BaseDateDataModel() : base()
        {

        }

        #endregion
    }
}
