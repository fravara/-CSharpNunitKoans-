using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K040_AboutArrays : KoanHelper
	{
		[Test]
		public void CreatingArrays ()
		{
			var empty_array = new object[] { };
			Assert.AreEqual (typeof(object[]), empty_array.GetType ());

            // Noteu que heu de comprovar explícitament les subclasses
            Assert.IsTrue (typeof(Array).IsAssignableFrom (empty_array.GetType ()));

			Assert.AreEqual (0, empty_array.Length);
        }

        [Test]
		public void ArrayLiterals ()
		{
			var array = new[] { 42 };
			Assert.AreEqual (typeof(int[]), array.GetType (), "No heu d'especificar un tipus si els elements poden ser inferits");
			Assert.AreEqual (new int[] { 42 }, array, "Aquests arrays són literalment iguals... Però no veureu aquest string en el missatge d'error.");

            // Els arrays són 0-based o 1-based?
            Assert.AreEqual(42, array[((int)0)], "Bé, o és 0 o és 1... Teniu 110010-110010 oportunitats de fer-ho bé.");
            // Original Assert.AreEqual (42, array [((int)FILL_ME_IN)], "Bé, o és 0 o és 1... Teniu 110010-110010 oportunitats de fer-ho bé.");

			// Això és important perquè...
			Assert.IsTrue (array.IsFixedSize, "...perquè els arrays de mida fixa no són dinàmics.");

			// Moguda aquesta crida Throws() a un mètode separat FixedSizeArraysCannotGrow()...
			// ...significa que no podem fer això: array[1] = 13;
			// Assert.Throws(typeof(FILL_ME_IN), delegate() { array[1] = 13; });

			// Això és perquè l'array està fixat a length 1. Podríeu escriure una funció que
			// creés un nou array més gran que l'anterior, copiés els elements, i retornés 
			// aquest nou array, o podríeu fer això:
			var dynamicArray = new List<int> ();
			dynamicArray.Add (42);
			CollectionAssert.AreEqual (array, dynamicArray.ToArray (), "Els arrays dinàmics poden créixer");

			dynamicArray.Add (13);
            CollectionAssert.AreEqual((new int[] { 42, (int)13 }), dynamicArray.ToArray(), "Idenfitica tots els elements de l'array");
            // Original CollectionAssert.AreEqual ((new int[] { 42, (int)FILL_ME_IN }), dynamicArray.ToArray (), "Idenfitica tots els elements de l'array");
		}

		[Test]
		public void TestMe ()
		{
			var array = new[] { 42 };
			var dynamicArray = new List<int> ();
			dynamicArray.Add (42);
			CollectionAssert.AreEqual (array, dynamicArray.ToArray (), "La resposta a la Pregunta Definitiva és 42. Però aquesta no és la resposta d'aquest Assert.");

            dynamicArray.Add (13);
			CollectionAssert.AreEqual ((new int[] { 42, (int)13 }), dynamicArray.ToArray (), "Coneixeu l'expressió: 'So Long, and Thanks for All the Array Elements...'?");
            // Original CollectionAssert.AreEqual((new int[] { 42, (int)FILL_ME_IN }), dynamicArray.ToArray(), "Coneixeu l'expressió: 'So Long, and Thanks for All the Array Elements...'?");

        }

        // Tret de la crida de l'Assert.Throws() superior
        [Test]
		public void FixedSizeArraysCannotGrow ()
		{
			try {
				var array = new[] { 42 };
				array [1] = 13;
			} catch (Exception exception) {
                Assert.AreEqual ("Índex array esta fora de rango.", exception.Message, "Els arrays de mida fixa, comparats amb els dinàmics, per la seva veritable definició, no poden créixer. Estan estancats, i tal... Yeah.");
                // Original Assert.AreEqual(FILL_ME_IN, exception.Message, "Els arrays de mida fixa, comparats amb els dinàmics, per la seva veritable definició, no poden créixer. Estan estancats, i tal... Yeah.");
            }

        }

        [Test]
		public void AccessingArrayElements ()
		{
			var array = new[] { "mantega", "gos", "i", "melmelada" };

			Assert.AreEqual ("mantega", array [0], "Hi ha gossos a qui els agrada molt endrapar, però potser no es menjarien la mantega tota sola... No sé si m'enteneu...");
			Assert.AreEqual ("melmelada", array [3], "Amb una mica de _ _ _ _ _ _ _ _ _ potser ja podríem començar a parlar seriosament.");

            try
            {
				var cannotDoThis = array [-1];
			} catch (Exception exception) {
                Assert.AreEqual("Índex fora dels limits de la matriu.", exception.Message,
					"Corregir aquesta us ajudarà a entendre on anar a buscar les excepcions quan succeeixin...");
			}
            
		}

		[Test]
		public void SlicingArrays ()
		{
			var array = new[] { "cacauet", "mantega", "amb", "gelatina" };

			// Cridar el mètode Take(x) d'un array retornarà els x números especificats de l'inici de l'array.
			Assert.AreEqual (new string[] { "cacauet", "mantega" }, array.Take (2).ToArray (), "George Washington Carver estaria orgullós de tu si sabés que has trobat un altre ús per la mantega de cacauet.");
			// Cridar el mètode Skip(y) d'un array farà un bypass dels y números especificats de l'inici de l'array i retornarà els elements restants.
			CollectionAssert.AreEqual (new string[] { "amb" }, array.Skip (2).Take (1).ToArray (), "Les teves habilitats de slicing arrays necessiten millorar per assolit un bon karma de C#.");
		}

		[Test]
		public void PushingAndPopping ()
		{
			var array = new[] { 1, 2 };
			// Noteu com un array és posat en una pila. Potser no era el que t'esperaves...
			var stack = new Stack (array);
			stack.Push ("darrer");
			CollectionAssert.AreEqual ((ICollection)stack, stack.ToArray (), "Convertir aquesta pila novament a un array us podria sorprendre.");
			var poppedValue = stack.Pop ();
			Assert.AreEqual ("darrer", poppedValue, "Els elements que s'extreuen provenen del cim de la pila (se'n diu fer un 'Pop'). Suposo que si provinguessin del cul... Es diria 'Plop'?");
			CollectionAssert.AreEqual ((ICollection)stack, stack.ToArray (), "De fet, no estic gaire segur de què fa això aquí... Suposo que no hi havia prou material com per crear un AboutStacks...");
		}

		[Test]
		public void ManagingElementsAtBothEnds ()
		{
			// En Ruby, "shifting" es defineix així:
			// Shift == Remove First Element
			// Unshift == Insert Element at Beginning

			// C# no ofereix això de manera nativa, però teniu diverses opcions. Usarem la LinkedList<T> per implementar-ho.
			// En C#, els noms de les funcions són relativament intuïtius, però Shift = RemoveFirst i Unshift = AddFirst.
			var array = new[] { "Hola", "Món" };
			var list = new LinkedList<string> (array);

			list.AddFirst ("Digues");
			CollectionAssert.AreEqual ((ICollection)list, list.ToArray (), "Ja n'hem tingut suficient d'AboutLists.. Què hi fa això aquí?");

			list.RemoveLast ();
			CollectionAssert.AreEqual ((ICollection)list, list.ToArray (), "Si us hi fixeu, veureu que la Hello Kitty no té boca (pobres dibuixos animats, perquè en Krilin tampoc té nas!). Així doncs, com pot dir 'Hola'?");

			list.RemoveFirst ();
			CollectionAssert.AreEqual ((ICollection)list, list.ToArray (), "Me estabas buscando a mí?");

			list.AddAfter (list.Find ("Hello"), "World");
			CollectionAssert.AreEqual ((ICollection)list, list.ToArray (), "Ara sí que això ja és un súper test d'una llista... Simplement cridem ToArray() diverses vegades. Tot i això, és una bona pràctica.");
		}
	}
}