using System;
using VPC.Entities.EntityCore;


namespace VPC.Entities.WorkFlow.Engine
{
    public static partial class WorkFlowEngine
    {
        public const string _registration = "276AFE8A-B4E8-4782-B28F-7CEA3AC29AAA";
        public const string _sendToDoctor = "8F5FC327-93B9-44B0-BCCA-365BB0F0EC0E";
         public const string _doctorAttending = "E0AFF037-E478-4D19-8BD6-C25B22EF6460";

         public const string _sendToPharmacy = "A6A7F7DE-3115-4C3C-8768-28B6B7910A78";
         public const string _pharmacyAttending = "9750256A-637D-49F1-9F5B-286D472F29AD";

         public const string _close = "B9D2ADFC-EDC6-4BCE-9DC6-570F506908A0";
        public const string _user = InfoType.User;
        public const string _userName = "Key_User";


        [WorkFlowModel(Name = _userName, Context = _user, Key = "Key_Registration", Status="Key_Registration" ,Description = "Registration",TransitionResourceValue="Registration",StatusResourceValue ="Registered")]
        public static Guid Registration
        {
            get
            {
                return new Guid(_registration);
            }
        }
        
        [WorkFlowModel(Name = _userName, Context = _user, Key = "Key_SendToDoctor", Status="Key_SendToDoctor", Description = "Send to doctor",TransitionResourceValue="Send to doctor",StatusResourceValue ="Sent to doctor")]
        public static Guid SendToDoctor
        {
            get
            {
                return new Guid(_sendToDoctor);
            }
        }

        [WorkFlowModel(Name = _userName, Context = _user, Key = "Key_DoctorAttending", Status="Key_DoctorAttending", Description = "Doctor attending",TransitionResourceValue="Doctor Attending",StatusResourceValue ="Doctor attended")]
        public static Guid DoctorAttending
        {
            get
            {
                return new Guid(_doctorAttending);
            }
        }

        [WorkFlowModel(Name = _userName, Context = _user, Key = "Key_SendToPharmacy", Status="Key_SendToPharmacy", Description = "Send to pharmacy",TransitionResourceValue="Send To Pharmacy",StatusResourceValue ="Sent to pharmacy")]
        public static Guid SendToPharmacy
        {
            get
            {
                return new Guid(_sendToPharmacy);
            }
        }

        [WorkFlowModel(Name = _userName, Context = _user, Key = "Key_PharmacyAttending", Status="Key_PharmacyAttending", Description = "Pharmacy attending",TransitionResourceValue="Pharmacy Attending",StatusResourceValue ="Pharmacy attended")]
        public static Guid PharmacyAttending
        {
            get
            {
                return new Guid(_pharmacyAttending);
            }
        }

        [WorkFlowModel(Name = _userName, Context = _user, Key = "Key_Close", Status="Key_Close", Description = "Close",TransitionResourceValue="Close",StatusResourceValue ="Closed")]
        public static Guid Close
        {
            get
            {
                return new Guid(_close);
            }
        }


    }

}
