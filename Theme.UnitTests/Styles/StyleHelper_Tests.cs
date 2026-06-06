// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Theme.Styles;

namespace Genova.Theme.UnitTests.Styles;

public class StyleHelperTests
{
    [Fact]
    public void Condense_returns_null_when_input_is_null()
    {
        string? input = null;
        string? result = StyleHelper.Condense(input);
        Assert.Null(result);
    }

    [Fact]
    public void Condense_returns_input_when_input_is_empty()
    {
        string? input = "";
        string? result = StyleHelper.Condense(input);
        Assert.Equal("", result);
    }

    [Fact]
    public void Condense_returns_empty_when_input_is_whitespace()
    {
        string? input = "   \n\t  ";
        string? result = StyleHelper.Condense(input);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Condense_condenses_CSS_rules_to_single_line()
    {
        string input = @"
body {
  margin: 0;
  overflow-wrap: break-word;
  -webkit-font-smoothing: antialiased;
}";
        string? result = StyleHelper.Condense(input);
        Assert.Equal("body { margin: 0; overflow-wrap: break-word; -webkit-font-smoothing: antialiased; }", result);
    }

    [Fact]
    public void Condense_removes_blank_lines()
    {
        string input = @"
body {
  margin: 0;
}

article {
  background: #fff;
}

";
        string? result = StyleHelper.Condense(input);
        Assert.DoesNotContain("\n\n", result);
    }

    [Fact]
    public void Condense_leaves_media_rule_open_close_unaffected()
    {
        string input = @"
@media (prefers-color-scheme: dark) {
  .content {
    background-color: #1b1e21;
  }
}
";
        string? result = StyleHelper.Condense(input);
        Assert.Contains("@media (prefers-color-scheme: dark) {", result);
        Assert.Contains("}", result);
    }

    [Fact]
    public void Condense_indents_child_rules_inside_media()
    {
        string input = @"
@media (prefers-color-scheme: dark) {
  .content {
    background-color: #1b1e21;
  }
}
";
        string? result = StyleHelper.Condense(input);
        Assert.Contains("  .content { background-color: #1b1e21; }", result);
    }

    [Fact]
    public void Condense_removes_CSS_comments()
    {
        string input = @"
body {
  /* This is a comment */
  margin: 0; /* another comment */
  overflow-wrap: break-word;
  -webkit-font-smoothing: antialiased; /* trailing comment */
}
/* comment outside rule */
article, .content {
  background-color: #d5d5d5; /* comment at end */
}
";
        string? result = StyleHelper.Condense(input);

        // Assert no comment markers remain
        Assert.DoesNotContain("/*", result);
        Assert.DoesNotContain("*/", result);

        // Assert rules are still condensed correctly
        Assert.Contains("body { margin: 0; overflow-wrap: break-word; -webkit-font-smoothing: antialiased; }", result);
        Assert.Contains("article, .content { background-color: #d5d5d5; }", result);
    }

    [Fact]
    public void Condense_properly_handles_a_print_media_rule()
    {
        string input = @"
/* =====================  WIDTH CONTAINER  ===================== */
.layout-container {
  position: relative;
  padding: 0 1.5rem;
}
/* 60ch ˜ 55–60 glyphs = happy eyeballs. */
.layout-container,
.reading-area {
  max-width: 60ch;
  margin: 0 auto;
}

@media print {
  .layout-container {
    max-width: none;
  }
}
/* =====================  SKIP LINKS  ===================== */
/* Off-screen until focused, then full-width overlay */
.skip-links {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  transform: translateY(-100%);
  z-index: 1000;
}
".Trim();
        string? result = StyleHelper.Condense(input);
        File.WriteAllText(@"c:\temp\condensed.css", result);

        string expect = @"
.layout-container { position: relative; padding: 0 1.5rem; }
.layout-container, .reading-area { max-width: 60ch; margin: 0 auto; }
@media print {
  .layout-container { max-width: none; }
}
.skip-links { position: absolute; top: 0; left: 0; width: 100%; transform: translateY(-100%); z-index: 1000; }
".Replace("\r\n", "\n").Trim();
        Assert.Equal(expect, result);
    }
}
