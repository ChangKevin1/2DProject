using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

    public class ShakeComponent : MonoBehaviour
    {
        [SerializeField] private Transform target_;
        [SerializeField] private float defaultDuration_ = 0.5f;
        [SerializeField] private float defaultAmplitude_ = 0.75f;
        [SerializeField] private float defaultFinalAmplitude_ = 0.25f;
        [SerializeField] private AnimationCurve defaultAmplitudeCurve_ = AnimationCurve.Linear(0, 0, 1, 1);

        private float duration_;
        private float remainingTime_;
        private float amplitude_;
        private float finalAmplitude_;
        private AnimationCurve amplitudeCurve_;
        private Vector3 initialPosition_;

        private void Awake()
        {
            if (target_ == null)
            {
                target_ = GetComponent<Transform>();
            }
            initialPosition_ = target_.localPosition;
        }

        public void Shake(float duration = -1, float amplitude = -1, float finalAmplitude = -1, AnimationCurve amplitudeCurve = null)
        {
        initialPosition_ = target_.localPosition;
            duration_ = duration > 0 ? duration : defaultDuration_;
            amplitude_ = amplitude >= 0 ? amplitude : defaultAmplitude_;
            finalAmplitude_ = finalAmplitude >= 0 ? finalAmplitude : defaultFinalAmplitude_;
            amplitudeCurve_ = amplitudeCurve != null ? amplitudeCurve : defaultAmplitudeCurve_;

            remainingTime_ = duration_;
            enabled = true;
        }

        private void Update()
        {
            if (remainingTime_ > 0)
            {
                float curveValue = amplitudeCurve_.Evaluate(1 - (remainingTime_ / duration_));
                float amplitude = (1 - curveValue) * amplitude_ + curveValue * finalAmplitude_;
                transform.localPosition = initialPosition_ + Random.insideUnitSphere * amplitude;
                remainingTime_ -= Time.deltaTime;
            }
            else
            {
                remainingTime_ = 0f;
                transform.localPosition = initialPosition_;
                enabled = false;
            }
        }

#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button("Test Shake")]
#endif
        [ContextMenu("Test Shake")]
        private void EDITOR_TestShake()
        {
            Shake();
        }
#endif
    }
