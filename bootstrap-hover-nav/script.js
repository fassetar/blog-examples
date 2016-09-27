var app = angular.module('myjanney', ['ui.bootstrap', 'ui.router', 'ngAnimate']);
app.config(function($stateProvider, $urlRouterProvider, $locationProvider) {
  $urlRouterProvider.otherwise("/");
  $stateProvider
    .state('Home', {
      url: "/"
    }).state('user.accounts', {
      url: '/user/accounts',
      templateUrl: 'user/accounts.html'
    }).state('user.settings', {
      url: '/user/settings',
      controller: 'SettingsCtrl',
      templateUrl: 'user/settings/settings.html'
    }).state('user.settings.one', {
      url: '/one',
      template: '<div>Settings nested route 1</div>'
    }).state('user.settings.two', {
      url: '/two',
      template: '<div>Settings nested route 2</div>'
    });
}).run(['$rootScope', '$state', function($rootScope, $state) {

  $rootScope.$on('$stateChangeStart', function() {
    $rootScope.stateIsLoading = true;
  });

  $rootScope.$on('$stateChangeSuccess', function() {
    $rootScope.stateIsLoading = false;
  });

}]);
app.controller('mainController', function($scope, $window) {
  $scope.preview = {
    index: 0,
    flag: false
  };

  $scope.hoverIn = function(index) {
    $scope.preview.true = true;
    $scope.preview.index = index;
  };

  $scope.hoverOut = function() {
    $scope.preview.flag = false;
    return $scope.tabs.filter(function(tab) {
      if (tab.active) {
        var id = $scope.tabs.indexOf(tab);
        console.log(id);
        $scope.preview.index = id;
      }
    })[0];
  };


  //Active Click
  $scope.activeTab = function(index) {
    return $scope.tabs.filter(function(tab) {
      if (tab.active) {
        tab.active = false;
      }

      $scope.tabs[index].active = true;
    })[0];
  }

  // Display Class 
  $scope.displayTab = function(index) {
    //If not preview
    if ($scope.preview.flag) {
      if ($scope.tabs[index].active)
        return true;
    } else {
      if (index == $scope.preview.index)
        return true;
    }

    return '';
  }

  $scope.tabs = [{
    title: 'Accounts',
    subtabs: [{
      title: 'Summary',
      content: 'Dynamic content 1'
    }, {
      title: 'Balances',
      content: 'Dynamic content 2'
    }, {
      title: 'Taxs',
      content: 'Dynamic content 3'
    }, {
      title: 'Estimated Income',
      content: 'Dynamic content 8'
    }],
    active: true
  }, {
    title: 'Statements & Documenets',
    subtabs: [{
      title: 'Statements',
      content: 'Dynamic content 1'
    }, {
      title: 'Confirmations',
      content: 'Dynamic content 2'
    }, {
      title: 'Proxy & Prospectus',
      content: 'Dynamic content 3'
    }, {
      title: 'Tax Forms',
      content: 'Dynamic content 2'
    }],
    active: false
  }, {
    title: 'Research & Education',
    subtabs: [{
      title: 'Investor Eduction',
      content: 'Dynamic content 1'
    }, {
      title: 'Janney Research',
      content: 'Dynamic content 2'
    }, {
      title: 'Markets',
      content: 'Dynamic content 1'
    }, {
      title: 'S & P Commentary',
      content: 'Dynamic content 1'
    }, {
      title: 'S & P Reports',
      content: 'Dynamic content 1'
    }],
    active: false
  }, {
    title: 'Bill Pay/Rewards',
    subtabs: [{
      title: 'Bill Pay',
      content: 'Dynamic content 1'
    }, {
      title: 'Janney Rewards',
      content: 'Dynamic content 2'
    }, {
      title: 'Debit Card/Expense Report',
      content: 'Dynamic content 1'
    }],
    active: false
  }, {
    title: 'Profile',
    subtabs: [{
      title: 'Account Linking',
      content: 'Dynamic content 1'
    }, {
      title: 'Contact Settings',
      content: 'Dynamic content 2'
    }, {
      title: 'Beneficiaries',
      content: 'Dynamic content 1'
    }, {
      title: 'Interested Parties',
      content: 'Dynamic content 1'
    }],
    active: false
  }];
});