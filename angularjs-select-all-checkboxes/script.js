var myApp = angular.module('myApp', []);
myApp.controller('mainController', function($scope) {
  $scope.items = [ 
          { item:1, flag: true}, 
          { item:2, flag: true}, 
          { item:3, flag: true }
  ];
  
  $scope.allNeedsClicked = function () {
      var newValue = !$scope.allNeedsMet();
      for (var i = 0; i < $scope.items.length; i++) {
          $scope.items[i].flag = newValue;
      }
  };

  $scope.allNeedsMet = function () {
      var needsMet = [];
      for (var i = 0; i < $scope.items.length; i++) {
          if ($scope.items[i].flag === true)
              needsMet.push(true);
      }
      console.log($scope.items.length, needsMet.length);
      return (needsMet.length === $scope.items.length);
  };
});
    
