namespace iPetros
{
    [Documentation("Αντιπροσωπεύει ένα log")]
    public class LogDataModel : BaseDateDataModel
    {
        #region Public Properties

        [Documentation("Το μήνυμα")]
        public string Message { get; set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public LogDataModel()
        {

        }

        #endregion
    }
}
