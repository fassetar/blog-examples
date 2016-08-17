angular.module('ui.bootstrap.demo', ['ui.bootstrap']);
angular.module('ui.bootstrap.demo').controller('CarouselDemoCtrl', function ($scope) {
  $scope.slides = [
      {
        icon: 'fa-university',  
        title: 'Accessioning'
      },{
        icon: 'fa-flask',
        title: 'Biofluids'
      }, {
        icon: 'fa-users',
        title: 'Managers'
      }
    ];
});