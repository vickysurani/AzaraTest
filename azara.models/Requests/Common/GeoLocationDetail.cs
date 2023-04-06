﻿namespace azara.models.Requests.Common;

public class SetLocaionDetail
{
    public string Location { get; set; }

    public string Longitude { get; set; }

    public string Latitude { get; set; }
}

public class GeoLocationDetail
{
    //public int place_id { get; set; }

    //public string licence { get; set; }

    //public string osm_type { get; set; }

    //public long osm_id { get; set; }

    //public List<string> boundingbox { get; set; }

    //public string lat { get; set; }

    //public string lon { get; set; }

    //public string display_name { get; set; }

    //public string @class { get; set; }

    //public string types { get; set; }

    //public double importance { get; set; }

    //public string icon { get; set; }

    public List<Result> results { get; set; }
    public string status { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class AddressComponent
{
    public string long_name { get; set; }
    public string short_name { get; set; }
    public List<string> types { get; set; }
}

public class Bounds
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

public class Geometry
{
    public Bounds bounds { get; set; }
    public Location location { get; set; }
    public string location_type { get; set; }
    public Viewport viewport { get; set; }
}

public class Location
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Northeast
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Result
{
    public List<AddressComponent> address_components { get; set; }
    public string formatted_address { get; set; }
    public Geometry geometry { get; set; }
    public string place_id { get; set; }
    public List<string> types { get; set; }
}

public class Southwest
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Viewport
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

