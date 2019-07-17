using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;

namespace VPC.Entities.SchedulerConfiguration
{
    public enum SyncStartTime
    {
       [Description("01:00")]
       One = 0100,
       [Description("01:30")]
       OneThirty = 0130,
       [Description("02:00")]
       Two = 0200,
       [Description("02:30")]
       TwoThirty = 0230,
       [Description("03:00")]
       Three = 0300,
       [Description("03:30")]
       ThreeThirty = 0330,
       [Description("04:00")]
       Four = 0400,
       [Description("04:30")]
       FourThirty = 0430,
       [Description("05:00")]
       Five = 0500,
       [Description("05:30")]
       FiveThirty = 0530,
       [Description("06:00")]
       Six = 0600,
       [Description("06:30")]
       SixThirty = 0630,
       [Description("07:00")]
       Seven = 0700,
       [Description("07:30")]
       SevenThirty = 0730,
       [Description("08:00")]
       Eigth = 0800,
       [Description("08:30")]
       EigthThirty = 0830,
       [Description("09:00")]
       Nine = 0900,
       [Description("09:30")]
       NineThirty = 0930,
       [Description("10:00")]
       Ten = 1000,
       [Description("10:30")]
       TenThirty = 1030,
       [Description("11:00")]
       Eleven = 1100,
       [Description("11:30")]
       ElevenThirty = 1130,
       [Description("12:00")]
       Twelve = 1200,
       [Description("12:30")]
       TwelveThirty = 1230,
        [Description("13:00")]
        Thirteen = 1300,
        [Description("13:30")]
        ThirteenThirty = 1330,
        [Description("14:00")]
        Forteen = 1400,
        [Description("14:30")]
        ForteenThirty = 1430,
        [Description("15:00")]
        Fifteen = 1500,
        [Description("15:30")]
        FifteenThirty = 1530,
        [Description("16:00")]
        Sixteen = 1600,
        [Description("16:30")]
        SixteenThirty = 1630,
        [Description("17:00")]
        Seventeen = 1700,
        [Description("17:30")]
        SeventeenThirty = 1730,
        [Description("18:00")]
        Eighteen = 1800,
        [Description("18:30")]
        EighteenThirty = 1830,
        [Description("19:00")]
        Ninteen = 1900,
        [Description("19:30")]
        NinteenThirty = 1930,
        [Description("20:00")]
        Twenty = 2000,
        [Description("20:30")]
        TwentyThirty = 2030,
        [Description("21:00")]
        TwentyOne = 2100,
        [Description("21:30")]
        TwentyOneThirty = 2130,
        [Description("22:00")]
        TwentyTwo = 2200,
        [Description("22:30")]
        TwentyTwoThirty = 2230,
        [Description("23:00")]
        TwentyThree = 2300,
        [Description("23:30")]
        TwentyThreeThirty = 2330,
        [Description("00:00")]
        TwentyFour = 0000,
        [Description("00:30")]
        TwentyFourThirty = 0030
    }
}
