using System;
using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K090_AboutMethods : KoanHelper
	{
		// Els m�todes _extension ens permeten afegir m�todes a qualsevol classe
		// sense haver de recompilar. Tan sols heu de referenciar el namespace
		// en el qual es troben els m�todes, per a poder utilitzar-los.
		// Com que les classes classe ExtensionMethods i AboutMethods es troben 
        // al namespace, AboutMethods els podr� trobar autom�ticament.
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
		// Els m�todes _extension poden extendre qualsevol classe referenciant
        // el nom de la classe que extenen. Per exemple, podem extendre la
        // classe string com farem a continuaci�...
        [Test]
		public void ExtendingCoreClasses ()
		{
			Assert.Equals ("Hola, Cory", "Cory".SayHi ());
		}
		// Evidentment, qualsevol de les coses que podeu fer amb "parameter",
		// amb els m�todes _extension, tamb� ho podeu fer amb els m�todes locals.
		private string[] LocalMethodWithVariableParameters (params string[] names)
		{
			return names;
		}

		[Test]
		public void LocalMethodsWithVariableParams ()
		{
			Assert.Equals (new string[] { "Hola, Cory", "Cory" }, this.LocalMethodWithVariableParameters ("Cory", "Will", "Corey"));
		}
		// Noteu com hem cridat el m�tode amb "this.LocalMethodWithVariableParameters"
		// Aix� no �s necessari pels m�todes locals.
		[Test]
		public void LocalMethodsWithoutExplicitReceiver ()
		{
			Assert.Equals (new string[] { "Hola, Cory", "Cory" }, LocalMethodWithVariableParameters ("Cory", "Will", "Corey"));
		}
		// Per� s� �s necessari pels m�todes _extension, des del moment 
        // en qu� necessita una variable instanciada.
		// Aix� doncs no funcionaria, donant el seg�ent error de compilaci�:
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
				return "Aix� �s secret";
			}

			private string SooperSeekrit ()
			{
				return "Ning� em trobar�!";
			}
		}

		class StateSecret : InnerSecret
		{
			public string InformationLeak ()
			{
				return SuperSecret ();
			}
		}
		// Els m�todes est�tics no requereixen una inst�ncia 
        // de l'objecte per a ser cridats.
		[Test]
		public void CallingStaticMethodsWithoutAnInstance ()
		{
			Assert.Equals ("Clau", InnerSecret.Key ());
		}
		// De fet, no els podeu cridar des d'una inst�ncia
		// de la classe. Aix�, per exemple, no compilaria:
		// InnerSecret secret = new InnerSecret();
		// Assert.Equals(FILL_ME_IN, secret.Key());
		[Test]
		public void CallingPublicMethodsOnAnInstance ()
		{
			InnerSecret secret = new InnerSecret ();
			Assert.Equals ("Secret", secret.Secret ());
		}
		// Els m�todes protegits nom�s poden ser cridats per una subclasse
		// Anem a cridar el m�tode public anomenat InformationLeak, de la 
        // classe StateSecret, que retorna el valor del m�tode protected SuperSecret
		[Test]
		public void CallingProtectedMethodsOnAnInstance ()
		{
			StateSecret secret = new StateSecret ();
			Assert.Equals ("Aix� �s secret", secret.InformationLeak ());
		}
		// Per�, no podem cridar els m�todes privats d'InnerSecret
		// ni des d'una inst�ncia, ni des d'una subclasse.
        // Simplement no est� disponible.
        // D'acord, b�, aix� no �s ben b� del tot cert.
        // La reflexi� (reflection, en angl�s), et pot aconseguir
        // gaireb� qualsevol cosa, per� se'ns en va massa aqu�...
        [Test]
		public void SubvertPrivateMethods ()
		{
			InnerSecret secret = new InnerSecret ();
			string superSecretMessage = secret.GetType ()
                .GetMethod ("SooperSeekrit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke (secret, null) as string;
			Assert.Equals ("Ning� em trobar�!", superSecretMessage);
		}
		// Fins al moment, sempre hem utilitzat tipus expl�cits per als retorns.
		// Tamb� �s possible crear m�todes que facin un canvi din�mic de tipus, en funci� de l'entrada.
		// Aquests s�n els que s�n coneguts com a gen�rics.
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
