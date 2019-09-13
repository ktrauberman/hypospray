using System;

namespace Hypospray
{
	public class InstanceRegistration : IRegistration
	{
		public object Instance { get; }

		public InstanceRegistration(Type type, object instance, ILifecycle lifecycle)
		{
			RegisteredType = type;
			Instance = instance;
			Lifecycle = lifecycle;
		}

		public ILifecycle Lifecycle { get; }
		public Type RegisteredType { get; }
	}
}