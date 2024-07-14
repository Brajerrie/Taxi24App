using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi24App.Enums
{
    public enum TripStatus
    {
        NotStarted,
        Waiting,
        InProgress,
        Completed
    }

    public enum DriverStatus
    {
        Available = 1,
        Busy,
        Unavailable,
    }
}