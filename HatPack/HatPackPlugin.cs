﻿using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
//code examples borrowed from townofus creators if it aint broke don't fix it LOL
namespace HatPack
{
    [BepInPlugin(Id , "HatPack", Version)]
    [BepInProcess("Among Us.exe")]
    public class HatPackPlugin : BasePlugin
    {
        public const string Version = "3.1.0";

        public const string Id = "hats.pack";

        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load()
        {
            Assets.LoadAssetBundle();
            Harmony.PatchAll();
        }

        public class HatExtension
        {
            public string condition { get; set; }
            public Sprite FlipImage { get; set; }
            public Sprite BackFlipImage { get; set; }

            public bool isUnlocked()
            {
                if (condition == null || condition.ToLower() == "none")
                    return true;
                return false;
            }
        }

        protected internal struct AuthorData
        {
            public string AuthorName;
            public string HatName;
            public string FloorHatName;
            public string ClimbHatName;
            public string LeftImageName;
            public bool bounce;
            public bool altShader;
        }
        //Be sure to spell everything right or it will not load all hats after spelling error
        //Must be prefab name not name of asset for hatname
        private static List<AuthorData> authorDatas = new List<AuthorData>()
        {
            new AuthorData {AuthorName = "Berg", HatName = "birdhead", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "blackbirdhead", bounce = false },
            new AuthorData {AuthorName = "Angel", HatName = "jess", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "murderghost" , bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "odaidenhat", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "Omega", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "reapercostume",FloorHatName ="reaperdead",ClimbHatName = "reaperclimb",LeftImageName = "reapercostumeleft", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "reapermask",FloorHatName ="reaperdead",ClimbHatName = "reaperclimb",LeftImageName = "reapermaskleft", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "viking", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "vikingbeer", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "pineapple", bounce = false},
            //new AuthorData {AuthorName = "Berg", HatName = "willhair", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "vader",FloorHatName ="vaderdead",ClimbHatName = "vaderclimb", bounce = false},
            new AuthorData {AuthorName = "Berg", HatName = "unclesam",FloorHatName ="unclesamdead", bounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Bunpix", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Cadbury", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "CatEars", bounce = true},
            new AuthorData {AuthorName = "Angel", HatName = "dirtybirb", bounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "DJ", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "EnbyScarf", bounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Espeon", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Gwendolyn", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Jester", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "PizzaRod", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Sombra", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Sprxk", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Swole", bounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "TransScarf", bounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Unicorn", bounce = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Wings", bounce = false},
            new AuthorData {AuthorName = "Paradox", HatName = "Dino", bounce = true, altShader=true},
            new AuthorData {AuthorName = "Berg", HatName = "Ugg", bounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "SilverSylveon", bounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "DownyCrake", bounce = false},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Ram", bounce = false , altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "Kitsune", bounce = true, altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "GlitchedSwole", bounce = true, altShader = true},
            new AuthorData {AuthorName = "NightRaiderTea", HatName = "TigerShark", bounce = true, altShader = true}
        };

        internal static Dictionary<uint, AuthorData> IdToData = new Dictionary<uint, AuthorData>();

