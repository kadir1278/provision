function DrawGraphic(dataList) {
    var data = [];
    for (var i = 0; i < dataList.length; i++) {
        data.push(
            {
                "date": dataList[i].date,
                "forexBuying": dataList[i].forexBuying,
                "banknoteSelling": dataList[i].banknoteSelling,
                "currencyName": dataList[i].currencyName,
                "forexSelling": dataList[i].forexSelling,
                "banknoteBuying": dataList[i].banknoteBuying
            },
        )
    }

    // If data is not available
    if (typeof data === "undefined") {

        // Output error
        $("#graph").text("No Data Available.");

        // Otherwise
    } else {

        // Set SVGs dimensions and margins
        var margin = { top: 20, right: 20, bottom: 30, left: 50 },
            width = 960 - margin.left - margin.right,
            height = 300 - margin.top - margin.bottom;

        // Parse dates
        var parseTime = d3.timeParse("%Y-%m-%d"),
            formatTime = d3.timeFormat("%b %e");

        // Setup ranges
        var x = d3.scaleTime().range([0, width]),
            y = d3.scaleLinear().range([height, 0]);

        // Setup line
        var valueline = d3.line()
            .x(function (d) { return x(d.date); })
            .y(function (d) { return y(d.forexBuying); });

        // Setup tooltips
        var div = d3.select("#graph").append("div")
            .attr("class", "tooltip")
            .style("opacity", 0);

        // Append SVG to container DIV, append group to SVG
        var svg = d3.select("#graph").append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform",
                "translate( " + margin.left + ", " + margin.top + " )");

        // Format the data
        data.forEach(function (d) {
            d.date = parseTime(d.date);
            d.forexBuying = +d.forexBuying;
        });

        //---

        // get the min/max dates
        var extent = d3.extent(data, function (d) { return d.date; }),

            // hash the existing days for easy lookup
            dateHash = data.reduce(function (agg, d) {
                agg[d.date] = true;
                return agg;
            }, {});

        // make even intervals
        d3.timeDays(extent[0], extent[1])
            // drop the existing ones
            .filter(function (date) {
                return !dateHash[date];
            })
            // and push them into the array
            .forEach(function (date) {
                var emptyRow = { date: date, forexBuying: 0 };
                data.push(emptyRow);
            });
        // re-sort the data
        data.sort(function (a, b) { return d3.ascending(a.date, b.date); });

        //---

        // Scale the ranges
        x.domain(d3.extent(data, function (d) { return d.date; }));
        y.domain([0, d3.max(data, function (d) { return d.forexBuying; })]);

        // Append the line to the graph
        svg.append("path")
            .data([data])
            .attr("class", "line")
            .attr("d", valueline);

        // Append tooltips to the graph
        svg.selectAll("dot")
            .data(data)
            .enter().append("circle")
            .attr("class", "node")
            .attr("r", 5)
            .attr("cx", function (d) { return x(d.date); })
            .attr("cy", function (d) { return y(d.forexBuying); })
            .on("mouseover", function (d) {
                div.transition()
                    .duration(200)
                    .style("opacity", 1);
                div.html(
                    "Currency Name : " + d.currencyName + "<br/>" +
                    "Date : " + formatTime(d.date) + "<br/>" +
                    "Forex Buying : " + d.forexBuying + "<br/>" +
                    "Forex Selling: " + d.forexSelling + "<br/>" +
                    "Banknote Selling : " + d.banknoteSelling + "<br/>" +
                    "BanknoteBuying : " + d.banknoteBuying + "<br/>")
            })
            .on("mousemove", function () {
                return div
                    .style("top", (d3.event.pageY + 16) + "px")
                    .style("left", (d3.event.pageX + 16) + "px");
            })
            .on("mouseout", function () {
                div.transition()
                    .duration(200)
                    .style("opacity", 0);
            });

        // Append X Axis to the graph
        svg.append("g")
            .attr("transform", "translate( 0," + height + " )")
            .call(d3.axisBottom(x));

        // Append Y Axis to the graph
        svg.append("g")
            .call(d3.axisLeft(y));
    }
}
