// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Security.Cryptography.Encryption.TripleDes.Tests
{
    internal class TripleDesProvider : ITripleDESProvider
    {
        public TripleDES Create()
        {
            return TripleDES.Create();
        }
    }

    public partial class TripleDESFactory
    {
        private static readonly ITripleDESProvider s_provider = new TripleDesProvider();
    }
}
