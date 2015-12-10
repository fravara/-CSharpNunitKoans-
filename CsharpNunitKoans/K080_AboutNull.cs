using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K080_AboutNull : KoanHelper
	{
		[Test]
		public void NilIsNotAnObject ()
		{
			// No tot, tot, tot, tot, tot, són objectes...
			Assert.IsTrue (typeof(object).IsAssignableFrom (null), "Per citar l'episodi de World of Warcraft de South Park: 'Can you kill that which has no life?'"); 
		}

		[Test]
		public void NullIsNotAnInstanceOfObjectAndInvokingAMethodThrowsNullReferenceException ()
		{
			Object nullReference = null;

			try {
				nullReference.GetHashCode ();
			} catch (Exception exception) {
				Assert.AreEqual ("Referencia a un objecte no establert com instancia d'un objecte.", exception.Message, "I per si encara no esteu segurs, feu un cop d'ull al nom d'aquest mètode...");
			}
		}

		[Test]
		public void CheckingThatAnObjectIsNull ()
		{
			object obj = null;
			Assert.IsTrue (obj == null);
		}

		[Test]
		public void ABetterWayToCheckThatAnObjectIsNull ()
		{
			object obj = null;
			Assert.IsNull (obj, "Tan de bo tinguéssim un objecte passat com a paràmetre...");
		}

		[Test]
		public void AWayNotToCheckThatAnObjectIsNull ()
		{
			object obj = null;
			Assert.IsTrue (obj.Equals (null));
		}
		
		// Passant Null Objects als mètodes d'extensió de C#... estarà permès?!?!?
		// http://www.peteonsoftware.com/index.php/2012/06/21/c-extension-methods-on-null-objects/
	}
}
