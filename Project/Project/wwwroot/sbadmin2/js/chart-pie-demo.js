// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito, -apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

// Fetch percentages data
fetch("https://localhost:7159/Manage/Application/GetPercentages")
  .then(response => response.json())
  .then(data => {
    const totalApplications = data.totalApplications;
    const totalApproved = data.totalApproved;
    const totalRejected = data.totalRejected;
    const totalProcessing = data.totalProcessing;
    const totalCanceled = data.totalCanceled;

    // Calculate percentages
    const percentages = [
      (totalApproved / totalApplications) * 100,
      (totalRejected / totalApplications) * 100,
      (totalProcessing / totalApplications) * 100,
      (totalCanceled / totalApplications) * 100
    ];

    // Initialize the chart with the fetched data
    initPieChart(percentages);
  });

function initPieChart(percentages) {
  var ctx = document.getElementById("myPieChart");
  var myPieChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
      labels: ["Approved", "Rejected", "Processing", "Canceled"],
      datasets: [{
        data: percentages,
        backgroundColor: ['#1cc88a', '#ea0606', '#36b9cc', '#f6c23e'],
        hoverBackgroundColor: ['#139d6c', '#b60909', '#2c9faf', '#f4b619'],
        hoverBorderColor: "rgba(234, 236, 244, 1)",
      }],
    },
    options: {
      maintainAspectRatio: false,
      tooltips: {
        backgroundColor: "rgb(255,255,255)",
        bodyFontColor: "#858796",
        borderColor: '#dddfeb',
        borderWidth: 1,
        xPadding: 15,
        yPadding: 15,
        displayColors: false,
        caretPadding: 10,
      },
      legend: {
        display: false
      },
      cutoutPercentage: 80,
    },
  });
}
