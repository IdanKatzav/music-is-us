$(document).ready(function getBranchAddress(){
    $('.displayOnMap').click(function () {
        var prop = $(this).data('prop');
        parseAddressForMapAPI(prop)
    })
    function parseAddressForMapAPI(address) {
        console.log(address);
        var parsedBySymbol = address.split(", ");
        var tempStr = [parsedBySymbol.length];
        var parsedAddress = "";
        for (var i = 0; i < parsedBySymbol.length;i++) {
            tempStr[i] = parsedBySymbol[i].replaceAll(" ", "+");
            parsedAddress += tempStr[i] + ",";
        }
        setSrcForMap(parsedAddress);
    }
    function setSrcForMap(AddressToDisplay) {
        $(".mapDisplay").attr('src', "https://www.google.com/maps/embed/v1/place?key=AIzaSyBsG-gcX20RziyqnfWo6p-H44M4vbECMQ4&q=" + AddressToDisplay);
    }
})