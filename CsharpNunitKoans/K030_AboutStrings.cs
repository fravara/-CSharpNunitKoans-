using System;
using System.Text;
using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K030_AboutStrings : KoanHelper
	{
		//Nota: Aquest és un dels koans més llargs, i potser també,
		//un dels més importants. El comportament d'String en .NET
        //no és sempre el que potser esperaríeu, especialment quan
		//es fan concatenacions i noves línies, i aquesta és una de
        //les principals causes de fugues de memòria en apps .NET
		[Test]
		public void DoubleQuotedStringsAreStrings ()
		{
			var str = "Hola, Món";
            Assert.AreEqual(typeof(string), str.GetType(), "Què li diu Arial a Serif? - Perdona, no ets el meu Tipus.");
            // Original Assert.AreEqual (typeof(FILL_ME_IN), str.GetType (), "Què li diu Arial a Serif? - Perdona, no ets el meu Tipus.");
		}

		[Test]
		public void SingleQuotedStringsAreNotStrings ()
		{
			// Noteu l'ús de les cometes simples a continuació. No són cometes dobles, que són les usades per strings habitualment.
			var str = 'H';
            Assert.AreEqual(typeof(char), str.GetType(), "Un simple arbre no fa un bosc.");
            // Original Assert.AreEqual (typeof(FILL_ME_IN), str.GetType (), "Un simple arbre no fa un bosc.");
		}

		[Test]
		public void CreateAStringWhichContainsDoubleQuotes ()
		{
			var str = "Hola, \"Món\"";
            Assert.AreEqual(11, str.Length, "Si la longitud és errònia i esteu atrapats, considereu maneres d'escapar...");
            // Original Assert.AreEqual (FILL_ME_IN, str.Length, "Si la longitud és errònia i esteu atrapats, considereu maneres d'escapar...");
		}

		[Test]
		public void AnotherWayToCreateAStringWhichContainsDoubleQuotes ()
		{
			//El símbol @ crea el que s'anomena 'verbatim string literal'. 
			//Aquí teniu una cosa que podeu fer amb això:
			var str = @"Hola, ""Món""";

            Assert.AreEqual(11, str.Length, "Tantes cometes, tan pocs caràcters...");
            // Original Assert.AreEqual (FILL_ME_IN, str.Length, "Tantes cometes, tan pocs caràcters...");
		}

		[Test]
		public void VerbatimStringsCanHandleFlexibleQuoting ()
		{
			var strA = @"Verbatim Strings poden tractar ambdós caràcters ' i "" ";
			var strB = "Verbatim Strings poden tractar ambdós caràcters ' i \" ";
			Assert.AreEqual (strB.Equals (strA), strA.Equals (strB), "Ja us hem dit tres vegades que els Verbatim Strings poden tractar les cometes simples i dobles... Bé, suposo que això és la que fa quatre.");
		}

		[Test]
		public void VerbatimStringsCanHandleMultipleLinesToo ()
		{
			//Un literal verbatim string és aquell en què no es processen
			//seqüències d'escapament. Conté exactament els caràcters que
			//s'hi entren, i pot abarcar múltiples línies. 
            var verbatimString = @"Jo
sóc una
línia trencada";
			Assert.AreEqual (25, verbatimString.Length, "És una nova línia un o dos caràcters?");
			//El que creeu pel literal string haurà d'escapar els caràcters de nova línia.
			var literalString = "Jo\nsóc una\nlínia trencada";
            Assert.AreEqual (literalString, verbatimString, "No seria fer trampa si miréssiu a sota. No, no als vostres peus; al mètode que teniu a continuació.");
		}

		[Test]
		public void ACrossPlatformWayToHandleLineEndings ()
		{
            //Tenint en compte que els finals de línia són diferents a cada
            //plataforma (exemple: \r\n a Windows, \n a Linux), no hauríeu de
            //posar mai seqüències d'escapament "hardcoded".

            //Una manera millor, i també més neta, és utilitzar una propietat
            //de la classe Environment. 

            //Modifiqueu la línia que teniu a continuació:
            var environmentNewLine = Environment.NewLine;
            // Original var environmentNewLine = FILL_ME_IN;

            //(Tractarem la concatenació i millors maneres de fer-ho en breu...)
            const string verbatimString = @"Jo
sóc una
línia trencada";
			var literalString = "Jo" + environmentNewLine + "sóc una" + environmentNewLine + "línia trencada";
			Assert.AreEqual (literalString, verbatimString, "És una nova línia només un o dos caràcters?");
		}

		[Test]
		public void PlusWillConcatenateTwoStrings ()
		{
			var str = "Hola, " + "Món";
            Assert.AreEqual("Hola, Món", str, "Una frase tan simple, i sovint es necessita tant de codi per fer-la aparèixer...");
            // Original Assert.AreEqual (FILL_ME_IN, str, "Una frase tan simple, i sovint es necessita tant de codi per fer-la aparèixer...");
		}

		[Test]
		public void PlusConcatenationWillNotModifyOriginalStrings ()
		{
			var strA = "Hola, ";
			var strB = "Món";
			var fullString = strA + strB;
            Assert.AreEqual("Hola, ", strA, "El nom del mètode suggereix que no hauria de ser modificat, però només hi ha una manera d'esbrinar-ho...");
            Assert.AreEqual("Món", strB, "Bé, suposo que verificar ambdues variables és tècnicament una altra manera...");
            Assert.AreEqual("Hola, Món",fullString, "Imagino que li han anomenat concatenació per fer-ho sonar tan complexe com realment és.");
            /* Original
            Assert.AreEqual(FILL_ME_IN, strA, "El nom del mètode suggereix que no hauria de ser modificat, però només hi ha una manera d'esbrinar-ho...");
            Assert.AreEqual(FILL_ME_IN, strB, "Bé, suposo que verificar ambdues variables és tècnicament una altra manera...");
            Assert.AreEqual(FILL_ME_IN, fullString, "Imagino que li han anomenat concatenació per fer-ho sonar tan complexe com realment és.");
            */
        }

		[Test]
		public void PlusEqualsWillModifyTheTargetString ()
		{
			var strA = "Hola, ";
			var strB = "Món";
			strA += strB;
            Assert.AreEqual("Hola, Món", strA, "Ets un lloro ara?");
            Assert.AreEqual("Món", strB, "Per què aturar-se al 'Món'? Per què no 'Univers'? Per què no 'Zoidberg'?");
            /* Oginal
            Assert.AreEqual (FILL_ME_IN, strA, "Ets un lloro ara?");
			Assert.AreEqual (FILL_ME_IN, strB, "Per què aturar-se al 'Món'? Per què no 'Univers'? Per què no 'Zoidberg'?");
	        */	
        }

		[Test]
		public void StringsAreReallyImmutable ()
		{
			//'Immutable' significa que el valor no canvia.
            //Els Strings, numbers i el valor null són de debò immutables.

			//Concatenar strings està bé i tal, però si creieu que 
            //esteu modificant l'string original, aleshores aneu errats!
			
			var strA = "Hola, ";
			var originalString = strA;
			const string strB = "Món";
			strA += strB;
            Assert.AreEqual("Hola, ", originalString, "'Sigues tu mateix. El món adora l'original.' - Jacques Cocteau");
            // Original Assert.AreEqual (FILL_ME_IN, originalString, "'Sigues tu mateix. El món adora l'original.' - Jacques Cocteau");

			//Què acaba de passar? Bé, la concatenació d'strings de fet
			//agafa strA i strB i crea un nou string en memòria, que té
            //el nou valor. No modifica l'string original!
            //Aquest és un punt molt important. Per exemple, si feu ús
            //d'aquest tipus de concatenació en un loop 'llarg', 
            //utilitzareu molta memòria, ja que l'string original estarà
            //esperant en memòria fins que el garbage collector el reculli.
            //Anem a fer un cop d'ull a millors maneres de lidiar amb 
            //un número elevat de concatenacions.
		}

		[Test]
		public void ABetterWayToConcatenateLotsOfStrings ()
		{
			//Com mostra la càpsula de C# anterior, concatenar molts strings
			//pot ser una mala idea. Si necessiteu fer-ho, podeu fer-ho així:
			var strBuilder = new StringBuilder ();
			for (int i = 0; i < 100; i++) {
				strBuilder.Append ("a");
			}
			var str = strBuilder.ToString ();
            Assert.AreEqual(100, str.Length, "Quan en Charles M. Schulz va dibuixar els crits d'en Charlie Brown (AAAARGH!) - es devia preguntar mai si els escrivia amb faltes d'ortografia?");
            // Original Assert.AreEqual (FILL_ME_IN, str.Length, "Quan en Charles M. Schulz va dibuixar els crits d'en Charlie Brown (AAAARGH!) - es devia preguntar mai si els escrivia amb faltes d'ortografia?");

			//La clau en aquest cas és que heu de crear un objecte StringBuilder, 
			//que admet una sobrecàrrega major que la d'un string.
            //La regla del polze us diu doncs que, si heu de concatenar menys de 
            //5 string, += està bé, però si en necessiteu més, useu StringBuilder!
		}

		[Test]
		public void LiteralStringsInterpretsEscapeCharacters ()
		{
			var str = "\n";
            Assert.AreEqual(1, str.Length, "Això és com 'el so d'una mà aplaudint...'");
            // Original Assert.AreEqual (FILL_ME_IN, str.Length, "Això és com 'el so d'una mà aplaudint...'");
		}

		[Test]
		public void VerbatimStringsDoNotInterpretEscapeCharacters ()
		{
			var str = @"\n";
            Assert.AreEqual(2, str.Length, "'L'art de debò selecciona i parafraseja, però poques vegades dóna una traducció mot a mot.' - Thomas Bailey Aldrich");
            // Original Assert.AreEqual (FILL_ME_IN, str.Length, "'L'art de debò selecciona i parafraseja, però poques vegades dóna una traducció mot a mot.' - Thomas Bailey Aldrich");
		}

		[Test]
		public void VerbatimStringsStillDoNotInterpretEscapeCharacters ()
		{
			var str = @"\\\";
            Assert.AreEqual(3, str.Length, "Tal vegada ens convindria fer un cop d'ull al significat de mot a mot, eh?");
            // Original Assert.AreEqual (FILL_ME_IN, str.Length, "Tal vegada ens convindria fer un cop d'ull al significat de mot a mot, eh?");
		}

		[Test]
		public void YouDoNotNeedConcatenationToInsertVariablesInAString ()
		{
			var mon = "Món";
			var str = String.Format ("Hola, {0}", mon);
            Assert.AreEqual("Hola, Món", str, "Habitualment això és la primera cosa que fan els programadors...");
            // Original Assert.AreEqual (FILL_ME_IN, str, "Habitualment això és la primera cosa que fan els programadors...");
		}

		[Test]
		public void AnyExpressionCanBeUsedInFormatString ()
		{
			var str = String.Format ("L'arrel quadrada de 9 és {0}", Math.Sqrt (9));
            Assert.AreEqual("L'arrel quadrada de 9 és 3", str, ".NET convertirà el valor a un string, però vosaltres encara haureu de fer les matemàtiques.");
            // Original Assert.AreEqual(FILL_ME_IN, str, ".NET convertirà el valor a un string, però vosaltres encara haureu de fer les matemàtiques.");
        }

        [Test]
		public void YouCanGetASubstringFromAString ()
		{
			var str = "Cansalada, enciam i pebrots";
            Assert.AreEqual("pebrots", str.Substring(20), "En una versió d'aquest mètode sobrecarregat, només haureu d'especificar on començar.");
            Assert.AreEqual("salada", str.Substring(3, 6), "A l'altra, també haureu d'especificar fins on voleu arribar");
            /*
            Assert.AreEqual(FILL_ME_IN, str.Substring(20), "En una versió d'aquest mètode sobrecarregat, només haureu d'especificar on començar.");
            Assert.AreEqual(FILL_ME_IN, str.Substring(3, 6), "A l'altra, també haureu d'especificar fins on voleu arribar");
            */
        }

        [Test]
		public void YouCanGetASingleCharacterFromAString ()
		{
            var str = "Cansa1ada, enciam i pebrots";
			//Recordeu, ja que estem treballant amb caràcters, que heu d'usar cometes simples per delimitar la vostra opció.
			Assert.AreEqual ('1', str[5], "els arrays indexats a 0 sovint poden resultar confusos... compteu el 0, o no?");
            // Original Assert.AreEqual(FILL_ME_IN, str[5], "els arrays indexats a 0 sovint poden resultar confusos... compteu el 0, o no?");

        }

        [Test]
		public void SingleCharactersAreRepresentedByIntegers ()
		{
			Assert.AreEqual (97, 'a');
			Assert.AreEqual (98, 'b');
            Assert.AreEqual(true, 'b' == ('a' + 1), "Després podeu anar a donar les gràcies al vostre professor d'àlgebra...");
            // Original Assert.AreEqual (FILL_ME_IN, 'b' == ('a' + 1), "Després podeu anar a donar les gràcies al vostre professor d'àlgebra...");
		}

		[Test]
		public void CanCheckEqualityOfStringsWithAssertButStringCollectionsRequireSomethingDifferent ()
		{
			var strArray1 = new[] { "alpha", "beta", "gamma" };
			var strArray2 = new[] { "alpha", "beta", "gamma" };
            Assert.AreEqual(strArray1, strArray2, "Tant de bo hi hagués una altra classe, diferent d'Assert, només per a col·leccions...");
            // Original Assert.AreEqual (FILL_ME_IN, strArray2, "Tant de bo hi hagués una altra classe, diferent d'Assert, només per a col·leccions...");
		}

		[Test]
		public void StringsCanBeSplit ()
		{
			var str = "Salsitxa Ou Formatge";
			string[] words = str.Split ();
            //Atencio! El que buscàvem abans existeix!
            CollectionAssert.AreEqual(new[] { "Salsitxa","Ou","Formatge" }, words, "Identifica tots els elements dins l'string 'words'. Oh! I espavil... Que m'està entrant gana!");
            // Original CollectionAssert.AreEqual (new[] { FILL_ME_IN }, words, "Identifica tots els elements dins l'string 'words'. Oh! I espavil... Que m'està entrant gana!");
		}

		[Test]
		public void StringsCanBeSplitUsingCharacters ()
		{
			var str = "Peter:Flopsy:Mopsy:Cottontail";
			string[] words = str.Split (':');
            CollectionAssert.AreEqual(new[] { "Peter", "Flopsy", "Mopsy","Cottontail" }, words, "Només havia de trobar una manera per a separar les llebres.");
            // Original CollectionAssert.AreEqual (new[] { FILL_ME_IN }, words, "Només havia de trobar una manera per a separar les llebres.");
		}

		[Test]
		public void StringsCanBeSplitUsingRegularExpressions ()
		{
			var str = "the:rain:in:spain";
			var regex = new System.Text.RegularExpressions.Regex (":");
			string[] words = regex.Split (str);
            CollectionAssert.AreEqual(new[] { "the", "rain", "in", "spain" }, words, "La manera en què Eliza Doolittle va parlar per primer cop a 'My Fair Lady' trencaria el karma de qualsevol.");
            //  Original CollectionAssert.AreEqual(new[] { FILL_ME_IN }, words, "La manera en què Eliza Doolittle va parlar per primer cop a 'My Fair Lady' trencaria el karma de qualsevol.");

            //El tractament de les expressions regulars queda fora de l'àmbit d'aquesta pràctica/tutorial.
            //El llibre "Mastering Regular Expressions" per això, és una bona referència per tenir a la
            //vostra biblioteca de llibres de programació:
            //http://www.amazon.com/Mastering-Regular-Expressions-Jeffrey-Friedl/dp/0596528124
        }
        // Començar RJG afegint més sobre strings
        [Test]
		public void UnderstandingThePerformanceOfCheckingForEmptyStrings ()
		{
			var str = "";
			//A continuació, 6 comprovacions per un string buit.
			Assert.IsTrue (str.Equals (""));                    // 1
			Assert.IsTrue (string.Equals (str, ""));            // 2
			Assert.IsTrue (str == "");                          // 3
			Assert.IsTrue (string.IsNullOrWhiteSpace (str));    // 4 (introduït amb .NET 4.0)
			Assert.IsTrue (string.IsNullOrEmpty (str));         // 5
			Assert.IsTrue (str.Length == 0);                    // 6
			//Aquests, de fet, estan ordenats per ordre d'eficiència, de pitjor a millor.
            //És a dir, sempre que tingueu opció de fer == serà millor, i comparar tipus numèrics.
			//Si tinguéssiu FxCop instal·lat, us avisaria d'eficiència pobra, en funció de la implementació.
			//Per a més informació sobre FxCop: https://www.microsoft.com/en-us/download/details.aspx?id=6544

			//Potser hi ha una manera millor de compartir aquesta informació, 
            //però proveu a afegir True a sota, just després de llegir això.
			Assert.AreEqual (true, str.Equals(""), "A vegades cal aturar-se i agafar-se una estona per sentir l'eficiència...");
			// Original Assert.AreEqual (true, FILL_ME_IN, "A vegades cal aturar-se i agafar-se una estona per sentir l'eficiència...");
		}

		[Test]
		public void CaseMattersWhenComparingStrings ()
		{
			var strProper = "United States of America";
			var strUpper = "UNITED STATES OF AMERICA";
            Assert.AreEqual(strUpper.Equals(strProper), strProper.Equals(strUpper), "No és el que dius... És el com ho dius!");
            // Original Assert.AreEqual (FILL_ME_IN, strProper.Equals (strUpper), "No és el que dius... És el com ho dius!");
		}

		[Test]
		public void ConvertTheCaseToMakeComparisonSoThatCaseDoesNotMatter ()
		{
			var strProper = "United States of America";
			var strUpper = "UNITED STATES OF AMERICA";

			// Els Strings tenen mètodes built-in que permeten fer conversions.
			// Feu una conversió de strProper per fer-lo coincidir amb strUpper.
			Assert.AreEqual (strProper.ToUpper(), strUpper, "How can you argue with something in ALL CAPS? Someone took the time to press the shift key..");
            // Original Assert.AreEqual(FILL_ME_IN, strUpper, "How can you argue with something in ALL CAPS? Someone took the time to press the shift key..");

        }

        [Test]
		public void OrUseThisToCompareStringsUsingCaseInsensitivity ()
		{
			var strProper = "United States of America";
			var strUpper = "UNITED STATES OF AMERICA";

			Assert.AreEqual (String.Compare (strProper, strUpper, StringComparison.OrdinalIgnoreCase), 0, "Sis d'un, mitja dotzena d'un altre.");
            // Original Assert.AreEqual(String.Compare(strProper, strUpper, StringComparison.OrdinalIgnoreCase), FILL_ME_IN, "Sis d'un, mitja dotzena d'un altre.");

            // Hi ha més maneres de comparar Strings també... (.CompareTo, etc.), però això 
            // si no us sembla malament ho treballarem a la secció "AboutObjects". :D
        }

        [Test]
		public void InsertingStrings ()
		{
			var str = "John Adams";
			Assert.AreEqual (str.Insert (5, "Quincy "), "John Quincy Adams", "Em pregunto si devien dir 'Q' al 6è president dels Estats Units, igual que al darrer Bush també li deien 'W'...");
            // Original Assert.AreEqual(str.Insert(5, "Quincy "), FILL_ME_IN, "Em pregunto si devien dir 'Q' al 6è president dels Estats Units, igual que al darrer Bush també li deien 'W'...");

            // Fixeu-vos que Insert() no modifica el valor de l'string immutable str
            // Assert.AreEqual(str, "John Quincy Adams"); // Això no passaria
            // Però continueu més avall...
        }

        [Test]
		public void CallingInsertDoesNotAssignUnlessYouExplicitlySaveTheResult ()
		{
			var str = "John Adams";
			str = str.Insert (5, "Quincy ");
            Assert.AreEqual("John Quincy Adams", str, "'Proporcionar els mitjans per adquirir els coneixements és ... el major benefici que pot ser conferit a la humanitat' - John Quincy Adams");
            // Original Assert.AreEqual(FILL_ME_IN, str, "'Proporcionar els mitjans per adquirir els coneixements és ... el major benefici que pot ser conferit a la humanitat' - John Quincy Adams");

        }

        [Test]
		public void RemovingStrings ()
		{
			var str = "OholeNE";
			str = str.Remove (1, 4);
			Assert.AreEqual (str, "ONE", "Connectar amb TigerWoords us podria ajudar a identificar l'string resultant d'aquest 'hole' in 'ONE'...");
            // Original Assert.AreEqual(str, FILL_ME_IN, "Connectar amb TigerWoords us podria ajudar a identificar l'string resultant d'aquest 'hole' in 'ONE'...");

        }

        [Test]
		public void UnderstandingTrim ()
		{
			var str = "     Tot el què realment importa    ";
			Assert.AreEqual (str.Trim (), "Tot el què realment importa", "Sí, són només uns pobres exercicis, però després d'aprendre múltiples llenguatges de programació, a vegades encara haureu de fer un double check per assegurar-vos que fan el que espereu.");
            // Original Assert.AreEqual(str.Trim(), FILL_ME_IN, "Sí, són només uns pobres exercicis, però després d'aprendre múltiples llenguatges de programació, a vegades encara haureu de fer un double check per assegurar-vos que fan el que espereu.");

        }

        [Test]
		public void UnderstandingTrimStart ()
		{
            var str = "     Tot el què realment importa    ";
			Assert.AreEqual ("Tot el què realment importa    ", str.TrimStart (), "Trobo que aquesta és més fàcil d'entendre vista després de les altres funcions de Trim.");
            // Original Assert.AreEqual(FILL_ME_IN, str.TrimStart(), "Trobo que aquesta és més fàcil d'entendre vista després de les altres funcions de Trim.");

        }

        [Test]
		public void UnderstandingTrimEnd ()
		{
            var str = "     Tot el què realment importa    ";
			Assert.AreEqual ("     Tot el què realment importa", str.TrimEnd (), "Crec que Dan Brown va fer això a molts dels seus llibres - crea el suspens, dóna profunditat als personatges i la trama, i finalment tot-es-resol-en-unes-poques-pàgines-i-fi.");
		}
	}
}
