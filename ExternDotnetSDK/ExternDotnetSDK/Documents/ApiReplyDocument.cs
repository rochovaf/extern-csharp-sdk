﻿using System;
using System.Collections.Generic;
using ExternDotnetSDK.Common;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ExternDotnetSDK.Documents
{
    public class ApiReplyDocument
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public byte[] PrintContent { get; set; }
        public string Filename { get; set; }
        public byte[] Signature { get; set; }
        public List<Link> Links { get; set; }
        public Guid DocflowId { get; set; }
        public Guid DocumentId { get; set; }
    }
}