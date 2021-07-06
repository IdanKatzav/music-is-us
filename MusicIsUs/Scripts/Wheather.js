var weatherCallback = function (data) {
    console.log(data);
    var results = data.query.results
    var firstResult = results.channel.item.condition
    console.log(firstResult);

    var location = 'Unknown' // not returned in response
    var temp = firstResult.temp
    var text = firstResult.text

}