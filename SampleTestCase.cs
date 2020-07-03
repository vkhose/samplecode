// Copyright 2018 Confluent Inc.
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
//
// Refer to LICENSE for more information.

// Disable obsolete warnings. ConstructValueSubjectName is still used a an internal implementation detail.
#pragma warning disable CS0618

using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using au.com.example.@event;
using Confluent.Kafka;
using Confluent.Kafka.Examples.@event;


namespace Confluent.SchemaRegistry.Serdes.UnitTests
{
    public class SerializeDeserialzeTests
    {
        private ISchemaRegistryClient schemaRegistryClient;
        private string testTopic;
        private Dictionary<string, int> store = new Dictionary<string, int>();

        public SerializeDeserialzeTests()
        {
            testTopic = "topic";
            var schemaRegistryMock = new Mock<ISchemaRegistryClient>();
            schemaRegistryMock.Setup(x => x.ConstructValueSubjectName(testTopic, It.IsAny<string>())).Returns($"{testTopic}-value");
            schemaRegistryMock.Setup(x => x.RegisterSchemaAsync("topic-value", It.IsAny<string>())).ReturnsAsync(
                (string topic, string schema) => store.TryGetValue(schema, out int id) ? id : store[schema] = store.Count + 1
            );
            schemaRegistryMock.Setup(x => x.GetSchemaAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(
                (int id, string format) => new Schema(store.Where(x => x.Value == id).First().Key, null, SchemaType.Avro)
            );
            schemaRegistryClient = schemaRegistryMock.Object;   
        }

    
        [Fact]
        public void ISpecificRecordComplex()
        {
            var serializer = new AvroSerializer<Parent>(schemaRegistryClient);
            var deserializer = new AvroDeserializer<Parent>(schemaRegistryClient);

            var user = new Parent
            {
                children = new List<Child>
                {
                    new Child
                    {
                        name = "test"
                    }
                }
            };

            var bytes = serializer.SerializeAsync(user, new SerializationContext(MessageComponentType.Value, testTopic)).Result;
            var result = deserializer.DeserializeAsync(bytes, false, new SerializationContext(MessageComponentType.Value, testTopic)).Result;

            //Assert.Equal(user.name, result.name);
            //Assert.Equal(user.favorite_color, result.favorite_color);
            //Assert.Equal(user.favorite_number, result.favorite_number);
        }
    }
}
