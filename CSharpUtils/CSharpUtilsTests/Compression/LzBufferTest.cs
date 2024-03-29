﻿using CSharpUtils.Compression.Lz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using CSharpUtils.Extensions;
using CSharpUtils.Compression;
using System.Collections.Generic;

namespace CSharpUtilsTests.Compression
{
	[TestClass()]
	public class LzBufferTest
	{
		[TestMethod()]
		public void FindMaxSequenceTest()
		{
			LzBuffer LzBuffer = new LzBuffer(3);
			LzBuffer.AddBytes(new byte[] {
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18,
				7, 8, 9, 10, 12, 13,
				7, 8, 9, 10, 11, 16
			});

			var Result = LzBuffer.FindMaxSequence(
				LzBuffer.Size - 6,
				LzBuffer.Size - 6,
				0x1000,
				3,
				16,
				true
			);

			Assert.AreEqual("LzBuffer.FindSequenceResult(Offset=7, Size=5)", Result.ToString());
		}

		[TestMethod()]
		public void HandleWithOverlappingTest()
		{
			var Data = Encoding.UTF8.GetBytes("abccccccabc");
			var Results = new List<string>();
			LzBuffer.Handle(Data, 2, 15 + 2, ushort.MaxValue, true, (int Position, int FoundOffset, int FoundSize) =>
			{
				if (FoundSize == 0)
				{
					Results.Add("PUT(" + Data[Position] + ")");
				}
				else
				{
					Results.Add("REPEAT(" + FoundOffset + "," + FoundSize + ")");
				}
			});

			Assert.AreEqual(
				"PUT(97),PUT(98),PUT(99),REPEAT(-1,5),REPEAT(-8,3)",
				Results.ToStringArray()
			);
		}

		[TestMethod()]
		public void HandleWithoutOverlappingTest()
		{
			var Data = Encoding.UTF8.GetBytes("abccccccccccccccccccccccabc");
			var Results = new List<string>();
			LzBuffer.Handle(Data, 3, 9, ushort.MaxValue, false, (int Position, int FoundOffset, int FoundSize) =>
			{
				if (FoundSize == 0)
				{
					Results.Add("PUT(" + Data[Position] + ")");
				}
				else
				{
					Results.Add("REPEAT(" + FoundOffset + "," + FoundSize + ")");
				}
			});

			Assert.AreEqual(
				"PUT(97),PUT(98),PUT(99),PUT(99),PUT(99),REPEAT(-3,3),REPEAT(-6,6),REPEAT(-9,9),PUT(99),REPEAT(-24,3)",
				Results.ToStringArray()
			);
		}

	}
}
