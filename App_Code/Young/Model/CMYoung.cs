using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ding.Core;

namespace Elim.Young
{
  [Serializable]
  public class CMYoung : Model
  {
    #region Properties
    public PMYoung Young;
    #endregion

    #region Constructor
    public CMYoung() {
      this.Young = new PMYoung();
    }
    #endregion

  }
}