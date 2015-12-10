using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K020_AboutVariables : KoanHelper
	{
		//Assignacions en paral·lel (com ara les que es poden fer amb Ruby, permeten
		//assignar múltiples variables en un sol cop, utilitzant sintaxi d'arrays. 
        //Exemple: 
        //first_name, last_name = ["John", "Smith"]
		//I el que faria seria: first_name == "John" i last_name == "Smith"
		//Això no està disponible en C#, però hi ha una sèrie de mètodes d'assignació
        //prou interessants, que sí podem utilitzar.
		[Test]
		public void ImplicitAssignment ()
		{
			//Tot i que no especifiquem tipus de manera explícita, 
            //el compilador en triarà un per nosaltres quan escollim la paraula reservada var
			var name = "John";
            Assert.AreEqual(typeof(System.String), name.GetType(), "Identify the data type of the value John to proceed on your Karma quest.");
            //Original Assert.AreEqual (typeof(FILL_ME_IN), name.GetType (), "Identify the data type of the value John to proceed on your Karma quest.");

            //però només si pot. Per tant, això no funciona...
            // (Proveu de descomentar la línia següent per veure com reacciona el compilador)
			//var array = null;

			//També coneix el tipus, aleshores quan el de dalt estigui correcte, això no funcionarà.
            // (De nou, podeu descomentar la línia de sota per observar els missatges d'error del compilador)
			//name = 42;
		}

		[Test]
		public void ImplicitArrayAssignmentWithSameTypes ()
		{
			//Tot i que no especifiquem tipus de manera explícita, el compilador en triarà un per nosaltres
			var names = new[] { "John", "Smith" };
            Assert.AreEqual(typeof(System.String[]), names.GetType(), "Determine the type of the array elements to improve your Karma.");
            //Original Assert.AreEqual (typeof(FILL_ME_IN), names.GetType (), "Determine the type of the array elements to improve your Karma.");

			//però només si pot. Per tant, això no funciona...
            // (Proveu de descomentar la línia següent per veure com reacciona el compilador)
			//var array = new[] { "John", 1 };
		}

		[Test]
		public void MultipleAssignmentsOnSingleLine ()
		{
			//Podeu fer assignacions múltiples en una línia, però heu de ser explícits
			string firstName = "John", lastName = "Smith";
            //I per explícits, ens referim a que no ho podeu utilitzar
            // (Proveu de descomentar la línia següent per veure com reacciona el compilador)
            //var oneName = "John", anotherName = "Smith";
            Assert.AreEqual(lastName, firstName, "Explicit type assignment should not impact the value.");
            Assert.AreEqual(firstName, lastName, "Implicit type assignment should also not impact the value (although lastName was explicitly set).");
            // Original Assert.AreEqual (FILL_ME_IN, firstName, "Explicit type assignment should not impact the value.");
			// Original Assert.AreEqual (FILL_ME_IN, lastName, "Implicit type assignment should also not impact the value (although lastName was explicitly set).");
		}

		[Test]
		public void ImplicitAssignmentBasedOnAnotherVariable ()
		{
			long someNumber = 92286;
			var myNum = someNumber;
			Assert.AreEqual (typeof(long), myNum.GetType (), "The apple does not fall far from the tree.  Neither does the type of the long variable you pulled the value from.");

			// Això hauria de fallar, perquè uns tipus no tenen perquè convertir-se de manera implícita en uns altres...
			// (De nou, podeu descomentar la línia de sota per observar els missatges d'error del compilador)
			// myNum = myNum + 3.14159265;
			// Per a més informació en conversions implícites, podeu mirar: http://msdn.microsoft.com/en-us/library/y5b434w4.aspx
			// Mirarem de cobrir com tractar això a continuació.
		}

		[Test]
		public void ImplicitAssignmentBasedOnExpressionResults ()
		{
			int anInteger = 333;
			long aLongNumber = 92286L;
			var myTotal = anInteger * aLongNumber;
            Assert.AreEqual(typeof(long), myTotal.GetType(), "An int and a long walk into a bar... And their baby is a...");
            // Original Assert.AreEqual (typeof(FILL_ME_IN), myTotal.GetType (), "An int and a long walk into a bar... And their baby is a...");
		}

		[Test]
		public void ExplicitAssignmentByCasting ()
		{
			// Els tipus de "buckets" més petits de disseny similar (com ara els tipus numèrics) poden desar
			// els seus valors en "buckets" més grans (short --> int --> long --> float --> double)

			// Però a la inversa no funciona (grans dins petits) - no implícitament vaja.
			long aLongNumber = 92286L;
			// (Mireu de descomentar la línia de sota per mirar com reacciona el compilador)
			//int cannotDoThis = aLongNumber;

			// Però podeu fer un "cast" per assegurar-vos que la conversió es fa, com havíeu esperat de manera explícita.
			var canDoThis = (short)aLongNumber;

            Assert.AreEqual (typeof(short), canDoThis.GetType (), "Do not be short on patience. Your path to enlightenment is a process, not a destination.");
            Assert.AreEqual ((short)aLongNumber, canDoThis, "Notice how the information is changed/lost in the conversion. This is why the compiler cannot implicitly do it.");

            // Original Assert.AreEqual (typeof(FILL_ME_IN), canDoThis.GetType (), "Do not be short on patience. Your path to enlightenment is a process, not a destination.");
            // Original Assert.AreEqual (FILL_ME_IN, canDoThis, "Notice how the information is changed/lost in the conversion. This is why the compiler cannot implicitly do it.");
        }
        // També podeu declarar un tipus definit per l'usuari per habilitar la conversió implícita
        // No us preocupeu excessivament si no enteneu la definició de la classe (aneu a la secció AboutClasses)
        class SpecialInt
		{
			private int val;

			public SpecialInt (int i)
			{
				val = i;
			}
			// Això és una definició implícita de conversió d'SpecialInt a int
			public static implicit operator int (SpecialInt si)
			{
				return si.val;
			}
			// Això és una definició implícita de conversió d'int a SpecialInt
			public static implicit operator SpecialInt (int i)
			{
				return new SpecialInt (i);
			}
			// No és per complicar el tema, però potser hauríeu d'utilitzar explícitament l'implícit
			// el tipus definit per l'usuari implícit, per habilitar la conversió implícita.
		}

		[Test]
		public void ImplicitConversionUsingImplicitKeyword ()
		{
			var oneSpecialInt = new SpecialInt (123);
			// Conversió implícita a integer
			int implicitInt = oneSpecialInt;
			// Conversió implícita a SpecialInt
			SpecialInt anotherSpecialInt = implicitInt;

			var firstTest = implicitInt + oneSpecialInt;
			var secondTest = anotherSpecialInt + implicitInt;

            Assert.AreEqual(typeof(int), firstTest.GetType(), "Un int i un tipus definit per l'usuari que pot retornar un int entren en un bar... El seu bebé és un...");
            Assert.AreEqual(typeof(int), secondTest.GetType(), "Això us mostrarà que el tipus no es basa en l'ordre dels operands de l'expressió, sinó en el seu resultat.");

            // Original Assert.AreEqual (typeof(FILL_ME_IN), firstTest.GetType (), "Un int i un tipus definit per l'usuari que pot retornar un int entren en un bar... El seu bebé és un...");
			// Original Assert.AreEqual (typeof(FILL_ME_IN), secondTest.GetType (), "Això us mostrarà que el tipus no es basa en l'ordre dels operands de l'expressió, sinó en el seu resultat.");
		}
	}
}