﻿using System;
using System.Collections.Generic;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Docflows
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocflowPageItem
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }

        [JsonConverter(typeof (DocflowDescriptionConverter))]
        public DocflowDescription Description { get; set; }

        [UsedImplicitly]
        public Urn Type { get; set; }

        [UsedImplicitly]
        public Urn Status { get; set; }

        [UsedImplicitly]
        public Urn SuccessState { get; set; }

        [UsedImplicitly]
        public List<Link> Links { get; set; }

        [UsedImplicitly]
        public DateTime SendDate { get; set; }

        [UsedImplicitly]
        public DateTime? LastChangeDate { get; set; }
    }
}