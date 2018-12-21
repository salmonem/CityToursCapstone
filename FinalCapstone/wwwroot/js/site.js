//function initialize() {

//    var markers = [];
//    var map = new google.maps.map(document.getelementbyid('map-canvas'), {
//        maptypeid: google.maps.maptypeid.roadmap
//    });

//    var defaultbounds = new google.maps.latlngbounds(
//        new google.maps.latlng(-33.8902, 151.1759),
//        new google.maps.latlng(-33.8474, 151.2631));
//    map.fitbounds(defaultbounds);

//    // create the search box and link it to the ui element.
//    var input = /** @type {htmlinputelement} */(
//        document.getelementbyid('pac-input'));
//    map.controls[google.maps.controlposition.top_left].push(input);

//    //var searchbox = new google.maps.places.searchbox(
//    ///** @type {htmlinputelement} */(input));

//    // [start region_getplaces]
//    // listen for the event fired when the user selects an item from the
//    // pick list. retrieve the matching places for that item.
//    google.maps.event.addlistener(searchbox, 'places_changed', function () {
//        var places = searchbox.getplaces();

//        if (places.length == 0) {
//            return;
//        }
//        for (var i = 0, marker; marker = markers[i]; i++) {
//            marker.setmap(null);
//        }

//        // for each place, get the icon, place name, and location.
//        markers = [];
//        var bounds = new google.maps.latlngbounds();
//        for (var i = 0, place; place = places[i]; i++) {
//            var image = {
//                url: place.icon,
//                size: new google.maps.size(71, 71),
//                origin: new google.maps.point(0, 0),
//                anchor: new google.maps.point(17, 34),
//                scaledsize: new google.maps.size(25, 25)
//            };

//            // create a marker for each place.
//            var marker = new google.maps.marker({
//                map: map,
//                icon: image,
//                title: place.name,
//                position: place.geometry.location
//            });

//            markers.push(marker);

//            bounds.extend(place.geometry.location);
//        }

//        map.fitbounds(bounds);
//    });
//    // [end region_getplaces]

//    // bias the searchbox results towards places that are within the bounds of the
//    // current map's viewport.
//    google.maps.event.addlistener(map, 'bounds_changed', function () {
//        var bounds = map.getbounds();
//        searchbox.setbounds(bounds);
//    });
//}

//google.maps.event.adddomlistener(window, 'load', initialize);