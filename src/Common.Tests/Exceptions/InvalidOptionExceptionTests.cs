// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Tests.Extensions
{
    using System;
    using Common.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// A test fixture for the <see cref="InvalidOptionException"/> class.
    /// </summary>
    [TestClass]
    public class InvalidOptionExceptionTests
    {
        /// <summary>
        /// A good case default constructor test.
        /// </summary>
        [TestMethod]
        public void Ctor_NoArgs_SuccessfulInstantiation()
        {
            InvalidOptionException target = new InvalidOptionException();

            Assert.IsNotNull(target);
            Assert.AreEqual("An invalid program option was specified.", target.Message);
            Assert.IsNull(target.OptionName);
            Assert.IsNull(target.InnerException);
        }

        /// <summary>
        /// A good case constructor test with a message.
        /// </summary>
        [TestMethod]
        public void Ctor_ValidMessage_SuccessfulInstantiation()
        {
            string message = "Horrible configuration error.";
            InvalidOptionException target = new InvalidOptionException(message);

            Assert.IsNotNull(target);
            Assert.AreEqual(message, target.Message);
            Assert.IsNull(target.OptionName);
            Assert.IsNull(target.InnerException);
        }

        /// <summary>
        /// A good case constructor test with a message and command name.
        /// </summary>
        [TestMethod]
        public void Ctor_MessageAndCommandName_SuccessfulInstantiation()
        {
            string message = "Horrible configuration error.";
            string optionName = "An option";
            InvalidOptionException target = new InvalidOptionException(message, optionName);

            Assert.IsNotNull(target);
            Assert.AreEqual(message, target.Message);
            Assert.AreEqual(optionName, target.OptionName);
            Assert.IsNull(target.InnerException);
        }

        /// <summary>
        /// A good case constructor test with a message and inner exception.
        /// </summary>
        [TestMethod]
        public void Ctor_MessageAndInnerException_SuccessfulInstantiation()
        {
            string message = "Horrible configuration error.";
            ArgumentException innerException = new ArgumentException("Horrible inner exception");
            InvalidOptionException target = new InvalidOptionException(message, innerException);

            Assert.IsNotNull(target);
            Assert.AreEqual(message, target.Message);
            Assert.IsNull(target.OptionName);
            Assert.AreEqual(innerException, target.InnerException);
        }

        /// <summary>
        /// A good case constructor test with a message, command name and inner exception.
        /// </summary>
        [TestMethod]
        public void Ctor_AllParams_SuccessfulInstantiation()
        {
            string message = "Horrible configuration error.";
            string optionName = "An option";
            ArgumentException innerException = new ArgumentException("Horrible inner exception");
            InvalidOptionException target = new InvalidOptionException(message, optionName, innerException);

            Assert.IsNotNull(target);
            Assert.AreEqual(message, target.Message);
            Assert.AreEqual(optionName, target.OptionName);
            Assert.AreEqual(innerException, target.InnerException);
        }
    }
}
