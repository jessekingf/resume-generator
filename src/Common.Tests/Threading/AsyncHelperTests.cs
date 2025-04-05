namespace Common.Tests.Threading;

using System;
using System.Threading.Tasks;
using Common.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// A test fixture for the <see cref="AsyncHelper"/>  class.
/// </summary>
[TestClass]
public class AsyncHelperTests
{
    /// <summary>
    /// Whether the test method was called.
    /// </summary>
    private bool methodRun;

    /// <summary>
    /// Initialization run before each test.
    /// </summary>
    [TestInitialize]
    public void InitializeTest()
    {
        this.methodRun = false;
    }

    /// <summary>
    /// A good case test RunSync with a void function.
    /// </summary>
    [TestMethod]
    public void RunSync_VoidFunction_Success()
    {
        AsyncHelper.RunSync(() => this.Test());
        Assert.IsTrue(this.methodRun);
    }

    /// <summary>
    /// A good case test for RunSync with a function with a value.
    /// </summary>
    [TestMethod]
    public void RunSync_FunctionWithReturn_Success()
    {
        bool result = AsyncHelper.RunSync<bool>(() => this.Test(1));
        Assert.IsTrue(this.methodRun);
        Assert.IsTrue(this.methodRun);
    }

    /// <summary>
    /// A test for RunSync with a null void function.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RunSync_NullVoidFunction_Throws()
    {
        try
        {
            AsyncHelper.RunSync(null);
        }
        catch (ArgumentNullException ex)
        {
            Assert.AreEqual("function", ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A test for RunSync with a null function with a return value.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RunSync_NullFunctionWithReturn_Throws()
    {
        try
        {
            AsyncHelper.RunSync<bool>(null);
        }
        catch (ArgumentNullException ex)
        {
            Assert.AreEqual("function", ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A test asynchronous void method.
    /// </summary>
    private async Task Test()
    {
        await Task.Delay(1);
        this.methodRun = true;
    }

    /// <summary>
    ///  A test asynchronous method with a return value.
    /// </summary>
    /// <param name="delay">The time in milliseconds for the delay to simulate work..</param>
    /// <returns>A test return value.</returns>
    private async Task<bool> Test(int delay)
    {
        await Task.Delay(delay);
        this.methodRun = true;
        return this.methodRun;
    }
}
