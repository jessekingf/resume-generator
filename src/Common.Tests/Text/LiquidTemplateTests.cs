namespace Common.Tests.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// A test fixture for the <see cref="LiquidTemplate"/> class.
/// </summary>
[TestClass]
public class LiquidTemplateTests
{
    /// <summary>
    /// A test for the default constructor.
    /// </summary>
    [TestMethod]
    public void LiquidTemplate_Default_Success()
    {
        LiquidTemplate target = new LiquidTemplate();

        Assert.IsNotNull(target);
        Assert.IsNull(target.Template);
        Assert.IsNotNull(target.RegisteredTypes);
        Assert.AreEqual(0, target.RegisteredTypes.Count);
    }

    /// <summary>
    /// A constructor test with a valid template.
    /// </summary>
    [TestMethod]
    public void LiquidTemplate_ValidTemplate_Success()
    {
        string template = "Hello {{ p.Firstname }} {{ p.Lastname }}";
        LiquidTemplate target = new LiquidTemplate(template);

        Assert.IsNotNull(target);
        Assert.AreEqual(template, target.Template);
        Assert.IsNotNull(target.RegisteredTypes);
        Assert.AreEqual(0, target.RegisteredTypes.Count);
    }

    /// <summary>
    /// A constructor test with an empty template.
    /// </summary>
    [TestMethod]
    public void LiquidTemplate_EmptyTemplate_Success()
    {
        string template = string.Empty;
        LiquidTemplate target = new LiquidTemplate(template);

        Assert.IsNotNull(target);
        Assert.AreEqual(template, target.Template);
        Assert.IsNotNull(target.RegisteredTypes);
        Assert.AreEqual(0, target.RegisteredTypes.Count);
    }

    /// <summary>
    /// A constructor test with a null template.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LiquidTemplate_NullTemplate_Throws()
    {
        string template = null;

        try
        {
            LiquidTemplate target = new LiquidTemplate(template);
        }
        catch (ArgumentNullException ex)
        {
            Assert.AreEqual("template", ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A good case test for the Parse method.
    /// </summary>
    [TestMethod]
    public void Parse_ValidTemplate_Success()
    {
        string template = "Hello {{ p.Firstname }} {{ p.Lastname }}";

        LiquidTemplate target = new LiquidTemplate();
        target.Parse(template);

        Assert.AreEqual(template, target.Template);
    }

    /// <summary>
    /// A test for the Parse method with an empty template.
    /// </summary>
    [TestMethod]
    public void Parse_EmptyTemplate_Success()
    {
        string template = string.Empty;

        LiquidTemplate target = new LiquidTemplate();
        target.Parse(template);

        Assert.AreEqual(template, target.Template);
    }

    /// <summary>
    /// A test for the Parse method with a null template.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Parse_NullTemplate_Throws()
    {
        string template = null;

        try
        {
            LiquidTemplate target = new LiquidTemplate();
            target.Parse(template);
        }
        catch (ArgumentNullException ex)
        {
            Assert.AreEqual("template", ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A good case test for the RegisterType method.
    /// </summary>
    [TestMethod]
    public void RegisterType_ValidType_Success()
    {
        List<Type> expectedTypes = new List<Type>()
        {
            typeof(string),
            typeof(Person),
        };

        LiquidTemplate target = new LiquidTemplate();
        foreach (Type type in expectedTypes)
        {
            target.RegisterType(type);
        }

        CollectionAssert.AreEquivalent(expectedTypes, target.RegisteredTypes.ToList());
    }

    /// <summary>
    /// A test for the RegisterType method where the type being registered is a duplicate.
    /// </summary>
    [TestMethod]
    public void RegisterType_DuplicateType_Ignored()
    {
        List<Type> expectedTypes = new List<Type>()
        {
            typeof(Person),
        };

        LiquidTemplate target = new LiquidTemplate();
        target.RegisterType(typeof(Person));
        target.RegisterType(typeof(Person)); // duplicate

        CollectionAssert.AreEquivalent(expectedTypes, target.RegisteredTypes.ToList());
    }

    /// <summary>
    /// A test for the RegisterType method with a null type.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RegisterType_Nullype_Throws()
    {
        Type type = null;

        try
        {
            LiquidTemplate target = new LiquidTemplate();
            target.RegisterType(type);
        }
        catch (ArgumentNullException ex)
        {
            Assert.AreEqual("type", ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A good case test for the Render method.
    /// </summary>
    [TestMethod]
    public void Render_ValidContext_Success()
    {
        // TODO: Setup the test to cover all features of liquid templates.
        string template = "Hello {{ p.Firstname }} {{ p.Lastname }}";

        Person model = new Person()
        {
            Firstname = "Jim",
            Lastname = "Bob",
        };
        TemplateContext context = new TemplateContext("p", model);

        LiquidTemplate target = new LiquidTemplate(template);
        target.RegisterType(typeof(Person));
        string result = target.Render(context);

        Assert.AreEqual("Hello Jim Bob", result);
    }

    /// <summary>
    /// A test for the Render method where the template is empty.
    /// </summary>
    [TestMethod]
    public void Render_EmptyTemplateValidContext_EmptyReturned()
    {
        string template = string.Empty;

        Person model = new Person()
        {
            Firstname = "Jim",
            Lastname = "Bob",
        };
        TemplateContext context = new TemplateContext("p", model);

        LiquidTemplate target = new LiquidTemplate(template);
        target.RegisterType(typeof(Person));
        string result = target.Render(context);

        Assert.AreEqual(string.Empty, result);
    }

    /// <summary>
    /// A test for the Render method where the template has not been set.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Render_TemplateNotSet_Throws()
    {
        Person model = new Person()
        {
            Firstname = "Jim",
            Lastname = "Bob",
        };
        TemplateContext context = new TemplateContext("p", model);

        try
        {
            LiquidTemplate target = new LiquidTemplate();
            target.RegisterType(typeof(Person));
            target.Render(context);
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The template has not been parsed.", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// A test for the Render method where the template has not been set.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Render_NullContext_Throws()
    {
        string template = "Hello {{ p.Firstname }} {{ p.Lastname }}";

        try
        {
            LiquidTemplate target = new LiquidTemplate(template);
            target.RegisterType(typeof(Person));
            target.Render(null);
        }
        catch (ArgumentNullException ex)
        {
            Assert.AreEqual("context", ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A sample model object to use for the template tests.
    /// </summary>
    private class Person
    {
        /// <summary>
        /// Gets or sets the first sample parameter.
        /// </summary>
        public string Firstname
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the second sample parameter.
        /// </summary>
        public string Lastname
        {
            get;
            set;
        }
    }
}
