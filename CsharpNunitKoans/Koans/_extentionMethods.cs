﻿using System;
using NUnit.Framework;

namespace TheKoans
{
	public static class ExtensionMethods
	{
		public static string HelloWorld (this KoanHelper koan)
		{
			return "Hola!";
		}

		public static string SayHello (this KoanHelper koan, string name)
		{
			return String.Format ("Hola, {0}!", name);
		}

		public static string[] MethodWithVariableArguments (this KoanHelper koan, params string[] names)
		{
			return names;
		}

		public static string SayHi (this String str)
		{
			return "Hola, " + str;
		}
	}
}
