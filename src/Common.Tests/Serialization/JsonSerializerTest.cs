// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Tests.Serialization
{
    using System;
    using Common.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test fixture for the <see cref="JsonSerializer"/> class.
    /// </summary>
    [TestClass]
    public class JsonSerializerTest
    {
        /// <summary>
        /// A good case test for Deserialize.
        /// </summary>
        [TestMethod]
        public void Deserialize_ValidInput_ObjectDeserialized()
        {
            // Setup the test.
            string json = @"{""propertyOne"":""This is a property to serialize."",""propertyTwo"":""This is another property to serialize.""}";

            // Run the test.
            JsonSerializer serializer = new JsonSerializer();
            SerializableTestObject results = serializer.Deserialize<SerializableTestObject>(json);

            // Validate the results.
            Assert.AreEqual("This is a property to serialize.", results.PropertyOne);
            Assert.AreEqual("This is another property to serialize.", results.PropertyTwo);
        }

        /// <summary>
        /// A test for Deserialize where the JSON string is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deserialize_NullJson_ThrowsArgumentException()
        {
            try
            {
                // Setup the test.
                string json = null;

                // Run the test.
                JsonSerializer serializer = new JsonSerializer();
                serializer.Deserialize<SerializableTestObject>(json);
            }
            catch (ArgumentException ex)
            {
                // Validate the results.
                Assert.AreEqual("json", ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A test for Deserialize where the JSON string is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deserialize_EmptyJson_ThrowsArgumentException()
        {
            try
            {
                // Setup the test.
                string json = string.Empty;

                // Run the test.
                JsonSerializer serializer = new JsonSerializer();
                serializer.Deserialize<SerializableTestObject>(json);
            }
            catch (ArgumentException ex)
            {
                // Validate the results.
                Assert.AreEqual("json", ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A good case test for Serialize.
        /// </summary>
        [TestMethod]
        public void Serialize_ValidInput_ObjectSerialized()
        {
            // Setup the test.
            SerializableTestObject value = new SerializableTestObject
            {
                PropertyOne = "This is a property to serialize.",
                PropertyTwo = "This is another property to serialize.",
            };

            string expectedResults = @"{""propertyOne"":""This is a property to serialize."",""propertyTwo"":""This is another property to serialize.""}";

            // Run the test.
            JsonSerializer serializer = new JsonSerializer();
            string results = serializer.Serialize<SerializableTestObject>(value);

            // Validate the results.
            Assert.AreEqual(expectedResults, results);
        }

        /// <summary>
        /// A test for Serialize where the object to serialize is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Serialize_NullObject_ThrowsArgumentNullExceptionn()
        {
            try
            {
                // Setup the test.
                JsonSerializer serializer = new JsonSerializer();
                SerializableTestObject value = null;

                // Run the test.
                serializer.Serialize<SerializableTestObject>(value);
            }
            catch (ArgumentNullException ex)
            {
                // Validate the results.
                Assert.AreEqual("value", ex.ParamName);
                throw;
            }
        }
    }
}
