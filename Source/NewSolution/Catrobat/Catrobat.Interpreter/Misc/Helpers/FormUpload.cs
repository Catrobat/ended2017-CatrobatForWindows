using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Catrobat.Interpreter.Misc.Helpers
{
    // Implements multipart/form-data POST in C# http://www.ietf.org/rfc/rfc2388.txt
    // http://www.briangrinstead.com/blog/multipart-form-post-in-c
    public static class FormUpload
    {
        public delegate void FinishedCallback(string result);

        private static readonly Encoding _encoding = Encoding.UTF8;

        public static HttpWebRequest MultipartFormDataPost(string postUrl, string userAgent, Dictionary<string, object> postParameters, FinishedCallback callback)
        {
            var formDataBoundary = string.Format("----------{0:N}", Guid.NewGuid());
            var contentType = "multipart/form-data; boundary=" + formDataBoundary;

            var formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, userAgent, contentType, formData, callback);
        }

        private static HttpWebRequest PostForm(string postUrl, string userAgent, string contentType, byte[] formData, FinishedCallback callback)
        {
            var request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            //request.UserAgent = userAgent; // TODO: check and comment in!
            request.CookieContainer = new CookieContainer();
            //request.ContentLength = formData.Length; // Not needed in WP7

            // You could add authentication here as well if needed:
            // request.PreAuthenticate = true;
            // request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            // request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("username" + ":" + "password")));

            // Send the form data to the request.
            request.BeginGetRequestStream(ar =>
                {
                    var requestStream = request.EndGetRequestStream(ar);
                    using (var sw = new StreamWriter(requestStream))
                    {
                        requestStream.Write(formData, 0, formData.Length);
                        //requestStream.Close(); // TODO: Works without this line?
                    }

                    request.BeginGetResponse(a =>
                        {
                            var response = request.EndGetResponse(a);
                            var responseStream = response.GetResponseStream();
                            using (var sr = new StreamReader(responseStream))
                            {
                                // Parse the response message here
                                callback(sr.ReadToEnd());
                            }
                        }, null);
                }, null);

            return request;
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new MemoryStream();
            var needsClrf = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsClrf)
                {
                    formDataStream.Write(_encoding.GetBytes("\r\n"), 0, _encoding.GetByteCount("\r\n"));
                }

                needsClrf = true;

                var value = param.Value as FileParameter;
                if (value != null)
                {
                    var fileToUpload = value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    var header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                                               boundary,
                                               param.Key,
                                               fileToUpload.FileName ?? param.Key,
                                               fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(_encoding.GetBytes(header), 0, _encoding.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    var postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                                                 boundary,
                                                 param.Key,
                                                 param.Value);
                    formDataStream.Write(_encoding.GetBytes(postData), 0, _encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            var footer = string.Format("\r\n--{0}--\r\n", boundary);
            formDataStream.Write(_encoding.GetBytes(footer), 0, _encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            var formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            //formDataStream.Close(); // TODO: works without this line?
            formDataStream.Dispose();

            return formData;
        }

        public class FileParameter
        {
            public byte[] File { get; private set; }
            public string FileName { get; private set; }
            public string ContentType { get; private set; }
            public FileParameter(byte[] file) : this(file, null) {}
            public FileParameter(byte[] file, string filename) : this(file, filename, null) {}

            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
    }
}