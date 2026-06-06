// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Testing.QualityTests;
using Genova.Theme.Styles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genova.Theme.QualityTests;

[TestClass]
public class CodeQuality_Tests : CodeQuality_Base
{
    public CodeQuality_Tests()
        : base(typeof(StyleBuilder).Assembly, "Genova.Theme")
    {
    }

    [TestMethod]
    public void Required_test()
    {
        // This test is required to ensure that the test class is recognized by the test runner.
        // It does not need to contain any assertions or logic.
        string message = "This is a placeholder test to ensure the test class is recognized.";
        Assert.IsFalse(string.IsNullOrEmpty(message));
    }
}
