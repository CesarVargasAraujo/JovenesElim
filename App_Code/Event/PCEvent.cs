using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ding.Core;
using Elim.Core;
using Elim.Young;
using Ding.Enums;

namespace Elim.Event
{
  public class PCEvent : ControllerP<CMEvent>
  {
    private DBEvent DBEvent;
    private PCYoung PCYoung;
    public PCEvent() : base() { DBEvent = new DBEvent(DB); PCYoung = new PCYoung(); }
    public PCEvent(Guid? Model) : this() { SetModel(Model); }

    #region Register
		public bool RegisterAssistance(Guid EventId, Guid? YoungId, Guid? ChurchId, string Name, string Surnames, string Email, string Facebook, DateTime? Birthday, decimal Cooperation)
    {
      Model.Event = DBEvent.Get(EventId);
			if (PCYoung.SaveYoung(YoungId,ChurchId, Name, Surnames, Email, Facebook, Birthday))
      {
				DBEvent.InsertAssistence(EventId, PCYoung.Model.Young.YoungId.G(), Cooperation);
        AddMessage("IYoungEventAssitance", MessageType.Success, new MessageCollection() { { "Young", PCYoung.Model.Young.Name }, { "Event", Model.Event.Name } });
      }
      else
        Model.Messages = PCYoung.Model.Messages;
      
      return !ContainValidationMessages;
    }    

    #endregion
  }
}