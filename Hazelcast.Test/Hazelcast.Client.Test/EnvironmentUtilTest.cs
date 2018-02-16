﻿// Copyright (c) 2008-2018, Hazelcast, Inc. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Hazelcast.Util;
using NUnit.Framework;


namespace Hazelcast.Client.Test
{
    public class EnvironmentUtilTest
    {
        [Test]
        public void TestDLLVersion()
        {
            var version = typeof(EnvironmentUtil).Assembly.GetName().Version.ToString();
            Assert.True(version.StartsWith(EnvironmentUtil.GetDllVersion()));
            Assert.False(EnvironmentUtil.GetDllVersion().EndsWith(".0"));
        }
    }
}