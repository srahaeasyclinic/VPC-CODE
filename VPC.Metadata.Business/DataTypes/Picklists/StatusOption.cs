using System;
using System.Collections.Generic;
using System.Text;

namespace VPC.Metadata.Business.DataTypes.Picklists
{
    public enum StatusOption
    {
        All = -1,
        Disabled = 0,
        Enabled = 1,
        Deleted = 2,
        IsWelknown = 3,
        IsBookMark = 4,
        IsDefaut = 5,
        Archive = 6,
        WaitingForVerification = 7,
        Locked = 8
    }
}
