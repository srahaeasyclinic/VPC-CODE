using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VPC.Framework.Business.Initilize.StaticContents
{
    public static class EmailTemplatesContent
    {

        public static string NewUserCredentialBody = "<html lang=\"en\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head><body><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\">" +
                                                    "<tbody><tr><td style=\"background-color:#044767; height:90px\">" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                    "<td style=\"vertical-align:middle;width:30px\">&nbsp;</td><td>&nbsp;</td></tr>" +
                                                    "<tr><td style=\"vertical-align:middle; width:30px\">&nbsp;</td>" +
                                                    "<td><img alt=\"\" src=\"http://test.myproduction.no/assets/img/small_logo.png\" style=\"height:40px; width:169px\"/></td>" +
                                                    "</tr><tr><td style=\"vertical-align:middle; width:30px\"> &nbsp;</td><td>&nbsp;</td>" +
                                                    "</tr></tbody></table></td></tr><tr><td style=\"vertical-align:middle\">" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                    "<td><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody>" +
                                                    "<tr><td style=\"width:30px\"> &nbsp;</td><td>&nbsp;</td>" +
                                                    "</tr><tr><td style=\"width:30px\"> &nbsp;</td><td><span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\">" +
                                                    "<span style=\"font-size:18px\"> Hi&nbsp;[FirstName] &nbsp;[MiddleName]&nbsp;[LastName],</span></span></strong></span></td>" +
                                                    "</tr></tbody></table><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody>" +
                                                    "<tr><td style=\"width:30px\"> &nbsp;</td><td style=\"vertical-align:top\"><span style=\"font-size:16px\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"> Welcome to View production and thanks for signing up.</span></span></td>" +
                                                    "</tr><tr><td>&nbsp;</td></tr></tbody></table><hr/><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\">" +
                                                    "<tbody><tr><td style=\"vertical-align:top\"><p style=\"text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:20px\"><span style=\"color:#999999\"> Your credential</span></span></span></p>" +
                                                     "</td></tr><tr><td style=\"text-align:center; vertical-align:top\"> &nbsp;</td></tr></tbody></table></td></tr></tbody>" +
                                                      "</table></td></tr><tr>" +
                                                      "<td style=\"text-align:center; vertical-align:top\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"color:#999999\"> Workspace </span></span><br/>" +
                                                      "<span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\"><span style=\"font-size:18px\">[TenantCode]</span></span></strong></span></td></tr>" +
                                                      "<tr><td style=\"text-align:center; vertical-align:top\"> &nbsp;</td></tr><tr>" +
                                                      "<td style=\"text-align:center; vertical-align:top\"><span style=\"color:#999999\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:16px\">Username</span></span></span><br/>" +
                                                      "<span style=\"color:#000000\"><strong><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:18px\">[UserCredential.Username] </span></span></strong></span></td>" +
                                                      "</tr><tr><td style=\"text-align:center; vertical-align:top\">&nbsp;</td></tr><tr>" +
                                                      "<td style=\"height:30px;text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"color:#999999\"><span style=\"font-size:18px\">Password</span></span></span><br/>" +
                                                      "<span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\"><span style=\"font-size:20px\">[UserCredential.Password] </span></span></strong></span></td>" +
                                                      "</tr><tr>" +
                                                      "<td style=\"height:100px; text-align:center\">" +
                                                      "<span style=\"font-family:Tahoma,Geneva,sans-serif\">" +
                                                      "<a href=\"http://test.myproduction.no/?tenant=[TenantCode]\" style=\"text-decoration:none; font-size:18px; color:#FFFFFF; border-style:solid;border-color:#ED8E20;border-width:15px 30px;display:inline-block;background:#ED8E20 none repeat scroll 0% 0%; border-radius:5px; font-weight:normal; font-style:normal; line-height:22px; width:auto; text-align:center; " +
                                                      "\" target=\"_blank\">Click here to login</a></span>" +
                                                      "</td>" +
                                                      "</tr><tr><td style=\"height:10px; text-align:center\"><hr/></td></tr>" +
                                                      "<tr><td><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                      "<td style=\"width:200px\"> &nbsp;</td><td style=\"width:200px\"><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:200px\">" +
                                                      "<tbody><tr><td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/facebook.png\"/></a></td>" +
                                                      "<td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/twitter.png\"/></a></td>" +
                                                      "<td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/instagram.png\"/></a></td>" +
                                                      "</tr></tbody></table></td><td style=\"height:50px;width:200px\">&nbsp;</td></tr></tbody><tbody></tbody></table>" +
                                                    "<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\"><tbody>" +
                                                     "<tr><td style=\"text-align:center;vertical-align:top\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:14px\"><span style=\"color:#999999\">If you didn&#39;t create an account using this email, please ignore this email.</span></span></span></td></tr>" +
                                                    "<tr><td style=\"height:40px\"> &nbsp;</td></tr></tbody></table></td></tr></tbody></table></body></html>";

        public static string NewUserCredentialTitle = "New user credential";


        public static string ResetPasswordBody = "<html lang=\"en\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head><body>" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\">" +
                                                    "<tbody><tr><td style=\"background-color:#044767; height:90px\">" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                    "<td style=\"vertical-align:middle;width:30px\">&nbsp;</td><td>&nbsp;</td></tr>" +
                                                    "<tr><td style=\"vertical-align:middle; width:30px\">&nbsp;</td>" +
                                                    "<td><img alt=\"\" src=\"http://test.myproduction.no/assets/img/small_logo.png\" style=\"height:40px; width:169px\"/></td>" +
                                                    "</tr><tr><td style=\"vertical-align:middle; width:30px\"> &nbsp;</td><td>&nbsp;</td>" +
                                                    "</tr></tbody></table></td></tr><tr><td style=\"vertical-align:middle\">" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                    "<td><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody>" +
                                                    "<tr><td style=\"width:30px\"> &nbsp;</td><td>&nbsp;</td>" +
                                                    "</tr><tr><td style=\"width:30px\"> &nbsp;</td><td><span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\">" +
                                                    "<span style=\"font-size:18px\"> Hi&nbsp;[FirstName] &nbsp;[MiddleName]&nbsp;[LastName],</span></span></strong></span></td>" +
                                                    "</tr></tbody></table><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody>" +
                                                    "<tr><td style=\"width:30px\"> &nbsp;</td><td style=\"vertical-align:top\"><span style=\"font-size:16px\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"> We received a request to reset your password.</span></span></td>" +
                                                    "</tr><tr><td>&nbsp;</td></tr></tbody></table><hr/><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\">" +
                                                    "<tbody><tr><td style=\"vertical-align:top\"><p style=\"text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:20px\"><span style=\"color:#999999\"> Your credential</span></span></span></p>" +
                                                     "</td></tr><tr><td style=\"text-align:center; vertical-align:top\"> &nbsp;</td></tr></tbody></table></td></tr></tbody>" +
                                                      "</table></td></tr><tr>" +
                                                      "<td style=\"text-align:center; vertical-align:top\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"color:#999999\"> Workspace </span></span><br/>" +
                                                      "<span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\"><span style=\"font-size:18px\">[TenantCode]</span></span></strong></span></td></tr>" +
                                                      "<tr><td style=\"text-align:center; vertical-align:top\"> &nbsp;</td></tr><tr>" +
                                                      "<td style=\"text-align:center; vertical-align:top\"><span style=\"color:#999999\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:16px\">Username</span></span></span><br/>" +
                                                      "<span style=\"color:#000000\"><strong><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:18px\">[UserCredential.Username] </span></span></strong></span></td>" +
                                                      "</tr><tr><td style=\"text-align:center; vertical-align:top\">&nbsp;</td></tr><tr>" +
                                                      "<td style=\"height:30px;text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"color:#999999\"><span style=\"font-size:18px\">Password</span></span></span><br/>" +
                                                      "<span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\"><span style=\"font-size:20px\">[UserCredential.Password] </span></span></strong></span>" +
                                                        "</td>" +
                                                      "</tr><tr><td style=\"height:100px; text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><a href=\"http://test.myproduction.no/?tenant=[TenantCode]\" style=\"text-decoration:none;font-size:18px;color:#FFFFFF;border-style:solid;border-color:#ED8E20;border-width:15px 30px;display:inline-block;background:#ED8E20 none repeat scroll 0% 0%;border-radius:5px;font-weight:normal;font-style:normal;line-height:22px;width:auto;text-align:center;\" target=\"_blank\">Click here to login</a></span></td>" +
                                                      "</tr><tr><td style=\"height:10px; text-align:center\"><hr/></td></tr>" +
                                                      "<tr><td><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                      "<td style=\"width:200px\"> &nbsp;</td><td style=\"width:200px\"><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:200px\">" +
                                                      "<tbody><tr><td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/facebook.png\"/></a></td>" +
                                                      "<td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/twitter.png\"/></a></td>" +
                                                      "<td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/instagram.png\"/></a></td>" +
                                                      "</tr></tbody></table></td><td style=\"height:50px;width:200px\">&nbsp;</td></tr></tbody><tbody></tbody></table>" +
                                                    "<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\"><tbody>" +
                                                     "<tr><td style=\"text-align:center;vertical-align:top\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:14px\"><span style=\"color:#999999\">If you didn&#39;t reset password, please ignore this email.</span></span></span></td></tr>" +
                                                    "<tr><td style=\"height:40px\"> &nbsp;</td></tr></tbody></table></td></tr></tbody></table></body></html>";


        public static string ResetPasswordTitle = "Reset password";


        public static string ChangePasswordBody = "<html lang=\"en\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head><body>" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\">" +
                                                    "<tbody><tr><td style=\"background-color:#044767; height:90px\">" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                    "<td style=\"vertical-align:middle;width:30px\">&nbsp;</td><td>&nbsp;</td></tr>" +
                                                    "<tr><td style=\"vertical-align:middle; width:30px\">&nbsp;</td>" +
                                                    "<td><img alt=\"\" src=\"http://test.myproduction.no/assets/img/small_logo.png\" style=\"height:40px; width:169px\"/></td>" +
                                                    "</tr><tr><td style=\"vertical-align:middle; width:30px\"> &nbsp;</td><td>&nbsp;</td>" +
                                                    "</tr></tbody></table></td></tr><tr><td style=\"vertical-align:middle\">" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                    "<td><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody>" +
                                                    "<tr><td style=\"width:30px\"> &nbsp;</td><td>&nbsp;</td>" +
                                                    "</tr><tr><td style=\"width:30px\"> &nbsp;</td><td><span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\">" +
                                                    "<span style=\"font-size:18px\"> Hi&nbsp;[FirstName] &nbsp;[MiddleName]&nbsp;[LastName],</span></span></strong></span></td>" +
                                                    "</tr></tbody></table><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody>" +
                                                    "<tr><td style=\"width:30px\"> &nbsp;</td><td style=\"vertical-align:top\"><span style=\"font-size:16px\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"> We received a request to changed your password.</span></span></td>" +
                                                    "</tr><tr><td>&nbsp;</td></tr></tbody></table><hr/><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\">" +
                                                    "<tbody><tr><td style=\"vertical-align:top\"><p style=\"text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:20px\"><span style=\"color:#999999\"> Your credential</span></span></span></p>" +
                                                     "</td></tr><tr><td style=\"text-align:center; vertical-align:top\"> &nbsp;</td></tr></tbody></table></td></tr></tbody>" +
                                                      "</table></td></tr><tr>" +
                                                      "<td style=\"text-align:center; vertical-align:top\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"color:#999999\"> Workspace </span></span><br/>" +
                                                      "<span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\"><span style=\"font-size:18px\">[TenantCode]</span></span></strong></span></td></tr>" +
                                                      "<tr><td style=\"text-align:center; vertical-align:top\"> &nbsp;</td></tr><tr>" +
                                                      "<td style=\"text-align:center; vertical-align:top\"><span style=\"color:#999999\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:16px\">Username</span></span></span><br/>" +
                                                      "<span style=\"color:#000000\"><strong><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:18px\">[UserCredential.Username] </span></span></strong></span></td>" +
                                                      "</tr><tr><td style=\"text-align:center; vertical-align:top\">&nbsp;</td></tr><tr>" +
                                                      "<td style=\"height:30px;text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"color:#999999\"><span style=\"font-size:18px\">Password</span></span></span><br/>" +
                                                      "<span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\"><span style=\"font-size:20px\">[UserCredential.Password] </span></span></strong></span></td>" +
                                                      "</tr><tr><td style=\"height:100px; text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><a href=\"http://test.myproduction.no/?tenant=[TenantCode]\" style=\"text-decoration:none;font-size:18px;color:#FFFFFF;border-style:solid;border-color:#ED8E20;border-width:15px 30px;display:inline-block;background:#ED8E20 none repeat scroll 0% 0%;border-radius:5px;font-weight:normal;font-style:normal;line-height:22px;width:auto;text-align:center;\" target=\"_blank\">Click here to login</a></span></td>" +
                                                      "</tr><tr><td style=\"height:10px; text-align:center\"><hr/></td></tr>" +
                                                      "<tr><td><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                      "<td style=\"width:200px\"> &nbsp;</td><td style=\"width:200px\"><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:200px\">" +
                                                      "<tbody><tr><td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/facebook.png\"/></a></td>" +
                                                      "<td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/twitter.png\"/></a></td>" +
                                                      "<td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/instagram.png\"/></a></td>" +
                                                      "</tr></tbody></table></td><td style=\"height:50px;width:200px\">&nbsp;</td></tr></tbody><tbody></tbody></table>" +
                                                    "<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\"><tbody>" +
                                                     "<tr><td style=\"text-align:center;vertical-align:top\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:14px\"><span style=\"color:#999999\">If you didn&#39;t request, please ignore this email.</span></span></span></td></tr>" +
                                                    "<tr><td style=\"height:40px\"> &nbsp;</td></tr></tbody></table></td></tr></tbody></table></body></html>";


        public static string ChangePasswordTitle = "Change password";



         public static string UserExportBody = "<html lang=\"en\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head><body>" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\">" +
                                                    "<tbody><tr><td style=\"background-color:#044767; height:90px\">" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                    "<td style=\"vertical-align:middle;width:30px\">&nbsp;</td><td>&nbsp;</td></tr>" +
                                                    "<tr><td style=\"vertical-align:middle; width:30px\">&nbsp;</td>" +
                                                    "<td><img alt=\"\" src=\"http://test.myproduction.no/assets/img/small_logo.png\" style=\"height:40px; width:169px\"/></td>" +
                                                    "</tr><tr><td style=\"vertical-align:middle; width:30px\"> &nbsp;</td><td>&nbsp;</td>" +
                                                    "</tr></tbody></table></td></tr><tr><td style=\"vertical-align:middle\">" +
                                                    "<table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                    "<td><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody>" +
                                                    "<tr><td style=\"width:30px\"> &nbsp;</td><td>&nbsp;</td>" +
                                                    "</tr><tr><td style=\"width:30px\"> &nbsp;</td><td><span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\">" +
                                                    "<span style=\"font-size:18px\"> Hi&nbsp;[FirstName], Please find the attachment</span></span></strong></span></td>" +
                                                    "</tr></tbody></table><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody>" +
                                                    "<tr><td style=\"width:30px\"> &nbsp;</td><td style=\"vertical-align:top\"><span style=\"font-size:16px\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"> We received a request to changed your password.</span></span></td>" +
                                                    "</tr><tr><td>&nbsp;</td></tr></tbody></table><hr/><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\">" +
                                                    "<tbody><tr><td style=\"vertical-align:top\"><p style=\"text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:20px\"><span style=\"color:#999999\"> Your credential</span></span></span></p>" +
                                                     "</td></tr><tr><td style=\"text-align:center; vertical-align:top\"> &nbsp;</td></tr></tbody></table></td></tr></tbody>" +
                                                      "</table></td></tr><tr>" +
                                                      "<td style=\"text-align:center; vertical-align:top\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"color:#999999\"> Workspace </span></span><br/>" +
                                                      "<span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\"><span style=\"font-size:18px\">[TenantCode]</span></span></strong></span></td></tr>" +
                                                      "<tr><td style=\"text-align:center; vertical-align:top\"> &nbsp;</td></tr><tr>" +
                                                      "<td style=\"text-align:center; vertical-align:top\"><span style=\"color:#999999\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:16px\">Username</span></span></span><br/>" +
                                                      "<span style=\"color:#000000\"><strong><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:18px\">[UserCredential.Username] </span></span></strong></span></td>" +
                                                      "</tr><tr><td style=\"text-align:center; vertical-align:top\">&nbsp;</td></tr><tr>" +
                                                      "<td style=\"height:30px;text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"color:#999999\"><span style=\"font-size:18px\">Password</span></span></span><br/>" +
                                                      "<span style=\"font-family:Tahoma,Geneva,sans-serif\"><strong><span style=\"color:#000000\"><span style=\"font-size:20px\">[UserCredential.Password] </span></span></strong></span></td>" +
                                                      "</tr><tr><td style=\"height:100px; text-align:center\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><a href=\"http://test.myproduction.no/?tenant=[TenantCode]\" style=\"text-decoration:none;font-size:18px;color:#FFFFFF;border-style:solid;border-color:#ED8E20;border-width:15px 30px;display:inline-block;background:#ED8E20 none repeat scroll 0% 0%;border-radius:5px;font-weight:normal;font-style:normal;line-height:22px;width:auto;text-align:center;\" target=\"_blank\">Click here to login</a></span></td>" +
                                                      "</tr><tr><td style=\"height:10px; text-align:center\"><hr/></td></tr>" +
                                                      "<tr><td><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:600px\"><tbody><tr>" +
                                                      "<td style=\"width:200px\"> &nbsp;</td><td style=\"width:200px\"><table cellpadding=\"1\" cellspacing=\"1\" style=\"width:200px\">" +
                                                      "<tbody><tr><td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/facebook.png\"/></a></td>" +
                                                      "<td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/twitter.png\"/></a></td>" +
                                                      "<td style=\"text-align:center\"><a href=\"https://www.instagram.com/accounts/login/?hl=en\" target=\"_blank\"><img src=\"http://test.myproduction.no/assets/img/instagram.png\"/></a></td>" +
                                                      "</tr></tbody></table></td><td style=\"height:50px;width:200px\">&nbsp;</td></tr></tbody><tbody></tbody></table>" +
                                                    "<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\"><tbody>" +
                                                     "<tr><td style=\"text-align:center;vertical-align:top\"><span style=\"font-family:Tahoma,Geneva,sans-serif\"><span style=\"font-size:14px\"><span style=\"color:#999999\">If you didn&#39;t request, please ignore this email.</span></span></span></td></tr>" +
                                                    "<tr><td style=\"height:40px\"> &nbsp;</td></tr></tbody></table></td></tr></tbody></table></body></html>";


        public static string UserExportTitle = "User export";
    }
}
