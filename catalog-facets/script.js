var myApp = angular.module('myApp', []);
myApp.controller('mainController', function($scope) {
  $scope.items = [{
          item:"apples", 
          flag: true
        }, {
          item:"oranges", 
          flag: true
        }, {
          item:"pears",
          flag: false 
        }];
  $scope.itemsUnchanged = angular.copy($scope.items);
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
      return (needsMet.length === $scope.items.length);
  };
  
  $scope.update = function() {
      if (angular.equals($scope.items, $scope.itemsUnchanged)) {
          return "Not Changed";
      } else {
          return "Changed";
      }
  };
  $scope.url = function(){
    var url = [];
    //url = $scope.items[1].item;
    angular.forEach($scope.items, function(key, value) {
        if(key.flag)
          url.push(key.item);
    });
    return url.toString();
  }
});
    
