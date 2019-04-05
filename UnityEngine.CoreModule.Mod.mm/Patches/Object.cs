#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

using MonoMod;
using System;
using System.IO;

namespace UnityEngine {
    public class patch_Object : UnityEngine.Object {

        public static extern void orig_cctor();
        [MonoModConstructor]
        public static void cctor() {
            orig_cctor();
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Test.txt"), "Hi Zandra!");
        }

    }
}
