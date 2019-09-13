using System;

namespace Hypospray
{
	public class TransientLifecycle : ILifecycle
	{
		public object ResolveInstance(Func<Type, object> resolver) => null;
	}
}