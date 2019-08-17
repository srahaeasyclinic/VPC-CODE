using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Storage {
    public class Setting {
        public string ColumnWidth { get; set; }
        public bool ShowHeader { get; set; }

        public LinkSetting LinkSetting{get;set;}
        public AddSetting AddSetting{get;set;}
        public CalendarSetting CalendarSetting{get;set;}
        public CheckboxSetting CheckboxSetting{get;set;}
        public CustomSetting CustomSetting{get;set;}
        public DropDownSetting DropDownSetting{get;set;}
        public FileSetting FileSetting{get;set;}
        public MultiSelectDropDownSetting MultiSelectDropDownSetting{get;set;}
        public RadioSetting RadioSetting{get;set;}
        public RickTextBoxSetting RickTextBoxSetting{get;set;}

        public TabSetting TabSetting{get;set;}
         public TextBoxSetting TextBoxSetting{get;set;}

    }

    public class LinkSetting {
        public int Type{get; set;}
        public string Url{get; set;}
    }
    public class AddSetting {

    }
    public class CalendarSetting {

    }
    public class CheckboxSetting {

    }
    public class CustomSetting {

    }

    public class DropDownSetting {

    }

    public class FileSetting {

    }

    public class MultiSelectDropDownSetting {

    }
    public class RadioSetting {

    }
    public class RickTextBoxSetting {

    }
    public class TabSetting {

    }
    public class TextBoxSetting {

    }

}