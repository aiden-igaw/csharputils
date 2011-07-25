﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpUtils.Html;

namespace CSharpUtilsTests.Html
{
	[TestClass]
	public class HtmlUtilsTest
	{
		[TestMethod]
		public void TestEscapeHtmlCharacters()
		{
			Assert.AreEqual("&lt;p&gt;&quot;test", HtmlUtils.EscapeHtmlCharacters("<p>\"test"));
		}
	}
}
