using System;

namespace Hypospray
{
	public class TypeRegistration : IRegistration
	{
		public TypeRegistration(Type type, ILifecycle lifecycle)
		{
			RegisteredType = type;
			Lifecycle = lifecycle;
		}

		public ILifecycle Lifecycle { get; }
		public Type RegisteredType { get; }
	}
}