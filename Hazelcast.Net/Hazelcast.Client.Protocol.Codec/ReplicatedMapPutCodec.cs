// Copyright (c) 2008-2018, Hazelcast, Inc. All Rights Reserved.
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

using Hazelcast.Client.Protocol.Util;
using Hazelcast.IO;
using Hazelcast.IO.Serialization;

// Client Protocol version, Since:1.0 - Update:1.0
namespace Hazelcast.Client.Protocol.Codec
{
    internal sealed class ReplicatedMapPutCodec
    {
        public static readonly ReplicatedMapMessageType RequestType = ReplicatedMapMessageType.ReplicatedMapPut;
        public const int ResponseType = 105;
        public const bool Retryable = false;

        //************************ REQUEST *************************//

        public class RequestParameters
        {
            public static readonly ReplicatedMapMessageType TYPE = RequestType;
            public string name;
            public IData key;
            public IData value;
            public long ttl;

            public static int CalculateDataSize(string name, IData key, IData value, long ttl)
            {
                var dataSize = ClientMessage.HeaderSize;
                dataSize += ParameterUtil.CalculateDataSize(name);
                dataSize += ParameterUtil.CalculateDataSize(key);
                dataSize += ParameterUtil.CalculateDataSize(value);
                dataSize += Bits.LongSizeInBytes;
                return dataSize;
            }
        }

        public static ClientMessage EncodeRequest(string name, IData key, IData value, long ttl)
        {
            var requiredDataSize = RequestParameters.CalculateDataSize(name, key, value, ttl);
            var clientMessage = ClientMessage.CreateForEncode(requiredDataSize);
            clientMessage.SetMessageType((int) RequestType);
            clientMessage.SetRetryable(Retryable);
            clientMessage.Set(name);
            clientMessage.Set(key);
            clientMessage.Set(value);
            clientMessage.Set(ttl);
            clientMessage.UpdateFrameLength();
            return clientMessage;
        }

        //************************ RESPONSE *************************//
        public class ResponseParameters
        {
            public IData response;
        }

        public static ResponseParameters DecodeResponse(IClientMessage clientMessage)
        {
            var parameters = new ResponseParameters();
            var responseIsNull = clientMessage.GetBoolean();
            if (!responseIsNull)
            {
                var response = clientMessage.GetData();
                parameters.response = response;
            }
            return parameters;
        }
    }
}