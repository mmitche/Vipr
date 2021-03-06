﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Vipr.Core.CodeModel
{
    public abstract class OdcmType : OdcmAnnotatedObject
    {
        public string Namespace { get; set; }

        public OdcmType Base { get; set; }

        public IList<OdcmType> Derived { get; private set; }

        public OdcmType(string name, string @namespace)
            : base(name)
        {
            Namespace = @namespace;
            Derived = new List<OdcmType>();
        }

        public string FullName
        {
            get { return Namespace + "." + Name; }
        }

        public abstract string AsReference();
    }
}
