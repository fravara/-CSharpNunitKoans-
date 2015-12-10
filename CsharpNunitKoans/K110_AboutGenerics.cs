using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K110_AboutGenerics : KoanHelper
	{
		public interface Animal
		{
		}

		public class Dog : Animal
		{
		}

		public class Cat : Animal
		{
		}

		public class Robot
		{
		}

		public class SayHello<TAnimal>
		{
			public string HelloMessage ()
			{
				return string.Format ("Hello {0}", typeof(TAnimal).Name);
			}
		}

		public class SayHelloToAnimalsOnly<TAnimal> where TAnimal : Animal
		{
			public string HelloMessage ()
			{
				return string.Format ("Hello {0}", typeof(TAnimal).Name);
			}
		}

		[Test]
		public void GenericTypeIsACompositionOfTypes ()
		{
			var helloCat = new SayHello<Cat> ();
			Assert.AreEqual (typeof(SayHello<Cat>), helloCat.GetType ());
		}

		[Test]
		public void GenericTypeCanGetTheGenericArgumentName ()
		{
			var helloCat = new SayHello<Cat> ();
			Assert.AreEqual ("Hello Cat", helloCat.HelloMessage ());
		}

		[Test]
		public void GenericTypeCanBeUsedWithAnyTypeEvenPrimitives ()
		{
			var helloInt = new SayHello<int> ();
			Assert.AreEqual ("Hello Int32", helloInt.HelloMessage ());
		}

		[Test]
		public void IsGenericTypeInformation ()
		{
			var helloCat = new SayHello<Cat> ();

			Assert.AreEqual (FILL_ME_IN, helloCat.GetType ().IsGenericType);
			Assert.AreEqual (typeof(FILL_ME_IN), typeof(SayHello<>).IsGenericTypeDefinition);
		}

		[Test]
		public void GetGenericTypeFromUsage ()
		{
			var helloCat = new SayHello<Cat> ();
			var expectedType = typeof(SayHello<>);

			Assert.AreEqual (typeof(SayHello<Cat>), helloCat.GetType ().GetGenericTypeDefinition ());
			Assert.AreEqual (true, typeof(SayHello<>).IsGenericTypeDefinition);
		}

		[Test]
		public void ComposedTypesShareTheSameGenericTypeDefinition ()
		{
			var helloCat = new SayHello<Cat> ();
			var helloDog = new SayHello<Dog> ();

			var genericTypeFromDogToCompare = helloDog.GetType ().GetGenericTypeDefinition ();

			Assert.AreEqual (genericTypeFromDogToCompare, helloCat.GetType ().GetGenericTypeDefinition ());
		}

		[Test]
		public void GenericTypeParameterCanRestrictedToCertainTypes ()
		{
			// Podeu escriure aix�:
			var helloInt = new SayHello<int> ();

			// Per� no podeu escriure aix�, perqu� int no hereta d'Animal:
			// var helloAnimalInt = new SayHelloToAnimalsOnly<int> ();

			// Tamb� us podeu assegurar que:
			//- �s una classe:
			//     public class MyGeneric<T> where T : class
			//- t� un constructor:
			//     public class MyGeneric<T> where T : new()
			//- podeu afegir condicions m�ltiples:
			//     public class MyGeneric<T> where T : Animal, class

			Assert.AreEqual (true, helloInt.GetType().IsGenericType);
		}

		public interface IHasName
		{
			string Name { get; }
		}

		public class Person : IHasName
		{
			public string Name { get; set; }
		}

		public class AnimalOwner<TOwner,TAnimal> 
			where TOwner : IHasName
			where TAnimal : Animal
		{
			TOwner owner;

			public AnimalOwner (TOwner owner)
			{
				this.owner = owner;
			}

			public string Describe ()
			{
				return string.Format ("{0} estima {0} les mascotes", owner.Name, typeof(TAnimal).Name);
			}
		}

		[Test]
		public void CanComposeWithMultipleGenerics ()
		{
			var paul = new AnimalOwner<Person,Cat> (new Person{ Name = "Manu" });
			var expected = new[]{ typeof(Person), typeof(Cat) };

			Assert.AreEqual (2, paul.GetType ().GetGenericArguments ().Length);
			Assert.AreEqual (expected, paul.GetType ().GetGenericArguments ());
		}

		[Test]
		public void WhenEnforceConstrainFromTypeYouUseThatType ()
		{
			var paul = new AnimalOwner<Person,Cat> (new Person{ Name = "Manu" });

			// En el gen�ric, en la descripci� del m�tode, podeu utilitzar la propietat owner.Name
			// perqu� sabem que el propietari HA d'implementar la interf�cie IHasName, que t� la propietat Name.
			Assert.AreEqual (FILL_ME_IN, paul.Describe ());
		}
	}
}
