using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using engenious;
using engenious.Graphics;

namespace Sample
{
    public static class MainClass
	{
        [STAThread()]
		public static void Main (string[] args)
        {
   //          var s = new smth();
   
			using(var test = new TestGame ())
				test.Run ();
		}
	}
}
