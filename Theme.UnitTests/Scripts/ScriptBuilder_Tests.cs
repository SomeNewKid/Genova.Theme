// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System;
using FluentAssertions;
using Genova.Theme.Scripts;
using Xunit;

namespace Genova.Theme.UnitTests.Scripts;

public class ScriptBuilder_Tests
{
    [Fact]
    public void Constructor_should_throw_ArgumentNullException_when_options_is_null()
    {
        // Act
        Action act = () => _ = new ScriptBuilder(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("options");
    }
}
