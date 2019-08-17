using System;
using VPC.Entities.EntityCore;


namespace VPC.Entities.WorkFlow.Engine
{
    public static partial class WorkFlowEngine
    {
        public const string _draft = "47424808-C04B-48EF-BC36-F4181152A697";
        public const string _fail = "D9A9E5AB-5219-47FE-8222-7BF610F3441B";
        public const string _sent = "15CFFEFD-42C2-4048-BAB9-0F1A145A8C35";
        public const string _cancel="C707FCC8-FD8D-4E7D-9ADB-45A4EAB30895";
        public const string _email = InfoType.Email;
        public const string _emailName = "Email";


        [WorkFlowModel(Name = _emailName, Context = _email, Key = "Draft", Status="ReadyToSend", Description = "EmailReadyToSendDesc",TransitionResourceValue="Draft",StatusResourceValue ="Ready to send")]
        public static Guid Draft
        {
            get
            {
                return new Guid(_draft);
            }
        }

        [WorkFlowModel(Name = _emailName, Context = _email, Key = "Send", Status="Sent" ,Description = "EmailSentDesc",TransitionResourceValue="Send",StatusResourceValue ="Sent")]
        public static Guid Sent
        {
            get
            {
                return new Guid(_sent);
            }
        }  
        
        [WorkFlowModel(Name = _emailName, Context = _email, Key = "Failure", Status="Failed" , Description = "EmailFailureDesc",TransitionResourceValue="Failure",StatusResourceValue ="Failed")]
        public static Guid Fail
        {
            get
            {
                return new Guid(_fail);
            }
        }
        
         
        [WorkFlowModel(Name = _emailName, Context = _email, Key = "Cancel", Status="Cancelled" , Description = "EmailCancelDesc",TransitionResourceValue="Cancel",StatusResourceValue ="Cancelled")]
        public static Guid Cancel
        {
            get
            {
                return new Guid(_cancel);
            }
        }
             

    }

}
