using OWML.Common;
using OWML.ModHelper;

namespace EmptyHollow
{
    public class EmptyHollow : ModBehaviour
    {
        private const string NewHorizons = "xen.NewHorizons";

        private static EmptyHollow instance;
        public static EmptyHollow Instance => instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (ModHelper.Interaction.ModExists(NewHorizons))
                ModHelper.Interaction.GetModApi<INewHorizons>(NewHorizons).GetStarSystemLoadedEvent().AddListener(OnStarSystemLoaded);
            else
                GlobalMessenger.AddListener("WakeUp", new Callback(OnWakeUp));
        }

        private static void OnWakeUp()
        {
            if (LoadManager.GetCurrentScene() != OWScene.SolarSystem) return;
            DetachAll();
        }

        private static void OnStarSystemLoaded(string starSystem)
        {
            if (starSystem != "SolarSystem") return;
            DetachAll();
        }

        private static void DetachAll()
        {
            try
            {
                UnityEngine.GameObject brittle = UnityEngine.GameObject.Find("BrittleHollow_Body");
                if (brittle != null && brittle.transform != null)
                {
                    UnityEngine.Transform blackHole = brittle.transform.Find("Sector_BH");
                    if (blackHole != null && blackHole.gameObject != null)
                    {
                        UnityEngine.Transform northHemisphere = blackHole.Find("Sector_NorthHemisphere");
                        if (northHemisphere != null && northHemisphere.gameObject != null)
                        {
                            UnityEngine.Transform northPole = northHemisphere.Find("Sector_NorthPole");
                            if (northPole != null && northPole.gameObject != null)
                            {
                                northPole.gameObject.AddComponent<CustomDetachableFragment>();
                            }
                            else
                            {
                                instance.ModHelper.Console.WriteLine("Cannot find Brittle Hollow North Pole Sector", MessageType.Error);
                            }
                        }
                        else
                        {
                            instance.ModHelper.Console.WriteLine("Cannot find Brittle Hollow North Hemisphere Sector", MessageType.Error);
                        }
                        UnityEngine.Transform southHemisphere = blackHole.Find("Sector_SouthHemisphere");
                        if (southHemisphere != null && southHemisphere.gameObject != null)
                        {
                            UnityEngine.Transform southPole = southHemisphere.Find("Sector_SouthPole");
                            if (southPole != null && southPole.gameObject != null)
                            {
                                southPole.gameObject.AddComponent<CustomDetachableFragment>();
                            }
                            else
                            {
                                instance.ModHelper.Console.WriteLine("Cannot find Brittle Hollow South Pole Sector", MessageType.Error);
                            }
                        }
                        else
                        {
                            instance.ModHelper.Console.WriteLine("Cannot find Brittle Hollow South Hemisphere Sector", MessageType.Error);
                        }
                        UnityEngine.Transform crossroads = blackHole.Find("Sector_Crossroads");
                        if (crossroads != null && crossroads.gameObject != null)
                        {
                            crossroads.gameObject.AddComponent<CustomDetachableFragment>();
                        }
                        else
                        {
                            instance.ModHelper.Console.WriteLine("Cannot find Brittle Hollow Crossroads Sector", MessageType.Error);
                        }
                        UnityEngine.Transform quantum = blackHole.Find("Sector_QuantumFragment");
                        if (quantum != null && quantum.gameObject != null)
                        {
                            quantum.gameObject.GetComponent<TimedFragmentIntegrity>()._earliestTime = 0;
                        }
                        else
                        {
                            instance.ModHelper.Console.WriteLine("Cannot find Brittle Hollow Quantum Fragment Sector", MessageType.Error);
                        }
                    }
                    else
                    {
                        instance.ModHelper.Console.WriteLine("Cannot find Brittle Hollow Black Hole Sector", MessageType.Error);
                    }
                    foreach (FragmentIntegrity fi in brittle.GetComponentsInChildren<FragmentIntegrity>())
                    {
                        try
                        {
                            fi._integrity = 0;
                            fi.AddDamage(1);
                            fi.CallOnTakeDamage();
                        }
                        catch (System.Exception ex)
                        {
                            instance.ModHelper.Console.WriteLine(ex.ToString(), MessageType.Error);
                        }
                    }
                }
                else
                {
                    instance.ModHelper.Console.WriteLine("Cannot find Brittle Hollow", MessageType.Error);
                    return;
                }
            }
            catch (System.Exception ex)
            {
                instance.ModHelper.Console.WriteLine(ex.ToString(), MessageType.Error);
            }
        }
    }
}
