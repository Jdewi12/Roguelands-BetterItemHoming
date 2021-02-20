using GadgetCore.API;
using System;

namespace BetterItemHoming
{
    [Gadget("BetterItemHoming", RequiredOnClients: false)]
    public class BetterItemHoming : Gadget
    {
        public const string MOD_VERSION = "1.1"; // Set this to the version of your mod.
        public const string CONFIG_VERSION = "1.0"; // Increment this whenever you change your mod's config file.

        public static GadgetCore.GadgetLogger logger;

        internal static void Log(string text)
        {
            logger.Log(text);
        }

        protected override void PrePatch()
        {
            logger = base.Logger;
        }

        protected override void Initialize()
        {
            Log("Gadget Initialised");
        }
    }
}