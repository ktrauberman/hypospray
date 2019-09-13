using System;

namespace Hypospray
{
	public interface ILifecycle
	{
		object ResolveInstance(Func<Type, object> resolver);
	}
}