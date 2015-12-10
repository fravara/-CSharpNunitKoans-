using NUnit.Framework;

namespace TheKoans
{
    //falta per fer
	[TestFixture]
	public class K100_AboutInheritance : KoanHelper
	{
		public class Dog
		{
			public string Name { get; set; }

			public Dog (string name)
			{
				Name = name;
			}
			// Perquè un mètode pugui ser sobreescrit per subclasses, ha de ser definit com a virtual.
			public virtual string Bark ()
			{
				return "BUP";
			}
		}

		public class Chihuahua : Dog
		{
			// L'única manera de "construir" un gos és donar-li un nom.
            // Des del moment en què un Chihuahua és un gos, això s'ha de
            // correspondre amb un constructor public/protected.
            // Des del moment en què l'únic constructor public/protected
            // d'un gos requereix un nom, aquest constructor public/protected
            // del qual parlem, també haurà de requerir un nom.
			public Chihuahua (string name)
                : base (name)
			{
			}
			// A no ser que no ho faci! Si és així, haurem de cridar el constructor
            // base amb un nom, en algun punt...
			public Chihuahua ()
                : base ("Ima Chihuahua")
			{
			}
			// Perquè un Chihuahua pugui bordar d'una manera una mica més "light"
			// que no pas un gos normal, quan algú cridi el mètode "Bark", la classe 
            // base ha de ser "virtual" i la derivada haurà d'incloure "override".
			public override string Bark ()
			{
				return "bbbbbrip";
			}
			// Una classe derivada pot tenir altres mètodes o propietats
			// addicionals, sempre i quan necessitem afegir nous comportaments.
			public string Wag ()
			{
				return "Estic content i moc la cua!";
			}
		}

		[Test]
		public void SubclassesHaveTheParentAsAnAncestor ()
		{
			Assert.IsTrue (typeof(Dog).IsAssignableFrom (typeof(Chihuahua)));
		}

		[Test]
		public void AllClassesUltimatelyInheritFromAnObject ()
		{
			Assert.IsTrue (typeof(object).IsAssignableFrom (typeof(Chihuahua)));
		}

		[Test]
		public void SubclassesInheritBehaviorFromParentClass ()
		{
			var chico = new Chihuahua ("Chico");
			Assert.Equals ("Chico", chico.Name);
		}

		[Test]
		public void SubclassesAddNewBehavior ()
		{
			var chico = new Chihuahua ("Chico");
			Assert.Equals ("Feliç!", chico.Wag ());

			// Podem buscar els mètodes públics de la instància 
            // d'un objecte de la següent manera:
			Assert.IsNotNull (chico.GetType ().GetMethod ("Wag"));

			// D'aquesta manera podem constatar que el mètode "Wag"
            // no es troba a gos. Proveu a cridar "Wag" d'un gos... 
			var dog = new Dog ("Fluffy");
			Assert.IsNull (dog.GetType ().GetMethod ("Wag"));
		}

		[Test]
		public void SubclassesCanModifyExistingBehavior ()
		{
			var chico = new Chihuahua ("Chico");
			Assert.Equals ("yip", chico.Bark ());

			// Noteu que encara que fem un cast de l'objecte cap a gos
			// encara obtenim el comportament de Chihuahua's behavior. 
            // Ja ho diuen que tenen caràcter els petits...
			Dog dog = chico as Dog;
			Assert.Equals ("yip", dog.Bark ());

			var fido = new Dog ("Fido");
			Assert.Equals ("WOOF", fido.Bark ());
		}

		public class ReallyYippyChihuahua : Chihuahua
		{
			public ReallyYippyChihuahua (string name) : base (name)
			{
			}
			// És possible redefinir el comportament de classes on 
            // els mètodes no han estat marcats amb "virtual", però 
            // aneu alerta perquè us passar factura. Per exemple:
			public new string Wag ()
			{
				return "BUP BUP BUP!!!";
			}
		}

		[Test]
		public void SubclassesCanRedefineBehaviorThatIsNotVirtual ()
		{
			ReallyYippyChihuahua suzie = new ReallyYippyChihuahua ("Suzie");
			Assert.Equals ("BUP BUP BUP!!!", suzie.Wag ());
		}

		[Test]
		public void NewingAMethodDoesNotChangeTheBaseBehavior ()
		{
			// Això és crucial que ho entengueu. A la càpsula 6 heu vist
            // que el mètode "Wag" feia el que havíem definit a la nostra
            // classe... Però que passa quan fem això?
			Chihuahua bennie = new ReallyYippyChihuahua ("Bennie");
			Assert.Equals ("Feliç!", bennie.Wag ());

			// Així és. El comportament de l'objecte és dependent només
			// de qui preteneu ser (a diferència de quan sobreescrivim un
			// mètode virtual). Recordeu això en el vostre camí a la il·luminació.

		}

		public class BullDog : Dog
		{
			public BullDog (string name) : base (name)
			{
			}

			public override string Bark ()
			{
				return base.Bark () + ", GROWL";
			}
		}

		[Test]
		public void SubclassesCanInvokeParentBehaviorUsingBase ()
		{
			var ralph = new BullDog ("Ralph");
			Assert.Equals ("WOOF, GROWL", ralph.Bark ());
		}

		public class GreatDane : Dog
		{
			public GreatDane (string name) : base (name)
			{
			}

			public string Growl ()
			{
				return base.Bark () + ", GROWL";
			}
		}

		[Test]
		public void YouCanCallBaseEvenFromOtherMethods ()
		{
			var george = new BullDog ("George");
			Assert.Equals ("WOOF, GROWL", george.Bark ());
		}
	}
}
