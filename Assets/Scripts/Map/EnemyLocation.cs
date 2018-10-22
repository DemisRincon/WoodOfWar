namespace Mapbox.Examples
{
    using Mapbox.Unity.Location;
    using Mapbox.Unity.Map;
    using Mapbox.Utils;
    using UnityEngine;

    public class EnemyLocation : MonoBehaviour
    {
        [SerializeField]
        private AbstractMap _map;

        [SerializeField]
        private float xRangeDistance;

        [SerializeField]
        private float yRangeDistance;

        [SerializeField]
        private float elevation;
        bool _isInitialized;

        ILocationProvider _locationProvider;
        ILocationProvider LocationProvider
        {
            get
            {
                if (_locationProvider == null)
                {
                    _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
                }

                return _locationProvider;
            }
        }

        Vector3 _targetPosition;
        Vector2d enemyPostion;
        [SerializeField]
        private float lifeCounter;

        [SerializeField]
        private int lifeCounterInt;

        [SerializeField]
        private TextMesh text;
        public Vector2d _initalPosition;
        private bool flag;

        void Start()
        {
            flag = false;
            enemyPostion = new Vector2d(Random.Range(-1* xRangeDistance, xRangeDistance), Random.Range(-1* yRangeDistance, yRangeDistance));
            _map.OnInitialized += () => _isInitialized = true;
            lifeCounter = Random.Range(120, 600);
        }

        private void Update()
        {
            lifeCounter -= Time.deltaTime;
            lifeCounterInt = (int)lifeCounter;
            text.text = (lifeCounterInt/60).ToString()+":"+ (lifeCounterInt % 60).ToString();
            if (lifeCounter <= 0)
            {
                enemyPostion = new Vector2d(Random.Range(-1 * xRangeDistance, xRangeDistance), Random.Range(-1 * yRangeDistance, yRangeDistance));
                lifeCounter= Random.Range(120, 600);
            }
        }
        
        void LateUpdate()
        {
            if (_isInitialized)
            {
                
                if (LocationProvider.CurrentLocation.LatitudeLongitude.ToString() != "0.00000,0.00000" && flag == false)
                {
                    flag = true;
                    _initalPosition = LocationProvider.CurrentLocation.LatitudeLongitude;
                }
                transform.localPosition = (_map.GeoToWorldPosition(_initalPosition + enemyPostion)) + new Vector3(0, elevation, 0);
            }
        
        }

    }
}