namespace Mapbox.Examples
{
    using Mapbox.Unity.Location;
    using Mapbox.Unity.Map;
    using Mapbox.Utils;
    using UnityEngine;

    public class ImmediatePositionWithLocationProvider : MonoBehaviour
	{
		[SerializeField]
		private AbstractMap _map;
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

        void Start()
		{
           
            _map.OnInitialized += () => _isInitialized = true;

        }

        
        void LateUpdate()
		{

            if (_isInitialized)
            {
                transform.localPosition = _map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude) + new Vector3(0, elevation, 0);

        
            }
        }
	}
}