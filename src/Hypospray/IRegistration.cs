using System;
using System.Net.NetworkInformation;

namespace Hypospray
{
	public interface IRegistration
	{
		ILifecycle Lifecycle { get; }
		Type RegisteredType { get; }
	}
}