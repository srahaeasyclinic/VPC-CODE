namespace VPC.Entities.EntityCore
{
    public class InfoPrefix
    {
        #region prefix used in User entity (should be unique)  //ENTITY DEFAULT PREFIX _US
        public const string UserActivityHisotory_User = "_US_UAH";
        public const string UserInRole_User = "_US_UIR";
        public const string ContactInformation_User = "_US_COI";
        public const string OfficialAddress_User = "_US_OFF";
        public const string InvoiceAddress_User = "_US_INV";
        public const string PostalAddress_User = "_US_POS";
        public const string Timezone_User = "_US_TIM";
        public const string Language_User = "_US_LAN";
        public const string Nationality_User = "_US_NAT";
        public const string Category_User = "_US_CAT";
        public const string Crew_User = "_US_CRE";
        public const string UserGroup_User = "_US_USG";
        public const string UserWorkFlow_User = "_US_WOU";
        public const string UserCredential_User = "_US_UCR";
        public const string UserEmployment_User = "_US_UEM";
        #endregion


        #region prefix used in Tenant entity (should be unique) //ENTITY DEFAULT PREFIX _TE
        public const string ContactInformation_Tenant = "_TE_CON";
        public const string PasswordPolicy_Tenant = "_TE_PAS";
        public const string TenantType_Tenant = "_TE_TTY";
        public const string OfficialAddress_Tenant = "_TE_OFF";
        public const string InvoiceAddress_Tenant = "_TE_INV";
        public const string PostalAddress_Tenant = "_TE_POS";
        public const string Language_Tenant = "_TE_LAN";
        public const string SuperAdminId_Tenant = "_TE_SUP";
        public const string Subscription_Tenant = "_TE_SUB";
        public const string TenantIPRange_Tenant = "_TE_TIP";
        public const string TenantServiceStatus_Tenant = "_TE_TSS";
        public const string Timezone_Tenant = "_TE_TIT";
        #endregion




        #region prefix used in City picklist (should be unique) //ENTITY DEFAULT PREFIX _CI
        public const string UpdatedBy_City = "_CI_UPD";
        public const string Active_City = "_CI_ACT";
        public const string Country_PVCity = "_PVC_COU";
        public const string State_PVCity = "_PVC_STA";
        public const string Municipality_PVCity = "_PVC_MUN";

        #endregion

        #region Used in Customer entity (should be unique) //_CUS

        public const string ContactInformation_customer = "_CUS_COI";
        public const string OfficialAddress_customer = "_CUS_OFF";
        public const string InvoiceAddress_customer = "_CUS_INV";
        public const string PostalAddress_customer = "_CUS_POS";
        public const string CustomerCredentialId_customer = "_CUS_CRD";

        #endregion


        #region Used in Location entity (should be unique) //_LOC

        public const string Company_Location = "_LOC_COM";
        public const string OfficialAddress_Location = "_LOC_OFF";
        public const string ContactInformation_Location = "_LOC_COI";
        public const string Timezone_Location = "_LOC_TMZ";

        #endregion

        #region Used in ProductGroup Picklist (should be unique) //_PRG

        public const string ProductGroup_Active = "_PRG_ACT";
        public const string ProductGroup_ParentPicklist = "_PRG_PRT";
        public const string ProductGroup_UpdatedBy = "_PRG_UPB";
        #endregion

        #region Used in ProductType Picklist (should be unique) //_PRT

        public const string ProductType_Active = "_PRT_ACT";
        public const string ProductType_ParentPicklist = "_PRT_PRT";
        public const string ProductType_UpdatedBy = "_PRT_UPB";
        #endregion

        #region prefix used in Country picklist (should be unique) //ENTITY DEFAULT PREFIX _CO
        public const string UpdatedBy_Country = "_CO_UPD";
        public const string Active_Country = "_CO_ACT";
        public const string Currency_Country = "_CO_COU";
        public const string Language_Country = "_CO_LAN";
        public const string Timezone_Country = "_CO_TIM";

        #endregion

        #region prefix used in state picklist (should be unique) //ENTITY DEFAULT PREFIX _ST
        public const string Active_State = "_ST_ACC";
        public const string CountryId_State = "_ST_COU";


        #endregion

        #region prefix used in product (should be unique) //Entity Default Prefix _PRD

        public const string Active_Product = "_PRD_ACC";
        public const string PurchaseUOM_Product = "_PRD_PUM";
        public const string SalesUOM_Product = "_PRD_SLUM";
        public const string TAXCode_Product = "_PRD_TAXC";
        public const string StockGroupCategory_Product = "_PRD_STGC";
        public const string DefaultLocation_Product = "_PRD_DFL";
        public const string UOM_Product = "_PRD_UOM";
        public const string ProcessingType_Product = "_PRD_PRCT";
        public const string ProductType_Product = "_PRD_PRDT";
        public const string ProductGroup_Product = "_PRD_PRGP";
        public const string ProductCategory_Product = "_PRD_PRDC";
        public const string ProcurementRule_Product = "_PRD_PRCR";
        public const string WeightUOM_Product = "_PRD_WGU";
        public const string VolumeUOM_Product = "_PRD_VLU";
        public const string ManufacturerCode_Product = "_PRD_MNF";
        public const string CountryOfOrigin_Product = "_PRD_CORG";

        #endregion

        #region prefix used in municipality picklist (should be unique) //ENTITY DEFAULT PREFIX _MU
        public const string Municipality_State = "_MU_Sta";
        public const string Municipality_CountryId = "_MU_Con";
        #endregion

        #region prefix used in municipality picklist (should be unique) //ENTITY DEFAULT PREFIX _MUNC
        public const string Active_Municipality = "_MUNC_ACC";
        public const string CountryId_Municipality = "_MUNC_COU";
        public const string StateId_Municipality = "_MUNC_STA";
        #endregion

        #region prefix used in room (should be unique) //ENTITY DEFAULT PREFIX _ROO
        public const string LocationId_Room = "_ROO_LOC";
        public const string RoomTypeId_Room = "_ROO_RTI";
        #endregion

        #region prefix used in company (should be unique) //ENTITY DEFAULT PREFIX _COM

        public const string ContactInformation_Company = "_COM_COI";
        public const string OfficialAddress_Company = "_COM_OFF";
        public const string InvoiceAddress_Company = "_COM_INV";
        public const string PostalAddress_Company = "_COM_POS";
        public const string Avater_Company = "_COM_AVT";
        public const string Timezone_Company = "_COM_TMZ";
        public const string PreferredLanguage_Company = "_COM_PRL";

        #endregion

        #region prefix used in contact (should be unique) //ENTITY DEFAULT PREFIX _CON

        public const string Title_Contact = "_CON_TTL";

        #endregion

        #region prefix used in user company (should be unique) //ENTITY DEFAULT PREFIX _USC

        public const string User_UserCompany = "_USC_USR";
        public const string Company_UserCompany = "_USC_COM";


        #endregion

        #region prefix used in user department (should be unique) //ENTITY DEFAULT PREFIX _USD

        public const string User_UserDepartment = "_USD_USR";
        public const string Department_UserDepartment = "_USD_DEP";


        #endregion
               
        #region prefix used in user location (should be unique) //ENTITY DEFAULT PREFIX _USL

        public const string User_UserLocation = "_USL_USR";
        public const string Location_UserLocation = "_USL_LOC";


        #endregion

        #region prefix used in User Group (should be unique) //ENTITY DEFAULT PREFIX _USGP
        public const string UpdatedBy_UserGroup = "_USGP_UPD";
        public const string Active_UserGroup = "_USGP_ACT";
        #endregion


    }
}