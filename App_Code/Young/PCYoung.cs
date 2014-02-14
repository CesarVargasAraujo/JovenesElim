using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elim.Core;
using Ding.Core;
using Ding.Enums;

namespace Elim.Young 
{
  public class PCYoung : ControllerP<CMYoung>
  {
    private DBYoung DBYoung;
    public PCYoung() : base() { DBYoung = new DBYoung(DB); }
    public PCYoung(Guid? Model) : this() { SetModel(Model); }


    #region Save

		public bool SaveYoung(Guid? YoungId, Guid? ChurchId, string Name, string Surnames, string Email, string Facebook, DateTime? BirthDay)
    {
			return Save(YoungId, ChurchId, Name, Surnames, Email, Facebook, BirthDay);
    }

		private bool Save(Guid? YoungId, Guid? ChurchId, string Name, string Surnames, string Email, string Facebook, DateTime? BirthDay)
    {
      #region Receive Parameters
			bool IsNew = YoungId.IsNull();
			Model.Young.YoungId = IsNew ? Guid.NewGuid() : YoungId;
      Model.Young.Facebook = Facebook;
      Model.Young.ChurchId = ChurchId;
      #endregion

      #region Validations
      #region General
      if (Model.Young.ChurchId.IsNull())
        AddMessage("VChurch", MessageType.Validation);
      Model.Young.Name = ValidateString("Name", Name);
      Model.Young.Surnames = Surnames;
      Model.Young.Email = Email.IsEmpty() ? null : ValidateStringWithRegex("Email", Email, CustomGeneral.RegexEmail);
      Model.Young.Birthday = BirthDay.IsNull() ? null : BirthDay.NullDT();
     /* if (Model.Young.Birthday.IsNull())
        AddMessage("VBirthDay", MessageType.Validation);*/
      #endregion
      #endregion

      #region Save
      if (!ContainMessages)
      {
				if (IsNew)
					DBYoung.Insert(Model.Young);
				else
					DBYoung.Update(Model.Young);

				AddMessage(IsNew ? "IYoung" : "UYoung", MessageType.Success, new MessageCollection() { { "Young", Model.Young.Name } });
      }
      #endregion      
      return !ContainValidationMessages;
    }
    #endregion
  }
}