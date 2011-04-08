﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharpUtilsTests
{
	class Paths
	{
		static public String ProjectPath
		{
			get
			{
				return Directory.GetCurrentDirectory() + @"\..\..\..";
			}
		}

		static public String ProjectTestInputPath
		{
			get
			{
				return ProjectPath + @"\TestInput";
			}
		}

		static public String ProjectTestInputMountedPath
		{
			get
			{
				return ProjectPath + @"\TestInputMounted";
			}
		}

		static public String ProjectTestOutputPath
		{
			get
			{
				return ProjectPath + @"\TestOutput";
			}
		}
	}
}