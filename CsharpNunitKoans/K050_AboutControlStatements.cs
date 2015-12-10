using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K050_AboutControlStatements : KoanHelper
	{
		[Test]
		public void IfThenElseStatementsWithBrackets ()
		{
			bool b;
			if (true) {
				b = true;
			} else {
				b = false;
			}
			
            Assert.AreEqual (true, b, "'To b or not to b', that really is the answer...");
		}

		[Test]
		public void IfThenElseStatementsWithoutBrackets ()
		{
			bool b;
			if (true)
				b = true;
			else
				b = false;
			
            Assert.AreEqual (true, b, "A no ser que l'statement fos un 'break', un 'continue' o un 'return', jo sempre utilitzaria els corxets per evitar confusions.");
		}

		[Test]
		public void IfThenStatementsWithBrackets ()
		{
			bool b = false;
			if (true) {
				b = true;
			}

			Assert.AreEqual (true, b, "Un bon dia, un hacker em va dir: 'brackets do not cause a racket'. Ens donen més claredat i potencial pels futurs statements.");
		}

		[Test]
		public void IfThenStatementsWithoutBrackets ()
		{
			bool b = false;
			if (true)
				b = true;

			Assert.AreEqual (true, b, "El codi pot semblar més net sense ells, però els corxets ens ajudaran a evitar confusions.");
		}

		[Test]
		public void WhyItIsWiseToAlwaysUseBrackets ()
		{
			bool b1 = false;
			bool b2 = false;

			int counter = 1;

			if (counter == 0)
				b1 = true;
			b2 = true;

			Assert.AreEqual (false, b1, "Indentar sovint farà que el vostre codi sigui més llegible, però només quan ho feu de manera correcta!");
			Assert.AreEqual (true, b2, "L'espai en blanc no sempre serà el vostre amic... Prepareu-vos!");
		}

		[Test]
		public void TernaryOperators ()
		{
			Assert.AreEqual (false, (true ? 1 : 0), "Ha arribat el vostre tern, petites llagostes, de triar el valor apropiat..");
			Assert.AreEqual (true, (false ? 1 : 0), "O potser hi podeu ternar a intentar aquí fins que trieu correctament?");
		}
		
        // Això queda fora de lloc pels control statements, però és necessari per entendre l'AssignIfNullOperator que trobareu més endavant...
		[Test]
		public void NullableTypes ()
		{
			int i = 0;
			//i = null; // No podeu fer això.
			int? nullableInt = null; // Però sí podeu fer això!
			int j;  // I què me'n dieu d'això?

			Assert.AreEqual (false, i == null, "Això té el mateix efecte que utilitzar: int i = new int();");
			Assert.AreEqual (true, nullableInt == null, "Es fa estrany veure 'int'? -- com si el programador no n'estigués segur -- doncs ja es tracta d'això home!!!");
			//Assert.AreEqual(FILL_ME_IN, j == null, "Utilitzant variables no inicialitzades, no està permès, encara que sigui per comprovar-ne el valor.");
		}

		[Test]
		public void AssignIfNullOperator ()
		{
			int? nullableInt = null;

			int x = nullableInt ?? 42;

			Assert.AreEqual (42, x, "Esbrinar això és com intentar esbrinar el sentit de la vida...");
		}

		[Test]
		public void IsOperators ()
		{
			bool isAboutMethods = false;
			bool isKoan = false;
			bool isAboutControlStatements = false;

			var myType = this;

			if (myType is K090_AboutMethods)
				isAboutMethods = true;

			if (myType is KoanHelper)
				isKoan = true;

			if (myType is K050_AboutControlStatements)
				isAboutControlStatements = true;

			// Si teniu un ReSharper, no mogueu el ratolí per sobre de les línies ondulades!
			Assert.AreEqual (false, isAboutMethods, "Com a bon booleà, us donarà dues oportunitats de fer aquest correctament.");
            Assert.AreEqual(true, isKoan, "Com a Koan, no és qüestió de fer aquest correctament, sinó d'aprendre.");
			Assert.AreEqual (true, isAboutControlStatements, "Com a qüestió implícita en l'aprenentatge, sempre hi ha d'haver l'opció d'aprendre més.");


		}

		[Test]
		public void WhileStatement ()
		{
			int i = 1;
			int result = 1;
			while (i <= 5) {
				result = result * i;
				i += 1;
			}
			Assert.AreEqual (1 * 1 * 2 * 3 * 4 * 5, result, "Si aquest fos uns quants segles més jove, aquest mètode podria ser un WhilstStatement.");
		}

		[Test]
		public void DoWhileStatement ()
		{
			int i = 1;
			int result = 1;
			do {
				result = result * i;
				i += 1;
			} while (i <= 5);
			Assert.AreEqual (1 * 1 * 2 * 3 * 4 * 5, result, "Lògicament, aquest té més sentit per mi. Un ha de fer feina primer per saber quan s'ha de parar.");
		}

		[Test]
		public void BreakStatement ()
		{
			int i = 1;
			int result = 1;
			while (i < 10) {
				if (i > 5) {
					break;
				}
				result = result * i;
				i += 1;
			}
			Assert.AreEqual (1 * 1 * 2 * 3 * 4 * 5, result, "Awww - Break out! Le Break: c'est chic!");
		}

		[Test]
		public void ContinueStatement ()
		{
			int i = 0;
			var result = 0;
			while (i < 10) {
				i += 1;
				if ((i % 2) == 0) {
					continue;
				}
				result += i;
			}
			Assert.AreEqual (0+1+3+5+7+9, result, "No deixeu que les matemàtiques espantin el vostre Karma... Persevereu!");
		}

		[Test]
		public void ForStatement ()
		{
			var list = new List<string> { "hamburguesa", "amb", "ceba" };
			for (var i = 0; i < list.Count; i++) {
				list [i] = (list [i].ToUpper ());
			}
			CollectionAssert.AreEqual (new[] { "HAMBURGUESA","AMB","CEBA" }, list, "Convertir els valors mentre els parsegem sona perillós, eh?");
		}

		[Test]
		public void ForEachStatement ()
		{
			var list = new List<string> { "bocadillo", "de", "calamares" };
			var finalList = new List<string> ();
			foreach (string item in list) {
				finalList.Add (item.ToUpper ());
			}
			CollectionAssert.AreEqual (new[] { "bocadillo", "de", "calamares" }, list, "Tal i com les dades van, això només necessita una mica de maionesa.");
			CollectionAssert.AreEqual (new[] { "BOCADILLO", "DE", "CALAMARES" }, finalList, "HE DIT, 'TAL I COM LA DADES VAN, AIXÒ NOMÉS NECESSITA UNA MICA DE MAIONESA!'");

			// Per més informació, podeu consultar: http://msdn.microsoft.com/en-us/library/ttw7t8t6.aspx
		}

		[Test]
		public void ModifyingACollectionDuringForEach ()
		{
			var list = new List<string> { "fish", "and", "chips" };
			try {
				foreach (string item in list) {
					list.Add (item.ToUpper ());
				}
			} catch (Exception ex) {
				Assert.AreEqual (typeof(System.InvalidOperationException), ex.GetType (), "Putinejant la col·lecció durant una enumeració foreach no sembla correcte... per no dir prohibit.");
			}
		}

		[Test]
		public void CatchingModificationExceptions ()
		{
			string whoCaughtTheException = "Ningú";

			var list = new List<string> { "fish", "and", "chips" };
			try {
				foreach (string item in list) {
					try {
						list.Add (item.ToUpper ());
					} catch {
						whoCaughtTheException = "Quan hem intentat fer-hi un Add";
					}
				}
			} catch {
				whoCaughtTheException = "Quan hem intentat moure'ns al següent item de la llista";
			}

			Assert.AreEqual ("Quan hem intentat moure'ns al següent item de la llista", whoCaughtTheException, "Si podeu solucionar aquest misteri whodunit, el vostre karma C# us ho agrairà.");
		}
	}
}