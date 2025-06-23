using System;

namespace eUseControl.BusinessLogic.Interfaces
{
     public interface ISession
     {
          string StartSession(string username);
          void EndSession();
          bool IsActiveSession();
     }
}
