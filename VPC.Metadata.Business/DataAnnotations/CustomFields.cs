using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace VPC.Metadata.Business.DataAnnotations
{
    
    public enum ResourceFields
    {
        [Description("Create")]
        Resource_Operation_Create,
        [Description("Key")]
        Resource_Field_Key,
        [Description("Text")]
        Resource_Field_Text,
        [Description("Language")]
        Resource_Field_Language,
        [Description("Delete")]
        Resource_Operation_Delete,
        [Description("Translate View")]
        Resource_Field_TranslateView,
        [Description("Cancel")]
        Resource_Operation_Cancel,
        [Description("Edit Resource")]
        EditResource,
        [Description("Update")]
        Update,
        [Description("Save")]
        Save,
        [Description("Key is required")]
        Resource_Field_Key_Required_Message,
        [Description("Text is required")]
        Resource_Field_Text_Required_Message,
        [Description("Language is requied")]
        Resource_Field_Language_Required_Message,
        [Description("Resource saved successfully")]
        Resource_Operation_Save_Success_Message,
        [Description("Resource deleted successfully")]
        Resource_Operation_Delete_Success_Message,

    }
}
