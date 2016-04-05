
$(function () {
window.onload = function () {
    var url = "@Url.Action('ChartData','Chart')";
    $.getJSON(url,function(data){
        var chart = new CanvasJS.Chart("chartContainer",data);
        chart.render();
    });
    chart.render();
}
});