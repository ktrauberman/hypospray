using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hypospray
{
	public class Container
	{
		private readonly List<IRegistration> _registrations = new List<IRegistration>();

		public void RegisterTransient<TType>() => _registrations.Add(new TypeRegistration(typeof(TType), new TransientLifecycle()));

		public void RegisterTransient<TType>(TType instance)
			=> _registrations.Add(new InstanceRegistration(typeof(TType), instance, new SingletonLifecycle()));

		public void RegisterSingleton<TType>() => _registrations.Add(new TypeRegistration(typeof(TType), new SingletonLifecycle()));

		public void RegisterSingleton<TType>(TType instance)
			=> _registrations.Add(new InstanceRegistration(typeof(TType), instance, new SingletonLifecycle()));

		public TType Resolve<TType>() => (TType) Resolve(typeof(TType));

		public object Resolve(Type type)
		{
			// TODO assumes only one registration exists, allow multiple registrations
			// TODO handle lifecycles
			// Get the registrations for this type
			var registration = _registrations.Where(reg => reg.RegisteredType == type);
			
			switch (registration)
			{
				case InstanceRegistration instanceReg: return instanceReg.Instance;

				case TypeRegistration typeReg:
					// Get the public constructor with the most parameters that are all registered
					var constructor = type.GetConstructors()
					   .Where(ctor => ctor.IsPublic)
					   .Where(
							ctor => ctor.GetParameters()
							   .All(param => IsRegistered(param.ParameterType))
						)
					   .Aggregate(ConstructorWithMostParameters);

					if (constructor == null) throw new Exception($"Could Not Resolve {type.Name}.");

					// Resolve the parameter instances and create an instance of the type
					var parameterInstances = constructor.GetParameters().Select(param => Resolve(param.ParameterType));
					return Activator.CreateInstance(type, parameterInstances);

				default: throw new Exception("Type Not Registered");
			}
		}

		private static ConstructorInfo ConstructorWithMostParameters(ConstructorInfo c1, ConstructorInfo c2) => c1.GetParameters().Length > c2.GetParameters().Length ? c1 : c2;

		private bool IsRegistered(Type type) => _registrations.Any(reg => reg.RegisteredType == type);
	}
}