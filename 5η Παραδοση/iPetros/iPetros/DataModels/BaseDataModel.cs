using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iPetros
{
    [Documentation("Η βάση όλων των μοντέλων μας")]
    public class BaseDataModel
    {
        #region Public Properties

        [Documentation("Μοναδικό id μοντέλου")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        #endregion

        #region Constructors

        [Documentation("Βασικός κατασκευαστής")]
        public BaseDataModel() : base()
        {

        }

        #endregion
    }
}
