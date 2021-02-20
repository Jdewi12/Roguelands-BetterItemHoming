using UnityEngine;
using HarmonyLib;
using GadgetCore.API;

namespace BetterItemHoming.Patches
{
    [HarmonyPatch(typeof(ItemScript))]
    [HarmonyPatch("OnTriggerEnter")]
    [HarmonyGadget("BetterItemHoming")]
    public static class Patch_ItemScript_OnTriggerEnter
    {
        [HarmonyPrefix]
        public static bool Prefix(Collider c, ref GameObject ___target, bool ___localItem, ItemScript __instance)
        {
            if (___target == null)
            {
                if (___localItem)
                {
                    return true; // run the original method
                }
                else if (Network.isServer && (c.gameObject.layer == 8 || c.gameObject.layer == 26)) // layer 8 is player, layer 26 is otherplayer
                {
                    ___target = Nearest(__instance.transform.position);
                }
            }

            return false; // don't run original method
        }

        // returns the nearest player (by distance squared for performance)
        public static GameObject Nearest(Vector2 pos)
        {
            GameObject target = null;
            var scripts = GameObject.FindObjectsOfType<PlayerScript>();
            float closestSqr = Mathf.Infinity;
            foreach (PlayerScript script in scripts)
            {
                var player = script.gameObject;
                float sqrDist = Mathf.Pow(player.transform.position.x - pos.x, 2) + Mathf.Pow(player.transform.position.y - pos.y, 2);
                if (sqrDist < closestSqr)
                {
                    closestSqr = sqrDist;
                    target = player;
                }
            }
            return target;
        }
    }
}