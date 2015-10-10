namespace FlpToFp.Core
{
    /// <summary>
    /// An internal representation of a waypoint
    /// </summary>
    class Waypoint
    {
        private readonly double _latitude;
        private readonly double _longitude;
        private readonly string _identifier;

        /// <summary>
        /// Creates a new immutable waypoint
        /// </summary>
        /// <param name="latitude">Latitude in decimal format</param>
        /// <param name="longitude">Longitude in decimal format</param>
        /// <param name="identifier">Identifier up to 5 letters</param>
        public Waypoint(double latitude, double longitude, string identifier)
        {
            _latitude = latitude;
            _longitude = longitude;
            _identifier = identifier;
        }
    
        /// <summary>
        /// Laittude in decimal format
        /// </summary>
        public double Latitude
        {
            get { return _latitude; }
        }

        /// <summary>
        /// Longitude in decimal format
        /// </summary>
        public double Longitude
        {
            get { return _longitude; }
        }

        /// <summary>
        /// Identifier up to 5 letters
        /// </summary>
        public string Identifier
        {
            get { return _identifier; }
        }

    }
}