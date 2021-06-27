function drawPieChart(jsonData) {
    var height = 200;
    var width = 200;

    var innerRadius = width / 4;
    var outerRadius = width / 2;

    // Generates the pie chart as a function that is called to handle data.
    var pie = d3.pie().value(function (d) {
        return d.count;
    });

    // Sets the colors for the pie slices automaticlly.
    var color = d3.schemeCategory10;

    const arc = d3.arc().innerRadius(innerRadius)
          .outerRadius(outerRadius);
    // Sets the size of the pie chart window on the screen.
    const svg = d3.select('.pieChart').append('.svg').attr('width', width).attr('height', height);

    const arcs = svg.selectAll('g.arc').data(pie(jsonData)).enter().append('g').attr('class', 'arc');

    arcs.append('path').attr('fill', function (d, i) {
        return color[i];
    }).attr('d', arc);
}