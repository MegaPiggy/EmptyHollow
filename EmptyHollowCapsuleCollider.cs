using OWML.ModHelper;
using UnityEngine;

namespace EmptyHollow
{
    public partial class EmptyHollow : ModBehaviour
    {
        [UnityEngine.RequireComponent(typeof(UnityEngine.CapsuleCollider))]
        public class CapsuleCollider : OWCapsuleCollider
        {
            public class Fixer : UnityEngine.MonoBehaviour
            {

            }

            private UnityEngine.GameObject _fix;
            public UnityEngine.GameObject Fix
            {
                get
                {
                    if (_fix == null)
                    {
                        _fix = new UnityEngine.GameObject("CapsuleColliderFix", typeof(Fixer));
                        _fix.transform.parent = gameObject.transform;
                        _fix.transform.localPosition = UnityEngine.Vector3.zero;
                        _fix.transform.localEulerAngles = UnityEngine.Vector3.zero;
                    }
                    return _fix;
                }
            }
            private OWCustomCollider.TrackedTransform _trackedFix;

            public override void Awake()
            {
                _collider = GetComponent<UnityEngine.Collider>();
                _capsule = GetComponent<UnityEngine.CapsuleCollider>();
                _owCollider = gameObject.GetAddComponent<OWCollider>();
                _trackedTransforms = new System.Collections.Generic.List<OWCustomCollider.TrackedTransform>(9);
                _owCollider.OnColliderDisabled += new OWCollider.ColliderDisabledEvent(OnColliderDisabled);
                _trackedFix = new OWCustomCollider.TrackedTransform(Fix.transform);
                _trackedTransforms.Add(_trackedFix);
                enabled = true;
            }

            public new void FixedUpdate()
            {
                _trackedFix.transform = Fix.transform;
                if (!_trackedTransforms.Contains(_trackedFix))
                    _trackedTransforms.Add(_trackedFix);
                base.FixedUpdate();
                if (!_trackedTransforms.Contains(_trackedFix))
                    _trackedTransforms.Add(_trackedFix);
                if (enabled == false)
                    enabled = true;
            }

            public override bool IsTrackerInCollider(OWCustomCollider.TrackedTransform tracker)
            {
                if (tracker.transform == _trackedFix.transform)
                    return true;
                return base.IsTrackerInCollider(tracker);
            }

            public new void OnTriggerEnter(UnityEngine.Collider hitCollider)
            {
                base.OnTriggerEnter(hitCollider);
                if (!_trackedTransforms.Contains(_trackedFix))
                    _trackedTransforms.Add(_trackedFix);
                if (enabled == false)
                    enabled = true;
            }

            public new void OnTriggerExit(UnityEngine.Collider hitCollider)
            {
                base.OnTriggerEnter(hitCollider);
                if (!_trackedTransforms.Contains(_trackedFix))
                    _trackedTransforms.Add(_trackedFix);
                if (enabled == false)
                    enabled = true;
            }

            public void OnCollisionEnter(UnityEngine.Collision collision)
            {
                UnityEngine.Collider hitCollider = collision.collider;
                if (hitCollider.isTrigger)
                    base.OnTriggerEnter(hitCollider);
                if (!_trackedTransforms.Contains(_trackedFix))
                    _trackedTransforms.Add(_trackedFix);
                if (enabled == false)
                    enabled = true;
            }

            public void OnCollisionExit(UnityEngine.Collision collision)
            {
                UnityEngine.Collider hitCollider = collision.collider;
                if (hitCollider.isTrigger)
                    base.OnTriggerEnter(hitCollider);
                if (!_trackedTransforms.Contains(_trackedFix))
                    _trackedTransforms.Add(_trackedFix);
                if (enabled == false)
                    enabled = true;
            }
        }
    }
}