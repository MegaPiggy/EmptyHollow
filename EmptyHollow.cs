using OWML.Common;
using OWML.ModHelper;
using System.Collections;
using UnityEngine;

namespace EmptyHollow
{
    public partial class EmptyHollow : ModBehaviour
    {
        private const string NewHorizons = "xen.NewHorizons";

        private static EmptyHollow instance;
        public static EmptyHollow Instance => instance;

        private static GravityCrystalTrio gravityCrystalTrio;
        public static GravityCrystalTrio GravityCrystalTrio => gravityCrystalTrio;

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
                            UnityEngine.Transform interactables = crossroads.Find("Interactables_Crossroads");
                            UnityEngine.Transform visible = interactables.Find("VisibleFrom_BH");
                            UnityEngine.Transform gravityCrystals = visible.Find("GravityCrystals");
                            UnityEngine.Transform ocg = gravityCrystals.Find("OtherComponentsGroup");
                            UnityEngine.GameObject gravityCrystal = ocg.Find("Prefab_NOM_GravityCrystal").gameObject;
                            gravityCrystal.SetActive(false);
                            UnityEngine.GameObject gravityCrystalTest1 = UnityEngine.Object.Instantiate(gravityCrystal, ocg);
                            gravityCrystalTest1.name = "Prefab_NOM_GravityCrystal_Test";
                            UnityEngine.GameObject gravityCrystalTest2 = UnityEngine.Object.Instantiate(gravityCrystal, ocg);
                            gravityCrystalTest2.name = "Prefab_NOM_GravityCrystal_Test2";
                            UnityEngine.GameObject gravityCrystalTest3 = UnityEngine.Object.Instantiate(gravityCrystal, ocg);
                            gravityCrystalTest3.name = "Prefab_NOM_GravityCrystal_Test3";
                            gravityCrystal.SetActive(true);
                            gravityCrystalTrio = new GravityCrystalTrio(gravityCrystalTest1, gravityCrystalTest2, gravityCrystalTest3);
                            gravityCrystalTest1.GetComponentInChildren<OWCollider>()._initialized = false;
                            gravityCrystalTest2.GetComponentInChildren<OWCollider>()._initialized = false;
                            gravityCrystalTest1.GetComponentInChildren<DirectionalForceVolume>()._attachedBody = gravityCrystalTest1.GetComponentInParent<OWRigidbody>();
                            gravityCrystalTest1.GetComponentInChildren<DirectionalForceVolume>()._triggerVolume = gravityCrystalTest1.GetComponentInChildren<OWTriggerVolume>();
                            gravityCrystalTest2.GetComponentInChildren<DirectionalForceVolume>()._attachedBody = gravityCrystalTest2.GetComponentInParent<OWRigidbody>();
                            gravityCrystalTest2.GetComponentInChildren<DirectionalForceVolume>()._triggerVolume = gravityCrystalTest2.GetComponentInChildren<OWTriggerVolume>();
                            gravityCrystalTest3.GetComponentInChildren<DirectionalForceVolume>()._attachedBody = gravityCrystalTest3.GetComponentInParent<OWRigidbody>();
                            gravityCrystalTest3.GetComponentInChildren<DirectionalForceVolume>()._triggerVolume = gravityCrystalTest3.GetComponentInChildren<OWTriggerVolume>();
                            gravityCrystalTest1.transform.localPosition = new UnityEngine.Vector3(-20.5f, 155.5f, 1);
                            gravityCrystalTest1.transform.localEulerAngles = new UnityEngine.Vector3(0, 0, 0);
                            gravityCrystalTest2.transform.localPosition = new UnityEngine.Vector3(-8.5f, 156.575f, 1);
                            gravityCrystalTest2.transform.localEulerAngles = new UnityEngine.Vector3(0, 90, 0);
                            gravityCrystalTest3.transform.localPosition = new UnityEngine.Vector3(18, 155.75f, 1);
                            gravityCrystalTest3.transform.localEulerAngles = new UnityEngine.Vector3(0, 0, 0);
                            OWCapsuleCollider gccp1 = gravityCrystalTest1.GetComponentInChildren<OWCapsuleCollider>();
                            OWCapsuleCollider gccp2 = gravityCrystalTest2.GetComponentInChildren<OWCapsuleCollider>();
                            OWCapsuleCollider gccp3 = gravityCrystalTest3.GetComponentInChildren<OWCapsuleCollider>();
                            bool ubc = gccp1._useBottomCap;
                            bool utc = gccp1._useTopCap;
                            UnityEngine.GameObject Volume1 = gccp1.gameObject;
                            UnityEngine.GameObject Volume2 = gccp2.gameObject;
                            UnityEngine.GameObject Volume3 = gccp3.gameObject;
                            UnityEngine.Object.DestroyImmediate(gccp1);
                            UnityEngine.Object.DestroyImmediate(gccp2);
                            UnityEngine.Object.DestroyImmediate(gccp3);
                            EmptyHollow.CapsuleCollider ehcc1 = Volume1.AddComponent<EmptyHollow.CapsuleCollider>();
                            EmptyHollow.CapsuleCollider ehcc2 = Volume2.AddComponent<EmptyHollow.CapsuleCollider>();
                            EmptyHollow.CapsuleCollider ehcc3 = Volume3.AddComponent<EmptyHollow.CapsuleCollider>();
                            ehcc1._useBottomCap = ubc;
                            ehcc1._useTopCap = utc;
                            ehcc2._useBottomCap = ubc;
                            ehcc2._useTopCap = utc;
                            ehcc3._useBottomCap = ubc;
                            ehcc3._useTopCap = utc;
                            gravityCrystalTest1.SetActive(true);
                            gravityCrystalTest2.SetActive(true);
                            gravityCrystalTest3.SetActive(true);
                            gravityCrystalTest2.GetComponentInChildren<UnityEngine.CapsuleCollider>().radius = 15;
                            gravityCrystalTest3.GetComponentInChildren<UnityEngine.CapsuleCollider>().height = 21;
                            gravityCrystalTest3.GetComponentInChildren<UnityEngine.CapsuleCollider>().radius = 21;
                            Instance.StartCoroutine(ActivateCrystals());
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

