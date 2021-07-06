$(document).ready(function() {
    $('.displayOnMap').click(function() {
        var prop = $(this).data('prop');
        console.log(prop);
        parseAddressForMapAPI(prop)
    })
    function parseAddressForMapAPI(address) {
        console.log(address);
        addressStr = address.toString();
        let parsedBySymbol = addressStr.split(", ");
        let tempStr = [parsedBySymbol.length];
        let parsedAddress = "";
        for (let i = 0; i < parsedBySymbol.length; i++) {
            tempStr[i] = parsedBySymbol[i].replaceAll(" ", "+");
            parsedAddress += tempStr[i] + ",";
        }
        setSrcForMap(parsedAddress);
    }
    function setSrcForMap(AddressToDisplay) {
        $(".mapDisplay").attr('src', "https://www.google.com/maps/embed/v1/place?key=AIzaSyBsG-gcX20RziyqnfWo6p-H44M4vbECMQ4&q=" + AddressToDisplay);
    }
})