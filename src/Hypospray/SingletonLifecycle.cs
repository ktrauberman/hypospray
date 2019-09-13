using System;

namespace Hypospray
{
	public class SingletonLifecycle : ILifecycle
	{
		public object ResolveInstance(Func<Type, object> resolver) => null;
	}
}