        private static IEnumerator ActivateCrystals()
        {
            yield return new WaitForEndOfFrame();
            ActivateOnly();
            yield return new WaitForEndOfFrame();
            DectivateOnly();
        }

        public static void ActivateDectivate()
        {
            UnityEngine.GameObject playerDetector = UnityEngine.GameObject.FindGameObjectWithTag("PlayerDetector");
            UnityEngine.CapsuleCollider capsuleCollider = playerDetector.GetComponent<UnityEngine.CapsuleCollider>();
            gravityCrystalTrio.One.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerEnter(capsuleCollider);
            gravityCrystalTrio.Two.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerEnter(capsuleCollider);
            gravityCrystalTrio.Three.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerEnter(capsuleCollider);
            gravityCrystalTrio.One.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerExit(capsuleCollider);
            gravityCrystalTrio.Two.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerExit(capsuleCollider);
            gravityCrystalTrio.Three.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerExit(capsuleCollider);
        }

        public static void ActivateOnly()
        {
            UnityEngine.GameObject playerDetector = UnityEngine.GameObject.FindGameObjectWithTag("PlayerDetector");
            UnityEngine.CapsuleCollider capsuleCollider = playerDetector.GetComponent<UnityEngine.CapsuleCollider>();
            gravityCrystalTrio.One.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerEnter(capsuleCollider);
            gravityCrystalTrio.Two.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerEnter(capsuleCollider);
            gravityCrystalTrio.Three.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerEnter(capsuleCollider);
        }

        public static void DectivateOnly()
        {
            UnityEngine.GameObject playerDetector = UnityEngine.GameObject.FindGameObjectWithTag("PlayerDetector");
            UnityEngine.CapsuleCollider capsuleCollider = playerDetector.GetComponent<UnityEngine.CapsuleCollider>();
            gravityCrystalTrio.One.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerExit(capsuleCollider);
            gravityCrystalTrio.Two.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerExit(capsuleCollider);
            gravityCrystalTrio.Three.GetComponentInChildren<EmptyHollow.CapsuleCollider>().OnTriggerExit(capsuleCollider);
        }
    }
}
