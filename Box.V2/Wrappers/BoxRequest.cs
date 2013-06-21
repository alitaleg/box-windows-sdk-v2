﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Box.V2
{
    public class BoxRequest : IBoxRequest
    {
        public BoxRequest(Uri hostUri) : this(hostUri, string.Empty) { }

        public BoxRequest(Uri hostUri, string path)
        {
            Host = hostUri;
            Path = path;

            HttpHeaders = new Dictionary<string, string>();
            Parameters = new Dictionary<string, string>();
            PayloadParameters = new Dictionary<string, string>();
        }

        public Uri Host { get; private set; }

        public string Path { get; private set; }

        public virtual RequestMethod Method { get; set; }

        public Dictionary<string, string> HttpHeaders { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }

        public Dictionary<string, string> PayloadParameters { get; private set; }

        public Uri Uri { get { return new Uri(Host, Path); } }

        public string Payload { get; set; }

        public string Authorization { get; set; }

        /// <summary>
        /// Returns the full Uri including host, path, and querystring
        /// </summary>
        public Uri AbsoluteUri
        {
            get
            {
                return new Uri(Uri,
                    Parameters.Count == 0 ? string.Empty :
                    string.Format("?{0}", GetQueryString()));
            }
        }

        /// <summary>
        /// Returns the query string of the parameters dictionary
        /// </summary>
        /// <returns></returns>
        public string GetQueryString()
        {
            if (Parameters.Count == 0)
                return string.Empty;

            var paramStrings = Parameters
                                .Where(p => !string.IsNullOrEmpty(p.Value))
                                .Select(p => string.Format("{0}={1}", p.Key, p.Value));

            return string.Join("&", paramStrings);
        }
    }

    public enum RequestMethod
    {
        Get,
        Post,
        Put,
        Delete,
        Options
    }
}