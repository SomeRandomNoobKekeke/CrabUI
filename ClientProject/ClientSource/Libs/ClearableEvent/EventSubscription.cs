using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class EventSubscription
  {
    public void Cancel()
    {
      CancelAction?.Invoke();
      CancelAction = null;
    }
    private Action CancelAction;
    public EventSubscription(Action cancelAction) => CancelAction = cancelAction;
  }
}