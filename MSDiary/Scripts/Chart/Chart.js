
$(function () {
window.onload = function () {
    var chart = new CanvasJS.Chart("chartContainer", {
        title:{
            text: "Evolução do seu saldo "
        },
        axisX:{
            title: "Data",
            titleFontColor: "green",
            titleFontWeight: "bold"
        },
        axisY:{
            title: "Saldo",
            titleFontWeight: "bold",
            titleFontColor: "green"
        },
        data: [
		{
		    // Change type to "doughnut", "line", "splineArea", etc.
		    color: "green",
		    type: "line",
		    dataPoints: [
				{ x: 10, y: 10 },
				{ x: 20, y: 12 },
				{ x: 30, y: 8 },
				{ x: 40, y: 14 },
				{ x: 50, y: 6 },
				{ x: 60, y: 24 },
				{ x: 70, y: -4 },
				{ x: 80, y: 10 }
		    ]
		}
        ]
    });
    chart.render();
}
});