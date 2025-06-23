using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using System;

namespace eUseControl.BusinessLogic
{
     public class SessionBL : UserAPI, ISession
     {
          private bool _isActive;
          private string _username;

          public string StartSession(string username)
          {
               _username = username;
               _isActive = true;
               return $"Sesiune începută pentru {_username}";
          }

          public void EndSession()
          {
               _isActive = false;
               _username = null;
          }

          public bool IsActiveSession()
          {
               return _isActive;
          }
     }
}
