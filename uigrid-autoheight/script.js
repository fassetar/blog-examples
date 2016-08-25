 var app = angular.module("app", ["ngTouch", "ui.grid", "ui.grid.autoResize",  'ui.grid.pagination']);
    app.controller("MyCtrl", ["$scope", function ($scope) {
        $scope.gridOptions = {
            paginationPageSizes: [5, 10],
            paginationPageSize: 5,
            enablePaginationControls: true,
            data: [{
                letter: "a",
                count: 1
            }, {
                letter: "b",
                count: 2
            }, {
                letter: "c",
                count: 3
            }, {
                letter: "d",
                count: 4
            }, {
                letter: "e",
                count: 5
            }, {
                letter: "f",
                count: 6
            }],
            enableHorizontalScrollbar: 0,
            enableVerticalScrollbar: 0
        }
        
        for (var i = 0; i< Math.random(11 + 5) * 19; i++){
           $scope.gridOptions.data.push({
              letter: "a",
              count: i + 7
           });
        }
        
        $scope.gridOptions.onRegisterApi = function(gridApi){
            gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                var newHeight = (pageSize * 30) + 68;
                angular.element(document.getElementsByClassName('grid')[0]).css('height', newHeight + 'px');  
                console.log('get pageSize - ',  pageSize);
            });
        };
    }]);