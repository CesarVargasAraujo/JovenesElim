using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ding.Core;
using Elim.Young;

namespace Elim.Event
{
  [Serializable]
  public class CMEvent : Model
  {
    #region Properties
    public PMEvent Event;
    public PMYoung Young;
    #endregion

    #region Constructor
    public CMEvent() {
      this.Event = new PMEvent();
      this.Young = new PMYoung();
    }
    #endregion


  }
}