        private static bool _customHatsLoaded = false;
        [HarmonyPatch(typeof(HatManager), nameof(HatManager.GetHatById))]
        public static class AddCustomHats
        {
            public static void Prefix(PlayerControl __instance)
            {
                if (!_customHatsLoaded)
                {
                    var allHats = HatManager.Instance.AllHats;

                    foreach (var data in authorDatas)
                    {
                        HatID++;

                        if (data.FloorHatName != null && data.ClimbHatName != null && data.LeftImageName != null)
                        {
                            System.Console.WriteLine($"Adding {data.HatName} and associated floor/climb hats/left image");
                            if (data.bounce)
                            {
                                if (data.altShader == true)
                                {
                                    System.Console.WriteLine($"Adding {data.HatName} with Alt shaders and bounce");
                                    allHats.Add(CreateHat(GetSprite(data.HatName), GetSprite(data.ClimbHatName), GetSprite(data.FloorHatName), null, true, true));
                                }
                                else
                                {
                                    System.Console.WriteLine($"Adding {data.HatName} with bounce enabled");
                                    allHats.Add(CreateHat(GetSprite(data.HatName), GetSprite(data.ClimbHatName), GetSprite(data.FloorHatName),GetSprite(data.LeftImageName), true, false));
                                }
                            }
                            else
                            {
                                System.Console.WriteLine($"Adding {data.HatName} with bounce disabled");
                                allHats.Add(CreateHat(GetSprite(data.HatName), GetSprite(data.ClimbHatName), GetSprite(data.FloorHatName), GetSprite(data.LeftImageName)));
                            }

                        }
                        else
                        {
                            if (data.altShader == true)
                            {
                                System.Console.WriteLine($"Adding {data.HatName} with Alt shaders");
                                allHats.Add(CreateHat(GetSprite(data.HatName), null, null,null, false, true));
                            }
                            else
                            {
                                System.Console.WriteLine($"Adding {data.HatName}");
                                allHats.Add(CreateHat(GetSprite(data.HatName)));
                            }
                        }
                        IdToData.Add((uint)HatManager.Instance.AllHats.Count - 1, data);

                        _customHatsLoaded = true;
                    }



                    _customHatsLoaded = true;
                }
            }

            public static Sprite GetSprite(string name)
                => Assets.LoadAsset(name).Cast<GameObject>().GetComponent<SpriteRenderer>().sprite;

            private static int HatID = 0;

            private static HatBehaviour CreateHat(Sprite sprite, Sprite climb = null, Sprite floor = null, Sprite leftimage = null, bool bounce = false, bool altshader = false)
            {
                var magicShader = DestroyableSingleton<HatManager>.Instance.AllHats[90].Cast<HatBehaviour>().AltShader;
                var newHat = ScriptableObject.CreateInstance<HatBehaviour>();
                newHat.MainImage = sprite;
                newHat.ProductId = $"hat_{sprite.name}";
                newHat.Order = 199 + HatID;
                newHat.InFront = true;
                newHat.NoBounce = bounce;
                newHat.FloorImage = floor;
                newHat.ClimbImage = climb;
                newHat.LeftMainImage = leftimage;
                newHat.ChipOffset = new Vector2(-0.1f, 0.4f);
                if(altshader == true) { newHat.AltShader = magicShader; }

                return newHat;
            }
        }


        //[HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.HandleAnimation))]
        //private static class PlayerPhysicsHandleAnimationPatch
        //{
        //    private static void Postfix(PlayerPhysics __instance)
        //    {
        //        AnimationClip currentAnimation = __instance.Animator.GetCurrentAnimation();
        //        if (currentAnimation == __instance.ClimbAnim || currentAnimation == __instance.ClimbDownAnim) return;
        //        HatParent hp = __instance.myPlayer.HatRenderer;
        //        if (hp.Hat == null) return;
        //        foreach(var data in authorDatas)
        //        {
        //            if (data.LeftImageName != null)
        //            {
        //                if (__instance.rend.flipX)
        //                {
        //                    hp.FrontLayer.sprite = AddCustomHats.GetSprite(data.LeftImageName);
        //                }
        //                else
        //                {
        //                    hp.FrontLayer.sprite = hp.Hat.MainImage;
        //                }
        //            }
        //            //if (data.BackFlipImage != null)
        //            //{
        //            //    if (__instance.rend.flipX)
        //            //    {
        //            //        hp.BackLayer.sprite = extend.BackFlipImage;
        //            //    }
        //            //    else
        //            //    {
        //            //        hp.BackLayer.sprite = hp.Hat.BackImage;
        //            //    }
        //            //}
        //        }
                
        //    }
        //}

    }
}
