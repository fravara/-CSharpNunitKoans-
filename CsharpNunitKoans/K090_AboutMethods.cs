using System;
using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K090_AboutMethods : KoanHelper
	{
		// Els mètodes _extension ens permeten afegir mètodes a qualsevol classe
		// sense haver de recompilar. Tan sols heu de referenciar el namespace
		// en el qual es troben els mètodes, per a poder utilitzar-los.
		// Com que les classes classe ExtensionMethods i AboutMethods es troben 
        // al namespace, AboutMethods els podrà trobar automàticament.
		[Test]
		public void ExtensionMethodsShowUpInTheCurrentClass ()
		{
			Assert.Equals ("Hello!", this.HelloWorld ());
		}

		[Test]
		public void ExtensionMethodsWithParameters ()
		{
			Assert.Equals ("Hello, Cory", this.SayHello ("Cory"));
		}

		[Test]
		public void ExtensionMethodsWithVariableParameters ()
		{
			Assert.Equals (new string[] {"Cory", "Will", "Corey" }, this.MethodWithVariableArguments ("Cory", "Will", "Corey"));
		}
		// Els mètodes _extension poden extendre qualsevol classe referenciant
        // el nom de la classe que extenen. Per exemple, podem extendre la
        // classe string com farem a continuació...
        [Test]
		public void ExtendingCoreClasses ()
		{
			Assert.Equals ("Hola, Cory", "Cory".SayHi ());
		}
		// Evidentment, qualsevol de les coses que podeu fer amb "parameter",
		// amb els mètodes _extension, també ho podeu fer amb els mètodes locals.
		private string[] LocalMethodWithVariableParameters (params string[] names)
		{
			return names;
		}

		[Test]
		public void LocalMethodsWithVariableParams ()
		{
			Assert.Equals (new string[] { "Hola, Cory", "Cory" }, this.LocalMethodWithVariableParameters ("Cory", "Will", "Corey"));
		}
		// Noteu com hem cridat el mètode amb "this.LocalMethodWithVariableParameters"
		// Això no és necessari pels mètodes locals.
		[Test]
		public void LocalMethodsWithoutExplicitReceiver ()
		{
			Assert.Equals (new string[] { "Hola, Cory", "Cory" }, LocalMethodWithVariableParameters ("Cory", "Will", "Corey"));
		}
		// Però sí és necessari pels mètodes _extension, des del moment 
        // en què necessita una variable instanciada.
		// Això doncs no funcionaria, donant el següent error de compilació:
		// Assert.Equals(FILL_ME_IN, MethodWithVariableArguments("Cory", "Will", "Corey"));
		class InnerSecret
		{
			public static string Key ()
			{
				return "Clau";
			}

			public string Secret ()
			{
				return "Secret";
			}

			protected string SuperSecret ()
			{
				return "Això és secret";
			}

			private string SooperSeekrit ()
			{
				return "Ningú em trobarà!";
			}
		}

		class StateSecret : InnerSecret
		{
			public string InformationLeak ()
			{
				return SuperSecret ();
			}
		}
		// Els mètodes estàtics no requereixen una instància 
        // de l'objecte per a ser cridats.
		[Test]
		public void CallingStaticMethodsWithoutAnInstance ()
		{
			Assert.Equals ("Clau", InnerSecret.Key ());
		}
		// De fet, no els podeu cridar des d'una instància
		// de la classe. Això, per exemple, no compilaria:
		// InnerSecret secret = new InnerSecret();
		// Assert.Equals(FILL_ME_IN, secret.Key());
		[Test]
		public void CallingPublicMethodsOnAnInstance ()
		{
			InnerSecret secret = new InnerSecret ();
			Assert.Equals ("Secret", secret.Secret ());
		}
		// Els mètodes protegits només poden ser cridats per una subclasse
		// Anem a cridar el mètode public anomenat InformationLeak, de la 
        // classe StateSecret, que retorna el valor del mètode protected SuperSecret
		[Test]
		public void CallingProtectedMethodsOnAnInstance ()
		{
			StateSecret secret = new StateSecret ();
			Assert.Equals ("Això és secret", secret.InformationLeak ());
		}
		// Però, no podem cridar els mètodes privats d'InnerSecret
		// ni des d'una instància, ni des d'una subclasse.
        // Simplement no està disponible.
        // D'acord, bé, això no és ben bé del tot cert.
        // La reflexió (reflection, en anglès), et pot aconseguir
        // gairebé qualsevol cosa, però se'ns en va massa aquí...
        [Test]
		public void SubvertPrivateMethods ()
		{
			InnerSecret secret = new InnerSecret ();
			string superSecretMessage = secret.GetType ()
                .GetMethod ("SooperSeekrit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke (secret, null) as string;
			Assert.Equals ("Ningú em trobarà!", superSecretMessage);
		}
		// Fins al moment, sempre hem utilitzat tipus explícits per als retorns.
		// També és possible crear mètodes que facin un canvi dinàmic de tipus, en funció de l'entrada.
		// Aquests són els que són coneguts com a genèrics.
		public static T GiveMeBack<T> (T p1)
		{
			return p1;
		}

		[Test]
		public void CallingGenericMethods ()
		{
			Assert.Equals (typeof(int), GiveMeBack<int> (1).GetType ());

			Assert.Equals ("Hi!", GiveMeBack<string> ("Hi!"));
		}
	}
}
