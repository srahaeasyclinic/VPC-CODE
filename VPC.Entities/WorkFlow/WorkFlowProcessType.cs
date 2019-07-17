
using System.ComponentModel;

namespace VPC.Entities.WorkFlow
{
    public enum WorkFlowProcessType
    {
        [Description("Pre process")]
        PreProcess = 1,

        [Description("Process")]
        Process = 2,

        [Description("Post process")]
        PostProcess = 3,
    }

}
