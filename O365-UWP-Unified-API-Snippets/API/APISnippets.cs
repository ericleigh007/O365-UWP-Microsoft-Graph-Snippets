﻿// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace O365_UWP_Unified_API_Snippets
{
    class APISnippets
    {
        const string serviceEndpoint = "https://graph.microsoft.com/v1.0/";
        const string BetaserviceEndpoint = "https://graph.microsoft.com/beta/";

        static string tenant = App.Current.Resources["ida:Domain"].ToString();

        // Returns the Metadata for the released API
        public static async Task<string> GetMetaDataAsync()
        {
            string xDocString = "";

            try
            {
                HttpClient client = new HttpClient();
                var token = await AuthenticationHelper.GetTokenHelperAsync();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                // Endpoint for the specified group
                Uri Endpoint = new Uri(serviceEndpoint + "$metadata");

                HttpResponseMessage response = await client.GetAsync(Endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var xDoc = new XmlDocument();
                    xDoc.LoadXml(responseContent);
                    xDocString = xDoc.OuterXml;
                }
                else
                {
                    Debug.WriteLine("We could not get the metadata. The request returned this status code: " + response.StatusCode);
                    return null;
                }

            }


            catch (Exception e)
            {
                Debug.WriteLine("We could not get the metadata: " + e.Message);
                return null;

            }

            return xDocString;
        }

        // Returns the Metadata for the released API
        public static async Task<string> GetBetaDataAsync()
        {
            string xDocString = "";

            try
            {
                HttpClient client = new HttpClient();
                var token = await AuthenticationHelper.GetTokenHelperAsync();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                // Endpoint for the specified group
                Uri Endpoint = new Uri(BetaserviceEndpoint + "$metadata");

                HttpResponseMessage response = await client.GetAsync(Endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var xDoc = new XmlDocument();
                    xDoc.LoadXml(responseContent);
                    xDocString = xDoc.OuterXml;
                }
                else
                {
                    Debug.WriteLine("We could not get the Beta metadata. The request returned this status code: " + response.StatusCode);
                    return null;
                }

            }


            catch (Exception e)
            {
                Debug.WriteLine("We could not get the Beta metadata: " + e.Message);
                return null;

            }

            return xDocString;
        }
    }
}


//********************************************************* 
// 
//O365-UWP-Microsoft-Graph-Snippets, https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Snippets
//
//Copyright (c) Microsoft Corporation
//All rights reserved. 
//
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// ""Software""), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:

// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
//********************************************************* 