let dotNetRef;

let map;
let marker;
let infoWindow;

export function openInfo() {
    infoWindow.close();

    infoWindow.open(map, marker);
}

export function setDotNetRef(_dotNetRef) {

    dotNetRef = _dotNetRef;
    console.log("setDotNetRef done: " + dotNetRef)
}

export function init(id) {

    if (map)
        return;

    // 초기화 여부 확인

    var mapOptions = {
        center: new naver.maps.LatLng(35.841716, 128.618540),
        zoom: 17,
        zoomControl: false, //줌 컨트롤의 표시 여부
        zoomControlOptions: { //줌 컨트롤의 옵션
            position: naver.maps.Position.TOP_LEFT
        }
    };


    map = new naver.maps.Map(id, mapOptions);

    marker = new naver.maps.Marker({
        map: map,
        position: map.getCenter(),
    });
    infoWindow = new naver.maps.InfoWindow({
        content: '<p>이 위치에서 검색</p>',
        disableAnchor: false,
        borderColor: '#aaaaaa',
        anchorSize: new naver.maps.Size(10, 10),
    });

    naver.maps.Event.addListener(map, 'center_changed', function (center) {
        console.log(center);
        //marker.setPosition(center);
    });

    naver.maps.Event.addListener(map, 'zoom_changed', function (zoom) {
        console.log(zoom);
    });

    // 클릭으로 위치가 변경된 경우
    naver.maps.Event.addListener(map, 'click', function (e) {
        console.log(e);
        marker.setPosition(e.latlng);
        //map.setCenter(e.latlng);

        // dotnet call
        if (dotNetRef)
        {
            console.log("before OnLocationChanged");
            dotNetRef.invokeMethodAsync('OnLocationChanged', e.latlng.x, e.latlng.y);
            console.log("after OnLocationChanged");
        }

        naver.maps.Service.reverseGeocode({ coords: e.latlng }, function (status, response) {
            if (status !== naver.maps.Service.Status.OK) {
                return alert('Something wrong!');
            }

            var result = response.v2, // 검색 결과의 컨테이너
                items = result.results, // 검색 결과의 배열
                address = result.address; // 검색 결과로 만든 주소

            // do Something

            console.log(address);

            if (dotNetRef)
            {
                console.log("before OnAddressChanged");
                dotNetRef.invokeMethodAsync('OnAddressChanged', address.jibunAddress);
                console.log("after OnAddressChanged");
            }
            


            //infoWindow.close();
            const content = '<p>이 위치에서 검색</p>';
            //const content = '<p>이 위치에서 검색</p><p>' + e.latlng.y + ',' + e.latlng.x + '</p>';
            //infoWindow.setContent(content);
            //infoWindow.open(map, marker);
        });

    });
}


export function getCenter(){
    if (!map)
        return undefined;

    return map.getCenter();
}
