﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpUtils.Extensions
{
	static public class StringExtensions
	{
		static public String Substr(this String String, int StartIndex)
		{
			if (StartIndex < 0) StartIndex = String.Length + StartIndex;
			StartIndex = MathUtils.Clamp(StartIndex, 0, String.Length);
			return String.Substring(StartIndex);
		}

		static public String Substr(this String String, int StartIndex, int Length)
		{
			if (StartIndex < 0) StartIndex = String.Length + StartIndex;
			StartIndex = MathUtils.Clamp(StartIndex, 0, String.Length);
			var End = StartIndex + Length;
			if (Length < 0) Length = String.Length + Length - StartIndex;
			Length = MathUtils.Clamp(Length, 0, String.Length - StartIndex);
			return String.Substring(StartIndex, Length);
		}

		static public byte[] GetStringzBytes(this String String, Encoding Encoding)
		{
			return String.GetBytes(Encoding).Concat(new byte[] { 0 }).ToArray();
		}

		static public byte[] GetBytes(this String This)
		{
			return This.GetBytes(Encoding.UTF8);
		}

		static public byte[] GetBytes(this String This, Encoding Encoding)
		{
			return Encoding.GetBytes(This);
		}

		static public String EscapeString(this String This)
		{
			var That = "";
			foreach (var C in This)
			{
				switch (C)
				{
					case '\n': That += @"\n"; break;
					case '\r': That += @"\r"; break;
					case '\t': That += @"\t"; break;
					default: That += C; break;
				}
			}
			return That;
		}
	}
}
