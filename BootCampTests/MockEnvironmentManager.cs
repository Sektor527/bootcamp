using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BootCamp;

namespace BootCampTests
{
	enum MockAction
	{
		DoNothing,
		ThrowException
	};

	class MockEnvironmentManager : EnvironmentManager
	{
		internal MockEnvironmentManager(MockAction action)
		{
			_action = action;
		}

		internal override void Run(Game game)
		{
			switch (_action)
			{
				case MockAction.DoNothing: return;
				case MockAction.ThrowException: throw new Win32Exception();
			}
		}

		MockAction _action;
	}
}
