using GpscWebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GpscWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthenticationController : ApiController
    {
        private string CurrentDomainPath => "LDAP://DC=pttgrp,DC=corp";

        [HttpPost]
        public ResultModel<AuthenticateModel> Login([FromBody] JObject Body)
        {
            string Username = Body["Username"].ToString();
            string Password = Body["Password"].ToString();

            //try
            //{
            //    DirectoryEntry de = new DirectoryEntry(CurrentDomainPath, Username, Password);
            //    DirectorySearcher dsearch = new DirectorySearcher(de);
            //    SearchResult res = null;
            //    dsearch.PageSize = 100000;
            //    res = dsearch.FindOne();

            //    return new ResultModel<AuthenticateModel>()
            //    {
            //        ResultCode = HttpStatusCode.OK.GetHashCode(),
            //        Message = "",
            //        Result = new AuthenticateModel()
            //        {
            //            UserCode = "UserCode123456"
            //        }
            //    };
            //}
            //catch (Exception ex)
            //{
            //    return new ResultModel<AuthenticateModel>()
            //    {
            //        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //        Message = "",
            //        Result = new AuthenticateModel()
            //        {
            //            UserCode = ""
            //        }
            //    };
            //}


            if (Username.Equals("admin") && Password.Equals("123456"))
            {
                return new ResultModel<AuthenticateModel>()
                {
                    ResultCode = HttpStatusCode.OK.GetHashCode(),
                    Message = "",
                    Result = new AuthenticateModel()
                    {
                        UserCode = "UserCode123456"
                    }
                };
            }
            return new ResultModel<AuthenticateModel>()
            {
                ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
                Message = "",
                Result = new AuthenticateModel()
                {
                    UserCode = ""
                }
            };

            //try
            //{
            //    string LdapConnectionString = $"ldap.forumsys.com";
            //    LdapDirectoryIdentifier Ldap = new LdapDirectoryIdentifier("gpsc-web-p01.pttgrp.corp", 389);
            //    LdapConnection Connection = new LdapConnection(Ldap)
            //    {
            //        AuthType = AuthType.Basic
            //    };
            //    Connection.SessionOptions.ProtocolVersion = 3;
            //    string LdapUsername = $"cn={Username},DC=pttgrp,DC=corp";
            //    NetworkCredential Credential = new NetworkCredential(LdapUsername, Password);
            //    Connection.Bind(Credential);

            //    return new ResultModel<AuthenticateModel>()
            //    {
            //        ResultCode = HttpStatusCode.OK.GetHashCode(),
            //        Message = "",
            //        Result = new AuthenticateModel()
            //        {
            //            UserCode = "UserCode123456"
            //        }
            //    };
            //}
            //catch (Exception ex)
            //{
            //    return new ResultModel<AuthenticateModel>()
            //    {
            //        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //        Message = ex.Message
            //    };
            //}

            //NetworkCredential credential = new NetworkCredential("", "");


            //try
            //{
            //    //Object obj = Entry.NativeObject;
            //    DirectorySearcher Searcher = new DirectorySearcher(Entry)
            //    {
            //        Filter = $"(SAMAccountName={Username})",

            //    };
            //    Searcher.PropertiesToLoad.Add("cn");
            //    SearchResult ResultOfSearch = Searcher.FindOne();
            //    if (ResultOfSearch != null)
            //    {
            //        string ResultPath = ResultOfSearch.Path;
            //        string _filterAttribute = (String)ResultOfSearch.Properties["cn"][0];
            //        Result.ResultCode = HttpStatusCode.OK.GetHashCode();
            //        Result.UserCode = ResultPath;
            //    }

            //}
            //catch (Exception ex)
            //{

            //}

            //if (Username.Equals("admin") && Password.Equals("123456"))
            //{
            //    Result.ResultCode = HttpStatusCode.OK.GetHashCode();
            //    Result.UserCode = "UserCode123456";
            //}

        }
    }
}
