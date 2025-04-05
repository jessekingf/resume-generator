namespace Common.Tests.Text;

using System;
using System.Collections.Generic;
using Common.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Test fixture for the <see cref="TemplateContext"/> class.
/// </summary>
[TestClass]
public class TemplateContextTests
{
    /// <summary>
    /// Good case default constructor test.
    /// </summary>
    [TestMethod]
    public void TemplateContext_Default_Success()
    {
        TemplateContext target = new TemplateContext();
        Assert.IsNotNull(target.Values);
        Assert.AreEqual(0, target.Values.Count);
    }

    /// <summary>
    /// Good case constructor test with an initial value.
    /// </summary>
    [TestMethod]
    public void TemplateContext_WithValue_Success()
    {
        string alias = "test";
        object value = new object();
        Dictionary<string, object> expectedValues = new Dictionary<string, object>()
        {
            { alias, value },
        };

        TemplateContext target = new TemplateContext(alias, value);
        CollectionAssert.AreEqual(expectedValues, target.Values);
    }

    /// <summary>
    /// A constructor test with a null alias.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TemplateContext_NullAlias_Throws()
    {
        string alias = null;
        object value = new object();

        try
        {
            _ = new TemplateContext(alias, value);
        }
        catch (ArgumentException ex)
        {
            Assert.AreEqual(nameof(alias), ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A constructor test with an empty alias.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TemplateContext_EmptyAlias_Throws()
    {
        string alias = string.Empty;
        object value = new object();

        try
        {
            _ = new TemplateContext(alias, value);
        }
        catch (ArgumentException ex)
        {
            Assert.AreEqual(nameof(alias), ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A constructor test with a null value.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TemplateContext_NullValue_Throws()
    {
        string alias = "test";
        object value = null;

        try
        {
            _ = new TemplateContext(alias, value);
        }
        catch (ArgumentNullException ex)
        {
            Assert.AreEqual(nameof(value), ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// Good case SetValue test.
    /// </summary>
    [TestMethod]
    public void SetValue_ValidParams_Success()
    {
        string alias = "test";
        object value = new object();
        Dictionary<string, object> expectedValues = new Dictionary<string, object>()
        {
            { alias, value },
        };

        TemplateContext target = new TemplateContext();
        target.SetValue(alias, value);
        CollectionAssert.AreEqual(expectedValues, target.Values);
    }

    /// <summary>
    /// A SetValue test with a null alias.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SetValue_NullAlias_Throws()
    {
        string alias = null;
        object value = new object();

        try
        {
            TemplateContext target = new TemplateContext();
            target.SetValue(alias, value);
        }
        catch (ArgumentException ex)
        {
            Assert.AreEqual(nameof(alias), ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A SetValue test with an empty alias.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SetValue_EmptyAlias_Throws()
    {
        string alias = string.Empty;
        object value = new object();

        try
        {
            TemplateContext target = new TemplateContext();
            target.SetValue(alias, value);
        }
        catch (ArgumentException ex)
        {
            Assert.AreEqual(nameof(alias), ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A SetValue test with a null value.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SetValue_NullValue_Throws()
    {
        string alias = "test";
        object value = null;

        try
        {
            TemplateContext target = new TemplateContext();
            target.SetValue(alias, value);
        }
        catch (ArgumentNullException ex)
        {
            Assert.AreEqual(nameof(value), ex.ParamName);
            throw;
        }
    }
}
